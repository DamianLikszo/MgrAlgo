using System;
using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Services;
using Moq;
using System.IO;
using System.Text;
using magisterka.Validators;
using Xunit;

namespace Test
{
    public class FileReaderServiceTests
    {
        private readonly Mock<IStreamReader> _readerMock;
        private readonly Mock<IMyMessageBox> _messageBoxMock;
        private readonly Mock<ICoverageFileValidator> _coverageFileValidatorMock;
        private readonly FileReaderService _fileReaderService;

        public FileReaderServiceTests()
        {
            _readerMock = new Mock<IStreamReader>();
            _messageBoxMock = new Mock<IMyMessageBox>();
            _coverageFileValidatorMock = new Mock<ICoverageFileValidator>();

            _fileReaderService = new FileReaderService(_readerMock.Object, _messageBoxMock.Object,
                _coverageFileValidatorMock.Object);
        }

        [Fact]
        public void ReadFile_WhenPutRightFile_ThenShouldReturnContent()
        {
            //Arrange
            var path = "path";
            var content = $"1;2;3;{Environment.NewLine}1;2;3;{Environment.NewLine}1;2;3;{Environment.NewLine}";              
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(content));
            _readerMock.Setup(x => x.GetReader(path)).Returns(new StreamReader(ms));
            
            //Act
            var result = _fileReaderService.ReadFile(path);

            //Assert
            var expect = new List<string> {"1;2;3;", "1;2;3;", "1;2;3;"};
            Assert.Equal(expect, result);
        }

        [Fact]
        public void ReadFile_WhenStreamReaderThrowException_ThenReturnNull()
        {
            //Arrange
            var path = "path";
            _readerMock.Setup(x => x.GetReader(path)).Throws(new Exception()); 
            
            //Act
            var result = _fileReaderService.ReadFile(path);
            
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void ConvertContentToData_WhenPassRightContent_ThenShouldConvertToData()
        {
            // Arrange
            var content = new List<string>{"1;2;3", "1;2;3", "1;2;3"};
            
            // Act
            var result = _fileReaderService.ConvertContentToData(content);

            // Assert
            var expect = new List<List<int>>
                {new List<int> {1, 2, 3}, new List<int> {1, 2, 3}, new List<int> {1, 2, 3}};
            Assert.Equal(expect, result);
        }

        [Fact]
        public void ConvertContentToData_WhenPassContentWithWrongChars_ThenShouldReturnNull()
        {
            // Arrange
            var content = new List<string> { "1;b;3", "1;2;3", "a;2;3" };
            
            // Act
            var result = _fileReaderService.ConvertContentToData(content);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ConvertContentToData_WhenPassContentWithWrongChars_ThenShouldShowMessage()
        {
            // Arrange
            var content = new List<string> { "1;b;3", "1;2;3", "a;2;3" };
            
            // Act
            _fileReaderService.ConvertContentToData(content);

            // Assert
            _messageBoxMock.Verify(x => x.Show(It.IsAny<string>()), Times.Once);
        }
        
    }
}
