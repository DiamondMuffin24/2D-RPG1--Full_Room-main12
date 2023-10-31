using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coinCounter = 0;
    public Text coinText;

    private void Update()
    {
        coinText.text = coinCounter.ToString();
    }

}
