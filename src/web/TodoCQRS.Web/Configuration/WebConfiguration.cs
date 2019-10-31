using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoCQRS.Web.Configuration
{
    public class WebConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; private set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; private set; }
    }
}
