using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsService2.Models
{
    public class MessageModel
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public decimal AccountState { get; set; }
        public DateTime Created { get; set; }
        public string Body { get; set; }
    }
}
