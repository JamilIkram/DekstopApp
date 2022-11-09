using EmployeeManagementSystem.Data_set;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class Form1 : Form
    {
        //SqlDataAdapter adapt = null;
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("data source=DESKTOP-S4UTGJ3; database = EmployeeManagementSystem; integrated security = true");
        string imagelocation = "";
        SqlCommand cmd;

        private void Reset()
        {
            txtDID.Text = "";
            txtDName.Text = "";
            txtDGID.Text = "";
            txtDGName.Text = "";
            txtDGSalary.Text = "";
            txtEID.Text = "";
            txtEName.Text = "";
            txtEAddress.Text = "";
            txtEContact.Text = "";
            comboBoxDP1.Text = "";
            comboBoxDg2.Text = "";

            viewCategory();
            viewPosition();
            viewInfo();

            txtDName.Show();
            lblDName.Show();           
            txtDGName.Show();
            lblDGName.Show();
            txtDGSalary.Show();
            lblDGSalary.Show();
            txtEName.Show();
            txtEAddress.Show();
            txtEContact.Show();
            lblEName.Show();
            lblEAddress.Show();
            lblEContact.Show();
        }

        private void btnDSave_Click(object sender, EventArgs e)
        {
            {
                if (txtDID.Text=="")
                {
                    txtDID.BackColor = Color.LightCoral;
                    MessageBox.Show
                        ("Depertment ID is required","Validation Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                if(txtDName.Text=="")
                {
                    txtDName.BackColor = Color.LightCoral;
                    MessageBox.Show("Name of Depertment is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                con.Open();
                string query = "INSERT INTO Depertment(Dep_ID,Dep_Name) VALUES('" + txtDID.Text + "','" + txtDName.Text + "')";
                SqlDataAdapter SDA = new SqlDataAdapter(query, con);
                SDA.SelectCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Inserted sucessfully", "Record Added",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
        }

        private void btnDUpdate_Click(object sender, EventArgs e)
        {
            if(txtDID.Text=="")
            {
                MessageBox.Show("Please Double click on a row for updation", "Validation Erron", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(MessageBox.Show("Are you sure to update'"+ txtDName.Text+"'","Update confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                con.Open();
                string query = "UPDATE Depertment SET Dep_Name='" + txtDName.Text + "' WHERE Dep_ID='" + txtDID.Text + "'";
                SqlDataAdapter SDA = new SqlDataAdapter(query, con);

                SDA.SelectCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updation successfully", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            viewCategory();
        }

        public void viewCategory()
        {
            con.Open();
            string query = "SELECT * FROM Depertment";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            SDA.Fill(dt);
            dataGridViewDep.DataSource = dt;
            con.Close();
        }

        private void btnDDelete_Click(object sender, EventArgs e)
        {
            if (txtDID.Text == "")
            {
                MessageBox.Show("Please Double click on a row for deletion", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Are you sure to delete '" + txtDName.Text + "'", "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                string query = "DELETE FROM Depertment WHERE Dep_ID='" + txtDID.Text + "'";
                SqlDataAdapter SDA = new SqlDataAdapter(query, con);
                SDA.SelectCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted successfully", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            viewCategory();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'employeeManagementSystemDataSet18.Designation' table. You can move, or remove it, as needed.
            this.designationTableAdapter4.Fill(this.employeeManagementSystemDataSet18.Designation);
            // TODO: This line of code loads data into the 'employeeManagementSystemDataSet15.Depertment' table. You can move, or remove it, as needed.
            this.depertmentTableAdapter3.Fill(this.employeeManagementSystemDataSet15.Depertment);
            // TODO: This line of code loads data into the 'employeeManagementSystemDataSet14.EmployeeInfo' table. You can move, or remove it, as needed.
            this.employeeInfoTableAdapter1.Fill(this.employeeManagementSystemDataSet14.EmployeeInfo);
            // TODO: This line of code loads data into the 'employeeManagementSystemDataSet13.Designation' table. You can move, or remove it, as needed.
            this.designationTableAdapter3.Fill(this.employeeManagementSystemDataSet13.Designation);
            // TODO: This line of code loads data into the 'employeeManagementSystemDataSet12.Depertment' table. You can move, or remove it, as needed.
            this.depertmentTableAdapter2.Fill(this.employeeManagementSystemDataSet12.Depertment);
            // TODO: This line of code loads data into the 'employeeManagementSystemDataSet10.Designation' table. You can move, or remove it, as needed.
            //this.designationTableAdapter2.Fill(this.employeeManagementSystemDataSet10.Designation);
            // TODO: This line of code loads data into the 'employeeManagementSystemDataSet6.Designation' table. You can move, or remove it, as needed.
            //this.designationTableAdapter1.Fill(this.employeeManagementSystemDataSet6.Designation);
            // TODO: This line of code loads data into the 'employeeManagementSystemDataSet3.Depertment' table. You can move, or remove it, as needed.
            //this.depertmentTableAdapter1.Fill(this.employeeManagementSystemDataSet3.Depertment);
            // TODO: This line of code loads data into the 'employeeManagementSystemDataSet2.EmployeeInfo' table. You can move, or remove it, as needed.
            //this.employeeInfoTableAdapter.Fill(this.employeeManagementSystemDataSet2.EmployeeInfo);
            // TODO: This line of code loads data into the 'employeeManagementSystemDataSet1.Designation' table. You can move, or remove it, as needed.
            //this.designationTableAdapter.Fill(this.employeeManagementSystemDataSet1.Designation);
            // TODO: This line of code loads data into the 'employeeManagementSystemDataSet.Depertment' table. You can move, or remove it, as needed.
            //this.depertmentTableAdapter.Fill(this.employeeManagementSystemDataSet.Depertment);



        }

        private void btnDGSave_Click(object sender, EventArgs e)
        {
            {
                if (txtDGID.Text == "")
                {
                    txtDGID.BackColor = Color.LightCyan;
                    MessageBox.Show("Designation Id is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtDGName.Text == "")
                {
                    txtDGName.BackColor = Color.LightGoldenrodYellow;
                    MessageBox.Show("Name of Designation is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtDGSalary.Text == "")
                {
                    txtDGSalary.BackColor = Color.LightSteelBlue;
                    MessageBox.Show("Salary Amount is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                con.Open();
                string query = "INSERT INTO Designation(Des_ID, Des_Name,Salary) VALUES('" + txtDGID.Text + "','" + txtDGName.Text + "','" + txtDGSalary.Text + "')";
                SqlDataAdapter SDA = new SqlDataAdapter(query, con);
                SDA.SelectCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Inserted successfully", "Record Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
        }

        private void btnDGUpdate_Click(object sender, EventArgs e)
        {
            if (txtDGID.Text == "")
            {
                MessageBox.Show("Please Double click on a row for updation", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Are you sure to update '" + txtDGName.Text+"'", "Update confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                string query = "UPDATE Designation SET Des_Name='" + txtDGName.Text+ "'  WHERE Des_ID = '" + txtDGID.Text +"'";
                
                SqlDataAdapter SDA = new SqlDataAdapter(query, con);
                SDA.SelectCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updation successfully", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            viewPosition();
        }

        private void lblDGDelete_Click(object sender, EventArgs e)
        {
            if (txtDGID.Text == "")
            {
                MessageBox.Show("Please Double click on a row for deletion", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Are you sure to delete '" + txtDGName.Text + "',", "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                string query = "DELETE FROM Designation WHERE Des_ID='" + txtDGID.Text + "'";
                SqlDataAdapter SDA = new SqlDataAdapter(query, con);
                SDA.SelectCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted successfully", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            viewPosition();
        }

        public void viewPosition()
        {
            con.Open();
            string query = "select*from Designation";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            SDA.Fill(dt);
            dataGridViewDes.DataSource = dt;
            con.Close();
        }
        string Gender;
        private string imgLocation;

        private void btnESave_Click(object sender, EventArgs e)
        {
            if (radiobtnMale.Checked == true)
            {
                Gender = "Male";
            }
            else
            {
                Gender = "Female";
            }
            if (txtEID.Text == "")
            {
                txtEID.BackColor = Color.LightCyan;
                MessageBox.Show(" Id is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtEName.Text == "")
            {
                txtEName.BackColor = Color.LightCyan;
                MessageBox.Show("Name is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtEAddress.Text == "")
            {
                txtEAddress.BackColor = Color.LightCyan;
                MessageBox.Show("Address is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (txtEContact.Text == "")
            //{
            //    txtEContact.BackColor = Color.LightCyan;
            //    MessageBox.Show("Contact number is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            con.Open();
            string query = "INSERT INTO EmployeeInfo(Emp_ID, Emp_Name, Emp_Address,Emp_Contact, Dep_ID, Des_ID, Emp_Gender, Emp_DOB) VALUES('" + txtEID.Text + "','" + txtEName.Text + "','" + txtEAddress.Text + "','" + txtEContact.Text + "','" + comboBoxDP1.SelectedValue.ToString() + "','" + comboBoxDg2.SelectedValue.ToString() + "','" + Gender + "','" + dateTimePicker1.Value + "')";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Inserted successfully", "Record Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Reset();
        }

        private void btnEUpdate_Click(object sender, EventArgs e)
        {
            if (txtEID.Text == "")
            {
                MessageBox.Show("Please Double click on a row for updation", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Are you sure to update '" + txtEName.Text + "'", "Update confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                string query = "UPDATE EmployeeInfo SET Emp_Name='" + txtEName.Text + "' WHERE Emp_ID='" + txtEID.Text + "'";
                SqlDataAdapter SDA = new SqlDataAdapter(query, con);
                SDA.SelectCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updation successfully", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }       
            viewInfo();
        }

        public void viewInfo()
        {
            con.Open();
            string query = "SELECT * FROM EmployeeInfo";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            SDA.Fill(dt);
            dataGridViewE.DataSource = dt;
            con.Close();
        }

        private void btnEDelete_Click(object sender, EventArgs e)
        {
            if (txtEID.Text == "")
            {
                MessageBox.Show("Please Double click on a row for deletion", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Are you sure to delete '" + txtEName.Text + "'", "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                string query = "DELETE FROM EmployeeInfo WHERE Emp_ID='" + txtEID.Text + "'";
                SqlDataAdapter SDA = new SqlDataAdapter(query, con);
                SDA.SelectCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted successfully", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            viewInfo();
        }

        private void dataGridViewDep_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtDID.Text = dataGridViewDep.SelectedRows[0].Cells[0].Value.ToString();
            txtDName.Text = dataGridViewDep.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void dataGridViewDes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtDGID.Text = dataGridViewDes.SelectedRows[0].Cells[0].Value.ToString();
            txtDGName.Text = dataGridViewDes.SelectedRows[0].Cells[1].Value.ToString();
            txtDGSalary.Text = dataGridViewDes.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void dataGridViewE_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtEID.Text = dataGridViewE.SelectedRows[0].Cells[0].Value.ToString();
            txtEName.Text = dataGridViewE.SelectedRows[0].Cells[1].Value.ToString();
            txtEAddress.Text = dataGridViewE.SelectedRows[0].Cells[2].Value.ToString();
            txtEContact.Text = dataGridViewE.SelectedRows[0].Cells[3].Value.ToString();
            comboBoxDP1.Text = dataGridViewE.SelectedRows[0].Cells[4].Value.ToString();
            comboBoxDg2.Text = dataGridViewE.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void btnbrowsPhoto_Click(object sender, EventArgs e)
        {
            SelectFile();
        }
        private void SelectFile()
        {
            open.Filter = "JPG (*.JPG)|*.jpg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                using (var img = new Bitmap(open.FileName))
                {
                    pictureBox1.Image = new Bitmap(img);
                }
                // image file path
                newFilePath = open.FileName;
                isNewFile = true;
            }
        }
        string newFilePath = string.Empty;
        string oldFilePath = string.Empty;
        bool isNewFile = true;
        OpenFileDialog open = new OpenFileDialog();

        public string EmployeeID { get; private set; }

        private void crystalReportViewer1_Load_1(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            con.Open();
            string q = "select * From EmployeeInfo;";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adap.Fill(ds, "EmployeeInfo");
            CrystalReport1 cr1 = new CrystalReport1();
            cr1.SetDataSource(ds);
            crystalReportViewer1.ReportSource = cr1;
            con.Close();
            crystalReportViewer1.Refresh();
            con.Close();
        }

        //private void AddButtonColumn()
        //{
        //    DataGridViewButtonColumn btnReport = new DataGridViewButtonColumn();
        //    btnReport.HeaderText = "#";
        //    btnReport.Text = "Report";
        //    btnReport.Name = "btnReport";
        //    btnReport.UseColumnTextForButtonValue = true;
        //    dataGridView3.Columns.Add(btnReport);

        //    DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
        //    btnEdit.HeaderText = "#";
        //    btnEdit.Text = "Edit";
        //    btnEdit.Name = "btnEdit";
        //    btnEdit.UseColumnTextForButtonValue = true;
        //    dataGridView3.Columns.Add(btnEdit);

        //    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
        //    btnDelete.HeaderText = "#";
        //    btnDelete.Text = "Delete";
        //    btnDelete.Name = "btnDelete";
        //    btnDelete.UseColumnTextForButtonValue = true;
        //    dataGridView3.Columns.Add(btnDelete);
        //}
        //private string UpdateFile()
        //{
        //    string strFilePath = string.Empty;
        //    if (isNewFile)
        //    {
        //        strFilePath = "\\images\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
        //        //pictureBox1.Image.Save(Application.StartupPath + strFilePath);
        //        File.Copy(newFilePath, Application.StartupPath + strFilePath);

        //        //remove old file
        //        RemoveFile(Application.StartupPath + oldFilePath);
        //    }
        //    else
        //    {
        //        strFilePath = oldFilePath;
        //    }

        //    return strFilePath;
        //}

        //private void RemoveFile(string filePath)
        //{
        //    if (File.Exists(filePath))
        //    {
        //        if (!filePath.Contains("default"))
        //        {
        //            File.Delete(filePath);
        //        }
        //        pictureBox1.Image = null;
        //    }
        //}
        //private void AddNew()
        //{
        //    // save image
        //    string strFilePath = AddFile();
        //    string query = @"INSERT INTO [dbo].[Employee]
        //        ([EmployeeName]
        //       ,[JoinDate]
        //       ,[Gender]
        //       ,[FilePath]
        //       ,[DesignationID])
        //        VALUES
        //       (@EmployeeName
        //       ,@JoinDate
        //       ,@Gender
        //       ,@FilePath
        //       ,@DesignationID)";
        //    cmd = new SqlCommand(query, con);
        //    con.Open();
        //    cmd.Parameters.AddWithValue("@EmployeeName", textBox1.Text.Trim());
        //    cmd.Parameters.AddWithValue("@JoinDate", pickDoB.Value);
        //    cmd.Parameters.AddWithValue("@Gender", radioFemale.Checked == true ? "Female" : "Male");
        //    cmd.Parameters.AddWithValue("@FilePath", strFilePath);
        //    cmd.Parameters.AddWithValue("@DesignationID", cmbDesig.SelectedValue.ToString());
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //    MessageBox.Show("Data added successfully.");
        //}
        //private void Updates()
        //{
        //    // save image
        //    string strFilePath = UpdateFile();

        //    cmd = new SqlCommand(@"UPDATE [Employee]
        //       SET [EmployeeName] = @EmployeeName
        //          ,[JoinDate] = @JoinDate
        //          ,[Gender] = @Gender
        //          ,[FilePath] = @FilePath
        //          ,[DesignationID] = @DesignationID
        //     WHERE [EmployeeID] = @EmployeeID", con);
        //    con.Open();
        //    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
        //    cmd.Parameters.AddWithValue("@EmployeeName", textBox1.Text.Trim());
        //    cmd.Parameters.AddWithValue("@JoinDate", pickDoB.Value);
        //    cmd.Parameters.AddWithValue("@Gender", radioFemale.Checked == true ? "Female" : "Male");
        //    cmd.Parameters.AddWithValue("@FilePath", strFilePath);
        //    cmd.Parameters.AddWithValue("@DesignationID", cmbDesig.SelectedValue.ToString());
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //    MessageBox.Show("Data updated successfully.");
        //}
        //private void Edit()
        //{
        //    con.Open();
        //    DataTable dt = new DataTable();
        //    string query = @"SELECT [EmployeeID]
        //          ,[EmployeeName]
        //          ,[JoinDate]
        //          ,[Gender]
        //          ,[FilePath]
        //          ,[DesignationID]
        //      FROM [Employee] WHERE [EmployeeID] = " + EmployeeID;
        //    adapt = new SqlDataAdapter(query, con);
        //    adapt.Fill(dt);
        //    con.Close();

        //    if (dt.Rows.Count > 0)
        //    {
        //        btnAdd.Text = "Update";

        //        EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"].ToString());
        //        textBox1.Text = dt.Rows[0]["EmployeeName"].ToString();
        //        pickDoB.Value = Convert.ToDateTime(dt.Rows[0]["JoinDate"].ToString());
        //        radioFemale.Checked = dt.Rows[0]["Gender"].ToString() == "Female" ? true : false;
        //        radioMale.Checked = dt.Rows[0]["Gender"].ToString() == "Female" ? false : true;
        //        // set image
        //        if (dt.Rows[0]["FilePath"].ToString() != null)
        //        {
        //            if (File.Exists(Application.StartupPath + dt.Rows[0]["FilePath"].ToString()))
        //            {
        //                using (var img = new Bitmap(Application.StartupPath + dt.Rows[0]["FilePath"].ToString()))
        //                {
        //                    pictureBox1.Image = new Bitmap(img);
        //                    lblFile.Text = dt.Rows[0]["FilePath"].ToString();
        //                    isNewFile = false;
        //                    oldFilePath = dt.Rows[0]["FilePath"].ToString();
        //                }
        //            }
        //            else
        //            {
        //                using (var img = new Bitmap(Application.StartupPath + "\\images\\default_img.png"))
        //                {
        //                    pictureBox1.Image = new Bitmap(img);
        //                    lblFile.Text = "\\images\\default_img.png";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            using (var img = new Bitmap(Application.StartupPath + "\\images\\default_img.png"))
        //            {
        //                pictureBox1.Image = new Bitmap(img);
        //                lblFile.Text = "\\images\\default_img.png";
        //            }
        //        }
        //        cmbDesig.SelectedValue = dt.Rows[0]["DesignationID"].ToString();
        //    }
        //}
        //private void Delete()
        //{
        //    DialogResult dialogResult = MessageBox.Show("Are you sure to remove?", "Confirm Message", MessageBoxButtons.YesNo);
        //    if (dialogResult == DialogResult.Yes)
        //    {
        //        con.Open();
        //        DataTable dt = new DataTable();
        //        string query = @"SELECT [EmployeeID]
        //              ,[EmployeeName]
        //              ,[JoinDate]
        //              ,[Gender]
        //              ,[FilePath]
        //              ,[DesignationID]
        //          FROM [Employee] WHERE [EmployeeID] = " + EmployeeID;
        //        adapt = new SqlDataAdapter(query, con);
        //        adapt.Fill(dt);
        //        con.Close();

        //        if (dt.Rows.Count > 0)
        //        {
        //            if (dt.Rows[0]["FilePath"] != null)
        //            {
        //                // remove old file
        //                RemoveFile(Application.StartupPath + dt.Rows[0]["FilePath"].ToString());
        //            }

        //            string q = @"DELETE FROM [Employee]
        //            WHERE EmployeeID = @EmployeeID";
        //            cmd = new SqlCommand(q, con);
        //            con.Open();
        //            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //            ResetEmployee();
        //            LoadEmp();
        //            MessageBox.Show("Data removed successfully.");

        //        }
        //    }
        //    else if (dialogResult == DialogResult.No)
        //    {
        //        //do something else
        //    }
        //}
        //private void btnAdd_Click(object sender, EventArgs e)
        //{


        //    if (Emp_ID > 0)
        //    {
        //        Updates();
        //    }
        //    else
        //    {
        //        AddNew();
        //    }
        //    ResetEmployee();
        //    LoadEmp();

        //}

        //private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex != -1 && e.ColumnIndex != -1 && dataGridView3.Rows.Count > e.RowIndex + 1)
        //    {
        //        var v = dataGridView3.Rows[e.RowIndex].Cells["EmployeeID"].Value;
        //        EmployeeID = dataGridView3.Rows[e.RowIndex].Cells["EmployeeID"].Value == null ? 0 : Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells["EmployeeID"].Value);
        //        if ("Report" == dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
        //        {
        //            Form2 f2 = new Form2();
        //            f2.Show();
        //        }
        //        if ("Edit" == dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
        //        {
        //            Edit();
        //        }
        //        if ("Delete" == dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
        //        {
        //            Delete();
        //        }
        //    }
        //}

        //private void btnBrowse_Click(object sender, EventArgs e)
        //{
        //    SelectFile();
        //}
        //private void SelectFile()
        //{
        //    open.Filter = "JPG (*.JPG)|*.jpg";
        //    if (open.ShowDialog() == DialogResult.OK)
        //    {
        //        using (var img = new Bitmap(open.FileName))
        //        {
        //            pictureBox1.Image = new Bitmap(img);
        //        }
        //        // image file path
        //        newFilePath = open.FileName;
        //        isNewFile = true;
        //    }
        //}
        //string newFilePath = string.Empty;
        //string oldFilePath = string.Empty;
        //bool isNewFile = true;
        //OpenFileDialog open = new OpenFileDialog();
    }
}
