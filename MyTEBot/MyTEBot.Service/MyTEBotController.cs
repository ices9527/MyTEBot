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
        public IEnumerable<string> GetResponse(string enterpriseID, string message)
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

        //Helper objHelper = new Helper();

        ///// <summary>
        ///// This method is used to get the Resume validations
        ///// </summary>
        ///// <param name="parameters"></param>
        ///// <returns></returns>
        //[HttpPost, Queryable(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
        //public IQueryable<OutputDTO> GetResume(ODataActionParameters parameters)
        //{
        //    string EnterpriseID;
        //    string Message;
        //    string ResponseString = "";
        //    DataSet DBResult;
        //    DataTable dt;


        //    EnterpriseID = parameters.Where(x => x.Key == objHelper.GetXMLValue("Values", "EnterpriseID")).FirstOrDefault().Value.ToString();
        //    Message = parameters.Where(x => x.Key == objHelper.GetXMLValue("Values", "Message")).FirstOrDefault().Value.ToString();

        //    DbContext context = new DbContext(GetConnString());
        //    //string Result = context.Database.SqlQuery<O>("DBP_CMS_RRD_VALIDATION_SIDDHU", Message, EnterpriseID).ToString();
        //    //var Result = context.Database.SqlQuery(typeof(Message), "exec DBP_CMS_RRD_VALIDATION_SIDDHU @P_RRD,@P_ENT_ID", Message, EnterpriseID);

        //    DBResult = GetListDataSet(Helper.SP_CMS_RRD_VALIDATION + " '" + Message + "','" + EnterpriseID + "'");
        //    dt = DBResult.Tables[0];

        //    if (dt.Rows[0][0].ToString() == "0" && dt.Rows[0][1].ToString().ToUpper() == "HELPTEXT")
        //    {
        //        ResponseString = objHelper.GetXMLValue("Validations", "HelpText");
        //    }
        //    else if (dt.Rows[0][0].ToString() == "0" && dt.Rows[0][1].ToString() == "RRD_WRONGFORMAT")
        //    {
        //        ResponseString = objHelper.GetXMLValue("Validations", "RRDWrongFormat").Replace("~rrdno~", Message);
        //    }
        //    else if (dt.Rows[0][0].ToString() == "0" && dt.Rows[0][1].ToString() == "RRD_CLOSED")
        //    {
        //        ResponseString = objHelper.GetXMLValue("Validations", "RRDClosed").Replace("~rrdno~", Message);
        //    }
        //    else if (dt.Rows[0][0].ToString() == "0" && dt.Rows[0][1].ToString() == "RRD_DELETED")
        //    {
        //        ResponseString = objHelper.GetXMLValue("Validations", "RRDDeleted").Replace("~rrdno~", Message);
        //    }
        //    else if (dt.Rows[0][0].ToString() == "0" && dt.Rows[0][1].ToString() == "RRD_DORMANT")
        //    {
        //        ResponseString = objHelper.GetXMLValue("Validations", "RRDDormant").Replace("~rrdno~", Message);
        //    }
        //    else if (dt.Rows[0][0].ToString() == "0" && dt.Rows[0][1].ToString() == "RRD_UNAPPROVED")
        //    {
        //        ResponseString = objHelper.GetXMLValue("Validations", "RRDUnApproved").Replace("~rrdno~", Message);
        //    }
        //    else if (dt.Rows[0][0].ToString() == "0" && dt.Rows[0][1].ToString() == "RRD_SENT_APP")
        //    {
        //        ResponseString = objHelper.GetXMLValue("Validations", "RRDSentApp").Replace("~rrdno~", Message);
        //    }
        //    else if (dt.Rows[0][0].ToString() == "0" && dt.Rows[0][1].ToString() == "RRD_REJECTED")
        //    {
        //        ResponseString = objHelper.GetXMLValue("Validations", "RRDRejected").Replace("~rrdno~", Message);
        //    }
        //    else if (dt.Rows[0][0].ToString() == "0" && dt.Rows[0][1].ToString() == "RRD_NOTFOUND")
        //    {
        //        ResponseString = objHelper.GetXMLValue("Validations", "RRDNotFound").Replace("~rrdno~", Message);
        //    }
        //    else if (dt.Rows[0][0].ToString() == "0" && dt.Rows[0][1].ToString() == "USER_NOTVALID")
        //    {
        //        ResponseString = objHelper.GetXMLValue("Validations", "USERNotValid").Replace("~rrdno~", Message);
        //    }
        //    else if (dt.Rows[0][0].ToString() == "0" && dt.Rows[0][1].ToString() == "TAG_NORESUME")
        //    {
        //        ResponseString = objHelper.GetXMLValue("Validations", "TAGNoResume").Replace("~rrdno~", Message);
        //    }
        //    else
        //    {
        //        string sqlText = Helper.SP_CMS_ABACUSBOT_TASK_INSERT_UPDATE + " NULL,'" + EnterpriseID + "','" + Message + "',2";
        //        ExecuteQuery(sqlText);
        //        ResponseString = objHelper.GetXMLValue("Validations", "EmailSent");
        //    }

        //    OutputDTO objPeople = new OutputDTO();
        //    objPeople.Message = ResponseString;

        //    IList<OutputDTO> list = new List<OutputDTO>();
        //    list.Add(objPeople);
        //    return list.Where(x => x.Message.Contains(ResponseString)).AsQueryable();

        //    //return ResponseString;
        //    //return DataRepository.People.Where(x => x.PeopleKey.Contains(ResponseString)).AsQueryable();

        //}
    }
}