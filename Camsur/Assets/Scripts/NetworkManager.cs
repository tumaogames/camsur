using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

public class NetworkManager : GSClass<NetworkManager>
{
    //Login
    public TMP_InputField userName;
    public TMP_InputField Password;
    //Register
    public TMP_InputField registerUserName;
    public TMP_InputField registerPassword;
    public TMP_InputField confirmPassword;
    public TMP_InputField email;
    public TMP_InputField contactNumber;
    public MSUIManager ms_UIManager;

    public string result;

    public string userN, passW, ruserN, rpassW, confirmPass, mail, contactNum;

    public void Login()
    {
        userN = userName.text;
        passW = Password.text;
        StartCoroutine(LoginUpload(userN, passW, "https://www.tumaogames.com/ci/users/unityLogin"));
    }

    public void Register()
    {
        ruserN = registerUserName.text;
        rpassW = registerPassword.text;
        confirmPass = confirmPassword.text;
        mail = email.text;
        contactNum = contactNumber.text;
        StartCoroutine(RegisterUpload(ruserN, rpassW, confirmPass, mail, contactNum, "https://www.tumaogames.com/ci/users/unityRegister"));
    }

    IEnumerator LoginUpload(string userN, string passW, string url)
    { 
        WWWForm form = new WWWForm();
        ms_UIManager.ShowLoginLoadingImage();
        form.AddField("username", userN);
        form.AddField("password", passW);

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
                if (MyHelpers.ValidateJSON(result))
                {
                    GameManager.Instance.Player = PlayerInfo.CreateFromJSON(result);
                    if (GameManager.Instance.Player.login)
                    {
                        SceneManager.LoadScene("PlayerMenuScene");
                    }
                }
                else
                {
                    ms_UIManager.LoginErrorMessage.text = result;
                    ms_UIManager.ShowLoginLoadingImage(false);
                }
            }
        }
    }


    IEnumerator RegisterUpload(string ruserN, string rpassW, string confirmPass, string mail, string contactNumber, string url)
    {
        WWWForm form = new WWWForm();

                ms_UIManager.ShowRegisterLoadingImage();
                form.AddField("username", ruserN);
                form.AddField("password", rpassW);
                form.AddField("password_again", confirmPass);
                form.AddField("contact_number", contactNumber);
                form.AddField("email", mail);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                result = www.downloadHandler.text;
                /*GameManager.Instance.Player = PlayerInfo.CreateFromJSON(result);
                if (GameManager.Instance.Player.login)
                {
                    SceneManager.LoadScene("PlayerMenuScene");
                }*/
                ms_UIManager.RegisterErrorMessage.text = result;
                ms_UIManager.ShowRegisterLoadingImage(false);
            }
        }
    }

    IEnumerator Upload(string method, string userN, string passW, string confirmPass, string contactNumber, string mail, string url)
    {
        WWWForm form = new WWWForm();
        switch (method)
        {
            case "Login":
                ms_UIManager.ShowLoginLoadingImage();
                form.AddField("username", userN);
                form.AddField("password", passW);
                break;
            case "Register":
                ms_UIManager.ShowRegisterLoadingImage();
                form.AddField("username", userN);
                form.AddField("password", passW);
                form.AddField("password_again", confirmPass);
                form.AddField("contact_number", contactNumber);
                form.AddField("email", mail);
                break;
            default:
                // code block
                break;
        }
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            www.certificateHandler = new BypassCertificate();
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
                        else
                        {
                            ms_UIManager.LoginErrorMessage.text = result;
                            ms_UIManager.ShowLoginLoadingImage(false);
                        }
                        break;
                    case "Register":
                        result = www.downloadHandler.text;
                        /*GameManager.Instance.Player = PlayerInfo.CreateFromJSON(result);
                        if (GameManager.Instance.Player.login)
                        {
                            SceneManager.LoadScene("PlayerMenuScene");
                        }*/
                        ms_UIManager.RegisterErrorMessage.text = result;
                        ms_UIManager.ShowRegisterLoadingImage(false);
                        break;
                    default:
                        // code block
                        break;
                }
            }
        }
    }



}