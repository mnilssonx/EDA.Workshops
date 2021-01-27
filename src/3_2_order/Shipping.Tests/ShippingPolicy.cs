using System;
using System.Collections.Generic;
using System.Data;

namespace Shipping.Tests
{
    public class App
    {
        List<IEvent> history = new List<IEvent>();

        public void Given(params IEvent[] events) => history.AddRange(events);

        public void When(IEvent @event)
        {
            history.Add(@event);
            var state = history.Rehydrate<Order>();

            var cmd = ShippingPolicy.When((dynamic)@event);
            history.AddRange(OrderBehavior.Handle(state, (dynamic)cmd));
        }

        public void Then(Action<IEvent[]> f)
            => f(history.ToArray());
    }

    public class ShippingPolicy
    {
        public static ICommand When(PaymentReceived @event) => new CompletePayment();
        public static ICommand When(GoodsPicked @event) => new CompletePacking();
    }

    public static class OrderBehavior
    {
        public static IEnumerable<IEvent> Handle(this Order order, CompletePayment command)
        {
            if (order.Packed && order.Payed)
                yield return new GoodsShipped();
        }

        public static IEnumerable<IEvent> Handle(this Order order, CompletePacking command)
        {
            if (order.Packed && order.Payed)
                yield return new GoodsShipped();
        }
    }

    public class Order
    {
        public bool Payed;
        public bool Packed;

        public Order When(IEvent @event) => this;

        public Order When(PaymentReceived @event)
        {
            Payed = true;
            return this;
        }
        public Order When(GoodsPicked @event)
        {
            Packed = true;
            return this;
        }
    }
}