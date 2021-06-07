using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;
using MLAPI.Transports.UNET;

public class MenuScript : MonoBehaviour
{
    public GameObject menuPanel;
    public string ipAddress = "127.0.0.1";
    UNetTransport transport;
    public Camera lobbyCamera;

    public void Host()
    {
        menuPanel.SetActive(false);
        lobbyCamera.gameObject.SetActive(false);
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(GetRandomSpawn(), Quaternion.identity);
    }

    private void ApprovalCheck(byte[] connectionData, ulong clinetID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == "Password1234";
        callback(true, null, approve, GetRandomSpawn(), Quaternion.identity);
    }




    public void Join()
    {
        transport = NetworkManager.Singleton.GetComponent<UNetTransport>();
        transport.ConnectAddress = ipAddress;
        menuPanel.SetActive(false);
        lobbyCamera.gameObject.SetActive(false);
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("Password1234");
        NetworkManager.Singleton.StartClient();
    }



    public void IpAddressChanged(string newAddress)
    {
        this.ipAddress = newAddress;
    }

    Vector3 GetRandomSpawn()
    {
        float x = Random.Range(-40f, 40f);
        float z = Random.Range(-40f, 40f);

        return new Vector3(x, 0f, z);
    }
}
