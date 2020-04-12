using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APItestRestSharp
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SimpleGetRequest()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}", Method.GET);
            request.AddUrlSegment("postid", 1);

            var response = client.Execute(request);

            var deserial = new JsonDeserializer();
            var outPut = deserial.Deserialize<Dictionary<string, string>>(response);
            var result = outPut["author"];
            Assert.That(result, Is.EqualTo("sarfraz"));
        }

        [Test]
        public void PostwithAnnonymusBody()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("profile", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { Name = "Ali" });


            var response = client.Execute(request);
            var deserial = new JsonDeserializer();
            var outPut = deserial.Deserialize<Dictionary<string, string>>(response);
            var result = outPut["Name"];
            Assert.That(result, Is.EqualTo("Ali"));

        }

        [Test]
        public void PostwithTypeBody()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new posts() { id = 30, author = "Rao", title = "film"});


            var response = client.Execute(request);
            var deserial = new JsonDeserializer();
            var outPut = deserial.Deserialize<Dictionary<string, string>>(response);
            var result = outPut["author"];
            Assert.That(result, Is.EqualTo("Rao"));

        }

        [Test]
        public void PostwithBodyusingGeneric()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new posts() { id = 39, author = "Rao", title = "film" });


            var response = client.Execute<posts>(request);

            Assert.That((int)response.StatusCode, Is.EqualTo(201));
            Assert.That(response.Data.author, Is.EqualTo("Rao"));

        }

    }

    public class posts 
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
    }
}