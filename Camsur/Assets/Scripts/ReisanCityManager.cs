using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ReisanCityManager : MonoBehaviourPunCallbacks
{
    public Image home;
    public GameObject ChatManager;
    public SimpleTouchController rightController;
    // Start is called before the first frame update
    void Start()
    {
        ChatManager = GameObject.Find("ChatManager");
        if (Application.platform == RuntimePlatform.Android)
        {
            rightController.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnHome()
    {
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

    public override void OnLeftRoom()
    {
        Debug.Log("player left");
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
