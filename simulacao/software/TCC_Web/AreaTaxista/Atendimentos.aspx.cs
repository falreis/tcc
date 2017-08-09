using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCC_Domain.Domain;
using TCC_Infrastructure.Extensions;

namespace TCC_Web.AreaTaxista
{
    public partial class Atendimentos : PageBase
    {
        #region Propriedades
        #endregion

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.CarregarControles();
            }
        }

        #endregion

        #region Métodos

        private void CarregarControles()
        {
            this.CarregarCaixaStatusAtendimento();
        }

        private void CarregarCaixaStatusAtendimento()
        {
            foreach (StatusRequisicao status_atendimento in Enum.GetValues(typeof(StatusRequisicao)))
                ddlStatus.Items.Add(new ListItem(status_atendimento.ObterDescricao(), status_atendimento.ObterValor()));
            ddlStatus.AdicionarItemDefault();
        }

        #endregion
    }
}