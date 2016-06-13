using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel=Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Data;


namespace SoapWebService.SampleTest
{
    class WorkbookUtil
    {
        Dictionary<string, string> props = new Dictionary<string, string>();
        private const string CONNECTION_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=<FILENAME>;Extended Properties=\"Excel 8.0;HDR=Yes;\";";




        public DataTable GetDataTableFromExcelFile(string fullFileName, string sheetName)
        {

            props["Provider"] = "Microsoft.ACE.OLEDB.12.0";
            props["Data Source"] = fullFileName;
            props["Extended Properties"] = "Excel 12.0";
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }
            string properties = sb.ToString();

            OleDbConnection objConnection = new OleDbConnection();
            objConnection = new OleDbConnection(properties);

            using (OleDbConnection conn = new OleDbConnection(properties))
            {
                conn.Open();
                //Get All Sheets Name
                DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });

                //Get the First Sheet Name
                string firstSheetName = sheetsName.Rows[0][2].ToString();

                //Query String 
                string sql = string.Format("SELECT * FROM [{0}]", firstSheetName);
                OleDbDataAdapter ada = new OleDbDataAdapter(sql, properties);
                DataSet set = new DataSet();
                ada.Fill(set);
                return set.Tables[0];
            }

        }
    }
}



