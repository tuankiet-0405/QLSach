using BUS;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSach
{
    public partial class frmSQL : Form
    {
        private readonly SachService sachService = new SachService();
        private readonly LoaiSachService loaiService = new LoaiSachService();
        SqlQL context = new SqlQL();
        public frmSQL()
        {
            InitializeComponent();
        }

        private void frmQLS_Load(object sender, EventArgs e)
        {
            SqlQL context = new SqlQL();
            var loaiSaches = loaiService.Getall();
            var saches = sachService.Getall();
            fillCMB(loaiSaches);
            BindGrid(saches);
        }
        public void fillCMB(List<LoaiSach> loaiSaches)
        {
            cmbLoai.DataSource = loaiSaches;
            cmbLoai.DisplayMember = "TenLoai";
            cmbLoai.ValueMember = "Maloai";
        }
        public void BindGrid (List<Sach> saches)
        {
            dgvDS.Rows.Clear();
            foreach(var item in saches)
            {
                int index = dgvDS.Rows.Add();
                dgvDS.Rows[index].Cells[0].Value = item.MaSach;
                dgvDS.Rows[index].Cells[1].Value = item.TenSach;
                dgvDS.Rows[index].Cells[2].Value = item.NamXB;
                dgvDS.Rows[index].Cells[3].Value = item.LoaiSach.TenLoai;
            }
        }

        private void dgvDS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlQL context = new SqlQL();
            txtMaSach.Enabled = false;
            int index = e.RowIndex;
            if (index >= 0)
            {
                txtMaSach.Text = dgvDS.Rows[index].Cells[0].Value.ToString();
                txtTenSach.Text = dgvDS.Rows[index].Cells[1].Value.ToString();
                txtNamXB.Text = dgvDS.Rows[index].Cells[2].Value.ToString();
                cmbLoai.Text = dgvDS.Rows[index].Cells[3].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Sach updatedSach = new Sach();
            updatedSach.MaSach = txtMaSach.Text;
            updatedSach.TenSach = txtTenSach.Text;
            updatedSach.NamXB = Convert.ToInt32(txtNamXB.Text);
            updatedSach.MaLoai = ((LoaiSach)cmbLoai.SelectedItem).MaLoai;
            string t = sachService.ADD(txtMaSach.Text, updatedSach);
            MessageBox.Show(t);
            BindGrid(sachService.Getall());
            clear();
        }
        public void clear()
        {
            txtMaSach.Clear();
            txtNamXB.Clear();
            txtTenSach.Clear();
            cmbLoai.SelectedIndex = 0;
        }
        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            var saches = sachService.Search(txtFind.Text);
            BindGrid(saches.ToList());
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (sachService.GetById(txtMaSach.Text) != null)
            {
                var sach = sachService.GetById(txtMaSach.Text);
                if (MessageBox.Show("Bạn có muốn xóa không", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    sachService.Delete(txtMaSach.Text);
                    BindGrid(sachService.Getall());
                }
            }
            else
            {
                MessageBox.Show("Sách cần xóa không tồn tại");
            }
            clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Sach updatedSach = new Sach();
            updatedSach.MaSach=txtMaSach.Text;
            updatedSach.TenSach = txtTenSach.Text;
            updatedSach.NamXB = Convert.ToInt32(txtNamXB.Text);
            updatedSach.MaLoai = ((LoaiSach)cmbLoai.SelectedItem).MaLoai;
            string result = sachService.Update(txtMaSach.Text, updatedSach);
            MessageBox.Show(result); 
            BindGrid(sachService.Getall());
            clear();
        }
        private void dgvDS_DoubleClick(object sender, EventArgs e)
        {
            txtMaSach.Enabled = true;
        }
    }
}
