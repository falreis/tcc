using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Resources;
using System.Globalization;
using System.Collections;
using TCC_Domain.Domain;
using TCC_Infrastructure.Attributes;

namespace TCC_Web
{
    public class Base
    {
        #region Propriedades

        public static bool UsuarioPossuiPermissaoAcesso
        {
            get { return VerificaPermissaoAcesso(); }
        }

        public static Usuario SessaoUsuario
        {
            get { return (Usuario) HttpContext.Current.Session[Constantes.CONSTANTE_SESSAO_USUARIO]; }
        }

        #endregion

        #region Métodos Privados

        private static bool VerificaPermissaoAcesso()
        {
            if (HttpContext.Current.Application[Constantes.CONSTANTE_APLICACAO_PAGINAS] != null)
            {
                List<KVAttribute> paginas = (List<KVAttribute>)HttpContext.Current.Application[Constantes.CONSTANTE_APLICACAO_PAGINAS];
                KVAttribute pagina = paginas.FirstOrDefault(
                    p => p.Key == HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath && 
                        (p.Value == "0" || SessaoUsuario != null && 
                            (p.Value ==  "1" && SessaoUsuario.TipoUsuario == TipoUsuario.Taxista || p.Value ==  "2" && SessaoUsuario.TipoUsuario == TipoUsuario.Cliente)
                        )
                    );

                return (pagina != null);
            }
            return false;
        }

        #endregion
    }
}