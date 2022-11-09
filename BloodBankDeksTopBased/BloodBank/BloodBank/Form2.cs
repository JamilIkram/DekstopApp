using BloodBank.Reports;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Report2WithSqlConn();
        }
        //private void Report2WithSqlConn()
        //{
        //    string query = @"SELECT em.[DonorID]
        //          ,em.[DonorName]
        //          ,em.[DonationDate]
        //          ,em.[Gender]
        //          ,em.[FilePath]
        //          ,dg.[BankName] BankID
        //      FROM [BloodBankDB].[dbo].[Donor] em
        //      left join BloodBank dg on em.BankID=dg.BankID WHERE em.[DonorID] = " + Form1.DonorID;
        //    string connectionString = "server=DESKTOP-S4UTGJ3;Initial Catalog=BloodBankDB;Integrated Security=True;";
        //    SqlConnection con = new SqlConnection(connectionString);
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter adap = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    adap.Fill(ds, "Donor");
        //    for (var i = 0; i < ds.Tables["Donor"].Rows.Count; i++)
        //    {
        //        if (ds.Tables["Donor"].Rows[i]["FilePath"] != null)
        //        {
        //            if (!string.IsNullOrEmpty(ds.Tables["Donor"].Rows[i]["FilePath"].ToString()))
        //            {
        //                string strFilePath = Application.StartupPath + ds.Tables["Donor"].Rows[i]["FilePath"].ToString();
        //                if (File.Exists(strFilePath))
        //                {
        //                    ds.Tables["Donor"].Rows[i]["FilePath"] = strFilePath;
        //                }
        //            }
        //        }
        //    }

        //    CrystalReport1 cr2 = new CrystalReport1();
        //    cr2.SetDataSource(ds);
        //    crystalReportViewer1.ReportSource = cr2;
        //    con.Close();
        //    crystalReportViewer1.Refresh();
        //}
        private void Report2WithSqlConn()
        {
            string query = @"SELECT TOP (1000) em.[DonorID]
                  ,em.[DonorName]
                  ,em.[DonationDate]
                  ,em.[Gender]
                  ,em.[FilePath]
                  ,dg.[BankName] BankID
              FROM [BloodBankDB].[dbo].[Donor] em
              left join BloodBank dg on em.BankID=dg.BankID WHERE em.[DonorID] = " + Form1.DonorID;
            string connectionString = "server=DESKTOP-S4UTGJ3;Initial Catalog=BloodBankDB;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adap.Fill(ds, "Donor");
            for (var i = 0; i < ds.Tables["Donor"].Rows.Count; i++)
            {
                if (ds.Tables["Donor"].Rows[i]["FilePath"] != null)
                {
                    if (!string.IsNullOrEmpty(ds.Tables["Donor"].Rows[i]["FilePath"].ToString()))
                    {
                        string strFilePath = Application.StartupPath + ds.Tables["Donor"].Rows[i]["FilePath"].ToString();
                        if (File.Exists(strFilePath))
                        {
                            ds.Tables["Donor"].Rows[i]["FilePath"] = strFilePath;
                        }
                    }
                }
            }

            CrystalReport2 cr2 = new CrystalReport2();
            cr2.SetDataSource(ds);
            crystalReportViewer1.ReportSource = cr2;
            con.Close();
            crystalReportViewer1.Refresh();
        }
    }
}
