using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YLWService;

namespace YLW_WebClient
{
    public interface ISmplInputer
    {
        void SetReadOnlyMode(bool rdonly);
        bool LoadDocument(ReportParam p);
        void Reload(DataSet pds);
    }
}
