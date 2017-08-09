using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Domain;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Directions.Request;
using TCC_Application.Exceptions;

namespace TCC_Application
{
    public class Geolocation
    {
        public static Localizacao ObterLocalizacao(string endereco)
        {
            GeocodingRequest request = new GeocodingRequest();
            request.Address = endereco;
            GeocodingResponse response = GoogleMaps.Geocode.Query(request);

            if (response != null)
            {
                Result resultado = response.Results.FirstOrDefault();
                if (resultado != null && resultado.Geometry != null && resultado.Geometry.Location != null)
                {
                    Localizacao localizacao = new Localizacao();
                    localizacao.Latitude = resultado.Geometry.Location.Latitude;
                    localizacao.Longitude = resultado.Geometry.Location.Longitude;
                    localizacao.Data = DateTime.Now;

                    return localizacao;
                }
            }
            return null;
        }
        
        public static Leg ObterDirecao(double latTaxista, double lngTaxista, double latCliente, double lngCliente)
        {
            DirectionsRequest request = new DirectionsRequest();
            request.Origin = Localizacao.ObterEndereco(latTaxista, lngTaxista);
            request.Destination = Localizacao.ObterEndereco(latCliente, lngCliente);
            //request.TravelMode = TravelMode.Transit;
            //request.DepartureTime = DateTime.Now;

            try
            {
                DirectionsResponse response = GoogleMapsApi.GoogleMaps.Directions.Query(request);
                return response.Routes.First().Legs.First();
            }
            catch { }

            return null;
        }

        public static Leg ObterDirecao(string origem, string destino)
        {
            DirectionsRequest request = new DirectionsRequest();
            request.Origin = origem;
            request.Destination = destino;
            //request.TravelMode = TravelMode.Transit;
            //request.DepartureTime = DateTime.Now;
            
            DirectionsResponse response = GoogleMapsApi.GoogleMaps.Directions.Query(request);
            if (response.Status != DirectionsStatusCodes.OK)
                throw new GoogleMapsApiException(response.StatusStr);
            return response.Routes.First().Legs.First();

            return null;
        }
    }
}
