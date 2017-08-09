using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TCC_Web.AreaTaxista
{
    public partial class DetalheAtendimentos : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void showModalPopupServerOperatorButton_Click(object sender, EventArgs e)
        {
            this.modalPopup.Show();
        }

        protected void btnFechar_Click(Object sender, EventArgs e)
        {
            this.modalPopup.Hide();
        }
    }
}