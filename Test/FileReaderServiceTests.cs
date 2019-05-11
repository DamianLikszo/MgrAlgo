using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using magisterka.Validators;
using Moq;
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
        public void OpeAndReadFile_WhenPathIsNull_ThenShouldReturnNull()
        {
            //Arrange

            //Act
            var result = _fileReaderService.OpenAndReadFile();

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void OpeAndReadFile_WhenContentIsNull_ThenShouldReturnNull()
        {
            //Arrange
            var path = "path";
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog()).Returns(path);

            //Act
            var result = _fileReaderService.OpenAndReadFile();

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void OpeAndReadFile_WhenCoverageDataIsNull_ThenShouldReturnNull()
        {
            //Arrange
            var path = "path";
            var content = new List<string> { "1;0;1", "1;1;1", "0;0;1" };
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog()).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path)).Returns(content);

            //Act
            var result = _fileReaderService.OpenAndReadFile();

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void OpeAndReadFile_WhenCoverageFileIsWrong_ThenShouldReturnNull()
        {
            //Arrange
            var path = "path";
            var content = new List<string> { "1;0;1", "1;1;1", "0;0;1" };
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 0, 1}, new List<int> {1, 1, 1}, new List<int> {0, 0, 1}});

            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog()).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(content)).Returns(coverageData);
            _coverageFileValidatorMock.Setup(x => x.ValidAndShow(It.IsAny<CoverageFile>())).Returns(false);

            //Act
            var result = _fileReaderService.OpenAndReadFile();

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void OpeAndReadFile_WhenEverythingIsFine_ThenShouldReturnCoverageFile()
        {
            //Arrange
            var path = "path";
            var content = new List<string> { "1;0;1", "1;1;1", "0;0;1" };
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 0, 1}, new List<int> {1, 1, 1}, new List<int> {0, 0, 1}});

            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog()).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(content)).Returns(coverageData);
            _coverageFileValidatorMock.Setup(x => x.ValidAndShow(It.IsAny<CoverageFile>())).Returns(true);

            //Act
            var result = _fileReaderService.OpenAndReadFile();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(path, result.Path);
            Assert.Equal(coverageData, result.CoverageData);
        }

        [Fact]
        public void SaveFile_WhenPathIsNull_ThenShouldReturnFalse()
        {
            //Arrange
            var granuleSet = new GranuleSet
                {new Granule(new[] {1, 0, 1}), new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 0, 1})};

            //Act
            var result = _fileReaderService.SaveFile(granuleSet);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void SaveFile_WhenEverythingIsFine_ThenShouldReturnTrue()
        {
            //Arrange
            var path = "path";
            var granuleSet = new GranuleSet()
                {new Granule(new[] {1, 0, 1}), new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 0, 1})};
            _fileServiceMock.Setup(x => x.GetPathFromSaveFileDialog()).Returns(path);
            _fileServiceMock.Setup(x => x.SaveFile(path, It.IsAny<List<string>>())).Returns(true);

            //Act
            var result = _fileReaderService.SaveFile(granuleSet);

            //Assert
            Assert.True(result);
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

