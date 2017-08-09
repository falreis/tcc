-- =============================================
-- Script Template
-- =============================================

create table UF(
	UF_ID int not null primary key,
	UF_NOME varchar(50) not null
);

create table ESTADO_CIVIL(
	ECI_ID int not null primary key,
	ECI_NOME varchar(50) not null
);

create table RAMO_ATIVIDADE(
	RAT_ID int not null primary key,
	RAT_NOME varchar(50) not null
);

create table SEXO(
	SEX_ID int not null primary key,
	SEX_NOME varchar(50) not null
);

create table STATUS_CLIENTE(
	STC_ID int not null primary key,
	STC_NOME varchar(50) not null
);

create table STATUS_REQUISICAO(
	STR_ID int not null primary key,
	STR_NOME varchar(50) not null
);

create table STATUS_TAXISTA(
	STT_ID int not null primary key,
	STT_NOME varchar(50) not null
);

create table TIPO_PESSOA(
	TPE_ID int not null primary key,
	TPE_NOME varchar(50) not null
);

create table TIPO_USUARIO(
	TUS_ID int not null primary key,
	TUS_NOME varchar(50) not null
);

