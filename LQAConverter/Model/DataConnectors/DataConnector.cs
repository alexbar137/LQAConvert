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

            Freelancers.FreelancersDataContext site = new Freelancers.FreelancersDataContext(new Uri("http://inside.office.palex/lqa/_vti_bin/ListData.svc"));
            site.Credentials = CredentialCache.DefaultNetworkCredentials;

            
            
           
        }

        public class Language 
        {
        }
        
        
    }
}
