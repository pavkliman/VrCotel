using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftActive;
    public InputActionProperty rightActive;

    public InputActionProperty leftChanel;
    public InputActionProperty rightChanel;

    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;

    // Update is called once per frame
    void Update()
    {
        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNoraml, out int leftNormal, out bool leftValid);

        leftTeleportation.SetActive(!isLeftRayHovering && leftChanel.action.ReadValue<float>() == 0 && leftActive.action.ReadValue<float>() > 0.1f);
        
        bool isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNoraml, out int rightNormal, out bool rightValid);

        rightTeleportation.SetActive(!isRightRayHovering && rightChanel.action.ReadValue<float>() == 0 && rightActive.action.ReadValue<float>() > 0.1f);
    }
}
