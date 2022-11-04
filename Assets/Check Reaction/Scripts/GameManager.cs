using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform circleTransform, pointerTransorm;

    [HideInInspector] public bool inZone = false;

    [SerializeField] int needCountWins = 3;
    [SerializeField] UnityEvent winEvents;

    int currentWins = 0;
    CheckZone checkZone;
    Pointer pointer;

    // Start is called before the first frame update
    void Start()
    {
        checkZone = circleTransform.GetComponent<CheckZone>();
        pointer = pointerTransorm.GetComponent<Pointer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            if (inZone)
            {
                currentWins++;
                Debug.Log(currentWins);
                if(currentWins == needCountWins)
                {
                    winEvents.Invoke();
                    Destroy(transform.gameObject);
                }
                else
                {
                    checkZone.RandRotate();
                    pointer.ReturnToStart();
                }
            }
            else
            {
                currentWins = 0;
                Debug.Log(currentWins);
                checkZone.RandRotate();
                pointer.ReturnToStart();
            }
        }
    }
}
