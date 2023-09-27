using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class InteractionMethodSwitcher : MonoBehaviour
{
    [SerializeField] private Transform centerEye;
    [SerializeField] private XRGazeInteractor gaze;
    [SerializeField] private Transform reticle;
    [SerializeField] private float gazeSpeed = 5;
    [SerializeField] private Transform rightRayInteractor;
    [SerializeField] private Transform leftRayInteractor;
    [SerializeField] private InputActionReference trackingStateActionReference;

    private void LateUpdate()
    {
        var trackingStateValue = (InputTrackingState)trackingStateActionReference.action.ReadValue<int>();
        bool controllerPresence = trackingStateValue != InputTrackingState.None;

        rightRayInteractor.gameObject.SetActive(controllerPresence);
        leftRayInteractor.gameObject.SetActive(controllerPresence);

        if (controllerPresence)
        {
            reticle.gameObject.SetActive(false);
            return;
        }

        gaze.transform.SetPositionAndRotation(
            Vector3.Lerp(gaze.transform.position, centerEye.position, Time.deltaTime * gazeSpeed),
            Quaternion.Lerp(gaze.transform.rotation, centerEye.rotation, Time.deltaTime * gazeSpeed));

        if (gaze.TryGetCurrentUIRaycastResult(out var result))
        {
            reticle.position = result.worldPosition;
            reticle.forward = result.worldNormal;
            reticle.gameObject.SetActive(true);
        }
        else
        {
            reticle.gameObject.SetActive(false);
        }
    }
}
