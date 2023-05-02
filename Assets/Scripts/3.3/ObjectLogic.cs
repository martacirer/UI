using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectLogic : MonoBehaviour
{
    private GameLogic _gl;
    private void Start()
    {
        _gl = FindObjectOfType<GameLogic>();
        this.gameObject.SetActive(true);

        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnMouseOver()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        
        _gl.counter++;

        _gl.counterText.text = _gl.counter.ToString() + "/4";

        //Particulas


        this.gameObject.SetActive(false);
    }
}
