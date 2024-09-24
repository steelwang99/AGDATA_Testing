using NUnit.Framework;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using log4net;
using System.Net;

namespace AGDATAApiTesting
{
    [TestFixture]
    public class AGDATAApiClientTests
    {
        private AGDATAApiClient _client;
        // Initialize logger
        private static readonly ILog log = LogManager.GetLogger(typeof(AGDATAApiClientTests));

        [SetUp]
        public void Setup()
        {
            log.Info("Setting up the API client for testing.");
            // Initialize the API client
            _client = new AGDATAApiClient();
        }

        [Test]
        public async Task Test_01_GetPosts_ReturnsOk()
        {
            log.Info("Starting Test_01_GetPosts_ReturnsOk");

            // Action
            RestResponse response = await _client.GetPostsAsync();
            log.Info("GET /posts request sent.");

            // Assert
            Assert.AreEqual(200, (int)response.StatusCode, "Expected status code 200 OK");
            log.Info("Received status code 200 OK.");

            Assert.IsNotEmpty(response.Content, "Response content should not be empty");
            log.Info("Response content is not empty.");

            // Parse the response and validate content
            JArray posts = JArray.Parse(response.Content);
            Assert.IsTrue(posts.Count > 0, "Expected at least one post");
            log.Info($"Found {posts.Count} posts.");
        }

        [Test]
        // Data driven
        [TestCase("SteelAutomation1", "This is for automation test1", 1)]
        [TestCase("SteelAutomation2", "This is for automation test2", 2)]
        [TestCase("SteelAutomation3", "This is for automation test3", 3)]
        public async Task Test_02_CreatePost_ReturnsCreated(string title, string body, int userId)
        {
            log.Info("Starting Test_02_CreatePost_ReturnsCreated");

            var postBody = new
            {
                title = title,
                body = body,
                userId = userId
            };

            // Add data driven
            RestResponse response = await _client.CreatePostAsync(postBody);
            log.Info("POST /posts request sent.");

            // Assert
            Assert.AreEqual(201, (int)response.StatusCode, "Expected status code 201 Created");
            log.Info("Received status code 201 Created.");

            JObject post = JObject.Parse(response.Content);
            Assert.AreEqual(title, post["title"].ToString());
            Assert.AreEqual(body, post["body"].ToString());
            log.Info($"Record created successfully with title '{title}' and body '{body}'.");
        }

        [Test]
        // Add data driven tests
        [TestCase(4, "SteelAutomation4", "This is for automation test4", 4)]
        [TestCase(5, "SteelAutomation5", "This is for automation test5", 5)]
        [TestCase(6, "SteelAutomation6", "This is for automation test6", 6)]
        public async Task Test_03_UpdatePost_ReturnsOk(int postId, string title, string body, int userId)
        {
            log.Info($"Starting Test_03_UpdatePost_ReturnsOk for postId: {postId}, title: {title}, body: {body}, userId: {userId}");
            // Arrange
            var updatedPost = new
            {
                id = postId,
                title = title,
                body = body,
                userId = userId
            };

            // Act
            RestResponse response = await _client.UpdatePostAsync(postId, updatedPost);
            log.Info($"PUT /posts/{postId} request sent.");

            // Assert
            Assert.AreEqual(200, (int)response.StatusCode, "Expected status code 200 OK");
            log.Info("Received status code 200 OK.");

            JObject post = JObject.Parse(response.Content);
            Assert.AreEqual(title, post["title"].ToString(), $"Expected title to be '{title}'");
            Assert.AreEqual(body, post["body"].ToString(), $"Expected body to be '{body}'");
            log.Info($"Post updated successfully for postId {postId} with title '{title}' and body '{body}'.");
        }

        [Test]
        // Add data driven tests
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task Test_04_DeletePost_ReturnsOk(int postId)
        {
            log.Info("Starting Test_04_DeletePost_ReturnsOk");

            // Action
            RestResponse response = await _client.DeletePostAsync(postId);
            log.Info($"DELETE /posts/{postId} request sent.");

            // Assert
            Assert.AreEqual(200, (int)response.StatusCode, "Expected status code 200 OK");
            log.Info("Post deleted successfully, received status code 200 OK.");
        }

        [Test]
        // Add data driven tests
        [TestCase(7, "Steel1", "This is a comment from Steel1.")]
        [TestCase(8, "Steel2", "This is a comment from Steel1.")]
        [TestCase(9, "Steel3", "This is a comment from Steel1.")]
        public async Task Test_05_CreateCommentForPost_ReturnsCreated(int postId, string name, string body)
        {
            log.Info($"Starting Test_05_CreateCommentForPost_ReturnsCreated for postId: {postId}, name: {name}");

            var commentBody = new
            {
                name = name,
                body = body,
                postId = postId
            };

            // Action
            RestResponse response = await _client.CreateCommentForPostAsync(postId, commentBody);
            log.Info($"POST /posts/{postId}/comments request sent.");

            // Assert
            Assert.AreEqual(201, (int)response.StatusCode, "Expected status code 201 Created");
            log.Info($"Comment created successfully, received status code 201 Created for postId: {postId}.");

            JObject comment = JObject.Parse(response.Content);
            Assert.AreEqual(name, comment["name"].ToString(), $"Expected name to be '{name}'");
            Assert.AreEqual(body, comment["body"].ToString(), $"Expected body to be '{body}'");
            log.Info($"Comment created successfully for postId {postId} with name '{name}', body '{body}'.");
        }

        [Test]
        // Add data driven
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task Test_06_GetCommentsForPost_ReturnsOk(int postId)
        {
            log.Info($"Starting Test_06_GetCommentsForPost_ReturnsOk for postId: {postId}");

            // Action
            RestResponse response = await _client.GetCommentsForPostAsync(1);
            log.Info($"GET /comments?postId= {postId} request sent.");

            // Assert
            Assert.AreEqual(200, (int)response.StatusCode, $"Expected status code 200 OK for postId: {postId}.");
            log.Info($"Received status code 200 OK for postId: {postId}.");

            Assert.IsNotEmpty(response.Content, $"Comments content should not be empty for postId: {postId}.");
            log.Info("Comments content is not empty for postId: {postId}.");

            // Parse response and validate content
            JArray comments = JArray.Parse(response.Content);
            Assert.IsTrue(comments.Count > 0, $"Expected at least one comment for postId: {postId}.");
            log.Info($"Found {comments.Count} comments for postId: {postId}.");
        }
    }
}
