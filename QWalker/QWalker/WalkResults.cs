using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Numerics;

namespace QWalker
{
    public class WalkResults
    {
        public double[][] Results { get; set; }
        public DateTime DateRun { get; set; }
        //adding initial conditions to here should hopefully make it easier to save to Db
        public Complex Left { get; set; }
        public Complex Right { get; set; }
    }
}
