using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace TCC_Domain.Core.Repository
{
    public interface IRepositoryBase<T>
    {
        T Obter(object id);
        T Obter(DetachedCriteria criteria);
        IList<T> ObterLista(DetachedCriteria criteria);
        IList<T> ListarTodos<T>() where T : class;
        void Salvar(T value);
        void Excluir(T value);
    }
}
