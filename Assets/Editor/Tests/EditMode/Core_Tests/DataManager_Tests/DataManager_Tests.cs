using AssemblyCSharp.Assets.Code.Core.DataManager.Impl;
using AssemblyCSharp.Assets.Code.Core.DataManager.Impl.Connection;
using AssemblyCSharp.Assets.Code.Core.DataManager.Impl.Texture;
using AssemblyCSharp.Assets.Code.Core.DataManager.Interface;
using AssemblyCSharp.Assets.Code.Core.DataManager.Interface.Connection.Entities;
using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Connection;
using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Providers.PlayerPrefs.Impl;
using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Texture;
using AssemblyCSharp.Assets.Code.Core.Storage.Interface.Texture;
using AssemblyCSharp.Assets.Code.Core.YGOProDeck.Interface;
using NSubstitute;
using NUnit.Framework;

public class DataManager_Tests
{
    [Test]
    public void Given_ConnectionInfo_When_ConnectionInfoIsRequired_Then_DataManagerShouldRecallItFromSavedMemory()
    {
        var testPlayerPrefsProvider = new PlayerPrefsProvider();
        var testConnectionStorageProvider = new ConnectionStorageProvider(testPlayerPrefsProvider);
        var testConnectionDataManager = new ConnectionDataManager(testConnectionStorageProvider);
        var testDataManager = new DataManager(testConnectionDataManager, null, null);
        var testIP = "111.1.1.111";
        var testPort = "3000";
        var testConnectionInfo = new ConnectionInfo(testIP, testPort);

        UnityEngine.PlayerPrefs.DeleteKey("connectionInfo");
        testDataManager.SaveConnectionInfo(testConnectionInfo);
        var returnedConnectionInfo = testDataManager.GetConnectionInfo();

        Assert.AreEqual(testIP, returnedConnectionInfo.IpAddress);
        Assert.AreEqual(testPort, returnedConnectionInfo.Port);
    }

    [Test]
    public void Given_TheTextureDataManagerContainsAnImage_When_ACardIDIsGiven_Then_TheCorrespondingImageShouldBeReturned()
    {
        var testDataManager = Substitute.For<IDataManager>();
        var testTextureStorageProvider = Substitute.For<ITextureStorageProvider>();
        var testImage = Substitute.For<UnityEngine.Texture>();
        testTextureStorageProvider.SaveTexture("testID", testImage);

        var returnedImage = testDataManager.GetCardImage("testID");

        Assert.AreEqual(testImage, returnedImage);
    }

    [Test]
    public void Given_TheTextureDataManagerDoesntContainAnImage_When_ACardIDIsGiven_Then_AnAPICallShouldBeMade()
    {
        var testID = "testID";
        var testAPI = Substitute.For<IYGOProDeckApiProvider>();
        var testTextureProvider = new TextureStorageProvider();
        var testTextureDataManager = new TextureDataManager(testAPI, testTextureProvider);
        var testDataManager = new DataManager(null, null, testTextureDataManager);

        testDataManager.GetCardImage(testID);

        testAPI.Received().GetCardImage(testID);
    }
}
