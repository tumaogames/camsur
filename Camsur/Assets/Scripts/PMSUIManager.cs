using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PMSUIManager : MonoBehaviourPunCallbacks
{
    public TMP_Text username;
    public Image selectBorder;
    public TMP_Text errorMessage;
    public MultiplayerManager mManager;
    public Image loadingImage;
    string gameVersion = "1";
    public Button startGame;
    private const string ROOM_NAME = "MainRoom";

    // Start is called before the first frame update
    void Start()
    {
        username.text = GameManager.Instance.Player.username.ToString();
        InvokeRepeating("ButtonStartActivate", 0.3f, 0.3f);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void ButtonStartActivate()
    {
        if (MultiplayerManager.onConnectedtoMaster)
        {
            startGame.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {

    }

    public void StartGame()
    {
        loadingImage.gameObject.SetActive(true);
        PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, new RoomOptions { MaxPlayers = 6 }, null);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Masterclient");
        }
        
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("ReisanCity");
    }
}
