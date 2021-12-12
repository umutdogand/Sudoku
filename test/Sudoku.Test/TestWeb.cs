using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Sudoku.Test
{
    public class TestWeb
    {
        [Theory]
        [InlineData(true, 9, 30)]
        public async void TestGameBoard(bool uniqueControl, int size, int difficulty)
        {
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();
            var apiResponse = await client.GetAsync($"/Sudoku/GameBoard?uniqueControl={uniqueControl}&diffuculty={difficulty}&boardSize={size}");
            Assert.True(apiResponse.IsSuccessStatusCode);

        }

        [Fact]
        public async void TestSolver()
        {
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();
            var apiResponse = await client.GetAsync($"/Sudoku/Solver");
            Assert.True(apiResponse.IsSuccessStatusCode);

        }
    }
}