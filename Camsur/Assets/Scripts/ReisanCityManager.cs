using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ReisanCityManager : MonoBehaviourPun
{
    public Image home;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnHome()
    {
        if (!photonView.IsMine) return;
        SceneManager.LoadScene("MenuScene");
    }
}
