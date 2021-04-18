using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApp.Web.Services
{
    public class BooksService : APIServices
    {

        public BooksService(string controller="Books") : base(controller)
        {
        }
    }
}
