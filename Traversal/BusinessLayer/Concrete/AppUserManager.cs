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
    public class AppUserManager : IAppUserService
    {
        IAppUserDal _AppUserDal;

        public AppUserManager(IAppUserDal appUserDal)
        {
            _AppUserDal = appUserDal;
        }

        public void TAdd(AppUser t)
        {
            _AppUserDal.Insert(t);
        }

        public void TDelete(AppUser t)
        {
            _AppUserDal.Delete(t);
        }

        public AppUser TGetById(int id)
        {
           return _AppUserDal.GetByID(id);
        }

        public List<AppUser> TGetList()
        {
            return _AppUserDal.GetList();
        }

        public void TUpdate(AppUser t)
        {
            _AppUserDal.Update(t);
        }
    }
}
