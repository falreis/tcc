DO $$
DECLARE var_pes_id varchar;
DECLARE var_usr_id varchar;
DECLARE var_ras_id varchar;
DECLARE var_vei_id varchar;

declare var_uuid_pes_id uuid;
declare var_uuid_usr_id uuid;
declare var_uuid_ras_id uuid;
declare var_uuid_vei_id uuid;

DECLARE var_index integer;
DECLARE var_login varchar;
DECLARE var_nome varchar;
DECLARE var_cpf varchar;
DECLARE var_placa varchar;
BEGIN
  var_index := 20;

  LOOP
    var_index := var_index + 1;
    
    var_pes_id := newid();
    var_usr_id := newid();
    
    raise info'%', pg_sleep(0.01);  --modifica o número para geração de ID
    var_ras_id := newid();
    var_vei_id := newid();

    var_login := 'taxista' || var_index || '@falreis.eng.br';
    var_nome := 'Taxista' || var_index;
    var_cpf := 'CPFT' || var_index;
    var_placa := 'TAX00' || var_index;

    var_uuid_pes_id := CAST(var_pes_id AS uuid);
    var_uuid_usr_id := CAST(var_usr_id AS uuid);
    var_uuid_ras_id := CAST(var_ras_id AS uuid);
    var_uuid_vei_id := CAST(var_vei_id AS uuid);

    /* insere clientes no banco */
    
    insert into pessoa(pes_id, tpe_id, end_id, pes_ativo)
    values(var_uuid_pes_id, 1, '85b5bb62-e113-478f-a8e7-951165aa2008', true);

    insert into pessoa_fisica(pfi_id, pfi_nome, pfi_apelido, pfi_cpf, pfi_rg, sex_id, pfi_data_nascimento, eci_id, pfi_profissao)
    values(var_uuid_pes_id, var_nome, var_nome, var_cpf, var_cpf, 1, '1988-04-12 00:00:00', 3, 'Taxista');

    insert into usuario(usr_id, tus_id, pes_id, usr_login, usr_senha, usr_email, usr_ativo)
    values(var_uuid_usr_id, 1, var_uuid_pes_id, var_login, 'E10ADC3949BA59ABBE56E057F20F883E', var_login, true);

    insert into cliente(cli_id, stc_id)
    values (var_uuid_usr_id, 1);

    insert into rastreador(ras_id, ras_codigo_seguranca, ras_ativo)
    values (var_uuid_ras_id, var_uuid_ras_id, true);

    insert into veiculo(vei_id, mdv_id, vei_ano_fabricacao, vei_ano_modelo, vei_cor_predominante, vei_placa, vei_renavam, pes_id, vei_autorizacao_trafego, ras_id, vei_ativo)
    values (var_uuid_vei_id, 109, 2005, 2005, 'Prata', var_placa, '1234567890', var_uuid_pes_id, 'AT001.0001', var_uuid_ras_id, true);

    insert into taxista(tax_id, tax_cnh, tax_numero_licenca, vei_id, stt_id)
    values (var_uuid_usr_id, '1287329', '238470932', var_uuid_vei_id, 1);
    
    /* fim da inserção de dados */

    raise info'%', var_pes_id;
    raise info'%', var_usr_id;
    raise info'%', var_login;

    IF var_index >= 99 THEN
        EXIT;  -- exit loop
    END IF;

    raise info'%', pg_sleep(0.01);
  END LOOP;
END $$

