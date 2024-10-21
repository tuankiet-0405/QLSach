using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class LoaiSachService
    {
        SqlQL context = new SqlQL();

        public void Add(LoaiSach newLoaiSach)
        {
           
        }

        public List<LoaiSach> Getall()
        {
            return context.LoaiSaches.ToList();
        }

        public object GetById(string v)
        {
            return context.LoaiSaches.FirstOrDefault(p => p.MaLoai.ToString() == v);
        }

        public object GetByTen(string text)
        {
            throw new NotImplementedException();
        }
    }
}
