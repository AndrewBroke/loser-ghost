using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZone : MonoBehaviour
{

    GameManager gameManager;
    
    void Awake()
    {
        gameManager = GameObject.Find("MiniGame").GetComponent<GameManager>();
        RandRotate();        
    }

    internal void RandRotate()
    {
        float rand = Random.Range(0, 360);
        transform.Rotate(Vector3.forward, rand);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "pointer")
        {
            gameManager.inZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "pointer")
        {
            gameManager.inZone = false;
        }
    }


}
