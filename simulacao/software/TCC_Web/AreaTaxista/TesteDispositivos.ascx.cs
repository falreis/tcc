using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TCC_Web.AreaTaxista
{
    public partial class TesteDispositivos : UserControlBase
    {
        #region Propriedades
        #endregion

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            base.OnInit(e);
            btnFechar.Click += new EventHandler(btnFechar_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFechar_Click(Object sender, EventArgs e)
        {
            this.modalPopup.Hide();
        }

        #endregion

        #region Métodos
        #endregion

        #region Métodos Públicos

        public void Abrir()
        {
            this.modalPopup.Show();
        }

        #endregion
    }
}