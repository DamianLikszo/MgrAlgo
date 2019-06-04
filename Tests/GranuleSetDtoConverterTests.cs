using System.Collections.Generic;
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
            _granuleService = new GranuleService();
        }

        [Theory]
        [MemberData(nameof(DataForConvert))]
        public void ConvertToDto_WhenPutGranuleSet_ThenShouldReturnDto(List<Granule> granules, GranuleDto[] expected)
        {
            //Arrange
            var granuleSet = _granuleService.BuildGranuleSet(granules);

            //Act
            var result = _granuleSetDtoConverter.ConvertToDto(granuleSet);

            //Assert
            var comparer = new EnumerableGranuleDtoComparer();
            Assert.Equal(expected, result, comparer);
        }

        [Theory]
        [MemberData(nameof(DataForConvert))]
        public void ConvertFromDto_WhenPutDto_ThenShouldReturnGranuleSet(List<Granule> expectedGranules, GranuleDto[] granulesDto)
        {
            //Arrange

            //Act
            var result = _granuleSetDtoConverter.ConvertFromDto(granulesDto);

            //Assert
            var expected = _granuleService.BuildGranuleSet(expectedGranules);

            var comparer = new GranuleSetComparer();
            Assert.Equal(expected, result, comparer);
        }

        #region Test Data
        public static IEnumerable<object[]> DataForConvert => new List<object[]>
        {
            // granules, expectedResult
            new object[]
            {
                // simple
                new List<Granule>
                    {new Granule(new[] {1, 1, 1}, 1), new Granule(new[] {0, 1, 1}, 2), new Granule(new[] {0, 0, 1}, 3)},
                new[]
                {
                    new GranuleDto(new[] {1, 1, 1}, 1) {Children = new[] {new[] {0, 1, 1}}},
                    new GranuleDto(new[] {0, 1, 1}, 2) {Children = new[] {new[] {0, 0, 1}}},
                    new GranuleDto(new[] {0, 0, 1}, 3)
                }
            },
            new object[]
            {
                // all combinations in 3 digits
                new List<Granule>
                {
                    new Granule(new[] {0, 0, 0}, 1),
                    new Granule(new[] {0, 0, 1}, 2),
                    new Granule(new[] {0, 1, 0}, 3),
                    new Granule(new[] {0, 1, 1}, 4),
                    new Granule(new[] {1, 0, 0}, 5),
                    new Granule(new[] {1, 0, 1}, 6),
                    new Granule(new[] {1, 1, 0}, 7),
                    new Granule(new[] {1, 1, 1}, 8),
                },
                new[]
                {
                    new GranuleDto(new[] {1, 1, 1}, 8) {Children = new[] {new[] {0, 1, 1}, new[] {1, 0, 1}, new[] {1, 1, 0}}},
                    new GranuleDto(new[] {0, 1, 1}, 4) {Children = new[] {new[] {0, 0, 1}, new[] {0, 1, 0}}},
                    new GranuleDto(new[] {1, 0, 1}, 6) {Children = new[] {new[] {0, 0, 1}, new[] {1, 0, 0}}},
                    new GranuleDto(new[] {1, 1, 0}, 7) {Children = new[] {new[] {1, 0, 0}, new[] {0, 1, 0}}},
                    new GranuleDto(new[] {0, 1, 0}, 3) {Children = new[] {new[] {0, 0, 0}}},
                    new GranuleDto(new[] {1, 0, 0}, 5) {Children = new[] {new[] {0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 1}, 2) {Children = new[] {new[] {0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 0}, 1)
                }
            },
            new object[]
            {
                //Multiple branches from multi points
                new List<Granule>
                {
                    new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1),
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2),
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}, 3),
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}, 4),
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5),
                    new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}, 6),
                    new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}, 7),
                    new Granule(new[] {0, 0, 0, 1, 0, 1, 0, 0}, 8),
                    new Granule(new[] {0, 0, 0, 1, 1, 0, 0, 0}, 9),
                    new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}, 10),
                    new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}, 11),
                    new Granule(new[] {0, 0, 1, 0, 0, 1, 0, 0}, 12),
                    new Granule(new[] {0, 0, 1, 0, 1, 0, 0, 0}, 13),
                    new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}, 14)
                },
                new []
                {
                    new GranuleDto(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1) {Children = new[] {new[] {0, 1, 1, 1, 1, 1, 1, 1}}},
                    new GranuleDto(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2) {Children = new[] {new[] {0, 1, 1, 1, 1, 1, 0, 1}, new[] {0, 1, 1, 1, 1, 1, 1, 0}}},
                    new GranuleDto(new[] {0, 1, 1, 1, 1, 1, 0, 1}, 4) {Children = new[] {new[] {0, 1, 1, 1, 1, 1, 0, 0}}},
                    new GranuleDto(new[] {0, 1, 1, 1, 1, 1, 1, 0}, 3) {Children = new[] {new[] {0, 1, 1, 1, 1, 1, 0, 0}}},
                    new GranuleDto(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5) {Children = new[] {new[] {0, 1, 0, 1, 1, 1, 0, 0}, new[] {0, 0, 1, 0, 1, 1, 0, 0}}},
                    new GranuleDto(new[] {0, 1, 0, 1, 1, 1, 0, 0}, 6) {Children = new[] {new[] {0, 0, 0, 1, 1, 1, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 0, 1, 1, 1, 0, 0}, 10) {Children = new[] {new[] {0, 0, 0, 1, 0, 1, 0, 0}, new[] {0, 0, 0, 1, 1, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 0, 1, 0, 1, 0, 0}, 8) {Children = new[] {new[] {0, 0, 0, 1, 0, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 0, 1, 0, 0, 0, 0}, 7),
                    new GranuleDto(new[] {0, 0, 0, 1, 1, 0, 0, 0}, 9) {Children = new[] {new[] {0, 0, 0, 1, 0, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 1, 0, 1, 1, 0, 0}, 14) {Children = new[] {new[] {0, 0, 1, 0, 0, 1, 0, 0}, new[] {0, 0, 1, 0, 1, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 1, 0, 0, 1, 0, 0}, 12) {Children = new[] {new[] {0, 0, 1, 0, 0, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 1, 0, 1, 0, 0, 0}, 13) {Children = new[] {new[] {0, 0, 1, 0, 0, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 1, 0, 0, 0, 0, 0}, 11)
                }
            }
        };
        #endregion
    }
}
