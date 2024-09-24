using RestSharp;
using System.Threading.Tasks;
using log4net;
using System.Reflection;

namespace AGDATAApiTesting
{
    public class AGDATAApiClient
    {
        // Initialize logger
        private static readonly ILog log = LogManager.GetLogger(typeof(AGDATAApiClient));
        private readonly RestClient _client;


        public AGDATAApiClient()
        {
            _client = new RestClient("https://jsonplaceholder.typicode.com");
            log.Info("Initialized RestClient with the base URL: https://jsonplaceholder.typicode.com");
        }

        public async Task<RestResponse> GetPostsAsync()
        {
            var request = new RestRequest("/posts", Method.Get);
            log.Info("Sending GET request to /posts endpoint");

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                log.Info("GET request from /posts successfully");
            }
            else
            {
                log.Error($"GET request from /posts endpoint failed with status code: {response.StatusCode}");
            }

            return response;
        }

        public async Task<RestResponse> CreatePostAsync(object postBody)
        {
            var request = new RestRequest("/posts", Method.Post);
            request.AddJsonBody(postBody);
            log.Info($"Sending POST request to /posts endpoint with body: {postBody}");

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                log.Info("POST request to /posts endpoint successful");
            }
            else
            {
                log.Error($"POST request to /posts endpoint failed with status code: {response.StatusCode}");
            }

            return response;
        }

        public async Task<RestResponse> UpdatePostAsync(int postId, object postBody)
        {
            var request = new RestRequest($"/posts/{postId}", Method.Put);
            request.AddJsonBody(postBody);
            log.Info($"Sending PUT request to /posts/{postId} with body: {postBody}");

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                log.Info($"PUT request to /posts/{postId} successful");
            }
            else
            {
                log.Error($"PUT request to /posts/{postId} failed with status code: {response.StatusCode}");
            }

            return response;
        }

        public async Task<RestResponse> DeletePostAsync(int postId)
        {
            var request = new RestRequest($"/posts/{postId}", Method.Delete);
            log.Info($"Sending DELETE request to /posts/{postId}");

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                log.Info($"DELETE request to /posts/{postId} successful");
            }
            else
            {
                log.Error($"DELETE request to /posts/{postId} failed with status code: {response.StatusCode}");
            }

            return response;
        }

        public async Task<RestResponse> CreateCommentForPostAsync(int postId, object commentBody)
        {
            var request = new RestRequest($"/posts/{postId}/comments", Method.Post);
            request.AddJsonBody(commentBody);
            log.Info($"Sending POST request to /posts/{postId}/comments with body: {commentBody}");

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                log.Info($"POST request to /posts/{postId}/comments successful");
            }
            else
            {
                log.Error($"POST request to /posts/{postId}/comments failed with status code: {response.StatusCode}");
            }

            return response;
        }

        public async Task<RestResponse> GetCommentsForPostAsync(int postId)
        {
            var request = new RestRequest($"/comments?postId={postId}", Method.Get);
            log.Info($"Sending GET request to /comments?postId={postId}");

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                log.Info($"GET request to /comments?postId={postId} successful");
            }
            else
            {
                log.Error($"GET request to /comments?postId={postId} failed with status code: {response.StatusCode}");
            }

            return response;
        }
    }
}

