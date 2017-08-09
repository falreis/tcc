using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Domain;

namespace TCC_Application
{
    public static class WebMethods
    {
        public static string GetAtendimento(Guid stakeholderID)
        {
            OFMS ofms = OFMS.GetInstance();
            Requisicao requisicao = ofms.ObterRequisicao(stakeholderID);
            if (requisicao != null)
            {
                return string.Format("{0}|{1}|{2}|{3}",
                        requisicao.Taxista.PosicaoAtual.Latitude.ToString().Replace(",", "."), requisicao.Taxista.PosicaoAtual.Longitude.ToString().Replace(",", "."),
                        requisicao.Cliente.PosicaoAtual.Latitude.ToString().Replace(",", "."), requisicao.Cliente.PosicaoAtual.Longitude.ToString().Replace(",", ".")
                    );
            }
            return String.Empty;
        }

        public static string GetLocation(Guid taxistaID)
        {
            OFMS ofms = OFMS.GetInstance();
            Taxista taxista = ofms.Taxistas.FirstOrDefault(taxi => taxi.ID == taxistaID);

            if (taxista != null && taxista.PosicaoAtual != null)
                return string.Format("{0}|{1}", taxista.PosicaoAtual.Latitude.ToString().Replace(",", "."), taxista.PosicaoAtual.Longitude.ToString().Replace(",", "."));

            return String.Empty;
        }
    }
}
