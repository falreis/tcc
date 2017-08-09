using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMapsApi.Entities.Common;
using TCC_Infrastructure.Cryptography;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class Cliente : Usuario
    {
        #region Atributos

        private StatusCliente _status;

        #endregion

        #region Propriedades

        public virtual StatusCliente Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public virtual Localizacao PosicaoAtual{ get; set; }

        #endregion

        #region Repositório

        private static ClienteRepository Repositorio
        {
            get { return new ClienteRepository(); }
        }

        #endregion

        #region Métodos

        public Cliente() 
        {
            this._tipoUsuario = TipoUsuario.Cliente;
            this._status = StatusCliente.SemStatus;
        }

        public static Cliente LogarCliente(string login, string senha)
        {
            return Repositorio.LogarCliente(login, Cryptography.CriptografarSenhaHashMD5(senha));
        }

        #endregion

        #region Métodos Estáticos

        public static IList<Cliente> ListarTodos()
        {
            return Repositorio.ListarTodos<Cliente>();
        }

        #endregion
    }
}
