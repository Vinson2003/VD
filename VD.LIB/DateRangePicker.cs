using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.LIB
{
    public static class DateOptions
    {
        public static string dateOption(string elementId)
        {
            // Implementasi dari dateOptionDOB Anda di sini.
            // Contoh sederhana:
            return string.Format("$('#{0}').datepicker();", elementId);
        }
    }
}
