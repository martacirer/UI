using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    public int counter = 0;
    public TextMeshProUGUI counterText;

    private void Start()
    {
        counterText.text = counter.ToString() + "/4";
    }


}
