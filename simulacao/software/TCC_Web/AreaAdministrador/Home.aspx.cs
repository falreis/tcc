using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCC_Domain.Domain;
using TCC_Application;

namespace TCC_Web.AreaAdministrador
{
    public class TaxistaWeb
    {
        public string placa;
        public double latitude;
        public double longitude;
        public int status;
    }

    public partial class Home : System.Web.UI.Page
    {
        #region Propriedades
        #endregion

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            gvTaxistas.RowDataBound += new GridViewRowEventHandler(gvTaxistas_RowDataBound);
            timerTaxista.Tick += new EventHandler<EventArgs>(timerTaxista_Tick);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                this.AtualizarGrid();
        }

        protected void gvTaxistas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Taxista taxista = (Taxista) e.Row.DataItem;
                Label lblLatitude = (Label)e.Row.FindControl("lblLatitude");
                Label lblLongitude = (Label)e.Row.FindControl("lblLongitude");
                Label lblNome = (Label)e.Row.FindControl("lblNome");
                Label lblDataHora = (Label)e.Row.FindControl("lblDataHora");

                lblLatitude.Text = taxista.PosicaoAtual.Latitude.ToString();
                lblLongitude.Text = taxista.PosicaoAtual.Longitude.ToString();
                lblNome.Text = (taxista.Pessoa.TipoPessoa == TipoPessoa.PessoaFisica) ? ((PessoaFisica)taxista.Pessoa).Nome : ((PessoaJuridica)taxista.Pessoa).RazaoSocial;
                lblDataHora.Text = taxista.PosicaoAtual.Data.ToString("G");
            }
        }

        protected void timerTaxista_Tick(object sender, EventArgs e)
        {
            this.AtualizarGrid();
        }

        #endregion


        #region Métodos

        private void AtualizarGrid()
        {
            OFMS ofms = OFMS.GetInstance();
            gvTaxistas.DataSource = ofms.Taxistas.OrderByDescending(taxi => taxi.PosicaoAtual.Data);
            gvTaxistas.DataBind();
        }

        #endregion

        #region WebMethods

        [System.Web.Services.WebMethod]
        public static string Teste()
        {
            OFMS ofms = OFMS.GetInstance();
            List<Taxista> taxistas = ofms.Taxistas.OrderByDescending(taxi => taxi.PosicaoAtual.Data).ToList();

            List<TaxistaWeb> taxistas_web = new List<TaxistaWeb>();

            foreach (Taxista taxista in taxistas)
            {
                TaxistaWeb taxi_web = new TaxistaWeb();
                taxi_web.placa = taxista.Pessoa.NomeOuRazaoSocial;
                taxi_web.latitude = taxista.PosicaoAtual.Latitude;
                taxi_web.longitude = taxista.PosicaoAtual.Longitude;
                taxi_web.status = (int)taxista.Status;

                taxistas_web.Add(taxi_web);
            }

            int livres = taxistas.Count(tx => tx.Status == StatusTaxista.TaxiLivre);

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return "{\"Taxista\":" + oSerializer.Serialize(taxistas_web) + "}";
        }

        #endregion
    }
}