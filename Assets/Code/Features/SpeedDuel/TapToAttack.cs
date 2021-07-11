using AssemblyCSharp.Assets.Code.Core.Models.Impl.ModelEventsHandler;
using AssemblyCSharp.Assets.Code.Core.Models.Interface.ModelEventsHandler.Entities;
using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Providers.PlayerPrefs.Interface;
using AssemblyCSharp.Assets.Code.UIComponents.Constants;
using UnityEngine;
using Zenject;

namespace AssemblyCSharp.Assets.Code.Features.SpeedDuel
{
    public class TapToAttack : MonoBehaviour
    {
        private IPlayerPrefsProvider _playerPrefsProvider;
        private ModelEventHandler _modelEventHandler;

        private Camera _camera;
        private string _settingsKey;

        #region Constructors

        [Inject]
        public void Construct(IPlayerPrefsProvider playerPrefsProvider,
                              ModelEventHandler modelEventHandler)
        {
            _playerPrefsProvider = playerPrefsProvider;
            _modelEventHandler = modelEventHandler;
        }

        #endregion

        #region Lifecycle

        private void Awake()
        {
            _camera = Camera.main;
            _settingsKey = SettingsKeys.TapToAttack;
        }

        private void Update()
        {
            if (!_playerPrefsProvider.GetBool(_settingsKey))
            {
                return;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                SendRaycast(Input.GetTouch(0));
            }

            #region EditorCode
#if UNITY_EDITOR

            if (Input.GetMouseButtonUp(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                var hitSomething = Physics.Raycast(ray, out RaycastHit hit);

                if (!hitSomething)
                {
                    return;
                }

                var objectID = hit.transform.GetInstanceID().ToString();
                _modelEventHandler.RaiseEventByEventName(ModelEvent.Attack, objectID);
            }

#endif
            #endregion

        }

        #endregion

        private void SendRaycast(Touch touch)
        {
            var ray = _camera.ScreenPointToRay(touch.position);
            var hitSomething = Physics.Raycast(ray, out RaycastHit hit);

            if (!hitSomething)
            {
                return;
            }

            var objectID = hit.transform.GetInstanceID().ToString();
            _modelEventHandler.RaiseEventByEventName(ModelEvent.Attack, objectID);
        }
    }
}
