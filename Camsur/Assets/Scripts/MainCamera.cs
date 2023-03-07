using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainCamera : MonoBehaviourPunCallbacks
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            GetComponent<Camera>().gameObject.SetActive(false);
        } else
        {
            GetComponent<Camera>().transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
