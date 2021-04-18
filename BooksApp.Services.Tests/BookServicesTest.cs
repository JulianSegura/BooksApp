using BooksApp.API.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BooksApp.Services.Tests
{
    public class BookServicesTest
    {
        private readonly BookServices _sut = BookServices.Start();
        
        [Test]
        public void Service_Connection_Is_Working()
        {
            var expected = HttpStatusCode.OK;

            var result = _sut.GetAllAsync().StatusCode;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Return_List_Of_Books()
        {
            var resultString = _sut.GetAllAsync().Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<Book>>(resultString);

            Assert.That(result, Is.InstanceOf<IList<Book>>());
        }

        [Test]
        public void Book_Exists()
        {
            int id = 1;
            bool expected = true;

            var result = _sut.Exists(id);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Book_Doesnt_Exists()
        {
            int id = 0;
            bool expected = false;

            var result = _sut.Exists(id);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Finds_And_Retuns_Existing_Book_By_Id()
        {
            int id = 1;
            var expected = HttpStatusCode.OK;

            var result = _sut.GetByIdAsync(id).StatusCode;

            Assert.That(result,Is.EqualTo(expected));
        }

    }
}
