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
    [Activity(Label = "CRUD")]
    public class CRUD : Activity
    {
        private int currentIndex = 0;
        TextView etEmployeeId;
        EditText etLastName;
        EditText etFirstName;
        EditText etHourlyWage;
        EditText etHoursWorked;
        TextView etTotalPayroll;
        TextView tvCRUDError;
        EditText etFindId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.Title = "Oh CRUD...";

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CRUD);

            etEmployeeId = (TextView)FindViewById(Resource.Id.etEmployeeId);
            etLastName = (EditText)FindViewById(Resource.Id.etLastName);
            etFirstName = (EditText)FindViewById(Resource.Id.etFirstName);
            etHourlyWage = (EditText)FindViewById(Resource.Id.etHourlyWage);
            etHoursWorked = (EditText)FindViewById(Resource.Id.etHoursWorked);
            etTotalPayroll = (TextView)FindViewById(Resource.Id.etTotalPayroll);
            tvCRUDError = (TextView)FindViewById(Resource.Id.tvCRUDError);
            etFindId = (EditText)FindViewById(Resource.Id.etFindId);


            Button btnPrevious = (Button)FindViewById(Resource.Id.btnPrevious);
            btnPrevious.Click += BtnPrevious_Click;

            Button btnNext = (Button)FindViewById(Resource.Id.btnNext);
            btnNext.Click += BtnNext_Click;

            Button btnFindId = (Button)FindViewById(Resource.Id.btnFindId);
            btnFindId.Click += BtnFindId_Click;

            Button btnCreate = (Button)FindViewById(Resource.Id.btnCreate);
            btnCreate.Click += BtnCreate_Click;

            Button btnUpdate = (Button)FindViewById(Resource.Id.btnUpdate);
            btnUpdate.Click += BtnUpdate_Click;

            Button btnDelete = (Button)FindViewById(Resource.Id.btnDelete);
            btnDelete.Click += BtnDelete_Click;

            Button btnBack = (Button)FindViewById(Resource.Id.btnBack);
            btnBack.Click += BtnBack_Click;

            if (MainActivity.EmployeeListSortedByLastName.Count == 0)
            {
                tvCRUDError.Text = "There are not any employees, try the create button.";
            }
            else
            {
                showCurrent(currentIndex);
            }

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Intent returnToOperations = new Intent(this, typeof(Operations));
            SetResult(Result.Ok, returnToOperations);
            Finish();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (MainActivity.EmployeeListSortedByLastName.Count == 0)
            {
                tvCRUDError.Text = "There are no more employees... you got them all.";
            }
            else
            {
                //at the beginning
                if (MainActivity.EmployeeListSortedByLastName.Count == 1)
                {
                    MainActivity.EmployeeListSortedByLastName.RemoveAt(currentIndex);
                    etEmployeeId.Text = "";
                    etFirstName.Text = "";
                    etLastName.Text = "";
                    etHourlyWage.Text = "";
                    etHoursWorked.Text = "";
                    etTotalPayroll.Text = "";
                }
                else if (MainActivity.EmployeeListSortedByLastName.Count - 1 == currentIndex)
                {
                    MainActivity.EmployeeListSortedByLastName.RemoveAt(currentIndex);
                    currentIndex--;
                    showCurrent(currentIndex);

                }
                else
                {
                    MainActivity.EmployeeListSortedByLastName.RemoveAt(currentIndex);
                    showCurrent(currentIndex);
                }
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            tvCRUDError.Text = "";
            if (MainActivity.EmployeeListSortedByLastName.Count == 0)
            {
                tvCRUDError.Text = "Looks like there is no one to update, try the create button";
            }

            else
            {
                //check values for validity

                int newId, newWorked;
                double newWage;
                bool validWage, validWorked, validId;

                validId = System.Int32.TryParse(etEmployeeId.Text, out newId);
                validWage = System.Double.TryParse(etHourlyWage.Text, out newWage);
                validWorked = System.Int32.TryParse(etHoursWorked.Text, out newWorked);
                newWage = Math.Round(newWage, 2);

                if (etFirstName.Text == "" || etLastName.Text == "")
                {
                    tvCRUDError.Text = "Please enter a valid first and last name";
                }
                else if (!validWage || newWage < 9.00 || newWage > 180.00)
                {
                    tvCRUDError.Text = "Please enter a wage between 9.00 and 180.00";
                }
                else if (!validWorked || newWorked < 0 || newWorked > 100)
                {
                    tvCRUDError.Text = "Hours worked must be an integer between 0 and 100";
                }
                else if (!validId)
                {
                    tvCRUDError.Text = "Please enter a valid integer for the Id";
                }
                else
                {
                    if (MainActivity.EmployeeListSortedByLastName.ElementAt(currentIndex).Key == newId)
                    {
                        MainActivity.EmployeeListSortedByLastName[newId].firstName = etFirstName.Text;
                        MainActivity.EmployeeListSortedByLastName[newId].lastName = etLastName.Text;
                        MainActivity.EmployeeListSortedByLastName[newId].hourlyWage = newWage;
                        MainActivity.EmployeeListSortedByLastName[newId].hoursWorked = newWorked;
                        MainActivity.EmployeeListSortedByLastName[newId].totalPayRoll = newWage * newWorked;
                        showCurrent(currentIndex);
                        return;
                    }
                    else if (MainActivity.EmployeeListSortedByLastName.ContainsKey(newId))
                    {
                        tvCRUDError.Text = "Another employee already has this id, try a different key";
                    }
                    else
                    {
                        MainActivity.EmployeeListSortedByLastName.RemoveAt(currentIndex);
                        Employee person = new Employee();
                        person.employeeId = newId;
                        person.firstName = etFirstName.Text;
                        person.lastName = etLastName.Text;
                        person.hourlyWage = newWage;
                        person.hoursWorked = newWorked;
                        person.totalPayRoll = (newWage) * (newWorked);
                        MainActivity.EmployeeListSortedByLastName.Add(person.employeeId, person);
                        currentIndex = MainActivity.EmployeeListSortedByLastName.IndexOfKey(person.employeeId);
                        showCurrent(currentIndex);
                    }

                }

            }
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            tvCRUDError.Text = "";

            if (MainActivity.EmployeeListSortedByLastName.Count == 0)
            {
                currentIndex = 0;
                Employee person = Employee.makeEmployee();
                MainActivity.EmployeeListSortedByLastName.Add(person.employeeId, person);
                MainActivity.numberOfEmployees++;
                showCurrent(currentIndex);
            }
            else
            {
                currentIndex = MainActivity.EmployeeListSortedByLastName.Count;
                Employee person = Employee.makeEmployee();
                MainActivity.EmployeeListSortedByLastName.Add(person.employeeId, person);
                MainActivity.numberOfEmployees++;
                showCurrent(currentIndex);
            }

        }

        private void BtnFindId_Click(object sender, EventArgs e)
        {
            tvCRUDError.Text = "";
            if (MainActivity.EmployeeListSortedByLastName.Count == 0)
            {
                tvCRUDError.Text = "This list is empty, try pressing the create button.";
            }
            else
            {
                //check to see if id input is valid
                int searchId;
                bool validId = System.Int32.TryParse(etFindId.Text, out searchId);
                if (!validId)
                {
                    tvCRUDError.Text = "Please enter an integer for the searchId.";
                }

                else
                {
                    //try to look for the id

                    if (MainActivity.EmployeeListSortedByLastName.ContainsKey(searchId))
                    {
                        //find the index
                        currentIndex = MainActivity.EmployeeListSortedByLastName.IndexOfKey(searchId);
                        showCurrent(currentIndex);
                    }
                    else
                    {
                        tvCRUDError.Text = "The Id you are searching with does not exist.";
                    }
                }

            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (MainActivity.EmployeeListSortedByLastName.Count != 0)
            {
                if (currentIndex >= MainActivity.EmployeeListSortedByLastName.Count - 1)
                {
                    tvCRUDError.Text = "STAHP! Already at the last index.";
                }
                else
                {
                    currentIndex++;
                    showCurrent(currentIndex);
                    tvCRUDError.Text = "";
                }
            }
            else
            {
                tvCRUDError.Text = "Man this sorted list is empty, try the create button.";
            }

        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (MainActivity.EmployeeListSortedByLastName.Count != 0)
            {
                if (currentIndex > 0)
                {
                    currentIndex--;
                    showCurrent(currentIndex);
                    tvCRUDError.Text = "";
                }
                else
                {
                    tvCRUDError.Text = "YOU SHALL NOT BACK! At first index already.";
                }
            }

            else
            {
                tvCRUDError.Text = "Man this sorted list is empty, try the create button.";
            }

        }

        private void showCurrent(int index)
        {
            Employee s;
            KeyValuePair<int, Employee> keyPair;
            keyPair = MainActivity.EmployeeListSortedByLastName.ElementAt(index);
            s = MainActivity.EmployeeListSortedByLastName[keyPair.Key];

            etEmployeeId.Text = s.employeeId.ToString();
            etFirstName.Text = s.firstName;
            etLastName.Text = s.lastName;
            etHourlyWage.Text = s.hourlyWage.ToString();
            etHoursWorked.Text = s.hoursWorked.ToString();
            etTotalPayroll.Text = s.totalPayRoll.ToString("0.00");
        }

    }
}