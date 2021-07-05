using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Connection;
using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Providers.PlayerPrefs.Impl;
using AssemblyCSharp.Assets.Code.Core.Storage.Interface.Connection.Models;
using NUnit.Framework;

public class ConnectionStorageProvider_Tests
{
    private string testIP = "123.4.5.678";
    private string testPort = "8080";
    
    [Test]
    public void Given_ValidConnectionInfo_When_ConnectionInfoIsSavedAndRecalled_Then_TheSameConnectionInfoShouldBeReturned()
    {
        var testConnectionInfoModel = new ConnectionInfoModel(testIP, testPort);
        var testPlayerPrefsProvider = new PlayerPrefsProvider();
        var testConnectionStorageProvider = new ConnectionStorageProvider(testPlayerPrefsProvider);

        testConnectionStorageProvider.SaveConnectionInfo(testConnectionInfoModel);
        var returnedConnectionInfo = testConnectionStorageProvider.GetConnectionInfo();

        Assert.AreEqual(testIP, returnedConnectionInfo.IpAddress);
        Assert.AreEqual(testPort, returnedConnectionInfo.Port);
    }
}
