using NUnit.Framework;
using AssemblyCSharp.Assets.Code.Core.General.Extensions;

public class FloatExtensions_Tests
{
    private float testNumber = 10f;
    
    [Test]
    public void Given_AFloatValue_When_TheValueIsWithinTheRange_Then_ReturnsTrue()
    {
        bool test = testNumber.IsWithinRange(1, 20);

        Assert.IsTrue(test);
    }

    [Test]
    public void Given_AFloatValue_When_TheValueIsNotWithinTheRange_Then_ReturnsFalse()
    {
        bool test = testNumber.IsWithinRange(20, 40);

        Assert.IsFalse(test);
    }
}
