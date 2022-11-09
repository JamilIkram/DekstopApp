using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBank
{
    public partial class Form1 : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter adapt = null;
        public static int DonorID = 0;
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=DESKTOP-S4UTGJ3;Initial Catalog=BloodBankDB;Integrated Security=True");
            AddButtonColumn();
            LoadDoner();
            ResetDoner();
        }

        int bankid = 0;
        public DataTable GetBloodBank()
        {
            DataSet dsData = new DataSet();
            {
                con.Open();
                string query = "SELECT [BankID],[BankName] FROM [BloodBank]";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dsData);
                con.Close();
                return dsData.Tables[0];
            }
        }
        private void ResetBloodBank()
        {
            btnInsertBank.Show();
            btnUpdateBank.Hide();
            txtBankName.Text = "";
            dataGridView1.DataSource = GetBloodBank();
            bankid = 0;
            string q = "SELECT n.[DonorID],n.[DonorName],b.[BankName] FROM [Donor] n JOIN [BloodBank] b ON n.BankID = b.BankID";
          
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adap.Fill(ds, "Donor");

        }

        private void btnInsertBank_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = null;
            con.Open();
            string insertQ = "INSERT INTO [BloodBank]([BankName]) VALUES('" + txtBankName.Text.Trim() + "')";
            cmd = new SqlCommand(insertQ, con);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                MessageBox.Show("INSERT Perform Succesfully.");
            }
            else
            {
                MessageBox.Show("Operation failed");
            }
            con.Close();
            FillCmbBloodBank();
            ResetBloodBank();
        }
        

        private void btnUpdateBank_Click(object sender, EventArgs e)
        {
            string selectQ = "SELECT [BankID],[BankName] FROM [BloodBank] WHERE [BankID] = " + bankid;
            SqlCommand cmd = new SqlCommand(selectQ, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    bankid = Convert.ToInt32(reader["BankID"]);
                }
                reader.Close();
                string updateQ = "UPDATE [BloodBank] SET [BankName] = '" + txtBankName.Text.Trim() + "' WHERE [BankID] = " + bankid;
                cmd = new SqlCommand(updateQ, con);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("UPDATE Perform Succesfully.");
                }
                else
                {
                    MessageBox.Show("Operation failed.");
                }
            }
            else
            {
                MessageBox.Show("No data found.");
            }
            con.Close();
            ResetBloodBank();
            FillCmbBloodBank();
        }

        private void btnResetBank_Click(object sender, EventArgs e)
        {
            ResetBloodBank();
        }

        private void ResetDoner()
        {
            DonorID = 0;
            btnAdd.Text = "Add";
            newFilePath = "";
            pictureBox1.Image = null;
            using (var img = new Bitmap(Application.StartupPath + "\\images\\default_img.png"))
            {
                pictureBox1.Image = new Bitmap(img);

            }
            txtDonner.Text = "";
            pickDoB.Text = "";
            radioFemale.Checked = true;
            cmbBank.Text = "";
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

        private void FillCmbBloodBank()
        {
            var bloodbank = GetBloodBank();
            cmbBank.DataSource = bloodbank;
            cmbBank.DisplayMember = "BankName";
            cmbBank.ValueMember = "BankID";
        }

        private string UpdateFile()
        {
            string strFilePath = string.Empty;
            if (isNewFile)
            {
                strFilePath = "\\images\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                
                File.Copy(newFilePath, Application.StartupPath + strFilePath);

                
                RemoveFile(Application.StartupPath + oldFilePath);
            }
            else
            {
                strFilePath = oldFilePath;
            }

            return strFilePath;
        }

        private void RemoveFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (!filePath.Contains("default"))
                {
                    File.Delete(filePath);
                }
                pictureBox1.Image = null;
            }
        }

        private void LoadDoner()
        {
            con.Open();
            DataTable dt = new DataTable();
            string query = @"SELECT e.[DonorID],e.[DonorName],e.[DonationDate],e.[Gender],e.[FilePath],d.[BankName] FROM [Donor] e JOIN [BloodBank] d ON e.BankID = d.BankID";
            adapt = new SqlDataAdapter(query, con);
            adapt.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void Updates()
        {
           
            string strFilePath = UpdateFile();

            cmd = new SqlCommand(@"UPDATE [Donor]
               SET [DonorName] = @DonorName
                  ,[DonationDate] = @DonationDate
                  ,[Gender] = @Gender
                  ,[FilePath] = @FilePath
                  ,[BankID] = @BankID
             WHERE [DonorID] = @DonorID", con);
            con.Open();
            cmd.Parameters.AddWithValue("@DonorID", DonorID);
            cmd.Parameters.AddWithValue("@DonorName", txtDonner.Text.Trim());
            cmd.Parameters.AddWithValue("@DonationDate", pickDoB.Value);
            cmd.Parameters.AddWithValue("@Gender", radioFemale.Checked == true ? "Female" : "Male");
            cmd.Parameters.AddWithValue("@FilePath", strFilePath);
            cmd.Parameters.AddWithValue("@BankID", cmbBank.SelectedValue.ToString());
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Data updated successfully.");
        }

        private string AddFile()
        {
            string strFilePath = string.Empty;
            if (isNewFile)
            {
                strFilePath = "\\images\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                
                File.Copy(newFilePath, Application.StartupPath + strFilePath);
            }

            return strFilePath;
        }

        private void Edit()
        {
            con.Open();
            DataTable dt = new DataTable();
            string query = @"SELECT [DonorID]
                  ,[DonorName]
                  ,[DonationDate]
                  ,[Gender]
                  ,[FilePath]
                  ,[BankID]
              FROM [Donor] WHERE [DonorID] = " + DonorID;
            adapt = new SqlDataAdapter(query, con);
            adapt.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                btnAdd.Text = "Update";

                DonorID = Convert.ToInt32(dt.Rows[0]["DonorID"].ToString());
                txtDonner.Text = dt.Rows[0]["DonorName"].ToString();
                pickDoB.Value = Convert.ToDateTime(dt.Rows[0]["DonationDate"].ToString());
                radioFemale.Checked = dt.Rows[0]["Gender"].ToString() == "Female" ? true : false;
                radioMale.Checked = dt.Rows[0]["Gender"].ToString() == "Female" ? false : true;
               
                if (dt.Rows[0]["FilePath"].ToString() != null)
                {
                    if (File.Exists(Application.StartupPath + dt.Rows[0]["FilePath"].ToString()))
                    {
                        using (var img = new Bitmap(Application.StartupPath + dt.Rows[0]["FilePath"].ToString()))
                        {
                            pictureBox1.Image = new Bitmap(img);
                            lblFile.Text = dt.Rows[0]["FilePath"].ToString();
                            isNewFile = false;
                            oldFilePath = dt.Rows[0]["FilePath"].ToString();
                        }
                    }
                    else
                    {
                        using (var img = new Bitmap(Application.StartupPath + "\\images\\default_img.png"))
                        {
                            pictureBox1.Image = new Bitmap(img);
                            lblFile.Text = "\\images\\default_img.png";
                        }
                    }
                }
                else
                {
                    using (var img = new Bitmap(Application.StartupPath + "\\images\\default_img.png"))
                    {
                        pictureBox1.Image = new Bitmap(img);
                        lblFile.Text = "\\images\\default_img.png";
                    }
                }
                cmbBank.SelectedValue = dt.Rows[0]["BankID"].ToString();
            }
        }
        private void AddNew()
        {
            
            string strFilePath = AddFile();
            string query = @"INSERT INTO [dbo].[Donor]
                ([DonorName]
               ,[DonationDate]
               ,[Gender]
               ,[FilePath]
               ,[BankID])
                VALUES
               (@DonorName
               ,@DonationDate
               ,@Gender
               ,@FilePath
               ,@BankID)";
            cmd = new SqlCommand(query, con);
            con.Open();
            cmd.Parameters.AddWithValue("@DonorName", txtDonner.Text.Trim());
            cmd.Parameters.AddWithValue("@DonationDate", pickDoB.Value);
            cmd.Parameters.AddWithValue("@Gender", radioFemale.Checked == true ? "Female" : "Male");
            cmd.Parameters.AddWithValue("@FilePath", strFilePath);
            cmd.Parameters.AddWithValue("@BankID", cmbBank.SelectedValue.ToString());
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Data added successfully.");
        }

        private void Delete()
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to remove?", "Confirm Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                con.Open();
                DataTable dt = new DataTable();
                string query = @"SELECT [DonorID]
                      ,[DonorName]
                      ,[DonationDate]
                      ,[Gender]
                      ,[FilePath]
                      ,[BankID]
                  FROM [Donor] WHERE [DonorID] = " + DonorID;
                adapt = new SqlDataAdapter(query, con);
                adapt.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["FilePath"] != null)
                    {
                        
                        RemoveFile(Application.StartupPath + dt.Rows[0]["FilePath"].ToString());
                    }

                    string q = @"DELETE FROM [Donor]
                    WHERE DonorID = @DonorID";
                    cmd = new SqlCommand(q, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@DonorID", DonorID);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ResetDoner();
                    LoadDoner();
                    MessageBox.Show("Data removed successfully.");

                }
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (DonorID > 0)
            {
                Updates();
            }
            else
            {
                AddNew();
            }
            ResetDoner();
            LoadDoner();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetDoner();
        }

        private void AddButtonColumn()
        {
            DataGridViewButtonColumn btnReport = new DataGridViewButtonColumn();
            btnReport.HeaderText = "#";
            btnReport.Text = "Report";
            btnReport.Name = "btnReport";
            btnReport.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(btnReport);

            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            btnEdit.HeaderText = "#";
            btnEdit.Text = "Edit";
            btnEdit.Name = "btnEdit";
            btnEdit.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(btnEdit);

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "#";
            btnDelete.Text = "Delete";
            btnDelete.Name = "btnDelete";
            btnDelete.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(btnDelete);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1 && dataGridView2.Rows.Count > e.RowIndex + 1)
            {
                var v = dataGridView2.Rows[e.RowIndex].Cells["DonorID"].Value;
                DonorID = dataGridView2.Rows[e.RowIndex].Cells["DonorID"].Value == null ? 0 : Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["DonorID"].Value);
                if ("Report" == dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                {
                    Form2 f2 = new Form2();
                    f2.Show();
                }
                if ("Edit" == dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                {
                    Edit();
                }
                if ("Delete" == dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                {
                    Delete();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetBloodBank();
            ResetDoner();
            FillCmbBloodBank();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SelectFile();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0) 
            {
                btnInsertBank.Hide();
                btnUpdateBank.Show();
                string BankID = dataGridView1.Rows[e.RowIndex].Cells["BankID"].Value.ToString();
                string BankName = dataGridView1.Rows[e.RowIndex].Cells["BankName"].Value.ToString();
                Int32.TryParse(BankID, out bankid);
                txtBankName.Text = BankName;
            }

            if (e.ColumnIndex == 1) // delete button
            {
                string BankID = dataGridView1.Rows[e.RowIndex].Cells["BankID"].Value.ToString();
                string BankName = dataGridView1.Rows[e.RowIndex].Cells["BankName"].Value.ToString();
                Int32.TryParse(BankID, out bankid);

                // example of connected architechture
                if (MessageBox.Show("Are you sure to delete '" + BankName + "'", "Delete confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string selectQ = "SELECT [BankID],[BankName] FROM [BloodBank] WHERE [BankID] = " + bankid;
                    SqlCommand cmd = new SqlCommand(selectQ, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            bankid = Convert.ToInt32(reader["BankID"]);
                        }
                        reader.Close();

                        string deleteQ = "DELETE FROM [BloodBank] WHERE [BankID] = " + bankid;
                        cmd = new SqlCommand(deleteQ, con);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("DELETE Perform Succesfully.");
                        }
                        else
                        {
                            MessageBox.Show("Operation failed.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found.");
                    }
                    con.Close();
                    ResetBloodBank();
                }
            }
        }
    }
}
