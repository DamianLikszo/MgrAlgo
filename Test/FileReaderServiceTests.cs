using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using Moq;
using magisterka.Validators;
using magisterka.Wrappers;
using Xunit;

namespace Test
{
    public class FileReaderServiceTests
    {
        private readonly Mock<IMyMessageBox> _myMessageBoxMock;
        private readonly Mock<ICoverageFileValidator> _coverageFileValidatorMock;
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly FileReaderService _fileReaderService;

        public FileReaderServiceTests()
        {
            _myMessageBoxMock = new Mock<IMyMessageBox>();
            _coverageFileValidatorMock = new Mock<ICoverageFileValidator>();
            _fileServiceMock = new Mock<IFileService>();

            _fileReaderService = new FileReaderService(_myMessageBoxMock.Object, _coverageFileValidatorMock.Object,
                _fileServiceMock.Object);
        }

        [Fact]
        public void ConvertContentToData_WhenPassRightContent_ThenShouldConvertToData()
        {
            // Arrange
            var content = new List<string>{"1;2;3", "1;2;3", "1;2;3"};
            
            // Act
            var result = _fileReaderService.ConvertContentToCoverageData(content);

            // Assert
            var expect = new CoverageData(new List<List<int>>
                {new List<int> {1, 2, 3}, new List<int> {1, 2, 3}, new List<int> {1, 2, 3}});
            Assert.Equal(expect.Data, result.Data);
        }

        [Fact]
        public void ConvertContentToData_WhenPassContentWithWrongChars_ThenShouldReturnNull()
        {
            // Arrange
            var content = new List<string> { "1;b;3", "1;2;3", "a;2;3" };
            
            // Act
            var result = _fileReaderService.ConvertContentToCoverageData(content);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ConvertContentToData_WhenPassContentWithWrongChars_ThenShouldShowMessage()
        {
            // Arrange
            var content = new List<string> { "1;b;3", "1;2;3", "a;2;3" };
            
            // Act
            _fileReaderService.ConvertContentToCoverageData(content);

            // Assert
            _myMessageBoxMock.Verify(x => x.Show(It.IsAny<string>()), Times.Once);
        }
        
    }
}
