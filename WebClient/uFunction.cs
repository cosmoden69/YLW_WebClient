using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using YLWService;

namespace YLW_WebClient.CAA
{
    class uFunction
    {
        public static int GetDelayDays(int companySeq, string frdt, string todt)
        {
            try
            {
                string strSql = "";
                strSql += " SELECT dbo._fnAdjGetSolarWDays('" + Utils.ConvertToString(companySeq) + "', '" + frdt + "', '" + todt + "') AS CNT ";
                strSql += " FOR JSON PATH ";
                DataSet ds = YLWService.MTRServiceModule.CallMTRGetDataSetPost(companySeq, strSql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Utils.ToInt(ds.Tables[0].Rows[0]["CNT"]);
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
    }
}
