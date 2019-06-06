using System.Collections.Generic;
using App.Interfaces;
using App.Models;
using App.Services;
using Xunit;

namespace Test
{
    public class CoverageDataConverterTests
    {
        private readonly ICoverageDataConverter _coverageDataConverter;

        public CoverageDataConverterTests()
        {
            _coverageDataConverter = new CoverageDataConverter();
        }

        [Fact]
        public void ConvertContentToData_WhenPassRightContent_ThenShouldConvertToDataWithoutError()
        {
            // Arrange
            var content = new List<string>{"1;2;3", "1;2;3", "1;2;3"};
            
            // Act
            var result = _coverageDataConverter.Convert(content, out var error);

            // Assert
            var expected = new CoverageData(new List<List<int>>
                {new List<int> {1, 2, 3}, new List<int> {1, 2, 3}, new List<int> {1, 2, 3}});
            Assert.Equal(expected, result);
            Assert.Null(error);
        }


        [Fact]
        public void ConvertContentToData_WhenPassContentWithSeparatorOnEndOfLine_ThenShouldConvertToDataWithoutError()
        {
            // Arrange
            var content = new List<string> { "1;2;3;", "1;2;3", "1;2;3" };

            // Act
            var result = _coverageDataConverter.Convert(content, out var error);

            // Assert
            var expected = new CoverageData(new List<List<int>>
                {new List<int> {1, 2, 3}, new List<int> {1, 2, 3}, new List<int> {1, 2, 3}});
            Assert.Equal(expected, result);
            Assert.Null(error);
        }

        [Fact]
        public void ConvertContentToData_WhenPassContentWithWrongChars_ThenShouldReturnNullWithError()
        {
            // Arrange
            var content = new List<string> { "1;b;3", "1;2;3", "a;2;3" };
            
            // Act
            var result = _coverageDataConverter.Convert(content, out var error);

            // Assert
            Assert.Null(result);
            Assert.NotEmpty(error);
        }
    }
}
