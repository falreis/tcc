<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetalheAtendimentos.ascx.cs" Inherits="TCC_Web.AreaTaxista.DetalheAtendimentos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<script type="text/javascript">
    var styleToSelect;
    // Add click handlers for buttons to show and hide modal popup on pageLoad
    function pageLoad() {
        $addHandler($get("showModalPopupClientButton"), 'click', showModalPopupViaClient);
        $addHandler($get("hideModalPopupViaClientButton"), 'click', hideModalPopupViaClient);
    }

    function showModalPopupViaClient(ev) {
        ev.preventDefault();
        var modalPopupBehavior = $find('programmaticModalPopupBehavior');
        modalPopupBehavior.show();
    }

    function hideModalPopupViaClient(ev) {
        ev.preventDefault();
        var modalPopupBehavior = $find('programmaticModalPopupBehavior');
        modalPopupBehavior.hide();
    }
</script>--%>

<asp:UpdatePanel ID="updDetalheAtendimento" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:LinkButton runat="server" ID="showModalPopupServerOperatorButton" Text="Detalhe Atendimento"
            OnClick="showModalPopupServerOperatorButton_Click" />
        
        <asp:Button runat="server" ID="hiddenForModalPopup" Style="display:none" />
        <ajaxToolkit:ModalPopupExtender runat="server" ID="modalPopup" BehaviorID="modalPopupBehavior"
            TargetControlID="hiddenForModalPopup" PopupControlID="panelPopup" DropShadow="True" 
            PopupDragHandleControlID="modalPopupDragHandle" RepositionMode="RepositionOnWindowScroll">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel runat="server" CssClass="modalpopup" ID="panelPopup" Width="350px">
            <div class="modalpopup-title">Detalhes do atendimento</div>
            <table>
                <tr>
                    <td>Início:</td>
                    <td><asp:Label ID="lblInicio" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Fim:</td>
                    <td><asp:Label ID="lblFim" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Responsável:</td>
                    <td><asp:Label ID="lblResponsavel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Status:</td>
                    <td><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                </tr>
            </table>
            <br />
            <div style="text-align:center;">
                <asp:Button ID="btnFechar" runat="server" Text="Fechar" CausesValidation="false" OnClick="btnFechar_Click" />
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
