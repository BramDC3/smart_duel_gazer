using AssemblyCSharp.Assets.Code.Core.DataManager.Impl.Connection;
using AssemblyCSharp.Assets.Code.Core.DataManager.Interface.Connection.Entities;
using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Connection;
using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Providers.PlayerPrefs.Impl;
using NUnit.Framework;

public class ConnectionDataManager_Tests
{
    string testIP = "testIP";
    string testPort = "testPort";
    
    [Test]
    public void Given_ValidConnectionInfo_When_ConnectionInfoIsSavedAndCalled_Then_TheGivenConnectionInfoShouldBeReturned()
    {
        var testPlayerPrefsProvider = new PlayerPrefsProvider();
        var testConnectionStorageProvider = new ConnectionStorageProvider(testPlayerPrefsProvider);
        var testConnectionDataManager = new ConnectionDataManager(testConnectionStorageProvider);
        var testConnectionInfo = new ConnectionInfo(testIP, testPort);

        testConnectionDataManager.SaveConnectionInfo(testConnectionInfo);
        var returnedConnectionInfo = testConnectionDataManager.GetConnectionInfo();

        Assert.AreEqual(testIP, returnedConnectionInfo.IpAddress);
        Assert.AreEqual(testPort, returnedConnectionInfo.Port);
    }
}
