using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCC_Application.Arguments
{
    public class WorkflowArgument
    {
        public const string CONSTANTE_WORKFLOW_IN_ARGUMENT = "InArgs";
        public const string CONSTANTE_WORKFLOW_OUT_ARGUMENT = "OutArgs";

        public Guid ClienteID { get; set; }
        public double ClienteLatitude { get; set; }
        public double ClienteLongitude { get; set; }

        public Guid AtendimentoID { get; set; }
        public Guid TaxistaID { get; set; }
        
        public WorkflowArgument()
        {
        }

        public WorkflowArgument(Guid clienteID, double clienteLatitude, double clienteLongitude, Guid atendimentoID, Guid taxistaID)
        {
            this.ClienteID = clienteID;
            this.ClienteLatitude = clienteLatitude;
            this.ClienteLongitude = clienteLongitude;

            this.AtendimentoID = atendimentoID;
            this.TaxistaID = taxistaID;
        }

        public Dictionary<string, object> GenerateDictionary()
        {
            Dictionary<string, object> dicionario = new Dictionary<string, object>();
            dicionario.Add(CONSTANTE_WORKFLOW_IN_ARGUMENT, this);

            return dicionario;
        }
    }
}
