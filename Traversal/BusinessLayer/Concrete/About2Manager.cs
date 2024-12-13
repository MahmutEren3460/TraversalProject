using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class About2Manager : IAbout2Service
    {
        IAbout2Dal _About2Dal;

        public About2Manager(IAbout2Dal about2Dal)
        {
            _About2Dal = about2Dal;
        }

        public void TAdd(About2 t)
        {
            _About2Dal.Insert(t);
        }

        public void TDelete(About2 t)
        {
           _About2Dal.Delete(t);
        }

        public About2 TGetById(int id)
        {
            return _About2Dal.GetByID(id);
        }

        public List<About2> TGetList()
        {
            return _About2Dal.GetList();
        }

        public void TUpdate(About2 t)
        {
            _About2Dal.Update(t);
        }
    }
}
