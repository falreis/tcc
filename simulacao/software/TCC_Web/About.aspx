<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Master/Publico.Master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="TCC_Web.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="about">
		<p>
			<span class="about-title"><b>Objetivos</b></span>
			<span><b>Objetivo Geral</b></span>
			<span>Propor um modelo de requisição de taxis que permita a aproximação de taxistas e usuários de forma a melhorar a qualidade de atendimento e minimizar o tempo de espera, em grandes cidades.</span>

			<span><b>Objetivos Específicos</b></span>
			<span>O trabalho terá como objetivos adicionais:
				<br />a) Aumentar a taxa de ocupação dos taxis e reduzir o número de quilômetros percorridos sem passageiros;
				<br />b) Integrar diferentes serviços de requisição de taxi;
				<br />c) Realizar rastreamento dos veículos, aumentando a segurança de passageiros e motoristas;
				<br />d) Coletar informações sobre atendimento, tempo de espera, pontos com maior número de requisições, dentre outros, de modo a aumentar a eficiência desse tipo de transporte;
				<br />e) Diminuir a taxa de desistência e/ou cancelamentos em serviços agendados, devido à demora no atendimento;
				<br />f) Coletar, estatisticamente, informações de trânsito e tráfego;
			</span>
		</p>
				
		<p>
			<span class="about-title"><b>Relevância</b></span>
			<span>O trabalho é relevante, pois visa diminuir o tempo de espera por taxis, além de aumentar a taxa de ocupação dos veículos. Os usuários deverão esperar menos tempo, pois serão capazes de encontrar o taxi disponível mais próximo na vizinhança. Para os taxistas, o trabalho é relevante, pois diminui a ociosidade de seus veículos, reduzindo a quantidade de quilômetros rodados sem que haja clientes. </span>
			<span>Nas principais cidades do país há indisponibilidade de oferta de atendimento de taxis, em horários de “rush” e até mesmo em horários específicos, como as noites de sábado (OLIVEIRA, 2011), (OLIVEIRA, 2012) (TERRA S.A., 2011). Essa indisponibilidade leva as unidades controladoras de serviços de trânsito a propor regras de funcionamento aos taxis conveniados (LOPES, 2012), aumentar a frota (TERRA S.A., 2011) (OLIVEIRA, 2011), e adotar outras medidas de modo a possibilitar aumento do serviço.</span>
			<span>Entre as causas da indisponibilidade de serviços de taxi estão o trânsito nas grandes cidades e o aumento da demanda (CASTELLO BRANCO, 2012), resultado da aplicação da Lei Seca (BRASIL, 2008). Como resultado, na cidade de Belo Horizonte, houve um aumento da demanda por serviços entre 15 e 20%, causando problemas no atendimento ao público. Devido à indisponibilidade de serviços, o tempo mínimo de espera de um passageiro ao ligar em uma cooperativa em horários de pico é de 30 minutos, sendo necessários, em média 12 minutos até que um operador consiga um taxi para o cliente (OLIVEIRA, 2011).</span>
		</p>
					
		<p>
			<span class="about-title"><b>Resultados Esperados</b></span>
			<span>Espera-se ao final do trabalho um dispositivo do tipo rastreador via sinais 3G WCDMA capaz de se comunicar com um servidor de controle, fornecendo informações em tempo real sobre a localização de um veículo ou uma frota de veículos. Espera-se que o rastreador possa fornecer dados detalhados do equipamento monitorado sem que haja atraso superior a 10 segundos entre a posição enviada pelo recurso e o processamento da central veicular. </span>
			<span>Espera-se que um software do tipo TCC_Domain seja construído a fim de monitorar a localização de taxistas e processar as requisições de atendimento a serviços, enviando o recurso mais próximo à requisição de modo com que ela possa ser atendida no menor tempo possível, com melhor previsão de tempo para atendimento e qualidade. </span>
			<span>Espera-se ao final do trabalho um software para dispositivos móveis que se comunique com a central de processamento e permita a obtenção de localização de taxis, podendo realizar uma chamada para atendimento. </span>
			<span>Espera-se também que o rastreador veicular seja acoplado a outro dispositivo que permita ao taxista aceitar ou não uma requisição de serviço, dependendo de sua disponibilidade, além de informar sobre a localização do passageiro, o trajeto até ele e o tempo estimado para o atendimento. </span>
			<span>
				Espera-se ao final do trabalho que os resultados obtidos através do sistema a ser desenvolvido sejam superiores em relação aos existentes no mercado, em relação aos clientes, nos seguintes critérios:
				<br />a) Tempo de atendimento;
				<br />b) Previsibilidade de atendimento, com estimativas mais próximas à realidade sobre o tempo até o atendimento;
				<br />c) Maior disponibilidade de serviços, em diferentes dias e horários;
				<br />d) Maior abrangência no atendimento;
				<br />e) Menor tempo de resposta a requisições;
			</span>
			<span>
				Por fim, esperam-se, em relação aos taxistas, resultados superiores aos existentes no mercado nos seguintes critérios:
				<br />a) Taxa de ocupação de veículos;
				<br />b) Número de quilômetros percorridos sem passageiros;
				<br />c) Taxa de desistência e/ou cancelamentos em serviços agendados, devido à demora no atendimento;
				<br />d) Quantidade de informações sobre atendimento, tempo de espera e pontos com maior número de requisições
			</span>
		</p>
	</div>
</asp:Content>
