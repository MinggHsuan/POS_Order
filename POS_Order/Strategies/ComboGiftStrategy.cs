using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS_Order.MenuModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace POS_Order.Strategies
{
    public class FindSameItem
    {
        public static List<Conditionbox> FindCommon(List<Item> items, List<Conditionbox> condition)
        {
            //List<Conditionbox> box = new List<Conditionbox>();
            //for (int i = 0; i < items.Count; i++)
            //{
            //    for (int j = 0; j < condition.Count; j++)
            //    {
            //        if (items[i].name == condition[j].name)
            //        {
            //            box.Add(new Conditionbox(items[i].name,  items[i].amount,condition[j].conditionID));
            //            continue;
            //        }
            //    }
            //}
            //return box;
            var temp = items.Join(condition,
                item => item.name,
                cond => cond.name,
                (item, cond) => new Conditionbox(item.name, item.amount, cond.conditionAmount, cond.conditionID))
                .ToList();
            return temp;

        }
    }
    public class Conditionbox
    {
        public string name; //購買的品項
        public int subtotal;
        public int price;
        public int amount; // 購買數量
        public int conditionAmount; //第幾組的數量條件限制
        public int conditionID; // 第幾組的condition
        public Conditionbox(string name, int price)
        {
            this.name = name;
            this.price = price;
        }
        //public Conditionbox(string name, int price, int conditionAmount)
        //{
        //    this.name = name;
        //    this.price = price;
        //    this.conditionAmount = conditionAmount;
        //    this.subtotal = price * conditionAmount;

        //}
        public Conditionbox(string name, int conditionAmount, int conditionID)
        {
            this.name = name;
            this.conditionAmount = conditionAmount;
            this.conditionID = conditionID;
        }
        public Conditionbox(string name, int amount, int conditionAmount, int conditionID)
        {
            this.name = name;
            this.amount = amount;
            this.conditionAmount = conditionAmount;
            this.conditionID = conditionID;
        }

        //public Conditionbox(string name, string rewardType, int price, int amount, int conditionAmount, int conditionID)
        //{
        //    this.name = name;
        //    this.rewardType = rewardType;
        //    this.price = price;
        //    this.amount = amount;
        //    this.conditionAmount = conditionAmount;
        //    this.conditionID = conditionID;
        //}

    }
    public class ComboGiftStrategy : ADiscountStrategy
    {

        public ComboGiftStrategy(MenuModel.Discount discountType, List<Item> items) : base(discountType, items)
        {
        }

        public override void Discount()
        {
            //Dictionary<int, List<Conditionbox>> classify = new Dictionary<int, List<Conditionbox>>();
            //List<int> minComboAmount = new List<int>();
            //for (int i = 0; i < discountType.Conditions.Length; i++)
            //{
            //    string[] nameType = discountType.Conditions[i].Name.Split('|');
            //    for (int j = 0; j < nameType.Length; j++)
            //    {
            //        condition.Add(new Conditionbox(nameType[j], "", 0, discountType.Conditions[i].RequirAmount, i));
            //    }
            //}
            List<Conditionbox> condition = discountType.Conditions
                .SelectMany((x, index) => x.Name.Split('|')
                .Select(name => new Conditionbox(name, x.RequirAmount, index)))
                .ToList();

            List<Conditionbox> buyItem = FindSameItem.FindCommon(items, condition);
            //for (int i = 0; i < discountType.Conditions.Length; i++)
            //{
            //    if (!buyItem.Any(x => x.conditionID == i))
            //    {
            //        continue;
            //    }
            //    classify.Add(i, buyItem.Where(x => x.conditionID == i).ToList());

            //}
            var classify = buyItem.GroupBy(x => new { x.conditionID, x.conditionAmount })
                .Select(x => new
                {
                    totalAmount = x.Sum(y => y.amount),
                    requirAmount = x.Key.conditionAmount
                })
                .Where(x => (x.totalAmount / x.requirAmount) > 0)
                .ToList();

            if (classify.Count != discountType.Conditions.Length)
            {
                return;
            }

            //for (int i = 0; i < classify.Count; i++)
            //{
            //    if (classify[i].Sum(x => x.amount) / classify[i][0].conditionAmount == 0)
            //    {
            //        return;
            //    }
            //    minComboAmount.Add(classify[i].Sum(x => x.amount) / classify[i][0].conditionAmount);
            //}

            //var ComboCount = classify
            //    .Select(x => new
            //    {
            //        ComboCount = x.Value.Sum(y => y.amount) / x.Value.First().conditionAmount
            //    }).ToList();
            int minCombo = classify.Min(x => x.totalAmount / x.requirAmount);

            List<Conditionbox> RewardsCondition = new List<Conditionbox>();
            Dictionary<int, List<Conditionbox>> RewardsClassify = new Dictionary<int, List<Conditionbox>>();

            if (discountType.Rewards.Any(x => x.RewardType == ""))
            {
                var rewardItem = discountType.Rewards
                    .Where(x => x.RewardType == "")
                    .Select(x => (new Item($"(贈送){x.Name}", 0, minCombo * x.RewardsAmount))).ToList();
                items.AddRange(rewardItem);
            }

            //for (int i = 0; i < discountType.Rewards.Length; i++)
            //{
            //    if (discountType.Rewards[i].RewardType == "")
            //    {
            //        items.Add(new Item($"(贈送){discountType.Rewards[i].Name}", 0, minComboAmount.Min() * discountType.Rewards[i].RewardsAmount));
            //        continue;
            //    }
            //    string[] nameType = discountType.Rewards[i].Name.Split('|');
            //    for (int j = 0; j < nameType.Length; j++)
            //    {
            //        if (buyItem.Any(x => x.name == nameType[j]))
            //        {
            //            RewardsCondition.Add(new Conditionbox(nameType[j], discountType.Rewards[i].RewardType, buyItem[i].price, 0, discountType.Rewards[i].RewardsAmount, i));
            //        }
            //    }
            //}

            RewardsCondition = discountType.Rewards
                .SelectMany((x, index) => x.Name.Split('|')
                .Select(name => new Conditionbox(name, x.RewardsAmount, index)))
                .ToList();
            //for (int i = 0; i < RewardsCondition.Count; i++)
            //{
            //    if (!RewardsClassify.ContainsKey(RewardsCondition[i].conditionID))
            //    {
            //        RewardsClassify.Add(RewardsCondition[i].conditionID, new List<Conditionbox>() { RewardsCondition[i] });

            //    }
            //    else
            //    {
            //        RewardsClassify[RewardsCondition[i].conditionID].Add(RewardsCondition[i]);
            //    }

            //}
            RewardsClassify = RewardsCondition.GroupBy(x => x.conditionID)
                .ToDictionary(x => x.Key, x => x.ToList());

            //foreach (var reward in RewardsClassify)
            //{
            //    string type = reward.Value[0].rewardType;
            //    if (type == "Min")
            //    {
            //        int minPrice = reward.Value.Min(x => x.price);
            //        Conditionbox conditionbox = reward.Value.Where(x => x.price == minPrice).OrderBy(x => new Random(Guid.NewGuid().GetHashCode()).Next()).First();

            //        items.Add(new Item($"(贈送){conditionbox.name}", minPrice, minComboAmount.Min() * reward.Value[0].conditionAmount));
            //    }
            //}
            //var temp = items.Join(condition,
            //   item => item.name,
            //   cond => cond.name,
            //   (item, cond) => new Conditionbox(item.name, item.amount, cond.conditionAmount, cond.conditionID))
            //   .ToList();

            var rewards = RewardsClassify.SelectMany(x => x.Value)
                .Join(items,
                reward => reward.name,
                item => item.name,
                (reward, item) => new Conditionbox(item.name, item.price)
                ).ToList();

            items.AddRange(discountType.Rewards.Select(x =>
            {
                Conditionbox gifts = null;
                if (x.RewardType == "Min")
                {
                    int minprice = rewards.Min(y => y.price);
                    gifts = rewards.Where(y => y.price == minprice)
                        .First();
                }
                if (x.RewardType == "Max")
                {
                    int maxprice = rewards.Max(y => y.price);
                    gifts = rewards.Where(y => y.price == maxprice)
                        .First();
                }
                if (x.RewardType == "Random")
                {
                    gifts = rewards.OrderBy(y => new Random(Guid.NewGuid().GetHashCode()).Next()).First();
                }
                return new Item($"(贈送){gifts.name}", 0, minCombo * x.RewardsAmount);
            }));


            //if (discountType.Rewards.Any(x => x.RewardType == "Min"))
            //{
            //    int minprice = rewards.Min(x => x.price);
            //    var gifts = rewards.Where(x => x.price == minprice)
            //        .First();

            //    var rewardItem = discountType.Rewards
            //        .Where(x => x.RewardType == "Min")
            //        .Select(x => (new Item($"(贈送){gifts.name}", 0, ComboCount.Min(y => y.ComboCount) * x.RewardsAmount))).ToList();
            //    items.AddRange(rewardItem);
            //}
            //if (discountType.Rewards.Any(x => x.RewardType == "Max"))
            //{
            //    int maxprice = rewards.Max(x => x.price);
            //    var gifts = rewards.Where(x => x.price == maxprice)
            //        .First();

            //    var rewardItem = discountType.Rewards
            //        .Where(x => x.RewardType == "Max")
            //        .Select(x => (new Item($"(贈送){gifts.name}", 0, ComboCount.Min(y => y.ComboCount) * x.RewardsAmount))).ToList();
            //    items.AddRange(rewardItem);
            //}
            //if (discountType.Rewards.Any(x => x.RewardType == "Random"))
            //{
            //    var gifts = rewards.OrderBy(x => new Random(Guid.NewGuid().GetHashCode()).Next()).First();
            //    var rewardItem = discountType.Rewards
            //        .Where(x => x.RewardType == "Random")
            //        .Select(x => (new Item($"(贈送){gifts.name}", 0, ComboCount.Min(y => y.ComboCount) * x.RewardsAmount))).ToList();
            //    items.AddRange(rewardItem);
            //}






            //List<Item> uyItem = new List<Item>();
            //List<Item> buySideItem = new List<Item>();
            //List<int> minComboAmount = new List<int>();

            //for (int i = 0; i < discountType.Conditions.Length; i++)
            //{
            //    if (discountType.Conditions[i].Name.Contains('|'))
            //    {
            //        string[] nameType = discountType.Conditions[i].Name.Split('|');
            //        for (int j = 0; j < nameType.Length; j++)
            //        {
            //            Item Sideitem = items.Find(x => x.name == nameType[j]);
            //            if (Sideitem == null)
            //            {
            //                continue;
            //            }
            //            buySideItem.Add(Sideitem);
            //        }
            //        if (buySideItem.Count == 0)
            //        {
            //            return;
            //        }

            //        minComboAmount.Add((buySideItem.Sum(x => x.amount)) / discountType.Conditions[i].RequirAmount);
            //        continue;
            //    }
            //    Item Item = items.Find(x => x.name == discountType.Conditions[i].Name && x.amount >= discountType.Conditions[i].RequirAmount);
            //    if (Item == null)
            //    {
            //        return;
            //    }
            //    buyItem.Add(Item);
            //    minComboAmount.Add(Item.amount / discountType.Conditions[i].RequirAmount);
            //}



            //for (int i = 0; i < discountType.Rewards.Length; i++)
            //{
            //    items.Add(new Item($"(贈送){discountType.Rewards[i].Name}", 0, minComboAmount.Min() * discountType.Rewards[i].RewardsAmount));
            //}

        }
    }
}
