using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TCC_Web.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITaxistaLocation" in both code and config file together.
    [ServiceContract]
    public interface ITaxistaLocation
    {
        [OperationContract]
        void IniciarPosicaoComStatusAleatorio(Guid ID, double latitude, double longitude);
    }
}
