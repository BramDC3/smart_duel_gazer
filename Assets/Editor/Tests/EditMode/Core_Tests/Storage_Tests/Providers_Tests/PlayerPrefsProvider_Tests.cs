using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Providers.PlayerPrefs.Impl;
using NUnit.Framework;

public class PlayerPrefsProvider_Tests
{
    string testKey = "testKey";
    string testString = "testString";

    [Test]
    public void Given_PlayerPrefsContainsAGivenKey_When_HasKeyIsCalled_Then_ReturnsTrue()
    {
        var playerPrefsProvider = new PlayerPrefsProvider();
        UnityEngine.PlayerPrefs.SetString(testKey, testString);

        bool hasKey = playerPrefsProvider.HasKey(testKey);

        Assert.IsTrue(hasKey);
    }

    [Test]
    public void Given_PlayerPrefsDoesntContainAGivenKey_When_HasKeyIsCalled_Then_ReturnsFalse()
    {
        var playerPrefsProvider = new PlayerPrefsProvider();
        if(UnityEngine.PlayerPrefs.HasKey(testKey))
        {
            //Ensure Test Key has been deleted from internal memory
            UnityEngine.PlayerPrefs.DeleteKey(testKey);
        }        

        bool hasKey = playerPrefsProvider.HasKey(testKey);

        Assert.IsFalse(hasKey);
    }

    [Test]
    public void Given_PlayerPrefsContainsKey_When_GetStringIsCalled_Then_ReturnsStringAssociatedWithTheGivenKey()
    {
        var playerPrefsProvider  = new PlayerPrefsProvider();
        UnityEngine.PlayerPrefs.SetString(testKey, testString);

        string providerString = playerPrefsProvider.GetString(testKey);

        Assert.AreEqual(testString, providerString);
    }

    [Test]
    public void Given_ValidInputIsRecieved_When_SetStringIsCalled_Then_StringIsAddedToPlayerPrefsUnderGivenKey()
    {
        var playerPrefsProvider = new PlayerPrefsProvider();

        playerPrefsProvider.SetString(testKey, testString);

        Assert.IsTrue(UnityEngine.PlayerPrefs.HasKey(testKey));
        Assert.AreEqual(testString, UnityEngine.PlayerPrefs.GetString(testKey));
    }
}
