using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCC_Domain.Domain;
using TCC_Application;
using TCC_Application.Exceptions;
using GoogleMapsApi.Entities;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using System.Text;

namespace TCC_Web.AreaTaxista
{
    public partial class Home : PageBase
    {
        #region Constantes

        public const int VIEW_LOCATION = 0;
        public const int VIEW_RESPOSTA_ATENDIMENTO = 1;
        public const int VIEW_AGUARDANDO_CONFIRMACAO = 2;
        public const int VIEW_AGUARDANDO_ATENDIMENTO = 3;
        public const int VIEW_EM_ATENDIMENTO = 4;

        #endregion

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            base.OnInit(e);
            viewLocation.Activate += new EventHandler(viewLocation_Activate);
            viewRespostaAtendimento.Activate += new EventHandler(viewRespostaAtendimento_Activate);
            viewAguardandoAtendimento.Activate += new EventHandler(viewAguardandoAtendimento_Activate);
            viewEmAtendimento.Activate += new EventHandler(viewEmAtendimento_Activate);

            timerLocation.Tick += new EventHandler<EventArgs>(timerLocation_Tick);
            timerAguardandoConfirmacao.Tick += new EventHandler<EventArgs>(timerAguardandoConfirmacao_Tick);

            btnAtender.Click += new EventHandler(btnAtender_Click);
            btnRecusar.Click += new EventHandler(btnRecusar_Click);
            btnIniciarAtendimento.Click += new EventHandler(btnIniciarAtendimento_Click);
            btnFinalizarAtendimento.Click += new EventHandler(btnFinalizarAtendimento_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OFMS ofms = OFMS.GetInstance();
                Requisicao requisicao = ofms.ObterRequisicao(Base.SessaoUsuario.ID);
                if (requisicao != null)
                {
                    switch (requisicao.Status)
                    {
                        case StatusRequisicao.RequisicaoRealizada:
                            multiview.ActiveViewIndex = VIEW_RESPOSTA_ATENDIMENTO;
                            break;
                        case StatusRequisicao.AguardandoResposta:
                            multiview.ActiveViewIndex = VIEW_AGUARDANDO_CONFIRMACAO;
                            break;
                        case StatusRequisicao.AguardandoAtendimento:
                            multiview.ActiveViewIndex = VIEW_AGUARDANDO_ATENDIMENTO;
                            break;
                        case StatusRequisicao.EmProcessamento:
                            multiview.ActiveViewIndex = VIEW_EM_ATENDIMENTO;
                            break;
                        default:
                            multiview.ActiveViewIndex = VIEW_LOCATION;
                            break;
                    }
                }
                else
                    multiview.ActiveViewIndex = VIEW_LOCATION;
            }
        }

        protected void viewLocation_Activate(object sender, EventArgs e)
        {
            lblTitulo.Text = App_LocalResources.MensagensResource.MINHA_POSICAO_ATUAL;
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "GetLocation()", true);
        }

        protected void viewRespostaAtendimento_Activate(object sender, EventArgs e)
        {
            lblTitulo.Text = App_LocalResources.MensagensResource.PROCESSAMENTO_REQUISICAO;
        }

        protected void viewAguardandoAtendimento_Activate(object sender, EventArgs e)
        {
            lblTitulo.Text = App_LocalResources.MensagensResource.ATENDIMENTO_REQUISICAO;

            OFMS ofms = OFMS.GetInstance();
            Requisicao requisicao = ofms.ObterRequisicao(Base.SessaoUsuario.ID);
            if (requisicao != null && requisicao.Taxista != null && requisicao.Taxista.PosicaoAtual != null)
            {
                DirectionsRequest request = new DirectionsRequest();
                request.Origin = Localizacao.ObterEndereco(requisicao.Taxista.PosicaoAtual.Latitude, requisicao.Taxista.PosicaoAtual.Longitude);
                request.Destination = Localizacao.ObterEndereco(requisicao.Cliente.PosicaoAtual.Latitude, requisicao.Cliente.PosicaoAtual.Longitude);

                try
                {
                    DirectionsResponse response = GoogleMapsApi.GoogleMaps.Directions.Query(request);
                    Leg leg = response.Routes.First().Legs.First();
                    lblPrevisao.Text = leg.Duration.Text.ToString();
                    lblDistancia.Text = leg.Distance.Text.ToString();

                    StringBuilder rota = new StringBuilder();
                    foreach (Step step in leg.Steps)
                        rota.Append("&#8226; " + step.HtmlInstructions.Replace("<b>", "").Replace("<b />", "") + "<br />");
                    lblRota.Text = rota.ToString();

                    if (requisicao.Cliente != null)
                        lblCliente.Text = requisicao.Cliente.Pessoa.NomeOuRazaoSocial;
                }
                catch { }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "GetAtendimento()", true);
        }

        protected void viewEmAtendimento_Activate(object sender, EventArgs e)
        {
            lblTitulo.Text = App_LocalResources.MensagensResource.PROCESSAMENTO_REQUISICAO;
        }

        protected void timerLocation_Tick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "GetLocation()", true);

            OFMS ofms = OFMS.GetInstance();
            Requisicao atendimento = ofms.GetRequisicaoRealizada(Base.SessaoUsuario.ID);

            if (atendimento != null)
                multiview.ActiveViewIndex = VIEW_RESPOSTA_ATENDIMENTO;
        }

        protected void timerAguardandoConfirmacao_Tick(object sender, EventArgs e)
        {
            OFMS ofms = OFMS.GetInstance();
            bool cliente_confirmou = ofms.ExistsRequisicaoAguardandoAtendimento(Base.SessaoUsuario.ID);
            if (cliente_confirmou)
            {
                multiview.ActiveViewIndex = VIEW_AGUARDANDO_ATENDIMENTO;
            }
            else
            {
                //verifica se o cliente não cancelou a requisição
                if (!ofms.ExistsRequisicaoAguardandoConfirmacao(Base.SessaoUsuario.ID))
                {
                    PageBase.CaixaMensagens.ExibirRedirecionar(App_LocalResources.LabelsResource.REQUISICAO,
                        App_LocalResources.MensagensResource.CLIENTE_CANCELOU_REQUISICAO, Request.AppRelativeCurrentExecutionFilePath);

                    multiview.ActiveViewIndex = VIEW_LOCATION;
                }
            }
        }

        protected void btnAtender_Click(object sender, EventArgs e)
        {
            OFMS ofms = OFMS.GetInstance();
            ofms.AceitarRequisicao(Base.SessaoUsuario.ID);
            multiview.ActiveViewIndex = VIEW_AGUARDANDO_CONFIRMACAO;
        }

        protected void btnRecusar_Click(object sender, EventArgs e)
        {
            try
            {
                OFMS ofms = OFMS.GetInstance();
                ofms.RecusarAtendimento(Base.SessaoUsuario.ID);
            }
            catch (NenhumTaxiParaAtenderRequisicaoException) { }

            PageBase.CaixaMensagens.ExibirRedirecionar(App_LocalResources.LabelsResource.REQUISICAO,
                        App_LocalResources.MensagensResource.REQUISICAO_RECUSADA, Request.AppRelativeCurrentExecutionFilePath);

            multiview.ActiveViewIndex = VIEW_LOCATION;
        }

        protected void btnIniciarAtendimento_Click(object sender, EventArgs e)
        {
            OFMS ofms = OFMS.GetInstance();
            ofms.IniciarAtendimento(Base.SessaoUsuario.ID);

            multiview.ActiveViewIndex = VIEW_EM_ATENDIMENTO;
        }

        protected void btnFinalizarAtendimento_Click(object sender, EventArgs e)
        {
            OFMS ofms = OFMS.GetInstance();
            ofms.ConcluirRequisicao(Base.SessaoUsuario.ID);

            multiview.ActiveViewIndex = VIEW_LOCATION;
        }

        #endregion

        #region WebMethods

        [System.Web.Services.WebMethod]
        public static string GetLocation()
        {
            return TCC_Application.WebMethods.GetLocation(Base.SessaoUsuario.ID);
        }

        [System.Web.Services.WebMethod]
        public static string GetAtendimento()
        {
            return TCC_Application.WebMethods.GetAtendimento(Base.SessaoUsuario.ID);
        }
        
        #endregion
    }
}