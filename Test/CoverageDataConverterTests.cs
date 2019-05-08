using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using Moq;
using magisterka.Wrappers;
using Xunit;

namespace Test
{
    public class CoverageDataConverterTests
    {
        private readonly Mock<IMyMessageBox> _myMessageBoxMock;
        private readonly ICoverageDataConverter _coverageDataConverter;

        public CoverageDataConverterTests()
        {
            _myMessageBoxMock = new Mock<IMyMessageBox>();    
            _coverageDataConverter = new CoverageDataConverter(_myMessageBoxMock.Object);
        }

        [Fact]
        public void ConvertContentToData_WhenPassRightContent_ThenShouldConvertToData()
        {
            // Arrange
            var content = new List<string>{"1;2;3", "1;2;3", "1;2;3"};
            
            // Act
            var result = _coverageDataConverter.Convert(content);

            // Assert
            var expected = new CoverageData(new List<List<int>>
                {new List<int> {1, 2, 3}, new List<int> {1, 2, 3}, new List<int> {1, 2, 3}});
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ConvertContentToData_WhenPassContentWithWrongChars_ThenShouldReturnNull()
        {
            // Arrange
            var content = new List<string> { "1;b;3", "1;2;3", "a;2;3" };
            
            // Act
            var result = _coverageDataConverter.Convert(content);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ConvertContentToData_WhenPassContentWithWrongChars_ThenShouldShowMessage()
        {
            // Arrange
            var content = new List<string> { "1;b;3", "1;2;3", "a;2;3" };

            // Act
            _coverageDataConverter.Convert(content);

            // Assert
            _myMessageBoxMock.Verify(x => x.Show(It.IsAny<string>()), Times.Once);
        }
        
    }
}
