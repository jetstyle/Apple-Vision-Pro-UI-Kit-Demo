using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEnvironment : MonoBehaviour
{
    public Camera Camera;
    public OVRPassthroughLayer Passthrough;

    public void SwitchToCubemap(bool isOn)
    {
        if (!isOn)
            return;

        Camera.clearFlags = CameraClearFlags.Skybox;
        Passthrough.enabled = false;
        Shader.SetGlobalFloat("_staticValue", 0.0f);
    }

    public void SwitchToPassthrough(bool isOn)
    {
        if (!isOn)
            return;

        Camera.clearFlags = CameraClearFlags.SolidColor;
        Passthrough.enabled = true;
        Shader.SetGlobalFloat("_staticValue", 0.0f);
    }

    public void SwitchToPassthroughWithTransparent(bool isOn)
    {
        if (!isOn)
            return;

        Camera.clearFlags = CameraClearFlags.SolidColor;
        Passthrough.enabled = true;
        Shader.SetGlobalFloat("_staticValue", 1.0f);
    }
}
