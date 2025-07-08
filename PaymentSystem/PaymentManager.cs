using System;
using System.Collections.Generic;

namespace PaymentSystem {
    // Class to manage and track payments
    public class PaymentManager {
        // Fields
        private List<Payment> payments = new List<Payment>();

        // Methods
        public void AddPayment(Payment payment) {
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            payments.Add(payment);
        }

        public void ValidatePayments() {
            Console.WriteLine("Validating all payments...");
            for (int i = payments.Count - 1; i >= 0; i--) {
                if (!payments[i].ValidatePayment()) {
                    Console.WriteLine("Invalid payment found:");
                    payments[i].LogPayment();
                    payments.RemoveAt(i);
                }
            }
        }

        public void AuthorizePayments() {
            Console.WriteLine("Authorizing online payments...");
            foreach (var payment in payments) {
                var onlinePayment = payment as OnlinePayment;
                if (onlinePayment != null) {
                    onlinePayment.Authorize();
                }
            }
        }
        public void RecordOffline() {
            Console.WriteLine("Recording offline payments...");
            foreach (var payment in payments) {
                var offlinePayment = payment as OfflinePayment;
                if (offlinePayment != null) {
                    offlinePayment.RecordPayment();
                }
            }
        }
        public void ProcessPayments() {
            Console.WriteLine("Processing all payments...");
            foreach (var payment in payments) {
                payment.ProcessPayment();
            }
        }
    }
}