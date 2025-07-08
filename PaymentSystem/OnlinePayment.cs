using System;

namespace PaymentSystem {
    // Abstract class representing an online payment
    public abstract class OnlinePayment : Payment {
        // Properties
        public string PaymentGateway { get; private set; }

        // Constructor
        public OnlinePayment(decimal amount, string currency, string paymentGateway)
            : base(amount, currency) {
            PaymentGateway = paymentGateway ?? throw new ArgumentNullException(nameof(paymentGateway));
        }

        // Methods
        public abstract void Authorize();

        public override bool ValidatePayment() {
            if (!base.ValidatePayment()) {
                return false;
            }
            decimal minimum = Currency == "EUR" ? 5.00m : 5.00m;
            return Amount > minimum;
        }

        public override void LogPayment() {
            base.LogPayment();
            Console.WriteLine($"Payment Gateway: {PaymentGateway}");
            Console.WriteLine("Payment Mode: Online");
        }
    }
}