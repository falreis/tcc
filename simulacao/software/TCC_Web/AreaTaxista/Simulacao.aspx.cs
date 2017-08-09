using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Geocoding.Response;
using TCC_Domain.Domain;

namespace TCC_Web.AreaTaxista
{
    public partial class Simulacao : PageBase
    {
        #region Propriedades

        #endregion

        #region Eventos

        protected void Page_Init(Object sender, EventArgs e)
        {
            base.OnInit(e);
            btnTestar.Click += new EventHandler(btnTestar_Click);
            btnCalcularDistancia.Click += new EventHandler(btnCalcularDistancia_Click);
            this.btnTestarHibernate.Click += new EventHandler(btnTestarHibernate_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnTestar_Click(Object sender, EventArgs e)
        {
            var geocodingRequest = new GeocodingRequest{
                Location = new Location(-19.904356, -43.925691)
            };

            GeocodingResponse geocodingResponse = GoogleMaps.Geocode.Query(geocodingRequest);
            if (geocodingResponse != null && geocodingResponse.Status == Status.OK)
            {
                var drivingDirectionRequest = new DirectionsRequest
                {
                    Origin = ObterEndereco(geocodingResponse),
                    Destination = "Avenida Amazonas 7000, Belo Horizonte, MG, Brazil",
                    Sensor = false,
                    Alternatives = false
                };

                DirectionsResponse drivingDirections = GoogleMaps.Directions.Query(drivingDirectionRequest);
                if (drivingDirections != null && drivingDirections.Status == DirectionsStatusCodes.OK)
                    lblDistancia.Text = string.Format("Distância Total: {0} m.", ObterDistanciaTotal(drivingDirections).ToString());
            }
        }

        protected void btnCalcularDistancia_Click(Object sender, EventArgs ea)
        {
        }

        [System.Web.Services.WebMethod]
        public static string Teste()
        {
            List<Employee> eList = new List<Employee>();
            Employee e = new Employee();
            e.Name = "Minal";
            e.Age = 24;

            eList.Add(e);

            e = new Employee();
            e.Name = "Santosh";
            e.Age = 24;

            eList.Add(e);

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return "{\"Employee\":" + oSerializer.Serialize(eList) + "}";
        }

        protected void btnTestarHibernate_Click(object sender, EventArgs e)
        {
            Endereco endereco = new Endereco();
            endereco.CEP = "31030410";
            endereco.Cidade = "Belo Horizonte";
            endereco.Complemento = "402";
            endereco.Estado = UF.MinasGerais;
            endereco.Logradouro = "Rua A";
            endereco.Numero = "255";
            endereco.Referencia = "sem referencias";
            endereco.Salvar();
        }

        #endregion

        #region Métodos

        public double ObterDistanciaTotal(DirectionsResponse response)
        {
            double distancia_total = 0;

            foreach (Route rota in response.Routes)
                foreach (Leg leg in rota.Legs)
                    distancia_total += Convert.ToInt32(leg.Distance.Value);

            return distancia_total;
        }

        public string ObterEndereco(GeocodingResponse response)
        {
            if (response.Results != null)
                return response.Results.FirstOrDefault().FormattedAddress;

            return String.Empty;
        }

        #endregion
    }

    public class Employee
    {
        public string Name;
        public int Age;
    }
}