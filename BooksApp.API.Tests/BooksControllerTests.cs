using BooksApp.API.Controllers;
using BooksApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;

namespace BooksApp.API.Tests
{
    public class BooksControllerTests
    {
        private readonly BooksController _sut = new BooksController();

        [Test]
        public void Get_List_Of_Books()
        {
            var result = _sut.Get().Result;

            Assert.That(result.Count(),Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void Get_Book_By_Id()
        {
            int id = 1;

            var result = _sut.GetById(1).Result;

            Assert.That(result.Value.Id, Is.EqualTo(id));
        }

        [Test]
        public void Book_Doesnt_Exist_Throws_Not_Found()
        {
            var id = 0;

            var result = _sut.GetById(id).Result.Result;

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Add_Book()
        {
            var book = new Book() {Title = "TestBook", PageCount = 50, Excerpt = "TestBook Summary", Description="Book Description", PublishDate = DateTime.Now };

            var result = _sut.AddBook(book).Result.Value;
            
            Assert.That(result, Is.InstanceOf<Book>());
        }

        [DatapointSource]
        Book[] books = new Book[] { new Book() { PageCount = 50, Excerpt = "TestBook Without Tittle", Description = "Book Description", PublishDate = DateTime.Now },
        new Book() { Title="TestBook Without Pagecount", Excerpt = "TestBook Summary", Description = "Book Description", PublishDate = DateTime.Now }};
        [Theory]
        public void Book_Without_Tittle_Or_PageCount_Throws_BadRequest(Book book)
        {
            var result = _sut.AddBook(book).Result.Result;

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}