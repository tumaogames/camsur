using UnityEngine;
using Photon.Pun;

public class PhotonCamera : MonoBehaviourPun
{
    public void Start()
    {
        //stop assigning controls if this is not the player related to this peer
        if (!photonView.IsMine) return;
        this.GetComponent<Camera>().enabled = true;
    }
}
