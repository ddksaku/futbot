using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace FutbotWeb.Http.Script
{
    class SharedInfo : Super
    {
        public SharedInfo(Context context)
            : base("SharedInfo", context)
        {

        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(string.Format(Constants.shared_info,this.GetUnixTime()));

            this.InitGet(ref web_request);
            this.InitXmlHeader(ref web_request, Constants.basic_route1);
        }


        string parse_utas(string facing, string protocol)
        {
            string domain = facing;//.Substring(0, facing.IndexOf(':'));

            return string.Format("{0}://{1}", protocol, domain);
        }

        string parse_utas_client(string facing, string protocol)
        {
            string domain = facing.Substring(0, facing.IndexOf(':'));

            return string.Format("{0}://{1}", protocol, domain);
        }

        public override void Handle(HttpWebResponse web_response)
        {
            string json = this.ReadResponse(ref web_response);
            var shard_info = this.ParseJson<FutbotWeb.Json.SharedInfo.RootObject>(json);

            var info = FutbotWeb.Json.SharedInfo.get_shard(shard_info, this._context.Authentication.Platform);
         

            if (info != null)
            {
                int n = info.platforms.IndexOf(this._context.Authentication.Platform);

                this._context.Fifa.custom_data = info.customdata1[n];
                this._context.Fifa.utas_url = this.parse_utas(info.clientFacingIpPort, info.clientProtocol);
                this._context.Fifa.utas_client_url = this.parse_utas_client(info.clientFacingIpPort, info.clientProtocol);
            }
            else
                throw new RequestException<SharedInfo>("Unable to parse Shared Info or find Plattform: " + this._context.Authentication.Platform);
        }
    }
}
