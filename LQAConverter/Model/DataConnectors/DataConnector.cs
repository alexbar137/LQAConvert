using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SP = Microsoft.SharePoint.Client;
using System.Diagnostics;
using System.Net;
using System.Data.Services.Client;


namespace LQAConverter.Model.DataConnectors
{
    public class DataConnector
    {
        public DataConnector() 
        {

            string siteURL = "http://inside.office.palex/lqa/_vti_bin/ListData.svc";
            ClientContext context = new ClientContext(new Uri(siteURL));
            CredentialCache cc = new CredentialCache();
            cc.Add(new Uri(siteURL), "NTLM", CredentialCache.DefaultNetworkCredentials);
            context.Credentials = cc;
            context.AuthenticationMode = ClientAuthenticationMode.Default;

            Web webSite = context.Web;
            var collList = webSite.Lists;

            context.Load(webSite);
            context.Load(collList);
            context.ExecuteQuery();

            foreach (var i in collList)
            {
                Debug.WriteLine(i.Title);
            }
           
        }

        public class Language 
        {
        }
        
        
    }
}
