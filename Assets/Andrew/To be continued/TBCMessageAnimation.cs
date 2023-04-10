using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TBCMessageAnimation : MonoBehaviour
{
    [SerializeField] int _frames;
    [SerializeField] UnityEvent _pressEvents;

    TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();

        StartCoroutine("Animate");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            _pressEvents.Invoke();
        }
    }

    IEnumerator Animate()
    {
        while (true)
        {
            for (int i = 0; i < _frames; i++)
            {
                Color currentColor = _text.color;
                currentColor.a -= 1.0f / _frames;
                _text.color = currentColor;
                yield return new WaitForSeconds(0.02f);
            }

            for (int i = 0; i < _frames; i++)
            {
                Color currentColor = _text.color;
                currentColor.a += 1.0f / _frames;
                _text.color = currentColor;
                yield return new WaitForSeconds(0.02f);
            }
        }
        
    }
}
