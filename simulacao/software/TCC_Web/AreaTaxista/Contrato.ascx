<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contrato.ascx.cs" Inherits="TCC_Web.AreaTaxista.Contrato" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<script type="text/javascript">
    var styleToSelect;
    // Add click handlers for buttons to show and hide modal popup on pageLoad
    function pageLoad() {
        //$addHandler($get("showModalPopupClientButton"), 'click', showModalPopupViaClient);
        $addHandler($get("btnFechar"), 'click', hideModalPopupViaClient);
    }

    function showModalPopupViaClient(ev) {
        ev.preventDefault();
        var modalPopupBehavior = $find('modalPopupBehavior');
        modalPopupBehavior.show();
    }

    function hideModalPopupViaClient(ev) {
        ev.preventDefault();
        var modalPopupBehavior = $find('modalPopupBehavior');
        modalPopupBehavior.hide();
    }
</script>--%>

<asp:UpdatePanel ID="updDetalheAtendimento" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button runat="server" ID="hiddenForModalPopup" Style="display:none" />
        <ajaxToolkit:ModalPopupExtender runat="server" ID="modalPopup" BehaviorID="modalPopupBehavior"
            TargetControlID="hiddenForModalPopup" PopupControlID="panelPopup" DropShadow="True" 
            PopupDragHandleControlID="modalPopupDragHandle" RepositionMode="RepositionOnWindowScroll">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel runat="server" CssClass="modalpopup" ID="panelPopup" Width="550px">
            <div class="modalpopup-title">Teste de Funcionamento do Dispositivo</div>
            <div style="overflow:auto; height:500px;";>
                <p>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed posuere imperdiet arcu, a pulvinar sem viverra fringilla. 
                    Morbi vel luctus ante. Pellentesque augue tellus, tincidunt eu facilisis ac, rutrum nec dolor. 
                    Quisque varius volutpat mattis. Nam venenatis urna ac diam sollicitudin vel ullamcorper ipsum accumsan. 
                    Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. 
                    Cras venenatis sagittis dignissim. Morbi ac est ante, ac placerat turpis. Nam sed enim leo. 
                    Nunc dignissim, purus in pharetra adipiscing, turpis dolor rutrum nulla, et tempus enim est vel mi. 
                    Sed varius, magna eget pulvinar pharetra, risus nibh venenatis enim, sed condimentum turpis eros molestie felis. 
                    Fusce mattis neque dui. Proin sit amet odio diam, tincidunt fermentum odio. 
                    Pellentesque sodales, orci eget accumsan venenatis, enim sapien fermentum orci, tristique commodo nibh diam nec massa. 
                    Praesent nunc sem, dapibus eu iaculis molestie, ultrices sed purus.
                </p>
                <br />
                <p>
                    Vestibulum eu dui vitae dui porta sollicitudin sit amet non est. Praesent vitae sapien purus, vitae semper risus. 
                    Morbi mattis ligula quis est adipiscing a faucibus mi facilisis. Etiam vel purus sapien. Nunc sed neque mauris. 
                    Proin vitae metus eros, nec faucibus neque. Ut lacinia mollis mauris, quis lobortis eros convallis eget. 
                    Donec vel nisi id dui mollis adipiscing sed vel ante. Maecenas arcu nulla, adipiscing id tincidunt quis, aliquet at orci. 
                    Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; 
                    Vestibulum dapibus feugiat justo, et ornare orci feugiat sit amet. Aenean feugiat commodo dictum.
                </p>
                <br />
                <p>
                    Donec sed nisl eget justo aliquet pharetra vel ultrices enim. Suspendisse mollis nisi non tortor dictum a iaculis ante consectetur. 
                    In non urna ut risus scelerisque tempus ac vitae nunc. Proin sed aliquet orci. Etiam vel aliquet mi. 
                    In hac habitasse platea dictumst. Sed laoreet augue vitae velit malesuada nec ornare risus ornare. 
                    Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nam iaculis posuere urna. 
                    Sed arcu diam, molestie pharetra elementum sed, placerat vel nunc. Aenean semper vulputate turpis, a feugiat sem sodales nec. 
                    Donec quis tristique elit. Mauris sollicitudin consectetur nunc et malesuada. Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
                    Duis mi quam, vestibulum ultricies viverra vitae, faucibus vitae nisi.
                </p>
                <br />
                <p>
                    Nulla neque massa, viverra non ultrices ut, ornare quis quam. Morbi eleifend tempus ligula, ornare porttitor neque lacinia at. 
                    In suscipit mauris at dui consequat vitae consectetur nunc blandit. Nulla eu ipsum risus. 
                    In ut velit ut sem faucibus aliquet vitae id sem. Aenean ut nunc at felis venenatis euismod. 
                    Donec vitae nibh a felis scelerisque rutrum. Morbi nec mauris non sapien luctus euismod vitae sed enim. 
                    Phasellus vitae libero vel odio dapibus convallis id a nibh. Suspendisse porta augue et nibh pharetra luctus. 
                    Quisque lacinia neque vitae magna rutrum at ornare lectus interdum. Maecenas quis nisl lacus. Nulla eget massa arcu. 
                    Praesent vel vestibulum mi. Vivamus non aliquet eros.
                </p>
                <br />
                <p>
                    Donec diam tortor, lobortis ut feugiat sed, vestibulum quis nulla. Sed sem odio, dignissim sit amet mollis a, luctus et dolor. 
                    Aenean commodo sollicitudin massa eget imperdiet. Aenean vel ante quis ante pulvinar lacinia. 
                    Praesent sed ipsum nunc, nec suscipit ipsum. 
                    Morbi lobortis, massa sit amet fringilla hendrerit, enim leo porttitor dui, et feugiat lacus orci sed nisi. 
                    Pellentesque ac est ligula. Nulla facilisi. Mauris vitae enim lorem, vitae egestas quam. 
                    Quisque ac ante a enim rutrum pellentesque eu id diam. Proin blandit lacus non elit lobortis et suscipit felis aliquet.
                </p>
            </div>

            <br />
            <div style="text-align:center;">
                <asp:Button ID="btnAceitar" runat="server" Text="Aceitar" CausesValidation="false" />
                &nbsp;&nbsp;
                <asp:Button ID="btnRecusar" runat="server" Text="Recusar" CausesValidation="false" />
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>