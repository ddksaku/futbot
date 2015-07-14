using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Http.Script
{
    public class MinMax<T>
    {
        public T Min { get; set; }
        public T Max { get; set; }

        public MinMax()
        {
            this.Min = default(T);
            this.Max = default(T);
        }
    }
    
    public class SearchContext
    {
        public MinMax<int> BuyNow, Bid;
        public Constants.Search Search;
        public Constants.CardLevel CardLevel;

        public SearchContext()
        {
            this.BuyNow = new MinMax<int>();
            this.Bid = new MinMax<int>();
            this.Search = Constants.Search.Player;
            this.CardLevel = Constants.CardLevel.gold;
        }

    }
}