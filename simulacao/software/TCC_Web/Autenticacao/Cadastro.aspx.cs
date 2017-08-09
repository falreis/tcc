using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using TCC_Domain.Domain;
using TCC_Infrastructure.Extensions;

namespace TCC_Web
{
    public partial class Cadastro : System.Web.UI.Page
    {
        #region Propriedades

        public TipoUsuario? TipoUsuarioLogin
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString[Constantes.CONSTANTE_QUERY_TIPO_USUARIO]))
                {
                    if (Request.QueryString[Constantes.CONSTANTE_QUERY_TIPO_USUARIO].ToUpper() == "TAXISTA")
                        return TipoUsuario.Taxista;
                    else if (Request.QueryString[Constantes.CONSTANTE_QUERY_TIPO_USUARIO].ToUpper() == "CLIENTE")
                        return TipoUsuario.Cliente;
                }
                return null;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            //base.OnInit(e);
            rbtTipoPessoa.SelectedIndexChanged += new EventHandler(rbtTipoPessoa_SelectedIndexChanged);
            btnConfirmar.Click += new EventHandler(btnConfirmar_Click);
            btnCancelar.Click += new EventHandler(btnCancelar_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!this.TipoUsuarioLogin.HasValue)
                    Response.Redirect("~/");

                CarregarControles();
            }
        }

        protected void rbtTipoPessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(rbtTipoPessoa.SelectedValue) == (int)TipoPessoa.PessoaFisica)
                multiviewTipoPessoa.ActiveViewIndex = 0;
            else
                multiviewTipoPessoa.ActiveViewIndex = 1;
            updMultiview.Update();
        }

        protected void btnConfirmar_Click(Object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (this.TipoUsuarioLogin.HasValue)
                {
                    if (this.TipoUsuarioLogin.Value == TipoUsuario.Taxista)
                    {
                        Taxista taxista = ObterDadosFormularioTaxista();
                        Pessoa pessoa = null;

                        if (Convert.ToInt32(rbtTipoPessoa.SelectedValue) == (int)TipoPessoa.PessoaFisica)
                            pessoa = ObterDadosFormularioPessoaFisica();
                        else
                            pessoa = ObterDadosFormularioPessoaJuridica();
                        
                        taxista.Pessoa = pessoa;
                        taxista.Salvar();

                        //realiza login do usuario
                        Session.Add(Constantes.CONSTANTE_SESSAO_USUARIO, taxista);

                        //exibe mensagem de confirmação do cadastro
                        PageBase.CaixaMensagens.ExibirRedirecionar(App_LocalResources.LabelsResource.CADASTRO, App_LocalResources.MensagensResource.CADASTRO_SUCESSO, Constantes.CONSTANTE_PAGINA_HOME_TAXISTA);
                    }
                    else if (this.TipoUsuarioLogin.Value == TipoUsuario.Cliente)
                    {
                        Cliente cliente = ObterDadosFormularioCliente();
                        Pessoa pessoa = null;

                        if (Convert.ToInt32(rbtTipoPessoa.SelectedValue) == (int)TipoPessoa.PessoaFisica)
                            pessoa = ObterDadosFormularioPessoaFisica();
                        else
                            pessoa = ObterDadosFormularioPessoaJuridica();

                        cliente.Pessoa = pessoa;
                        cliente.Salvar();

                        //realiza login do usuário
                        Session.Add(Constantes.CONSTANTE_SESSAO_USUARIO, cliente);

                        //exibe mensagem de confirmação do cadastro
                        PageBase.CaixaMensagens.ExibirRedirecionar(App_LocalResources.LabelsResource.CADASTRO, App_LocalResources.MensagensResource.CADASTRO_SUCESSO, Constantes.CONSTANTE_PAGINA_HOME_CLIENTE);
                    }
                    else
                        PageBase.CaixaMensagens.ExibirRedirecionar(App_LocalResources.LabelsResource.CADASTRO, App_LocalResources.MensagensResource.TIPO_USUARIO_NAO_DEFINIDO, Constantes.CONSTANTE_PAGINA_LOGIN);
                }
                else
                    PageBase.CaixaMensagens.ExibirRedirecionar(App_LocalResources.LabelsResource.CADASTRO, App_LocalResources.MensagensResource.TIPO_USUARIO_NAO_DEFINIDO, Constantes.CONSTANTE_PAGINA_LOGIN);

                updMultiview.Update();
            }
        }

        protected void btnCancelar_Click(Object sender, EventArgs e)
        {
            Response.Redirect(Constantes.CONSTANTE_PAGINA_LOGIN);
        }

        #endregion 

        #region Métodos

        private void CarregarControles()
        {
            CarregarRadioButtonTipoPessoa();
            CarregarCaixaEstado();
            CarregarCaixaEstadoCivil();
            CarregarCaixaRamoAtividade();
            CarregarRadioButtonSexoESexoResponsavel();

            if (this.TipoUsuarioLogin.Value == TipoUsuario.Taxista)
            {
                pnlProfissao.Visible = false;
                txtProfissao.Text = "Taxista";
            }
        }

        private void CarregarRadioButtonTipoPessoa()
        {
            foreach (TipoPessoa tipoPessoa in Enum.GetValues(typeof(TipoPessoa)))
                rbtTipoPessoa.Items.Add(new ListItem(tipoPessoa.ObterDescricao(), tipoPessoa.ObterValor()));
            rbtTipoPessoa.Items[0].Selected = true;
        }

        private void CarregarCaixaEstado()
        {
            foreach (UF uf in Enum.GetValues(typeof(UF)))
                ddlUF.Items.Add(new ListItem(uf.ObterDescricao(), uf.ObterValor()));
            ddlUF.AdicionarItemDefault();
        }

        private void CarregarCaixaEstadoCivil()
        {
            foreach (EstadoCivil estadoCivil in Enum.GetValues(typeof(EstadoCivil)))
                ddlEstadoCivil.Items.Add(new ListItem(estadoCivil.ObterDescricao(), estadoCivil.ObterValor()));
            ddlEstadoCivil.AdicionarItemDefault();
        }

        private void CarregarCaixaRamoAtividade()
        {
            foreach (RamoAtividade ramoAtividade in Enum.GetValues(typeof(RamoAtividade)))
                ddlRamoAtividade.Items.Add(new ListItem(ramoAtividade.ObterDescricao(), ramoAtividade.ObterValor()));
            ddlRamoAtividade.AdicionarItemDefault();
        }

        private void CarregarRadioButtonSexoESexoResponsavel()
        {
            foreach (Sexo sexo in Enum.GetValues(typeof(Sexo)))
            {
                rbtSexo.Items.Add(new ListItem(sexo.ObterDescricao(), sexo.ObterValor()));
                rbtSexoResponsavel.Items.Add(new ListItem(sexo.ObterDescricao(), sexo.ObterValor()));
            }
        }

        private PessoaFisica ObterDadosFormularioPessoaFisica()
        {
            PessoaFisica pessoaFisica = new PessoaFisica();
            pessoaFisica.Nome = txtNome.Text;
            pessoaFisica.Apelido = txtApelido.Text;
            pessoaFisica.CPF = txtCPF.Text;
            pessoaFisica.DataNascimento = Convert.ToDateTime(txtDataNascimento.Text);
            pessoaFisica.EstadoCivil = (EstadoCivil)Convert.ToInt32(ddlEstadoCivil.SelectedValue);
            pessoaFisica.Profissao = txtProfissao.Text;
            pessoaFisica.RG = txtRG.Text;
            pessoaFisica.Sexo = (Sexo)Convert.ToInt32(rbtSexo.SelectedValue);

            pessoaFisica.Endereco = new Endereco();
            pessoaFisica.Endereco.CEP = txtCEP.Text;
            pessoaFisica.Endereco.Estado = (UF)Convert.ToInt32(ddlUF.SelectedValue);
            pessoaFisica.Endereco.Cidade = txtCidade.Text;
            pessoaFisica.Endereco.Logradouro = txtLogradouro.Text;
            pessoaFisica.Endereco.Numero = txtNumero.Text;
            pessoaFisica.Endereco.Complemento = txtComplemento.Text;
            pessoaFisica.Endereco.Referencia = txtReferencia.Text;

            pessoaFisica.Telefones = this.ObterDadosFormularioTelefones();
            foreach (Telefone telefone in pessoaFisica.Telefones)
                telefone.Pessoa = pessoaFisica;

            return pessoaFisica;
        }

        private PessoaJuridica ObterDadosFormularioPessoaJuridica()
        {
            PessoaJuridica pessoaJuridica = new PessoaJuridica();
            pessoaJuridica.CNPJ = txtCNPJ.Text;
            pessoaJuridica.RazaoSocial = txtRazaoSocial.Text;
            pessoaJuridica.NomeFantasia = txtNomeFantasia.Text;
            pessoaJuridica.InscricaoEstadual = txtInscricaoEstadual.Text;
            pessoaJuridica.Site = txtSite.Text;
            pessoaJuridica.RamoAtividade = (RamoAtividade)Convert.ToInt32(ddlRamoAtividade.SelectedValue);

            pessoaJuridica.NomeResponsavel = txtNomeResponsavel.Text;
            pessoaJuridica.SetorResponsavel = txtSetorResponsavel.Text;
            pessoaJuridica.CargoResponsavel = txtCargoResponsavel.Text;
            pessoaJuridica.SexoResponsavel = (Sexo)Convert.ToInt32(ddlRamoAtividade.SelectedValue);
            pessoaJuridica.EmailResponsavel = txtEmailResponsavel.Text;

            pessoaJuridica.Endereco = new Endereco();
            pessoaJuridica.Endereco.CEP = txtCEP.Text;
            pessoaJuridica.Endereco.Estado = (UF)Convert.ToInt32(ddlUF.SelectedValue);
            pessoaJuridica.Endereco.Cidade = txtCidade.Text;
            pessoaJuridica.Endereco.Logradouro = txtLogradouro.Text;
            pessoaJuridica.Endereco.Numero = txtNumero.Text;
            pessoaJuridica.Endereco.Complemento = txtComplemento.Text;
            pessoaJuridica.Endereco.Referencia = txtReferencia.Text;

            pessoaJuridica.Telefones = this.ObterDadosFormularioTelefones();
            foreach (Telefone telefone in pessoaJuridica.Telefones)
                telefone.Pessoa = pessoaJuridica;

            return pessoaJuridica;
        }

        private Taxista ObterDadosFormularioTaxista()
        {
            Taxista taxista = new Taxista();
            taxista.Email = txtEmail.Text;
            taxista.Senha = txtSenha.Text;
            taxista.PermitirNotificacaoEmail = chkEnvioEmail.Checked;
            return taxista;
        }

        private Cliente ObterDadosFormularioCliente()
        {
            Cliente cliente = new Cliente();
            cliente.Email = txtEmail.Text;
            cliente.Senha = txtSenha.Text;
            cliente.PermitirNotificacaoEmail = chkEnvioEmail.Checked;
            return cliente;
        }

        private List<Telefone> ObterDadosFormularioTelefones()
        {
            const int LENGTH_TELEFONE = 10;
            List<Telefone> telefones = new List<Telefone>();

            //telefone residencial
            if (!string.IsNullOrEmpty(txtTelefoneResidencial.Text) && txtTelefoneResidencial.Text.Length == LENGTH_TELEFONE)
                telefones.Add(new Telefone(txtTelefoneResidencial.Text.Substring(0,2), txtTelefoneResidencial.Text.Substring(2), false));

            //telefone comercial
            if (!string.IsNullOrEmpty(txtTelefoneComercial.Text) && txtTelefoneComercial.Text.Length == LENGTH_TELEFONE)
                telefones.Add(new Telefone(txtTelefoneComercial.Text.Substring(0, 2), txtTelefoneComercial.Text.Substring(2), false));

            //telefone celular
            if (!string.IsNullOrEmpty(txtTelefoneCelular.Text) && txtTelefoneCelular.Text.Length == LENGTH_TELEFONE)
                telefones.Add(new Telefone(txtTelefoneCelular.Text.Substring(0, 2), txtTelefoneCelular.Text.Substring(2), chkEnvioSMS.Checked));

            return telefones;
        }

        #endregion
    }
}