using System;

namespace PaymentSystem {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("PaymentPlus Backend Demonstration\n");

            PaymentManager manager = new PaymentManager();

            try {
                // Credit Card Payments
                manager.AddPayment(new CreditCardPayment(10.00m, "CAD", "Stripe", "1234-5678-9012-3456", "12/25", "123")); // Valid
                manager.AddPayment(new CreditCardPayment(4.99m, "CAD", "Stripe", "1234-5678-9012-3456", "12/25", "123")); // Invalid: < $5
                manager.AddPayment(new CreditCardPayment(9.99m, "CAD", "Stripe", "1234-5678-9012-3456", "12/25", "123")); // Invalid: .99
                manager.AddPayment(new CreditCardPayment(9.00m, "EUR", "Stripe", "1234-5678-9012-3456", "12/25", "123")); // Invalid: < €10
                manager.AddPayment(new CreditCardPayment(10.00m, "EUR", "Stripe", "1234-5678-9012-3456", "12/25", "123")); // Valid

                // Bitcoin Payments
                manager.AddPayment(new BitcoinPayment(6.00m, "USD", "Coinbase", "wallet123")); // Valid
                manager.AddPayment(new BitcoinPayment(5.00m, "USD", "Coinbase", "wallet123")); // Invalid: <= $5
                manager.AddPayment(new BitcoinPayment(5.99m, "USD", "Coinbase", "wallet123")); // Invalid: .99

                // Cash Payments
                manager.AddPayment(new CashPayment(1.99m, "CAD")); // Valid: .99 allowed
                manager.AddPayment(new CashPayment(10.00m, "CAD")); // Valid
                manager.AddPayment(new CashPayment(0.00m, "CAD")); // Invalid: <= 0
                manager.AddPayment(new CashPayment(10.00m, "EUR")); // Invalid: EUR offline

                // Cheque Payments
                manager.AddPayment(new ChequePayment(5.00m, "USD", "12345", "BankA")); // Valid
                manager.AddPayment(new ChequePayment(5.50m, "USD", "12346", "BankB")); // Invalid: Non-whole USD
                manager.AddPayment(new ChequePayment(10.00m, "CAD", "12347", "BankC")); // Valid
                manager.AddPayment(new ChequePayment(10.00m, "EUR", "12348", "BankD")); // Invalid: EUR offline
            } catch (Exception ex) {
                Console.WriteLine($"Error adding payment: {ex.Message}");
            }

            manager.ValidatePayments();
            manager.AuthorizePayments();
            manager.RecordOffline();
            manager.ProcessPayments();

            Console.WriteLine("\nHardcoded Demonstration Complete.\n");

            Console.WriteLine("Would you like to try the interactive UI? (y/n)");
            string response = Console.ReadLine()?.Trim().ToLower();
            if (response == "y") {
                Console.WriteLine("\nStarting Interactive UI...\n");
                UI ui = new UI();
                ui.ProcessInput();
            } else {
                Console.WriteLine("\nExiting program.");
            }
        }
    }
}