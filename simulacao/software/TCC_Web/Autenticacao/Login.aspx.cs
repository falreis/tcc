using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCC_Domain.Domain;
using TCC_Infrastructure.Extensions;

namespace TCC_Web
{
    public partial class Login : System.Web.UI.Page
    {
        #region Propriedades

        public TipoUsuario ? TipoUsuarioLogin
        {
            get 
            {
                if (!string.IsNullOrEmpty(Request.QueryString[Constantes.CONSTANTE_QUERY_TIPO_USUARIO]))
                {
                    if (Request.QueryString[Constantes.CONSTANTE_QUERY_TIPO_USUARIO].ToUpper() == "TAXISTA")
                        return TipoUsuario.Taxista;
                    else if (Request.QueryString[Constantes.CONSTANTE_QUERY_TIPO_USUARIO].ToUpper() == "CLIENTE")
                        return TipoUsuario.Cliente;
                }
                return null;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            //base.OnInit(e);
            btnEntrar.Click += new EventHandler(btnEntrar_Click);
            lkbCadastro.Click += new EventHandler(lkbCadastro_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!this.TipoUsuarioLogin.HasValue)
                    Response.Redirect("~/");
            }
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Usuario usuario = null;
                if (this.TipoUsuarioLogin.HasValue)
                {
                    string pagina_home = "~/";

                    if (this.TipoUsuarioLogin.Value == TipoUsuario.Taxista)
                    {
                        usuario = Taxista.LogarTaxista(txtEmail.Text, txtSenha.Text);
                        pagina_home = Constantes.CONSTANTE_PAGINA_HOME_TAXISTA;
                    }
                    else if (this.TipoUsuarioLogin.Value == TipoUsuario.Cliente)
                    {
                        usuario = Cliente.LogarCliente(txtEmail.Text, txtSenha.Text);
                        pagina_home = Constantes.CONSTANTE_PAGINA_HOME_CLIENTE;
                    }

                    //loga usuário de acordo com o perfil e redireciona para a página inicial
                    if (usuario != null)
                    {
                        Session.Clear();
                        Session.Add(Constantes.CONSTANTE_SESSAO_USUARIO, usuario);
                        Response.Redirect(pagina_home);
                    }
                    else
                        PageBase.CaixaMensagens.Exibir(App_LocalResources.LabelsResource.AUTENTICACAO, App_LocalResources.MensagensResource.LOGIN_NAO_REALIZADO);
                }
                else
                    Response.Redirect("~/");
            }
        }

        protected void lkbCadastro_Click(Object sender, EventArgs e)
        {
            if(this.TipoUsuarioLogin.HasValue)
                Response.Redirect(string.Format(Constantes.CONSTANTE_PAGINA_CADASTRO_QUERY, this.TipoUsuarioLogin.Value.ObterDescricao()));
            else
                Response.Redirect("~/");
        }

        #endregion

        #region Métodos
        #endregion
    }
}