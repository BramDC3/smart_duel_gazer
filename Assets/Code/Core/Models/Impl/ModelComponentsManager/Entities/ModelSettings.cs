using UnityEngine;

namespace AssemblyCSharp.Assets.Code.Core.Models.Impl.ModelComponentsManager.Entities
{
    public class ModelSettings : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _modelScale;
        public Vector3 ModelScale { get => _modelScale; }
    }
}
