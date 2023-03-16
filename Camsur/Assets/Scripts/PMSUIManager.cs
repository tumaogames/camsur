using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using Photon;
using Photon.Realtime;

public class PMSUIManager : MonoBehaviourPunCallbacks
{
    public TMP_Text username;
    public Image selectBorder;
    public TMP_Text errorMessage;
    public MultiplayerManager mManager;
    public Image loadingImage;
    string gameVersion = "1";

    // Start is called before the first frame update
    void Start()
    {
        username.text = GameManager.Instance.Player.username.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void StartGame()
    {
        loadingImage.gameObject.SetActive(true);
        PhotonNetwork.JoinOrCreateRoom("MainRoom", new RoomOptions { MaxPlayers = 6 }, null);
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel("ReisanCity");
    }
}
