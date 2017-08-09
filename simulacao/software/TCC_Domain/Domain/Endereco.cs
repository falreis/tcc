using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class Endereco
    {
        #region Atributos

        private Guid _ID;
        private string _CEP;
        private UF _estado;
        private string _cidade;
        private string _logradouro;
        private string _numero;
        private string _complemento;
        private string _referencia;

        #endregion

        #region Propriedades

        public virtual Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual string CEP
        {
            get { return _CEP; }
            set { _CEP = value; }
        }

        public virtual UF Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public virtual string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }

        public virtual string Logradouro
        {
            get { return _logradouro; }
            set { _logradouro = value; }
        }

        public virtual string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public virtual string Complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }

        public virtual string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }

        #endregion

        #region Repositório

        private EnderecoRepository Repositorio
        {
            get { return new EnderecoRepository(); }
        }

        #endregion

        #region Métodos

        public Endereco() { }

        public virtual void Salvar()
        {
            Repositorio.Salvar(this);
        }

        #endregion
    }
}
