using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FutbotWeb.Http.Script
{
    class AuctionHouseSearch : Super
    {
        public AuctionHouseSearch(Context context, Constants.Search search)
            : base("AuctionHouseSearch", context)
        {
            this.search_ = search;
            this.args_ = new List<string>();

            this.args_.Add("type");
            this.args_.Add(search.ToString().ToLower());
        }

        public void set_search_context(SearchContext context)
        {
            this.args_[1] = context.Search.ToString().ToLower();

            if (context.CardLevel != Constants.CardLevel.INVALID)
                this.set_parameter("lev", context.CardLevel.ToString());

            if (context.Bid.Min != 0)
                this.set_parameter("micr", context.Bid.Min.ToString());
            if (context.Bid.Max != 0)
                this.set_parameter("macr", context.Bid.Max.ToString());

            if (context.BuyNow.Min != 0)
                this.set_parameter("minb", context.BuyNow.Min.ToString());
            if (context.BuyNow.Max != 0)
                this.set_parameter("maxb", context.BuyNow.Max.ToString());
        }

        public void set_parameter(params string[] args)
        {
            foreach (string str in args)
                this.args_.Add(str);
        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(string.Format(Constants.transfermarket, this._context.Fifa.utas_client_url, this.EncodePost(this.args_.ToArray())));

            this.InitPost(ref web_request);
            this.InitXmlHeader(ref web_request, this._context.Fifa.utas_url, Constants.XMethod.GET);
            this.InitNucHeader(ref web_request);
            this.InitSessionId(ref web_request);
            this.InitPhishingToken(ref web_request);

            web_request.ContentType = Constants.json_content;
            this.SetData(" ");
        }
        public override void Handle(HttpWebResponse web_response)
        {
            string json = this.ReadResponse(ref web_response);

            var auction_info = this.ParseJson<FutbotWeb.Json.SearchInfo.RootObject<FutbotWeb.Json.ItemData.PlayerItemData>>(json);

            if (auction_info != null)
            {
                if (auction_info.auctionInfo != null)
                    this._context.Fifa.last_info = auction_info;
                else
                    throw new RequestException<AuctionHouseSearch>("Unable to parse auction info");
            }
            else
                throw new RequestException<AuctionHouseSearch>("Unable to parse auction info");
        }

        Constants.Search search_;
        List<string> args_;
    }
}
