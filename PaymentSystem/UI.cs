using System;
using System.Collections.Generic;

namespace PaymentSystem {
    // This class handles the UI menu and user choices for the payment system
    internal class UI {
        // Fields
        private static readonly List<string> ValidCommands = new List<string> { "1", "2", "3", "4", "5", "6" };
        private static readonly List<string> ValidCurrencies = new List<string> { "CAD", "USD", "EUR" };
        private string selectedCurrency;

        // Constructor
        public UI() {
            selectedCurrency = "CAD";
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
                        Console.WriteLine("Credit Card Payment selected ");
                        break;
                    case "3":
                        Console.WriteLine("Bitcoin Payment selected ");
                        break;
                    case "4":
                        Console.WriteLine("Cash Payment selected");
                        break;
                    case "5":
                        Console.WriteLine("Cheque Payment selected "); 
                        break;
                    case "6":
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

        public string GetSelectedCurrency() {
            return selectedCurrency;
        }
    }
}