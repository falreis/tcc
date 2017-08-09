using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCC_Web.App_LocalResources;
using System.Web.UI.WebControls;

namespace TCC_Web
{
    public static class Extensions
    {
        #region WebControls

        public static void AdicionarItemDefault(this DropDownList dddl)
        {
            dddl.Items.Insert(0, new ListItem(LabelsResource.SELECIONE, String.Empty));
        }

        #endregion
    }
}