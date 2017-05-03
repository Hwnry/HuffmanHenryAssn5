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
    [Activity(Label = "Statistics")]
    public class Statistics : Activity
    {
        TextView tvMaxHours;
        TextView tvMinHours;
        TextView tvMaxWage;
        TextView tvMinWage;
        TextView tvAverageHours;
        TextView tvAverageWage;
        TextView tvTotalPayroll;
        TextView tvStats;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            this.Title = "Statistics";
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Statistics);

            tvMaxHours = (TextView)FindViewById(Resource.Id.tvMaxHours);
            tvMinHours = (TextView)FindViewById(Resource.Id.tvMinHours);
            tvMaxWage = (TextView)FindViewById(Resource.Id.tvMaxWage);
            tvMinWage = (TextView)FindViewById(Resource.Id.tvMinimumWage);
            tvAverageHours = (TextView)FindViewById(Resource.Id.tvAveragHours);
            tvAverageWage = (TextView)FindViewById(Resource.Id.tvAverageWage);
            tvTotalPayroll = (TextView)FindViewById(Resource.Id.tvTotal);
            tvStats = (TextView)FindViewById(Resource.Id.tvStats);

            Button btnBack = (Button)FindViewById(Resource.Id.btnOps);
            btnBack.Click += BtnBack_Click;

            tvStats.Text = ""; 
            if(MainActivity.EmployeeListSortedByLastName.Count == 0)
            {
                tvStats.Text = "List is empty";
            }
            else
            {
                //do calcs
                tvMaxHours.Text = tvMaxHours.Text + maxHours().ToString();
                tvMinHours.Text = tvMinHours.Text + minHours().ToString();
                tvMaxWage.Text = tvMaxWage.Text + maxWage().ToString();
                tvMinWage.Text = tvMinWage.Text + minWage().ToString();
                tvAverageWage.Text = tvAverageWage.Text + averageWage().ToString();
                tvAverageHours.Text = tvAverageHours.Text + averageHours().ToString();
                tvTotalPayroll.Text = tvTotalPayroll.Text + totalPayroll().ToString();
            }

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private int maxHours()
        {
            int max = 0;
            foreach(KeyValuePair<int, Employee> e in MainActivity.EmployeeListSortedByLastName)
            {
              if(max < MainActivity.EmployeeListSortedByLastName[e.Key].hoursWorked)
                {
                    max = MainActivity.EmployeeListSortedByLastName[e.Key].hoursWorked;
                }
            }
            return max;
        }

        private int minHours()
        {
            int min = 100;
            foreach(KeyValuePair<int,Employee> e in MainActivity.EmployeeListSortedByLastName)
            {
                if( min > MainActivity.EmployeeListSortedByLastName[e.Key].hoursWorked)
                {
                    min = MainActivity.EmployeeListSortedByLastName[e.Key].hoursWorked;
                }
            }
            return min;
        }

        private double maxWage()
        {
            double max = 0;
            foreach (KeyValuePair<int, Employee> e in MainActivity.EmployeeListSortedByLastName)
            {
                if (max < MainActivity.EmployeeListSortedByLastName[e.Key].hourlyWage)
                {
                    max = MainActivity.EmployeeListSortedByLastName[e.Key].hourlyWage;
                }
            }
            return max;
        }

        private double minWage()
        {
            double min = 180.00;
            foreach (KeyValuePair<int, Employee> e in MainActivity.EmployeeListSortedByLastName)
            {
                if (min > MainActivity.EmployeeListSortedByLastName[e.Key].hourlyWage)
                {
                    min = MainActivity.EmployeeListSortedByLastName[e.Key].hourlyWage;
                }
            }
            return min;
        }

        private double averageWage()
        {
            double sum = 0;
            double average = 0;
            foreach (KeyValuePair<int, Employee> e in MainActivity.EmployeeListSortedByLastName)
            {
                    sum += MainActivity.EmployeeListSortedByLastName[e.Key].hourlyWage; 
            }
            average = sum / MainActivity.EmployeeListSortedByLastName.Count;
            return Math.Round(average, 2);
        }

        private int averageHours()
        {
            int sum = 0;
            int average = 0;
            foreach (KeyValuePair<int, Employee> e in MainActivity.EmployeeListSortedByLastName)
            {
                sum += MainActivity.EmployeeListSortedByLastName[e.Key].hoursWorked;
            }
            average = sum / MainActivity.EmployeeListSortedByLastName.Count;
            return average;
        }

        private double totalPayroll()
        {
            double sum = 0;
            foreach (KeyValuePair<int, Employee> e in MainActivity.EmployeeListSortedByLastName)
            {
                sum += MainActivity.EmployeeListSortedByLastName[e.Key].totalPayRoll;
            }
            
            return sum;
        }
    }
}