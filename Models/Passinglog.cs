using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace MLFF_DEMO.Models {
    public class passinglog {
        public string GANTRY_NAME { set; get; }
        public string PASSING_TIME { set; get; }
        public float AMOUNT { set; get; }
        public float IS_READ { set; get; }
    }
}