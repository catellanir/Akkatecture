// The MIT License (MIT)
//
// Copyright (c) 2018 - 2021 Lutando Ngqakaza
// https://github.com/Lutando/Akkatecture 
// 
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Linq.Expressions;
using Akka.Actor;
using Akka.Persistence;
using Akka.TestKit;
using Akkatecture.Aggregates;
using Akkatecture.Core;
using Akkatecture.TestFixture.Aggregates;

namespace Akkatecture.TestFixture.Extensions
{
    public static class TestKitExtensions
    {
        public static IFixtureArranger<TAggregate, TIdentity, TAggregateSnapshot> FixtureFor<TAggregate, TIdentity, TAggregateSnapshot>(
            this TestKitBase testKit, TIdentity aggregateId)
            where TAggregate : ReceivePersistentActor, IAggregateRoot<TIdentity>
            where TAggregateSnapshot : AggregateSnapshot<TAggregate, TIdentity>
            where TIdentity : IIdentity
        {
            return new AggregateFixture<TAggregate, TIdentity, TAggregateSnapshot>(testKit).For(aggregateId);
        }
        
        public static IFixtureArranger<TAggregate, TIdentity, TAggregateSnapshot> FixtureFor<TAggregateManager, TAggregate, TIdentity, TAggregateSnapshot>(
            this TestKitBase testKit, Expression<Func<TAggregateManager>> aggregateManagerFactory, TIdentity aggregateId)
            where TAggregateManager : ReceiveActor, IAggregateManager<TAggregate, TIdentity>
            where TAggregate : ReceivePersistentActor, IAggregateRoot<TIdentity>
            where TAggregateSnapshot : AggregateSnapshot<TAggregate, TIdentity>
            where TIdentity : IIdentity
        {
            return new AggregateFixture<TAggregate, TIdentity, TAggregateSnapshot>(testKit).Using(aggregateManagerFactory,aggregateId);
        }
    }
}