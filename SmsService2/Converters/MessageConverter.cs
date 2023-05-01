using SmsService2.Models;
using SmsService2.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsService2.Converters
{
    public static class MessageConverter
    {
        public static Message ToDao(MessageModel messageModel)
        {
            Message message = new Message();

            message.Created = messageModel.Created;
            message.Sender = messageModel.Sender;
            message.Recipient = messageModel.Recipient;
            message.AccountState = messageModel.AccountState;

            return message;
        }
    }
}
