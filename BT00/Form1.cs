using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace BT00
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAccess2003_Click(object sender, EventArgs e)
        {
            //Khai bao cac doi tuong can su dung de ket noi
            //1. Chuoi ket noi  den csdl access 2003
            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=..\..\..\DATA\QLSV_Mau.mdb";

            //2. Khai bao doi tuong ket doi
            OleDbConnection cnn = new OleDbConnection(strcon);
            //3. Mo ket noi
            cnn.Open();
            //4. Kiem tra xem co ket noi thanh cong khong?
            if (cnn.State == ConnectionState.Open)
                MessageBox.Show("Ket noi voi SQL.mdb thanh cong", "Thong bao ket noi", MessageBoxButtons.OK, MessageBoxIcon.None);
            //5. Sau khi su dung thi dong ket noi
            cnn.Close();

        }

        private void btnAccess2019_Click(object sender, EventArgs e)
        {
            //Khai bao cac doi tuong can su dung de ket noi
            //1. Chuoi ket noi  den csdl access 2003
            string strcon = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=..\..\..\DATA\QLSV_Mau.accdb";

            //2. Khai bao doi tuong ket doi
            OleDbConnection cnn = new OleDbConnection(strcon);
            //3. Mo ket noi
            cnn.Open();
            //4. Kiem tra xem co ket noi thanh cong khong?
            if (cnn.State == ConnectionState.Open)
                MessageBox.Show("Ket noi voi SQL.mdb thanh cong", "Thong bao ket noi", MessageBoxButtons.OK, MessageBoxIcon.None);
            //5. Sau khi su dung thi dong ket noi
            cnn.Close();
        }
    }
}
