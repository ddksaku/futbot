using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace FutbotWeb.Http.Script
{
    class AccountInfo : Super
    {
        public AccountInfo(Context context,string route)
            : base("AccountInfo", context)
        {
            this.route_ = route;
        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(string.Format(Constants.account_info,this.GetUnixTime()));

            this.InitGet(ref web_request);
            this.InitXmlHeader(ref web_request, this.route_);
            this.InitNucHeader(ref web_request);
        }
        public override void Handle(HttpWebResponse web_response)
        {
            string json = this.ReadResponse(ref web_response);

            var account_info = this.ParseJson<FutbotWeb.Json.AccountInfo.RootObject>(json);

            if (account_info != null)
            {
                if (!this.route_.Contains("s2") && this._context.Authentication.Platform == "xbox")
                {
                    this._context.Fifa.persona_name = account_info.userAccountInfo.personas[0].personaName;
                    this._context.Fifa.persona_id = account_info.userAccountInfo.personas[0].personaId;
                    this._context.Fifa.plattform = account_info.userAccountInfo.personas[0].userClubList[0].platform;
                }
                else if (this.route_.Contains("s2") && this._context.Authentication.Platform == "ps3")
                {
                    this._context.Fifa.persona_name = account_info.userAccountInfo.personas[0].personaName;
                    this._context.Fifa.persona_id = account_info.userAccountInfo.personas[0].personaId;
                    this._context.Fifa.plattform = account_info.userAccountInfo.personas[0].userClubList[0].platform;
                }
            }
            else
                throw new RequestException<AccountInfo>("Unable to find Location Header");
        }

        string route_;
    }
}
