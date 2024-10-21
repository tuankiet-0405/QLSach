using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class SachService
    {
        SqlQL context = new SqlQL();
        public List<Sach> Getall()
        {
            return context.Saches.ToList();
        }

        public List<Sach> Search(string search)
        {
            return context.Saches.Where(p => p.TenSach.Contains(search) || p.MaSach.Contains(search) || p.NamXB.ToString().Contains(search)||p.LoaiSach.TenLoai.Contains(search)).ToList();
        }

        public string ADD(string id,Sach SAdd)
        {
            Sach s = new Sach();
            if (string.IsNullOrEmpty(SAdd.MaSach) || SAdd.NamXB <= 0 || string.IsNullOrEmpty(SAdd.TenSach) || string.IsNullOrEmpty(SAdd.MaLoai.ToString()))
            {
                return "Vui lòng nhập đầy đủ thông tin";
            }
            if (id.Length !=6 )
            {
                return "Mã sách phải có 6 kí tự";
            }
            else if(Getall().Any(p=>p.MaSach == id))
            {
                return "Mã số bị trùng";
            }
            s.MaSach = SAdd.MaSach;
            s.TenSach = SAdd.TenSach;
            s.NamXB = SAdd.NamXB;
            s.MaLoai = SAdd.MaLoai;
                context.Saches.Add(s);
                context.SaveChanges();
                return "Thêm thành công";
        }

        public string Update(string id, Sach Update)
        {
            Sach s= new Sach();
            if (string.IsNullOrEmpty(Update.MaSach) || Update.NamXB<=0 || string.IsNullOrEmpty(Update.TenSach) || string.IsNullOrEmpty(Update.MaLoai.ToString()))
            {
                return "Vui lòng nhập đầy đủ thông tin";
            }
            if (id.Length != 6)
            {
                return "Mã sách phải có 6 kí tự";
            }
            Sach existingSach = context.Saches.FirstOrDefault(p => p.MaSach == id);
            if (existingSach == null)
            {
                
                return "Sách không tồn tại"; 
            }
            s.MaSach = Update.MaSach;
            s.TenSach = Update.TenSach;
            s.NamXB = Update.NamXB;
            s.MaLoai = Update.MaLoai;
            context.Saches.AddOrUpdate(s, existingSach);
            context.SaveChanges();
            return "Cập nhật thành công";
        }
        public void Delete(string id)
        {
            Sach sach = context.Saches.FirstOrDefault(p => p.MaSach == id);
            if (sach != null)
            {
                context.Saches.Remove(sach);
                context.SaveChanges();
            }
        }

        public Sach GetById(string id)
        {
            return context.Saches.FirstOrDefault(p => p.MaSach == id);
        }
    }
}
