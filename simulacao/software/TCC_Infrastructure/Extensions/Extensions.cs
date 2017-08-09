using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TCC_Infrastructure.Attributes;
using TCC_Infrastructure.App_Resources;

namespace TCC_Infrastructure.Extensions
{
    public static class Extensions
    {
        #region Enum (KVAtributes)

        public static string ObterDescricao(this Enum enumerator)
        {
            MemberInfo memberInfo = enumerator.GetType().GetMember(enumerator.ToString()).FirstOrDefault();
            if (memberInfo != null)
            {
                KVAttribute attribute = (KVAttribute)memberInfo.GetCustomAttributes(typeof(KVAttribute), false).FirstOrDefault();
                return attribute.Value;
            }
            return String.Empty;
        }

        public static string ObterValor(this Enum enumerator)
        {
            MemberInfo memberInfo = enumerator.GetType().GetMember(enumerator.ToString()).FirstOrDefault();
            if (memberInfo != null)
            {
                KVAttribute attribute = (KVAttribute)memberInfo.GetCustomAttributes(typeof(KVAttribute), false).FirstOrDefault();
                return attribute.Key;
            }
            return String.Empty;
        }

        #endregion

        
    }
}