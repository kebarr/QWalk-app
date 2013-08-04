using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QWalker.Models
{
    public class InputModel
    {
        //private string _left;

        //public string Left
        //{
        //    get { return _left;
        //    }
        //    set { _left = value; }
        //}
        public string Left { get; set; }
        public string Right { get; set; }

        public string ErrorMessage { get; set; }

        public double[][] Results { get; set; }
    }
}