using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Validators;
using Moq;
using Xunit;

namespace Test
{
    public class CoverageFileValidatorTests
    {
        private readonly ICoverageFileValidator _coverageFileValidator;
        private readonly Mock<IMyMessageBox> _myMessageBoxMock;

        public CoverageFileValidatorTests()
        {
            _coverageFileValidator = new CoverageFileValidator();
            _myMessageBoxMock = new Mock<IMyMessageBox>();
        }

        [Fact]
        public void Valid_WhenPutRightCoverageFile_ThenShouldReturnTrueWithoutError()
        {
            //Arrange
            var path = "path";
            var data = new List<List<int>> {new List<int> {1, 2, 3}, new List<int> {1, 2, 3}, new List<int> {1, 2, 3}};
            var coverageFile = new CoverageFile(path, data);
            
            //Act
            var result = _coverageFileValidator.Valid(coverageFile, out var errorMessage);

            //Assert
            Assert.True(result);
            Assert.Null(errorMessage);
        }

        [Fact]
        public void Valid_WhenPutNullLikeCoverageFile_ThenShouldReturnFalseAndError()
        {
            //Arrange
            //Act
            var result = _coverageFileValidator.Valid(null, out var errorMessage);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(errorMessage);
        }

        [Fact]
        public void Valid_WhenPutNullLikePathInCoverageFile_ThenShouldReturnFalseAndError()
        {
            //Arrange
            var data = new List<List<int>> {new List<int> {1, 2, 3}, new List<int> {1, 2, 3}, new List<int> {1, 2, 3}};
            var coverageFile = new CoverageFile(null, data);

            //Act
            var result = _coverageFileValidator.Valid(coverageFile, out var errorMessage);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(errorMessage);
        }

        [Fact]
        public void Valid_WhenPutEmptyDataInCoverageFile_ThenShouldReturnFalseAndError()
        {
            //Arrange
            var path = "path";
            var data = new List<List<int>>();
            var coverageFile = new CoverageFile(path, data);

            //Act
            var result = _coverageFileValidator.Valid(coverageFile, out var errorMessage);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(errorMessage);
        }

        [Fact]
        public void Valid_WhenPutDataWithNotEqualColumnsInCoverageFile_ThenShouldReturnFalseAndError()
        {
            //Arrange
            var path = "path";
            var data = new List<List<int>> {new List<int> {1, 2, 3}, new List<int> {1, 2, 3, 4}, new List<int> {1, 2}};
            var coverageFile = new CoverageFile(path, data);

            //Act
            var result = _coverageFileValidator.Valid(coverageFile, out var errorMessage);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(errorMessage);
        }

        [Fact]
        public void ValidAndShow_WhenPutRightCoverageFile_ThenReturnTrueWithoutMessage()
        {
            //Arrange
            var path = "path";
            var data = new List<List<int>> { new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 } };
            var coverageFile = new CoverageFile(path, data);

            //Act
            var result = _coverageFileValidator.ValidAndShow(coverageFile, _myMessageBoxMock.Object);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidAndShow_WhenPutWrongCoverageFile_ThenReturnFalseWithMessage()
        {
            //Arrange
            var path = "path";
            var wrongData = new List<List<int>> { new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 } };
            var coverageFile = new CoverageFile(path, wrongData);

            //Act
            var result = _coverageFileValidator.ValidAndShow(coverageFile, _myMessageBoxMock.Object);

            //Assert
            Assert.False(result);
            _myMessageBoxMock.Verify(x => x.Show(It.IsAny<string>()), Times.Once);
        }
    }
}
