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
}