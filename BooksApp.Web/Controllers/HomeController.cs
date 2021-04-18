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
            return View(GetBooks());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = GetBooks(id).FirstOrDefault();

            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = GetBooks(id).FirstOrDefault();

            //ToDo: Create View for Edit
            return View(book);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(BookDTO book)
        {
            //ToDo: Add validation to DTO before sending to edit
        }

        private List<BookDTO> GetBooks( int id=0)
        {
            HttpResponseMessage apiResponse=new HttpResponseMessage();

            apiResponse=(id == 0) ? _bookService.GetAllAsync() : _bookService.GetByIdAsync(id);
            var responseString = apiResponse.Content.ReadAsStringAsync().Result;

            var lst = new List<BookDTO>();

            if (id == 0) lst = JsonConvert.DeserializeObject<List<BookDTO>>(responseString);
            else lst.Add(JsonConvert.DeserializeObject<BookDTO>(responseString));

            return lst;
        }
    }
}
