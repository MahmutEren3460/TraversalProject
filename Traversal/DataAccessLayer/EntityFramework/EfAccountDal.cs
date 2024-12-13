using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfAccountDal : GenericUowRepository<Account>, IAccountDal
    {
        private readonly Context _context;
        public EfAccountDal(Context context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAllReceivers()
        {
            return _context.Accounts.ToList();
        }
    }
}
