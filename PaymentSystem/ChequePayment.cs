using System;

namespace PaymentSystem {
    // Class representing a cheque payment
    public class ChequePayment : OfflinePayment {
        // Properties
        public string ChequeNumber { get; private set; }
        public string BankName { get; private set; }

        // Constructor
        public ChequePayment(decimal amount, string currency, string chequeNumber, string bankName)
            : base(amount, currency) {
            ChequeNumber = chequeNumber ?? throw new ArgumentNullException(nameof(chequeNumber));
            BankName = bankName ?? throw new ArgumentNullException(nameof(bankName));
        }

        // Methods
        public override void ProcessPayment() {
            Console.WriteLine($"Processing ChequePayment of {Amount:F2} {Currency} from {BankName}");
        }

        public override void RecordPayment() {
            Console.WriteLine($"Recording ChequePayment of {Amount:F2} {Currency} with cheque {ChequeNumber}");
        }

        public override bool ValidatePayment() {
            if (!base.ValidatePayment()) {
                return false;
            }

            if (Currency == "USD" && Amount % 1 != 0) {
                return false;
            }

            return true;
        }
        public override void LogPayment() {
            base.LogPayment();
            Console.WriteLine($"Cheque Number: {ChequeNumber}");
            Console.WriteLine($"Bank Name: {BankName}");
        }
    }
}