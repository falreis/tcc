using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using TCC_Domain.Domain;
using TCC_Infrastructure.Extensions;

namespace TCC_Web
{
    public class PageBase : System.Web.UI.Page
    {
        #region Propriedades

        public static MessageBox CaixaMensagens
        {
            get 
            {
                Page page = HttpContext.Current.Handler as Page;
                ContentPlaceHolder cp = page.Master.Master.FindControl("MainContent") as ContentPlaceHolder;
                return cp.FindControl("MessageBox") as MessageBox;
            } 
        }

        #endregion

        #region Eventos

        protected void OnInit(EventArgs e)
        {
            if (!Base.UsuarioPossuiPermissaoAcesso)
            {
                if (HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath == Constantes.CONSTANTE_PAGINA_HOME_TAXISTA)
                    Response.Redirect(string.Format(Constantes.CONSTANTE_PAGINA_LOGIN_QUERY, TipoUsuario.Taxista.ObterDescricao()));
                else if(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath == Constantes.CONSTANTE_PAGINA_HOME_CLIENTE)
                    Response.Redirect(string.Format(Constantes.CONSTANTE_PAGINA_LOGIN_QUERY, TipoUsuario.Cliente.ObterDescricao()));
                else
                    Response.Redirect(Constantes.CONSTANTE_PAGINA_LOGIN);
            }
        }

        protected void Page_Init(Object sender, EventArgs e)
        {
            OnInit(e);
        }

        #endregion
    }
}