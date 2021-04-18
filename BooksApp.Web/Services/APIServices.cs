using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BooksApp.Web.Services
{
    public abstract class APIServices
    {
        //Im assuming the front and the back are 2 projects completely separated. 
        //They dont share any code, the API is exposed and the WebApp consumes it.
        //This abstract class is controller agnostic, I pass the controller name on the constructor.

        private readonly HttpClient _client = new HttpClient();
        private readonly string _path = "https://localhost:44331";

        public APIServices(string controller)
        {
            _path = $"{_path}/{controller}";
        }

        public HttpResponseMessage GetAllAsync()
        {
            return _client.GetAsync(_path).Result;
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
            return _client.PutAsync(url, content).Result;
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
