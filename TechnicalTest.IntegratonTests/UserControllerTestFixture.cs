using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using TechnicalTest.Data;
using TechnicalTest.Models;

namespace TechnicalTest.IntegratonTests
{
    public class UserControllerTestFixture
    {
        private IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        private TestServer _testServer;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            var builder = new WebHostBuilder().UseConfiguration(_configuration).UseStartup<Startup>();
            _testServer = new TestServer(builder);
            _client = _testServer.CreateClient();
        }

        [Test]
        public async Task GivenANewUser_IShouldSeeTheUserIsCreated()
        {
            // Arrange
            var username = $"NewUser{Guid.NewGuid()}";
            var user = new User() { Name = username };
            var userRepository = (IUserRepository)_testServer.Services.GetService(typeof(IUserRepository));

            // Act
            await Post(_client, user);

            // Assert
            var result = await userRepository.Get(username);
            Assert.AreEqual(result.Name, username);
        }

        [Test]
        public async Task GivenUsers_IShouldSeeTheUsers()
        {
            // Arrange
            var userRepository = (IUserRepository)_testServer.Services.GetService(typeof(IUserRepository));

            var username1 = $"NewUser{Guid.NewGuid()}";
            var user1 = new Data.Models.User() { Name = username1 };
            await userRepository.Create(user1);

            var username2 = $"NewUser{Guid.NewGuid()}";
            var user2 = new Data.Models.User() { Name = username2 };
            await userRepository.Create(user2);

            var username3 = $"NewUser{Guid.NewGuid()}";
            var user3 = new Data.Models.User() { Name = username3 };
            await userRepository.Create(user3);

            // Act
            var response = await _client.GetAsync("/User");

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            var result = await response.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(result);

            Assert.IsTrue(users.Any(x => x.Name == username1));
            Assert.IsTrue(users.Any(x => x.Name == username2));
            Assert.IsTrue(users.Any(x => x.Name == username3));
        }

        [Test]
        public async Task GivenAUserIsDeleted_IShouldNoLongerBeAbleToGetTheUser()
        {
            throw new NotImplementedException();
        }

        private async Task Post(HttpClient client, User user)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(user);
            var content = new StringContent(jsonString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PostAsync("/User", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
