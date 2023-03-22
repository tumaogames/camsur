using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;

public class ChatManager : GSClass<ChatManager>, IChatClientListener
{
    [SerializeField] string username;
    string privateReceiver = "";
    [SerializeField] string currentChat;
    ChatClient chatClient;
    public bool isConnected;
    public TMP_Text chatDisplay;
    public TMP_InputField inputField;
    public Canvas myCanvas;
    public Transform chatOrigin;
    public GameObject myCallout;
    public bool typing;

    public void UsernameOnValueChange(string valueIn)
    {
        username = valueIn;
    }

    public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }

    public void SubmitPublicChatOnClick(string chattext)
    {
        if (privateReceiver == "" && inputField.text != "")
        {
            chatClient.PublishMessage("RegionChannel", currentChat);
            myCallout = PhotonNetwork.Instantiate("callOutBubble", chatOrigin.transform.position, Quaternion.identity,0 , new object[] { chattext });
            myCallout.GetComponent<ChatBubble>().Setup(chattext);
            if (chatOrigin.transform.childCount > 0)
            {
                for (int i = chatOrigin.transform.childCount - 1; i >= 0; i--)
                {
                    // Destroy each child object
                    GameObject.Destroy(chatOrigin.transform.GetChild(i).gameObject);
                }
            }
            myCallout.transform.SetParent(chatOrigin);
        }

        inputField.text = "";
        inputField.ActivateInputField(); //Re-focus on the input field
        inputField.Select(); //Re-focus on the input field
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        //throw new System.NotImplementedException();
    }

    public void OnConnected()
    {
        Debug.Log("Connected to chat");
        chatClient.Subscribe(new string[] { "RegionChannel" });
    }

    public void OnDisconnected()
    {
        //throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);
            chatDisplay.text += "\n " + msgs;
            Debug.Log(msgs);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        //throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        username = GameManager.Instance.Player.username;
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        Debug.Log("Connecting to chat");
        chatOrigin = GameManager.Instance.myPlayer.transform.Find("Boy").transform.Find("BoyRenderer").transform.Find("chatOrigin");
        StartCoroutine(WaitAndPrint(1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected)
        {
            chatClient.Service();
        }
        if (Input.GetKeyDown(KeyCode.Return) && inputField.text != "")
        {
            inputSubmitCallBack(inputField.text);
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (inputField.isFocused)
            {
                typing = true;
            }
            else
            {
                typing = false;
            }
        }
    }

    //Called when Input changes
    private void inputSubmitCallBack(string ctext)
    {
        Debug.Log("Input Submitted");
        SubmitPublicChatOnClick(ctext);
    }

    //Called when Input changes
    public void ButtoninputSubmitCallBack()
    {
        Debug.Log("button Input Submitted");
        SubmitPublicChatOnClick(inputField.text);
    }

    void OnDisable()
    {
        //Un-Register InputField Events
        inputField.onEndEdit.RemoveAllListeners();
        inputField.onValueChanged.RemoveAllListeners();
    }
}
