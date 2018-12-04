using System.Linq;
using TimeSheet2.EntityFramework;

namespace TimeSheet2.ViewModels.AccountViewModels
{
    public class AccountIndexViewModel
    {
        public IQueryable<ApplicationUser> Users { get; set; }


    }
}
