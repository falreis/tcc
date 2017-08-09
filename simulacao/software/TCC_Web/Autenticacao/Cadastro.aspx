<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Autenticacao.master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="TCC_Web.Cadastro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <table>
        <tr>
            <td>Tipo de Pessoa:</td>
            <td>
                <asp:UpdatePanel ID="updTipoPessoa" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rbtTipoPessoa" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"></asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

    <asp:UpdatePanel ID="updMultiview" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:MultiView ID="multiviewTipoPessoa" runat="server" ActiveViewIndex="0">
            <asp:View ID="viewPessoaFisica" runat="server">
                <h2>Dados Pessoais</h2>
                <table>
                    <tr>
                        <td>Nome:</td>
                        <td>
                            <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNome" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtNome" />
                        </td>
                    </tr>
                    <tr>
                        <td>Apelido:</td>
                        <td>
                            <asp:TextBox ID="txtApelido" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvApelido" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtApelido" />
                        </td>
                    </tr>
                    <tr>
                        <td>CPF:</td>
                        <td>
                            <asp:TextBox ID="txtCPF" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCPF" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtCPF" />
                            <asp:MaskedEditExtender ID="maskCPF" runat="server" Mask="999,999,999-99" PromptCharacter=" " 
                                TargetControlID="txtCPF" ></asp:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>RG:</td>
                        <td>
                            <asp:TextBox ID="txtRG" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRG" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtRG" />
                        </td>
                    </tr>
                    <tr>
                        <td>Sexo:</td>
                        <td>
                            <asp:RadioButtonList ID="rbtSexo" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvSexo" runat="server" Text="*" Display="Dynamic" ControlToValidate="rbtSexo" />
                        </td>
                    </tr>
                    <tr>
                        <td>Data Nascimento:</td>
                        <td>
                            <asp:TextBox ID="txtDataNascimento" runat="server"></asp:TextBox>
                            <asp:MaskedEditExtender ID="maskDataNascimento" runat="server" Mask="99/99/9999" PromptCharacter=" " 
                                TargetControlID="txtDataNascimento" ClearMaskOnLostFocus="false" MaskType="DateTime"></asp:MaskedEditExtender>
                            <asp:ImageButton runat="server" ID="imgDataNascimento" ImageUrl="~/images/calendar.png" CausesValidation="false" />
                            <asp:CalendarExtender ID="calDataNascimento" runat="server" TargetControlID="txtDataNascimento" PopupButtonID="imgDataNascimento" />
                            <asp:RequiredFieldValidator ID="rfvDataNascimento" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtDataNascimento" />
                        </td>
                    </tr>
                    <tr>
                        <td>Estado Civil:</td>
                        <td>
                            <asp:DropDownList id="ddlEstadoCivil" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEstadoCivil" runat="server" Text="*" Display="Dynamic" ControlToValidate="ddlEstadoCivil" />
                        </td>
                    </tr>
                    <asp:Panel ID="pnlProfissao" runat="server">
                        <tr>
                            <td>Profissão:</td>
                            <td>
                                <asp:TextBox ID="txtProfissao" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </asp:View>
            <asp:View ID="viewPessoaJuridica" runat="server">
                <h2>Dados da Empresa</h2>
                <table>
                    <tr>
                        <td>Razão Social:</td>
                        <td>
                            <asp:TextBox ID="txtRazaoSocial" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRazaoSocial" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtRazaoSocial" />
                        </td>
                    </tr>
                    <tr>
                        <td>Nome Fantasia:</td>
                        <td>
                            <asp:TextBox ID="txtNomeFantasia" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNomeFantasia" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtNomeFantasia" />
                        </td>
                    </tr>
                    <tr>
                        <td>CNPJ:</td>
                        <td>
                            <asp:TextBox ID="txtCNPJ" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCNPJ" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtCNPJ" />
                            <asp:MaskedEditExtender ID="maskCNPJ" runat="server" Mask="99,999,999/9999-99" PromptCharacter=" " 
                                TargetControlID="txtCNPJ" ></asp:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Inscrição Estadual:</td>
                        <td>
                            <asp:TextBox ID="txtInscricaoEstadual" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInscricaoEstadual" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtInscricaoEstadual" />
                        </td>
                    </tr>
                    <tr>
                        <td>Ramo Atividade:</td>
                        <td>
                            <asp:DropDownList id="ddlRamoAtividade" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRamoAtividade" runat="server" Text="*" Display="Dynamic" ControlToValidate="ddlRamoAtividade" />
                        </td>
                    </tr>
                    <tr>
                        <td>Site:</td>
                        <td>
                            <asp:TextBox ID="txtSite" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSite" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtSite" />
                            <asp:RegularExpressionValidator ID="revSite" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtSite" 
                                ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <h2>Responsável</h2>
                <table>
                    <tr>
                        <td>Nome:</td>
                        <td>
                            <asp:TextBox ID="txtNomeResponsavel" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator if="rfvNomeResponsavel" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtNomeResponsavel"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Setor:</td>
                        <td>
                            <asp:TextBox ID="txtSetorResponsavel" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSetorResponsavel" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtSetorResponsavel"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Cargo:</td>
                        <td>
                            <asp:TextBox ID="txtCargoResponsavel" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCargoResponsavel" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtCargoResponsavel"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Sexo:</td>
                        <td>
                            <asp:RadioButtonList ID="rbtSexoResponsavel" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvSexoResponsavel" runat="server" Text="*" Display="Dynamic" ControlToValidate="rbtSexoResponsavel" />
                        </td>
                    </tr>
                    <tr>
                        <td>Email:</td>
                        <td>
                            <asp:TextBox ID="txtEmailResponsavel" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmailResponsavel" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtEmailResponsavel" />
                            <asp:RegularExpressionValidator ID="revEmailResponsavel" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtEmailResponsavel" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <br />
    <h2>Endereço</h2>
    <table>
        <tr>
            <td>CEP:</td>
            <td>
                <asp:TextBox ID="txtCEP" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCEP" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtCEP" />
                <asp:MaskedEditExtender ID="maskCEP" runat="server" Mask="99,999-999" PromptCharacter=" " TargetControlID="txtCEP" ></asp:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td>Estado:</td>
            <td>
                <asp:DropDownList ID="ddlUF" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvUF" runat="server" Text="*" Display="Dynamic" ControlToValidate="ddlUF"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Cidade:</td>
            <td>
                <asp:TextBox ID="txtCidade" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCidade" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtCidade"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Bairro:</td>
            <td>
                <asp:TextBox ID="txtBairro" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvBairro" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtBairro"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Logradouro:</td>
            <td>
                <asp:TextBox ID="txtLogradouro" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLogradouro" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtLogradouro"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Número:</td>
            <td>
                <asp:TextBox ID="txtNumero" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumero" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtNumero" />
            </td>
        </tr>
        <tr>
            <td>Complemento:</td>
            <td>
                <asp:TextBox ID="txtComplemento" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Referência:</td>
            <td>
                <asp:TextBox ID="txtReferencia" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>

    <br />
    <h2>Telefones:</h2>
    <table>
        <tr>
            <td>Residencial:</td>
            <td>
                <asp:TextBox ID="txtTelefoneResidencial" runat="server"></asp:TextBox>
                <asp:MaskedEditExtender ID="maskTelefoneResidencial" runat="server" Mask="(99)9999-9999" PromptCharacter=" " 
                    TargetControlID="txtTelefoneResidencial" ></asp:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td>Comercial:</td>
            <td>
                <asp:TextBox ID="txtTelefoneComercial" runat="server"></asp:TextBox>
                <asp:MaskedEditExtender ID="maskTelefoneComercial" runat="server" Mask="(99)9999-9999" PromptCharacter=" " 
                    TargetControlID="txtTelefoneComercial" ></asp:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td>Celular:</td>
            <td>
                <asp:TextBox ID="txtTelefoneCelular" runat="server"></asp:TextBox>
                <asp:MaskedEditExtender ID="maskTelefoneCelular" runat="server" Mask="(99)9999-9999" PromptCharacter=" " 
                    TargetControlID="txtTelefoneCelular" ></asp:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:CheckBox ID="chkEnvioSMS" runat="server" 
                    Text="Permitir o envio de notificações do uso de software por SMS." />
            </td>
        </tr>
    </table>

    <br />
    <h2>Dados de Identificação:</h2>
    <table>
        <tr>
            <td>Email:</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtEmail" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtEmail" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Senha:</td>
            <td>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSenha" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtSenha" />
            </td>
        </tr>
        <tr>
            <td>Confirme Senha:</td>
            <td>
                <asp:TextBox ID="txtConfirmeSenha" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConfirmeSenha" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtConfirmeSenha" />
                <asp:CompareValidator ID="cmpConfirmeSenha" runat="server" Text="*" Display="Dynamic" Operator="Equal"
                    ControlToValidate="txtConfirmeSenha" ControlToCompare="txtSenha"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:CheckBox ID="chkEnvioEmail" runat="server" 
                    Text="Permitir o envio de notificações do uso de software por email." />
            </td>
        </tr>
    </table>

    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
