using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLW_WebClient
{
    public class ClsAddress
    {
        //우편번호
        public string Zipcd { get; set; }

        //도로명주소
        public string RoadAddress { get; set; }

        //지번주소
        public string JibunAddress { get; set; }

        //영문주소
        public string RoadAddressEnglish { get; set; }

        //상세주소
        public string AddressDetail { get; set; }
    }
}
