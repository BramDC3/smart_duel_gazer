using AssemblyCSharp.Assets.Code.Core.DataManager.Impl.GameObject;
using AssemblyCSharp.Assets.Code.Core.Storage.Impl.GameObject;
using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Providers.Resources.Interface;
using AssemblyCSharp.Assets.Code.Core.Storage.Interface.GameObject;
using NSubstitute;
using NUnit.Framework;

public class GameObjectDataManager_Tests
{
    private string testKey = "testKey";
    
    [Test]
    public void Given_AGameObject_When_TheGameObjectIsSavedAndRecalled_Then_TheGameObjectCanBeReturned()
    {        
        var testGameObjectStorageProvider = Substitute.For<IGameObjectStorageProvider>();
        var testGameObjectDataManager = new GameObjectDataManager(testGameObjectStorageProvider);
        var testGameObject = Substitute.For<UnityEngine.Object>() as UnityEngine.GameObject;

        var nullGameObject = testGameObjectDataManager.GetGameObject(testKey);
        testGameObjectDataManager.SaveGameObject(testKey, testGameObject);
        var returnedGameObject = testGameObjectDataManager.GetGameObject(testKey);

        testGameObjectStorageProvider.Received().SaveGameObject(testKey, testGameObject);
        Assert.IsNull(nullGameObject); //Additional check to ensure key is initially empty
        Assert.AreEqual(testGameObject, returnedGameObject);
    }

    [Test]
    public void Given_AGameObjectExistsForSpecifiedKey_When_RemoveKeyIsCalledOnThatKey_Then_TheGameObjectShouldBeRemoved()
    {
        var testGameObjectStorageProvider = Substitute.For<IGameObjectStorageProvider>();
        var testGameObjectDataManager = new GameObjectDataManager(testGameObjectStorageProvider);
        var testGameObject = Substitute.For<UnityEngine.Object>() as UnityEngine.GameObject;

        testGameObjectDataManager.SaveGameObject(testKey, testGameObject);
        var savedObjectTest = testGameObjectDataManager.GetGameObject(testKey);
        testGameObjectDataManager.RemoveGameObject(testKey);

        Assert.AreEqual(testGameObject, savedObjectTest); //Check to ensure SaveGameObject() works properly
        Assert.IsNull(testGameObjectDataManager.GetGameObject(testKey));
    }

    [Test]
    public void Given_ACardModelAlreadyExists_When_GetCardModelIsCalled_Then_ThatCardModelIsReturned()
    {
        var testGameObjectStorageProvider = Substitute.For<IGameObjectStorageProvider>();
        var testGameObjectDataManager = new GameObjectDataManager(testGameObjectStorageProvider);
        var testGameObject = Substitute.For<UnityEngine.Object>() as UnityEngine.GameObject;
        testGameObjectDataManager.SaveGameObject(testKey, testGameObject);

        var returnedModel = testGameObjectDataManager.GetCardModel(testKey);

        Assert.AreEqual(testGameObject, returnedModel);
    }

    [Test]
    public void Given_NoModelsExists_When_TheResourcesAreNotLoaded_TheResourceProviderIsCalledToLoadResources()
    {
        var testResourcesProvider = Substitute.For<IResourcesProvider>();
        var testGameObjectStorageProvider = new GameObjectStorageProvider(testResourcesProvider);
        var testGameObjectDataManager = new GameObjectDataManager(testGameObjectStorageProvider);

        var returnedModel = testGameObjectDataManager.GetCardModel(testKey);

        testResourcesProvider.Received().LoadAll<UnityEngine.GameObject>("Monsters");
        Assert.IsNull(returnedModel);
    }
}
