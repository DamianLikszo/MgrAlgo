using System.Collections.Generic;
using App.Interfaces;
using App.Models;
using App.Services;
using App.Validators;
using Moq;
using Test.Helpers;
using Xunit;

namespace Test
{
    public class ActionsServiceTests
    {
        private readonly ActionsService _actionsService;
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly Mock<ICoverageDataConverter> _coverageDataConverterMock;
        private readonly Mock<ICoverageFileValidator> _coverageFileValidatorMock;
        private readonly Mock<IGranuleService> _granuleServiceMock;

        public ActionsServiceTests()
        {
            _fileServiceMock = new Mock<IFileService>();
            _coverageDataConverterMock = new Mock<ICoverageDataConverter>();
            _coverageFileValidatorMock = new Mock<ICoverageFileValidator>();
            _granuleServiceMock = new Mock<IGranuleService>();
            var printGranuleServiceMock = new Mock<IPrintGranuleService>();
            var printGranSetServiceMock = new Mock<IPrintGranSetService>();
            _actionsService = new ActionsService(_fileServiceMock.Object, printGranuleServiceMock.Object,
                _coverageDataConverterMock.Object, _coverageFileValidatorMock.Object, _granuleServiceMock.Object,
                printGranSetServiceMock.Object);
        }

        [Fact]
        public void Load_WhenDoNotChooseFile_ThenShouldReturnNullWithoutError()
        {
            //Arrange

            //Act
            var result = _actionsService.Load(out var error);

            //Assert
            Assert.Null(result);
            Assert.Null(error);
        }

        [Fact]
        public void Load_WhenPathIsNull_ThenShouldReturnNullWithError()
        {
            //Arrange
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(string.Empty);

            //Act
            var result = _actionsService.Load(out var error);

            //Assert
            Assert.Null(result);
            Assert.NotEmpty(error);
        }

        [Fact]
        public void Load_WhenReadFileDoesNotReadContent_ThenShouldReturnNullWithErrorFromReadFileService()
        {
            //Arrange
            string error;
            var path = "path";
            
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error))
                .Callback(CallbackOutErrorHelper.DelegateForObject1);

            //Act
            var result = _actionsService.Load(out error);

            //Assert
            Assert.Null(result);
            Assert.Equal(CallbackOutErrorHelper.ErrorMessage, error);
        }

        [Fact]
        public void Load_WhenCoverageDataConverterFailed_ThenShouldReturnNullWithErrorFromConverter()
        {
            //Arrange
            string error;
            var path = "path";
            var content = new List<string> {"1;1;1", "1;0;1", "0;0;1"};

            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(It.IsAny<List<string>>(), out error))
                .Callback(CallbackOutErrorHelper.DelegateForObject1);

            //Act
            var result = _actionsService.Load(out error);

            //Assert
            Assert.Null(result);
            Assert.Equal(CallbackOutErrorHelper.ErrorMessage, error);
        }

        [Fact]
        public void Load_WhenCoverageFileValidatorReturnFalse_ThenShouldReturnNullWithErrorFromValidator()
        {
            //Arrange
            string error;
            var path = "path";
            var content = new List<string> { "1;1;1", "1;0;1", "0;0;1" };
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 1, 1}, new List<int> {1, 0, 1}, new List<int> {0, 0, 1}});

            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(content, out error)).Returns(coverageData);
            _coverageFileValidatorMock.Setup(x => x.Valid(It.IsAny<CoverageFile>(), out error))
                .Callback(CallbackOutErrorHelper.DelegateForObject1).Returns(false);
            
            //Act
            var result = _actionsService.Load(out error);

            //Assert
            Assert.Null(result);
            Assert.Equal(CallbackOutErrorHelper.ErrorMessage, error);
        }

        [Fact]
        public void Load_WhenEverythingIsFine_ThenShouldReturnObjectWithoutError()
        {
            //Arrange
            string error;
            var path = "path";
            var content = new List<string> { "1;1;1", "1;0;1", "0;0;1" };
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 1, 1}, new List<int> {1, 0, 1}, new List<int> {0, 0, 1}});

            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(content, out error)).Returns(coverageData);
            _coverageFileValidatorMock.Setup(x => x.Valid(It.IsAny<CoverageFile>(), out error)).Returns(true);
            _granuleServiceMock.Setup(x => x.GenerateGran(coverageData)).Returns(new GranuleSet());

            //Act
            var result = _actionsService.Load(out error);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.GranuleSet);
            Assert.Equal(path, result.Path);
            Assert.Null(error);
        }

        [Fact]
        public void SaveFile_WhenGranuleSetIsNull_ThenShouldReturnFalseWithError()
        {
            //Arrange

            //Act
            var result = _actionsService.SaveGranule(null, out var error);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(error);
        }


        [Fact]
        public void SaveFile_WhenDoNotChooseFile_ThenShouldReturnFalseWithoutError()
        {
            //Arrange
            var granuleSet = new GranuleSet
                {new Granule(new[] {1, 0, 1}, 1), new Granule(new[] {1, 1, 1}, 2), new Granule(new[] {0, 0, 1}, 3)};

            //Act
            var result = _actionsService.SaveGranule(granuleSet, out var error);

            //Assert
            Assert.False(result);
            Assert.Null(error);
        }

        [Fact]
        public void SaveFile_WhenPathIsNull_ThenShouldReturnFalseWithError()
        {
            //Arrange
            var granuleSet = new GranuleSet
                {new Granule(new[] {1, 0, 1}, 1), new Granule(new[] {1, 1, 1}, 2), new Granule(new[] {0, 0, 3}, 3)};
            _fileServiceMock.Setup(x => x.GetPathFromSaveFileDialog(It.IsAny<string>())).Returns(string.Empty);

            //Act
            var result = _actionsService.SaveGranule(granuleSet, out var error);

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
                {new Granule(new[] {1, 0, 1}, 1), new Granule(new[] {1, 1, 1}, 2), new Granule(new[] {0, 0, 1}, 3)};
            _fileServiceMock.Setup(x => x.GetPathFromSaveFileDialog(It.IsAny<string>())).Returns(path);

            string error;
            _fileServiceMock.Setup(x => x.SaveFile(path, It.IsAny<List<string>>(), out error)).Returns(true);

            //Act
            var result = _actionsService.SaveGranule(granuleSet, out error);

            //Assert
            Assert.True(result);
            Assert.Null(error);
        }
    }
}
