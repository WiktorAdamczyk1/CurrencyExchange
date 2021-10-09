using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class UserCurrencySettingsModel
    {
        public string UserId { get; set; }
        public bool USD { get; set; }
        public bool EUR { get; set; }
        public bool CHF { get; set; }
        public bool RUB { get; set; }
        public bool CZK { get; set; }
        public bool GBP { get; set; }
    }
}
