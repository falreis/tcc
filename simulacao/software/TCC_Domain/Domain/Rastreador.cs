using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class Rastreador
    {
        #region Atributos

        private Guid _ID;
        private Guid _codigoSeguranca;
        private bool _ativo;

        #endregion

        #region Propriedades

        public virtual Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual Guid CodigoSeguranca
        {
            get { return _codigoSeguranca; }
            set { _codigoSeguranca = value; }
        }

        public virtual bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        #endregion

        #region Repositório

        private static RastreadorRepository Repositorio
        {
            get { return new RastreadorRepository(); }
        }

        #endregion

        #region Métodos

        public Rastreador() 
        {
            this._ativo = true;
        }

        public virtual void Salvar()
        {
            Repositorio.Salvar(this);
        }

        #endregion
    }
}
