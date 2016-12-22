using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml;

namespace MyTEBot.Service.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Helper
    {

        //public const string SP_CMS_RRD_VALIDATION = "DBP_CMS_RRD_VALIDATION";
        //public const string SP_CMS_ABACUSBOT_TASK_INSERT_UPDATE = "DBP_CMS_ABACUSBOT_TASK_INSERT_UPDATE";


        //public string GetXMLValue(string MessageGrp, string MessageString)
        //{
        //    XmlTextReader reader = new XmlTextReader(HostingEnvironment.MapPath("~/MessageRepository.xml"));
        //    XmlDocument doc = new XmlDocument();
        //    XmlNode node = doc.ReadNode(reader);

        //    string ResultString = "";

        //    foreach (XmlNode chldNode in node.ChildNodes)
        //    {
        //        if (chldNode.Name == "MessageGroup" && chldNode.Attributes.Item(0).Value == MessageGrp)
        //        {
        //            if (chldNode.HasChildNodes)
        //            {
        //                foreach (XmlNode item in chldNode.ChildNodes)
        //                {
        //                    if (item.Name == "MessageString" && item.Attributes.Item(0).Value == MessageString)
        //                    {
        //                        ResultString = item.InnerText;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return ResultString;
        //}
    }
}
