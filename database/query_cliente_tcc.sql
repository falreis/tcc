DO $$
DECLARE var_pes_id varchar;
DECLARE var_usr_id varchar;


DECLARE var_index integer;
DECLARE var_login varchar;
DECLARE var_nome varchar;
DECLARE var_cpf varchar;

declare var_uuid_pes_id uuid;
declare var_uuid_usr_id uuid;
BEGIN
  var_index := 40;

  LOOP
    var_index := var_index + 1;
    
    var_pes_id := newid();
    var_usr_id := newid();

    var_login := 'cliente' || var_index || '@falreis.eng.br';
    var_nome := 'Taxista' || var_index;
    var_cpf := 'CPFC' || var_index;

    var_uuid_pes_id := CAST(var_pes_id AS uuid);
    var_uuid_usr_id := CAST(var_usr_id AS uuid);

    /* insere clientes no banco */
    
    insert into pessoa(pes_id, tpe_id, end_id, pes_ativo)
    values(var_uuid_pes_id, 1, '85b5bb62-e113-478f-a8e7-951165aa2008', true);

    insert into pessoa_fisica(pfi_id, pfi_nome, pfi_apelido, pfi_cpf, pfi_rg, sex_id, pfi_data_nascimento, eci_id, pfi_profissao)
    values(var_uuid_pes_id, var_nome, var_nome, var_cpf, var_cpf, 1, '1988-04-12 00:00:00', 3, 'Desenvolvedor');

    insert into usuario(usr_id, tus_id, pes_id, usr_login, usr_senha, usr_email, usr_ativo)
    values(var_uuid_usr_id, 2, var_uuid_pes_id, var_login, 'E10ADC3949BA59ABBE56E057F20F883E', var_login, true);

    insert into cliente(cli_id, stc_id)
    values (var_uuid_usr_id, 1);
    
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

