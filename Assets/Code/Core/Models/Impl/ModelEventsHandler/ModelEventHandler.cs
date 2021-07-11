using System;
using UnityEngine;
using AssemblyCSharp.Assets.Code.Core.Models.Interface.ModelEventsHandler;
using AssemblyCSharp.Assets.Code.Core.Models.Interface.ModelEventsHandler.Entities;

namespace AssemblyCSharp.Assets.Code.Core.Models.Impl.ModelEventsHandler
{
    public class ModelEventHandler : IModelEventHandler
    {
        //Create playfield event handler
        private event Action<GameObject> _OnActivatePlayfield;
        private event Action _OnPickUpPlayfield;
        private event Action _OnDestroyPlayfield;
        
        private event Action<string> _OnActivateModel;
        private event Action<string> _OnSummonMonster;
        private event Action<string> _OnDestroyMonster;
        private event Action<string> _OnAttack;

        private event Action<string> _OnRevealSetMonster;
        private event Action<string> _OnDestroySetMonster;

        private event Action<string> _OnSpellTrapActivate;
        private event Action<string> _OnReturnToFaceDown;
        private event Action<string> _OnSetCardRemove;

        private event Action<string, bool> _OnChangeMonsterVisibility;
        private event Action<string, string, bool> _OnSummonSetCard;
        private event Action<SkinnedMeshRenderer[]> _OnMonsterRemoval;

        #region Event Accessors

        public event Action<GameObject> OnActivatePlayfield
        {
            add => _OnActivatePlayfield += value;
            remove => _OnActivatePlayfield -= value;
        }
        public event Action OnPickupPlayfield
        {
            add => _OnPickUpPlayfield += value;
            remove => _OnPickUpPlayfield -= value;
        }
        public event Action OnDestroyPlayfield
        {
            add => _OnDestroyPlayfield += value;
            remove => _OnDestroyPlayfield -= value;
        }
        public event Action<string> OnActivateModel
        {
            add => _OnActivateModel += value;
            remove => _OnActivateModel -= value;
        }
        public event Action<string> OnSummonMonster 
        { 
            add => _OnSummonMonster += value;
            remove => _OnSummonMonster -= value;
        }
        public event Action<string> OnDestroyMonster 
        { 
            add => _OnDestroyMonster += value;
            remove => _OnDestroyMonster -= value;
        }
        public event Action<string> OnAttack
        {
            add => _OnAttack += value;
            remove => _OnAttack -= value;
        }
        public event Action<string> OnRevealSetMonster
        {
            add => _OnRevealSetMonster += value;
            remove => _OnRevealSetMonster -= value;
        }
        public event Action<string> OnDestroySetMonster
        {
            add => _OnDestroySetMonster += value;
            remove => _OnDestroySetMonster -= value;
        }
        public event Action<string> OnSpellTrapActivate
        {
            add => _OnSpellTrapActivate += value;
            remove => _OnSpellTrapActivate -= value;
        }
        public event Action<string> OnReturnToFaceDown
        {
            add => _OnReturnToFaceDown += value;
            remove => _OnReturnToFaceDown -= value;
        }
        public event Action<string> OnSetCardRemove
        {
            add => _OnSetCardRemove += value;
            remove => _OnSetCardRemove -= value;
        }
        public event Action<string, bool> OnChangeMonsterVisibility 
        {
            add => _OnChangeMonsterVisibility += value;
            remove => _OnChangeMonsterVisibility -= value;
        }
        public event Action<string, string, bool> OnSummonSetCard
        {
            add => _OnSummonSetCard += value;
            remove => _OnSummonSetCard -= value;
        }
        public event Action<SkinnedMeshRenderer[]> OnMonsterRemoval 
        { 
            add => _OnMonsterRemoval += value;
            remove => _OnMonsterRemoval -= value;
        }

        #endregion

        #region Playfield Events

        public void ActivatePlayfield(GameObject playfield)
        {
            _OnActivatePlayfield?.Invoke(playfield);
        }

        public void PickupPlayfield()
        {
            _OnPickUpPlayfield?.Invoke();
        }

        public void DestroyPlayfield()
        {
            _OnDestroyPlayfield?.Invoke();
        }

        #endregion

        #region Model Events

        public void ActivateModel(string zone)
        {
            _OnActivateModel?.Invoke(zone);
        }
        
        public void RaiseEventByEventName(ModelEvent eventName, string zoneOrMonsterID)
        {
            switch (eventName)
            {
                case ModelEvent.SummonMonster:
                    _OnSummonMonster?.Invoke(zoneOrMonsterID);
                    break;
                case ModelEvent.DestroyMonster:
                    _OnDestroyMonster?.Invoke(zoneOrMonsterID);
                    break;
                case ModelEvent.RevealSetMonster:
                    _OnRevealSetMonster?.Invoke(zoneOrMonsterID);
                    break;
                case ModelEvent.DestroySetMonster:
                    _OnDestroySetMonster?.Invoke(zoneOrMonsterID);
                    break;
                case ModelEvent.SpellTrapActivate:
                    _OnSpellTrapActivate?.Invoke(zoneOrMonsterID);
                    break;
                case ModelEvent.SetCardRemove:
                    _OnSetCardRemove?.Invoke(zoneOrMonsterID);
                    break;
                case ModelEvent.ReturnToFaceDown:
                    _OnReturnToFaceDown?.Invoke(zoneOrMonsterID);
                    break;
                case ModelEvent.Attack:
                    _OnAttack?.Invoke(zoneOrMonsterID);
                    break;
            }
        }

        public void RaiseSummonSetCardEvent(string zoneOrMonsterID, string modelName, bool isMonster)
        {
            _OnSummonSetCard?.Invoke(zoneOrMonsterID, modelName, isMonster);
        }
        
        public void RaiseChangeVisibilityEvent(string zoneOrMonsterID, bool state)
        {
            _OnChangeMonsterVisibility?.Invoke(zoneOrMonsterID, state);
        }

        public void RaiseMonsterRemovalEvent(SkinnedMeshRenderer[] renderers)
        {
            _OnMonsterRemoval?.Invoke(renderers);
        }

        #endregion
    }
}
