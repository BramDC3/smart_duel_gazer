using System;
using UnityEngine;
using AssemblyCSharp.Assets.Code.Core.Models.Interface.Entities;

namespace AssemblyCSharp.Assets.Code.Core.Models.Impl
{
    [CreateAssetMenu]
    public class ModelEventHandler : ScriptableObject, IModelEventHandler
    {
        public event Action<string> OnSummonMonster;
        public event Action<string, bool> OnChangeMonsterVisibility;
        public event Action<string, bool> OnDestroyMonster;
        public event Action<SkinnedMeshRenderer[]> OnMonsterDestruction;

        public void RaiseEvent(EventNames eventName, string zone)
        {
            switch (eventName)
            {
                case EventNames.SummonMonster:
                    OnSummonMonster?.Invoke(zone);
                    break;
            }
        }
        public void RaiseEvent(EventNames eventName, string zone, bool state)
        {
            switch (eventName)
            {
                case EventNames.ChangeMonsterVisibility:
                    OnChangeMonsterVisibility?.Invoke(zone, state);
                    break;
                case EventNames.DestroyMonster:
                    OnDestroyMonster?.Invoke(zone, state);
                    break;
            }
        }
        public void RaiseEvent(EventNames eventName, SkinnedMeshRenderer[] renderers)
        {
            if (eventName != EventNames.OnMonsterDestruction)
            {
                return;
            }
            OnMonsterDestruction?.Invoke(renderers);
        }
    }
}
