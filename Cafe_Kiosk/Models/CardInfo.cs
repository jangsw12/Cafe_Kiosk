using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Models
{
    public class CardInfo
    {
        public string Number { get; set; } = string.Empty;
        public string Expiry { get; set; } = string.Empty;
        public string CVC { get; set; } = string.Empty;
    }
}