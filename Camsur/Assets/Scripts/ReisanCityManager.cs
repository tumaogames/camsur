using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ReisanCityManager : MonoBehaviourPunCallbacks
{
    public Image home;
    PhotonView PBV;
    public GameObject ChatManager;
    // Start is called before the first frame update
    void Start()
    {
        PBV = GetComponent<PhotonView>();
        ChatManager = GameObject.Find("ChatManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnHome()
    {
        if (!PBV.IsMine)
        {
            return;
        }
        LeaveRoom();
        
    }

    public void LeaveRoom()
    {
        if (NetworkManager.Instance) Destroy(NetworkManager.Instance.gameObject);
        if (GameManager.Instance) Destroy(GameManager.Instance.gameObject);
        if (AudioManager.Instance) Destroy(GameManager.Instance.gameObject);
        Destroy(ChatManager);
        StartCoroutine(DoSwitchLevel());
    }

    IEnumerator DoSwitchLevel()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        PhotonNetwork.LoadLevel("MenuScene");
    }
}
