using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class opneingAni : MonoBehaviour
{
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void aniPause()
    {
        ani.speed = 0;
        
    }
    public void resume()
    {
        ani.speed = 1;
    }
    public void goToGame()
    {
        ES3.Save<bool>("isOpeining", true);
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
