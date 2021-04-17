using BooksApp.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
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
    }
}