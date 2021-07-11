using Zenject;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using AssemblyCSharp.Assets.Code.Core.General.Extensions;
using AssemblyCSharp.Assets.Code.Core.DataManager.Interface;
using AssemblyCSharp.Assets.Code.Core.Models.Impl.ModelEventsHandler;
using AssemblyCSharp.Assets.Code.Features.SpeedDuel.PrefabManager;
using AssemblyCSharp.Assets.Code.Core.Models.Impl.ModelComponentsManager;

namespace AssemblyCSharp.Assets.Code.Features.SpeedDuel.EventHandlers
{
    public class PlacementEventHandler : MonoBehaviour
    {
        private const string PlayfieldKey = "Playfield";

        [SerializeField]
        private GameObject _playfieldPrefab;
        [SerializeField]
        private GameObject _placementIndicator;
        [SerializeField]
        private Camera _mainCamera;

        private IDataManager _dataManager;
        private ModelEventHandler _modelEventHandler;

        private ARRaycastManager _arRaycastManager;
        private ARPlaneManager _arPlaneManager;
        private GameObject _prefabManager;
        private GameObject _speedDuelField;
        private List<ARRaycastHit> _hits;
        private Pose _placementPose;
        private bool _placementPoseIsValid = false;
        private bool _objectIsPlaced = false;

        #region Properties

        public GameObject SpeedDuelField { get => _speedDuelField; }

        #endregion

        #region Constructors

        [Inject]
        public void Construct(IDataManager dataManager,
                              ModelEventHandler modelEventHandler)
        {
            _dataManager = dataManager;
            _modelEventHandler = modelEventHandler;
        }

        #endregion

        #region LifeCycle

        private void Awake()
        {
            GetObjectReferences();
            _modelEventHandler.OnDestroyPlayfield += OnPlaymatDestroyed;
        }

        private void Update()
        {
            UpdatePlacementIndicatorIfNecessary();
        }

        private void OnDestroy()
        {
            _modelEventHandler.OnDestroyPlayfield -= OnPlaymatDestroyed;
            _dataManager.RemoveGameObject(PlayfieldKey);
        }

        #endregion

        private void GetObjectReferences()
        {
            _arRaycastManager = FindObjectOfType<ARRaycastManager>();
            _arPlaneManager = FindObjectOfType<ARPlaneManager>();
            _prefabManager = FindObjectOfType<SpeedDuelPrefabManager>().gameObject;
        }

        private void UpdatePlacementIndicatorIfNecessary()
        {

            #region Editor
            #if UNITY_EDITOR

            // Use Spacebar to place playfield if in Editor. Good for quick tests
            if (!_objectIsPlaced && Input.GetKeyDown(KeyCode.Space))
            {
                PlaceObject();
                _modelEventHandler.ActivatePlayfield(_speedDuelField);
                return;
            }

            #endif
            #endregion

            if (_objectIsPlaced)
            {
                return;
            }

            _hits = UpdatePlacementPose();
            UpdatePlacementIndicator();

            if (_placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
                SetPlaymatScale(_hits);
                _modelEventHandler.ActivatePlayfield(_speedDuelField);
            }
        }

        private List<ARRaycastHit> UpdatePlacementPose()
        {
            var screenCenter = _mainCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            _arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

            _placementPoseIsValid = hits.Count > 0;
            if (_placementPoseIsValid)
            {
                _placementPose = hits[hits.Count - 1].pose;

                var cameraForward = _mainCamera.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }

            return hits;
        }

        private void UpdatePlacementIndicator()
        {
            if (_placementPoseIsValid)
            {
                _placementIndicator.SetActive(true);
                _placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
                return;
            }
            _placementIndicator.SetActive(false);
        }

        private void PlaceObject()
        {
            _objectIsPlaced = true;
            _placementIndicator.SetActive(false);

            var playMat = _dataManager.GetGameObject(PlayfieldKey);
            if (playMat == null)
            {
                _speedDuelField = Instantiate(_playfieldPrefab, _placementPose.position, _placementPose.rotation);
                _prefabManager.transform.SetParent(_speedDuelField.transform);
                _prefabManager.transform.SetPositionAndRotation(_speedDuelField.transform.position, _speedDuelField.transform.rotation);
                _modelEventHandler.ActivatePlayfield(_speedDuelField);
                return;
            }
            else
            {
                playMat.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
                _modelEventHandler.ActivatePlayfield(_speedDuelField);
            }    
        }

        private void SetPlaymatScale(List<ARRaycastHit> hits)
        {
            if (hits == null)
            {
                return;
            }

            var scalePlane = GetCameraOrientation(_arPlaneManager.GetPlane(hits[hits.Count - 1].trackableId));
            if (scalePlane <= 0)
            {
                return;
            }

            _speedDuelField.transform.localScale = new Vector3(scalePlane, scalePlane, scalePlane);

            _arPlaneManager.SetTrackablesActive(false);
            _arPlaneManager.enabled = false;
        }

        private float GetCameraOrientation(ARPlane plane)
        {
            var cameraOriantation = _mainCamera.transform.rotation.y;

            if (cameraOriantation.IsWithinRange(45, 135) ||
                cameraOriantation.IsWithinRange(225, 315) ||
                cameraOriantation.IsWithinRange(-45, -135) ||
                cameraOriantation.IsWithinRange(-225, -315))
            {
                return plane.size.y;
            }
            return plane.size.x;
        }

        private void OnPlaymatDestroyed()
        {
            _objectIsPlaced = false;
            _placementIndicator.SetActive(true);
            _arPlaneManager.enabled = true;

            _dataManager.SaveGameObject(PlayfieldKey, _speedDuelField);
            _modelEventHandler.PickupPlayfield();
        }
    }
}
