using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    [SerializeField] Animator fadingAnimator;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EndIntro", 6.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EndIntro()
    {
        fadingAnimator.SetTrigger("FadeOut");
    }
}
