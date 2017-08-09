using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TCC_Application;

namespace TCC_Web.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IClienteRequest" in both code and config file together.
    [ServiceContract]
    public interface IClienteRequest
    {
        [OperationContract]
        DirectionsPoco RequisitarTaxi(Guid codigoCliente, double latitude, double longitude, MetodoBuscaTaxi metodo);

        [OperationContract]
        void AlterarStatusTaxistaAleatorio();

        [OperationContract]
        void MovimentarTaxistas();
    }
}
