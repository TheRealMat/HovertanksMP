using MLAPI;
using MLAPI.Connection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : NetworkBehaviour
{


    Colors colors;


    public void GiveRandomColors()
    {
        colors = NetworkManager.Singleton.gameObject.GetComponent<Colors>();
        SelectColorRed(Random.Range(1, colors.colorsRed.Length));
        SelectColorGreen(Random.Range(1, colors.colorsGreen.Length));
        SelectColorBlue(Random.Range(1, colors.colorsBlue.Length));
        SelectColorGlow(Random.Range(1, colors.colorsGlow.Length));
    }

    // button can only have one parameter, also has to be int
    // pls refactor, this is really bad
    public void SelectColorRed(int colorIndexRed)
    {
        // get local client id
        ulong localClientId = NetworkManager.Singleton.LocalClientId;

        // get local client object
        if (!NetworkManager.Singleton.ConnectedClients.TryGetValue(localClientId, out NetworkClient networkClient))
        {
            return;
        }

        TankColor[] tankColors = networkClient.PlayerObject.GetComponentsInChildren<TankColor>();
        if (tankColors == null)
        {
            return;
        }
        foreach (TankColor tankColor in tankColors)
        {
            tankColor.setColorRedServerRpc((byte)colorIndexRed);
        }
    }
    public void SelectColorGreen(int colorIndexGreen)
    {
        // get local clinet id
        ulong localClientId = NetworkManager.Singleton.LocalClientId;

        // get local client object
        if (!NetworkManager.Singleton.ConnectedClients.TryGetValue(localClientId, out NetworkClient networkClient))
        {
            return;
        }

        TankColor[] tankColors = networkClient.PlayerObject.GetComponentsInChildren<TankColor>();
        if (tankColors == null)
        {
            return;
        }
        foreach (TankColor tankColor in tankColors)
        {
            tankColor.setColorGreenServerRpc((byte)colorIndexGreen);
        }
    }
    public void SelectColorBlue(int colorIndexBlue)
    {
        // get local clinet id
        ulong localClientId = NetworkManager.Singleton.LocalClientId;

        // get local client object
        if (!NetworkManager.Singleton.ConnectedClients.TryGetValue(localClientId, out NetworkClient networkClient))
        {
            return;
        }

        TankColor[] tankColors = networkClient.PlayerObject.GetComponentsInChildren<TankColor>();
        if (tankColors == null)
        {
            return;
        }
        foreach (TankColor tankColor in tankColors)
        {
            tankColor.setColorBlueServerRpc((byte)colorIndexBlue);
        }
    }

    public void SelectColorGlow(int colorIndexGlow)
    {
        // get local client id
        ulong localClientId = NetworkManager.Singleton.LocalClientId;

        // get local client object
        if (!NetworkManager.Singleton.ConnectedClients.TryGetValue(localClientId, out NetworkClient networkClient))
        {
            return;
        }

        TankColor[] tankColors = networkClient.PlayerObject.GetComponentsInChildren<TankColor>();
        if (tankColors == null)
        {
            return;
        }
        foreach (TankColor tankColor in tankColors)
        {
            tankColor.setColorGlowServerRpc((byte)colorIndexGlow);
        }
    }
}
