using FluentAssertions;
using Xunit;

namespace ForestFire.Tests
{
    public class ForestTests
    {
        /// <summary>
        /// 
        /// 0 = Untouched
        /// 1 = Fire
        /// 2 = Ash
        /// 
        /// Initial
        /// 000
        /// 010
        /// 000
        /// 
        /// Result
        /// 010
        /// 121
        /// 010
        /// 
        /// </summary>
        [Fact(DisplayName = "Devrait avoir 4 feu à côtés, 4 non touchés sur les diagnales et 1 cendre au millieu")]
        public void IterateOneTest()
        {
            var forest = new Forest();

            forest.UntouchedTrees.Add(new UntouchedTree(new Position(0, 0)));
            forest.UntouchedTrees.Add(new UntouchedTree(new Position(0, 1)));
            forest.UntouchedTrees.Add(new UntouchedTree(new Position(0, 2)));
            forest.UntouchedTrees.Add(new UntouchedTree(new Position(1, 0)));
            forest.UntouchedTrees.Add(new UntouchedTree(new Position(1, 2)));
            forest.UntouchedTrees.Add(new UntouchedTree(new Position(2, 0)));
            forest.UntouchedTrees.Add(new UntouchedTree(new Position(2, 1)));
            forest.UntouchedTrees.Add(new UntouchedTree(new Position(2, 2)));

            forest.FireTrees.Add(new FireTree(new Position(1, 1)));

            forest.Iterate();

            forest.UntouchedTrees.Should().HaveCount(4);
            forest.FireTrees.Should().HaveCount(4);
            forest.AshTrees.Should().HaveCount(1);
        }

        /// <summary>
        /// 
        /// 0 = Untouched
        /// 1 = Fire
        /// 2 = Ash
        /// 3 = None
        /// 
        /// Initial
        /// 010
        /// 121
        /// 010
        /// 
        /// Result
        /// 121
        /// 232
        /// 121
        /// 
        /// </summary>
        [Fact(DisplayName = "Devrait avoir 4 feu sur les diagnales 4 cendre à côtés")]
        public void IterateTwoTest()
        {
            var forest = new Forest();

            forest.UntouchedTrees.Add(new UntouchedTree(new Position(0, 0)));
            forest.UntouchedTrees.Add(new UntouchedTree(new Position(0, 2)));
            forest.UntouchedTrees.Add(new UntouchedTree(new Position(2, 0)));
            forest.UntouchedTrees.Add(new UntouchedTree(new Position(2, 2)));

            forest.FireTrees.Add(new FireTree(new Position(0, 1)));
            forest.FireTrees.Add(new FireTree(new Position(1, 0)));
            forest.FireTrees.Add(new FireTree(new Position(1, 2)));
            forest.FireTrees.Add(new FireTree(new Position(2, 1)));

            forest.AshTrees.Add(new AshTree(new Position(1, 1)));

            forest.Iterate();

            forest.UntouchedTrees.Should().HaveCount(0);
            forest.FireTrees.Should().HaveCount(4);
            forest.AshTrees.Should().HaveCount(4);
        }

        /// <summary>
        /// 
        /// 0 = Untouched
        /// 1 = Fire
        /// 2 = Ash
        /// 3 = None
        /// 
        /// Initial
        /// 121
        /// 222
        /// 121
        /// 
        /// Result
        /// 232
        /// 333
        /// 232
        /// 
        /// </summary>
        [Fact(DisplayName = "Devrait 4 diagnoles cendres")]
        public void IterateThreeTest()
        {
            var forest = new Forest();

            forest.FireTrees.Add(new FireTree(new Position(0, 0)));
            forest.FireTrees.Add(new FireTree(new Position(0, 2)));
            forest.FireTrees.Add(new FireTree(new Position(2, 0)));
            forest.FireTrees.Add(new FireTree(new Position(2, 2)));

            forest.AshTrees.Add(new AshTree(new Position(0, 1)));
            forest.AshTrees.Add(new AshTree(new Position(1, 0)));
            forest.AshTrees.Add(new AshTree(new Position(1, 2)));
            forest.AshTrees.Add(new AshTree(new Position(2, 1)));
            forest.AshTrees.Add(new AshTree(new Position(1, 1)));

            forest.Iterate();

            forest.UntouchedTrees.Should().HaveCount(0);
            forest.FireTrees.Should().HaveCount(0);
            forest.AshTrees.Should().HaveCount(4);
        }

        /// <summary>
        /// 
        /// 0 = Untouched
        /// 1 = Fire
        /// 2 = Ash
        /// 
        /// Initial
        /// 0000
        /// 0000
        /// 0010
        /// 0000
        /// 
        /// Result
        /// 0010
        /// 0121
        /// 1232
        /// 0121
        /// 
        /// </summary>
        [Fact(DisplayName = "Devrait avoir 4 feu sur les diagnales 5 et cendre à côtés et au millieu")]
        public void SizeFourWithTwoIterateTest()
        {
            int forestSize = 4;
            var forest = new Forest();
            for (int i = 0; i < forestSize; i++)
            {
                for (int j = 0; j < forestSize; j++)
                {
                    if (i != forestSize / 2 || j != forestSize / 2)
                    {
                        forest.UntouchedTrees.Add(new UntouchedTree(new Position(i, j)));
                    }
                    else
                    {
                        forest.FireTrees.Add(new FireTree(new Position(i, j)));
                    }
                }
            }

            forest.Iterate();
            forest.Iterate();

            forest.UntouchedTrees.Should().HaveCount(5);
            forest.FireTrees.Should().HaveCount(6);
            forest.AshTrees.Should().HaveCount(4);
        }
    }
}
