using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rebar.Soa.Client;
using MyTEBot.Service.Tests;


namespace MyTEBot.Service.Tests.Utils
{
    public static class Caller
    {

        //public static List<Entities.MyTEBotResult> CallWebService(string term)
        //{
        //    var url = String.Format(Properties.Settings.Default["MyTEBotServiceURL"].ToString(), term);
        //    var uri = new Uri(url).CreateServiceableUri();
        //    var client = ServiceCallFactory.CreateServiceableWebClient();
        //    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
        //    var response =    client.DownloadData(uri.Uri);
        //    var stringResponse = Encoding.UTF8.GetString(response);
        //    return JsonConvert.DeserializeObject<List<Entities.MyTEBotResult>>(stringResponse);
        //}

    }
}
