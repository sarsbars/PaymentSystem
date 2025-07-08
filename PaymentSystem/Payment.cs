using System;

namespace PaymentSystem {
    // Abstract class representing a generic payment
    public abstract class Payment {
        // Properties
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        // Constructor
        public Payment(decimal amount, string currency) {
            Amount = amount;
            Currency = currency.ToUpper();
        }

        // Methods
        public abstract void ProcessPayment();

        public virtual bool ValidatePayment() {
            if (Amount <= 0) {
                return false;
            }

            if (Amount % 1 == 0.99m) {
                return false;
            }

            return true;
        }

        public virtual void LogPayment() {
            Console.WriteLine($"Payment Type: {GetType().Name}");
            Console.WriteLine($"Amount: {Amount:F2} {Currency}");
        }
    }
}