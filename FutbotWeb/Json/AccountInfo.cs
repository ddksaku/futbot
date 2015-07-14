using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Json
{
    public class AccountInfo
    {
        public class UserClubList
        {
            public string year { get; set; }
            public string platform { get; set; }
            public int lastAccessTime { get; set; }
            public string clubAbbr { get; set; }
            public string clubName { get; set; }
            public int teamId { get; set; }
            public int badgeId { get; set; }
            public int divisionOnline { get; set; }
            public int established { get; set; }
            public Dictionary<string, string> skuAccessList { get; set; }
        }

        public class Persona
        {
            public int personaId { get; set; }
            public string personaName { get; set; }
            public List<UserClubList> userClubList { get; set; }
            public int returningUser { get; set; }
        }

        public class UserAccountInfo
        {
            public List<Persona> personas { get; set; }
        }

        public class RootObject
        {
            public UserAccountInfo userAccountInfo { get; set; }
        }
    }
}
