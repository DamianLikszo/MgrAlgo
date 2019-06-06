using System.Collections.Generic;
using System.Windows.Forms;
using App.Interfaces;
using App.Services;
using Xunit;

namespace Test
{
    public class PrintGranSetServiceTests
    {
        private readonly IPrintGranSetService _printGranSetService;

        public PrintGranSetServiceTests()
        {
            _printGranSetService = new PrintGranSetService();
        }

        [Fact]
        public void Print_WhenPutGranuleSet_ThenShouldPreparePrint()
        {
            //Arrange
            var chains = new[]
            {
                new TreeNode("1", new[] {new TreeNode("1.1"), new TreeNode("1.2"),}),
                new TreeNode("2", new TreeNode[0])
            };

            //Act
            var result = _printGranSetService.Print(chains);

            //Assert
            var expected = new List<string> { "{1, 1.1, 1.2}", "{2}" };
            Assert.Equal(expected, result);
        }

    }
}
