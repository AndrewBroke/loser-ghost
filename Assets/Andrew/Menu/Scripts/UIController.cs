using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] Animator fadingAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLink()
    {
        Application.OpenURL("https://vk.com/brokenoceangames");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        fadingAnimator.SetTrigger("FadeOut");
    }
}
