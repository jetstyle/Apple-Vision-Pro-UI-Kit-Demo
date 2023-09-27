using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR.Hands;

public class OVRHandsEventsRetranslator : MonoBehaviour
{
    [SerializeField] private OVRHand OVRLeftHand;
    [SerializeField] private OVRHand OVRRightHand;
    [SerializeField] private InputActionReference pinchActionReference;
    [SerializeField] private InputActionReference pinchValueActionReference;

    private MetaAimHand metaRightHand;
    private MetaAimHand metaLeftHand;

    private void Start()
    {
        metaRightHand = MetaAimHand.CreateHand(UnityEngine.XR.InputDeviceCharacteristics.Right);
        metaLeftHand = MetaAimHand.CreateHand(UnityEngine.XR.InputDeviceCharacteristics.Left);
    }

    private void LateUpdate()
    {
        using (StateEvent.From(metaRightHand.device, out InputEventPtr rightHandEventPtr))
        {
            ((ButtonControl)metaRightHand.device[pinchActionReference.action.name]).WriteValueIntoEvent(OVRRightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) ? 1f : 0f, rightHandEventPtr);
            ((AxisControl)metaRightHand.device[pinchValueActionReference.action.name]).WriteValueIntoEvent(OVRRightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index), rightHandEventPtr);
            InputSystem.QueueEvent(rightHandEventPtr);
        }

        using (StateEvent.From(metaLeftHand.device, out InputEventPtr lefttHandEventPtr))
        {
            ((ButtonControl)metaLeftHand.device[pinchActionReference.action.name]).WriteValueIntoEvent(OVRLeftHand.GetFingerIsPinching(OVRHand.HandFinger.Index) ? 1f : 0f, lefttHandEventPtr);
            ((AxisControl)metaLeftHand.device[pinchValueActionReference.action.name]).WriteValueIntoEvent(OVRLeftHand.GetFingerPinchStrength(OVRHand.HandFinger.Index), lefttHandEventPtr);
            InputSystem.QueueEvent(lefttHandEventPtr);
        }
    }
}
