using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT01
{
    public partial class Form1 : Form
    {
        string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\..\DATA\QLSV_Mau.mdb";
        OleDbConnection cnn;
        DataSet ds = new DataSet();
        DataTable tblKhoa = new DataTable("KHOA");
        DataTable tblSinhVien = new DataTable("SINHVIEN");
        DataTable tblKetQua = new DataTable("KETQUA");
        OleDbCommand cmdKhoa, cmdSinhVien, cmdKetQua;
        int stt=-1;
        public Form1()
        {
            InitializeComponent();
        }

        private void txtNoiSinh_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSV.ReadOnly = false;
            foreach (Control ctl in this.Controls)
                if (ctl is TextBox)
                    (ctl as TextBox).Clear();
                else if (ctl is CheckBox)
                    (ctl as CheckBox).Checked = true;
                else if (ctl is ComboBox)
                    (ctl as ComboBox).SelectedIndex = 0;
                else if (ctl is DateTimePicker)
                    (ctl as DateTimePicker).Value = new DateTime(2006, 1, 1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Khoi Tao Ket Noi
            cnn = new OleDbConnection(strcon);
            //cnn.Open();
            //if (cnn.State == ConnectionState.Open)
            //    MessageBox.Show("OK");
            Tao_Cau_Truc_Cac_Bang();
            Nhap_Lieu_Tu_CSDL();
            Khoi_Tao_Combo_Khoa();
            stt = 0;
            Gan_Du_Lieu(stt);
        }

        private void Gan_Du_Lieu(int stt)
        {
            //lấy dòng dữ liệu thứ stt trong tblSinhVien
            DataRow rsv = tblSinhVien.Rows[stt];
            txtMaSV.Text = rsv["MaSV"].ToString();
            txtHo.Text = rsv["HoSV"].ToString();
            txtTen.Text = rsv["TenSV"].ToString();
            cbPhai.Checked = (Boolean)rsv["Phai"];
            dtpNgaySinh.Text = rsv["NgaySinh"].ToString();
            txtNoiSinh.Text = rsv["NoiSinh"].ToString();
            cbbKhoa.SelectedValue = rsv["MaKH"].ToString();
            txtHocBong.Text = rsv["HocBong"].ToString();
            //thể hiện số thứ tự mẫu tin hiện hành
            lblSTT.Text = (stt + 1) + "/" + tblSinhVien.Rows.Count;
            //Tính Tổng Điểm
            txtTongDiem.Text = TongDiem(txtMaSV.Text).ToString();
        }
        private double TongDiem(String MSV)
        {
            double kq = 0;
            object tb = tblKetQua.Compute("sum(Diem)", "MaSV='" + MSV + "'");
            //lưu ý trường hợp SV không có điểm thì phơng thức compute trả về giá trị DBNull
            if (tb == DBNull.Value)
                kq = 0;
            else
                kq = Convert.ToDouble(tb);
            return kq;
        }

        private void Khoi_Tao_Combo_Khoa()
        {
            cbbKhoa.DisplayMember = "TenKH";
            cbbKhoa.ValueMember = "MaKH";
            cbbKhoa.DataSource = tblKhoa;
        }

        private void Tao_Cau_Truc_Cac_Bang()
        {
            //Tạo cấu trúc cho datatable tương ứng với bảng Khoa
            tblKhoa.Columns.Add("MaKH", typeof(string));
            tblKhoa.Columns.Add("TenKH", typeof(string));
            //Tạo Khóa Chính cho tblKHOA
            tblKhoa.PrimaryKey = new DataColumn[] { tblKhoa.Columns["MaKH"] };
            //Tạo cấu trúc cho datatable tương ứng với bảng SinhVien
            tblSinhVien.Columns.Add("MaSV", typeof(String));
            tblSinhVien.Columns.Add("HoSV", typeof(String));
            tblSinhVien.Columns.Add("TenSV", typeof(String));
            tblSinhVien.Columns.Add("Phai", typeof(Boolean));
            tblSinhVien.Columns.Add("NgaySinh", typeof(DateTime));
            tblSinhVien.Columns.Add("NoiSinh", typeof(String));
            tblSinhVien.Columns.Add("MaKH", typeof(String));
            tblSinhVien.Columns.Add("HocBong", typeof(double));
            //Tạo Khóa Chính cho tblSINHVIEN
            tblSinhVien.PrimaryKey = new DataColumn[] { tblSinhVien.Columns["MaSV"] };
            //Tạo cấu trúc cho datatable tương ứng với bảng tblKETQUA
            tblKetQua.Columns.Add("MaSV", typeof(String));
            tblKetQua.Columns.Add("MaMH", typeof(String));
            tblKetQua.Columns.Add("diem", typeof(double));
            //Tạo Khóa Chính cho tblKETQUA
            tblKetQua.PrimaryKey = new DataColumn[] { tblKetQua.Columns["MaSV"], tblKetQua.Columns["MaMH"] };

            //Thêm đồng thời nhiều datatable
            ds.Tables.AddRange(new DataTable[] { tblKhoa, tblSinhVien, tblKetQua });
        }
        private void Moc_Noi_Quan_He_Cac_Bang()
        {
            //Tạo quan hệ giữa tblKhoa và tblSinhVien
            ds.Relations.Add("FK_KH_SV", ds.Tables["KHOA"].Columns["MaKH"], ds.Tables["SINHVIEN"].Columns["MaKH"], true);
            //Tạo quan hệ giữa tblSinhVien và tblKetQua
            ds.Relations.Add("FK_SV_KQ", ds.Tables["SINHVIEN"].Columns["MaSV"], ds.Tables["KETQUA"].Columns["MaSV"], true);
            //Loại bỏ Cacase Delete trong các quan hệ
            ds.Relations["FK_KH_SV"].ChildKeyConstraint.DeleteRule = Rule.None;
            ds.Relations["FK_SV_KQ"].ChildKeyConstraint.DeleteRule = Rule.None;

        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            if (stt == 0) return;
            stt--;
            Gan_Du_Lieu(stt);
        }

        private void btnSau_Click(object sender, EventArgs e)
        {
            if (stt == tblSinhVien.Rows.Count - 1) return;
            stt++;
            Gan_Du_Lieu(stt);
        }

        private void Nhap_Lieu_Tu_CSDL()
        {
            Nhap_Lieu_tblKhoa();
            NhapLieu_tblSinhVien();
            NhapLieu_tblKetQua();
        }

        private void NhapLieu_tblKetQua()
        {
            cnn.Open();

            cmdKetQua = new OleDbCommand("select * from KetQua", cnn);

            OleDbDataReader rkh = cmdKetQua.ExecuteReader();
            //tien hanh doc du kieu
            while (rkh.Read())// mỗi lần lặp thì rkh trỏ đến 1 dòng trong table SInhVien
            {
                DataRow r = tblKetQua.NewRow();
                for (int i = 0; i < rkh.FieldCount; i++)
                    r[i] = rkh[i];
                tblKetQua.Rows.Add(r);
            }
            //đóng datareader và đối tượng kết nối
            rkh.Close();
            cnn.Close();
        }

        private void btnKhong_Click(object sender, EventArgs e)
        {
            txtMaSV.ReadOnly = true;
            Gan_Du_Lieu(stt);
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            cnn.Open();
            if (txtMaSV.ReadOnly == true)
            {
                DataRow rsv = tblSinhVien.Rows.Find(txtMaSV.Text);
                //sua
                //1. update get inside dataTable
                rsv["HoSV"] = txtHo.Text;
                rsv["TenSV"] = txtTen.Text;
                rsv["Phai"] = cbPhai.Checked;
                rsv["NgaySinh"] = dtpNgaySinh.Text;
                rsv["NoiSinh"] = txtNoiSinh.Text;
                rsv["MaKH"] = cbbKhoa.SelectedValue.ToString();
                rsv["HocBong"] = txtHocBong.Text;
                //2. update gía trị trên form vào CSDL Access
                string Chuoi_Sua = "Update SinhVien Set ";
                Chuoi_Sua += "HoSV='" + txtHo.Text + "',";
                Chuoi_Sua += "TenSV='" + txtTen.Text + "',";
                Chuoi_Sua += "Phai=" + cbPhai.Checked + ",";
                Chuoi_Sua += "NgaySinh=#" + dtpNgaySinh.Value + "#,";
                Chuoi_Sua += "NoiSinh='" + txtNoiSinh.Text + "',";
                Chuoi_Sua += "MaKH='" + cbbKhoa.SelectedValue.ToString() + "',";
                Chuoi_Sua += "HocBong= " + txtHocBong.Text;
                Chuoi_Sua += " Where MaSV='" + txtMaSV.Text + "'";
                cmdSinhVien = new OleDbCommand(Chuoi_Sua,cnn);
                int n = cmdSinhVien.ExecuteNonQuery();
                if (n > 0)
                    MessageBox.Show("Update Sinh Viên thành công!");

            }
            else
            {
                DataRow rsv = tblSinhVien.Rows.Find(txtMaSV.Text);
                if (rsv != null)
                {
                    MessageBox.Show("Trùng mã SV!");
                    txtMaSV.Focus();
                    return;
                }
                rsv = tblSinhVien.NewRow();
                rsv["MaSV"] = txtMaSV.Text;
                rsv["HoSV"] = txtHo.Text;
                rsv["TenSV"] = txtTen.Text;
                rsv["Phai"] = cbPhai.Checked;
                rsv["NgaySinh"] = dtpNgaySinh.Text;
                rsv["NoiSinh"] = txtNoiSinh.Text;
                rsv["MaKH"] = cbbKhoa.SelectedValue.ToString();
                rsv["HocBong"] = txtHocBong.Text;
                tblSinhVien.Rows.Add(rsv);
                txtMaSV.ReadOnly = true;
                //them moi sv vao CSDL
                string Chuoi_Them = "Insert into SINHVIEN values (";
                Chuoi_Them +="'"+ txtMaSV.Text + "',";
                Chuoi_Them +="'"+ txtHo.Text + "',";
                Chuoi_Them +="'"+ txtTen.Text + "',";
                Chuoi_Them += cbPhai.Checked.ToString()+",";
                Chuoi_Them += "#" + dtpNgaySinh.Text + "#,";
                Chuoi_Them += "'" + txtNoiSinh.Text + "',";
                Chuoi_Them += "'" + cbbKhoa.SelectedValue.ToString() + "',";
                Chuoi_Them += txtHocBong.Text + ")";
                cmdSinhVien = new OleDbCommand(Chuoi_Them, cnn);
                int n = cmdSinhVien.ExecuteNonQuery();
                if (n > 0)
                    MessageBox.Show("Update Sinh Viên thành công!");
                txtMaSV.ReadOnly = true;

            }
            cnn.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaSV.ReadOnly = true;
            Gan_Du_Lieu(stt);
        }

        private void NhapLieu_tblSinhVien()
        {
            cnn.Open();

            cmdSinhVien = new OleDbCommand("select * from SinhVien", cnn);

            OleDbDataReader rkh = cmdSinhVien.ExecuteReader();
            //tien hanh doc du kieu
            while (rkh.Read())// mỗi lần lặp thì rkh trỏ đến 1 dòng trong table SInhVien
            {
                DataRow r = tblSinhVien.NewRow();
                for (int i = 0; i < rkh.FieldCount; i++)
                    r[i] = rkh[i];
                tblSinhVien.Rows.Add(r);
            }
            //đóng datareader và đối tượng kết nối
            rkh.Close();
            cnn.Close();
        }
    

        private void Nhap_Lieu_tblKhoa()
        {
            cnn.Open();
            //khoi tao doi tuong command tuong ung de doc du lieu tu table khoa
            cmdKhoa = new OleDbCommand("select * from Khoa", cnn);
            //tao doi tuong dataRealer de tien hanh doc tuong dong du lieu trong dataTable khoa
            OleDbDataReader rkh = cmdKhoa.ExecuteReader();
            //tien hanh doc du kieu
            while (rkh.Read())// mỗi lần lặp thì rkh trỏ đến 1 dòng trong table khoa
            {
                DataRow r = tblKhoa.NewRow();
                for (int i = 0; i < rkh.FieldCount; i++)
                    r[i] = rkh[i];
                tblKhoa.Rows.Add(r);
            }
            //đóng datareader và đối tượng kết nối
            rkh.Close();
            cnn.Close();
        }
    }
}
