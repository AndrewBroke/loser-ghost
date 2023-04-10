using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Hello();
    [DllImport("__Internal")]
    private static extern void ShowAdv();
    // Start is called before the first frame update
    void Start()
    {
        //Hello();
        try
        {
            ShowAdv();
        }
        catch
        {

        }
        
    }

    public void PauseGame()
    {

        Time.timeScale = 0;
        Camera.main.GetComponent<AudioListener>().enabled = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Camera.main.GetComponent<AudioListener>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
