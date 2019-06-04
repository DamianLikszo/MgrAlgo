using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using magisterka.Models;
using magisterka.Services;
using magisterka.Wrappers;
using Test.Helpers;
using Xunit;

namespace Test
{
    public class AppTestsIntegration
    {
        [Theory]
        [MemberData(nameof(DataForCheckDrawTreeView))]
        public void Test(string filename, TreeNode[] expected)
        {
            // Arrange
            var granuleSetPresenter = new GranuleSetPresenter();
            var granuleService = new GranuleService();
            var coverageDataConverter = new CoverageDataConverter();
            var fileService = new FileService(new MyStreamReader(), new MyOpenFileDialog(), new MySaveFileDialog(),
                new MyStreamWriter());

            // Act
            var path = Path.Combine(System.Environment.CurrentDirectory, "Samples", filename);
            var content = fileService.ReadFile(path, out var error);
            var coverage = coverageDataConverter.Convert(content, out error);
            var granules = granuleService.GenerateGranules(coverage);
            var granuleSet = granuleService.BuildGranuleSet(granules);
            var result = granuleSetPresenter.DrawTreeView(granuleSet);

            // Assert
            var comparer = new EnumerableTreeNodeComparer();
            Assert.True(string.IsNullOrEmpty(error));
            Assert.Equal(expected, result, comparer);
        }

        public static IEnumerable<object[]> DataForCheckDrawTreeView => new List<object[]>
        {
            // string filename, TreeNode[] expected

            #region Coverage1
            new object[]
            {
                "coverage1.csv",
                new []
                {
                    new TreeNode(new Granule(new[] {0, 0, 1, 0, 1, 1, 0, 1}, 5).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 1, 0, 1}, 8).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 1, 0, 1}, 6).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}, 3).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 0, 0, 1, 0}, 7).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {1, 1, 0, 1, 0, 0, 0, 0}, 1).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 0, 0, 0, 0}, 4).ToString()),
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 0, 0, 0, 0}, 2).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 1, 1, 1, 0, 0, 1, 0}, 7).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 0, 1, 0, 0, 0, 0, 0}, 3).ToString())
                        }),
                }
            },
            #endregion

            #region Coverage2
            new object[]
            {
                "coverage2.csv",
                new[]
                {
                    new TreeNode(new Granule(new[] {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0}, 3).ToString(),
                        new[]
                        {
                            new TreeNode(
                                new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0}, 14).ToString()),
                            new TreeNode(
                                new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0}, 12).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0}, 9).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0}, 3).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0}, 5)
                                .ToString()),
                            new TreeNode(
                                new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0}, 12).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0}, 9).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0}, 3).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0}, 1)
                                .ToString()),
                            new TreeNode(
                                new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0}, 12).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0}, 9).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0}, 3).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0}, 8)
                                .ToString()),
                            new TreeNode(
                                new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0}, 12).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0}, 9).ToString())
                        }),
                    new TreeNode(new Granule(new[] {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 0}, 3).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 6).ToString())
                        }),
                    new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1}, 7).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1}, 4)
                                .ToString()),
                            new TreeNode(
                                new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1}, 15).ToString()),
                            new TreeNode(
                                new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1}, 11).ToString())
                        }),
                    new TreeNode(new Granule(new[] {0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1}, 13).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1}, 2)
                                .ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 6).ToString())
                        }),
                    new TreeNode(new Granule(new[] {0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1}, 13).ToString(),
                        new[]
                        {
                            new TreeNode(new Granule(new[] {0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1}, 2)
                                .ToString()),
                            new TreeNode(
                                new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1}, 10).ToString()),
                            new TreeNode(new Granule(new[] {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1}, 4)
                                .ToString()),
                            new TreeNode(
                                new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1}, 15).ToString()),
                            new TreeNode(
                                new Granule(new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1}, 11).ToString())
                        }),
                }
            },
            #endregion
        };
    }
}
