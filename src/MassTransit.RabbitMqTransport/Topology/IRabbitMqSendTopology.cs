﻿// Copyright 2007-2017 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.RabbitMqTransport.Topology
{
    using System;
    using Builders;
    using MassTransit.Topology;


    public interface IRabbitMqSendTopology :
        ISendTopology
    {
        new IRabbitMqMessageSendTopologyConfigurator<T> GetMessageTopology<T>()
            where T : class;

        SendSettings GetSendSettings(Uri address);

        /// <summary>
        /// Returns the topology for the specified address
        /// </summary>
        /// <param name="address">A valid RabbitMQ endpoint address</param>
        /// <returns></returns>
        TopologyLayout GetTopologyLayout(Uri address);
    }
}