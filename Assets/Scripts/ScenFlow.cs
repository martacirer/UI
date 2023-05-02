using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenFlow : MonoBehaviour
{
    [SerializeField] GameObject blackPanel;
    [SerializeField] float waitTime;

    public IEnumerator NextScene(string nextScene)
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animator>().Play("NextScene");

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(nextScene);
    }
}
