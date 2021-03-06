// Copyright 2007-2017 Chris Patterson, Dru Sellers, Travis Smith, et. al.
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
    using MassTransit.Topology.Configuration;


    public interface IRabbitMqMessageConsumeTopologyConfigurator<TMessage> :
        IMessageConsumeTopologyConfigurator<TMessage>,
        IRabbitMqMessageConsumeTopology<TMessage>
        where TMessage : class
    {
        /// <summary>
        /// Adds the exchange bindings for this message type
        /// </summary>
        /// <param name="configure">Configure the binding and the exchange</param>
        void Bind(Action<IBindExchangeConfigurator> configure = null);
    }


    public interface IRabbitMqMessageConsumeTopologyConfigurator :
        IMessageConsumeTopologyConfigurator
    {
        new IRabbitMqMessageConsumeTopologyConfigurator<T> GetMessageTopology<T>()
            where T : class;

        /// <summary>
        /// Apply the message topology to the builder
        /// </summary>
        /// <param name="builder"></param>
        void Apply(IRabbitMqConsumeTopologyBuilder builder);
    }
}