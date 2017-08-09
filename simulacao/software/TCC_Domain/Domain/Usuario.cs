using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Repository;
using TCC_Infrastructure.Cryptography;

namespace TCC_Domain.Domain
{
    public class Usuario
    {
        #region Atributos

        protected Guid _ID;
        protected TipoUsuario _tipoUsuario;
        protected Pessoa _pessoa;
        protected string _login;
        protected string _senha;
        protected string _email;
        protected bool _permitirNotificacaoEmail;
        protected bool _ativo;

        #endregion

        #region Propriedades

        public virtual Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual TipoUsuario TipoUsuario
        {
            get { return _tipoUsuario; }
            set { _tipoUsuario = value; }
        }

        public virtual Pessoa Pessoa
        {
            get { return _pessoa; }
            set { _pessoa = value; }
        }

        public virtual string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public virtual string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }

        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public virtual bool PermitirNotificacaoEmail
        {
            get { return _permitirNotificacaoEmail; }
            set { _permitirNotificacaoEmail = value; }
        }

        public virtual bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        #endregion

        #region Repositório

        private static UsuarioRepository Repositorio
        {
            get { return new UsuarioRepository(); }
        }

        #endregion

        #region Métodos

        public Usuario() { }

        public virtual void Salvar()
        {
            this._login = this._email;
            this._senha = Cryptography.CriptografarSenhaHashMD5(this._senha);
            Repositorio.Salvar(this);
        }

        public static Usuario LogarUsuario(string login, string senha)
        {
            return Repositorio.LogarUsuario(login, Cryptography.CriptografarSenhaHashMD5(senha));
        }

        #endregion
    }
}
