using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Texture;
using NSubstitute;
using NUnit.Framework;

public class TextureStorageProvider_Tests
{
    private string testKey = "TestKey";
    
    [Test]
    public void Given_AnImageAsATexture_When_TheImageIsSavedAndRecalled_Then_TheSameImageShouldBeReturned()
    {
        var testTextureStorageProvider = new TextureStorageProvider();
        var testTexture = Substitute.For<UnityEngine.Texture>();

        testTextureStorageProvider.SaveTexture(testKey, testTexture);
        var textureToTestAgainst = testTextureStorageProvider.GetTexture(testKey);

        Assert.AreEqual(testTexture, textureToTestAgainst);
    }

    [Test]
    public void Given_AnEmptyTexture_When_TheImageIsSaved_Then_TheImageShouldReturnNull()
    {
        var testTextureStorageProvider = new TextureStorageProvider();

        testTextureStorageProvider.SaveTexture(testKey, null);
        var textureToTestAgainst = testTextureStorageProvider.GetTexture(testKey);

        Assert.IsNull(textureToTestAgainst);
    }
}
