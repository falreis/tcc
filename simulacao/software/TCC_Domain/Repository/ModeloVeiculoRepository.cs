using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using TCC_Domain.Core.Repository;
using TCC_Domain.Domain;

namespace TCC_Domain.Repository
{
    public class ModeloVeiculoRepository : RepositoryBase<ModeloVeiculo>
    {
        public IList<ModeloVeiculo> ListarPorCodigoMontadora(int codigoMontadora)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ModeloVeiculo>();
            criteria.Add(Property.ForName("Montadora.ID").Eq(codigoMontadora));
            return this.ObterLista(criteria);
        }
    }
}
