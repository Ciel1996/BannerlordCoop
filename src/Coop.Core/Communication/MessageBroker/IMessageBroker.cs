﻿using System;

namespace Coop.Core.Communication.MessageBroker
{
    public interface IMessageBroker : IDisposable
    {
        void Subscribe<T>(Action<MessagePayload<T>> subscriber);
        void Unsubscribe<T>(Action<MessagePayload<T>> subscriber);
        void Publish<T>(object sender, T message);
    }
}