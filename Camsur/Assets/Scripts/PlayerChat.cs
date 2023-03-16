using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class PlayerChat : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    public string chatText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Speak(string chattext)
    {
        
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // Get the instantiated game object
        GameObject instantiatedObj = info.photonView.gameObject;

        // Get the custom data sent with the instantiation
        object[] instantiationData = info.photonView.InstantiationData;
        if (instantiationData != null && instantiationData.Length > 0)
        {
            chatText = (string)instantiationData[0];
        }
        info.photonView.gameObject.GetComponent<ChatBubble>().Setup(chatText);
    }
}
