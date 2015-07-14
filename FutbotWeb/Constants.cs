using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb
{
    public class Constants
    {
        public const string user_agent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.66 Safari/537.36";

        public const string login_page = "http://www.ea.com/de/fussball/fifa-ultimate-team";
        public const string set_propertys = "http://www.easports.com/iframe/fut/?locale={0}&baseShowoffUrl={1}/show-off&guest_app_uri={1}";
        public const string shared_info = "http://www.easports.com/iframe/fut/p/ut/shards?_={0}";
        public const string account_info = "http://www.easports.com/iframe/fut/p/ut/game/fifa14/user/accountinfo?_={0}";
        public const string authenticate = "http://www.easports.com/iframe/fut/p/ut/auth";
        public const string question = "http://www.easports.com/iframe/fut/p/ut/game/fifa14/phishing/question?_={0}";
        public const string validate = "http://www.easports.com/iframe/fut/p/ut/game/fifa14/phishing/validate";
        public const string noop = "http://www.easports.com/iframe/fut/noop?_={0}";

        public const string transfermarket = "{0}/ut/game/fifa14/transfermarket{1}";
        public const string credits = "{0}/ut/game/fifa14/user/credits";
        public const string bid = "{0}/ut/game/fifa14/trade/{1}/bid";//{bid:200} xheader : put
        public const string trade = "{0}/ut/game/fifa14/trade{1}";//trade ids divided by %, example: ?tradeIds=1%2
        public const string item = "{0}/ut/game/fifa14/item";

        public const string items = "http://cdn.content.easports.com/fifa/fltOnlineAssets/C74DDF38-0B11-49b0-B199-2E2A11D1CC13/2014/fut/items/web/{0}.json";

        public const string basic_route1 = "https://utas.fut.ea.com";
        public const string basic_route2 = "https://utas.s2.fut.ea.com";

        public const string login_parameter = "email={0}&password={1}&_rememberMe=on&rememberMe=on&_eventId=submit&facebookAuth=";

        public const string locale = "en_GB", auth_locale = "en-GB";

        public const string json_content = "application/json";
        public const string www_encoded_content = "application/x-www-form-urlencoded";

        public const int timeout = 15000;//50 Seconds
        public const bool use_decoding = true;

        public enum Method
        {
            GET, POST
        }

        public enum XMethod
        {
            INVALID, GET, PUT, DELETE
        }
        public enum Search
        {
            Player
        }

        public enum ItemState
        {
            forSale, expired
        }

        public enum RareFlag
        {
            None, Shiny, Inform = 3, Toty = 5, Tots, Motm = 8
        }

        public enum CardLevel
        {
            INVALID, gold, silver, bronze
        }

        public enum Pile
        {
            trade
        }
    }
}
