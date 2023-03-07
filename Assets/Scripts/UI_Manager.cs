using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text coinText;
    public int coinCounter;

    public int playerLifesCounter = 3;
    public GameObject[] playerLifes;

    public static UI_Manager instance;
    private void Awake()
    {
        instance = this;
    }

    public void DeleteLatestPlayerLifesImage()
    {
        playerLifes[playerLifesCounter].SetActive(false);
    }

    public void IncreaseCoinCointerText()
    {
        Debug.Log("Increase coin counter");
        coinCounter = coinCounter + 1;
        coinText.text = coinCounter.ToString();
    }
}
