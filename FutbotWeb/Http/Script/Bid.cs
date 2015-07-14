using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FutbotWeb.Http.Script
{
    class Bid : Super
    {
        public Bid(Context context, long trade_id, int amount)
            : base("Bid", context)
        {
            this.amount_ = amount;
            this.trade_id_ = trade_id;
        }


        string parse_bid_json()
        {
            var bid = new FutbotWeb.Json.BidInfo.RootObject();
            bid.bid = this.amount_;

            return this.EncodeJson(bid);
        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(string.Format(Constants.bid, this._context.Fifa.utas_client_url, this.trade_id_));

            this.InitPost(ref web_request);
            this.InitXmlHeader(ref web_request, this._context.Fifa.utas_url, Constants.XMethod.PUT);
            this.InitNucHeader(ref web_request);
            this.InitSessionId(ref web_request);
            this.InitPhishingToken(ref web_request);

            web_request.ContentType = Constants.json_content;
            this.SetData(this.parse_bid_json());
        }
        public override void Handle(HttpWebResponse web_response)
        {
            string json = this.ReadResponse(ref web_response);

            var search_info = this.ParseJson<FutbotWeb.Json.SearchInfo.RootObject<FutbotWeb.Json.ItemData.PlayerItemData>>(json);

            if (search_info != null)
            {
                if (search_info.currencies != null)
                    this._context.Fifa.last_info = search_info;
                else
                    throw new RequestException<Bid>("Unable to parse search info");
            }
            else
                throw new RequestException<Bid>("Unable to parse search info");
        }

        long trade_id_;
        int amount_;
    }
}
