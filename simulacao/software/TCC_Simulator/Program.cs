using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TCC_Domain.Domain;
using TCC_Simulator.WebService;

namespace TCC_Simulator
{
    public class Program
    {
        const double LATITUDE_BASE = -19.904356;
        const double LONGITUDE_BASE = -43.925691;

        static IList<Taxista> taxistas;

        static Random random;
        static Random rand_pos;

        public static void Main(string[] args)
        {
            random = new Random(DateTime.Now.Millisecond);
            rand_pos = new Random(DateTime.Now.Millisecond + DateTime.Now.Minute);

            //Obtem a lista de todos os taxistas
            taxistas = Taxista.ListarTodos();
            //taxistas = new List<Taxista>();
            //taxistas.Add(Taxista.Obter(Guid.Parse("9eb7a533-6c2c-43a4-b6b1-e9e085b0c3e0")));
            Console.WriteLine("\n\n\n\n\n\n\n");

            //Realiza a inserção de informações no mapa a cada 10 segundos
            Timer t = new Timer(TimerCallback, null, 0, 100);

            //aguarda uma tecla para finalizar
            Console.ReadLine();
        }


        private static void TimerCallback(Object o)
        {
            int index = random.Next(taxistas.Count);

            //define a direção para onde será colocado o próximo ponto
            int direcao_latitude = (rand_pos.NextDouble() > 0.5) ? 1 : -1;
            int direcao_longitude = (rand_pos.NextDouble() > 0.5) ? 1 : -1;

            //se o taxi ainda não possui uma posição atual
            if (taxistas[index].PosicaoAtual == null)
            {
                const int CONSTANTE_DISTANCIA_MAXIMA_INICIAL_NOVO_PONTO = 20;
                double incremento_latitude = rand_pos.NextDouble() * direcao_latitude / CONSTANTE_DISTANCIA_MAXIMA_INICIAL_NOVO_PONTO;
                double incremento_longitude = rand_pos.NextDouble() * direcao_longitude / CONSTANTE_DISTANCIA_MAXIMA_INICIAL_NOVO_PONTO;

                //define a posição inicial do taxi
                taxistas[index].PosicaoAtual = new Localizacao(LATITUDE_BASE + incremento_latitude, LONGITUDE_BASE + incremento_longitude);
            }
            else
            {
                //atualiza a posição atual do taxi
                taxistas[index].PosicaoAtual.Latitude += 0.0005 * direcao_latitude;
                taxistas[index].PosicaoAtual.Longitude += 0.0005 * direcao_longitude;
            }

            Console.WriteLine(string.Format("Placa: {0}  > lat: {1} ; lng:{2}", taxistas[index].Veiculo.Placa, taxistas[index].PosicaoAtual.Latitude, taxistas[index].PosicaoAtual.Longitude));

            TaxistaLocationClient taxista_location = new TaxistaLocationClient();
            taxista_location.AtualizarPosicao(taxistas[index].ID, taxistas[index].PosicaoAtual.Latitude, taxistas[index].PosicaoAtual.Longitude);
            //GC.Collect();
        }
    }
}
