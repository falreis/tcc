<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Downloads.aspx.cs" Inherits="TCC_Web.Downloads" MasterPageFile="~/Master/Publico.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
		.title-button{
			color: #8E7217;
			font-family: 'CorbenBold';
			font-size: 18px;
			text-decoration: none;
		}
		
		.description-button{
			color: #63541F;
			font-family: 'Arial';
			font-size: 14px;
			text-decoration: none;
		}
		
		.button{
		    background-color: #EAB51D;
		    background-size: 100%;
			height: 50px;
			width: 250px;
			text-align:center;
			padding:10px;
		}
	</style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="main">
		<p>Faça download agora para as plataformas Windows Mobile e Android.</p>
		<table cellpadding="20px" align="center">
			<tr>
				<td>
					<div class="button">
						<a href="shop.html" class="title-button">Windows Mobile</a><br />
						<a href="shop.html" class="description-button">Em breve em versão para download.</a>
					</div>
				</td>
				<td>
					<div class="button">
						<a href="shop.html" class="title-button">Android</a><br />
						<a href="shop.html" class="description-button">Em breve em versão para download.</a>
					</div>
				</td>
			</tr>
		</table>
		<br /><br />
	</div>
</asp:Content>
