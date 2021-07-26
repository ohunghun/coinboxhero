using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    public GameObject loading;
    private void Awake()
    {
        bool isFirst = ES3.Load<bool>("isFirst", true);
        if (isFirst)
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        else
            loading.SetActive(false);
    }
    public void StartGame()
    {
        loading.SetActive(true);
        StartCoroutine("_startGame");
    }
    IEnumerator _startGame()
    {
        yield return null;
        if (isOpening())
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    bool isOpening()
    {
        bool isFirst=ES3.Load<bool>("isFirst", true);
        bool isOpening = ES3.Load<bool>("isOpeining", false);
        if( !isOpening&&!isFirst)
            return true;
        return false;
    }

}
