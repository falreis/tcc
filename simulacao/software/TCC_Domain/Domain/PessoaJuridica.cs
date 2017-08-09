using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class PessoaJuridica : Pessoa
    {
        #region Atributos

        private string _razaoSocial;
        private string _nomeFantasia;
        private string _CNPJ;
        private string _inscricaoEstadual;
        private RamoAtividade _ramoAtividade;
        private string _site;

        private string _nomeResponsavel;
        private string _setorResponsavel;
        private string _cargoResponsavel;
        private Sexo _sexoResponsavel;
        private string _emailResponsavel;

        #endregion

        #region Propriedades

        public virtual string RazaoSocial
        {
            get { return _razaoSocial; }
            set { _razaoSocial = value; }
        }

        public virtual string NomeFantasia
        {
            get { return _nomeFantasia; }
            set { _nomeFantasia = value; }
        }

        public virtual string CNPJ
        {
            get { return _CNPJ; }
            set { _CNPJ = value; }
        }

        public virtual string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set { _inscricaoEstadual = value; }
        }

        public virtual RamoAtividade RamoAtividade
        {
            get { return _ramoAtividade; }
            set { _ramoAtividade = value; }
        }

        public virtual string Site
        {
            get { return _site; }
            set { _site = value; }
        }

        public virtual string NomeResponsavel
        {
            get { return _nomeResponsavel; }
            set { _nomeResponsavel = value; }
        }

        public virtual string SetorResponsavel
        {
            get { return _setorResponsavel; }
            set { _setorResponsavel = value; }
        }

        public virtual string CargoResponsavel
        {
            get { return _cargoResponsavel; }
            set { _cargoResponsavel = value; }
        }

        public virtual Sexo SexoResponsavel
        {
            get { return _sexoResponsavel; }
            set { _sexoResponsavel = value; }
        }

        public virtual string EmailResponsavel
        {
            get { return _emailResponsavel; }
            set { _emailResponsavel = value; }
        }

        public override string NomeOuRazaoSocial
        {
            get { return _razaoSocial; }
        }

        #endregion

        #region Repositório

        private PessoaJuridicaRepository Repositorio
        {
            get { return new PessoaJuridicaRepository(); }
        }

        #endregion

        #region Métodos

        public PessoaJuridica()
        {
            this.TipoPessoa = TipoPessoa.PessoaJuridica;
        }

        public virtual void Salvar()
        {
            Repositorio.Salvar(this);
        }

        #endregion
    }
}
