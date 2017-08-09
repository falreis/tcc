using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Domain;
using System.Threading;
using TCC_RequestSimulator.TaxistaLocation;
using System.Xml;

namespace TCC_RequestSimulator
{
    class Program
    {
        private static Random rand_pos = new Random();

        #region Constantes

        private const double CONSTANTE_DISTANCIA_MAXIMA_INICIAL_NOVO_PONTO = 0.03;
        private const double PROBABILIDADE_MOVIMENTACAO_DIRECAO_NORTE_LATITUDE = 0.5;
        private const double PROBABILIDADE_MOVIMENTACAO_DIRECAO_LESTE_LONGITUDE = 0.5;

        private const int NUMERO_CLIENTES = 32;
        private const int NUMERO_TAXISTAS = 300;
        private const int TEMPO_ENTRE_REQUISICOES = 3453; // = 230; 3453;

        private const double LATITUDE_BASE = -19.918001;        //londres (51.50169)
        private const double LONGITUDE_BASE = -43.934455;       //londres (-0.123253)

        #endregion

        #region Atributos Estáticos ("Variáveis Globais")

        private static IList<Cliente> clientes;
        private static IList<Taxista> taxistas;
        private static Timer timer;

        private static int contador = 0;

        #endregion

        static void Main(string[] args)
        {
            contador = 0;

            Console.WriteLine("Processando..");

            //carrega clientes e taxistas
            CarregarTaxistas();
            CarregarClientes();

            Console.WriteLine("Aguarde..");
            Thread.Sleep(2000);

            Console.Clear();
            Console.WriteLine("Aperte ENTER para iniciar..");
            Console.ReadLine();
            Console.Clear();

            //processa as requisições de clientes
            timer = new Timer(IniciarRequisicao, null, 0, TEMPO_ENTRE_REQUISICOES);

            Console.WriteLine("HORA;;GPS-T;GPS-D;GPS-P;;BRC-T;BRC-D;BRC-P;;EUC-T;EUC-D;EUC-P;;");
            Console.ReadLine();
            Console.ReadLine();

            //Console.WriteLine("Início");
            //for (int i = 0; i < 250; i++)
            //{
            //    Localizacao local = DefinirPosicao();
            //    Console.WriteLine(ObterDistanciaEuclidiana(local.Latitude, local.Longitude, LATITUDE_BASE, LONGITUDE_BASE));
            //}
            //Console.ReadLine();
        }

        private static void IniciarRequisicao(Object o)
        {
            if (++contador > 4 && new Random().NextDouble() <= 0.3)
            {
                contador = 0;
                if (clientes != null && clientes.Count > 0)
                {
                    Cliente cli = clientes.ElementAt(0);
                    cli.PosicaoAtual = DefinirPosicao();

                    try
                    {
                        ClienteRequest.ClienteRequestClient request = new ClienteRequest.ClienteRequestClient();
                        Console.Write(DateTime.Now.ToShortTimeString() + ";|;");

                        for (int metodo = 1; metodo <= 3; metodo++)
                        {
                            ClienteRequest.DirectionsPoco directions = request.RequisitarTaxi(cli.ID, cli.PosicaoAtual.Latitude, cli.PosicaoAtual.Longitude, (ClienteRequest.MetodoBuscaTaxi)metodo);

                            if (!string.IsNullOrEmpty(directions.STATUS_CODE) && directions.STATUS_CODE == "OK")
                                Console.Write(string.Format("{0};{1};{2};;", directions.Duration.ToString(), directions.DistanceInMeters, directions.ProcessTime.TotalMilliseconds));
                            else
                                Console.Write(string.Format("ERRO: {0}", directions.STATUS_CODE));

                            Thread.Sleep(543);
                        }
                        Console.WriteLine();

                        request.MovimentarTaxistas();
                        request.AlterarStatusTaxistaAleatorio();
                    }
                    catch { }   //Requição não completada devido a falta de taxi
                    finally
                    {
                        clientes.RemoveAt(0);
                    }
                }
                else
                {
                    timer.Dispose();

                    Console.WriteLine();
                    Console.Write("Aperte ENTER para finalizar..");
                }
            }
        }

        private static void CarregarClientes()
        {
            clientes = Cliente.ListarTodos().OrderBy(cli => cli.Pessoa.NomeOuRazaoSocial).Take(NUMERO_CLIENTES).ToList();
        }

        private static void CarregarTaxistas()
        {
            taxistas = Taxista.ListarTodos().OrderBy(tx => tx.Pessoa.NomeOuRazaoSocial).Take(NUMERO_TAXISTAS).ToList();

            for(int i = 0; i < taxistas.Count; i++)
            {
                taxistas[i].PosicaoAtual = DefinirPosicao();

                TaxistaLocationClient taxista_location = new TaxistaLocationClient();
                taxista_location.IniciarPosicaoComStatusAleatorio(taxistas[i].ID, taxistas[i].PosicaoAtual.Latitude, taxistas[i].PosicaoAtual.Longitude);
            }
        }

        private static Localizacao DefinirPosicao()
        {
            //define a direção para onde será colocado o próximo ponto
            double direcao_latitude = (rand_pos.NextDouble() > PROBABILIDADE_MOVIMENTACAO_DIRECAO_NORTE_LATITUDE) ? rand_pos.NextDouble() : -rand_pos.NextDouble();
            double direcao_longitude = (rand_pos.NextDouble() > PROBABILIDADE_MOVIMENTACAO_DIRECAO_LESTE_LONGITUDE) ? rand_pos.NextDouble() : -rand_pos.NextDouble();

            double incremento_latitude = rand_pos.NextDouble() * direcao_latitude * CONSTANTE_DISTANCIA_MAXIMA_INICIAL_NOVO_PONTO;
            double incremento_longitude = rand_pos.NextDouble() * direcao_longitude * CONSTANTE_DISTANCIA_MAXIMA_INICIAL_NOVO_PONTO;

            //define a posição inicial do taxi
            return new Localizacao(LATITUDE_BASE + incremento_latitude, LONGITUDE_BASE + incremento_longitude);
        }


        private static double ObterDistanciaEuclidiana(double latTaxista, double lngTaxista, double latCliente, double lngCliente)
        {
            const double GRAUS_PARA_RADIANO = 0.017453;
            const double RAIO_TERRA = 6371.009;

            double fi = (latTaxista + latCliente) / 2;
            double latCli = latCliente * GRAUS_PARA_RADIANO;
            double latTax = latTaxista * GRAUS_PARA_RADIANO;
            double lngCli = lngCliente * GRAUS_PARA_RADIANO;
            double lngTax = lngTaxista * GRAUS_PARA_RADIANO;

            return RAIO_TERRA * Math.Sqrt((Math.Pow((latTax - latCli), 2)) + (Math.Pow((Math.Cos(fi) * (lngTax - lngCli)), 2)));
        }
    }
}
