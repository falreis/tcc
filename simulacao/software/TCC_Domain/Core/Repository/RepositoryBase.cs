using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate;
using NHibernate.Criterion;

namespace TCC_Domain.Core.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T>
    {
        #region Atributos

        private Configuration cfg;
        private ISessionFactory factory;
        private ISession session;

        #endregion

        #region Métodos

        public RepositoryBase()
        {
            cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(T).Assembly);

            factory = cfg.BuildSessionFactory();
            session = factory.OpenSession();
        }

        public T Obter(object id)
        {
            T objeto;
            
            try
            {
                objeto = this.session.Get<T>(id);
            }
            catch
            {
                throw;
            }
            finally
            {
                this.session.Close();
                this.factory.Close();
            }
            return objeto;
        }

        public T Obter(DetachedCriteria criteria)
        {
            T objeto;
            try
            {
                ICriteria query = criteria.GetExecutableCriteria(session); ;
                objeto = query.UniqueResult<T>();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.session.Close();
                this.factory.Close();
            }
            return objeto;
        }

        public IList<T> ObterLista(DetachedCriteria criteria)
        {
            IList<T> lista = null;
            try
            {
                ICriteria query = criteria.GetExecutableCriteria(session); ;
                lista = query.List<T>();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.session.Close();
                this.factory.Close();
            }

            return lista;
        }

        public void Salvar(T value)
        {
            ITransaction transaction = session.BeginTransaction();

            try
            {
                this.session.SaveOrUpdate(value);
                this.session.Flush();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                this.session.Close();
                this.factory.Close();
            }
        }

        public void Excluir(T value)
        {
            ITransaction transaction = session.BeginTransaction();

            try
            {
                this.session.Delete(value);
                this.session.Flush();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                this.session.Close();
                this.factory.Close();
            }
        }

        public IList<T> ListarTodos<T>() where T : class
        {
            IList<T> lista = null;
            try
            {
                ICriteria criteria = session.CreateCriteria<T>();
                lista = criteria.List<T>();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.session.Close();
                this.factory.Close();
            }

            return lista;
        }

        #endregion

        #region Métodos Estáticos

        public static void RegistrarTabelas()
        {
            //var cfg = new Configuration();
            //cfg.Configure();
            //cfg.AddAssembly(typeof(Endereco).Assembly);
            //new SchemaExport(cfg).Execute(false, true, false);

            //ISessionFactory factory = cfg.BuildSessionFactory();
            //ISession session = factory.OpenSession();
            //ITransaction transaction = session.BeginTransaction();

            ////Endereco endereco = new Endereco();
            ////endereco.CEP = "31030410";
            ////endereco.Cidade = "Belo Horizonte";
            ////endereco.Complemento = "402";
            ////endereco.Estado = UF.MinasGerais;
            ////endereco.Logradouro = "Rua A";
            ////endereco.Numero = "255";
            ////endereco.Referencia = "sem referencias";

            ////session.Save(endereco);
            //transaction.Commit();
            //session.Close();
        }

        #endregion
    }
}
