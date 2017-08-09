using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMapsApi.Entities.Common;
using System.Globalization;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class Localizacao
    {
        #region Atributos

        private Guid _ID;
        private DateTime _data;
        private Rastreador _rastreador;
        private double _latitude;
        private double _longitude;
        private string _endereco;

        #endregion

        #region Propriedades

        public virtual Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public virtual Rastreador Rastreador
        {
            get { return _rastreador; }
            set { _rastreador = value; }
        }

        public virtual double Latitude 
        {
            get { return _latitude; }
            set 
            {
                _endereco = null;
                _latitude = value; 
            } 
        }

        public virtual double Longitude 
        {
            get { return _longitude; }
            set 
            {
                _endereco = null;
                _longitude = value; 
            }
        }

        public virtual string LocationString
		{
			get
			{
				return Latitude.ToString(CultureInfo.InvariantCulture) + "," + Longitude.ToString(CultureInfo.InvariantCulture);
			}
		}

        public virtual string Endereco
        {
            get 
            {
                if (_endereco == null)
                    _endereco = ObterEndereco(this._latitude, this._longitude);
                return _endereco; 
            }
            set { _endereco = value; }
        }

        #endregion

        #region Repositório

        private LocalizacaoRepository Repositorio
        {
            get { return new LocalizacaoRepository(); }
        }

        #endregion

        #region Métodos

        public Localizacao() 
        {
            this._ID = Guid.NewGuid();
            this._data = DateTime.Now;
        }

        public Localizacao(double latitude, double longitude)
        {
            this._ID = Guid.NewGuid();
            this._data = DateTime.Now;

            this._latitude = latitude;
            this._longitude = longitude;
        }

        public static String ObterEndereco(double latitude, double longitude)
        {
            var geocodingRequest = new GeocodingRequest { Location = new Location(latitude, longitude) };
            GeocodingResponse geocodingResponse = GoogleMaps.Geocode.Query(geocodingRequest);

            if (geocodingResponse.Results != null)
                if(geocodingResponse.Results.FirstOrDefault() != null)
                    return geocodingResponse.Results.FirstOrDefault().FormattedAddress;

            return String.Empty;
        }

        #endregion

    }
}
