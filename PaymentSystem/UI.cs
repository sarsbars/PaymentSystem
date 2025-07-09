using System;
using System.Collections.Generic;

namespace PaymentSystem {
    // This class handles the UI menu and user choices for the payment system
    internal class UI {
        // Fields
        private static readonly List<string> ValidCommands = new List<string> { "1", "2", "3", "4", "5", "6" };
        private static readonly List<string> ValidCurrencies = new List<string> { "CAD", "USD", "EUR" };
        private string selectedCurrency;
        private PaymentManager paymentManager;

        // Constructor
        public UI() {
            selectedCurrency = "CAD";
            paymentManager = new PaymentManager();
        }

        // Methods
        public void DisplayMenu() {
            Console.WriteLine($"Welcome to the Payment System! Current currency: {selectedCurrency}\n");
            Console.WriteLine("Please select from the following options:\n");
            Console.WriteLine("1. Change Currency");
            Console.WriteLine("2. Make Credit Card Payment");
            Console.WriteLine("3. Make Bitcoin Payment");
            Console.WriteLine("4. Make Cash Payment");
            Console.WriteLine("5. Make Cheque Payment");
            Console.WriteLine("6. Exit");
        }

        public void ProcessInput() {
            while (true) {
                DisplayMenu();
                Console.Write("\nEnter your choice: ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input)) {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                if (!ValidCommands.Contains(input)) {
                    Console.WriteLine("Invalid command. Try 1, 2, 3, 4, 5, or 6.");
                    continue;
                }

                switch (input) {
                    case "1":
                        ChangeCurrency();
                        break;
                    case "2":
                        ProcessCreditCardPayment();
                        break;
                    case "3":
                        ProcessBitcoinPayment();
                        break;
                    case "4":
                        ProcessCashPayment();
                        break;
                    case "5":
                        ProcessChequePayment();
                        break;
                    case "6":
                        paymentManager.ValidatePayments();
                        paymentManager.AuthorizePayments();
                        paymentManager.RecordOffline();
                        paymentManager.ProcessPayments();
                        Console.WriteLine("Exiting program.");
                        return;
                }
            }
        }
        private void ChangeCurrency() {
            Console.WriteLine("\nAvailable currencies: CAD, USD, EUR");
            Console.Write("Enter the currency you want to use: ");
            string currency = Console.ReadLine()?.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(currency)) {
                Console.WriteLine("Error: Currency cannot be empty.");
                return;
            }

            if (!ValidCurrencies.Contains(currency)) {
                Console.WriteLine("Error: Invalid currency. Please choose CAD, USD, or EUR.");
                return;
            }

            selectedCurrency = currency;
            Console.WriteLine($"Currency changed to {selectedCurrency}.");
        }

        private void ProcessCreditCardPayment() {
            try {
                Console.Write("Enter amount: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount)) {
                    Console.WriteLine("Error: Invalid amount format.");
                    return;
                }
                Console.Write("Enter card number: ");
                string cardNumber = Console.ReadLine()?.Trim();
                Console.Write("Enter expiry date (MM/YY): ");
                string expiryDate = Console.ReadLine()?.Trim();
                Console.Write("Enter CVV: ");
                string cvv = Console.ReadLine()?.Trim();

                var payment = new CreditCardPayment(amount, selectedCurrency, "Stripe", cardNumber, expiryDate, cvv);
                paymentManager.AddPayment(payment);
                Console.WriteLine("Credit Card Payment added.");
                payment.LogPayment(); 
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private void ProcessBitcoinPayment() {
            try {
                Console.Write("Enter amount: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount)) {
                    Console.WriteLine("Error: Invalid amount format.");
                    return;
                }
                Console.Write("Enter wallet ID: ");
                string walletId = Console.ReadLine()?.Trim();

                var payment = new BitcoinPayment(amount, selectedCurrency, "Coinbase", walletId);
                paymentManager.AddPayment(payment);
                Console.WriteLine("Bitcoin Payment added.");
                payment.LogPayment();
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private void ProcessCashPayment() {
            try {
                Console.Write("Enter amount: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount)) {
                    Console.WriteLine("Error: Invalid amount format.");
                    return;
                }

                var payment = new CashPayment(amount, selectedCurrency);
                paymentManager.AddPayment(payment);
                Console.WriteLine("Cash Payment added.");
                payment.LogPayment();
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private void ProcessChequePayment() {
            try {
                Console.Write("Enter amount: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount)) {
                    Console.WriteLine("Error: Invalid amount format.");
                    return;
                }
                Console.Write("Enter cheque number: ");
                string chequeNumber = Console.ReadLine()?.Trim();
                Console.Write("Enter bank name: ");
                string bankName = Console.ReadLine()?.Trim();

                var payment = new ChequePayment(amount, selectedCurrency, chequeNumber, bankName);
                paymentManager.AddPayment(payment);
                Console.WriteLine("Cheque Payment added.");
                payment.LogPayment();
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public string GetSelectedCurrency() {
            return selectedCurrency;
        }
    }
}