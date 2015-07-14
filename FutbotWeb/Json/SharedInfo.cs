using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Json
{
    public class SharedInfo
    {
        public class ShardInfo
        {
            public string shardId { get; set; }
            public string clientFacingIpPort { get; set; }
            public string clientProtocol { get; set; }
            public List<string> content { get; set; }
            public List<string> platforms { get; set; }
            public List<string> customdata1 { get; set; }
        }

        public class RootObject
        {
            public List<ShardInfo> shardInfo { get; set; }
        }

        public static ShardInfo get_shard(RootObject root, string str)
        {
            if (root == null || root.shardInfo == null)
                return null;

            foreach (ShardInfo info in root.shardInfo)
                if (info.platforms != null && info.platforms.Contains(str))
                    return info;

            return null;
        }
    }
}
