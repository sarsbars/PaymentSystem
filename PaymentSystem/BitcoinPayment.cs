using System;

namespace PaymentSystem {
    // Class representing a Bitcoin payment
    public class BitcoinPayment : OnlinePayment {
        // Properties
        public string WalletId { get; private set; }

        // Constructor
        public BitcoinPayment(decimal amount, string currency, string paymentGateway, string walletId)
            : base(amount, currency, paymentGateway) {
            WalletId = walletId ?? throw new ArgumentNullException(nameof(walletId));
        }

        // Methods
        public override void ProcessPayment() {
            Console.WriteLine($"Processing BitcoinPayment of {Amount:F2} {Currency} via {PaymentGateway}");
        }

        public override void Authorize() {
            Console.WriteLine($"Authorizing BitcoinPayment with wallet {WalletId} via {PaymentGateway}");
        }

        public override void LogPayment() {
            base.LogPayment();
            Console.WriteLine($"Wallet ID: {WalletId}");
        }
    }
}