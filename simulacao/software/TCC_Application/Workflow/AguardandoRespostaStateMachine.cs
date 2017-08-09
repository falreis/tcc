using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using TCC_Application.Arguments;
using TCC_Domain.Domain;

namespace TCC_Application.Workflow
{
    public sealed class AguardandoRespostaStateMachine : BaseActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            //WorkflowArgument argument = context.GetValue(this.InArgs);

            //OFMS ofms = OFMS.GetInstance();

            //Atendimento atendimento = new Atendimento();
            //atendimento.Status = StatusAtendimento.AguardandoResposta;
        }
    }
}
