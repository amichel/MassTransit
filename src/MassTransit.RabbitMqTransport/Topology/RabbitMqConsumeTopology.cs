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
    using System.Collections.Generic;
    using Configurators;
    using MassTransit.Topology;
    using MassTransit.Topology.Configuration;
    using Specifications;


    public class RabbitMqConsumeTopology :
        ConsumeTopology,
        IRabbitMqConsumeTopologyConfigurator
    {
        readonly IList<IRabbitMqConsumeTopologySpecification> _specifications;

        public RabbitMqConsumeTopology(IEntityNameFormatter entityNameFormatter)
            : base(entityNameFormatter)
        {
            ExchangeTypeSelector = new FanoutExchangeTypeSelector();

            _specifications = new List<IRabbitMqConsumeTopologySpecification>();
        }

        public IExchangeTypeSelector ExchangeTypeSelector { get; }

        IRabbitMqMessageConsumeTopologyConfigurator<T> IRabbitMqConsumeTopology.GetMessageTopology<T>()
        {
            IMessageConsumeTopologyConfigurator<T> configurator = base.GetMessageTopology<T>();

            return configurator as IRabbitMqMessageConsumeTopologyConfigurator<T>;
        }

        public void Apply(IRabbitMqConsumeTopologyBuilder builder)
        {
            foreach (var specification in _specifications)
            {
                specification.Apply(builder);
            }

            ForEach<IRabbitMqMessageConsumeTopologyConfigurator>(x => x.Apply(builder));
        }

        public void Bind(string exchangeName, Action<IBindExchangeConfigurator> configure = null)
        {
            var exchangeType = ExchangeTypeSelector.DefaultExchangeType;

            var autoDelete = false;
            var durable = true;

            var exchange = new MessageExchange(exchangeName, exchangeType, durable, autoDelete);

            var binding = new ExchangeBindingConfigurator(exchange, "");

            configure?.Invoke(binding);

            var specification = new ExchangeBindingConsumeTopologySpecification(binding);

            _specifications.Add(specification);
        }

        protected override IMessageConsumeTopologyConfigurator CreateMessageTopology<T>(Type type)
        {
            var entityNameFormatter = new MessageEntityNameFormatter<T>(EntityNameFormatter);
            var exchangeTypeSelector = new MessageExchangeTypeSelector<T>(ExchangeTypeSelector);

            var messageTopology = new RabbitMqMessageConsumeTopology<T>(entityNameFormatter, exchangeTypeSelector);

            OnMessageTopologyCreated(messageTopology);

            return messageTopology;
        }
    }
}