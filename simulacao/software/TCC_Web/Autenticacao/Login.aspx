<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Autenticacao.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TCC_Web.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Login</h2>
    <table class="login-table">
        <tr>
            <td>E-mail:</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" TabIndex="1" Width="95%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtEmail" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Senha:</td>
            <td>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" TabIndex="2" Width="95%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSenha" runat="server" Text="*" Display="Dynamic" ControlToValidate="txtSenha"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:right;padding-right:10px;">
                <asp:Button ID="btnEntrar" runat="server" Text="Entrar" TabIndex="3" CssClass="button" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="2" style="text-align:right">
                <asp:LinkButton ID="lkbCadastro" runat="server" EnableViewState="false" Text="Cadastre-se" Font-Size="12px" CausesValidation="false"></asp:LinkButton>
            </td>
        </tr>
    </table>    
</asp:Content>
