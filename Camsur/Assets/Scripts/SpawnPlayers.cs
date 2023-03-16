using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public Camera standByCamera;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject myPlayerGo = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        //((MonoBehaviour)myPlayerGo.GetComponent("IsometricPlayerMovementController")).enabled = true;
        standByCamera.gameObject.SetActive(false);
        GameManager.Instance.myPlayer = myPlayerGo;
    }
}
