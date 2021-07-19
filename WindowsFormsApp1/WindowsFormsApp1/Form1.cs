using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
   
    public partial class Form1 : Form
    {
        public Employee employee= new Employee();
        public string gender ="M";
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clear();
            populateDataGridViewEmployee();
            //dataGridViewEmployee.DataSource =db;
        }
   




        private void radioButtonMale_Checked(object sender, EventArgs e)
        {
            gender = "M";
        }

        

        private void radioFemale_Checked(object sender, EventArgs e)
        {
            gender = "F";
        }

        private void radioOthers_Checked(object sender, EventArgs e)
        {
            gender = "O";
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            clear();

        }
         void clear()
        {
            textBoxFName.Text = textBoxLName.Text = textBoxMName.Text = textBoxDepartment.Text = textBoxAddress.Text = "";
            buttonDelete.Enabled = false;
            buttonDelete.BackColor = Color.LightGray;
            buttonSave.Text = "Save";
            radioButtonMale.Checked = true;
            employee.Id = 0;
        }
        private void Save_Click(object sender, EventArgs e)
        {
            employee.FName = textBoxFName.Text.Trim();
            employee.LName = textBoxLName.Text.Trim();
            employee.MName = textBoxMName.Text.Trim();
            employee.Address = textBoxAddress.Text.Trim();
            employee.Department = textBoxDepartment.Text.Trim();
            employee.Gender = gender;

            using (EmployeeDbEntities db = new EmployeeDbEntities())
            {
                if (employee.Id == 0)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            clear();
            populateDataGridViewEmployee();
            MessageBox.Show("Saved Sucessfully");
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("You surely want to delete this record", "Alert!!!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                employee.Id = Convert.ToInt32(dataGridViewEmployee.CurrentRow.Cells["Id"].Value);
                using (EmployeeDbEntities db = new EmployeeDbEntities())
                {
                    var tempdata = db.Employees.Find(employee.Id);
                    db.Employees.Attach(tempdata);
                    db.Employees.Remove(tempdata);
                    db.SaveChanges();
                    clear();
                    populateDataGridViewEmployee();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
            }
            
        }

        void populateDataGridViewEmployee()
        {
            using (EmployeeDbEntities db = new EmployeeDbEntities())
            {
                
                dataGridViewEmployee.DataSource = db.Employees.ToList<Employee>();

            }

        }

        private void DataCellGrid_DoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dataGridViewEmployee.CurrentRow.Index!=-1)
            {
                employee.Id = Convert.ToInt32(dataGridViewEmployee.CurrentRow.Cells["Id"].Value);
                using (EmployeeDbEntities db = new EmployeeDbEntities())
                {

                    employee = db.Employees.Find(employee.Id);
                    textBoxFName.Text = employee.FName;
                    textBoxMName.Text = employee.MName;
                    textBoxLName.Text = employee.LName;
                    switch(employee.Gender)
                    {
                        case "M":
                            radioButtonMale.Checked = true;
                            break;
                        case "F":
                            radioButtonFemale.Checked = true;
                            break;
                        case "O":
                            radioButtonOthers.Checked = true;
                            break;
                    }
                    textBoxAddress.Text = employee.Address;
                    textBoxDepartment.Text = employee.Department;
                    buttonSave.Text = "Update";
                    buttonDelete.Enabled = true;
                    buttonDelete.BackColor = Color.Red;

                }

            }
        }

        private void p(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
