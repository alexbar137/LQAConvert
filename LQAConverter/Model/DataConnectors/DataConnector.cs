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
            context.Credentials = CredentialCache.DefaultNetworkCredentials;

            Web webSite = context.Web;
            ListCollection collList = webSite.Lists;

            context.Load(collList);
            context.ExecuteQuery();
            
            foreach (SP.List s in collList)
            {
                Debug.WriteLine("Title: {0} Created: {1}", s.Title, s.Created.ToString());
            }           
        }

        public class Language 
        {
        }
        
        
    }
}
