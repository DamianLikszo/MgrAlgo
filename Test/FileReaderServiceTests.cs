using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using Moq;
using Xunit;

namespace Test
{
    public class FileReaderServiceTests
    {
        private readonly IFileReaderService _fileReaderService;
        private readonly Mock<IFileService> _fileServiceMock;

        public FileReaderServiceTests()
        {
            _fileServiceMock = new Mock<IFileService>();
            _fileReaderService = new FileReaderService(_fileServiceMock.Object);
        }

        [Fact]
        public void SaveFile_WhenDoNotChooseFile_ThenShouldReturnFalseWithoutError()
        {
            //Arrange
            var granuleSet = new GranuleSet
                {new Granule(new[] {1, 0, 1}), new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 0, 1})};

            //Act
            var result = _fileReaderService.SaveFile(granuleSet, out var error);

            //Assert
            Assert.False(result);
            Assert.Null(error);
        }

        [Fact]
        public void SaveFile_WhenPathIsNull_ThenShouldReturnFalseWithError()
        {
            //Arrange
            var granuleSet = new GranuleSet
                {new Granule(new[] {1, 0, 1}), new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 0, 1})};
            _fileServiceMock.Setup(x => x.GetPathFromSaveFileDialog(It.IsAny<string>())).Returns(string.Empty);

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
            Assert.Null(error);
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

