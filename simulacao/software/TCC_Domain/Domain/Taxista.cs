using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMapsApi.Entities.Common;
using TCC_Domain.Repository;
using TCC_Infrastructure.Cryptography;

namespace TCC_Domain.Domain
{
    public class Taxista : Usuario
    {
        #region Atributos

        private string _CNH;
        private string _numeroLicenca;
        private Veiculo _veiculo;
        private StatusTaxista _status;

        #endregion

        #region Propriedades

        public virtual string CNH
        {
            get { return _CNH; }
            set { _CNH = value; }
        }

        public virtual string NumeroLicenca
        {
            get { return _numeroLicenca; }
            set { _numeroLicenca = value; }
        }

        public virtual Veiculo Veiculo
        {
            get { return _veiculo; }
            set { _veiculo = value; }
        }

        public virtual StatusTaxista Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public virtual Localizacao PosicaoAtual{ get; set; }

        #endregion

        #region Repositório

        private static TaxistaRepository Repositorio
        {
            get { return new TaxistaRepository(); }
        }

        #endregion

        #region Métodos

        public Taxista() 
        {
            this._tipoUsuario = TipoUsuario.Taxista;
            this._status = StatusTaxista.Pendente;
        }

        public static Taxista LogarTaxista(string login, string senha)
        {
            return Repositorio.LogarTaxista(login, Cryptography.CriptografarSenhaHashMD5(senha));
        }

        public static IList<Taxista> ListarTodos()
        {
            return Repositorio.ListarTodos<Taxista>();
        }

        public static Taxista Obter(Guid ID)
        {
            return Repositorio.Obter(ID);
        }

        #endregion
    }
}
