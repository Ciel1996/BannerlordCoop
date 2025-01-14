﻿using Autofac;
using Common;
using Common.LogicStates;
using Coop.Core.Client;
using Coop.Core.Communication.PacketHandlers;
using Coop.Core.Server;
using System;
using System.Xml.Serialization;

namespace Coop.Core
{
    public class CoopartiveMultiplayerExperience : IUpdateable
    {

        public static UpdateableList Updateables { get; } = new UpdateableList();

        private static IContainer _container;

        public int Priority => 0;

        public void Update(TimeSpan deltaTime)
        {
            Updateables.UpdateAll(deltaTime);
        }

        public void StartAsServer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule<CoopModule>();
            builder.RegisterModule<ServerModule>();
            _container = builder.Build();

            var server = _container.Resolve<ICoopNetwork>();
            Updateables.Add(server);

            var logic = _container.Resolve<ILogic>();
            logic.Start();


        }

        public void StartAsClient()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule<CoopModule>();
            builder.RegisterModule<ClientModule>();
            _container = builder.Build();

            var client = _container.Resolve<ICoopNetwork>();
            Updateables.Add(client);

            var logic = _container.Resolve<ILogic>();
            logic.Start();
        }
    }
}
