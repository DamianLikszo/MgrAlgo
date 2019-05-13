using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using magisterka.Validators;
using Moq;
using Xunit;

namespace Test
{
    public class ActionsServiceTests
    {
        private readonly ActionsService _actionsService;
        private readonly IFormData _formData;
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly Mock<ICoverageDataConverter> _coverageDataConverterMock;
        private readonly Mock<ICoverageFileValidator> _coverageFileValidatorMock;
        private readonly Mock<IGranuleService> _granuleServiceMock;
        private readonly Mock<IGranuleSetDtoConverter> _granuleSetDtoConverterMock;

        public ActionsServiceTests()
        {
            _formData = new FormData();
            _fileServiceMock = new Mock<IFileService>();
            _coverageDataConverterMock = new Mock<ICoverageDataConverter>();
            _coverageFileValidatorMock = new Mock<ICoverageFileValidator>();
            _granuleServiceMock = new Mock<IGranuleService>();
            _granuleSetDtoConverterMock = new Mock<IGranuleSetDtoConverter>();
            _actionsService = new ActionsService(_formData, _fileServiceMock.Object, _coverageDataConverterMock.Object,
                _coverageFileValidatorMock.Object, _granuleServiceMock.Object, _granuleSetDtoConverterMock.Object);
        }

        [Fact]
        public void Load_WhenPathIsNull_ThenShouldReturnFalse()
        {
            //Arrange

            //Act
            var result = _actionsService.Load();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Load_WhenReadFileDoesNotReadContent_ThenShouldReturnFalse()
        {
            //Arrange
            var path = "path";
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);

            //Act
            var result = _actionsService.Load();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Load_WhenCoverageDataConverterDoesNotConvertData_ThenShouldReturnFalse()
        {
            //Arrange
            var path = "path";
            var content = new List<string> { "1;1;1", "1;0;1", "0;0;1" };
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path)).Returns(content);

            //Act
            var result = _actionsService.Load();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Load_WhenCoverageFileValidatorReturnFalse_ThenShouldReturnFalse()
        {
            //Arrange
            var path = "path";
            var content = new List<string> { "1;1;1", "1;0;1", "0;0;1" };
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 1, 1}, new List<int> {1, 0, 1}, new List<int> {0, 0, 1}});
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(content)).Returns(coverageData);
            _coverageFileValidatorMock.Setup(x => x.ValidAndShow(It.IsAny<CoverageFile>())).Returns(false);

            //Act
            var result = _actionsService.Load();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Load_WhenEverythingIsFine_ThenShouldReturnTrue()
        {
            //Arrange
            var path = "path";
            var content = new List<string> { "1;1;1", "1;0;1", "0;0;1" };
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 1, 1}, new List<int> {1, 0, 1}, new List<int> {0, 0, 1}});
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(content)).Returns(coverageData);
            _coverageFileValidatorMock.Setup(x => x.ValidAndShow(It.IsAny<CoverageFile>())).Returns(true);

            //Act
            var result = _actionsService.Load();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Load_WhenEverythingIsFine_ThenShouldSaveResultsInFormDataObject()
        {
            //Arrange
            var path = "path";
            var content = new List<string> { "1;1;1", "1;0;1", "0;0;1" };
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 1, 1}, new List<int> {1, 0, 1}, new List<int> {0, 0, 1}});
            var granuleSet = new GranuleSet
                {new Granule(new[] {1, 1, 1}), new Granule(new[] {1, 0, 1}), new Granule(new[] {0, 0, 1})};
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(content)).Returns(coverageData);
            _coverageFileValidatorMock.Setup(x => x.ValidAndShow(It.IsAny<CoverageFile>())).Returns(true);
            _granuleServiceMock.Setup(x => x.GenerateGran(coverageData)).Returns(granuleSet);

            //Act
            _actionsService.Load();

            //Assert
            Assert.Equal(path, _formData.PathSource);
            Assert.Equal(granuleSet, _formData.GranuleSet);
        }
    }
}
