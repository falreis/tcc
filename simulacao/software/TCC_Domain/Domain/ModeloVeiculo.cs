using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class ModeloVeiculo
    {
        #region Atributos

        private int _ID;
        private MontadoraVeiculo _montadora;
        private string _nome;

        #endregion

        #region Propriedades

        public virtual int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual MontadoraVeiculo Montadora
        {
            get { return _montadora; }
            set { _montadora = value; }
        }

        public virtual string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        #endregion

        #region Repositório

        private static ModeloVeiculoRepository Repositorio
        {
            get { return new ModeloVeiculoRepository(); }
        }

        #endregion

        #region Métodos

        public ModeloVeiculo() { }

        public static ModeloVeiculo Obter(int id)
        {
            return Repositorio.Obter(id);
        }

        public static IList<ModeloVeiculo> ListarPorMontadora(int codigoMontadora)
        {
            return Repositorio.ListarPorCodigoMontadora(codigoMontadora);
        }

        #endregion
    }
}
