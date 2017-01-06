using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using MyTEBot.Service.Entities;

namespace MyTEBot.Service
{
    /// <summary>
    /// MyTEBotController is a default controller implementation for your WebAPI OData service. 
    /// Modify it to meet your requirements.
    /// </summary>
    public class MyTEBotController : ApiController
    {
        private MyTEBotService myTEBotService = new MyTEBotService();
        
        /// <summary>
        /// 
        /// </summary>
        [HttpGet,
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "1#")]
        public string GetResponse(string enterpriseID, string message)
        {
            try
            {
                var results = myTEBotService.Search(enterpriseID, message);
                return results;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}