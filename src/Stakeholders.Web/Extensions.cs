using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stakeholders.Web
{
    public static class Extensions
    {
        public static DateTime? ToNullable(this DateTime date)
        {
            if (date != default(DateTime))
            {
                return date;
            }

            return null;
        }
    }
}
