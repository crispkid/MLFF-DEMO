using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace MLFF_DEMO.Models {
    public class topup {
        public string USER_SN { set; get; }
        public float AMOUNT { set; get; }
    }
}