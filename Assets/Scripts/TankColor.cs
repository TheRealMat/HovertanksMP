using MLAPI.NetworkVariable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class TankColor : NetworkBehaviour
{
    private NetworkVariableByte colorIndexRed = new NetworkVariableByte();
    private NetworkVariableByte colorIndexGreen = new NetworkVariableByte();
    private NetworkVariableByte colorIndexBlue = new NetworkVariableByte();
    private NetworkVariableByte colorIndexGlow = new NetworkVariableByte();

    Colors colors;


    [ServerRpc]
    public void setColorRedServerRpc(byte newColorIndex)
    {
        if (newColorIndex > colors.colorsRed.Length) { return; }
        colorIndexRed.Value = newColorIndex;
    }
    [ServerRpc]
    public void setColorGreenServerRpc(byte newColorIndex)
    {
        if (newColorIndex > colors.colorsGreen.Length) { return; }
        colorIndexGreen.Value = newColorIndex;
    }
    [ServerRpc]
    public void setColorBlueServerRpc(byte newColorIndex)
    {
        if (newColorIndex > colors.colorsBlue.Length) { return; }
        colorIndexBlue.Value = newColorIndex;
    }
    [ServerRpc]
    public void setColorGlowServerRpc(byte newColorIndex)
    {
        if (newColorIndex > colors.colorsGlow.Length) { return; }
        colorIndexGlow.Value = newColorIndex;
    }

    private void OnEnable()
    {
        colors = NetworkManager.Singleton.gameObject.GetComponent<Colors>();
        colorIndexRed.OnValueChanged += OnColorChangedRed;
        colorIndexGreen.OnValueChanged += OnColorChangedGreen;
        colorIndexBlue.OnValueChanged += OnColorChangedBlue;
        colorIndexGlow.OnValueChanged += OnColorChangedGlow;
    }
    private void OnDisable()
    {
        colorIndexRed.OnValueChanged -= OnColorChangedRed;
        colorIndexGreen.OnValueChanged -= OnColorChangedGreen;
        colorIndexBlue.OnValueChanged -= OnColorChangedBlue;
        colorIndexGlow.OnValueChanged -= OnColorChangedGlow;
    }

    // maybe don't hardcode this, retard
    private void OnColorChangedRed(byte oldColorIndex, byte newColorIndex)
    {
        if (!IsClient) { return; }
        // using a propertyblock here would be more effecient
        GetComponent<Renderer>().material.SetVector("ColorRed", colors.colorsRed[newColorIndex]);
    }
    private void OnColorChangedGreen(byte oldColorIndex, byte newColorIndex)
    {
        if (!IsClient) { return; }
        GetComponent<Renderer>().material.SetVector("ColorGreen", colors.colorsGreen[newColorIndex]);
    }
    private void OnColorChangedBlue(byte oldColorIndex, byte newColorIndex)
    {
        if (!IsClient) { return; }
        GetComponent<Renderer>().material.SetVector("ColorBlue", colors.colorsBlue[newColorIndex]);
    }
    private void OnColorChangedGlow(byte oldColorIndex, byte newColorIndex)
    {
        if (!IsClient) { return; }
        GetComponent<Renderer>().material.SetVector("ColorGlow", colors.colorsGlow[newColorIndex]);
    }
}
