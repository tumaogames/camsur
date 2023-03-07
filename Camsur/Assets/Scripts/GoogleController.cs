using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class GoogleController : MonoBehaviour
{
    public string clientId = "684438695777-74q33vpb2m7e6881e9geigglvh73qail.apps.googleusercontent.com";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    [DllImport("__Internal")]
    private static extern void OpenOAuthInExternalTab(string url, string callbackFunctionName);
    public void Login(string callbackFunctionName)
    {
        var redirectUri = "https://camsurverse.unligames.com";
        var url = "https://accounts.google.com/o/oauth2/v2/auth"
                + $"?client_id={clientId}"
                + "&response_type=token"
                + "&scope=openid%20email%20profile"
                + $"&redirect_uri={redirectUri}";
        OpenOAuthInExternalTab(url, callbackFunctionName);
    }
    public void callbackFunctionName(string res)
    {
        var player = new PlayerInfo()
        {
            username = res,
            user_id = 0100,
            login = true,
            contact_number = 0
        };
        var Jplayer = JsonConvert.SerializeObject(player);

        GameManager.Instance.Player = PlayerInfo.CreateFromJSON(Jplayer);

        SceneManager.LoadScene("PlayerMenuScene");
    }
}
