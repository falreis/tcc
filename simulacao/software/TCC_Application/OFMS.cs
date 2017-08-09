using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TCC_Domain.Domain;
using TCC_Domain.Repository;
using TCC_Application.Arguments;
using System.Activities;
using TCC_Application.Workflow;
using TCC_Application.Exceptions;
using GoogleMapsApi.Entities.Directions.Response;

namespace TCC_Application
{
    public sealed class OFMS
    {
        #region Constantes

        private const string CONSTANTE_APPLICATION = "CONSTANTE_APPLICATION";
        private const double GRAU_LATITUDE_PARA_QUILOMETROS = 111.19;  //(RAIO_TERRA / (180/pi)  http://en.wikipedia.org/wiki/Geographical_distance
        private const double GRAUS_PARA_RADIANO = 0.017453;
        private const double RAIO_TERRA = 6371.009;

        private const int TOP_ROWS = 7;
        private static double[] raios = { 0, 0.7, 1.5, 2 };

        #endregion

        #region Variáveis

        private static OFMS _instance;
        private static List<Taxista> _taxistas;
        private static List<Requisicao> _requisicoes;
        private static List<Requisicao> _filaEspera;

        #endregion

        #region Propriedades

        public List<Taxista> Taxistas
        {
            get { return _taxistas; } 
        }

        public List<Requisicao> Atendimentos
        {
            get { return _requisicoes; }
        }

        #endregion

        #region Construtor

        private OFMS() { }

        #endregion

        #region Métodos Estáticos

        public static OFMS GetInstance() 
        {
            lock (typeof(OFMS))
            {
                if (_instance == null)
                {
                    _instance = new OFMS();
                    _taxistas = new List<Taxista>();
                    _requisicoes = new List<Requisicao>();
                    _filaEspera = new List<Requisicao>();
                }

                return _instance;
            }
        }

        public static void AtualizaPosicaoTaxista(Guid ID, double latitude, double longitude)
        {
            Taxista taxista = _taxistas.FirstOrDefault(taxi => taxi.ID == ID);
            if (taxista != null)
                AtualizaPosicaoTaxista(ID, latitude, longitude, taxista.Status);
            else
                AtualizaPosicaoTaxista(ID, latitude, longitude, StatusTaxista.TaxiLivre);
        }

        public static void AtualizaPosicaoTaxista(Guid ID, double latitude, double longitude, StatusTaxista status)
        {
            if (_taxistas == null)
                OFMS.GetInstance();

            lock (_taxistas)
            {
                Taxista taxista = _taxistas.FirstOrDefault(taxi => taxi.ID == ID);
                if (taxista == null)
                {
                    taxista = Taxista.Obter(ID);
                    taxista.Status = status;

                    if (taxista != null)
                        taxista.PosicaoAtual = new Localizacao();
                    else
                        throw new TaxistaNaoEncontradoException();

                    taxista.PosicaoAtual.Latitude = latitude;
                    taxista.PosicaoAtual.Longitude = longitude;
                    taxista.PosicaoAtual.Data = DateTime.Now;
                    _taxistas.Add(taxista);
                }
                else
                {
                    taxista.PosicaoAtual.Latitude = latitude;
                    taxista.PosicaoAtual.Longitude = longitude;
                    taxista.PosicaoAtual.Data = DateTime.Now;
                }
            }   
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Distância convencional em coordenada geografica
        /// </summary>
        /// <param name="latTaxista"></param>
        /// <param name="lngTaxista"></param>
        /// <param name="latCliente"></param>
        /// <param name="lngCliente"></param>
        /// <returns></returns>
        private double ObterDistanciaEuclidiana(double latTaxista, double lngTaxista, double latCliente, double lngCliente)
        {
            double fi = (latTaxista + latCliente) / 2;
            double latCli = latCliente * GRAUS_PARA_RADIANO;
            double latTax = latTaxista * GRAUS_PARA_RADIANO;
            double lngCli = lngCliente * GRAUS_PARA_RADIANO;
            double lngTax = lngTaxista * GRAUS_PARA_RADIANO;

            return RAIO_TERRA * Math.Sqrt((Math.Pow((latTax - latCli), 2)) + (Math.Pow((Math.Cos(fi) * (lngTax - lngCli)), 2)));
        }

        private IList<Taxista> FiltrarTaxistasLivresPorCirculos(Localizacao localCliente)
        {
            IList<Taxista> taxis = null;
            for (int i = 1; i < raios.Length; i++)
            {
                //filtra os taxistas de acordo através da distância euclidiana, para não efetuar muitas consultas ao sistema
                taxis = _taxistas.Where(taxi => taxi.Status == StatusTaxista.TaxiLivre &&
                            this.ObterDistanciaEuclidiana(taxi.PosicaoAtual.Latitude, taxi.PosicaoAtual.Longitude, localCliente.Latitude, localCliente.Longitude) >= raios[i - 1] &&
                            this.ObterDistanciaEuclidiana(taxi.PosicaoAtual.Latitude, taxi.PosicaoAtual.Longitude, localCliente.Latitude, localCliente.Longitude) < raios[i]).ToList();

                if (taxis.Count > 0)
                    break;
            }

            return taxis;
        }

        public Taxista ObterTaxiLivreMaisProximo(Localizacao localCliente)
        {
            IList<Taxista> taxis = this.FiltrarTaxistasLivresPorCirculos(localCliente);
            if (taxis != null && taxis.Count > 0)
            {
                //limita o número de entradas a serem processadas
                if (taxis.Count() > TOP_ROWS)
                    taxis = taxis.OrderBy(tx => this.ObterDistanciaEuclidiana(tx.PosicaoAtual.Latitude, tx.PosicaoAtual.Longitude, localCliente.Latitude, localCliente.Longitude)).Take(TOP_ROWS).ToList();

                TimeSpan minDuration = TimeSpan.MaxValue;
                Taxista taxistaMaisProximo = null;

                //busca o taxista mais próximo de acordo com as estimativas do Google API
                foreach (Taxista taxi in taxis)
                {
                    Leg response = Geolocation.ObterDirecao(taxi.PosicaoAtual.Endereco, localCliente.Endereco);
                    if (response != null)
                    {
                        if(response.Duration.Value < minDuration)
                        {
                            taxistaMaisProximo = taxi;
                            minDuration = response.Duration.Value;
                        }
                    }
                }

                return taxistaMaisProximo;
            }
            return null;
        }

        public bool ExistsRequisicao(Guid stakeholderID)
        {
            return this.ExistsRequisicao(stakeholderID, null);
        }

        public Requisicao ObterRequisicao(Guid stakeholderID)
        {
            return _requisicoes.FirstOrDefault(a => a.Cliente.ID == stakeholderID || a.Taxista.ID == stakeholderID);
        }

        private bool ExistsRequisicao(Guid stakeholderID, StatusRequisicao? status)
        {
            if(!status.HasValue)
                return _requisicoes.Exists(a => a.Cliente.ID == stakeholderID || a.Taxista.ID == stakeholderID);
            else
                return _requisicoes.Exists(a => a.Status == status && (a.Cliente.ID == stakeholderID || a.Taxista.ID == stakeholderID));
        }

        public DirectionsPoco ObterPrevisaoAtendimento(Guid stakeholderID)
        {
            Requisicao requisicao = this.ObterRequisicao(stakeholderID);
            if (requisicao != null)
            {
                Leg rota = Geolocation.ObterDirecao(requisicao.Taxista.PosicaoAtual.Endereco, requisicao.Cliente.PosicaoAtual.Endereco);
                if (rota != null && rota.Duration != null)
                {
                    return new DirectionsPoco(){
                                    Duration = rota.Duration.Value,
                                    //DurationInTraffic = (rota.DurationInTraffic != null? rota.DurationInTraffic.Value : TimeSpan.MaxValue),
                                    DistanceInMeters = rota.Distance.Value,
                                    STATUS_CODE = "OK"
                                };
                }
            }
            return null;
        }

        #region Requisição "Realizada"

        private Guid DefinirAtendimento(Requisicao atendimento, MetodoBuscaTaxi metodo)
        {
            TimeSpan time = new TimeSpan();
            return DefinirAtendimento(atendimento, metodo, out time);
        }

        private Guid DefinirAtendimento(Requisicao atendimento, MetodoBuscaTaxi metodo, out TimeSpan processTime)
        {
            processTime = new TimeSpan();
            DateTime beginTime;

            Taxista taxista = null;
            switch (metodo)
            {
                case MetodoBuscaTaxi.GPS:
                    beginTime = DateTime.Now;
                    taxista = this.ObterTaxiLivreMaisProximo(atendimento.Cliente.PosicaoAtual);
                    processTime = DateTime.Now.Subtract(beginTime);
                    break;
                case MetodoBuscaTaxi.BROADCASTING:
                    beginTime = DateTime.Now;
                    taxista = this.ObterTaxiLivrePeloMetodoBroadcasting(atendimento.Cliente.PosicaoAtual);
                    processTime = DateTime.Now.Subtract(beginTime);
                    break;
                case MetodoBuscaTaxi.DISTANCIA_EUCLIDIANA:
                    beginTime = DateTime.Now;
                    taxista = this.ObterTaxiLivrePorDistanciaEuclidiana(atendimento.Cliente.PosicaoAtual);
                    processTime = DateTime.Now.Subtract(beginTime);
                    break;
                case MetodoBuscaTaxi.OTIMIZACAO:
                    break;
                default:
                    taxista = this.ObterTaxiLivreMaisProximo(atendimento.Cliente.PosicaoAtual);
                    break;
            }

            if (taxista != null && _filaEspera.Count <= 0)
            {
                taxista.Status = StatusTaxista.EmAtendimento;
                atendimento.Status = StatusRequisicao.RequisicaoRealizada;

                //libera o taxista anterior (caso onde o taxista escolhido anteriormente recusou)
                if(atendimento.Taxista != null)
                    atendimento.Taxista.Status = StatusTaxista.TaxiLivre;

                atendimento.Taxista = taxista;

                //adiciona a lista de atendimentos
                _requisicoes.Add(atendimento);
            }
            else
            {
                //se não houver taxistas, a requisição aguarda na fila
                atendimento.Status = StatusRequisicao.EmProcessamento;
                
                //libera o taxista anterior e define remove taxista da responsabilidade
                if (atendimento.Taxista != null)
                {
                    atendimento.Taxista.Status = StatusTaxista.TaxiLivre;
                    atendimento.Taxista = null;
                }

                _filaEspera.Add(atendimento);       //adiciona a fila de espera

                throw new NenhumTaxiParaAtenderRequisicaoException();
            }
            return atendimento.ID;
        }

        public Guid IniciarRequisicao(Guid clienteID, double clienteLatitude, double clienteLongitude)
        {
            TimeSpan time = new TimeSpan();
            return this.IniciarRequisicao(clienteID, clienteLatitude, clienteLongitude, MetodoBuscaTaxi.GPS, out time);
        }

        public Guid IniciarRequisicao(Guid clienteID, double clienteLatitude, double clienteLongitude, MetodoBuscaTaxi metodo, out TimeSpan processTime)
        {
            Cliente cliente = new ClienteRepository().Obter(clienteID);

            if (cliente != null)
            {
                Requisicao atendimento = new Requisicao();
                atendimento.ID = Guid.NewGuid();
                atendimento.Cliente = cliente;

                atendimento.Cliente.PosicaoAtual = new Localizacao();
                atendimento.Cliente.PosicaoAtual.Latitude = clienteLatitude;
                atendimento.Cliente.PosicaoAtual.Longitude = clienteLongitude;
                atendimento.Cliente.PosicaoAtual.Data = DateTime.Now;

                atendimento.DataRequisicao = DateTime.Now;

                return this.DefinirAtendimento(atendimento, metodo, out processTime);
            }
            else
                throw new ClienteNaoEncontradoException();
        }

        public Requisicao GetRequisicaoRealizada(Guid stakeholderID)
        {
            return _requisicoes.FirstOrDefault(a => a.Status == StatusRequisicao.RequisicaoRealizada && (a.Taxista.ID == stakeholderID || a.Cliente.ID == stakeholderID));
        }

        #endregion

        #region Requisição "Em Processamento"

        public void IniciarRequisicaoEmProcessamento()
        {
            if (_filaEspera.Count > 0)
            {
                Requisicao requisicao;

                requisicao = _filaEspera.ElementAt(0);
                _filaEspera.RemoveAt(0);

                this.DefinirAtendimento(requisicao, MetodoBuscaTaxi.GPS);
            }
        }

        public bool ExistsRequisicaoEmProcessamento(Guid clienteID)
        {
            Requisicao requisicao = _filaEspera.FirstOrDefault(a => a.Cliente.ID == clienteID);
            return (requisicao != null);
        }

        #endregion

        #region Requisição "Aguardando Resposta"

        public void AceitarRequisicao(Guid taxistaID)
        {
            Requisicao atendimento = _requisicoes.FirstOrDefault(a => a.Status == StatusRequisicao.RequisicaoRealizada && a.Taxista.ID == taxistaID);
            if (atendimento != null)
                atendimento.Status = StatusRequisicao.AguardandoResposta;
            else
                throw new RequisicaoNaoEncontradaException();
        }

        public void RecusarAtendimento(Guid taxistaID)
        {
            Requisicao atendimento = _requisicoes.FirstOrDefault(a => a.Status == StatusRequisicao.RequisicaoRealizada && a.Taxista.ID == taxistaID);
            if (atendimento != null)
            {
                _requisicoes.Remove(atendimento);
                atendimento.Status = StatusRequisicao.RequisicaoRealizada;

                this.DefinirAtendimento(atendimento, MetodoBuscaTaxi.GPS);
            }
            else
            {
                if (atendimento == null)
                    throw new RequisicaoNaoEncontradaException();
                else
                    throw new TaxistaNaoEncontradoException();
            }
        }

        #endregion

        #region Requisição "Aguardando Confirmação"

        public bool ExistsRequisicaoAguardandoConfirmacao(Guid stakeholderID)
        {
            return _requisicoes.Exists(a => a.Status == StatusRequisicao.AguardandoResposta && (a.Cliente.ID == stakeholderID || a.Taxista.ID == stakeholderID));
        }

        public void ConfirmarRequisicao(Guid clienteID)
        {
            Requisicao atendimento = _requisicoes.FirstOrDefault(a => a.Status == StatusRequisicao.AguardandoResposta && a.Cliente.ID == clienteID);
            if (atendimento != null)
                atendimento.Status = StatusRequisicao.AguardandoAtendimento;
            else
                throw new RequisicaoNaoEncontradaException();
        }

        #endregion

        #region Requisição "Aguardando Atendimento"

        public bool ExistsRequisicaoAguardandoAtendimento(Guid stakeholderID)
        {
            return _requisicoes.Exists(a => a.Status == StatusRequisicao.AguardandoAtendimento && (a.Taxista.ID == stakeholderID || a.Cliente.ID == stakeholderID));
        }

        #endregion

        #region Requisição "Em Atendimento"

        public void IniciarAtendimento(Requisicao requisicao)
        {
            if (requisicao != null)
            {
                requisicao.Status = StatusRequisicao.EmAtendimento;
                requisicao.DataInicio = DateTime.Now;
            }
            else
                throw new RequisicaoNaoEncontradaException();
        }

        public void IniciarAtendimento(Guid stakeholderID)
        {
            Requisicao requisicao = _requisicoes.FirstOrDefault(a => a.Status == StatusRequisicao.AguardandoAtendimento && (a.Cliente.ID == stakeholderID || (a.Taxista != null && a.Taxista.ID == stakeholderID)));
            this.IniciarAtendimento(requisicao);
        }

        public bool ExistsRequisicaoEmAtendimento(Guid stakeholderID)
        {
            return _requisicoes.Exists(a => a.Status == StatusRequisicao.EmAtendimento && (a.Cliente.ID == stakeholderID || a.Taxista.ID == stakeholderID));
        }

        #endregion

        #region Requisição "Concluída"

        public void ConcluirRequisicao(Requisicao requisicao)
        {
            if (requisicao != null)
            {
                requisicao.Status = StatusRequisicao.Concluido;
                requisicao.DataFim = DateTime.Now;
                //requisicao.Salvar();

                _requisicoes.Remove(requisicao);
                requisicao.Taxista.Status = StatusTaxista.TaxiLivre;

                //verificar a lista de espera para ver se há atendimentos aguardando
                this.IniciarRequisicaoEmProcessamento();
            }
        }

        public void ConcluirRequisicao(Guid stakeholderID)
        {
            Requisicao requisicao = _requisicoes.FirstOrDefault(a => a.Status == StatusRequisicao.EmAtendimento && (a.Cliente.ID == stakeholderID || (a.Taxista != null && a.Taxista.ID == stakeholderID)));
            this.ConcluirRequisicao(requisicao);
        }

        #endregion

        #region Requisição "Cancelada" 

        public void CancelarRequisicao(Guid stakeholderID)
        {
            Requisicao requisicao = _requisicoes.FirstOrDefault(a => a.Cliente.ID == stakeholderID || (a.Taxista != null && a.Taxista.ID == stakeholderID));
            if (requisicao != null)
            {
                this.CancelarRequisicao(requisicao, false);
            }
            else
            {
                //procura requisicao na fila de espera
                requisicao = _filaEspera.FirstOrDefault(a => a.Cliente.ID == stakeholderID || (a.Taxista != null && a.Taxista.ID == stakeholderID));
                if (requisicao != null)
                    this.CancelarRequisicao(requisicao, true);
            }
        }

        private void CancelarRequisicao(Requisicao requisicao, bool fila_espera)
        {
            if (requisicao != null)
            {
                if (!fila_espera)       //atendimentos em curso
                {
                    requisicao.Status = StatusRequisicao.Cancelado;
                    requisicao.DataFim = DateTime.Now;
                    //atendimento.Salvar();

                    _requisicoes.Remove(requisicao);
                    requisicao.Taxista.Status = StatusTaxista.TaxiLivre;
                }
                else
                {
                    requisicao.Status = StatusRequisicao.Cancelado;
                    _filaEspera.Remove(requisicao);
                }
            }
        }

        #endregion

        #region Métodos de Teste (Avaliação da Solução)

        public void AlterarStatusTaxistaAleatorio()
        {
            const double PROBABILIDADE_MODIFICACAO_STATUS_TAXISTAS = 0.1; //número máximo de modificações = 10%

            lock (_taxistas)
            {
                Random rand = new Random();
                int number_changes = rand.Next((int)(_taxistas.Count * PROBABILIDADE_MODIFICACAO_STATUS_TAXISTAS));

                for (int i = 0; i < number_changes; i++)
                {
                    Taxista taxi = _taxistas.ElementAt(rand.Next(_taxistas.Count()));
                    taxi.Status = (taxi.Status == StatusTaxista.TaxiLivre) ? StatusTaxista.EmAtendimento : StatusTaxista.TaxiLivre;
                }
            }
        }

        public void MovimentarTaxistasAleatorio()
        {
            const double CONSTANTE_DESLOCAMENTO_TAXISTA = 0.0001;
            const double PROBABILIDADE_MOVIMENTACAO_DIRECAO_NORTE_LATITUDE = 0.5;
            const double PROBABILIDADE_MOVIMENTACAO_DIRECAO_LESTE_LONGITUDE = 0.5;
            const double PROBAILIDADE_TAXISTA_MOVIMENTAR = 0.9;

            Random rand_pos = new Random();

            lock (_taxistas)
            {
                for (int i = 0; i < _taxistas.Count; i++)
                {
                    Taxista taxi = _taxistas.ElementAt(i);

                    ////reposiciona o taxista pela cidade
                    //int direcao_latitude = (rand.NextDouble() > PROBABILIDADE_MOVIMENTACAO_DIRECAO_NORTE_LATITUDE) ? 1 : -1;
                    //int direcao_longitude = (rand.NextDouble() > PROBABILIDADE_MOVIMENTACAO_DIRECAO_LESTE_LONGITUDE) ? 1 : -1;

                    //taxi.PosicaoAtual.Latitude += (rand.NextDouble() * CONSTANTE_DESLOCAMENTO_TAXISTA) * direcao_latitude;
                    //taxi.PosicaoAtual.Longitude += (rand.NextDouble() * CONSTANTE_DESLOCAMENTO_TAXISTA) * direcao_longitude;

                    if (rand_pos.NextDouble() < PROBAILIDADE_TAXISTA_MOVIMENTAR)
                    {
                        double direcao_latitude = (rand_pos.NextDouble() > PROBABILIDADE_MOVIMENTACAO_DIRECAO_NORTE_LATITUDE) ? rand_pos.NextDouble() : -rand_pos.NextDouble();
                        double direcao_longitude = (rand_pos.NextDouble() > PROBABILIDADE_MOVIMENTACAO_DIRECAO_LESTE_LONGITUDE) ? rand_pos.NextDouble() : -rand_pos.NextDouble();

                        taxi.PosicaoAtual.Latitude += rand_pos.NextDouble() * direcao_latitude * CONSTANTE_DESLOCAMENTO_TAXISTA;
                        taxi.PosicaoAtual.Longitude += rand_pos.NextDouble() * direcao_longitude * CONSTANTE_DESLOCAMENTO_TAXISTA;
                    }
                }
            }
        }

        public Taxista ObterTaxiLivrePeloMetodoBroadcasting(Localizacao localCliente)
        {
            IList<Taxista> taxis = this.FiltrarTaxistasLivresPorCirculos(localCliente);
            if (taxis != null && taxis.Count > 0)
            {
                //limita o número de entradas a serem processadas
                if (taxis.Count > TOP_ROWS)
                    taxis = taxis.OrderBy(tx => this.ObterDistanciaEuclidiana(tx.PosicaoAtual.Latitude, tx.PosicaoAtual.Longitude, localCliente.Latitude, localCliente.Longitude)).Take(TOP_ROWS).ToList();
                return taxis.ElementAt(new Random().Next(taxis.Count - 1));
            }
            return null;
        }

        public Taxista ObterTaxiLivrePorDistanciaEuclidiana(Localizacao localCliente)
        {
            IList<Taxista> taxis = this.FiltrarTaxistasLivresPorCirculos(localCliente);
            if (taxis != null && taxis.Count > 0)
                return taxis.OrderBy(tx => this.ObterDistanciaEuclidiana(tx.PosicaoAtual.Latitude, tx.PosicaoAtual.Longitude, localCliente.Latitude, localCliente.Longitude)).First();

            return null;
        }

        #endregion

        #endregion
    }
}
