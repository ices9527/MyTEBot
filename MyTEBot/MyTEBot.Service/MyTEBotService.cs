﻿using System;
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
        
        private string TrackQuestion(string command)
        {
            var L1NO = command.Substring(0, 1);
            var Tracker = @"||" + GetTrackerItem(L1NO, "0", L1NO);
            if (command.Length >1)
            {
                var L2NO = command.Substring(1, 2);
                Tracker += @" |" + GetTrackerItem(L2NO, L1NO, command.Substring(0, 3));
            }
            Tracker += "\n\n";
            return Tracker;
        }
        
        private string GetRootLevel()
        {
            var ds = oleDbReader.GetData(@"SELECT [Answer] FROM [MyTEFAQ] WHERE Node = 'root'");
            return ds.Tables[0].Rows[0][0].ToString();
        }

        private string GetTrackerItem(string nodeCode, string parentNode, string command)
        {
            var ds = oleDbReader.GetData(@"SELECT [Content] FROM [MyTEFAQ] WHERE ParentNode ='" + parentNode + "' AND NodeCd ='" + nodeCode + "'");
            return string.Format("<{0}> {1}", command, ds.Tables[0].Rows[0]["Content"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["Content"].ToString());
        }

        private string GetLevelOneList(string command)
        {
            var ds = oleDbReader.GetData(@"SELECT [NodeCd],[Node] FROM [MyTEFAQ] WHERE ParentNode = '" + command + "'");
            return TrackQuestion(command) + ConcatItems(ds.Tables[0], "NodeCd", "Node", command,"C");
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
            return TrackQuestion(prefix) + ConcatItems(ds.Tables[0], "NodeCd", "Content", prefix,"Q");
        }

        private string GetLevelThreeItem(string parentNode, string nodeCode, int level)
        {
            var ds = oleDbReader.GetData(@"SELECT [NodeCd],[Content],[Answer] FROM [MyTEFAQ] WHERE ParentNode = '" + parentNode + "' AND NodeCd = '" + nodeCode + 
                                         "' AND Level = " + level);
            var questionNumber = GetParentCode(parentNode) + parentNode + nodeCode;
            return TrackQuestion(questionNumber) + FormatCodeAndQuestion(questionNumber, ds.Tables[0].Rows[0][1].ToString(),
                ds.Tables[0].Rows[0][2].ToString());
        }

        private string GetParentCode(string nodeCode)
        {
            var ds = oleDbReader.GetData(@"SELECT [ParentNode] FROM [MyTEFAQ] WHERE NodeCd = '" + nodeCode + "'");
                return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0
                    ? ds.Tables[0].Rows[0][0].ToString() : string.Empty;
        }

        private string FormatCodeAndContentForCategory(string parentNode,string code, string content)
        {
            return string.Format("<{0}{1}>(C) {2} \n", parentNode,code, content);
        }

        private string FormatCodeAndContentForQuestion(string parentNode, string code, string content)
        {
            return string.Format("<{0}{1}>(Q) {2} \n", parentNode, code, content);
        }

        private string FormatCodeAndQuestion(string code, string question, string answer)
        {
            return string.Format("<{0}>(Q) {1} \n (A): {2}", code, question, answer);
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

        private string ConcatItems(DataTable table, string columnName1, string columnName2, string parentNode, string level)
        {
            var concatResult = string.Empty;

            if (level.Equals("C"))
            {
                foreach (DataRow row in table.Rows)
                {
                    concatResult += FormatCodeAndContentForCategory(parentNode, row[columnName1] == DBNull.Value ? string.Empty : row[columnName1].ToString(),
                        row[columnName2] == DBNull.Value ? string.Empty : row[columnName2].ToString());
                }
                concatResult += string.Format("\n{0} categories \nPlease type 'L + number' to start with following category.", table.Rows.Count); 
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    concatResult += FormatCodeAndContentForQuestion(parentNode, row[columnName1] == DBNull.Value ? string.Empty : row[columnName1].ToString(),
                        row[columnName2] == DBNull.Value ? string.Empty : row[columnName2].ToString());
                }
                concatResult += string.Format("\n{0} questions \nPlease type 'L + number' to start with following category.", table.Rows.Count);
            }
            return concatResult.Trim();
        }
    }
  
}
