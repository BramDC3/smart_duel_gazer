using AssemblyCSharp.Assets.Code.Core.Models.Impl.ModelEventsHandler;
using AssemblyCSharp.Assets.Code.Core.Models.Interface.ModelEventsHandler.Entities;
using NSubstitute;
using NUnit.Framework;

public class ModelEventsHandler_Tests
{
    ModelEventHandler testModelEventsHandler = new ModelEventHandler();

    [Test]
    public void ActivatePlayfield_Tests()
    {
        UnityEngine.GameObject testObj = null;
        var testPlayfield = Substitute.For<UnityEngine.Object>() as UnityEngine.GameObject;
        testModelEventsHandler.OnActivatePlayfield += TestVar => testObj = testPlayfield;

        testModelEventsHandler.ActivatePlayfield(testPlayfield);

        Assert.AreEqual(testObj, testPlayfield);
    }

    [Test]
    public void RemovePlayfield_Test()
    {
        int numberOfTimesCalled = 0;
        testModelEventsHandler.OnRemovePlayfield += () => numberOfTimesCalled++;

        testModelEventsHandler.RemovePlayfield();

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void DestroyPlayfield_Test()
    {
        int numberOfTimesCalled = 0;
        testModelEventsHandler.OnDestroyPlayfield += () => numberOfTimesCalled++;

        testModelEventsHandler.DestroyPlayfield();

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void ActivateModel_Test()
    {
        int numberOfTimesCalled = 0;
        testModelEventsHandler.OnActivateModel += (args) => numberOfTimesCalled++;

        testModelEventsHandler.ActivateModel("testString");

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void RaiseEventByEventName_SummonMonster_Test()
    {
        int numberOfTimesCalled = 0;
        var eventName = ModelEvent.SummonMonster;
        testModelEventsHandler.OnSummonMonster += (args) => numberOfTimesCalled++;

        testModelEventsHandler.RaiseEventByEventName(eventName, null);

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void RaiseEventByEventName_DestroyMonster_Test()
    {
        int numberOfTimesCalled = 0;
        var eventName = ModelEvent.DestroyMonster;
        testModelEventsHandler.OnDestroyMonster += (args) => numberOfTimesCalled++;

        testModelEventsHandler.RaiseEventByEventName(eventName, null);

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void RaiseEventByEventName_RevealSetMonster_Test()
    {
        int numberOfTimesCalled = 0;
        var eventName = ModelEvent.RevealSetMonster;
        testModelEventsHandler.OnRevealSetMonster += (args) => numberOfTimesCalled++;

        testModelEventsHandler.RaiseEventByEventName(eventName, null);

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void RaiseEventByEventName_DestroySetMonster_Test()
    {
        int numberOfTimesCalled = 0;
        var eventName = ModelEvent.DestroySetMonster;
        testModelEventsHandler.OnDestroySetMonster += (args) => numberOfTimesCalled++;

        testModelEventsHandler.RaiseEventByEventName(eventName, null);

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void RaiseEventByEventName_SpellTrapActivate_Test()
    {
        int numberOfTimesCalled = 0;
        var eventName = ModelEvent.SpellTrapActivate;
        testModelEventsHandler.OnSpellTrapActivate += (args) => numberOfTimesCalled++;

        testModelEventsHandler.RaiseEventByEventName(eventName, null);

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void RaiseEventByEventName_SetCardRemove_Test()
    {
        int numberOfTimesCalled = 0;
        var eventName = ModelEvent.SetCardRemove;
        testModelEventsHandler.OnSetCardRemove += (args) => numberOfTimesCalled++;

        testModelEventsHandler.RaiseEventByEventName(eventName, null);

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void RaiseEventByEventName_ReturnToFaceDown_Test()
    {
        int numberOfTimesCalled = 0;
        var eventName = ModelEvent.ReturnToFaceDown;
        testModelEventsHandler.OnReturnToFaceDown += (args) => numberOfTimesCalled++;

        testModelEventsHandler.RaiseEventByEventName(eventName, null);

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void RaiseEventByEventName_Default_Test()
    {
        int numberOfTimesCalled = 0;
        testModelEventsHandler.OnSummonMonster += (args) => numberOfTimesCalled++;

        testModelEventsHandler.RaiseEventByEventName(default, null);

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void RaiseSummonSetCardEvent_Test()
    {
        int numberOfTimesCalled = 0;
        testModelEventsHandler.OnSummonSetCard += (args, args2, args3) => numberOfTimesCalled++;

        testModelEventsHandler.RaiseSummonSetCardEvent(null, null, true);

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void RaiseChangeVisibilityEvent_Test()
    {
        int numberOfTimesCalled = 0;
        testModelEventsHandler.OnChangeMonsterVisibility += (args, args2) => numberOfTimesCalled++;

        testModelEventsHandler.RaiseChangeVisibilityEvent(null, true);

        Assert.AreEqual(1, numberOfTimesCalled);
    }

    [Test]
    public void RaiseMonsterRemovalEvent_Test()
    {
        int numberOfTimesCalles = 0;
        testModelEventsHandler.OnMonsterRemoval += (args) => numberOfTimesCalles++;

        testModelEventsHandler.RaiseMonsterRemovalEvent(null);

        Assert.AreEqual(1, numberOfTimesCalles);
    }
}
