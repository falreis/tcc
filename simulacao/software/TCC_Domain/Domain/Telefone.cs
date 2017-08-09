using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class Telefone
    {
        #region Atributos

        private Guid _ID;
        private string _ddd;
        private string _numero;
        private bool _permitirSMS;
        private Pessoa _pessoa;

        #endregion

        #region Propriedades

        public virtual Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual string DDD
        {
            get { return _ddd; }
            set { _ddd = value; }
        }

        public virtual string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public virtual bool PermitirSMS
        {
            get { return _permitirSMS; }
            set { _permitirSMS = value; }
        }

        public virtual Pessoa Pessoa
        {
            get { return _pessoa; }
            set { _pessoa = value; }
        }

        #endregion

        #region Repositório

        private TelefoneRepository Repositorio
        {
            get { return new TelefoneRepository(); }
        }

        #endregion

        #region Métodos

        public Telefone() { }

        public Telefone(string ddd, string numero, bool sms)
        {
            this._ddd = ddd;
            this._numero = numero;
            this._permitirSMS = sms;
        }

        #endregion
    }
}
