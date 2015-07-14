using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using FutbotWeb.Http;
using FutbotWeb.Http.Script;
using System.Threading;
using Futbot;

namespace FutbotWeb
{
    public class FutbotScriptManager
    {
        #region properties

        protected Context _context;
        protected FutbotScript _script;

        public bool IsActive { get { return _script != null && !_script.Finished; } }

        public string Username
        {
            get { return this._context.Authentication.UserName; }
            set { this._context.Authentication.UserName = value; }
        }

        public string Password
        {
            get { return this._context.Authentication.Password; }
            set { this._context.Authentication.Password = value; }
        }

        public string SecretAnswer
        {
            get { return this._context.Authentication.SecretAnswer; }
            set { this._context.Authentication.SecretAnswer = value; }
        }

        public string Platform
        {
            get { return this._context.Authentication.Platform; }
            set { this._context.Authentication.Platform = value; }
        }

        #endregion

        #region init

        public FutbotScriptManager(FutbotScript script, AuthenticationInfo auth, Fifa fifa)
        {
            this._script = script;
            this._context = new Context(auth, fifa);
        }

        #endregion

        #region helpers

        public void ShortWait()
        {
            Thread.Sleep(400);
        }

        public void ProcessRequest(Super super)
        {
            HttpWebRequest request;

            LogManager.Debug("Preparing request " + super.RequestName);

            super.Prepare(out request);

            HttpWebResponse response;

            LogManager.Debug("Processing request " + super.RequestName);

            this.Send(request, out response, super.GetData());

            LogManager.Debug("Handling response " + super.RequestName);

            super.Handle(response);

            response.GetResponseStream().Close();

            LogManager.Debug("Finished " + super.RequestName);

            ShortWait();
        }

        public bool TryRequest(Super super)
        {
            try
            {
                this.ProcessRequest(super);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Send(HttpWebRequest request, out HttpWebResponse response, byte[] data)
        {
            if (data != null)
            {
                request.ContentLength = data.Length;
                var stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
            }

            request.Timeout = Constants.timeout;

            response = (HttpWebResponse)request.GetResponse();
        }

        bool TrySend(HttpWebRequest request, out HttpWebResponse response, byte[] data)
        {
            try
            {
                this.Send(request, out response, data);
                return true;
            }
            catch
            {
                response = null;
                return false;
            }
        }

        #endregion

        #region scripts

        public void RunScript()
        {
            _script.NextStep(this);
        }

        public void Authenticate()
        {
            this.ProcessRequest(new LoginPage(this._context));
            this.ProcessRequest(new TempLoginPage(this._context));
            this.ProcessRequest(new SignInPage(this._context));
            this.ProcessRequest(new SignIn(this._context));
            this.ProcessRequest(new Login(this._context));
            this.ProcessRequest(new LoginVerify(this._context));
            this.ProcessRequest(new LoginCheck(this._context));
            this.ProcessRequest(new WebAppPage(this._context));
            this.ProcessRequest(new SetPropertys(this._context));
            this.ProcessRequest(new NextUrl(this._context));
            this.ProcessRequest(new NextUrl(this._context));
            this.ProcessRequest(new NextUrl(this._context, false));

            this.ProcessRequest(new SharedInfo(this._context));
            this.ProcessRequest(new AccountInfo(this._context, "https://utas.fut.ea.com:443"));
            this.ProcessRequest(new AccountInfo(this._context, "https://utas.s2.fut.ea.com:443"));
            this.ProcessRequest(new Authentication(this._context));
            this.ProcessRequest(new QuestionPage(this._context));
            this.ProcessRequest(new ValidateQuestion(this._context));
        }

        public FutbotWeb.Json.SearchInfo.RootObject<FutbotWeb.Json.ItemData.PlayerItemData> search_players(SearchContext context)
        {
            AuctionHouseSearch ahs = new AuctionHouseSearch(this._context, Constants.Search.Player);

            ahs.set_search_context(context);
            this.ProcessRequest(ahs);

            return (FutbotWeb.Json.SearchInfo.RootObject<FutbotWeb.Json.ItemData.PlayerItemData>)this._context.Fifa.last_info;
        }

        public FutbotWeb.Json.CreditInfo.RootObject get_credits()
        {
            Credits credits = new Credits(this._context);

            this.ProcessRequest(credits);

            return (FutbotWeb.Json.CreditInfo.RootObject)this._context.Fifa.last_info;
        }

        public FutbotWeb.Json.SearchInfo.RootObject<FutbotWeb.Json.ItemData.PlayerItemData> bid_item(int amount, long trade_id)
        {
            Bid bid = new Bid(this._context, trade_id, amount);

            this.ProcessRequest(bid);

            return (FutbotWeb.Json.SearchInfo.RootObject<FutbotWeb.Json.ItemData.PlayerItemData>)this._context.Fifa.last_info;
        }

        public FutbotWeb.Json.DiscardInfo.RootObject discard_item(long card_id)
        {
            DiscardItem discard = new DiscardItem(this._context, card_id);

            this.ProcessRequest(discard);

            return (FutbotWeb.Json.DiscardInfo.RootObject)this._context.Fifa.last_info;
        }

        #endregion        
    }
}
