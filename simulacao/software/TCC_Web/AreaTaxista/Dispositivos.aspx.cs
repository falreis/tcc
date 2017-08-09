using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCC_Domain.Domain;

namespace TCC_Web.AreaTaxista
{
    public partial class Dispositivos : PageBase
    {
        #region Propriedades
        #endregion

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            base.OnInit(e);
            btnTestar.Click += new EventHandler(btnTestar_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Base.SessaoUsuario != null)
                    this.CarregarInformacoesDispositivos();
            }
        }

        protected void btnTestar_Click(object sender, EventArgs e)
        {
            ctrlTesteDispositivos.Abrir();
        }

        #endregion

        #region Métodos

        private void CarregarInformacoesDispositivos()
        {
            Taxista taxista = (Taxista)Base.SessaoUsuario;
            if (taxista != null && taxista.Veiculo != null && taxista.Veiculo.Rastreador != null)
            {
                lblNumeroSerie.Text = taxista.Veiculo.Rastreador.ID.ToString().ToUpper();
                lblCodigoSeguranca.Text = taxista.Veiculo.Rastreador.CodigoSeguranca.ToString().ToUpper();
            }
        }

        #endregion
    }
}