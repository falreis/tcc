<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBox.ascx.cs" Inherits="TCC_Web.MessageBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script type="text/javascript">
    function hideModalPopupViaClient() {
        var modalPopupBehavior = $find('modalMessageBoxPopupBehavior');
        modalPopupBehavior.hide();
    }

    function showMessageBox() {
        var modalPopupBehavior = $find('modalMessageBoxPopupBehavior');
        modalPopupBehavior.show();
    }
</script>

<asp:UpdatePanel ID="upMessageBox" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:Button runat="server" ID="hiddenForModalPopup" Style="display:none" />
        <ajaxToolkit:ModalPopupExtender runat="server" ID="modalPopup" BehaviorID="modalMessageBoxPopupBehavior"
            TargetControlID="hiddenForModalPopup" PopupControlID="panelMessageBoxPopup" DropShadow="True" 
            PopupDragHandleControlID="modalPopupMessageBoxDragHandle" RepositionMode="None">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel runat="server" CssClass="modalpopup" ID="panelMessageBoxPopup" Width="350px">
            <div class="modalpopup-title" style="width:94%">
                <span style="width:100%">
                    <asp:Literal ID="lblTitulo" runat="server" EnableViewState="false"></asp:Literal>
                </span>
            </div>
            <br /> 
            <div style="padding-top:3px;">
                <asp:Label ID="lblMensagem" runat="server" EnableViewState="false"></asp:Label>
            </div>
            <br />
            <div style="text-align:center;">
                <asp:Button ID="btnFechar" runat="server" Text="Fechar" EnableViewState="false" CausesValidation="false" 
                    OnClick="btnFechar_Click" OnClientClick="hideModalPopupViaClient();" />
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnFechar" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
