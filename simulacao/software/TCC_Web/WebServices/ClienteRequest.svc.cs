using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TCC_Application;
using TCC_Application.Exceptions;

namespace TCC_Web.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClienteRequest" in code, svc and config file together.
    public class ClienteRequest : IClienteRequest
    {
        public DirectionsPoco RequisitarTaxi(Guid codigoCliente, double latitude, double longitude, MetodoBuscaTaxi metodo)
        {
            try
            {
                TimeSpan processTime = new TimeSpan();

                OFMS ofms = OFMS.GetInstance();
                ofms.IniciarRequisicao(codigoCliente, latitude, longitude, metodo, out processTime);

                DirectionsPoco poco = ofms.ObterPrevisaoAtendimento(codigoCliente);
                ofms.CancelarRequisicao(codigoCliente);

                if (poco != null)
                    poco.ProcessTime = processTime;

                return poco;
            }
            catch(GoogleMapsApiException gmaex)
            {
                return new DirectionsPoco() { STATUS_CODE = gmaex.Message };
            }
        }

        //public TimeSpan RequisitarTaxiPorBroadcasting(Guid codigoCliente, double latitude, double longitude)
        //{
        //    OFMS ofms = OFMS.GetInstance();
        //    ofms.IniciarRequisicao(codigoCliente, latitude, longitude);

        //    return ofms.ObterPrevisaoAtendimento(codigoCliente);
        //}

        public void AlterarStatusTaxistaAleatorio()
        {
            OFMS ofms = OFMS.GetInstance();
            ofms.AlterarStatusTaxistaAleatorio();
        }

        public void MovimentarTaxistas()
        {
            OFMS ofms = OFMS.GetInstance();
            ofms.MovimentarTaxistasAleatorio();
        }
    }
}
