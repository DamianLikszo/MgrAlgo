using System.Collections.Generic;
using magisterka.Models;
using magisterka.Validators;
using Xunit;

namespace Test
{
    public class CoverageFileValidatorTests
    {
        private readonly ICoverageFileValidator _coverageFileValidator;

        public CoverageFileValidatorTests()
        {
            _coverageFileValidator = new CoverageFileValidator();
        }

        [Fact]
        public void Valid_WhenPutRightCoverageFile_ThenShouldReturnTrueWithoutError()
        {
            //Arrange
            var path = "path";
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 2, 3}, new List<int> {1, 2, 3}, new List<int> {1, 2, 3}});
            var coverageFile = new CoverageFile(path, coverageData);
            
            //Act
            var result = _coverageFileValidator.Valid(coverageFile, out var errorMessage);

            //Assert
            Assert.True(result);
            Assert.Null(errorMessage);
        }

        [Fact]
        public void Valid_WhenPutNullLikeCoverageFile_ThenShouldReturnFalseWithError()
        {
            //Arrange
            //Act
            var result = _coverageFileValidator.Valid(null, out var errorMessage);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(errorMessage);
        }

        [Fact]
        public void Valid_WhenPutNullLikePathInCoverageFile_ThenShouldReturnFalseWithError()
        {
            //Arrange
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 2, 3}, new List<int> {1, 2, 3}, new List<int> {1, 2, 3}});
            var coverageFile = new CoverageFile(null, coverageData);

            //Act
            var result = _coverageFileValidator.Valid(coverageFile, out var errorMessage);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(errorMessage);
        }

        [Fact]
        public void Valid_WhenPutEmptyDataInCoverageFile_ThenShouldReturnFalseWithError()
        {
            //Arrange
            var path = "path";
            var coverageData = new CoverageData(new List<List<int>>());
            var coverageFile = new CoverageFile(path, coverageData);

            //Act
            var result = _coverageFileValidator.Valid(coverageFile, out var errorMessage);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(errorMessage);
        }

        [Fact]
        public void Valid_WhenPutDataWithNotEqualColumnsInCoverageFile_ThenShouldReturnFalseWithError()
        {
            //Arrange
            var path = "path";
            var coverageData = new CoverageData(new List<List<int>>
                {new List<int> {1, 2, 3}, new List<int> {1, 2, 3, 4}, new List<int> {1, 2}});
            var coverageFile = new CoverageFile(path, coverageData);

            //Act
            var result = _coverageFileValidator.Valid(coverageFile, out var errorMessage);

            //Assert
            Assert.False(result);
            Assert.NotEmpty(errorMessage);
        }
    }
}
