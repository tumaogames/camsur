using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueBox : MonoBehaviour
{
    public int buttonInt;
    public GameObject DLButton;
    public TMP_Text DialogueText;
    public Button nextButton;
    public Button previousButton;
    public TextMeshProUGUI nextButtonText;
    public string SecondText = "Some of the gold coins will be hidden behind obstacles, so you will need to use your wits and quick reflexes to collect them.You can also earn bonus points by collecting power-ups that will enhance your abilities and help you to navigate through the game more easily. Once you have earned 300 gold coins in the Flaming Sunbird mini-game, you will be rewarded with a ticket to the CWC Skate Park. This is a popular location where you can showcase your skills and tricks on various ramps and obstacles. So, get ready adventurer, and show off your skills in the Flaming Sunbird mini-game to obtain your ticket to the CWC Skate Park! Good luck!";
    public string FirstText = "Hello adventurer! Your first task is to earn 300 gold coins in the Flaming Sunbird mini-game and obtain a ticket to the CWC Skate Park. The Flaming Sunbird is a legendary creature that can only be found in the deepest, darkest parts of the game world.It is said to have a fiery aura that can ignite any enemy it comes into contact with, making it a valuable asset to any adventurer. To complete this quest, you must navigate through the Flaming Sunbird mini-game and collect as many gold coins as possible.The game is designed to be challenging, so you will need to use your skills and strategy to succeed. As you play the mini-game, you will encounter various obstacles and challenges that will test your agility and reflexes.You must avoid crashing into walls or other obstacles and collect as many gold coins as you can to increase your score.";
    // Start is called before the first frame update
    void Start()
    {
        buttonInt = 1;
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("SetInteractible", .2f, 1f);
    }

    void SetInteractible()
    {
        if (buttonInt == 1)
        {
            nextButton.interactable = true;
            previousButton.interactable = false;
            nextButtonText.text = "next";
        }
        else
        {
            nextButtonText.text = "ok";
            previousButton.interactable = true;
        }
    }

    public void CloseButton()
    {
        Destroy(gameObject);
        NPC.OpenDialogueBox = false;
    }

    public void NextClick()
    {
        if (buttonInt == 2)
        {
            SceneManager.LoadScene("FlamingSunbird Minigame");
        }
        DialogueText.text = SecondText;
        buttonInt = 2;

    }

    public void PreviousClick()
    {
        DialogueText.text = FirstText;
        buttonInt = 1;
    }
}
