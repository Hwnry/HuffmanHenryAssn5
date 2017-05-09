using System;
using System.Collections.Generic;
using System.IO;
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
    [Activity(Label = "Operations")]
    public class Operations : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.Title = "Operations";

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Operations);

            Button btnCRUD = (Button)FindViewById(Resource.Id.btnCRUD);
            btnCRUD.Click += BtnCRUD_Click;

            Button btnStatistics = (Button)FindViewById(Resource.Id.btnStatistics);
            btnStatistics.Click += BtnStatistics_Click;

            Button btnInitial = (Button)FindViewById(Resource.Id.btnInitial);
            btnInitial.Click += BtnInitial_Click;

            Button btnExport = (Button) FindViewById(Resource.Id.btnExport);
            btnExport.Click += BtnExport_Click;

            Button btnImport = (Button) FindViewById(Resource.Id.btnImport);
            btnImport.Click += BtnImport_Click;

            Button btnSerialize = (Button) FindViewById(Resource.Id.btnSerialize);
            btnSerialize.Click += BtnSerialize_Click;

            Button btnDeserialize = (Button) FindViewById(Resource.Id.btnDeserialize);
            btnDeserialize.Click += BtnDeserialize_Click;
        }

        private void BtnDeserialize_Click(object sender, EventArgs e)
        {
            try
            {
                MainActivity.EmployeeListSortedByLastName.Clear();

                List<Employee> temp = new List<Employee>();

                string specialApplicationDataDirectory =
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                string fileName = Path.Combine(specialApplicationDataDirectory, "SerializedData.xml");

                System.Xml.Serialization.XmlSerializer serialCurrent = new
                    System.Xml.Serialization.XmlSerializer(typeof(List<Employee>));
                StreamReader sr = new StreamReader(fileName);

                temp = (List<Employee>) serialCurrent.Deserialize(sr);
                sr.Close();

                foreach (Employee person in temp)
                {
                    MainActivity.EmployeeListSortedByLastName.Add(person.employeeId, person);
                }
                Toast.MakeText(this, "Deserialized List.", ToastLength.Short).Show();
            }

            catch
            {
                Toast.MakeText(this, "Error Deserializing.", ToastLength.Short).Show();
            }
        }

        private void BtnSerialize_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the path and file name as before.
                string specialApplicationDataDirectory =
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                string fileName = Path.Combine(specialApplicationDataDirectory, "SerializedData.xml");

                System.Xml.Serialization.XmlSerializer serialCurrent = new
                    System.Xml.Serialization.XmlSerializer(typeof(List<Employee>));
                StreamWriter sw = new StreamWriter(fileName);
                serialCurrent.Serialize(sw, MainActivity.EmployeeListSortedByLastName.Values.ToList());
                sw.Close();

                Toast.MakeText(this, "Serialized List.", ToastLength.Short).Show();
            }
            catch
            {
                Toast.MakeText(this, "Error Serializing List.", ToastLength.Short).Show();
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            try
            {
                //clear the list 
                MainActivity.EmployeeListSortedByLastName.Clear();

                // Get the path as before.
                string specialApplicationDataDirectory =
                    System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.ApplicationData);
                string fileName = Path.Combine(specialApplicationDataDirectory, "EmployeeExport.csv");

                // Array to store the parsed fields of the current record.
                string[] fields;

                // The delimiter character must be stored as an array even though there
                // is only only one record. 
                char[] delimiter = {','};
                string currentRecord;
                StreamReader srCurrent = new StreamReader(fileName);

                // Priming read. The file might be empty.
                currentRecord = srCurrent.ReadLine();

                // Loop to process records.
                while (currentRecord != null)
                {
                    // Split the record into its component fields.
                    fields = currentRecord.Split(delimiter);

                    Employee currentEmployee = new Employee();

                    bool noErrors = true;
                    noErrors = System.Int32.TryParse(fields[0], out currentEmployee.employeeId);
                    currentEmployee.firstName = fields[1];
                    currentEmployee.lastName = fields[2];
                    noErrors = noErrors && System.Double.TryParse(fields[3], out currentEmployee.hourlyWage);
                    noErrors = noErrors && System.Int32.TryParse(fields[4], out currentEmployee.hoursWorked);
                    noErrors = noErrors && System.Double.TryParse(fields[5], out currentEmployee.totalPayRoll);

                    if (!noErrors)
                    {
                        Toast.MakeText(this, "Errors Importing.", ToastLength.Short).Show();
                        break;
                    }

                    //    // Add the current record to the list.
                    MainActivity.EmployeeListSortedByLastName.Add(currentEmployee.employeeId, currentEmployee);

                    // Try and read the next record.
                    currentRecord = srCurrent.ReadLine();
                }

                // Close the file.
                srCurrent.Close();

                Toast.MakeText(this, "Employees Imported", ToastLength.Short).Show();
            }
            catch
            {
                Toast.MakeText(this, "Error Importing Employees.", ToastLength.Short).Show();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the path.
                string specialApplicationDataDirectory =
                    System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.ApplicationData);
                string fileName = Path.Combine(specialApplicationDataDirectory, "EmployeeExport.csv");

                // Create the writer.
                StreamWriter swCurrent = new StreamWriter(fileName);

                //// Enumerate the list and write each record. Write a comma
                //// between each field. Write a carriage return at the end
                //// of line.
                foreach (KeyValuePair<int, Employee> eCurrent in MainActivity.EmployeeListSortedByLastName)
                {

                    swCurrent.Write(eCurrent.Value.employeeId.ToString());
                    swCurrent.Write(",");
                    swCurrent.Write(eCurrent.Value.firstName.ToString());
                    swCurrent.Write(",");
                    swCurrent.Write(eCurrent.Value.lastName.ToString());
                    swCurrent.Write(",");
                    swCurrent.Write(eCurrent.Value.hourlyWage.ToString());
                    swCurrent.Write(",");
                    swCurrent.Write(eCurrent.Value.hoursWorked.ToString());
                    swCurrent.Write(",");
                    swCurrent.Write(eCurrent.Value.totalPayRoll.ToString());

                    swCurrent.WriteLine();
                }

                // Close the file.
                swCurrent.Close();

                Toast.MakeText(this, "Exported Employee List.", ToastLength.Short).Show();
            }
            catch
            {
                Toast.MakeText(this, "Error Exporting Employee List.", ToastLength.Short).Show();
            }
        }

        private void BtnInitial_Click(object sender, EventArgs e)
        {
            MainActivity.EmployeeListSortedByLastName.Clear();
            Finish();
        }

        private void BtnStatistics_Click(object sender, EventArgs e)
        {
            Intent toStatistics = new Intent(this, typeof(Statistics));
            StartActivity(toStatistics);
        }

        private void BtnCRUD_Click(object sender, EventArgs e)
        {
            Intent toCRUD = new Intent(this, typeof(CRUD));
            StartActivityForResult(toCRUD, 0);
        }
    }

}

