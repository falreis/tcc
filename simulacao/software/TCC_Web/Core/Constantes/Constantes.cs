using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCC_Web
{
    public static class Constantes
    {
        public const string CONSTANTE_APLICACAO_PAGINAS = "CONSTANTE_APLICACAO_PAGINAS";
        public const string CONSTANTE_SESSAO_USUARIO = "CONSTANTE_SESSAO_USUARIO";

        public const string CONSTANTE_PAGINA_LOGIN = "~/Autenticacao/Login.aspx";
        public const string CONSTANTE_PAGINA_LOGIN_QUERY = "~/Autenticacao/Login.aspx?TipoUsuario={0}";
        public const string CONSTANTE_QUERY_TIPO_USUARIO = "TipoUsuario";

        public const string CONSTANTE_PAGINA_CADASTRO_QUERY = "~/Autenticacao/Cadastro.aspx?TipoUsuario={0}";

        public const string CONSTANTE_PAGINA_HOME_TAXISTA = "~/AreaTaxista/Home.aspx";
        public const string CONSTANTE_PAGINA_HOME_CLIENTE = "~/AreaCliente/Home.aspx";
        public const string CONSTANTE_PAGINA_HOME_ADMINISTRADOR = "~/AreaAdministrador/Home.aspx";
    }
}