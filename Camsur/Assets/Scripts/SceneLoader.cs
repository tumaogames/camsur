using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static List<GameObject> preservedObjects = new List<GameObject>();
    public static string previousScene;
    public static bool isPlayingMiniGame;
    public GameObject ChatManager;
    public GameObject playerPrefab;
    GameObject myPlayerGo;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        ChatManager = GameObject.Find("ChatManager");
        Destroy(ChatManager.gameObject);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void LoadScene(string sceneName)
    {
        previousScene = SceneManager.GetActiveScene().name;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PreserveOnLoad"))
        {
            obj.SetActive(false);
            if (GameManager.Instance) Destroy(GameManager.Instance.gameObject);
            isPlayingMiniGame = true;
        }
        
        PhotonNetwork.LoadLevel(sceneName);
    }

    public static void LoadPreviousScene()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(previousScene);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        foreach (GameObject obj in preservedObjects)
        {
            if (obj != null)
            {
                //myPlayerGo = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
                //GameManager.Instance.myPlayer = myPlayerGo;
                DontDestroyOnLoad(obj);
                //isPlayingMiniGame = false;
            }
        }
    }
}
