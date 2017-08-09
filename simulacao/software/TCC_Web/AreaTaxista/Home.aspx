<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Taxista.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TCC_Web.AreaTaxista.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="http://maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var map;
        var markerTaxi, markerCli;
        var directionsDisplay;

        function GetLocation() {
            PageMethods.GetLocation(CallSuccess, CallFailed);
        }

        function CallFailed() {
            alert("error");
        }

        function CallSuccess(res) {
            if (res == "")
                return;

            var split = res.split("|");
            var latitude = split[0];
            var longitude = split[1];
            var latlng = new google.maps.LatLng(latitude, longitude);

            if (!map) {
                var mapcanvas = document.createElement('div');
                mapcanvas.id = 'mapcanvas';
                mapcanvas.style.height = '600px';
                mapcanvas.style.width = '100%';
                document.querySelector('#map').appendChild(mapcanvas);
                var myOptions = {
                    zoom: 15,
                    center: latlng,
                    mapTypeControl: false,
                    navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL },
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };

                map = new google.maps.Map(document.getElementById("mapcanvas"), myOptions);

                new google.maps.TransitLayer().setMap(map);
            }

            if (markerTaxi)
                markerTaxi.setMap(null);

            markerTaxi = new google.maps.Marker({
                position: latlng,
                map: map,
                title: "Taxista",
                icon: '../images/marker_taxi.png'
            });

            if (directionsDisplay)
                directionsDisplay.setMap(null);
        }
    </script>

    <script type="text/javascript">
        function GetAtendimento() {
            PageMethods.GetAtendimento(AtendimentoSuccess, AtendimentoFailed);
        }

        function AtendimentoFailed() {
            alert("error");
        }

        function AtendimentoSuccess(res) {
            if (res == "")
                return;

            var split = res.split("|");
            var latTaxi = split[0];
            var lgnTaxi = split[1];
            var latCli = split[2];
            var lgnCli = split[3];

            var latlngTaxi = new google.maps.LatLng(latTaxi, lgnTaxi);
            var latlngCli = new google.maps.LatLng(latCli, lgnCli);

            if (!map) {
                var mapcanvas = document.createElement('div');
                mapcanvas.id = 'mapcanvas';
                mapcanvas.style.height = '600px';
                mapcanvas.style.width = '90%';
                document.querySelector('#map').appendChild(mapcanvas);
                var latlngTaxi = new google.maps.LatLng(latTaxi, lgnTaxi);
                var latlngCli = new google.maps.LatLng(latCli, lgnCli);

                var myOptions = {
                    center: latlngTaxi,
                    mapTypeControl: false,
                    navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL },
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };

                map = new google.maps.Map(document.getElementById("mapcanvas"), myOptions);

                new google.maps.TransitLayer().setMap(map);
            }

            if(markerTaxi)
                markerTaxi.setMap(null);

            markerTaxi = new google.maps.Marker({
                position: latlngTaxi,
                map: map,
                title: "Taxista",
                icon: '../images/marker_taxi.png'
            });

            if (markerCli)
                markerTaxi.setMap(null);

            markerCli = new google.maps.Marker({
                position: latlngCli,
                map: map,
                title: "Cliente",
                icon: '../images/marker_passenger.png'
            });

            //directions
            directionsDisplay = new google.maps.DirectionsRenderer();
            var directionsService = new google.maps.DirectionsService();

            var request = {
                origin: latlngTaxi,
                destination: latlngCli,
                travelMode: google.maps.TravelMode.DRIVING
            };

            directionsService.route(request, function (result, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(result);
                }
            });

            directionsDisplay.setMap(map);

//            var trafficLayer = new google.maps.TrafficLayer();
//            trafficLayer.setMap(map);
        }   
    </script>


    <asp:UpdatePanel id="upd" runat="server">
        <ContentTemplate>
            <h2><asp:Literal ID="lblTitulo" runat="server"></asp:Literal></h2> 

            <asp:MultiView ID="multiview" runat="server">
                <asp:View ID="viewLocation" runat="server">
                    <asp:Timer ID="timerLocation" runat="server" Interval="3000"></asp:Timer>
                </asp:View>
                <asp:View ID="viewRespostaAtendimento" runat="server">
                    Existe uma requisição de taxi para ser atendida.
                    <table>
                        <tr>
                            <td><asp:Button ID="btnAtender" runat="server" Text="Atender" CssClass="bigbutton" /></td>
                            <td><asp:Button ID="btnRecusar" runat="server" Text="Recusar" CssClass="bigbutton" /></td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="viewAguardandoConfirmacao" runat="server">
                    Aguardando confirmação do cliente..
                    <br />
                    <asp:Image ID="imgAguardeConfirmacao" runat="server" ImageUrl="~/images/carregando.gif" />
                    <asp:Timer ID="timerAguardandoConfirmacao" runat="server" Interval="3000"></asp:Timer>
                </asp:View>
                <asp:View ID="viewAguardandoAtendimento" runat="server">
                    <center>
                        <div class="requisicao-text">
                            Por favor, desloque-se até o local onde encontra-se o cliente..
                            <br />- Distância: <asp:Label ID="lblDistancia" runat="server"></asp:Label>
                            <br />- Previsão: <asp:Label ID="lblPrevisao" runat="server" Text="0"></asp:Label>
                            <br />- Cliente: <asp:Label ID="lblCliente" runat="server"></asp:Label>
                        </div>
                        <div class="requisicao-rota">
                            <br />Rota recomendada:
                            <br />
                            <div style="padding-left:12px">
                                <asp:Label ID="lblRota" runat="server"></asp:Label>
                            </div>
                        </div>
                    </center>
                    <asp:Button ID="btnIniciarAtendimento" runat="server" Text="Iniciar Atendimento" CssClass="bigbutton" />
                    <br />
                    <br />
                </asp:View>
                <asp:View ID="viewEmAtendimento" runat="server">
                    <asp:Button ID="btnFinalizarAtendimento" runat="server" Text="Finalizar Atendimento" CssClass="bigbutton" />
                    <br />
                    <br />
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="map"></div>
</asp:Content>
