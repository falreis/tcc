using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class MontadoraVeiculo
    {
        #region Atributos

        private int _ID;
        private string _nome;

        #endregion

        #region Propriedades

        public virtual int ID
        {
            get { return _ID;  }
            set { _ID = value; }
        }

        public virtual string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        #endregion

        #region Repositório

        private static MontadoraVeiculoRepository Repositorio
        {
            get { return new MontadoraVeiculoRepository(); }
        }

        #endregion

        #region Métodos

        public MontadoraVeiculo() { }

        public static IList<MontadoraVeiculo> ListarTodos()
        {
            return Repositorio.ListarTodos<MontadoraVeiculo>();
        }

        #endregion
    }
}
