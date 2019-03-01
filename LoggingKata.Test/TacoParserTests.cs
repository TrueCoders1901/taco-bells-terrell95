using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {

        [Theory]
        [InlineData("31.597099,-84.176122,Taco Bell Albany/... (Free trial * Add to Cart for a full POI info)")]
        [InlineData("70,45,Taco Bell with a name")]
        [InlineData("0,0,Taco Bell")]
        public void ShouldParse(string str)
        {
            //Arrange
            TacoParser tacoParser = new TacoParser();
            //Act
            ITrackable actual = tacoParser.Parse(str);
            //Assert
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("")]
        [InlineData("0.0,Taco Bell Test")]
        public void ShouldFailParse(string stringToTest)
        {
            // Arrange
            TacoParser tacoParser = new TacoParser();
            //Act
            ITrackable actual = tacoParser.Parse(stringToTest);
            //Assert
            Assert.Null(actual);
        }
    }
}
