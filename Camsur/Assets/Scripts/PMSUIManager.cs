using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PMSUIManager : MonoBehaviour
{
    public TMP_Text username;
    public Image selectBorder;
    public TMP_Text errorMessage;

    // Start is called before the first frame update
    void Start()
    {
        //username.text = GameManager.Instance.Player.username.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Debug.Log("LoadingScene");
        SceneManager.LoadScene("ReisanCity");
    }
}
