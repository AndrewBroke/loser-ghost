using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelWaiting : MonoBehaviour
{
    [SerializeField] GameObject endDialogue;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sr.color.a < 0.1f && endDialogue.activeSelf == false)
        {
            endDialogue.SetActive(true);
        }
    }
}
