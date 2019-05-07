using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using Moq;
using Xunit;

namespace Test
{
    public class GranuleServiceTests
    {
        private readonly IGranuleService _granuleService;
        private readonly Mock<IGranuleComparerForBuildTree> _comparerForBuildTreeMock;

        public GranuleServiceTests()
        {
            _comparerForBuildTreeMock = new Mock<IGranuleComparerForBuildTree>();
            _granuleService = new GranuleService(_comparerForBuildTreeMock.Object);
        }

        [Fact]
        public void GenerateGran_WhenSendCoverageData_ThenShouldBeSorted()
        {
            //Arrange
            var coverageData = new CoverageData(new List<List<int>>
            {
                new List<int> {1, 0, 1},
                new List<int> {0, 1, 0},
                new List<int> {1, 1, 1}
            });
            
            //Act
            _granuleService.GenerateGran(coverageData);

            //Assert
            _comparerForBuildTreeMock.Verify(x => x.Compare(It.IsAny<Granule>(), It.IsAny<Granule>()),
                Times.AtLeastOnce);
        }

        [Theory]
        [MemberData(nameof(DataForCheckGenerateGranule))]
        public void GenerateGran_WhenSendCoverageData_ThenShouldCreateGranuleSet(CoverageData coverageData, List<Granule> expect)
        {
            //Arrange

            //Act
            var result = _granuleService.GenerateGranules(coverageData);

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
                new List<Granule>
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
                new List<Granule>
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
                new List<Granule>
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
                new List<Granule>
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
                new List<Granule>
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
