using System.Collections.Generic;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;
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
                    new Granule(new[] {1, 1, 1}), new Granule(new[] {0, 1, 1}), new Granule(new[] {0, 0, 1})
                },
                new[]
                {
                    new TreeNode(new Granule(new[] {1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1}).ToString())
                        })
                }
            },
            // Two separate branches
            new object[]
            {
                new List<Granule>
                {
                    new Granule(new[] {0, 1, 1, 1}), new Granule(new[] {0, 0, 1, 1}), new Granule(new[] {0, 0, 0, 1}),
                    new Granule(new[] {1, 1, 1, 0}), new Granule(new[] {1, 0, 1, 0})
                },
                new[]
                {
                    new TreeNode(new Granule(new[] {1, 1, 1, 0}).ToString(),
                        new[] {new TreeNode(new Granule(new[] {1, 0, 1, 0}).ToString())}),
                    new TreeNode(new Granule(new[] {0, 1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 0, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1}).ToString())
                        })

                }
            },
            // Multiple branch from one the max point
            new object[]
            {
                new List<Granule>
                {
                    new Granule(new[] {1, 1}), new Granule(new[] {0, 1}), new Granule(new[] {1, 0}),
                    new Granule(new[] {0, 0})
                },
                new[]
                {
                    new TreeNode(new Granule(new[] {1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0}).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {1, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0}).ToString())
                        })
                }
            },
            // Multiple branch from one the point
            new object[]
            {
                new List<Granule>
                {
                    new Granule(new[] {0, 1, 1}), new Granule(new[] {0, 0, 1}), new Granule(new[] {0, 1, 0}),
                    new Granule(new[] {0, 0, 0}), new Granule(new[] {1, 1, 1})
                },
                new[]
                {
                    new TreeNode(new Granule(new[] {1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0}).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0}).ToString())
                        })
                }
            },
            //Multiple branches from multi points
            new object[]
            {
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
                new[]
                {
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 0, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 1, 0, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0}).ToString())
                        }),
                    // second branch
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 0, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 1, 1, 1, 1}).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 1}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 1, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 1, 1, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 0, 0, 0}).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}).ToString())
                        })
                }
            },
            // all combinations in 3 digits
            new object[]
            {
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
                    new TreeNode(new Granule(new[] {1, 1, 1}).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {0, 1, 1}).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 1}).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}).ToString())
                    }),
                    new TreeNode(new Granule(new[] {1, 1, 1}).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {0, 1, 1}).ToString()),
                        new TreeNode(new Granule(new[] {0, 1, 0}).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}).ToString())
                    }),
                    new TreeNode(new Granule(new[] {1, 1, 1}).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {1, 0, 1}).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 1}).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}).ToString())
                    }),
                    new TreeNode(new Granule(new[] {1, 1, 1}).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {1, 0, 1}).ToString()),
                        new TreeNode(new Granule(new[] {1, 0, 0}).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}).ToString())
                    }),
                    new TreeNode(new Granule(new[] {1, 1, 1}).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {1, 1, 0}).ToString()),
                        new TreeNode(new Granule(new[] {0, 1, 0}).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}).ToString())
                    }),
                    new TreeNode(new Granule(new[] {1, 1, 1}).ToString(), new[]
                    {
                        new TreeNode(new Granule(new[] {1, 1, 0}).ToString()),
                        new TreeNode(new Granule(new[] {1, 0, 0}).ToString()),
                        new TreeNode(new Granule(new[] {0, 0, 0}).ToString())
                    })
                }
            }
        };
        #endregion
    }
}
