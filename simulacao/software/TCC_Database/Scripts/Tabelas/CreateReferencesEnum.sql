-- =============================================
-- Script Template
-- =============================================

/*************************************************************************/
/***** Tabela CLIENTE *****/

alter table CLIENTE add constraint FK_CLIENTE_STATUSCLIENTE foreign key (STC_ID) references STATUS_CLIENTE(STC_ID);

/*************************************************************************/
/***** Tabela ENDERECO *****/

alter table ENDERECO add constraint FK_ENDERECO_UF foreign key (UF_ID) references UF(UF_ID);

/*************************************************************************/
/***** Tabela PESSOA *****/

alter table PESSOA add constraint FK_PESSOA_TIPOPESSSOA foreign key (TPE_ID) references TIPO_PESSOA(TPE_ID);

/*************************************************************************/
/***** Tabela PESSOA FISICA *****/

alter table PESSOA_FISICA add constraint FK_PESSOAFISICA_ESTADOCIVIL foreign key (ECI_ID) references ESTADO_CIVIL(ECI_ID);
alter table PESSOA_FISICA add constraint FK_PESSOAFISICA_SEXO foreign key (SEX_ID) references SEXO(SEX_ID);

/*************************************************************************/
/***** Tabela PESSOA JURIDICA *****/

alter table PESSOA_JURIDICA add constraint FK_PESSOAJURIDICA_RAMOATIVIDADE foreign key (RAT_ID) references RAMO_ATIVIDADE(RAT_ID);
alter table PESSOA_JURIDICA add constraint FK_PESSOAJURIDICA_SEXO foreign key (SEX_ID_RESPONSAVEL) references SEXO(SEX_ID);

/*************************************************************************/
/***** Tabela REQUISICAO *****/

alter table REQUISICAO add constraint FK_REQUISICAO_STATUSREQUISICAO foreign key (STR_ID) references STATUS_REQUISICAO(STR_ID);

/*************************************************************************/
/***** Tabela TAXISTA *****/

alter table TAXISTA add constraint FK_TAXISTA_STATUSTAXISTA foreign key (STT_ID) references STATUS_TAXISTA(STT_ID);

/*************************************************************************/
/***** Tabela USUARIO *****/

alter table USUARIO add constraint FK_USUARIO_TIPOUSUARIO foreign key (TUS_ID) references TIPO_USUARIO(TUS_ID);

/*************************************************************************/