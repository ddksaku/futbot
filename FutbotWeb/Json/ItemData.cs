using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Json
{
    public class ItemData
    {
        public class AttributeList
        {
            public int value { get; set; }
            public int index { get; set; }
        }

        public class StatsList
        {
            public int value { get; set; }
            public int index { get; set; }
        }

        public class LifetimeStat
        {
            public int value { get; set; }
            public int index { get; set; }
        }

        public class PlayerItemData
        {
            public long id { get; set; }
            public int timestamp { get; set; }
            public string itemType { get; set; }
            public string itemState { get; set; }
            public string formation { get; set; }
            public string preferredPosition { get; set; }
            public int rating { get; set; }
            public bool untradeable { get; set; }
            public string injuryType { get; set; }
            public int injuryGames { get; set; }
            public int suspension { get; set; }
            public int morale { get; set; }
            public int fitness { get; set; }
            public int assists { get; set; }
            public int resourceId { get; set; }
            public int discardValue { get; set; }
            public int lastSalePrice { get; set; }
            public int owners { get; set; }
            public int teamid { get; set; }
            public int training { get; set; }
            public int cardsubtypeid { get; set; }
            public int assetId { get; set; }
            public List<AttributeList> attributeList { get; set; }
            public List<StatsList> statsList { get; set; }
            public List<LifetimeStat> lifetimeStats { get; set; }
            public int contract { get; set; }
            public int rareflag { get; set; }
            public int playStyle { get; set; }
            public int lifetimeAssists { get; set; }
            public int loyaltyBonus { get; set; }
        }
    }
}
