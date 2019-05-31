using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
using Test.Helpers;
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

        [Fact]
        public void GenerateGran_WhenPutCoverageData_ThenShouldReturnGranuleSet()
        {
            //Arrange
            var coverageData = new CoverageData(new List<List<int>>());

            //Act
            var result = _granuleService.GenerateGran(coverageData);

            //Assert
            Assert.NotNull(result);
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

        #region Test Data
        public static IEnumerable<object[]> DataForCheckBuildGranuleSet => new List<object[]>
        {
            // List<Granule> granules, GranuleSet granuleSet with relations
            new object[]
            {
                new List<Granule>
                {
                    new Granule(new [] {0, 0, 0}, 1),
                    new Granule(new [] {0, 0, 1}, 2),
                    new Granule(new [] {0, 1, 0}, 3),
                    new Granule(new [] {0, 1, 1}, 4),
                    new Granule(new [] {1, 0, 0}, 5),
                    new Granule(new [] {1, 0, 1}, 6),
                    new Granule(new [] {1, 1, 0}, 7),
                    new Granule(new [] {1, 1, 1}, 8)
                },
                new GranuleSet
                {
                    Granules = new List<Granule>
                    {
                        new Granule(new [] {0, 0, 0}, 1)
                        {
                            Child = new List<Granule>(),
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {0, 0, 1}, 2), new Granule(new [] {0, 1, 0}, 3),
                                new Granule(new [] {1, 0, 0}, 5)
                            }
                        },
                        new Granule(new [] {0, 0, 1}, 2)
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0}, 1)},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {0, 1, 1}, 4), new Granule(new [] {1, 0, 1}, 6)
                            }
                        },
                        new Granule(new [] {0, 1, 0}, 3)
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0}, 1)},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {0, 1, 1}, 4), new Granule(new [] {1, 1, 0}, 7)
                            }
                        },
                        new Granule(new [] {0, 1, 1}, 4)
                        {
                            Child = new List<Granule>
                                {new Granule(new [] {0, 0, 1}, 2), new Granule(new [] {0, 1, 0}, 3)},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 1}, 8)
                            }
                        },
                        new Granule(new [] {1, 0, 0}, 5)
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0}, 1)},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 0, 1}, 6), new Granule(new [] {1, 1, 0}, 7)
                            }
                        },
                        new Granule(new [] {1, 0, 1}, 6)
                        {
                            Child = new List<Granule>
                                {new Granule(new [] {1, 0, 0}, 5), new Granule(new [] {0, 0, 1}, 2)},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 1}, 8)
                            }
                        },
                        new Granule(new [] {1, 1, 0}, 7)
                        {
                            Child = new List<Granule>
                                {new Granule(new [] {1, 0, 0}, 5), new Granule(new [] {0, 1, 0}, 3)},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 1}, 8)
                            }
                        },
                        new Granule(new [] {1, 1, 1}, 8)
                        {
                            Child = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 0}, 7), new Granule(new [] {1, 0, 1}, 6),
                                new Granule(new [] {0, 1, 1}, 4)
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
                    new Granule(new [] {0, 0, 0, 0}, 1),
                    new Granule(new [] {0, 0, 1, 0}, 2),
                    new Granule(new [] {0, 0, 0, 1}, 3),
                    new Granule(new [] {0, 1, 1, 1}, 4),
                    new Granule(new [] {1, 1, 0, 0}, 5),
                    new Granule(new [] {1, 1, 1, 1}, 6)
                },
                new GranuleSet
                {
                    Granules = new List<Granule>
                    {
                        new Granule(new [] {0, 0, 0, 0}, 1)
                        {
                            Child = new List<Granule>(),
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 0, 0}, 5), new Granule(new [] {0, 0, 1, 0}, 2),
                                new Granule(new [] {0, 0, 0, 1}, 3)
                            }
                        },
                        new Granule(new [] {0, 0, 1, 0}, 2)
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0, 0}, 1)},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {0, 1, 1, 1}, 4)
                            }
                        },
                        new Granule(new [] {0, 0, 0, 1}, 3)
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0, 0}, 1)},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {0, 1, 1, 1}, 4)
                            }
                        },
                        new Granule(new [] {0, 1, 1, 1}, 4)
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 1, 0}, 2), new Granule(new [] {0, 0, 0, 1}, 3)},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 1, 1}, 6)
                            }
                        },
                        new Granule(new [] {1, 1, 0, 0}, 5)
                        {
                            Child = new List<Granule> {new Granule(new [] {0, 0, 0, 0}, 1)},
                            Parent = new List<Granule>
                            {
                                new Granule(new [] {1, 1, 1, 1}, 6)
                            }
                        },
                        new Granule(new [] {1, 1, 1, 1}, 6)
                        {
                            Child = new List<Granule>
                                {new Granule(new [] {1, 1, 0, 0}, 5), new Granule(new [] {0, 1, 1, 1}, 4)},
                            Parent = new List<Granule>()
                        }
                    }
                }
            },
            //Multiple branches from multi points
            new object[]
            {
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
                new GranuleSet
                {
                    new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2)
                        },
                        Parent = new List<Granule>()
                    },
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}, 4),
                            new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}, 3)
                        },
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1)
                        }
                    },
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}, 4)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5)
                        },
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2)
                        }
                    },
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}, 3)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5)
                        },
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2)
                        }
                    },
                    new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}, 6),
                            new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}, 14)
                        },
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}, 3),
                            new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}, 4)
                        }
                    },
                    new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}, 6)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}, 10)
                        },
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5)
                        }
                    },
                    new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}, 10)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 0, 1, 0, 1, 0, 0}, 8),
                            new Granule(new[] {0, 0, 0, 1, 1, 0, 0, 0}, 9)
                        },
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}, 6)
                        }
                    },
                    new Granule(new[] {0, 0, 0, 1, 0, 1, 0, 0}, 8)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}, 7)
                        },
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}, 10)
                        }
                    },
                    new Granule(new[] {0, 0, 0, 1, 1, 0, 0, 0}, 9)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}, 7)
                        },
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}, 10)
                        }
                    },
                    new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}, 7)
                    {
                        Child = new List<Granule>(),
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 0, 1, 1, 0, 0, 0}, 9),
                            new Granule(new[] {0, 0, 0, 1, 0, 1, 0, 0}, 8)
                        }
                    },
                    new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}, 14)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 1, 0, 0, 1, 0, 0}, 12),
                            new Granule(new[] {0, 0, 1, 0, 1, 0, 0, 0}, 13)
                        },
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5)
                        }
                    },
                    new Granule(new[] {0, 0, 1, 0, 0, 1, 0, 0}, 12)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}, 11)
                        },
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}, 14)
                        }
                    },
                    new Granule(new[] {0, 0, 1, 0, 1, 0, 0, 0}, 13)
                    {
                        Child = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}, 11)
                        },
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}, 14)
                        }
                    },
                    new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}, 11)
                    {
                        Child = new List<Granule>(),
                        Parent = new List<Granule>
                        {
                            new Granule(new[] {0, 0, 1, 0, 1, 0, 0, 0}, 13),
                            new Granule(new[] {0, 0, 1, 0, 0, 1, 0, 0}, 12)
                        }
                    },
                }
            },
        };
        #endregion
        

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
                    new Granule(new [] {1, 0, 1}, 1),
                    new Granule(new [] {0, 1, 1}, 2),
                    new Granule(new [] {0, 0, 1}, 3)
                }
            },
            new object[]
            {
                // simple
                new CoverageData(new List<List<int>>
                {
                    new List<int> {0, 0, 1, 0},
                    new List<int> {1, 0, 0, 1},
                    new List<int> {0, 0, 0, 1}
                }),
                new List<Granule>
                {
                    new Granule(new [] {1, 0, 0}, 1),
                    new Granule(new [] {0, 1, 0}, 2),
                    new Granule(new [] {0, 1, 1}, 3)
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
                    new Granule(new [] {1, 0, 0, 1, 0}, 1),
                    new Granule(new [] {0, 1, 1, 1, 0}, 2),
                    new Granule(new [] {0, 0, 1, 1, 0}, 3),
                    new Granule(new [] {0, 0, 0, 1, 0}, 4),
                    new Granule(new [] {1, 0, 0, 1, 1}, 5)
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
                    new Granule(new [] {1, 0, 1}, 1),
                    new Granule(new [] {0, 1, 1}, 2),
                    new Granule(new [] {0, 0, 1}, 3)
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
                    new Granule(new [] {1, 0, 0, 0, 0}, 1),
                    new Granule(new [] {0, 1, 0, 1, 0}, 2),
                    new Granule(new [] {1, 0, 1, 0, 0}, 3),
                    new Granule(new [] {0, 0, 0, 1, 0}, 4),
                    new Granule(new [] {1, 0, 1, 0, 1}, 5)
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
                    new Granule(new [] {1, 1, 1, 1, 1}, 1),
                    new Granule(new [] {0, 1, 1, 1, 0}, 2),
                    new Granule(new [] {0, 0, 1, 0, 0}, 3),
                    new Granule(new [] {0, 0, 0, 1, 0}, 4),
                    new Granule(new [] {0, 0, 0, 0, 1}, 5)
                }
            },
        };
        #endregion
    }
}
