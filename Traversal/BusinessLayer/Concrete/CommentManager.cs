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
    public class CommentManager : ICommentService
    {
        ICommentDal _comment;

        public CommentManager(ICommentDal comment)
        {
            _comment = comment;
        }

        public void TAdd(Comment t)
        {
            _comment.Insert(t);
        }

        public void TDelete(Comment t)
        {
            _comment.Delete(t);
        }

        public Comment TGetById(int id)
        {
            return _comment.GetByID(id);
        }

        public List<Comment> TGetList()
        {
            return _comment.GetList();
        }

        public void TUpdate(Comment t)
        {
            _comment.Update(t);
        }
        public  List<Comment> GetByBlogID(int id)
        {
            return _comment.GetListByFilter(x=>x.DestinationID == id);
        }
        public List<Comment> TGetListCommentWithDestination()
        {
            return _comment.GetListCommentWithDestination();
        }

		public List<Comment> TGetListCommentWithDestinationAndAppUser(int id)
		{
			return _comment.GetListCommentWithDestinationAndAppUser(id);
		}
	}
}
