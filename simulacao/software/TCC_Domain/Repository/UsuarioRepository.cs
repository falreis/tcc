using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using TCC_Domain.Core.Repository;
using TCC_Domain.Domain;

namespace TCC_Domain.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>
    {
        public Usuario LogarUsuario(string login, string senha)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Usuario>();
            criteria.Add(Property.ForName("Login").Eq(login));
            criteria.Add(Property.ForName("Senha").Eq(senha));
            return this.Obter(criteria);
        }
    }
}
