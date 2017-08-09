using System;   
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TCC_Web.AreaTaxista
{
    public partial class Contrato : UserControlBase
    {
        #region Constantes

        private const string CHECKBOX_CLIENTID = "CHECKBOX_CLIENTID";

        #endregion

        #region Propriedades

        private string CheckboxUniqueID
        {
            get 
            {
                if (ViewState[CHECKBOX_CLIENTID] == null)
                    ViewState.Add(CHECKBOX_CLIENTID, String.Empty);
                return ViewState[CHECKBOX_CLIENTID].ToString();
            }
            set
            {
                if (ViewState[CHECKBOX_CLIENTID] == null)
                    ViewState.Add(CHECKBOX_CLIENTID, String.Empty);
                ViewState[CHECKBOX_CLIENTID] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Init(Object sender, EventArgs e)
        {
            base.OnInit(e);
            this.btnAceitar.Click += new EventHandler(btnAceitar_Click);
            this.btnRecusar.Click += new EventHandler(btnRecusar_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceitar_Click(Object sender, EventArgs e)
        {
            this.AtualizarCheckbox(true);
            this.modalPopup.Hide();
            updDetalheAtendimento.Update();
        }

        protected void btnRecusar_Click(Object sender, EventArgs e)
        {
            this.AtualizarCheckbox(false);
            this.modalPopup.Hide();
            updDetalheAtendimento.Update();
        }

        #endregion

        #region Métodos

        private void AtualizarCheckbox(bool aceite)
        {
            ((CheckBox)this.Page.FindControl(this.CheckboxUniqueID)).Checked = aceite;
        }

        #endregion

        #region Métodos Públicos

        public void Abrir(string checkboxUniqueID)
        {
            this.CheckboxUniqueID = checkboxUniqueID;
            this.modalPopup.Show();
            updDetalheAtendimento.Update();
        }

        #endregion
    }
}