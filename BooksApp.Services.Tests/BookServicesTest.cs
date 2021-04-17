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
        private readonly BookServices _sut = new BookServices();
        
        [Test]
        public void Connects_And_Returns_All_Books()
        {
            var result = _sut.GetAllAsync().StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [Test]
        public void Finds_And_Retuns_Existing_Book_By_Id()
        {
            int id = 1;
            var result = _sut.GetByIdAsync(id);

            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        [Test]
        public void Book_Doesnt_Exists()
        {
            int id = 0;
            var result = _sut.Exists(id);

            Assert.IsFalse(result);
        }
    }
}
