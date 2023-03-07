using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class FacebookController : MonoBehaviour
{
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                    Debug.Log("Initialized");
                }
                else
                {
                    Debug.Log("Failed to Initialize the Facebook SDK");
                }
            });
        }
        else
        {
            FB.ActivateApp();
        }
    }

    public void LoginWithFacebook()
    {
        FB.LogInWithReadPermissions(new List<string> { "public_profile" }, LoginCallback);
    }

    private void LoginCallback(ILoginResult result)
    {
        if (result.Error != null)
        {
            Debug.LogError("Error logging in to Facebook: " + result.Error);
        }
        else if (result.Cancelled)
        {
            Debug.Log("Facebook login cancelled.");
        }
        else
        {
            Debug.Log("Facebook login successful.");
            if (FB.IsLoggedIn)
            {
                FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            }
            // You can now retrieve the user's Facebook profile information.
        }
    }

    public void DisplayUsername(IResult result)
    {
        string name = (string)result.ResultDictionary["first_name"];
        var player = new PlayerInfo()
        {
            username = name,
            user_id = 0100,
            login = true,
            contact_number = 0
        };

        var Jplayer = JsonConvert.SerializeObject(player);

        GameManager.Instance.Player = PlayerInfo.CreateFromJSON(Jplayer);
        
        SceneManager.LoadScene("PlayerMenuScene");
    }

}

