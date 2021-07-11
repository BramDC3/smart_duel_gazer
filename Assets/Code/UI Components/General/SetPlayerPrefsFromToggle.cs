using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Providers.PlayerPrefs.Interface;
using AssemblyCSharp.Assets.Code.UIComponents.Constants;

namespace AssemblyCSharp.Assets.Code.UIComponents.General
{
    public class SetPlayerPrefsFromToggle : MonoBehaviour
    {
        [SerializeField]
        private Settings _settingsKey;

        private IPlayerPrefsProvider _playerPrefsProvider;

        private Toggle _toggle;
        private string _toggleKey;

        #region Constructors

        [Inject]
        public void Construct(IPlayerPrefsProvider playerPrefsProvider)
        {
            _playerPrefsProvider = playerPrefsProvider;
        }

        #endregion

        #region Lifecycle

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _toggleKey = _settingsKey.ToString();

            if (!_playerPrefsProvider.HasKey(_toggleKey))
            {
                _playerPrefsProvider.SetBool(_toggleKey, true);
            }

            _toggle.isOn = _playerPrefsProvider.GetBool(_toggleKey);

            RegisterClickListeners();
        }

        #endregion

        private void RegisterClickListeners()
        {
            _toggle.OnValueChangedAsObservable().Subscribe(_ => SetPreferencesUsingToggleValue(_toggle.isOn));
        }

        private void SetPreferencesUsingToggleValue(bool isEnabled)
        {
            if (_toggleKey == null)
            {
                Debug.LogError($"No Key Has Been Set!", this);
                return;
            }

            _playerPrefsProvider.SetBool(_toggleKey, isEnabled);
        }
    }
}
