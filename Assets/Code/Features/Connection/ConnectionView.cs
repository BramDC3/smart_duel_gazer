using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using AssemblyCSharp.Assets.Code.Core.Navigation.Interface;
using AssemblyCSharp.Assets.Code.Core.Dialog.Interface;
using AssemblyCSharp.Assets.Code.Core.Screen.Interface;
using AssemblyCSharp.Assets.Code.Core.DataManager.Interface;
using AssemblyCSharp.Assets.Code.Core.DataManager.Interface.Connection.Entities;
using AssemblyCSharp.Assets.Code.Features.Connection.Helpers;

namespace AssemblyCSharp.Assets.Code.Features.Connection
{
    public class ConnectionView : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField IpAddressInputField;

        [SerializeField]
        private TMP_InputField PortInputField;

        [SerializeField]
        private Button ConnectButton;

        private ConnectionFormValidators _connectionFormValidators;
        private INavigationService _navigationService;
        private IDialogService _dialogService;
        private IDataManager _dataManager;

        [Inject]
        public void Construct(
            ConnectionFormValidators connectionFormValidators,
            INavigationService navigationService,
            IDialogService dialogService,
            IScreenService screenService,
            IDataManager dataManager)
        {
            _connectionFormValidators = connectionFormValidators;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _dataManager = dataManager;

            screenService.UsePortraitOrientation();
            InitFormFields();
        }

        private void InitFormFields()
        {
            var connectionInfo = _dataManager.GetConnectionInfo();
            if (connectionInfo != null)
            {
                IpAddressInputField.text = connectionInfo.IpAddress;
                PortInputField.text = connectionInfo.Port;
            }

            ConnectButton.onClick.AddListener(OnConnectPressed);
        }

        private void OnConnectPressed()
        {
            var isFormValid = ValidateForm();
            if (!isFormValid)
            {
                return;
            }

            SaveConnectionInfo();
            ShowMainScene();
        }

        private bool ValidateForm()
        {
            string toastMessage = default;

            if (string.IsNullOrEmpty(IpAddressInputField.text))
            {
                toastMessage = "IP address is required.";
            }
            else if (string.IsNullOrEmpty(PortInputField.text))
            {
                toastMessage = "Port is required.";
            }
            else if (!_connectionFormValidators.IsValidIpAddress(IpAddressInputField.text))
            {
                toastMessage = "Not a valid IP address.";
            }
            else if (!_connectionFormValidators.IsValidPort(PortInputField.text))
            {
                toastMessage = "Not a valid port.";
            }

            if (toastMessage != default)
            {
                _dialogService.ShowToast(toastMessage);
            }

            return toastMessage == default;
        }

        private void SaveConnectionInfo()
        {
            var connectionInfo = new ConnectionInfo(IpAddressInputField.text, PortInputField.text);
            _dataManager.SaveConnectionInfo(connectionInfo);
        }

        private void ShowMainScene()
        {
            _navigationService.ShowMainScene();
        }
    }
}
