﻿using EmailService;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.MSMQ
{
    public interface IMqServices
    {
        void AddToQueue(Message email);
        //private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e);


    }
}
