using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLW_WebClient
{
    public interface IViewerSheet
    {
        void Print(string printername);
        void PrintPreview(string printername);
    }
}
