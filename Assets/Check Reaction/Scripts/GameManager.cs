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

    [SerializeField]
    float timeWaitEnd = 1, timeWaitStart = 0.5f;

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
                if(currentWins == needCountWins)
                {
                    winEvents.Invoke();
                    Destroy(transform.gameObject);
                }
                else
                {
                    StartCoroutine(wait());
                }
            }
            else
            {
                currentWins = 0;
                StartCoroutine(wait());
            }
        }
    }

    IEnumerator wait()
    {
        pointer.isMoving = false;
        yield return new WaitForSeconds(timeWaitEnd);
        checkZone.RandRotate();
        pointer.ReturnToStart();
        yield return new WaitForSeconds(timeWaitStart);
        pointer.isMoving = true;
    }
}
