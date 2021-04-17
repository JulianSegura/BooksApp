using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Services
{
    //Servicio para conectarse al fakeApi
    public class BookServices
    {
        private HttpClient _client = new HttpClient();
        private readonly string _path = "https://fakerestapi.azurewebsites.net/api/v1/Books";

        public HttpResponseMessage GetAllAsync()
        {
            return  _client.GetAsync(_path).Result;
        }

        public HttpResponseMessage GetByIdAsync(int id)
        {
            var url = $"{_path}/{id}";
            return _client.GetAsync(url).Result;
        }

        public HttpResponseMessage PostAsync(HttpContent content)
        {
            return _client.PostAsync(_path, content).Result;
        }

        public HttpResponseMessage PutAsync(int id, HttpContent content)
        {
            var url = $"{_path}/{id}";
            return  _client.PutAsync(url, content).Result;
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var url = $"{_path}/{id}";
            return await _client.DeleteAsync(url);
        }

        public bool Exists(int id)
        {
            var url = $"{_path}/{id}";
            var result = _client.GetAsync(url).Result;

            if (result.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
