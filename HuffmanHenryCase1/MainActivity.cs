using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;

/********************************************************
 * The team members for this group include:
 * Henry Huffman
 * Jaime Moreno
 * Martin Revilla
********************************************************/

namespace HuffmanHenryCase1
{
    [Activity(Label = "HuffmanHenryCase1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public static SortedList<int, Employee> EmployeeListSortedByLastName = new SortedList<int, Employee>();
        public static int numberOfEmployees;
        protected override void OnCreate(Bundle bundle)
        {
            this.Title = "Initialization";

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main); 

            Button btnGenerate = (Button)FindViewById(Resource.Id.btnGenerate);
            btnGenerate.Click += BtnGenerate_Click;

            Button btnExit = (Button)FindViewById(Resource.Id.btnExit);
            btnExit.Click += BtnExit_Click;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void BtnGenerate_Click(object sender, System.EventArgs e)
        {
            EditText etEmployeePrompt = (EditText)FindViewById(Resource.Id.etEmployeePrompt);
            bool validNumber;
            validNumber = System.Int32.TryParse(etEmployeePrompt.Text, out numberOfEmployees);

            if (validNumber)
            {
                if(numberOfEmployees <= 0)
                {
                    TextView tvError = (TextView)FindViewById(Resource.Id.tvError);
                    tvError.Text = "A negative number? Not in my app. Try between 1 and 1000000";
                }
                else if(numberOfEmployees > 1000000)
                {
                    TextView tvError = (TextView)FindViewById(Resource.Id.tvError);
                    tvError.Text = "More than 1000000? Not in my app. Try between 1 and 1000000";
                }

                else
                {
                    //generate the sorted list
                    for(int i = 0; i < numberOfEmployees; i++)
                    {
                        Employee person = Employee.makeEmployee();
                        EmployeeListSortedByLastName.Add(person.employeeId, person);

                    }

                    //move to next activity
                    Intent toOperations = new Intent(this, typeof(Operations));
                    StartActivity(toOperations); 
                }
            }

            else
            {
                TextView tvError = (TextView)FindViewById(Resource.Id.tvError);
                tvError.Text = "Invalid input. Please use an integer.";
            }
        }
    }
}

