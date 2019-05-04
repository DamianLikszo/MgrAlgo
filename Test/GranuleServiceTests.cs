using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using Xunit;

namespace Test
{
    public class GranuleServiceTests
    {
        private readonly IGranuleService _granuleService;

        public GranuleServiceTests()
        {
            _granuleService = new GranuleService();
        }

        [Theory]
        [MemberData(nameof(DataForCheckGenerateGranule))]
        public void GenerateGran_WhenSendCoverageData_ThenShouldCreateGranuleSet(CoverageData coverageData, GranuleSet expect)
        {
            //Arrange

            //Act
            var result = _granuleService.GenerateGran(coverageData);

            //Assert
            Assert.Equal(expect, result);
        }

        public static IEnumerable<object[]> DataForCheckGenerateGranule => new List<object[]>
        {
            new object[]
            {
                // simple
                new CoverageData(new List<List<int>>
                {
                    new List<int> {1, 0, 1},
                    new List<int> {0, 1, 0},
                    new List<int> {1, 1, 1}
                }),
                new GranuleSet
                {
                    new Granule(new List<int> {1, 0, 1}),
                    new Granule(new List<int> {0, 1, 1}),
                    new Granule(new List<int> {0, 0, 1})
                }
            },
            new object[]
            {
                // more Ux than Cx
                new CoverageData(new List<List<int>>
                {
                    new List<int> {1, 0, 1},
                    new List<int> {0, 1, 0},
                    new List<int> {0, 1, 1},
                    new List<int> {1, 1, 1},
                    new List<int> {1, 0, 0},
                }),
                new GranuleSet
                {
                    new Granule(new List<int> {1, 0, 0, 1, 0}),
                    new Granule(new List<int> {0, 1, 1, 1, 0}),
                    new Granule(new List<int> {0, 0, 1, 1, 0}),
                    new Granule(new List<int> {0, 0, 0, 1, 0}),
                    new Granule(new List<int> {1, 0, 0, 1, 1})
                }
            },
            new object[]
            {
                // more Cx than Ux
                new CoverageData(new List<List<int>>
                {
                    new List<int> {1, 0, 1, 1},
                    new List<int> {0, 1, 0, 1},
                    new List<int> {1, 1, 1, 1}
                }),
                new GranuleSet
                {
                    new Granule(new List<int> {1, 0, 1}),
                    new Granule(new List<int> {0, 1, 1}),
                    new Granule(new List<int> {0, 0, 1})
                }
            },
            new object[]
            {
                new CoverageData(new List<List<int>>
                {
                    new List<int> {1, 0, 1, 1, 1},
                    new List<int> {0, 1, 0, 1, 0},
                    new List<int> {1, 0, 1, 0, 1},
                    new List<int> {1, 1, 1, 1, 0},
                    new List<int> {1, 0, 0, 0, 1}
                }),
                new GranuleSet
                {
                    new Granule(new List<int> {1, 0, 0, 0, 0}),
                    new Granule(new List<int> {0, 1, 0, 1, 0}),
                    new Granule(new List<int> {1, 0, 1, 0, 0}),
                    new Granule(new List<int> {0, 0, 0, 1, 0}),
                    new Granule(new List<int> {1, 0, 1, 0, 1})
                }
            },
            new object[]
            {
                new CoverageData(new List<List<int>>
                {
                    new List<int> {0, 0, 0, 1},
                    new List<int> {0, 0, 1, 1},
                    new List<int> {0, 1, 1, 1},
                    new List<int> {1, 0, 1, 1},
                    new List<int> {1, 1, 0, 1}
                }),
                new GranuleSet
                {
                    new Granule(new List<int> {1, 1, 1, 1, 1}),
                    new Granule(new List<int> {0, 1, 1, 1, 0}),
                    new Granule(new List<int> {0, 0, 1, 0, 0}),
                    new Granule(new List<int> {0, 0, 0, 1, 0}),
                    new Granule(new List<int> {0, 0, 0, 0, 1})
                }
            },
        };
    }
}
