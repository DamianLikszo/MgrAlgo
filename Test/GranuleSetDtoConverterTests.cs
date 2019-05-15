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
                    {new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 1, 1}), new Granule(new[] {0, 0, 1})},
                new[]
                {
                    new GranuleDto(new[] {1, 1, 1}) {Child = new[] {new[] {0, 1, 1}}},
                    new GranuleDto(new[] {0, 1, 1}) {Child = new[] {new[] {0, 0, 1}}},
                    new GranuleDto(new[] {0, 0, 1})
                }
            },
            new object[]
            {
                // all combinations in 3 digits
                new List<Granule>
                {
                    new Granule(new[] {0, 0, 0}),
                    new Granule(new[] {0, 0, 1}),
                    new Granule(new[] {0, 1, 0}),
                    new Granule(new[] {0, 1, 1}),
                    new Granule(new[] {1, 0, 0}),
                    new Granule(new[] {1, 0, 1}),
                    new Granule(new[] {1, 1, 0}),
                    new Granule(new[] {1, 1, 1}),
                },
                new[]
                {
                    new GranuleDto(new[] {1, 1, 1}) {Child = new[] {new[] {0, 1, 1}, new[] {1, 0, 1}, new[] {1, 1, 0}}},
                    new GranuleDto(new[] {0, 1, 1}) {Child = new[] {new[] {0, 0, 1}, new[] {0, 1, 0}}},
                    new GranuleDto(new[] {1, 0, 1}) {Child = new[] {new[] {0, 0, 1}, new[] {1, 0, 0}}},
                    new GranuleDto(new[] {1, 1, 0}) {Child = new[] {new[] {1, 0, 0}, new[] {0, 1, 0}}},
                    new GranuleDto(new[] {0, 1, 0}) {Child = new[] {new[] {0, 0, 0}}},
                    new GranuleDto(new[] {1, 0, 0}) {Child = new[] {new[] {0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 1}) {Child = new[] {new[] {0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 0})
                }
            },
            new object[]
            {
                //Multiple branches from multi points
                new List<Granule>
                {
                    new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}),
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}),
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}),
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}),
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}),
                    new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}),
                    new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}),
                    new Granule(new[] {0, 0, 0, 1, 0, 1, 0, 0}),
                    new Granule(new[] {0, 0, 0, 1, 1, 0, 0, 0}),
                    new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}),
                    new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}),
                    new Granule(new[] {0, 0, 1, 0, 0, 1, 0, 0}),
                    new Granule(new[] {0, 0, 1, 0, 1, 0, 0, 0}),
                    new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0})
                },
                new []
                {
                    new GranuleDto(new[] {1, 1, 1, 1, 1, 1, 1, 1}) {Child = new[] {new[] {0, 1, 1, 1, 1, 1, 1, 1}}},
                    new GranuleDto(new[] {0, 1, 1, 1, 1, 1, 1, 1}) {Child = new[] {new[] {0, 1, 1, 1, 1, 1, 0, 1}, new[] {0, 1, 1, 1, 1, 1, 1, 0}}},
                    new GranuleDto(new[] {0, 1, 1, 1, 1, 1, 0, 1}) {Child = new[] {new[] {0, 1, 1, 1, 1, 1, 0, 0}}},
                    new GranuleDto(new[] {0, 1, 1, 1, 1, 1, 1, 0}) {Child = new[] {new[] {0, 1, 1, 1, 1, 1, 0, 0}}},
                    new GranuleDto(new[] {0, 1, 1, 1, 1, 1, 0, 0}) {Child = new[] {new[] {0, 1, 0, 1, 1, 1, 0, 0}, new[] {0, 0, 1, 0, 1, 1, 0, 0}}},
                    new GranuleDto(new[] {0, 1, 0, 1, 1, 1, 0, 0}) {Child = new[] {new[] {0, 0, 0, 1, 1, 1, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 0, 1, 1, 1, 0, 0}) {Child = new[] {new[] {0, 0, 0, 1, 0, 1, 0, 0}, new[] {0, 0, 0, 1, 1, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 0, 1, 0, 1, 0, 0}) {Child = new[] {new[] {0, 0, 0, 1, 0, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 0, 1, 0, 0, 0, 0}),
                    new GranuleDto(new[] {0, 0, 0, 1, 1, 0, 0, 0}) {Child = new[] {new[] {0, 0, 0, 1, 0, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 1, 0, 1, 1, 0, 0}) {Child = new[] {new[] {0, 0, 1, 0, 0, 1, 0, 0}, new[] {0, 0, 1, 0, 1, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 1, 0, 0, 1, 0, 0}) {Child = new[] {new[] {0, 0, 1, 0, 0, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 1, 0, 1, 0, 0, 0}) {Child = new[] {new[] {0, 0, 1, 0, 0, 0, 0, 0}}},
                    new GranuleDto(new[] {0, 0, 1, 0, 0, 0, 0, 0})
                }
            }
        };
        #endregion
    }
}
