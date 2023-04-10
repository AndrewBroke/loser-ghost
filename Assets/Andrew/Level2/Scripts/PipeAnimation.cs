using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PipeAnimation : MonoBehaviour
{
    [SerializeField] float animationSpeed = 1;
    [SerializeField] float animationFrequency = 0.01f;
    [SerializeField] private float initialFillAmount = 0;
    [SerializeField] private float endFillAmount = 1;
    [SerializeField] private UnityEvent endEvents;

    private Image _image;
    
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimation()
    {
        StartCoroutine("Animate");
    }

    IEnumerator Animate()
    {
        float frames = animationSpeed / 0.01f;
        float delta = (endFillAmount - initialFillAmount) / frames;
        for (int i = 0; i < frames; i++)
        {
            if(endFillAmount > initialFillAmount)
            {
                _image.fillAmount += delta;
            }
            else
            {
                _image.fillAmount -= delta;
            }
            yield return new WaitForSeconds(animationFrequency);
        }

        endEvents.Invoke();
    }
}
