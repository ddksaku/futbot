using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb.Http.Script
{
    class DiscardItem: Super
    {
        public DiscardItem(Context context,long card_id)
            : base("DiscardItem", context)
        {
            this.card_id_ = card_id;
        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(string.Format(Constants.item,this._context.Fifa.utas_client_url) + "/" + this.card_id_.ToString());

            this.InitPost(ref web_request);
            this.InitXmlHeader(ref web_request, this._context.Fifa.utas_url,Constants.XMethod.DELETE);
            this.InitNucHeader(ref web_request);
            this.InitSessionId(ref web_request);
            this.InitPhishingToken(ref web_request);

            web_request.ContentType = Constants.json_content;
            this.SetData(" ");
        }

        public override void Handle(HttpWebResponse web_response)
        {
            string json = this.ReadResponse(ref web_response);

            var discard_info = this.ParseJson<FutbotWeb.Json.DiscardInfo.RootObject>(json);

            if (discard_info != null)
            {
                if (discard_info.items != null)
                    this._context.Fifa.last_info = discard_info;
                else
                    throw new RequestException<DiscardItem>("Unable to parse discard info");
            }
            else
                throw new RequestException<DiscardItem>("Unable to parse discard info");
        }

        long card_id_;
    }
}
