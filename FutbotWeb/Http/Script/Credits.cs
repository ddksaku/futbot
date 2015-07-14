using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FutbotWeb.Http.Script
{
    class Credits : Super
    {
        public Credits(Context context)
            : base("Credits", context)
        {
        }


        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(string.Format(Constants.credits, this._context.Fifa.utas_client_url));

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

            var credit_info = this.ParseJson<FutbotWeb.Json.CreditInfo.RootObject>(json);

            if (credit_info != null)
            {
                if (credit_info.currencies != null)
                    this._context.Fifa.last_info = credit_info;
                else
                    throw new RequestException<Credits>("Unable to parse credit info");
            }
            else
                throw new RequestException<Credits>("Unable to parse credit info");
        }
    }
}
