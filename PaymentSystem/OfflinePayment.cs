using System;

namespace PaymentSystem {
    // Abstract class representing an offline payment
    public abstract class OfflinePayment : Payment {
        // Constructor
        public OfflinePayment(decimal amount, string currency)
            : base(amount, currency) {
        }

        // Methods
        public abstract void RecordPayment();

        public override bool ValidatePayment() {
            if (!base.ValidatePayment()) {
                return false;
            }

            if (Currency == "EUR") {
                return false;
            }

            return true;
        }

        public override void LogPayment() {
            base.LogPayment();
            Console.WriteLine("Payment Mode: Offline");
        }
    }
}