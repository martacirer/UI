using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject mainObject;
    [SerializeField] private float spawnTime;

    [SerializeField]private TextMeshProUGUI pointsText;
    public int pointsInt;

    public int type;
    public int pointsCounter;

    private void Start()
    {
        StartCoroutine(Spawn(spawnTime));
    }


    // Accede a la posicion de la camara para marcar los limites del spawn
    // Una vez tiene los limites, instancia el objeto aleatoriamente entre estos
    private IEnumerator Spawn(float seconds)
    {
        float xMin = Camera.main.transform.position.x - Camera.main.orthographicSize +1;
        float xMax = Camera.main.transform.position.x + Camera.main.orthographicSize -1;
        float yMin = Camera.main.transform.position.y - Camera.main.orthographicSize +1;
        float yMax = Camera.main.transform.position.y + Camera.main.orthographicSize -1;

        Vector3 randomPos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 5f);
        Instantiate(mainObject, randomPos, Quaternion.identity);


        SelectType();


        yield return new WaitForSeconds(seconds);
        StartCoroutine(Spawn(spawnTime));
        
    }


    private void SelectType()
    {
        type = Random.Range(0, 4);
    }


    public void AddPoints(int points)
    {
        pointsCounter += points;
        pointsText.text = pointsCounter.ToString();
    }

}
