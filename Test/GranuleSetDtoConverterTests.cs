using System.Collections.Generic;
using magisterka;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using Test.Helpers;
using Xunit;

namespace Test
{
    public class GranuleSetDtoConverterTests
    {
        private readonly IGranuleSetDtoConverter _granuleSetDtoConverter;
        private readonly IGranuleService _granuleService;

        public GranuleSetDtoConverterTests()
        {
            _granuleSetDtoConverter = new GranuleSetDtoConverter();
            var comparerForBuildTree = new NumberOfOnesForGranuleComparer();
            _granuleService = new GranuleService(comparerForBuildTree);
        }

        //TODO:add more tests
        [Fact]
        public void ConvertToDto_WhenPutGranuleSet_ThenShouldReturnDto()
        {
            //Arrange
            var granules = new List<Granule>
                {new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 1, 1}), new Granule(new[] {0, 0, 1})};
            var granuleSet = _granuleService.BuildGranuleSet(granules);

            //Act
            var result = _granuleSetDtoConverter.ConvertToDto(granuleSet);

            //Assert
            var expected = new[]
            {
                new GranuleDto(new[] {1, 1, 1}) {Child = new[] {new[] {0, 1, 1}}},
                new GranuleDto(new[] {0, 1, 1}) {Child = new[] {new[] {0, 0, 1}}},
                new GranuleDto(new[] {0, 0, 1})
            };

            var comparer = new EnumerableGranuleDtoComparer();
            Assert.Equal(expected, result, comparer);
        }

        //TODO: add more tests
        [Fact]
        public void ConvertFromDto_WhenPutDto_ThenShouldReturnGranuleSet()
        {
            //Arrange
            var granulesDto = new[]
            {
                new GranuleDto(new[] {1, 1, 1}) {Child = new[] {new[] {0, 1, 1}}},
                new GranuleDto(new[] {0, 1, 1}) {Child = new[] {new[] {0, 0, 1}}},
                new GranuleDto(new[] {0, 0, 1})
            };

            //Act
            var result = _granuleSetDtoConverter.ConvertFromDto(granulesDto);

            //Assert
            var expectedGranules = new List<Granule>
                {new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 1, 1}), new Granule(new[] {0, 0, 1})};
            var expected = _granuleService.BuildGranuleSet(expectedGranules);

            var comparer = new GranuleSetComparer();
            Assert.Equal(expected, result, comparer);
        }
    }
}
