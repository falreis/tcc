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
    public partial class CadastroVeiculo : PageBase
    {
        #region Propriedades
        #endregion

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            base.OnInit(e);
            this.ddlMarca.SelectedIndexChanged += new EventHandler(ddlMarca_SelectedIndexChanged);
            this.chkContrato.CheckedChanged += new EventHandler(chkContrato_CheckedChanged);
            btnConfirmar.Click += new EventHandler(btnConfirmar_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.CarregarControles();
                if (Base.SessaoUsuario != null)
                    this.CarregarInformacoesUsuario();
            }
        }

        protected void ddlMarca_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlMarca.SelectedValue))
                this.CarregarCaixaModelo(Convert.ToInt32(ddlMarca.SelectedValue));
            else
                this.CarregarCaixaModelo(null);
        }

        protected void chkContrato_CheckedChanged(Object sender, EventArgs e)
        {
            ctrlContrato.Abrir(chkContrato.UniqueID);
        }

        protected void btnConfirmar_Click(Object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Veiculo veiculo = this.ObterDadosFormularioVeiculo();
                veiculo.Rastreador = this.ObterDadosFormularioRastreador();
                veiculo.Proprietario = Base.SessaoUsuario.Pessoa;

                Taxista taxista = this.ObterDadosFormularioTaxista();
                taxista.Veiculo = veiculo;
                veiculo.Taxistas.Add(taxista);
                veiculo.Salvar();

                PageBase.CaixaMensagens.Exibir(App_LocalResources.LabelsResource.VEICULO, App_LocalResources.MensagensResource.VEICULO_SUCESSO);
            }
        }

        #endregion

        #region Métodos

        private void CarregarControles()
        {
            this.CarregarCaixaMontadora();
        }

        private void CarregarCaixaMontadora()
        {
            ddlMarca.DataSource = MontadoraVeiculo.ListarTodos();
            ddlMarca.DataTextField = "Nome";
            ddlMarca.DataValueField = "ID";
            ddlMarca.DataBind();
            ddlMarca.AdicionarItemDefault();
        }

        private void CarregarCaixaModelo(int ? codigoMontadora)
        {
            if (codigoMontadora.HasValue)
            {
                ddlModelo.DataSource = ModeloVeiculo.ListarPorMontadora(codigoMontadora.Value).OrderBy(m => m.Nome).ToList();
                ddlModelo.DataTextField = "Nome";
                ddlModelo.DataValueField = "ID";
                ddlModelo.DataBind();
                ddlModelo.AdicionarItemDefault();
                ddlModelo.Enabled = true;
            }
            else
            {
                ddlModelo.DataSource = null;
                ddlModelo.DataBind();
                ddlModelo.Enabled = false;
            }
        }

        private Veiculo ObterDadosFormularioVeiculo()
        {
            Veiculo veiculo = null;

            if (Base.SessaoUsuario != null && ((Taxista)Base.SessaoUsuario).Status != StatusTaxista.Pendente && ((Taxista)Base.SessaoUsuario).Veiculo != null)
                veiculo = ((Taxista)Base.SessaoUsuario).Veiculo;
            else
                veiculo = new Veiculo();

            veiculo.Modelo = ModeloVeiculo.Obter(Convert.ToInt32(ddlModelo.SelectedValue));
            veiculo.AnoFabricacao = Convert.ToInt32(txtAnoFabMod.Text.Substring(0, txtAnoFabMod.Text.IndexOf('/')));
            veiculo.AnoModelo = Convert.ToInt32(txtAnoFabMod.Text.Substring(txtAnoFabMod.Text.IndexOf('/')+1));
            veiculo.CorPredominante = txtCor.Text;
            veiculo.Placa = txtPlaca.Text.ToUpper();
            veiculo.Renavam = txtRenavam.Text;
            veiculo.AutorizacaoTrafego = txtAutorizacaoTrafego.Text;

            return veiculo;
        }

        private Rastreador ObterDadosFormularioRastreador()
        {
            Rastreador rastreador = new Rastreador();
            rastreador.ID = Guid.NewGuid();
            rastreador.CodigoSeguranca = Guid.NewGuid();
            return rastreador;
        }

        private Taxista ObterDadosFormularioTaxista()
        {
            Taxista taxista = (Taxista)Base.SessaoUsuario;
            taxista.CNH = txtCNH.Text;
            taxista.NumeroLicenca = txtNumeroLicenca.Text;
            taxista.Status = StatusTaxista.SemStatus;
            return taxista;
        }

        private void CarregarInformacoesUsuario()
        {
            Taxista taxista = (Taxista)Base.SessaoUsuario;
            if (taxista.Status != StatusTaxista.Pendente )
            {
                if(taxista.Veiculo != null){
                    int montadora = taxista.Veiculo.Modelo.Montadora.ID;
                    int modelo = taxista.Veiculo.Modelo.ID;

                    this.CarregarCaixaModelo(montadora);
                    ddlMarca.SelectedValue = montadora.ToString();
                    ddlModelo.SelectedValue = modelo.ToString();

                    txtAnoFabMod.Text = string.Format("{0}/{1}", taxista.Veiculo.AnoFabricacao, taxista.Veiculo.AnoModelo);
                    txtCor.Text = taxista.Veiculo.CorPredominante;
                    txtPlaca.Text = taxista.Veiculo.Placa.ToUpper();
                    txtRenavam.Text = taxista.Veiculo.Renavam;
                    txtAutorizacaoTrafego.Text = taxista.Veiculo.AutorizacaoTrafego;

                    if (taxista.Veiculo.Rastreador != null)
                    {
                        txtNumeroSerie.Text = taxista.Veiculo.Rastreador.ID.ToString().ToUpper();
                        txtCodigoSeguranca.Text = taxista.Veiculo.Rastreador.CodigoSeguranca.ToString().ToUpper();
                    }
                }

                txtCNH.Text = taxista.CNH;
                txtNumeroLicenca.Text = taxista.NumeroLicenca;

                tableContrato.Visible = false;
            }
        }

        #endregion
    }
}