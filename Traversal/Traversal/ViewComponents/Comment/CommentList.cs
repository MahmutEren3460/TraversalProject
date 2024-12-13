using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.Comment
{
    public class CommentList:ViewComponent
    {
        CommentManager Comment = new CommentManager(new EFCommentDal());
        Context context = new Context();

        public IViewComponentResult Invoke(int id)
        {
            ViewBag.commentCount = context.Comments.Where(x => x.DestinationID == id).Count();
            var values = Comment.TGetListCommentWithDestinationAndAppUser(id);
            return View(values);
        }
    }
}
