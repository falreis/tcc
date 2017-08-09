-- Function: newid()

-- DROP FUNCTION newid();

CREATE OR REPLACE FUNCTION newid()
  RETURNS character varying AS
$BODY$ 
DECLARE 
  v_seed_value varchar(32); 
BEGIN 
select 
md5( 
inet_client_addr()::varchar || 
timeofday() || 
inet_server_addr()::varchar || 
to_hex(inet_client_port()) 
) 
into v_seed_value; 

return (substr(v_seed_value,1,8) || '-' || 
        substr(v_seed_value,9,4) || '-' || 
        substr(v_seed_value,13,4) || '-' || 
        substr(v_seed_value,17,4) || '-' || 
        substr(v_seed_value,21,12)); 
END; 
$BODY$
  LANGUAGE plpgsql VOLATILE SECURITY DEFINER
  COST 100;
ALTER FUNCTION newid()
  OWNER TO postgres;
