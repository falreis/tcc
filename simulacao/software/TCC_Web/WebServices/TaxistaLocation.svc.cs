using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TCC_Application;
using TCC_Domain.Domain;

namespace TCC_Web.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TaxistaLocation" in code, svc and config file together.
    public class TaxistaLocation : ITaxistaLocation
    {
        public void IniciarPosicaoComStatusAleatorio(Guid ID, double latitude, double longitude)
        {
            double value = new Random().NextDouble();
            if(value > 0.5)
                OFMS.AtualizaPosicaoTaxista(ID, latitude, longitude, StatusTaxista.TaxiLivre);
            else
                OFMS.AtualizaPosicaoTaxista(ID, latitude, longitude, StatusTaxista.EmAtendimento);
        }

        public void AtualizarPosicao(Guid ID, double latitude, double longitude)
        {
            OFMS.AtualizaPosicaoTaxista(ID, latitude, longitude);
        }
    }
}
