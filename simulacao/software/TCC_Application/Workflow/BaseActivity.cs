using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using TCC_Application.Arguments;

namespace TCC_Application.Workflow
{
    public class BaseActivity : CodeActivity
    {
        public InArgument<WorkflowArgument> InArgs { get; set; }
        public OutArgument<WorkflowArgument> OutArgs { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
        }
    }
}
