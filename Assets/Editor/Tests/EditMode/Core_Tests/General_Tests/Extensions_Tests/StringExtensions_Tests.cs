using NUnit.Framework;
using AssemblyCSharp.Assets.Code.Core.General.Extensions;

public class StringExtensions_Tests
{
    [Test]
    public void Given_AString_When_TheStringHasQuotes_Then_TheyAreRemoved()
    {
        string testString = "\"String\"";

        string stringToTestAgainst = testString.RemoveQuotes();

        Assert.AreNotEqual(testString, stringToTestAgainst);
        Assert.AreEqual("String", stringToTestAgainst);
    }

    [Test]
    public void Given_AString_When_TheStringHasNoQuotes_Then_TheStringDoesntChange()
    {
        string testString = "String";

        string stringToTestAgainst = testString.RemoveQuotes();

        Assert.AreEqual(testString, stringToTestAgainst);
    }

    [Test]
    public void Given_AString_When_TheStringStartsWithA0Character_Then_The0IsRemoved()
    {
        string testString = "0123456789";

        string stringToTestAgainst = testString.RemoveLeadingZero();

        Assert.AreEqual("123456789", stringToTestAgainst);
    }

    [Test]
    public void Given_AString_When_TheStringDoesntStartWithA0Character_Then_ThenTheStringStaysTheSame()
    {
        string testString = "123456789";

        string stringToTestAgainst = testString.RemoveLeadingZero();

        Assert.AreEqual(testString, stringToTestAgainst);
    }
}
