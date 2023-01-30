using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLib;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class NetworkManager : GSClass<NetworkManager>
{
    //Login
    public TMP_InputField userName;
    public TMP_InputField Password;
    //Register
    public TMP_InputField registerUserName;
    public TMP_InputField registerPassword;
    public TMP_InputField confirmPassword;
    public TMP_InputField contactNumber;
    public MSUIManager ms_UIManager;

    public string result;

    public string userN, passW, ruserN, rpassW, confirmPass;
    public int contactNum;

    public void Login()
    {
        userN = userName.text;
        passW = Password.text;
        StartCoroutine(Upload("Login", userN, passW, "", 0, "https://www.tumaogames.com/ci/users/unityLogin"));
    }

    public void Register()
    {
        ruserN = registerUserName.text;
        rpassW = registerPassword.text;
        confirmPass = confirmPassword.text;
        contactNum = int.Parse(contactNumber.text);
        StartCoroutine(Upload("Register", ruserN, rpassW, confirmPass, contactNum, "https://www.tumaogames.com/ci/users/unityLogin"));
    }

    IEnumerator Upload(string method, string userN, string passW, string confirmPass, int contactNumber, string url)
    { 
        WWWForm form = new WWWForm();
        switch (method)
        {
            case "Login":
                form.AddField("username", userN);
                form.AddField("password", passW);
                break;
            case "Register":
                form.AddField("username", userN);
                form.AddField("password", passW);
                form.AddField("confirm_password", confirmPass);
                form.AddField("contact_number", contactNumber);
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
                switch (method)
                {
                    case "Login":
                        result = www.downloadHandler.text;
                        if (MyHelpers.ValidateJSON(result))
                        {
                            GameManager.Instance.Player = PlayerInfo.CreateFromJSON(result);
                            if (GameManager.Instance.Player.login)
                            {
                                SceneManager.LoadScene("PlayerMenuScene");
                            }
                        }
                        ms_UIManager.errorMessage.text = result;
                        break;
                    case "Register":
                        result = www.downloadHandler.text;
                        GameManager.Instance.Player = PlayerInfo.CreateFromJSON(result);
                        if (GameManager.Instance.Player.login)
                        {
                            SceneManager.LoadScene("PlayerMenuScene");
                        }
                        break;
                    default:
                        // code block
                        break;
                }
            }
        }
    }
}