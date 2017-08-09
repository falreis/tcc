<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="TCC_Web.ContactUs" MasterPageFile="~/Master/Publico.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

<style type="text/css">
    input[type="text"], textarea
    {
        width: 350px;
    }
</style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="main">
	    <table>
		    <tr>
			    <td width="25%" style="vertical-align:top;">
				    <p>
				    <span style="font-size: 16px; font-weight: bold; padding-bottom: 7px; display: block;">Informações</span>
				    Responsável: Felipe A. L. Reis 
				    <br />Email: falreis@gmail.com
				    <br />Skype: falreis
								
				    <br /><br />
				    Orientação: Marconi A. Pereira
				    <br />Email: marconi@decom.cefetmg.br
								
				    <br />
				    <br />Engenharia de Computação
				    <br />Departamento de Computação 
				    <br />DECOM - CEFET-MG
			    </p>
			    </td>
			    <td>
                    <br />
                    <table style="margin-left:100px">
                        <tr>
                            <td>Nome:</td>
                            <td><asp:TextBox ID="txtNome" runat="server" Width="350px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Email:</td>
                            <td><asp:TextBox ID="txtEmail" runat="server" Width="350px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Assunto:</td>
                            <td><asp:TextBox ID="txtAssunto" runat="server" Width="350px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Assunto:</td>
                            <td><asp:TextBox ID="txtMensagem" runat="server" TextMode="MultiLine" Rows="9" Width="350px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:right;"><asp:Button ID="btnEnviar" runat="server" Text="Enviar" /></td>
                        </tr>
                    </table>
			    </td>
		    </tr>
	    </table>
    </div>
</asp:Content>
