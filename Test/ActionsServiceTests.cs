using System;
using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using magisterka.Validators;
using magisterka.Wrappers;
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
        private readonly Mock<IMyJsonConvert> _myJsonConvertMock;

        public ActionsServiceTests()
        {
            _formData = new FormData();
            _fileServiceMock = new Mock<IFileService>();
            _coverageDataConverterMock = new Mock<ICoverageDataConverter>();
            _coverageFileValidatorMock = new Mock<ICoverageFileValidator>();
            _granuleServiceMock = new Mock<IGranuleService>();
            _granuleSetDtoConverterMock = new Mock<IGranuleSetDtoConverter>();
            _myJsonConvertMock = new Mock<IMyJsonConvert>();
            var printGranuleServiceMock = new Mock<IPrintGranuleService>();
            _actionsService = new ActionsService(_formData, _fileServiceMock.Object, printGranuleServiceMock.Object,
                _coverageDataConverterMock.Object, _coverageFileValidatorMock.Object, _granuleServiceMock.Object, 
                _granuleSetDtoConverterMock.Object, _myJsonConvertMock.Object);
        }

        [Fact]
        public void Load_WhenDoNotChooseFile_ThenShouldReturnFalseWithoutError()
        {
            //Arrange

            //Act
            var result = _actionsService.Load(out var error);

            //Assert
            Assert.False(result);
            Assert.Null(error);
        }

        [Fact]
        public void Load_WhenPathIsNull_ThenShouldReturnFalseWithError()
        {
            //Arrange
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(string.Empty);

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
            Assert.Null(error);
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
            Assert.Null(error);
        }


        [Fact]
        public void SerializeGranuleSetAndSaveFile_WhenGranuleSetIsNull_ThenShouldReturnFalseWithError()
        {
            //Arrange

            //Act
            var result = _actionsService.SerializeGranuleSetAndSaveFile(out var error);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(error);
        }

        [Fact]
        public void SerializeGranuleSetAndSaveFile_WhenDoNotChooseFile_ThenShouldReturnFalseWithoutError()
        {
            //Arrange
            _formData.GranuleSet = new GranuleSet();

            //Act
            var result = _actionsService.SerializeGranuleSetAndSaveFile(out var error);

            //Assert
            Assert.False(result);
            Assert.Null(error);
        }

        [Fact]
        public void SerializeGranuleSetAndSaveFile_WhenPathIsEmpty_ThenShouldReturnFalseWithError()
        {
            //Arrange
            _formData.GranuleSet = new GranuleSet();
            _fileServiceMock.Setup(x => x.GetPathFromSaveFileDialog(It.IsAny<string>())).Returns(string.Empty);

            //Act
            var result = _actionsService.SerializeGranuleSetAndSaveFile(out var error);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(error);
        }

        [Fact]
        public void SerializeGranuleSetAndSaveFile_WhenJsonConvertThrowException_ThenShouldReturnFalseWithError()
        {
            //Arrange
            var path = "path";

            _formData.GranuleSet = new GranuleSet();
            _fileServiceMock.Setup(x => x.GetPathFromSaveFileDialog(It.IsAny<string>())).Returns(path);
            _myJsonConvertMock.Setup(x => x.SerializeObject(It.IsAny<GranuleDto[]>())).Throws<Exception>();

            //Act
            var result = _actionsService.SerializeGranuleSetAndSaveFile(out var error);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(error);
        }
        [Fact]
        public void SerializeGranuleSetAndSaveFile_WhenFileServiceFailed_ThenShouldReturnFalseWithErrorFromFileService()
        {
            //Arrange
            string error;
            var path = "path";
            var json = "{\"json\"}";

            _formData.GranuleSet = new GranuleSet();
            _fileServiceMock.Setup(x => x.GetPathFromSaveFileDialog(It.IsAny<string>())).Returns(path);
            _myJsonConvertMock.Setup(x => x.SerializeObject(It.IsAny<GranuleDto[]>())).Returns(json);
            _fileServiceMock.Setup(x => x.SaveFile(path, It.IsAny<List<string>>(), out error))
                .Callback(CallbackOutErrorHelper.DelegateForObject2);

            //Act
            var result = _actionsService.SerializeGranuleSetAndSaveFile(out error);

            //Assert
            Assert.False(result);
            Assert.Equal(CallbackOutErrorHelper.ErrorMessage, error);
        }

        [Fact]
        public void SerializeGranuleSetAndSaveFile_WhenEverythingIsFine_ThenShouldReturnTrueWithoutError()
        {
            //Arrange
            string error;
            var path = "path";
            var json = "{\"json\"}";

            _formData.GranuleSet = new GranuleSet();
            _fileServiceMock.Setup(x => x.GetPathFromSaveFileDialog(It.IsAny<string>())).Returns(path);
            _myJsonConvertMock.Setup(x => x.SerializeObject(It.IsAny<GranuleDto[]>())).Returns(json);
            _fileServiceMock.Setup(x => x.SaveFile(path, It.IsAny<List<string>>(), out error)).Returns(true);

            //Act
            var result = _actionsService.SerializeGranuleSetAndSaveFile(out error);

            //Assert
            Assert.True(result);
            Assert.Null(error);
        }

        [Fact]
        public void OpenFileAndDeserializeGranuleSet_WhenDoNotChooseFile_ThenShouldReturnFalseWithoutError()
        {
            //Arrange

            //Act
            var result = _actionsService.OpenFileAndDeserializeGranuleSet(out var error);

            //Assert
            Assert.False(result);
            Assert.Null(error);
        }

        [Fact]
        public void OpenFileAndDeserializeGranuleSet_WhenPathIsEmpty_ThenShouldReturnFalseWithError()
        {
            //Arrange
            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(string.Empty);

            //Act
            var result = _actionsService.OpenFileAndDeserializeGranuleSet(out var error);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(error);
        }

        [Fact]
        public void OpenFileAndDeserializeGranuleSet_WhenReadFileFailed_ThenShouldReturnFalseWithErrorFromFileService()
        {
            //Arrange
            string error;
            var path = "path";

            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error))
                .Callback(CallbackOutErrorHelper.DelegateForObject1);

            //Act
            var result = _actionsService.OpenFileAndDeserializeGranuleSet(out error);

            //Assert
            Assert.False(result);
            Assert.Equal(CallbackOutErrorHelper.ErrorMessage, error);
        }

        [Fact]
        public void OpenFileAndDeserializeGranuleSet_WhenReadJsonWithMultipleLines_ThenShouldReturnFalseWithError()
        {
            //Arrange
            string error;
            var path = "path";
            var content = new List<string>{"{\"js", "on\""};

            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error)).Returns(content);
            
            //Act
            var result = _actionsService.OpenFileAndDeserializeGranuleSet(out error);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(error);
        }

        [Fact]
        public void OpenFileAndDeserializeGranuleSet_WhenJsonConvertThrowException_ThenShouldReturnFalseWithError()
        {
            //Arrange
            string error;
            var path = "path";
            var content = new List<string> { "{\"json\"" };

            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error)).Returns(content);
            _myJsonConvertMock.Setup(x => x.DeserializeObject<GranuleDto[]>(It.IsAny<string>())).Throws<Exception>();

            //Act
            var result = _actionsService.OpenFileAndDeserializeGranuleSet(out error);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(error);
        }

        [Fact]
        public void OpenFileAndDeserializeGranuleSet_WhenEverythingIsFine_ThenShouldReturnTrueWithoutError()
        {
            //Arrange
            string error;
            var path = "path";
            var content = new List<string> { "{\"json\"" };

            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error)).Returns(content);

            //Act
            var result = _actionsService.OpenFileAndDeserializeGranuleSet(out error);

            //Assert
            Assert.True(result);
            Assert.Null(error);
        }

        [Fact]
        public void OpenFileAndDeserializeGranuleSet_WhenEverythingIsFine_ThenShouldSaveResultInFormDataWithoutError()
        {
            //Arrange
            string error;
            var path = "path";
            var content = new List<string> { "{\"json\"" };
            var granuleSet = new GranuleSet();

            _fileServiceMock.Setup(x => x.GetPathFromOpenFileDialog(It.IsAny<string>())).Returns(path);
            _fileServiceMock.Setup(x => x.ReadFile(path, out error)).Returns(content);
            _granuleSetDtoConverterMock.Setup(x => x.ConvertFromDto(It.IsAny<GranuleDto[]>())).Returns(granuleSet);

            //Act
            _actionsService.OpenFileAndDeserializeGranuleSet(out error);

            //Assert
            Assert.Equal(granuleSet, _formData.GranuleSet);
            Assert.Equal(path, _formData.PathSource);
            Assert.Null(error);
        }

        [Fact]
        public void SaveFile_WhenGranuleSetIsNull_ThenShouldReturnFalseWithError()
        {
            //Arrange

            //Act
            var result = _actionsService.SaveGranule(out var error);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(error);
        }


        [Fact]
        public void SaveFile_WhenDoNotChooseFile_ThenShouldReturnFalseWithoutError()
        {
            //Arrange
            _formData.GranuleSet = new GranuleSet
                {new Granule(new[] {1, 0, 1}), new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 0, 1})};

            //Act
            var result = _actionsService.SaveGranule(out var error);

            //Assert
            Assert.False(result);
            Assert.Null(error);
        }

        [Fact]
        public void SaveFile_WhenPathIsNull_ThenShouldReturnFalseWithError()
        {
            //Arrange
            _formData.GranuleSet = new GranuleSet
                {new Granule(new[] {1, 0, 1}), new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 0, 1})};
            _fileServiceMock.Setup(x => x.GetPathFromSaveFileDialog(It.IsAny<string>())).Returns(string.Empty);

            //Act
            var result = _actionsService.SaveGranule(out var error);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(error);
        }

        [Fact]
        public void SaveFile_WhenEverythingIsFine_ThenShouldReturnTrueWithoutError()
        {
            //Arrange
            var path = "path";
            _formData.GranuleSet = new GranuleSet()
                {new Granule(new[] {1, 0, 1}), new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 0, 1})};
            _fileServiceMock.Setup(x => x.GetPathFromSaveFileDialog(It.IsAny<string>())).Returns(path);

            string error;
            _fileServiceMock.Setup(x => x.SaveFile(path, It.IsAny<List<string>>(), out error)).Returns(true);

            //Act
            var result = _actionsService.SaveGranule(out error);

            //Assert
            Assert.True(result);
            Assert.Null(error);
        }
    }
}
