using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMapsApi.Entities.Common;
using TCC_Domain.Repository;

namespace TCC_Domain.Domain
{
    public class Requisicao
    {
        #region Atributos

        private Guid _ID;
        private Taxista _taxista;
        private Cliente _cliente;
        private Queue<Localizacao> _rota;
        private DateTime _dataRequisicao;
        private DateTime _dataInicio;
        private DateTime? _dataFim;
        private StatusRequisicao _status;

        #endregion

        #region Propriedades

        public virtual Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual Taxista Taxista 
        {
            get { return _taxista; }
            set { _taxista = value; }
        }

        public virtual Cliente Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        public virtual Queue<Localizacao> Rota
        {
            get { return _rota; }
            set { _rota = value; }
        }

        public virtual DateTime DataRequisicao
        {
            get { return _dataRequisicao; }
            set { _dataRequisicao = value; }
        }

        public virtual DateTime DataInicio
        {
            get { return _dataInicio; }
            set { _dataInicio = value; }
        }

        public virtual DateTime? DataFim
        {
            get { return _dataFim; }
            set { _dataFim = value; }
        }

        public virtual StatusRequisicao Status
        {
            get { return _status; }
            set { _status = value; }
        }

        #endregion

        #region Repositório

        private RequisicaoRepository Repositorio
        {
            get { return new RequisicaoRepository(); }
        }

        #endregion

        #region Métodos

        public Requisicao()
        {
            this._dataRequisicao = DateTime.Now;
        }

        public virtual void Salvar()
        {
            Repositorio.Salvar(this);
        }

        #endregion
    }
}
