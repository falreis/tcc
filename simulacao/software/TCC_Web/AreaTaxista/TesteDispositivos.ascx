<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TesteDispositivos.ascx.cs" Inherits="TCC_Web.AreaTaxista.TesteDispositivos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:UpdatePanel ID="updDetalheAtendimento" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:Button runat="server" ID="hiddenForModalPopup" Style="display:none" />
        <ajaxToolkit:ModalPopupExtender runat="server" ID="modalPopup" BehaviorID="modalPopupBehavior"
            TargetControlID="hiddenForModalPopup" PopupControlID="panelPopup" DropShadow="True" 
            PopupDragHandleControlID="modalPopupDragHandle" RepositionMode="RepositionOnWindowScroll">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel runat="server" CssClass="modalpopup" ID="panelPopup" Width="350px">
            <div class="modalpopup-title">Teste de Funcionamento do Dispositivo</div>
            <table>
                <tr>
                    <td>Número Série:</td>
                    <td><asp:Label ID="lblNumeroSerie" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
                <tr>
                    <td>Código de Segurança:</td>
                    <td><asp:Label ID="lblCodigoSegurança" runat="server" EnableViewState="false"></asp:Label></td>
                </tr>
            </table>
            <br />
            <span class="modalpopup-title">Teste do Dispositivo:</span>
            <asp:TextBox ID="txtInformacoes" runat="server" TextMode="MultiLine" Enabled="false" Width="100%"></asp:TextBox>

            <br />
            <div style="text-align:center;">
                <asp:Button ID="btnFechar" runat="server" Text="Fechar" CausesValidation="false" />
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>