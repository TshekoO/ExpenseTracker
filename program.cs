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
            
        }
    }
}