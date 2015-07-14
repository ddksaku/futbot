using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Json
{
    public class SearchInfo
    {
        public class AuctionInfo<T>
        {
            public long tradeId { get; set; }
            public T itemData { get; set; }
            public int buyNowPrice { get; set; }
            public int currentBid { get; set; }
            public int offers { get; set; }
            public object watched { get; set; }
            public string bidState { get; set; }
            public int startingBid { get; set; }
            public string tradeState { get; set; }
            public int expires { get; set; }
            public string sellerName { get; set; }
            public int sellerEstablished { get; set; }
            public int sellerId { get; set; }
        }

        public class BidTokens
        {
            public int count { get; set; }
            public int updateTime { get; set; }
        }

        public class Currency
        {
            public string name { get; set; }
            public int funds { get; set; }
            public int finalFunds { get; set; }
        }

        public class RootObject<T>
        {
            public List<AuctionInfo<T>> auctionInfo { get; set; }
            public int credits { get; set; }
            public BidTokens bidTokens { get; set; }
            public List<Currency> currencies { get; set; }
            public object duplicateItemIdList { get; set; }
            public object errorState { get; set; }
        }
    }
}
