using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace MLFF_DEMO.Models {
    public class user_info {
        public string USER_SN { set; get; }
        public string USER_NAME { set; get; }
        public string USER_ID { set; get; }
        public string USER_PW { set; get; }
        public string MOBILE_NUMBER { set; get; }
        public string E_MAIL { set; get; }

    }
}