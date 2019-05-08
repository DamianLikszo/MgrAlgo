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
            var actual = _granuleService.BuildGranuleSet(granules);

            SortParentAndChild(actual);
            SortParentAndChild(expected);

            Assert.Equal(expected, actual);
            for (var i = 0; i < actual.Count; i++)
            {
                Assert.Equal(expected[i].Child, actual[i].Child);
                Assert.Equal(expected[i].Parent, actual[i].Parent);
            }
        }

        private static void SortParentAndChild(GranuleSet granuleSet)
        {
            var sortComparer = new SortGranulesInSameOrderComparer();
            foreach (var granule in granuleSet)
            {
                granule.Child.Sort(sortComparer);
                granule.Parent.Sort(sortComparer);
            }
        }

        //TODO: more, hide in class
        public static IEnumerable<object[]> DataForCheckBuildGranuleSet => new List<object[]>
        {
            // List<Granule> granules, GranuleSet granuleSet with relations
            new object[]
            {
                new List<Granule>
                {
                    new Granule(new List<int> {0, 0, 0}),
                    new Granule(new List<int> {0, 0, 1}),
                    new Granule(new List<int> {0, 1, 0}),
                    new Granule(new List<int> {0, 1, 1}),
                    new Granule(new List<int> {1, 0, 0}),
                    new Granule(new List<int> {1, 0, 1}),
                    new Granule(new List<int> {1, 1, 0}),
                    new Granule(new List<int> {1, 1, 1})
                },
                new GranuleSet
                {
                    Granules = new List<Granule>
                    {
                        new Granule(new List<int> {0, 0, 0})
                        {
                            Child = new List<Granule>(),
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {0, 0, 1}), new Granule(new List<int> {0, 1, 0}),
                                new Granule(new List<int> {1, 0, 0})
                            }
                        },
                        new Granule(new List<int> {0, 0, 1})
                        {
                            Child = new List<Granule> {new Granule(new List<int> {0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {0, 1, 1}), new Granule(new List<int> {1, 0, 1})
                            }
                        },
                        new Granule(new List<int> {0, 1, 0})
                        {
                            Child = new List<Granule> {new Granule(new List<int> {0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {0, 1, 1}), new Granule(new List<int> {1, 1, 0})
                            }
                        },
                        new Granule(new List<int> {0, 1, 1})
                        {
                            Child = new List<Granule>
                                {new Granule(new List<int> {0, 0, 1}), new Granule(new List<int> {0, 1, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {1, 1, 1})
                            }
                        },
                        new Granule(new List<int> {1, 0, 0})
                        {
                            Child = new List<Granule> {new Granule(new List<int> {0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {1, 0, 1}), new Granule(new List<int> {1, 1, 0})
                            }
                        },
                        new Granule(new List<int> {1, 0, 1})
                        {
                            Child = new List<Granule>
                                {new Granule(new List<int> {1, 0, 0}), new Granule(new List<int> {0, 0, 1})},
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {1, 1, 1})
                            }
                        },
                        new Granule(new List<int> {1, 1, 0})
                        {
                            Child = new List<Granule>
                                {new Granule(new List<int> {1, 0, 0}), new Granule(new List<int> {0, 1, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {1, 1, 1})
                            }
                        },
                        new Granule(new List<int> {1, 1, 1})
                        {
                            Child = new List<Granule>
                            {
                                new Granule(new List<int> {1, 1, 0}), new Granule(new List<int> {1, 0, 1}),
                                new Granule(new List<int> {0, 1, 1})
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
                    new Granule(new List<int> {0, 0, 0, 0}),
                    new Granule(new List<int> {0, 0, 1, 0}),
                    new Granule(new List<int> {0, 0, 0, 1}),
                    new Granule(new List<int> {0, 1, 1, 1}),
                    new Granule(new List<int> {1, 1, 0, 0}),
                    new Granule(new List<int> {1, 1, 1, 1})
                },
                new GranuleSet
                {
                    Granules = new List<Granule>
                    {
                        new Granule(new List<int> {0, 0, 0, 0})
                        {
                            Child = new List<Granule>(),
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {1, 1, 0, 0}), new Granule(new List<int> {0, 0, 1, 0}),
                                new Granule(new List<int> {0, 0, 0, 1})
                            }
                        },
                        new Granule(new List<int> {0, 0, 1, 0})
                        {
                            Child = new List<Granule> {new Granule(new List<int> {0, 0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {0, 1, 1, 1})
                            }
                        },
                        new Granule(new List<int> {0, 0, 0, 1})
                        {
                            Child = new List<Granule> {new Granule(new List<int> {0, 0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {0, 1, 1, 1})
                            }
                        },
                        new Granule(new List<int> {0, 1, 1, 1})
                        {
                            Child = new List<Granule> {new Granule(new List<int> {0, 0, 1, 0}), new Granule(new List<int> {0, 0, 0, 1})},
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {1, 1, 1, 1})
                            }
                        },
                        new Granule(new List<int> {1, 1, 0, 0})
                        {
                            Child = new List<Granule> {new Granule(new List<int> {0, 0, 0, 0})},
                            Parent = new List<Granule>
                            {
                                new Granule(new List<int> {1, 1, 1, 1})
                            }
                        },
                        new Granule(new List<int> {1, 1, 1, 1})
                        {
                            Child = new List<Granule>
                                {new Granule(new List<int> {1, 1, 0, 0}), new Granule(new List<int> {0, 1, 1, 1})},
                            Parent = new List<Granule>()
                        }
                    }
                }
            },
        };

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
            var actual = _granuleService.GenerateGranules(coverageData);

            //Assert
            Assert.Equal(expected, actual);
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
