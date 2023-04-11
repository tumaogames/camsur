using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class Warp : MonoBehaviourPunCallbacks, IConnectionCallbacks
{
    public string levelName;
    public string roomName;
    private bool hasLeftRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !hasLeftRoom)
        {
            hasLeftRoom = true;
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnLeftRoom()
    {
        StartCoroutine("JoinRoom");
    }

    IEnumerator JoinRoom()
    {
        // As long as the flag is NOT set
        while (PhotonNetwork.NetworkClientState != ClientState.ConnectedToMasterServer)
        {
            // wait a frame
            yield return null;
        }

        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 6 }, null);
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.LoadLevel(levelName);
    }
}
