using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using TCC_Domain.Core.Repository;
using TCC_Domain.Domain;

namespace TCC_Domain.Repository
{
    public class TaxistaRepository : RepositoryBase<Taxista>
    {
        public Taxista LogarTaxista(string login, string senha)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Taxista>();
            criteria.Add(Property.ForName("Login").Eq(login));
            criteria.Add(Property.ForName("Senha").Eq(senha));
            criteria.Add(Property.ForName("TipoUsuario").Eq(Convert.ToInt32(TipoUsuario.Taxista)));
            return this.Obter(criteria);
        }
    }
}
