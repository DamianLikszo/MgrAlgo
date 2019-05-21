using System.Collections.Generic;
using magisterka.Compares;
using magisterka.Enums;
using magisterka.Models;
using Xunit;

namespace Test
{
    public class GranuleComparerTests
    {
        private readonly IComparer<Granule> _comparer;

        public GranuleComparerTests()
        {
            _comparer = new GranuleComparer();
        }

        [Theory]
        [MemberData(nameof(DataForCheckComparer))]
        public void Compare_WhenCompareWithSomeOtherGranule_ThenShouldReturnResult(Granule gran1, Granule gran2,
            GranuleComparerResult expectedResult)
        {
            // Arrange

            // Act
            var result = (GranuleComparerResult) _comparer.Compare(gran1, gran2);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #region Test Data
        public static IEnumerable<object[]> DataForCheckComparer => new List<object[]>
        {
            // gran1, gran2, expectedResult
            new object[]
            {
                new Granule(new [] {1, 0, 1, 1, 0}),
                new Granule(new [] {1, 0, 1, 1, 0}),
                GranuleComparerResult.Equal
            },
            new object[]
            {
                new Granule(new [] {0, 0, 1, 0, 0}),
                new Granule(new [] {1, 0, 1, 1, 0}),
                GranuleComparerResult.IsLesser
            },
            new object[]
            {
                new Granule(new [] {1, 1, 1, 1, 0}),
                new Granule(new [] {1, 0, 1, 1, 0}),
                GranuleComparerResult.IsGreater
            },
            new object[]
            {
                new Granule(new [] {1, 0, 1}),
                new Granule(new [] {1, 0, 1, 1, 0}),
                GranuleComparerResult.CanNotCompare
            },
            new object[]
            {
                new Granule(new [] {1, 0, 1, 1, 0}),
                new Granule(new [] {1, 0, 1, 1}),
                GranuleComparerResult.CanNotCompare
            },
            new object[]
            {
                new Granule(new [] {1, 0, 1, 1, 1}),
                new Granule(new [] {1, 1, 1, 1, 0}),
                GranuleComparerResult.CanNotCompare
            },
            new object[]
            {
                new Granule(new [] {1, 0, 0, 0, 0}),
                new Granule(new [] {0, 0, 0, 0, 1}),
                GranuleComparerResult.CanNotCompare
            },
        };
        #endregion
    }
}
