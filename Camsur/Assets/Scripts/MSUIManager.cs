using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MSUIManager : MonoBehaviour
{
    public TMP_Text register;
    public GameObject loginMenu;
    public GameObject registerMenu;
    public TMP_Text errorMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRegisterMenu()
    {
        registerMenu.SetActive(true);
        loginMenu.SetActive(false);
    }

    public void ShowLoginMenu()
    {
        registerMenu.SetActive(false);
        loginMenu.SetActive(true);
    }
}
