using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCC_Domain.Domain;
using TCC_Application;
using TCC_Application.Workflow;
using System.Activities;
using TCC_Application.Arguments;
using TCC_Application.Exceptions;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;

namespace TCC_Web.AreaCliente
{
    public partial class Home : PageBase
    {
        #region Constantes

        public const string CONSTANTE_REQUISICAO = "CONSTANTE_REQUISICAO";
        public const string CONSTANTE_SCRIPT_GEOLOCALIZACAO = "navigator.geolocation.getCurrentPosition(vwPosicaoMapaSucesso, vwPosicaoMapaErro);";

        public const int VIEW_POSICAO = 0;
        public const int VIEW_AGUARDE_PROCESSAMENTO = 1;
        public const int VIEW_CONFIRMACAO = 2;
        public const int VIEW_AGUARDE_ATENDIMENTO = 3;
        public const int VIEW_EM_ATENDIMENTO = 4;
        public const int VIEW_TAXI_INDISPONIVEL = 5;

        #endregion

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            base.OnInit(e);

            viewPosicao.Activate += new EventHandler(viewPosicao_Activate);
            viewAguardeProcessamento.Activate += new EventHandler(viewAguardeProcessamento_Activate);
            viewConfirmacao.Activate +=new EventHandler(viewConfirmacao_Activate);
            viewAguardeAtendimento.Activate += new EventHandler(viewAguardeAtendimento_Activate);
            viewEmAtendimento.Activate += new EventHandler(viewEmAtendimento_Activate);
            viewTaxiIndisponivel.Activate += new EventHandler(viewTaxiIndisponivel_Activate);
            
            /* view Em Atendimento */
            btnSolicitar.Click += new EventHandler(btnSolicitar_Click);
            timerPosition.Tick += new EventHandler<EventArgs>(timerPosition_Tick);

            /* view Aguarde Processamento */
            btnCancelarAG.Click += new EventHandler(btnCancelarAG_Click);
            timerAguardeProcessamento.Tick += new EventHandler<EventArgs>(timerAguardeProcessamento_Tick);

            /* view Aguarde Confirmação */
            btnConfirmarSolicitacao.Click += new EventHandler(btnConfirmarSolicitacao_Click);
            btnCancelarSolicitacao.Click += new EventHandler(btnCancelarSolicitacao_Click);
            timerConfirmacao.Tick += new EventHandler<EventArgs>(timerConfirmacao_Tick);

            /* view Aguarde Atendimento */
            timerAguardeAtendimento.Tick += new EventHandler<EventArgs>(timerAguardeAtendimento_Tick);

            /* view Em Atendimento */
            timerEmAtendimento.Tick += new EventHandler<EventArgs>(timerEmAtendimento_Tick);

            /* view Taxi Indisponível */
            btnCancelarTaxiIndisponivel.Click += new EventHandler(btnCancelarTaxiIndisponivel_Click);
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
                            multiview.ActiveViewIndex = VIEW_AGUARDE_PROCESSAMENTO;
                            break;
                        case StatusRequisicao.AguardandoResposta:
                            multiview.ActiveViewIndex = VIEW_CONFIRMACAO;
                            break;
                        case StatusRequisicao.AguardandoAtendimento:
                            multiview.ActiveViewIndex = VIEW_AGUARDE_ATENDIMENTO;
                            break;
                        case StatusRequisicao.EmProcessamento:
                            multiview.ActiveViewIndex = VIEW_EM_ATENDIMENTO;
                            break;
                        default:
                            multiview.ActiveViewIndex = VIEW_TAXI_INDISPONIVEL;
                            break;
                    }
                }
                else
                    multiview.ActiveViewIndex = VIEW_POSICAO;
            }
        }
        
        #region View Posicao

        protected void viewPosicao_Activate(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), CONSTANTE_SCRIPT_GEOLOCALIZACAO, true);
        }

        protected void btnSolicitar_Click(object sender, EventArgs e)
        {
            Localizacao localizacao = Geolocation.ObterLocalizacao(txtEndereco.Text);
            if (localizacao != null)
            {
                try
                {
                    OFMS ofms = OFMS.GetInstance();
                    ofms.IniciarRequisicao(Base.SessaoUsuario.ID, localizacao.Latitude, localizacao.Longitude);

                    multiview.ActiveViewIndex = VIEW_AGUARDE_PROCESSAMENTO;
                }
                catch (NenhumTaxiParaAtenderRequisicaoException)
                {
                    PageBase.CaixaMensagens.Exibir(App_LocalResources.MensagensResource.NENHUM_TAXI_DISPONIVEL);
                    multiview.ActiveViewIndex = VIEW_TAXI_INDISPONIVEL;
                }
                catch (Exception)
                {
                    PageBase.CaixaMensagens.Exibir(App_LocalResources.MensagensResource.GEOLOCALIZACAO_INDISPONIVEL);
                }
            }
            else
            {
                PageBase.CaixaMensagens.Exibir(App_LocalResources.MensagensResource.GEOLOCALIZACAO_NAO_ENCONTRADA);
            }
        }

        protected void timerPosition_Tick(object sender, EventArgs e)
        {
            if (hiddenLatitude.Value != String.Empty && hiddenLongitude.Value != String.Empty)
                txtEndereco.Text = Localizacao.ObterEndereco(Convert.ToDouble(hiddenLatitude.Value.Replace(".", ",")), Convert.ToDouble(hiddenLongitude.Value.Replace(".", ",")));

            divCarregando.Visible = false;
            divPosicao.Visible = true;
            timerPosition.Enabled = false;
            txtEndereco.Focus();

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), CONSTANTE_SCRIPT_GEOLOCALIZACAO, true);
        }

        #endregion

        #region View Aguarde Processamento

        protected void viewAguardeProcessamento_Activate(object sender, EventArgs e)
        {
        }

        protected void timerAguardeProcessamento_Tick(object sender, EventArgs e)
        {
            OFMS ofms = OFMS.GetInstance();
            bool taxista_confirmou = ofms.ExistsRequisicaoAguardandoConfirmacao(Base.SessaoUsuario.ID);
            if (taxista_confirmou)
            {
                multiview.ActiveViewIndex = VIEW_CONFIRMACAO;
            }
            else
            {
                //verifica se a requisição foi recusada e agora está na fila de espera
                if (ofms.ExistsRequisicaoEmProcessamento(Base.SessaoUsuario.ID))
                {
                    //PageBase.CaixaMensagens.ExibirRedirecionar(App_LocalResources.LabelsResource.REQUISICAO,
                    //    App_LocalResources.MensagensResource.NENHUM_TAXI_DISPONIVEL, Request.AppRelativeCurrentExecutionFilePath);
                    multiview.ActiveViewIndex = VIEW_TAXI_INDISPONIVEL;
                }
            }
        }

        protected void btnCancelarAG_Click(object sender, EventArgs e)
        {
            this.CancelarRequisicao();
            PageBase.CaixaMensagens.ExibirRedirecionar(App_LocalResources.LabelsResource.REQUISICAO, 
                    App_LocalResources.MensagensResource.REQUISICAO_CANCELADA, Request.AppRelativeCurrentExecutionFilePath);

            multiview.ActiveViewIndex = VIEW_POSICAO;
        }

        #endregion

        #region View Confirmação

        protected void viewConfirmacao_Activate(object sender, EventArgs e)
        {
        }

        protected void btnConfirmarSolicitacao_Click(object sender, EventArgs e)
        {
            OFMS ofms = OFMS.GetInstance();
            ofms.ConfirmarRequisicao(Base.SessaoUsuario.ID);

            multiview.ActiveViewIndex = VIEW_AGUARDE_ATENDIMENTO;
        }

        protected void btnCancelarSolicitacao_Click(object sender, EventArgs e)
        {
            this.CancelarRequisicao();
            PageBase.CaixaMensagens.ExibirRedirecionar(App_LocalResources.LabelsResource.REQUISICAO,
                    App_LocalResources.MensagensResource.REQUISICAO_CANCELADA, Request.AppRelativeCurrentExecutionFilePath);

            multiview.ActiveViewIndex = VIEW_POSICAO;
        }

        protected void timerConfirmacao_Tick(object sender, EventArgs e)
        {
            this.CancelarRequisicao();
            PageBase.CaixaMensagens.ExibirRedirecionar(App_LocalResources.LabelsResource.REQUISICAO,
                    App_LocalResources.MensagensResource.REQUISICAO_CANCELADA, Request.AppRelativeCurrentExecutionFilePath);

            multiview.ActiveViewIndex = VIEW_POSICAO;
        }

        #endregion

        #region Aguarde Atendimento

        protected void viewAguardeAtendimento_Activate(object sender, EventArgs e)
        {
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
                    lblPlaca.Text = requisicao.Taxista.Veiculo.Placa;
                    lblResponsavel.Text = requisicao.Taxista.Pessoa.NomeOuRazaoSocial;
                }
                catch { }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "GetAtendimento()", true);
        }

        protected void timerAguardeAtendimento_Tick(object sender, EventArgs e)
        {
            OFMS ofms = OFMS.GetInstance();
            bool em_atedimento = ofms.ExistsRequisicaoEmAtendimento(Base.SessaoUsuario.ID);
            if (em_atedimento)
            {
                multiview.ActiveViewIndex = VIEW_EM_ATENDIMENTO;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "GetAtendimento()", true);
        }

        #endregion

        #region Em Atendimento

        protected void viewEmAtendimento_Activate(object sender, EventArgs e)
        {
        }

        protected void timerEmAtendimento_Tick(object sender, EventArgs e)
        {
            OFMS ofms = OFMS.GetInstance();
            bool existe_requisao = ofms.ExistsRequisicao(Base.SessaoUsuario.ID);
            if (!existe_requisao)
            {
                PageBase.CaixaMensagens.Exibir(App_LocalResources.MensagensResource.REQUISICAO_CONCLUIDA);
                multiview.ActiveViewIndex = VIEW_POSICAO;
            }
        }

        #endregion

        #region TaxiIndisponivel

        protected void viewTaxiIndisponivel_Activate(object sender, EventArgs e)
        {
        }

        protected void btnCancelarTaxiIndisponivel_Click(object sender, EventArgs e)
        {
            OFMS ofms = OFMS.GetInstance();
            ofms.CancelarRequisicao(Base.SessaoUsuario.ID);

            PageBase.CaixaMensagens.Exibir(App_LocalResources.MensagensResource.REQUISICAO_CANCELADA);
            multiview.ActiveViewIndex = VIEW_POSICAO;
        }

        #endregion

        #endregion

        #region Métodos

        private void CancelarRequisicao()
        {
            OFMS ofms = OFMS.GetInstance();
            ofms.CancelarRequisicao(Base.SessaoUsuario.ID);
        }

        #endregion

        #region WebMethods

        [System.Web.Services.WebMethod]
        public static string GetAtendimento()
        {
            return TCC_Application.WebMethods.GetAtendimento(Base.SessaoUsuario.ID);
        }

        #endregion
    }
}