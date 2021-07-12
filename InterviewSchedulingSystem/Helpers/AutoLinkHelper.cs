using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Helpers
{
    public static class AutoLinkHelper
    {
        public static string GenLink()
        {
            var guid = Guid.NewGuid().ToString().Split('-');
            string pass = "";
            foreach (var item in guid)
            {
                pass += item;
                if (pass.Length == 16)
                    return pass;
            }
            return pass;
        }
    }
}
