using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Read.Core.ValueObjects
{
    public class CartStatus
    {
        public string Status { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public CartStatus(string status, DateTime dateUpdated)
        {
            Status = status;
            DateUpdated = dateUpdated;
        }
     
    }
}
