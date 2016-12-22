using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace MyTEBot.Service
{
    /// <summary>
    /// Main connection between MyTE app and WebService
    /// </summary>
    public class MyTEBotService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enterpriesId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "1#")]
        public IEnumerable<string> Search(string enterpriesId, string message)
        {
            return new List<string>() {GetAnswer(message)};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string GetAnswer(string message)
        {
            var answer = string.Empty;
            int input = 0;
            if (int.TryParse(message,out input))
            {
                if (input < 10)
                {
                    answer = "The RRD number you entered is in wrong format, RRD number should be R## or r##";
                }
                else if (input < 20)
                {
                    answer = "The RRD ~rrdno~ that you have entered is already marked as Closed in the system, kindly enter an active demand to view the matching supply";
                }
                else if (input < 30)
                {
                    answer = "The RRD ~rrdno~ that you have entered is already marked as deleted in the system, kindly enter an active demand to view the matching supply";
                }
                else if (input < 40)
                {
                    answer = "The RRD ~rrdno~ that you have entered is currently in dormant state in the system, kindly enter an active demand to view the matching supply";
                }
                else if (input < 50)
                {
                    answer = "The RRD ~rrdno~ that you have entered is in unapproved state, kindly enter an approved demand to view the matching supply";
                }
                else
                {
                    answer =
                        @"      You can input the below mentioned parameters in the AbacusBot chat window to receive the information directly to your mail box.\r\n
      1 - To receive candidate resumes from the CMS IDC application enter 1 #RRD Number; E.g. 1 R123456 \r\n
      2 - To receive candidate resumes from the CMS PDC application enter 2 #RRD Number; E.g. 2 R123456 \r\n
      3 - To receive matching supply list for an IDC Demand enter 3 #RRD Number; E.g. 3 R123456 \r\n
      4 - To receive matching supply list for a PDC Demand enter 4 #RRD Number; E.g. 4 R123456 \r\n";
                }
            }
            else
            {
                answer =
                   @"      You can input the below mentioned parameters in the AbacusBot chat window to receive the information directly to your mail box.\r\n
      1 - To receive candidate resumes from the CMS IDC application enter 1 #RRD Number; E.g. 1 R123456 \r\n
      2 - To receive candidate resumes from the CMS PDC application enter 2 #RRD Number; E.g. 2 R123456 \r\n
      3 - To receive matching supply list for an IDC Demand enter 3 #RRD Number; E.g. 3 R123456 \r\n
      4 - To receive matching supply list for a PDC Demand enter 4 #RRD Number; E.g. 4 R123456 \r\n";               
            }

            return answer;

        }
    }
  
}
