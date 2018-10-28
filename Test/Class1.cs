using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class Class1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, 4);
        }

        [Fact]
        public void FailedTest()
        {
            Assert.Equal(5, 6);
        }
    }
}
