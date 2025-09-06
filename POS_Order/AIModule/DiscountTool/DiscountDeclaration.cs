using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.AIModule.DiscountTool
{
    internal class DiscountDeclaration : ToolDeclaration
    {
        public override string name => "AIModule.DiscountTool.UseDiscountTool";

        public override string description => "這是一個計算折扣的函數，請根據菜單和折扣方案還有使用者所點的餐點，傳入最優惠的折扣方案還有理由";

        public override Parameters parameters => new DiscountParameter();
    }
}
