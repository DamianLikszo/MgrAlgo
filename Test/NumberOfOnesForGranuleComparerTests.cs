using System.Collections.Generic;
using magisterka;
using magisterka.Models;
using Xunit;

namespace Test
{
    public class NumberOfOnesForGranuleComparerTests
    {
        private readonly IComparer<Granule> _comparer;

        public NumberOfOnesForGranuleComparerTests()
        {
            _comparer = new NumberOfOnesForGranuleComparer();
        }

        [Fact]
        public void Compare_WhenCompareWithNull_ThenShouldReturnMinValue()
        {
            // Arrange
            var gran = new Granule(new[] {1, 0, 1});

            // Act
            var result1 = _comparer.Compare(gran, null);
            var result2 = _comparer.Compare(null, gran);
            var result3 = _comparer.Compare(null, null);

            // Assert
            var expected = int.MinValue;
            Assert.Equal(expected, result1);
            Assert.Equal(expected, result2);
            Assert.Equal(expected, result3);
        }

        [Theory]
        [MemberData(nameof(DataForCheckComparer))]
        public void Compare_WhenCompareWithSomeOtherGranule_ThenShouldBaseOnNumberOfOnesInGranule(Granule gran1, Granule gran2,
            int expectedResult)
        {
            // Arrange

            // Act
            var result = _comparer.Compare(gran1, gran2);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> DataForCheckComparer => new List<object[]>
        {
            // gran1, gran2, expectedResult
            new object[]
            {
                new Granule(new List<int> {1, 0, 1, 1, 0}),
                new Granule(new List<int> {1, 0, 0, 1, 0}),
                1
            },
            new object[]
            {
                new Granule(new List<int> {0, 0, 1, 0, 0}),
                new Granule(new List<int> {1, 0, 1, 1, 0}),
                -1
            },
            new object[]
            {
                new Granule(new List<int> {1, 0, 1, 1, 0}),
                new Granule(new List<int> {1, 0, 1, 1, 0}),
                0
            },
            new object[]
            {
                new Granule(new List<int> {1, 0, 1}),
                new Granule(new List<int> {1, 0, 0, 0, 1}),
                0
            }
        };
    }
}
