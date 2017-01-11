using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using MyTEBot.Service.DAL;

namespace MyTEBot.Service
{
    /// <summary>
    /// Main connection between MyTE app and WebService
    /// </summary>
    public class MyTEBotService
    {
        private OLEDBReader oleDbReader;

        /// <summary>
        /// 
        /// </summary>
        public MyTEBotService()
        {
            this.oleDbReader = new OLEDBReader();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enterpriesId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "1#")]
        public string Search(string enterpriesId, string message)
        {
            //if (message.Trim().ToUpper().StartsWith("L"))
            //{
                return GetAnswer(message.Trim().ToUpper());               
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string GetAnswer(string message)
        {
            int number = 0;
            var answer = string.Empty;

            if (message.Equals("HELP") || message.Equals("L"))
            {
                answer = GetRootLevel();
            }
            else if (message.StartsWith("L"))
            {
                var command = message.Replace("L", string.Empty).Trim();
                if (command.Length ==1 && int.TryParse(command, out number))
                {
                    answer = GetLevelOneList(command);
                }
                else if (command.Length == 3)
                {
                    answer = GetLevelTwoAndThreeList(command.Substring(1, 2), 3);
                }
                else if (command.Length == 4)
                {
                    answer = GetLevelThreeItem(command.Substring(1,2),command.Substring(3,1),3);
                }
            }

            if (answer == string.Empty)
            {
                answer = GetRootLevel();
            }

            return answer;

        }
        
        private string GetRootLevel()
        {
            var ds = oleDbReader.GetData(@"SELECT [Answer] FROM [MyTEFAQ] WHERE Node = 'root'");
            return ds.Tables[0].Rows[0][0].ToString();
        }

        private string GetLevelOneList(string command)
        {
            var ds = oleDbReader.GetData(@"SELECT [NodeCd],[Node] FROM [MyTEFAQ] WHERE ParentNode = '" + command + "'");
            return ConcatItems(ds.Tables[0], "NodeCd", "Node", command);
        }

        private string GetLevelOneAndTwoItem(string nodeCode, string parentNodeCode, int level)
        {
            var ds =
                oleDbReader.GetData(@"SELECT [Content] FROM [MyTEFAQ] WHERE NodeCd = '" + nodeCode + " AND ParentNode = '" + parentNodeCode + 
                                "' and Level = " + level );
            return ds.Tables[0].ToString();
        }

        private string GetLevelTwoAndThreeList(string parentNode, int level)
        {
            var prefix = string.Empty;
            DataSet ds = null;
            if (level == 3)
            {
                prefix = GetParentCode(parentNode) + parentNode;
            }
            ds = oleDbReader.GetData(@"SELECT [NodeCd],[Content] FROM [MyTEFAQ] WHERE ParentNode = '" + parentNode + "' AND Level =" + level + " Order by [NodeCd]");
            return ConcatItems(ds.Tables[0], "NodeCd", "Content", prefix);
        }

        private string GetLevelThreeItem(string parentNode, string nodeCode, int level)
        {
            var ds = oleDbReader.GetData(@"SELECT [NodeCd],[Content],[Answer] FROM [MyTEFAQ] WHERE ParentNode = '" + parentNode + "' AND NodeCd = '" + nodeCode + 
                                         "' AND Level = " + level);
            var questionNumber = GetParentCode(parentNode) + parentNode + nodeCode;
            return FormatCodeAndQuestion(questionNumber, ds.Tables[0].Rows[0][1].ToString(),
                ds.Tables[0].Rows[0][2].ToString());
        }

        private string GetParentCode(string nodeCode)
        {
            var ds = oleDbReader.GetData(@"SELECT [ParentNode] FROM [MyTEFAQ] WHERE NodeCd = '" + nodeCode + "'");
                return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0
                    ? ds.Tables[0].Rows[0][0].ToString() : string.Empty;
        }

        private string FormatCodeAndContent(string parentNode,string code, string content)
        {
            return string.Format("<{0}{1}> - {2} \n", parentNode,code, content);
        }

        private string FormatCodeAndQuestion(string code, string question, string answer)
        {
            return string.Format("<{0}> - {1} \n Answer: {2}", code, question, answer);
        }

        private string ConcatQuestionAndAnswer(DataTable table, string nodeCode, string columnName1, string columnName2)
        {
            var concatResult = string.Empty;
            if (table != null && table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                concatResult = FormatCodeAndQuestion(nodeCode, row[columnName1] == DBNull.Value ? string.Empty : row[columnName1].ToString(),
                        row[columnName2] == DBNull.Value ? string.Empty : row[columnName2].ToString());
            }
            return concatResult;
        }

        private string ConcatItems(DataTable table, string columnName1, string columnName2, string parentNode)
        {
            var concatResult = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                concatResult += FormatCodeAndContent(parentNode,row[columnName1] == DBNull.Value ? string.Empty : row[columnName1].ToString(),
                    row[columnName2] == DBNull.Value ? string.Empty : row[columnName2].ToString());
            }
            concatResult = concatResult.Trim();
            return concatResult.Length <= 2 ? concatResult : concatResult.Substring(0, concatResult.Length - 2);
        }
    }
  
}
