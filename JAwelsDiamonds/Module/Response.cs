using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JAwelsDiamonds.Module
{
    public class Response<T>
    {
        public Boolean Success { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }

    }

}