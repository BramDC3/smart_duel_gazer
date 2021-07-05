using AssemblyCSharp.Assets.Code.Core.Models.Impl.ModelComponentsManager;
using AssemblyCSharp.Assets.Code.Core.Models.Impl.ModelEventsHandler;
using AssemblyCSharp.Assets.Code.Core.Models.Interface.ModelEventsHandler.Entities;
using AssemblyCSharp.Assets.Code.UIComponents.Constants;
using NUnit.Framework;

public class ModelComponentsManager_Tests
{
    private ModelEventHandler testEventHandler = new ModelEventHandler();
    private UnityEngine.GameObject obj;
    private ModelComponentsManager testModelComponentsManager;
    private string testZone = "testZone";

    [SetUp]
    public void SetUp()
    {
        obj = new UnityEngine.GameObject();
        obj.AddComponent(typeof(ModelComponentsManager));
        testModelComponentsManager = obj.GetComponent<ModelComponentsManager>();
    }

    [Test]
    public void UnfinishedTest()
    {
        var testModelAnimator = obj.GetComponent<UnityEngine.Animator>();

        testModelComponentsManager.Construct(testEventHandler);
        testEventHandler.ActivateModel(testZone);
        testEventHandler.RaiseEventByEventName(ModelEvent.RevealSetMonster, testZone);
        var test = testModelAnimator.GetBool(AnimatorParameters.DefenceBool);

        //Assert.IsTrue(test);
    }
}
