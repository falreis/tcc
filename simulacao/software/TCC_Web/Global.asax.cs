using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TCC_Web
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            TCC_Domain.Core.Repository.RepositoryBase<object>.RegistrarTabelas();

            List<TCC_Infrastructure.Attributes.KVAttribute> paginas = new List<TCC_Infrastructure.Attributes.KVAttribute>();
            paginas.Add(new TCC_Infrastructure.Attributes.KVAttribute("~/Autenticacao/Cadastro.aspx", "0"));
            paginas.Add(new TCC_Infrastructure.Attributes.KVAttribute("~/Autenticacao/Login.aspx", "0"));

            paginas.Add(new TCC_Infrastructure.Attributes.KVAttribute("~/AreaTaxista/Atendimentos.aspx", "1"));
            paginas.Add(new TCC_Infrastructure.Attributes.KVAttribute("~/AreaTaxista/CadastroVeiculo.aspx", "1"));
            paginas.Add(new TCC_Infrastructure.Attributes.KVAttribute("~/AreaTaxista/Dispositivos.aspx", "1"));
            paginas.Add(new TCC_Infrastructure.Attributes.KVAttribute("~/AreaTaxista/Home.aspx", "1"));
            paginas.Add(new TCC_Infrastructure.Attributes.KVAttribute("~/AreaTaxista/Simulacao.aspx", "1"));

            paginas.Add(new TCC_Infrastructure.Attributes.KVAttribute("~/AreaCliente/Home.aspx", "2"));

            Application.Add(Constantes.CONSTANTE_APLICACAO_PAGINAS, paginas);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
