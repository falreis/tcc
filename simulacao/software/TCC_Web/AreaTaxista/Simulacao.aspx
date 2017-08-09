<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Taxista.master" AutoEventWireup="true" CodeBehind="Simulacao.aspx.cs" Inherits="TCC_Web.AreaTaxista.Simulacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script src="http://maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script>

<script type="text/javascript">
    var map, directions;
    var markers = [];

    var Status =
    {
        TaxiLivre : 0,
        TaxiOcupado : 1,
        TaxiParado : 2,
        PassageiroLivre : 3
    }

    google.maps.event.addDomListener(window, 'load', initialize);

    function initialize() {
        var latlng = new google.maps.LatLng(-19.904356, -43.925691);
        var myOptions = {
            zoom: 13,
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
        directions = new google.maps.DirectionsRenderer();
        directions.setMap(map);

        google.maps.event.addListener(map, 'click', function (event) { placeMarker(event.latLng, Status.TaxiLivre); });

        //setInterval("Atualizar()", 1000);
    }

    function placeMarker(location, status) {
        var myicon = '../images/marker_green.png'
        switch (Number(status)) {
            case Status.TaxiLivre:
                myicon = '../images/marker_green.png';
                break;
            case Status.TaxiOcupado:
                myicon = '../images/marker_red.png'
                break;
            case Status.TaxiParado:
                myicon = '../images/marker_yellow.png'
                break;
            case Status.PassageiroLivre:
                myicon = '../images/marker_brown.png'
                break;
        }

        var marker = new google.maps.Marker({
            position: location, 
            map: map,
            title: "Placa: XXX-0000",
            icon: myicon
        });

        var marcador = new Object();
        marcador.marker = marker;
        marcador.status = status;

        markers.push(marcador);

//        var contentString = '<div id="content"><div id="siteNotice"></div>rock caves and ancient paintings. Uluru is listed as a World <br /><br /></div>';

//        var infowindow = new google.maps.InfoWindow({
//            content: contentString
//        });

//        google.maps.event.addListener(marker, 'click', function () { infowindow.open(map, marker); });
    }

    function LimparMarcadores() {
        if (markers) {
            for (i in markers) {
                markers[i].marker.setMap(null);
            }
            markers.length = 0;
        }
    }

    function Atualizar() {
        var tempMarkers = markers.slice();
        LimparMarcadores();

        if (tempMarkers) {
            for (i in tempMarkers) {
                var dirLat = (Math.random() > 0.3) ? 1 : -1;
                var dirLng = (Math.random() > 0.8) ? 1 : -1;

                tempMarkers[i].marker.position.Ya += 0.0002 * dirLat;
                tempMarkers[i].marker.position.Za += 0.0002 * dirLng;
                placeMarker(tempMarkers[i].marker.position, tempMarkers[i].status);
            }
        }
        tempMarkers.length = 0;
    }

    function AdicionarCliente() {
        placeMarker(new google.maps.LatLng(-19.904356, -43.925691), Status.PassageiroLivre);
    }

    function GerarMarcador() {
        var DISTANCIA = 40;
        var dirLat = (Math.random() > 0.5) ? 1 : -1;
        var dirLng = (Math.random() > 0.5) ? 1 : -1;
        var lat = -19.904356 + ((Math.random() / DISTANCIA) * dirLat);
        var lng = -43.925691 + ((Math.random() / DISTANCIA) * dirLng);

        placeMarker(new google.maps.LatLng(lat, lng), Status.TaxiLivre);
    }

    function calcRoute(origem, destino) {
        var request = {
            origin: origem,
            destination: destino,
            travelMode: google.maps.TravelMode.DRIVING
        };

        directionsService.route(request, function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                directions.setDirections(response);
            }
        });
    }

    function Teste() {
        // call server side method
        var obj = jQuery.parseJSON('{"name":"John"}');
        PageMethods.Teste(CallSuccess, CallFailed);
    }

    function CallSuccess(res)
    {
        var employeeList =  jQuery.parseJSON(res);
        for(i = 0;i<employeeList.Employee.length;i++)
            alert ('Name : ='+employeeList.Employee[i].Name+' Age : = '+employeeList.Employee[i].Age);
    }
    function CallFailed(res)
    {
        alert("error");
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <span class="button" onclick="GerarMarcador();">Adicionar Taxi</span>
    &nbsp;&nbsp;
    <span class="button" onclick="AdicionarCliente();">Adicionar Cliente</span>
    &nbsp;&nbsp;
    <span class="button" onclick="LimparMarcadores();">Limpar</span>
    &nbsp;&nbsp;
    <span class="button" onclick="calcRoute();">Calcular Rota</span>
    &nbsp;&nbsp;
    <span class="button" onclick="Teste();">Teste</span>
    <br /><br />
    <div id="map_canvas" style="height:400px; width:700px;"></div>
    <br />
    <br />
    <br />
    <asp:Label ID="lblDistancia" runat="server" Text="Distância Total:"></asp:Label>
    <br />
    <asp:Button ID="btnTestar" runat="server" Text="Calcular Distância" />&nbsp;
    <asp:Button id="btnCalcularDistancia" runat="server" Text="Teste" />
    <asp:Button ID="btnTestarHibernate" runat="server" Text="Testar Hibernate" />
</asp:Content>
