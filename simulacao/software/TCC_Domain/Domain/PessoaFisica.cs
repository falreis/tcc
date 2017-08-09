using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class PessoaFisica : Pessoa
    {
        #region Atributos

        private string _nome;
        private string _apelido;
        private string _CPF;
        private string _RG;
        private Sexo _sexo;
        private DateTime _dataNascimento;
        private EstadoCivil _estadoCivil;
        private string _profissao;

        #endregion

        #region Propriedades

        public virtual string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public virtual string Apelido
        {
            get { return _apelido; }
            set { _apelido = value; }
        }

        public virtual string CPF
        {
            get { return _CPF; }
            set { _CPF = value; }
        }

        public virtual string RG
        {
            get { return _RG; }
            set { _RG = value; }
        }

        public virtual Sexo Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }

        public virtual DateTime DataNascimento
        {
            get { return _dataNascimento; }
            set { _dataNascimento = value; }
        }

        public virtual EstadoCivil EstadoCivil
        {
            get { return _estadoCivil; }
            set { _estadoCivil = value; }
        }

        public virtual string Profissao
        {
            get { return _profissao; }
            set { _profissao = value; }
        }

        public override string NomeOuRazaoSocial
        {
            get { return _nome; }
        }

        #endregion

        #region Repositório

        private PessoaFisicaRepository Repositorio
        {
            get { return new PessoaFisicaRepository(); }
        }

        #endregion

        #region Métodos

        public PessoaFisica() 
        {
            this.TipoPessoa = TipoPessoa.PessoaFisica;
        }

        public virtual void Salvar()
        {
            Repositorio.Salvar(this);
        }

        #endregion
    }
}
