using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public Camera standByCamera;
    GameObject myPlayerGo;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        myPlayerGo = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        standByCamera.gameObject.SetActive(false);
        GameManager.Instance.myPlayer = myPlayerGo;
        ChatManager.Instance.chatOrigin = myPlayerGo.transform.Find("Boy").transform.Find("BoyRenderer").transform.Find("chatOrigin");
    }
}
