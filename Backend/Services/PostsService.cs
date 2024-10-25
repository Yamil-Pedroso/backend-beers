using Backend.DTOs;
using System.Text.Json;

namespace Backend.Services
{
    public class PostsService : IPostsService
    {
        private HttpClient _httpClient;

        public PostsService(HttpClient httpClient) // Generalmente los objetos no se deben inicializar dentro de otra clase, esto es proposito de prueba
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PostDto>> Get()
        {
            var result = await _httpClient.GetAsync(_httpClient.BaseAddress); // Aca podemos despues de _httpClient.(get, post, put, delete...)
            var body = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var post = JsonSerializer.Deserialize<IEnumerable<PostDto>>(body, options);


            if (post == null)
            {
                throw new InvalidOperationException("Failed to deserialize posts.");
            }

            return post;
        }
    }
}
