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
    public class NewsLetterManager : INewsLetterService
    {
        INewsLetterDal _newsLetter;

        public NewsLetterManager(INewsLetterDal newsLetter)
        {
            _newsLetter = newsLetter;
        }

        public void TAdd(NewsLetter t)
        {
            _newsLetter.Insert(t);
        }

        public void TDelete(NewsLetter t)
        {
            _newsLetter.Delete(t);
        }

        public NewsLetter TGetById(int id)
        {
            return _newsLetter.GetByID(id);
        }

        public List<NewsLetter> TGetList()
        {
            return _newsLetter.GetList();
        }

        public void TUpdate(NewsLetter t)
        {
            _newsLetter.Update(t);
        }
    }
}
