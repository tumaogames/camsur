using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _playButton;
    [SerializeField] private TMP_Text _score;
    [SerializeField]
    GameObject[] rootObjects;

    void Start()
    {
    }

    public void Starting()
    {
    }

    private void Awake()
    {
        Bird.OnDeath += OnGameOver;
        Bird.OnDeath += OnGMSScore;
        Bird.OnDeath += OnGMSSetScore;
        Bird.OnScore += OnScore;
    }

    private void OnDestroy()
    {
        Bird.OnDeath -= OnGameOver;
        Bird.OnDeath -= OnGMSSetScore;
        Bird.OnScore -= OnScore;
        Bird.OnDeath -= OnGMSScore;
    }

    public void RestartGame()
    {
        SceneManager.UnloadSceneAsync("FlamingSunbird Minigame");
        rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            if (obj.tag != "DontDestroy")
                obj.SetActive(false);
        }
        SceneManager.LoadScene("FlamingSunbird Minigame", LoadSceneMode.Additive);
        Bird.dead = false;
    }

    private void OnGameOver()
    {
        _playButton.SetActive(true);
    }

    private void OnScore() => _score.text = (int.Parse(_score.text) + 1).ToString();
    private void OnGMSScore() => GameManager.Instance.FlappyScore = Int32.Parse(_score.text);
    private void OnGMSSetScore() => SetScore();

    public void SetScore()
    {
        string suserN = GameManager.Instance.Player.username;
        if(GameManager.Instance.FlappyScore > GameManager.Instance.Player.score)
        {
            string score = _score.text;
            Debug.Log(score);
            StartCoroutine(SetScoreC(suserN, "https://www.tumaogames.com/ci/users/setUserScore", score));
        }
    }

    IEnumerator SetScoreC(string suserN, string url, string score)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", suserN);
        form.AddField("score", score);
        Debug.Log("yeah");
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
               string result = www.downloadHandler.text;
            }
        }
    }
}
