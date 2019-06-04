using System.Collections.Generic;
using System.Windows.Forms;
using App.Interfaces;
using App.Models;
using App.Services;
using Test.Helpers;
using Xunit;

namespace Test
{
    public class PresentGranuleSetTests
    {
        private readonly IGranuleSetPresenter _granuleSetPresenter;
        private readonly IGranuleService _granuleService;

        public PresentGranuleSetTests()
        {
            _granuleSetPresenter = new GranuleSetPresenter();
            _granuleService = new GranuleService();
        }

        [Theory]
        [MemberData(nameof(DataForCheckDrawTreeView))]
        public void DrawTreeView_WhenPutGranuleSet_ThenShouldReturnTreeNodes(List<Granule> granules, TreeNode[] expected)
        {
            //Arrange
            var granuleSet = _granuleService.BuildGranuleSet(granules);

            //Act
            var result = _granuleSetPresenter.DrawTreeView(granuleSet);

            var comparer = new EnumerableTreeNodeComparer();
            Assert.Equal(expected, result, comparer);
        }

        #region Test Data
        public static IEnumerable<object[]> DataForCheckDrawTreeView => new List<object[]>
        {
            // List<Granule> granules, TreeNode[] expected
            // One branch
            new object[]
            {
                new List<Granule>
                {
                    new Granule(new[] {1, 1, 1}, 1), new Granule(new[] {0, 1, 1}, 2), new Granule(new[] {0, 0, 1}, 3)
                },
                new[]
                {
                    new TreeNode(new Granule(new[] {1, 1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1}, 3).ToString())
                        })
                }
            },
            // Two separate branches
            new object[]
            {
                new List<Granule>
                {
                    new Granule(new[] {0, 1, 1, 1}, 1), new Granule(new[] {0, 0, 1, 1}, 2), new Granule(new[] {0, 0, 0, 1}, 3),
                    new Granule(new[] {1, 1, 1, 0}, 4), new Granule(new[] {1, 0, 1, 0}, 5)
                },
                new[]
                {
                    new TreeNode(new Granule(new[] {1, 1, 1, 0}, 4).ToString(),
                        new[] {new TreeNode(new Granule(new[] {1, 0, 1, 0}, 5).ToString())}),
                    new TreeNode(new Granule(new[] {0, 1, 1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 0, 1, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1}, 3).ToString())
                        })

                }
            },
            // Multiple branch from one the max point
            new object[]
            {
                new List<Granule>
                {
                    new Granule(new[] {1, 1}, 1), new Granule(new[] {0, 1}, 2), new Granule(new[] {1, 0}, 3),
                    new Granule(new[] {0, 0}, 4)
                },
                new[]
                {
                    new TreeNode(new Granule(new[] {1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 0}, 4).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {1, 0}, 3).ToString()),
                            new TreeNode(new Granule(new[] {0, 0}, 4).ToString())
                        })
                }
            },
            // Multiple branch from one the point
            new object[]
            {
                new List<Granule>
                {
                    new Granule(new[] {0, 1, 1}, 1), new Granule(new[] {0, 0, 1}, 2), new Granule(new[] {0, 1, 0}, 3),
                    new Granule(new[] {0, 0, 0}, 4), new Granule(new[] {1, 1, 1}, 5)
                },
                new[]
                {
                    new TreeNode(new Granule(new[] {1, 1, 1}, 5).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1}, 1).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0}, 4).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1}, 5).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1}, 1).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0}, 3).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0}, 4).ToString())
                        })
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
                new[]
                {
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}, 4).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}, 6).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}, 10).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 1, 0, 0}, 8).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}, 7).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}, 3).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}, 6).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}, 10).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 1, 0, 0}, 8).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}, 7).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}, 4).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}, 6).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}, 10).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 0, 0, 0}, 9).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}, 7).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}, 3).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}, 6).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}, 10).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 0, 0, 0}, 9).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}, 7).ToString())
                        }),
                    // second branch
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}, 4).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}, 14).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 1, 0, 0}, 12).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}, 11).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}, 3).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}, 14).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 1, 0, 0}, 12).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}, 11).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}, 4).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}, 14).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 0, 0, 0}, 13).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}, 11).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}, 1).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}, 2).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}, 3).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}, 5).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}, 14).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 0, 0, 0}, 13).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}, 11).ToString())
                        })
                }
            },
            // all combinations in 3 digits
            new object[]
            {
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
                    new TreeNode(new Granule(new[] {1, 1, 1}, 8).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {0, 1, 1}, 4).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 1}, 2).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}, 1).ToString())
                    }),
                    new TreeNode(new Granule(new[] {1, 1, 1}, 8).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {0, 1, 1}, 4).ToString()),
                        new TreeNode(new Granule(new[] {0, 1, 0}, 3).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}, 1).ToString())
                    }),
                    new TreeNode(new Granule(new[] {1, 1, 1}, 8).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {1, 0, 1}, 6).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 1}, 2).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}, 1).ToString())
                    }),
                    new TreeNode(new Granule(new[] {1, 1, 1}, 8).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {1, 0, 1}, 6).ToString()),
                        new TreeNode(new Granule(new[] {1, 0, 0}, 5).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}, 1).ToString())
                    }),
                    new TreeNode(new Granule(new[] {1, 1, 1}, 8).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {1, 1, 0}, 7).ToString()),
                        new TreeNode(new Granule(new[] {0, 1, 0}, 3).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}, 1).ToString())
                    }),
                    new TreeNode(new Granule(new[] {1, 1, 1}, 8).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {1, 1, 0}, 7).ToString()),
                        new TreeNode(new Granule(new[] {1, 0, 0}, 5).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}, 1).ToString())
                    })
                }
            }
        };
        #endregion
    }
}
