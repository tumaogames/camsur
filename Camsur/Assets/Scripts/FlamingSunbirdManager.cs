using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class FlamingSunbirdManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playButton;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnScene()
    {
        Bird.dead = false;
        _playButton.SetActive(false);
        SceneManager.UnloadSceneAsync("FlamingSunbird Minigame");
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            if (obj.tag != "DontDestroy")
                obj.SetActive(true);
        }
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.enabled = false;
            mainCamera.enabled = true;
        }
    }
}
