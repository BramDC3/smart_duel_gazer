using Zenject;
using UnityEngine;
using AssemblyCSharp.Assets.Code.Core.Models.Impl.ModelEventsHandler;
using AssemblyCSharp.Assets.Code.Core.Models.Impl.ModelComponentsManager.Entities;
using AssemblyCSharp.Assets.Code.Core.Models.Interface.ModelComponentsManager;
using AssemblyCSharp.Assets.Code.UIComponents.Constants;

namespace AssemblyCSharp.Assets.Code.Core.Models.Impl.ModelComponentsManager
{
    [RequireComponent(typeof(Animator)), RequireComponent(typeof(ModelSettings))]
    public class ModelComponentsManager : MonoBehaviour, IModelComponentsManager
    {
        private ModelEventHandler _eventHandler;

        private Animator _animator;
        private SkinnedMeshRenderer[] _renderers;
        private ModelSettings _settings;
        private string _zone;
        private bool _areRenderersEnabled;

        #region Constructor

        [Inject]
        public void Construct(ModelEventHandler modelEventHandler)
        {
            _eventHandler = modelEventHandler;
        }

        #endregion

        #region Lifecycle

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            _settings = GetComponent<ModelSettings>();            
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            _zone = null;
            UnsubscribeToEvents();
        }

        #endregion

        #region Event Subscriptions

        public void SubscribeToEvents()
        {
            _eventHandler.OnActivateModel += ActivateModel;
            _eventHandler.OnSummonMonster += SummonMonster;
            _eventHandler.OnRevealSetMonster += RevealSetMonster;
            _eventHandler.OnChangeMonsterVisibility += SetMonsterVisibility;
            _eventHandler.OnDestroyMonster += DestroyMonster;

            _eventHandler.OnActivatePlayfield += ActivatePlayfield;
            _eventHandler.OnPickupPlayfield += PickupPlayfield;
            _eventHandler.OnAttack += Attack;
        }

        public void UnsubscribeToEvents()
        {
            _eventHandler.OnActivateModel -= ActivateModel;
            _eventHandler.OnSummonMonster -= SummonMonster;
            _eventHandler.OnRevealSetMonster -= RevealSetMonster;
            _eventHandler.OnChangeMonsterVisibility -= SetMonsterVisibility;
            _eventHandler.OnDestroyMonster -= DestroyMonster;

            _eventHandler.OnActivatePlayfield -= ActivatePlayfield;
            _eventHandler.OnPickupPlayfield -= PickupPlayfield;
            _eventHandler.OnAttack -= Attack;
        }

        #endregion

        private void ActivateModel(string zone)
        {
            _zone = zone;
            ScaleModel();

            _eventHandler.OnActivateModel -= ActivateModel;
        }

        private void ScaleModel()
        {
            transform.parent.transform.localScale = _settings.ModelScale;
        }

        private void SummonMonster(string zone)
        {
            if (_zone != zone)
            {
                return;
            }

            _renderers.SetRendererVisibility(true);
            _areRenderersEnabled = true;
            _animator.SetBool(AnimatorParameters.DefenceBool, false);
            _animator.SetTrigger(AnimatorParameters.SummoningTrigger);
        }

        private void Attack(string monsterID)
        {
            if (transform.GetInstanceID().ToString() != monsterID)
            {
                return;
            }

            if (_animator.GetBool("IsDefence"))
            {
                return;
            }

            _animator.SetTrigger(AnimatorParameters.PlayMonsterAttack1Trigger);
        }

        private void RevealSetMonster(string zone)
        {
            if (_zone != zone)
            {
                return;
            }

            _animator.SetBool(AnimatorParameters.DefenceBool, true);
        }

        private void SetMonsterVisibility(string zone, bool state)
        {
            if (zone != _zone)
            {
                return;
            }

            _renderers.SetRendererVisibility(state);
            _areRenderersEnabled = state;
        }

        private void DestroyMonster(string zone)
        {
            if (zone != _zone)
            {
                return;
            }

            if (_animator.HasState(0, AnimatorParameters.DeathTrigger))
            {
                _animator.SetTrigger(AnimatorParameters.DeathTrigger);
                return;
            }
            
            ActivateParticlesAndRemoveModel();
        }

        private void ActivateParticlesAndRemoveModel()
        {
            _eventHandler.RaiseMonsterRemovalEvent(_renderers);
            _renderers.SetRendererVisibility(false);
            _eventHandler.OnDestroyMonster -= DestroyMonster;
        }

        private void ActivatePlayfield(GameObject playfield)
        {
            _renderers.SetRendererVisibility(_areRenderersEnabled);
        }
        
        private void PickupPlayfield()
        {
            _renderers.SetRendererVisibility(false);
        }

        public class Factory : PlaceholderFactory<GameObject, ModelComponentsManager>
        {
        }
    }

    public static class ModelComponentUtilities
    {
        public static void SetRendererVisibility(this SkinnedMeshRenderer[] renderers, bool visibility)
        {
            foreach (SkinnedMeshRenderer item in renderers)
            {
                item.enabled = visibility;
            }
        }
    }
}
