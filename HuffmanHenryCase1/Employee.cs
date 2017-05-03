using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

/********************************************************
 * The team members for this group include:
 * Henry Huffman
 * Jaime Moreno
 * Martin Revilla
********************************************************/

namespace HuffmanHenryCase1
{
    public class Employee
    {
        public int employeeId;
        public string lastName;
        public string firstName;
        public double hourlyWage;
        public int hoursWorked;
        public double totalPayRoll;

        private static string[] firstPrefix = { "a", "b", "c"};
        private static string[] lastPrefix = { "e", "f", "g" };
        private static int lastId = 100;
        private static Random r = new System.Random(1);

        public static Employee makeEmployee()
        {
            Employee e = new Employee();
            e.employeeId = ++lastId;
            e.firstName = firstPrefix[r.Next(0, firstPrefix.GetUpperBound(0) + 1)] + lastId.ToString();
            e.lastName = lastPrefix[r.Next(0, lastPrefix.GetUpperBound(0) + 1)] + lastId.ToString();
            e.hourlyWage = Math.Round(r.NextDouble() * (180.00 - 9.00) + 9.00, 2);
            e.hoursWorked = r.Next(0, 100);
            e.totalPayRoll = (e.hourlyWage) * (e.hoursWorked);
            return e; 
        }
    }
}