<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Cliente.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TCC_Web.AreaCliente.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        if (navigator.geolocation)
            navigator.geolocation.getCurrentPosition(vwPosicaoMapaSucesso, vwPosicaoMapaErro);
        else
            error('Navegador não suporta geolocalização.');

        function vwPosicaoMapaSucesso(position) {
            //Jquery Code 
            var mapcanvas = document.createElement('div');
            mapcanvas.id = 'mapcanvas';
            mapcanvas.style.height = '600px';
            mapcanvas.style.width = '90%';
            document.querySelector('#map').appendChild(mapcanvas);
            var latlng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            var myOptions = {
                zoom: 15,
                center: latlng,
                mapTypeControl: false,
                navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL },
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("mapcanvas"), myOptions);
            var marker = new google.maps.Marker({
                position: latlng,
                map: map,
                title: "Cliente",
                icon: '../images/marker_passenger.png'
            });

            document.getElementById("MainContent_MainContent_hiddenLatitude").value = position.coords.latitude;
            document.getElementById("MainContent_MainContent_hiddenLongitude").value = position.coords.longitude;
        }

        function vwPosicaoMapaErro(msg) {
            alert('Navegador não suporta geolocalização!');
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

            var mapcanvas = document.createElement('div');
            mapcanvas.id = 'mapcanvas';
            mapcanvas.style.height = '600px';
            mapcanvas.style.width = '90%';
            document.querySelector('#mapAguardandoAtendimento').appendChild(mapcanvas);
            var latlngTaxi = new google.maps.LatLng(latTaxi, lgnTaxi);
            var latlngCli = new google.maps.LatLng(latCli, lgnCli);

            var myOptions = {
                zoom: 13,
                center: latlngTaxi,
                mapTypeControl: false,
                navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL },
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("mapcanvas"), myOptions);
            var markerTaxi = new google.maps.Marker({
                position: latlngTaxi,
                map: map,
                title: "Taxista",
                icon: '../images/marker_taxi.png'
            });

            var markerCli = new google.maps.Marker({
                position: latlngCli,
                map: map,
                title: "Cliente",
                icon: '../images/marker_passenger.png'
            });

            //directions

            var directionsDisplay = new google.maps.DirectionsRenderer();
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

            var trafficLayer = new google.maps.TrafficLayer();
            trafficLayer.setMap(map);

            var transitLayer = new google.maps.TransitLayer();
            transitLayer.setMap(map);
        }
    </script>

    <asp:HiddenField ID="hiddenLatitude" runat="server" />
    <asp:HiddenField ID="hiddenLongitude" runat="server" />

    <asp:UpdatePanel ID="updPosicao" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:MultiView ID="multiview" runat="server">
                <asp:View ID="viewPosicao" runat="server">
                    <center>
                        <h2>Minha Posição Atual</h2>
                    
                        <div id="map"></div>
                        <div id="divCarregando" runat="server" visible="true">
                            <asp:Image ID="imgCarregando" runat="server" ImageUrl="~/images/carregando.gif" />
                        </div>
                        <div id="divPosicao" runat="server" visible="false">
                            <table class="requisicao-table">
                                <tr>
                                    <td>Endereço:</td>
                                </tr>
                                <tr>
                                     <td>
                                        <asp:TextBox ID="txtEndereco" runat="server" onfocus="this.select()" Width="100%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEndereco" runat="server" ControlToValidate="txtEndereco"
                                            Display="Dynamic" Text="*" ToolTip="Endereço é campo obrigatório!"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnSolicitar" runat="server" Text="Solicitar Taxi" CssClass="bigbutton" />
                        </div>
                    </center>
                    <asp:Timer ID="timerPosition" runat="server" Interval="3000"></asp:Timer>  
                </asp:View>
                <asp:View ID="viewAguardeProcessamento" runat="server">
                    <h2>Processamento de Requisição</h2>
                    Aguarde processamento..
                    <br />Procurando taxi mais próximo...
                    <br />
                    <asp:Image ID="imgAguardeProcessamento" runat="server" ImageUrl="~/images/carregando.gif" />
                    <br />
                    <br />
                    <asp:Button id="btnCancelarAG" runat="server" Text="Cancelar" CssClass="bigbutton" />
                    <asp:Timer ID="timerAguardeProcessamento" runat="server" Interval="2000"></asp:Timer>
                </asp:View>
                <asp:View ID="viewConfirmacao" runat="server">
                    <h2>Processamento de Requisição</h2>
                    Taxista confirmou requisição. Confirma solicitação?
                    <br />
                    <asp:Button ID="btnConfirmarSolicitacao" runat="server" Text="Confirmar" CssClass="bigbutton" />
                    <asp:Button ID="btnCancelarSolicitacao" runat="server" Text="Cancelar" CssClass="bigbutton" />

                    <asp:Timer ID="timerConfirmacao" runat="server" Interval="10000"></asp:Timer>
                </asp:View>
                <asp:View ID="viewAguardeAtendimento" runat="server">
                    <center>
                        <h2>Aguardando Atendimento</h2>
                        <div class="requisicao-text">
                            Aguarde atendimento..
                            <br />
                            <br />- Previsão: <asp:Label ID="lblPrevisao" runat="server"></asp:Label>
                            <br />- Placa: <asp:Label ID="lblPlaca" runat="server"></asp:Label>
                            <br />- Responsável: <asp:Label ID="lblResponsavel" runat="server"></asp:Label>
                            <br /><br />
                        </div>

                        <div id="mapAguardandoAtendimento"></div>
                    </center>

                    <asp:Timer ID="timerAguardeAtendimento" runat="server" Interval="10000"></asp:Timer>
                </asp:View>
                <asp:View ID="viewEmAtendimento" runat="server">
                    <h2>Atendimento de Solicitação</h2>
                    Em atendimento..
                    <br />
                    <asp:Timer ID="timerEmAtendimento" runat="server" Interval="15000"></asp:Timer>
                </asp:View>
                <asp:View ID="viewTaxiIndisponivel" runat="server">
                    <h2>Processamento de Requisição</h2>
                    Nenhum taxi disponível no momento. 
                    <br />Você está em nossa fila de espera.
                    <br />Aguardando taxis disponíveis..
                    <br />
                    <asp:Image ID="imgAguardeTaxiIndisponivel" runat="server" ImageUrl="~/images/carregando.gif" />
                    <br /><br />
                    <asp:Button ID="btnCancelarTaxiIndisponivel" runat="server" Text="Cancelar" CssClass="bigbutton" />
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

