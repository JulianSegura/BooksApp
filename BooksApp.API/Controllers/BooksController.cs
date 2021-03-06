using BooksApp.API.Models;
using BooksApp.API.Validations;
using BooksApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BooksApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookServices _bookService;
        private readonly IValidator<Book> _validator;
        public BooksController()
        {
            _bookService = BookServices.Start();
            _validator = new BookValidator(_bookService);
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            var result = _bookService.GetAllAsync();
            var resultResponse = await result.Content.ReadAsStringAsync();
            var booksList = JsonConvert.DeserializeObject<List<Book>>(resultResponse);

            return booksList;
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            if (!_bookService.Exists(id)) return NotFound();

            var result = _bookService.GetByIdAsync(id);
            var resultResponse = await result.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<Book>(resultResponse);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book book)
        {
            var validationResult = _validator.Validate(book);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            //ToDo: Explore option of creating an AddBookDTO. That way I can avoid the ID being sent from the FrontEnd

            var httpContent = CreateHttptStringContent(book);
            
            var result = _bookService.PostAsync(httpContent);
            //ToDo: Check statuscode to return appropiate error;
            var resultResponse = await result.Content.ReadAsStringAsync();
                
            return JsonConvert.DeserializeObject<Book>(resultResponse);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book bookUpdate)
        {
            //ToDo: Add validation to bookUpdate (try fluent validation)

            var book =  JsonConvert.DeserializeObject<Book>(await _bookService.GetByIdAsync(id).Content.ReadAsStringAsync());
            if (book == null) return NotFound(bookUpdate);

            book.Title = bookUpdate.Title;
            book.Description = bookUpdate.Description;
            book.PageCount = bookUpdate.PageCount;
            book.Excerpt = bookUpdate.Excerpt;
            book.PublishDate = bookUpdate.PublishDate;

            var httpContent = CreateHttptStringContent(book);

            var result = _bookService.PutAsync(id, httpContent);
            var resultResponse = await result.Content.ReadAsStringAsync();

            return Ok(JsonConvert.DeserializeObject<Book>(resultResponse));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_bookService.Exists(id)) return NotFound();

            await _bookService.DeleteAsync(id);

            return Ok();
        }

        private StringContent CreateHttptStringContent(Book book)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(book));
            httpContent.Headers.ContentType.MediaType = "application/json";

            return httpContent;
        }
    }
}
