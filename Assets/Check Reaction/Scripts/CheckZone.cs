using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZone : MonoBehaviour
{

    GameManager gameManager;
    
    void Awake()
    {
        gameManager = transform.parent.GetComponent<GameManager>();
        RandRotate();
    }

    internal void RandRotate()
    {
        GetComponent<Animator>().SetTrigger("appearance");
        float currentZ = transform.rotation.z;
        float rand = Random.Range(0, 360);
        while (true){
            if (Mathf.Abs(currentZ - rand) < 30)
            {
                rand = Random.Range(0, 360);
            }
            else
            {
                break;
            }      
        }
        
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
