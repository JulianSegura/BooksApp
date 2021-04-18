using BooksApp.Web.Models;
using BooksApp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BooksApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BooksService _bookService = new BooksService();

        public IActionResult Index()
        {
            var apiResponse = _bookService.GetAllAsync();
            var responseString = apiResponse.Content.ReadAsStringAsync().Result;
            var bookList = JsonConvert.DeserializeObject<List<BookDTO>>(responseString);

            return View(bookList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var apiResponse = _bookService.GetByIdAsync(id);
            var responseString = await apiResponse.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<BookDTO>(responseString);

            return View(book);
        }
    }
}
