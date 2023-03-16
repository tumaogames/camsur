using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections;

public class ChatReturnInput : GSClass<ChatReturnInput>
{
    public PhotonChatManager chatManager;
    public TMP_InputField inputField;
    public bool typing;

    void Start()
    {
        chatManager = GetComponent<PhotonChatManager>();
        StartCoroutine(WaitAndPrint(1.0f));
    }

    void Update()
    {
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
        chatManager.SubmitPublicChatOnClick(ctext);
    }

    //Called when Input changes
    public void ButtoninputSubmitCallBack()
    {
        Debug.Log("button Input Submitted");
        chatManager.SubmitPublicChatOnClick(inputField.text);
    }

    void OnDisable()
    {
        //Un-Register InputField Events
        inputField.onEndEdit.RemoveAllListeners();
        inputField.onValueChanged.RemoveAllListeners();
    }
}
