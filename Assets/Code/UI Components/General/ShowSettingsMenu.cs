using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace AssemblyCSharp.Assets.Code.UIComponents.General
{
    public class ShowSettingsMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _settingsMenu;

        private Button _settingsButton;

        private void Awake()
        {
            _settingsButton = GetComponent<Button>();
            RegisterClickListeners();
        }

        private void RegisterClickListeners()
        {
            _settingsButton.OnClickAsObservable().Subscribe(_ => OnSettingsButtonPressed());
        }

        private void OnSettingsButtonPressed()
        {
            if (_settingsMenu.activeSelf)
            {
                _settingsMenu.SetActive(false);
                return;
            }

            _settingsMenu.SetActive(true);
        }
    }
}
