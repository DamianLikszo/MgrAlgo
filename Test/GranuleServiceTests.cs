using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using Moq;
using Test.Helpers;
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

        [Theory]
        [MemberData(nameof(DataForCheckBuildGranuleSet))]
        public void BuildGranuleSet_WhenSendGranulesList_ThenShouldBuildGranulesSet(List<Granule> granules, GranuleSet expected)
        {
            //Arrange
            
            //Act
            var result = _granuleService.BuildGranuleSet(granules);

            //Assert
            var comparer = new GranuleSetComparer();
            Assert.Equal(expected, result, comparer);
        }

        //TODO: more
        #region Test Data
        public static IEnumerable<object[]> DataForCheckBuildGranuleSet => new List<object[]>
        {
            // List<Granule> granules, GranuleSet granuleSet with relations
            new object[]
            {
                new List<Granule>
                {
                    new Granule(new [] {0, 0, 0}),
                    new Granule(new [] {0, 0, 1}),
                    new Granule(new [] {0, 1, 0}),
                    new Granule(new [] {0, 1, 1}),
                    new Granule(new [] {1, 0, 0}),
                    new Granule(new [] {1, 0, 1}),
                    new Granule(new [] {1, 1, 0}),
                    new Granule(new [] {1, 1, 1})
                },
                new GranuleSet
                {
                    Granules = new List<Granule>
                    {
                        new Granule(new [] {0, 0, 0})
                        {
                            Child = new List<Granule>(),
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {0, 0, 1}), new Granule(new [] {0, 1, 0}),
                                new Granule(new [] {1, 0, 0})
                            }
                        },
                        new Granule(new [] {0, 0, 1})
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {0, 1, 1}), new Granule(new [] {1, 0, 1})
                            }
                        },
                        new Granule(new [] {0, 1, 0})
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {0, 1, 1}), new Granule(new [] {1, 1, 0})
                            }
                        },
                        new Granule(new [] {0, 1, 1})
                        {
                            Child = new List<Granule>
                                {new Granule(new [] {0, 0, 1}), new Granule(new [] {0, 1, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 1})
                            }
                        },
                        new Granule(new [] {1, 0, 0})
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 0, 1}), new Granule(new [] {1, 1, 0})
                            }
                        },
                        new Granule(new [] {1, 0, 1})
                        {
                            Child = new List<Granule>
                                {new Granule(new [] {1, 0, 0}), new Granule(new [] {0, 0, 1})},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 1})
                            }
                        },
                        new Granule(new [] {1, 1, 0})
                        {
                            Child = new List<Granule>
                                {new Granule(new [] {1, 0, 0}), new Granule(new [] {0, 1, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 1})
                            }
                        },
                        new Granule(new [] {1, 1, 1})
                        {
                            Child = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 0}), new Granule(new [] {1, 0, 1}),
                                new Granule(new [] {0, 1, 1})
                            },
                            Parent = new List<Granule>()
                        }
                    }
                }
            },
            new object[]
            {
                new List<Granule>
                {
                    new Granule(new [] {0, 0, 0, 0}),
                    new Granule(new [] {0, 0, 1, 0}),
                    new Granule(new [] {0, 0, 0, 1}),
                    new Granule(new [] {0, 1, 1, 1}),
                    new Granule(new [] {1, 1, 0, 0}),
                    new Granule(new [] {1, 1, 1, 1})
                },
                new GranuleSet
                {
                    Granules = new List<Granule>
                    {
                        new Granule(new [] {0, 0, 0, 0})
                        {
                            Child = new List<Granule>(),
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 0, 0}), new Granule(new [] {0, 0, 1, 0}),
                                new Granule(new [] {0, 0, 0, 1})
                            }
                        },
                        new Granule(new [] {0, 0, 1, 0})
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {0, 1, 1, 1})
                            }
                        },
                        new Granule(new [] {0, 0, 0, 1})
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {0, 1, 1, 1})
                            }
                        },
                        new Granule(new [] {0, 1, 1, 1})
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 1, 0}), new Granule(new [] {0, 0, 0, 1})},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 1, 1})
                            }
                        },
                        new Granule(new [] {1, 1, 0, 0})
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 1, 1})
                            }
                        },
                        new Granule(new [] {1, 1, 1, 1})
                        {
                            Child = new List<Granule>
                                {new Granule(new [] {1, 1, 0, 0}), new Granule(new [] {0, 1, 1, 1})},
                            Parent = new List<Granule>()
                        }
                    }
                }
            },
        };
        #endregion

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
        public void GenerateGran_WhenSendCoverageData_ThenShouldCreateGranuleSet(CoverageData coverageData, List<Granule> expected)
        {
            //Arrange

            //Act
            var result = _granuleService.GenerateGranules(coverageData);

            //Assert
            Assert.Equal(expected, result);
        }

        #region Test Data
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
                    new Granule(new [] {1, 0, 1}),
                    new Granule(new [] {0, 1, 1}),
                    new Granule(new [] {0, 0, 1})
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
                    new Granule(new [] {1, 0, 0, 1, 0}),
                    new Granule(new [] {0, 1, 1, 1, 0}),
                    new Granule(new [] {0, 0, 1, 1, 0}),
                    new Granule(new [] {0, 0, 0, 1, 0}),
                    new Granule(new [] {1, 0, 0, 1, 1})
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
                    new Granule(new [] {1, 0, 1}),
                    new Granule(new [] {0, 1, 1}),
                    new Granule(new [] {0, 0, 1})
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
                    new Granule(new [] {1, 0, 0, 0, 0}),
                    new Granule(new [] {0, 1, 0, 1, 0}),
                    new Granule(new [] {1, 0, 1, 0, 0}),
                    new Granule(new [] {0, 0, 0, 1, 0}),
                    new Granule(new [] {1, 0, 1, 0, 1})
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
                    new Granule(new [] {1, 1, 1, 1, 1}),
                    new Granule(new [] {0, 1, 1, 1, 0}),
                    new Granule(new [] {0, 0, 1, 0, 0}),
                    new Granule(new [] {0, 0, 0, 1, 0}),
                    new Granule(new [] {0, 0, 0, 0, 1})
                }
            },
        };
        #endregion
    }
}
