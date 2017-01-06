using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyTEBot.Service.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class OLEDBReader
    {
        private string connString = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public OLEDBReader()
        {
            connString = ConfigurationManager.ConnectionStrings["access_con"].ConnectionString + 
                HttpContext.Current.Server.MapPath(ConfigurationManager.ConnectionStrings["access_path"].ConnectionString); ;
        }

        //OleDbConnection cnnxls = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1\"");/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int GetScalar(string sql)
        {
            int result = 0;
            using (OleDbConnection conn = new OleDbConnection(this.connString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                result = cmd.ExecuteNonQuery();

                conn.Dispose();
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetData(string sql)
        {
            using (OleDbConnection conn = new OleDbConnection(this.connString))
            {
                conn.Open();
                OleDbDataAdapter myadapter = new OleDbDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                myadapter.Fill(ds);
                return ds;
            }
        } 
    }
}
