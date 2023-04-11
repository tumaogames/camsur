using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerManager : MonoBehaviourPunCallbacks, IConnectionCallbacks
{
    LoadBalancingClient loadBalancingClient;
    AppSettings appSettings = new AppSettings();
    private const string ROOM_NAME = "MainRoom";
    private const float RECONNECT_INTERVAL = 5f;

    string gameVersion = "1";
    public static bool onConnectedtoMaster;

    // Start is called before the first frame update
    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, new RoomOptions { MaxPlayers = 6 }, null);
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        try
        {
            loadBalancingClient = new LoadBalancingClient();
            LoadBalancingPeer loadBalancingPeer = (LoadBalancingPeer)loadBalancingClient.LoadBalancingPeer;
            loadBalancingPeer.MaximumTransferUnit = 500;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to initialize LoadBalancingClient: " + ex.Message);
        }
        onConnectedtoMaster = true;
        Debug.Log("Connected to master server.");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected from server: " + cause.ToString());

        StartCoroutine(Reconnect());
        if (this.CanRecoverFromDisconnect(cause))
        {
            this.Recover();
        }
    }

    IEnumerator Reconnect()
    {
        yield return new WaitForSeconds(RECONNECT_INTERVAL);

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinRoom(ROOM_NAME);
        }
    }

    private bool CanRecoverFromDisconnect(DisconnectCause cause)
    {
        switch (cause)
        {
            // the list here may be non exhaustive and is subject to review
            case DisconnectCause.Exception:
            case DisconnectCause.ServerTimeout:
            case DisconnectCause.ClientTimeout:
            case DisconnectCause.DisconnectByServerLogic:
            case DisconnectCause.DisconnectByServerReasonUnknown:
                return true;
        }
        return false;
    }

    private void Recover()
    {
        if (!loadBalancingClient.ReconnectAndRejoin())
        {
            Debug.LogError("ReconnectAndRejoin failed, trying Reconnect");
            if (!loadBalancingClient.ReconnectToMaster())
            {
                Debug.LogError("Reconnect failed, trying ConnectUsingSettings");
                if (!loadBalancingClient.ConnectUsingSettings(appSettings))
                {
                    Debug.LogError("ConnectUsingSettings failed");
                }
            }
        }
    }
}