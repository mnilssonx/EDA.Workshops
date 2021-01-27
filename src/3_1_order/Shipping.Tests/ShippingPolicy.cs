namespace Shipping.Tests
{
    public class ShippingPolicy
    {
        public static ICommand When(PaymentReceived @event, Order state) => Ship(state.When(@event));

        public static ICommand When(GoodsPicked @event, Order state) => Ship(state.When(@event));

        private static ICommand Ship(Order state)
        {
            return state.Payed && state.Packed ? new Ship() : null;
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