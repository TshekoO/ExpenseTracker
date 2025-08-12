using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ExpenseTracker
{
    //represents a single expense record 

    public class Expense
    {
        public decimal Amount { get; set; } // Money spent
        public string Category { get; set; }// Expense category 
        public string Description { get; set; } // Optional note
        public DateTime Date { get; set; } // Date expense occurred

        // Override ToString() to display expenses in a nice format
        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd} | {Category,-15} | R{Amount,-8} | {Description}";
        }
    }
    public class Program
    {
        // File where expenses will be saved
        static string FilePath = "expenses.json";

        // List to hold all expenses records 
        static List<Expenses> expenses = new List<Expenses>();

        static void Main()
        {
            //Load any previously saved expenses 
            LoadExpenses();

            // Main menu loop
            while (true)
            {
                Console.Clear();
                Console.WriteLine("== Expense Tracker ===");
                Console.WriteLine("1. Add Expenses");
                Console.WriteLine("2.View All Expenses");
                Console.WriteLine("3.Filter by Category");
                Console.WriteLine("4.Filter by Date Range");
                Console.WriteLine("5. Show Total Spending");
                Console.WriteLine("6. Export to CSV");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddExpense(); break;
                    case "2": ViewExpenses(expenses); break;
                    case "3": FilterByCategory(); break;
                    case "4": FilterByDateRange(); break;
                    case "5": ShowTotalSpending(); break;
                    case "6": ExportToCsv(); break;
                    case "7":
                        SaveExpenses(); // Save before exiting 
                        Console.WriteLine("Data saved. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        // Add a new expense
        static void AddExpense()
        {
            Console.write("Enter amount: R");

            // Validate numeric input 
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount. Press any key to return.");
                Console.ReadKey();
                return;
            }
            Console.Write("Enter category: ");
            string category = Console.ReadLine();

            Console.Write("Enter description: ");
            string description = Console.ReadLine();
            // Add expense to list
            expenses.Add(new Expense
            {
                Amount = amount,
                Category = category,
                Description = description,
                Date = DateTime.Now

            }
            );
            Console.WriteLine("Expense added! Press any key to continue...");
            Console.ReadKey();
        }
        // Display a list of expenses
        static void ViewExpenses(List<Expenses> list)
        {
            Console.Clear();
            Console.WriteLine("=== Expenses ===");

            if (!list.Any())
            {
                Console.WriteLine("No Expenses found.");
            }
            else
            {
                foreach (var expense in list)
                {
                    Console.WriteLine(expenses);

                }
            }
            Console.WriteLine("\nPress any key to return ...");
            Console.ReadKey();

        }
        //Show expenses for a specific category 
        static void FilterByCategory()
        {
            Console.Write("Enter category to filter: ");
            string category = Console.ReadLine();
            // Match ignoring case 
            var filtered = expenses
           .Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            ViewExpenses(filtered);
        }
        // Show expenses in a date range
        static void FilterByDateRange()
        {
            Console.Write("Enter start date (yyy-mm-dd):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime start))
            {
                Console.WriteLine("Invalid date. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter end date (yyyy-mm-dd):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime end))
            {
                Console.WriteLine("Invalid date. Press any key to return.");
                Console.ReadKey();
                return;
            }
            // Filter using LINQ
            var filtered = expenses
            .Where(e => e.Date.Date >= start.Date && e.Date.Date <= end.Date)
            .ToList();

            ViewExpenses(filtered);
        }
        // Calculate and show total spending
        static void ShowTotalSpending()
        {
            decimal total = expenses.Sum(e => e.Amount);
            Console.WriteLine($"\nTotal Spending: R{total}");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }
        // Export all expenses to a CSV file
        static void ExportToCsv()
        {
            string csvPath = "expenses.csv";
            var csv = new StringBuilder();

            // add CSV header
            csv.AppendLine("Date,Category,Amount,Description");

            // Add each expense as a CSV row
            foreach (var e in expenses)
            {
                csv.AppendLine($"{e.Date:yyyy-MM-dd},{e.Category},{e.Amount},{e.Description}");

            }
            File.WriteAllText(csvPath, csv.ToString());

            Console.WriteLine($"Exported to {csvPath}");
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }
        // Save expenses to JSON file
        static void SaveExpenses()
        {
            string json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);

        }
        // Load expenses from a JSON file
        static void LoadExpenses()
        {
            if (File.Exists(filePath))
            {
                string json = FilePath.ReadAllText(FilePath);
                expenses = JsonSerializer.Deserialize<List<Expense>>(json);
            }
        }
    }
}