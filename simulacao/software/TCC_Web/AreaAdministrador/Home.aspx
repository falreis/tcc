<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Administrador.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TCC_Web.AreaAdministrador.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="http://maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script>
    <script type="text/javascript">
        var map, directions;
        var markers = [];

        var StatusTaxista =
        {
            Pendente : 0,
            SemStatus : 1,
            TaxiLivre : 2,
            AguardandoConfirmacaoRequisicao: 3,
            EmAtendimento : 4,
            Suspenso : 5,
            ForaCirculacao : 6        
        }

        google.maps.event.addDomListener(window, 'load', initialize);

        function initialize() {
            var latlng = new google.maps.LatLng(-19.904356, -43.925691);
            var myOptions = {
                zoom: 12,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                panControl: false,
                zoomControl: true,
                mapTypeControl: true,
                scaleControl: true,
                streetViewControl: false,
                overviewMapControl: false
            };

            map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

            //transit layer
            var transitLayer = new google.maps.TransitLayer();
            transitLayer.setMap(map);

            //atualização
            setInterval("Atualizar()", 10000);
        }

        function placeMarker(latitude, longitude, placa, status) {
            var myicon = '../images/marker_taxi_green.png'
            switch (status) {
                case 2:
                    myicon = '../images/marker_taxi_green.png';
                    break;
                case 3:
                    myicon = '../images/marker_taxi_yellow.png'
                    break;
                case 4:
                    myicon = '../images/marker_taxi_red.png'
                    break;
            }

            var marker = new google.maps.Marker({
                position: new google.maps.LatLng(latitude, longitude),
                map: map,
                title: placa,
                icon: myicon
            });

            var marcador = new Object();
            marcador.marker = marker;
            marcador.status = status;

            markers.push(marcador);
        }

        function LimparMarcadores() {
            if (markers) {
                for (i in markers) {
                    markers[i].marker.setMap(null);
                }
                markers.length = 0;
            }
        }

        function Atualizar() 
        {
            // call server side method
            PageMethods.Teste(CallSuccess, CallFailed);
        }

        function CallSuccess(res) 
        {
            LimparMarcadores();
            var taxistas =  jQuery.parseJSON(res);
            for (i = 0; i < taxistas.Taxista.length; i++)
                placeMarker(taxistas.Taxista[i].latitude, taxistas.Taxista[i].longitude, taxistas.Taxista[i].placa, taxistas.Taxista[i].status)
        }

        function CallFailed(res)
        {
            alert("error");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Informações de Taxistas</h2>
    <asp:UpdatePanel id="updTaxistas" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:GridView ID="gvTaxistas" runat="server" EmptyDataText="Nenhum taxista encontrado!" AutoGenerateColumns="false" Visible="false">
                <Columns>
                    <asp:TemplateField HeaderText="Nome">
                        <ItemTemplate>
                            <asp:Label ID="lblNome" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Latitude">
                        <ItemTemplate>
                            <asp:Label ID="lblLatitude" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Longitude">
                        <ItemTemplate>
                            <asp:Label ID="lblLongitude" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Última Atualização">
                        <ItemTemplate>
                            <asp:Label ID="lblDataHora" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="timerTaxista" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Timer ID="timerTaxista" runat="server" Interval="20000"></asp:Timer>

    <br />
    <br />
    <div id="map_canvas" style="height:600px; width:800px;"></div>
</asp:Content>
