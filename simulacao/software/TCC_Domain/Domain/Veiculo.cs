using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class Veiculo
    {
        #region Atributos

        private Guid _ID;
        private ModeloVeiculo _modelo;
        private int _anoFabricacao;
        private int _anoModelo;
        private string _corPredominante;
        private string _placa;
        private string _renavam;

        private Pessoa _proprietario;
        private string _autorizaoTrafego;
        private IList<Taxista> _taxistas;
        private Rastreador _rastreador;
        private bool _ativo;

        #endregion

        #region Propriedades

        public virtual Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual ModeloVeiculo Modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }

        public virtual int AnoFabricacao
        {
            get { return _anoFabricacao; }
            set { _anoFabricacao = value; }
        }

        public virtual int AnoModelo
        {
            get { return _anoModelo; }
            set { _anoModelo = value; }
        }

        public virtual string CorPredominante
        {
            get { return _corPredominante; }
            set { _corPredominante = value; }
        }

        public virtual string Placa
        {
            get { return _placa; }
            set { _placa = value; }
        }

        public virtual string Renavam
        {
            get { return _renavam; }
            set { _renavam = value; }
        }

        public virtual Pessoa Proprietario
        {
            get { return _proprietario; }
            set { _proprietario = value; }
        }

        public virtual string AutorizacaoTrafego
        {
            get { return _autorizaoTrafego; }
            set { _autorizaoTrafego = value; }
        }

        public virtual IList<Taxista> Taxistas
        {
            get { return _taxistas; }
            set { _taxistas = value; }
        }

        public virtual Rastreador Rastreador
        {
            get { return _rastreador; }
            set { _rastreador = value; }
        }

        public virtual bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        #endregion

        #region Repositório

        private static VeiculoRepository Repositorio
        {
            get { return new VeiculoRepository(); }
        }

        #endregion

        #region Métodos

        public Veiculo() 
        {
            this._ativo = true;
            this.Taxistas = new List<Taxista>();
        }

        public virtual void Salvar()
        {
            Repositorio.Salvar(this);
        }

        #endregion
    }
}
