using System;

namespace PaymentSystem {
    // Class representing a cash payment
    public class CashPayment : OfflinePayment {
        // Constructor
        public CashPayment(decimal amount, string currency)
            : base(amount, currency) {
        }

        // Methods
        public override void ProcessPayment() {
            Console.WriteLine($"Processing CashPayment of {Amount:F2} {Currency}");
        }

        public override void RecordPayment() {
            Console.WriteLine($"Recording CashPayment of {Amount:F2} {Currency} in offline system");
        }

        public override bool ValidatePayment() {
            if (Amount <= 0) {
                return false;
            }

            if (Currency == "EUR") {
                return false; 
            }

            return true;
        }
    }
}