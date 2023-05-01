using SmsService2.Models;
using SmsService2.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SmsService2.TwilioApi
{
    public class TwilioMessage
    {
        string accountSid = ConfigurationManager.AppSettings["accountSid"];
        string authToken = ConfigurationManager.AppSettings["authToken"];
        MessageModel messageModel = new MessageModel();

        private MessageRepository _messageRepository = new MessageRepository();

        public void SendMessage()
        {
            TwilioClient.Init(accountSid, authToken);

            messageModel.Created = DateTime.Now;
            messageModel.Sender = ConfigurationManager.AppSettings["sender"];
            messageModel.AccountState = SetAccountState();
            messageModel.Recipient = ConfigurationManager.AppSettings["recipient"];
            messageModel.Body = SetMessageBody(messageModel.Recipient, messageModel.AccountState);

            var message = MessageResource.Create(
                body: messageModel.Body,
                from: new Twilio.Types.PhoneNumber(messageModel.Sender),
                to: new Twilio.Types.PhoneNumber(messageModel.Recipient)
            );

            _messageRepository.AddMessage(messageModel);

            Console.WriteLine(message.Sid);
        }

        private decimal SetAccountState()
        {
            var rnd = new Random();

            var accountState = rnd.Next(1, 100000000);
            return accountState / 100M;
        }

        private string SetMessageBody(string recipient, decimal accountState)
        {
            var messageBody = $"CSharp Bank S.A.\nStan konta wynosi {accountState.ToString("0.00")}";

            if (_messageRepository.IsNewCustomer(recipient) == false)
            {
                messageBody += $"\nBilans {_messageRepository.Balance(recipient, accountState)}";
            }

            return messageBody;
        }
    }
}
