using System.Collections.Generic;
using App.Interfaces;
using App.Models;
using App.Services;
using Xunit;

namespace Test
{
    public class PrintGranuleServiceTests
    {
        private readonly IPrintGranuleService _fileReaderService;

        public PrintGranuleServiceTests()
        {
            _fileReaderService = new PrintGranuleService();
        }

        [Fact]
        public void Print_WhenPutGranuleSet_ThenShouldPreparePrint()
        {
            //Arrange
            var granuleSet = new GranuleSet()
                {new Granule(new[] {1, 0, 1}, 1), new Granule(new[] {1, 1, 1}, 2), new Granule(new[] {0, 0, 1}, 3)};

            //Act
            var result = _fileReaderService.Print(granuleSet);

            //Assert
            var expected = new List<string> {";g(u1);g(u2);g(u3)", "u1;1;1;0", "u2;0;1;0", "u3;1;1;1"};
            Assert.Equal(expected, result);
        }
    }
}

