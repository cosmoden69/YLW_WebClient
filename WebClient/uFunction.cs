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

        public static string CreateLasRprtNo(int companySeq, string aseq, string rno)
        {
            try
            {
                YLWService.YlwSecurityJson security = YLWService.MTRServiceModule.SecurityJson.Clone();  //깊은복사
                security.serviceId = "Metro.Package.AdjSL.BisRprtGoodsPrint";
                security.methodId = "CreateLastRprtNo";
                security.companySeq = companySeq;

                DataSet ds = new DataSet("ROOT");
                DataTable dt = ds.Tables.Add("DataBlock1");

                dt.Columns.Add("AcptMgmtSeq");
                dt.Columns.Add("ReSurvAsgnNo");

                dt.Clear();
                DataRow dr = dt.Rows.Add();

                dr["AcptMgmtSeq"] = aseq;
                dr["ReSurvAsgnNo"] = rno;

                DataSet yds = YLWService.MTRServiceModule.CallMTRServiceCallPost(security, ds);
                if (yds != null && yds.Tables.Count > 0)
                {
                    return Utils.ConvertToString(yds.Tables[0].Rows[0]["LasRprtNo"]);
                }
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        public static string GetDeptGrpCd(string rptnm)
        {
            switch (rptnm)
            {
                case "RptAdjSLRptSurvRptPersHyundai":
                case "RptAdjSLRptSurvRptPersHyundaiMid":
                    return "2";
                case "RptAdjSLRptSurvRptPersHeungkuk":
                case "RptAdjSLRptSurvRptPersHeungkukMid":
                    return "3";
                case "RptAdjSLRptSurvRptPersMeritz":
                case "RptAdjSLRptSurvRptPersMeritzMid":
                    return "4";
                case "RptAdjSLRptSurvRptPersDBLife":
                case "RptAdjSLRptSurvRptPersDBLifeMid":
                    return "5";
                case "RptAdjSLRptSurvRptPersDBLoss":
                case "RptAdjSLRptSurvRptPersDBLossMid":
                    return "6";
                case "RptAdjSLRptSurvRptPersMGLossSmpl":
                case "RptAdjSLRptSurvRptPersMGLossSmplMid":
                    return "7";
                case "RptAdjSLRptSurvRptPersMGLoss":
                case "RptAdjSLRptSurvRptPersMGLossMid":
                    return "8";
                default:
                    return "1";
            }
        }
    }
}
