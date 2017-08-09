using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCC_Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class KVAttribute : System.Attribute
    {
        #region Atributos

        public string _key;
        public string _value;

        #endregion

        #region Propriedades

        public string Key {
            get { return _key; }
            set { _value = value; }
        }

        public string Value {
            get { return _value; }
            set { _value = value; } 
        }

        #endregion

        public KVAttribute(string key, string value)
        {
            this._key = key;
            this._value = value;
        }
    }
}
