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
        public void Connects_And_Returns_All_Books()
        {
            var expected = HttpStatusCode.OK;

            var result = _sut.GetAllAsync().StatusCode;

            Assert.That(result, Is.EqualTo(expected));
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

            var result = _sut.GetByIdAsync(id);

            Assert.IsTrue(result.IsSuccessStatusCode);
        }

    }
}
