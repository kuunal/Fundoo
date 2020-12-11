using EmailService;
using Experimental.System.Messaging;
using System;
using TokenAuthentication;

namespace BusinessLayer.MSMQ
{
    public class MsmqService : IMqServices
    {
        private readonly MessageQueue messageQueue;
        private IEmailSender _emailSender;

        public MsmqService(IEmailSender emailSender)
        {
            this.messageQueue = new MessageQueue();
            this.messageQueue.Path = @".\private$\Fundoo";
            if (MessageQueue.Exists(this.messageQueue.Path))
            {
                this.messageQueue = new MessageQueue(this.messageQueue.Path);
            }
            else
            {
                this.messageQueue = MessageQueue.Create(this.messageQueue.Path);
            }
            _emailSender = emailSender;
        }

        public void AddToQueue(string email)
        {
            messageQueue.Formatter= new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(email);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQueue.EndReceive(e.AsyncResult);
            string email = msg.Body.ToString();
            EmailService.Message message = new EmailService.Message(new string[] { email },
                    "Added as collaborator",
                    $"You have been collaborated");
            _emailSender.SendEmail(message);   
        }
    }
}
