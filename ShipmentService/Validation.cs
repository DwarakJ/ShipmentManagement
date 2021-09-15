using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService
{
    public class Validation
    {
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidUPC(string str)
        {
            if (str.Length > 15)
                return false;

            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public bool IsValidPurchaseDate(DateTime? date)
        {
            DateTime? today_date = System.DateTime.Today;

            double diff = (today_date - date).Value.Days;

            if (diff <= 30 && diff >= 0)
                return true;
            else
                return false;
        }
    }
}
