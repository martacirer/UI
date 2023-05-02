using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField]private Sprite[] texture;
    private SpriteRenderer Sp;
    private int localType;
    private SpawnManager _sm;

    private Material mat;

    private void Awake()
    {
        Sp = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        StartCoroutine(StayTime(lifeTime));
        _sm = FindObjectOfType<SpawnManager>();

        Sp.sprite = texture[_sm.type];
        localType = _sm.type;
    }

    // Cuando se ha instanciado el objeto, comienza una cuenta de X segundo hasta que este se destruye
    private IEnumerator StayTime(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }


    private void OnMouseDown()
    {
        if (localType.Equals(0))
        {
            _sm.AddPoints(+1);
        }
        else
        {
            _sm.AddPoints(-1);
        }

        Destroy(this.gameObject);
    }
}
