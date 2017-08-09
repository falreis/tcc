using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class Pessoa
    {
        #region Atributos

        protected Guid _ID;
        protected TipoPessoa _tipoPessoa;
        protected Endereco _endereco;
        protected IList<Telefone> _telefones;
        protected bool _ativo;

        #endregion

        #region Propriedades

        public virtual Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual TipoPessoa TipoPessoa
        {
            get { return _tipoPessoa; }
            set { _tipoPessoa = value; }
        }

        public virtual Endereco Endereco
        {
            get { return _endereco; }
            set { _endereco = value; }
        }

        public virtual IList<Telefone> Telefones
        {
            get { return _telefones; }
            set { _telefones = value; }
        }

        public virtual bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public virtual string NomeOuRazaoSocial
        {
            get { return String.Empty; }
        }

        #endregion

        #region Repositório

        private PessoaRepository Repositorio
        {
            get { return new PessoaRepository(); }
        }

        #endregion

        #region Métodos

        public Pessoa() 
        {
            this._ativo = true;
            this._endereco = new Endereco();
        }

        #endregion

    }
}
