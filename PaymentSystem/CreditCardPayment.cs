using System;

namespace PaymentSystem {
    // Class representing a credit card payment
    public class CreditCardPayment : OnlinePayment {
        // Properties
        public string CardNumber { get; private set; }
        public string ExpiryDate { get; private set; }
        public string CVV { get; private set; }

        // Constructor
        public CreditCardPayment(decimal amount, string currency, string paymentGateway, string cardNumber, string expiryDate, string cvv)
            : base(amount, currency, paymentGateway) {
            CardNumber = cardNumber ?? throw new ArgumentNullException(nameof(cardNumber));
            ExpiryDate = expiryDate ?? throw new ArgumentNullException(nameof(expiryDate));
            CVV = cvv ?? throw new ArgumentNullException(nameof(cvv));
        }

        // Methods
        public override void ProcessPayment() {
            Console.WriteLine($"Processing CreditCardPayment of {Amount:F2} {Currency} via {PaymentGateway}");
        }

        public override void Authorize() {
            Console.WriteLine($"Authorizing CreditCardPayment with card {CardNumber} via {PaymentGateway}");
        }

        public override bool ValidatePayment() {
            if (!base.ValidatePayment()) {
                return false;
            }

            decimal minimum = Currency == "EUR" ? 10.00m : 5.00m;
            return Amount >= minimum;
        }

        public override void LogPayment() {
            base.LogPayment();
            Console.WriteLine($"Card Number: {CardNumber}");
            Console.WriteLine($"Expiry Date: {ExpiryDate}");
            Console.WriteLine($"CVV: {CVV}");
        }
    }
}