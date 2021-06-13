using AssemblyCSharp.Assets.Code.Core.Storage.Impl.GameObject;
using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Providers.Resources.Interface;
using NSubstitute;
using NUnit.Framework;

public class GameObjectStorageProvider_Tests
{
    [Test]
    public void Given_AGameObjectDoesntExist_When_ItIsCalled_Then_ItShouldReturnNull()
    {
        var testResourcesProvider = Substitute.For<IResourcesProvider>();
        var testGameObjectStorageProvider = new GameObjectStorageProvider(testResourcesProvider);

        var returnedObject = testGameObjectStorageProvider.GetGameObject("testKey");

        Assert.IsNull(returnedObject);
    }
    
    [Test]
    public void Given_AGameObject_When_TheObjectIsSavedandRecalled_Then_TheReturnedObjectShouldBeTheSameObject()
    {
        var testResourcesProvider = Substitute.For<IResourcesProvider>();
        var testGameObjectStorageProvider = new GameObjectStorageProvider(testResourcesProvider);
        var testObject = Substitute.For<UnityEngine.Object>() as UnityEngine.GameObject;

        testGameObjectStorageProvider.SaveGameObject("testKey", testObject);
        var returnedObject = testGameObjectStorageProvider.GetGameObject("testKey");

        Assert.AreEqual(testObject, returnedObject);
    }

    [Test]
    public void Given_AGameObject_When_TheGameObjectIsRemoved_Then_ItShouldReturnNullWhenAskedFor()
    {
        var testResourcesProvider = Substitute.For<IResourcesProvider>();
        var testGameObjectStorageProvider = new GameObjectStorageProvider(testResourcesProvider);
        var testObject = Substitute.For<UnityEngine.Object>() as UnityEngine.GameObject;

        testGameObjectStorageProvider.SaveGameObject("testKey", testObject);
        testGameObjectStorageProvider.RemoveGameObject("testKey");
        var returnedObject = testGameObjectStorageProvider.GetGameObject("testKey");

        Assert.IsNull(returnedObject);
    }

    [Test]
    public void Given_AnExistingModel_When_GetCardModelIsCalled_Then_TheModelShouldBeReturned()
    {
        var testResourcesProvider = Substitute.For<IResourcesProvider>();
        var testGameObjectStorageProvider = new GameObjectStorageProvider(testResourcesProvider);
        var testObject = Substitute.For<UnityEngine.Object>() as UnityEngine.GameObject;
        testGameObjectStorageProvider.SaveGameObject("testKey", testObject);

        var returnedObject = testGameObjectStorageProvider.GetCardModel("testKey");

        Assert.AreEqual(testObject, returnedObject);
    }

    [Test]
    public void Given_ModelsAreNotLoaded_When_GetCardModelIsCalled_Then_LoadCardModelsShouldBeCalled()
    {
        var testResourcesProvider = Substitute.For<IResourcesProvider>();
        var testGameObjectStorageProvider = new GameObjectStorageProvider(testResourcesProvider);

        testGameObjectStorageProvider.GetCardModel("testKey");

        testResourcesProvider.Received().LoadAll<UnityEngine.GameObject>("Monsters");
    }
}
