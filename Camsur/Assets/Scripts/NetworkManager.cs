using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLib;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class NetworkManager : GSClass<NetworkManager>
{
    public TMP_InputField userName;
    public TMP_InputField Password;
    public string result;

    public string userN, passW;

    public void PostData()
    {
        userN = userName.text;
        passW = Password.text;
        StartCoroutine(Upload("Login", userN, passW, "https://www.tumaogames.com/ci/users/unityLogin"));
    }

    IEnumerator Upload(string method, string userN, string passW, string url)
    { 
        WWWForm form = new WWWForm();
        switch (method)
        {
            case "Login":
                form.AddField("username", userN);
                form.AddField("password", passW);
                break;
            default:
                // code block
                break;
        }
        using (UnityWebRequest www = UnityWebRequest.Post( url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                result = www.downloadHandler.text;
                GameManager.Instance.Player = PlayerInfo.CreateFromJSON(result);
                if (GameManager.Instance.Player.login)
                {
                    SceneManager.LoadScene("PlayerMenuScene");
                }
            }
        }
    }
}