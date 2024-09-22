using RestSharp;
using System.Threading.Tasks;

public class AGDATAApiClient
{
    private readonly RestClient _client;

    public AGDATAApiClient()
    {
        _client = new RestClient("https://jsonplaceholder.typicode.com");
    }

    public async Task<RestResponse> GetPostsAsync()
    {
        var request = new RestRequest("/posts", Method.Get);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> CreatePostAsync(object postBody)
    {
        var request = new RestRequest("/posts", Method.Post);
        request.AddJsonBody(postBody);
        return await _client.ExecuteAsync(request);
    }
    public async Task<RestResponse> UpdatePostAsync(int postId, object postBody)
    {
        var request = new RestRequest($"/posts/{postId}", Method.Put);
        request.AddJsonBody(postBody);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> DeletePostAsync(int postId)
    {
        var request = new RestRequest($"/posts/{postId}", Method.Delete);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> CreateCommentForPostAsync(int postId, object commentBody)
    {
        var request = new RestRequest($"/posts/{postId}/comments", Method.Post);
        request.AddJsonBody(commentBody);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> GetCommentsForPostAsync(int postId)
    {
        var request = new RestRequest($"/comments?postId={postId}", Method.Get);
        return await _client.ExecuteAsync(request);
    }





}
