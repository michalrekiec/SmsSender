using SmsService2.Converters;
using SmsService2.Database;
using SmsService2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace SmsService2.Repositories
{
    public class MessageRepository
    {
        public void AddMessage(MessageModel messageModel)
        {
            var messageToAdd = MessageConverter.ToDao(messageModel);

            using (var context = new ApplicationDbContext())
            {
                context.Messages.Add(messageToAdd);

                context.SaveChanges();
            }
        }

        public bool IsNewCustomer(string recipient)
        {
            var isNewCustomer = true;

            using (var context = new ApplicationDbContext())
            {
                var quantity = context.Messages.Where(x => x.Recipient == recipient).Count();

                if (quantity > 0)
                    isNewCustomer = false;
                else
                    isNewCustomer = true;
            }

            return isNewCustomer;
        }

        public string Balance(string recipient, decimal accountState)
        {
            string result = "";

            using (var context = new ApplicationDbContext())
            {
                var messageList = context.Messages.Where(x => x.Recipient == recipient).ToList();

                var balance =
                    accountState - messageList[messageList.Count - 1].AccountState;

                if (balance > 0)
                    result += $"+{balance.ToString("0.00")}";
                else
                    result += $"{balance.ToString("0.00")}";
            }

            return result;
        }
    }
}
