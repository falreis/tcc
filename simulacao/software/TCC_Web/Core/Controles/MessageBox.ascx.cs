using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TCC_Web
{
    public partial class MessageBox : UserControlBase
    {
        #region Constantes

        private const string URL_REDIRECT = "URL_REDIRECT";

        #endregion

        #region Propriedades

        private string UrlRedirect
        {
            get 
            {
                if (ViewState[URL_REDIRECT] == null)
                    ViewState.Add(URL_REDIRECT, String.Empty);
                return ViewState[URL_REDIRECT].ToString();
            }
            set 
            {
                if (ViewState[URL_REDIRECT] == null)
                    ViewState.Add(URL_REDIRECT, String.Empty);
                ViewState[URL_REDIRECT] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_init(object sender, EventArgs e)
        {
            base.OnInit(e);
            this.btnFechar.Click += new EventHandler(btnFechar_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.UrlRedirect))
                Response.Redirect(this.UrlRedirect);
            else
                modalPopup.Hide();
        }

        #endregion

        #region Métodos Públicos

        public void Exibir(string mensagem)
        {
            this.Exibir(String.Empty, mensagem);
        }

        public void Exibir(string titulo, string mensagem)
        {
            lblTitulo.Text = titulo;
            lblMensagem.Text = mensagem;
            modalPopup.Show();

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "setTimeout(function(){ showMessageBox(); },100);", true);
        }

        public void ExibirRedirecionar(string titulo, string mensagem, string url)
        {
            this.UrlRedirect = url;
            this.Exibir(titulo, mensagem);
        }

        #endregion

    }
}