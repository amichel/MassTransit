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
namespace MassTransit.RabbitMqTransport.Topology.Configurators
{
    using System.Collections.Generic;


    public class ExchangeBindingConfigurator :
        ExchangeBinding,
        IBindExchangeConfigurator
    {
        readonly MessageExchange _source;

        public ExchangeBindingConfigurator(MessageExchange source, string routingKey)
        {
            _source = source;
            RoutingKey = routingKey;

            Arguments = new Dictionary<string, object>();
        }

        public Exchange Source => _source;

        public string RoutingKey { get; set; }

        public IDictionary<string, object> Arguments { get; }

        public bool Durable
        {
            set { _source.Durable = value; }
        }

        public bool AutoDelete
        {
            set { _source.AutoDelete = value; }
        }

        public string ExchangeType
        {
            set { _source.Type = value; }
        }

        public void SetExchangeArgument(string key, object value)
        {
            if (value != null)
                _source.Arguments[key] = value;
            else
                _source.Arguments.Remove(key);
        }

        public void SetBindingArgument(string key, object value)
        {
            if (value != null)
                Arguments[key] = value;
            else
                Arguments.Remove(key);
        }
    }
}