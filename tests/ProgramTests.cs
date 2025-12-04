using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using Xunit;

namespace DevOpsDemo.Tests;

public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ProgramTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetHome_ReturnsSuccessAndCorrectContent()
    {
        // Act
        var response = await _client.GetAsync("/");
        
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);
        
        Assert.True(json.RootElement.TryGetProperty("message", out _));
        Assert.Equal("running", json.RootElement.GetProperty("status").GetString());
    }

    [Fact]
    public async Task GetHealth_ReturnsHealthy()
    {
        // Act
        var response = await _client.GetAsync("/health");
        
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);
        
        Assert.Equal("healthy", json.RootElement.GetProperty("status").GetString());
        Assert.True(json.RootElement.TryGetProperty("timestamp", out _));
    }

    [Fact]
    public async Task GetInfo_ReturnsApplicationInfo()
    {
        // Act
        var response = await _client.GetAsync("/api/info");
        
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);
        
        Assert.True(json.RootElement.TryGetProperty("app", out _));
        Assert.True(json.RootElement.TryGetProperty("features", out var features));
        Assert.True(features.GetArrayLength() > 0);
    }

    [Fact]
    public async Task AllEndpoints_ReturnApplicationJson()
    {
        // Arrange
        var endpoints = new[] { "/", "/health", "/api/info" };
        
        // Act & Assert
        foreach (var endpoint in endpoints)
        {
            var response = await _client.GetAsync(endpoint);
            Assert.Contains("application/json", 
                response.Content.Headers.ContentType?.ToString());
        }
    }
}
