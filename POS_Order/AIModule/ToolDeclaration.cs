using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.AIModule
{
    public abstract class ToolDeclaration
    {
        public abstract string name { get; }
        public abstract string description { get; }
        public abstract Parameters parameters { get; }
    }
}
