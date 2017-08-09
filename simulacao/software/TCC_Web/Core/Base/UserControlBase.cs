using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCC_Web
{
    public class UserControlBase : System.Web.UI.UserControl
    {
        protected void OnInit(EventArgs e)
        {
        }

        protected void Page_Init(Object sender, EventArgs e)
        {
            OnInit(e);
        }
    }
}