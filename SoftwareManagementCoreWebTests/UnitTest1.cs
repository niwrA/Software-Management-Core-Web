using CommandsShared;
using SoftwareManagementCoreWeb;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;

namespace SoftwareManagementCoreWebTests
{
    public class CommandDto
    {
        public string CommandTypeId { get; set; }
        public Guid EntityGuid { get; set; }
        public DateTime? ExecutedOn { get; set; }
        public Guid Guid { get; set; }
        public string ParametersJson { get; set; }
    }
    public class UnitTest1 : IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient _client;
        public UnitTest1(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
        }
        [Fact]
        public async Task CreatePostReturnsCreatedCommandAsync()
        {
            // Arrange
            var testGuid = Guid.NewGuid();
            var newIdea = new CommandDto {  CommandTypeId = "CreateProductCommand", EntityGuid = testGuid };

            // Act
            var response = await _client.PostAsJsonAsync("/api/commands", newIdea);

            // Assert
            response.EnsureSuccessStatusCode();
            var returnedSession = await response.Content.ReadAsJsonAsync<CommandDto>();
            Assert.Equal(testGuid, returnedSession.EntityGuid);
        }
    }
}
