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
        public void Load_WhenPathIsNull_ThenShouldReturnFalseAdnError()
        {
            //Arrange

            //Act
            var result = _actionsService.Load(out var error);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(error);
        }

        [Fact]
        public void Load_WhenReadFileDoesNotReadContent_ThenShouldReturnFalseWithErrorFromReadFileService()
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
            Assert.False(result);
            Assert.Equal(CallbackOutErrorHelper.ErrorMessage, error);
        }

        [Fact]
        public void Load_WhenCoverageDataConverterFailed_ThenShouldReturnFalseWithErrorFromConverter()
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
            Assert.False(result);
            Assert.Equal(CallbackOutErrorHelper.ErrorMessage, error);
        }

        [Fact]
        public void Load_WhenCoverageFileValidatorReturnFalse_ThenShouldReturnFalseWithErrorFromValidator()
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
            Assert.False(result);
            Assert.Equal(CallbackOutErrorHelper.ErrorMessage, error);
        }

        [Fact]
        public void Load_WhenEverythingIsFine_ThenShouldReturnTrueWithoutError()
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

            //Act
            var result = _actionsService.Load(out error);

            //Assert
            Assert.True(result);
            Assert.True(string.IsNullOrEmpty(error));
        }

        [Fact]
        public void Load_WhenEverythingIsFine_ThenShouldSaveResultsInFormDataObjectWithoutError()
        {
            //Arrange
            string error;
            var path = "path";
            var content = new List<string> { "1;1;1", "1;0;1", "0;0;1" };
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 1, 1}, new List<int> {1, 0, 1}, new List<int> {0, 0, 1}});
            var granuleSet = new GranuleSet
                {new Granule(new[] {1, 1, 1}), new Granule(new[] {1, 0, 1}), new Granule(new[] {0, 0, 1})};

            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error)).Returns(content);
            _coverageDataConverterMock.Setup(x => x.Convert(content, out error)).Returns(coverageData);
            _coverageFileValidatorMock.Setup(x => x.Valid(It.IsAny<CoverageFile>(), out error)).Returns(true);
            _granuleServiceMock.Setup(x => x.GenerateGran(coverageData)).Returns(granuleSet);

            //Act
            _actionsService.Load(out error);

            //Assert
            Assert.Equal(path, _formData.PathSource);
            Assert.Equal(granuleSet, _formData.GranuleSet);
            Assert.True(string.IsNullOrEmpty(error));
        }
    }
}
