using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using magisterka.Validators;
using Moq;
using Test.Helpers;
using Xunit;

namespace Test
{
    public class FileReaderServiceTests
    {
        private readonly IFileReaderService _fileReaderService;
        private readonly Mock<ICoverageFileValidator> _coverageFileValidatorMock;
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly Mock<ICoverageDataConverter> _coverageDataConverterMock;

        public FileReaderServiceTests()
        {
            _coverageFileValidatorMock = new Mock<ICoverageFileValidator>();
            _fileServiceMock = new Mock<IFileService>();
            _coverageDataConverterMock = new Mock<ICoverageDataConverter>();
            _fileReaderService = new FileReaderService(_coverageFileValidatorMock.Object, _fileServiceMock.Object,
                _coverageDataConverterMock.Object);
        }

        [Fact]
        public void OpeAndReadFile_WhenPathIsNull_ThenShouldReturnNullAndError()
        {
            //Arrange

            //Act
            var result = _fileReaderService.OpenAndReadFile(out var error);

            //Assert
            Assert.Null(result);
            Assert.NotEmpty(error);
        }

        [Fact]
        public void OpeAndReadFile_WhenReadFileFailed_ThenShouldReturnNullAndErrorFromReadFileService()
        {
            //Arrange
            var path = "path";
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            string error;
            _fileServiceMock.Setup(x => x.ReadFile(path, out error))
                .Callback(CallbackOutErrorHelper.DelegateForObject1);
            
            //Act
            var result = _fileReaderService.OpenAndReadFile(out error);

            //Assert
            Assert.Null(result);
            Assert.Equal(CallbackOutErrorHelper.ErrorMessage, error);
        }

        [Fact]
        public void OpeAndReadFile_WhenConverterFailed_ThenShouldReturnNullAndErrorFromConverter()
        {
            //Arrange
            var path = "path";
            var content = new List<string> { "1;0;1", "1;1;1", "0;0;1" };

            string error;
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(content, out error))
                .Callback(CallbackOutErrorHelper.DelegateForObject1);

            //Act
            var result = _fileReaderService.OpenAndReadFile(out error);

            //Assert
            Assert.Null(result);
            Assert.Equal(CallbackOutErrorHelper.ErrorMessage, error);
        }

        [Fact]
        public void OpeAndReadFile_WhenCoverageFileValidatorReturnFalse_ThenShouldReturnNullAndErrorFromValidator()
        {
            //Arrange
            var path = "path";
            var content = new List<string> { "1;0;1", "1;1;1", "0;0;1" };
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 0, 1}, new List<int> {1, 1, 1}, new List<int> {0, 0, 1}});

            string error;
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(content, out error)).Returns(coverageData);
            _coverageFileValidatorMock.Setup(x => x.Valid(It.IsAny<CoverageFile>(), out error))
                .Callback(CallbackOutErrorHelper.DelegateForObject1).Returns(false);

            //Act
            var result = _fileReaderService.OpenAndReadFile(out error);

            //Assert
            Assert.Null(result);
            Assert.Equal(CallbackOutErrorHelper.ErrorMessage, error);
        }

        [Fact]
        public void OpeAndReadFile_WhenEverythingIsFine_ThenShouldReturnCoverageFileWithoutError()
        {
            //Arrange
            var path = "path";
            var content = new List<string> { "1;0;1", "1;1;1", "0;0;1" };
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 0, 1}, new List<int> {1, 1, 1}, new List<int> {0, 0, 1}});

            string error;
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(content, out error)).Returns(coverageData);
            _coverageFileValidatorMock.Setup(x => x.Valid(It.IsAny<CoverageFile>(), out error)).Returns(true);

            //Act
            var result = _fileReaderService.OpenAndReadFile(out error);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(path, result.Path);
            Assert.Equal(coverageData, result.CoverageData);
            Assert.True(string.IsNullOrEmpty(error));
        }

        [Fact]
        public void SaveFile_WhenPathIsNull_ThenShouldReturnFalseAndError()
        {
            //Arrange
            var granuleSet = new GranuleSet
                {new Granule(new[] {1, 0, 1}), new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 0, 1})};

            //Act
            var result = _fileReaderService.SaveFile(granuleSet, out var error);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(error);
        }

        [Fact]
        public void SaveFile_WhenEverythingIsFine_ThenShouldReturnTrueWithoutError()
        {
            //Arrange
            var path = "path";
            var granuleSet = new GranuleSet()
                {new Granule(new[] {1, 0, 1}), new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 0, 1})};
            _fileServiceMock.Setup(x => x.GetPathFromSaveFileDialog(It.IsAny<string>())).Returns(path);

            string error;
            _fileServiceMock.Setup(x => x.SaveFile(path, It.IsAny<List<string>>(), out error)).Returns(true);

            //Act
            var result = _fileReaderService.SaveFile(granuleSet, out error);

            //Assert
            Assert.True(result);
            Assert.True(string.IsNullOrEmpty(error));
        }

        [Fact]
        public void PreparePrint_WhenPutGranuleSet_ThenShouldPreparePrint()
        {
            //Arrange
            var granuleSet = new GranuleSet()
                {new Granule(new[] {1, 0, 1}), new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 0, 1})};

            //Act
            var result = _fileReaderService.PreparePrint(granuleSet);

            //Assert
            var expected = new List<string> {"g(u1);g(u2);g(u3)", "u1;1;1;0", "u2;0;1;0", "u3;1;1;1"};
            Assert.Equal(expected, result);
        }
    }
}

