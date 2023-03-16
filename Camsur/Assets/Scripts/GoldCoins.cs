using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCoins : MonoBehaviour
{
    public TMP_Text goldCoins;

    // Start is called before the first frame update
    void Start()
    {
        goldCoins.text = GameManager.Instance.Player.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
