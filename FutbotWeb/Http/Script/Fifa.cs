using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FutbotWeb.Http.Script
{
    public class Fifa
    {
        public string next_url, utas_url, custom_data, nucleus_id, persona_name, plattform, session_id, utas_client_url, phising_token;
        public int persona_id;
        public CookieContainer cookie_container;
        public bool online;
        public object last_info;

        public Fifa()
        {
            this.next_url = "";
            this.utas_url = "";
            this.custom_data = "";
            this.nucleus_id = "";
            this.persona_name = "";
            this.plattform = "";
            this.utas_client_url = "";
            this.phising_token = "";
            this.persona_id = 0;
            this.cookie_container = new CookieContainer();

            this.online = false;

            this.last_info = null;
        }
    }
}
