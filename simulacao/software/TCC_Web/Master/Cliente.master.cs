using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TCC_Web.Master
{
    public partial class Cliente : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            lkbSair.Click += new EventHandler(lkbSair_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lkbSair.Visible = (Base.SessaoUsuario != null);
        }

        protected void lkbSair_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(Constantes.CONSTANTE_PAGINA_LOGIN);
        }
    }
}