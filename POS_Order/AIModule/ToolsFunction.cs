using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.AIModule
{
    public abstract class ToolsFunction
    {
        protected AIResponse.Args args;

        public ToolsFunction(AIResponse.Args args)
        {
            this.args = args;
        }
        public abstract AIResponse.Args UseTools();
    }
}
