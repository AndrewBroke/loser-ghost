using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    public bool isMoving = true;

    [SerializeField]
    float rotateSpeed = 1f, rotateRadiusX = 1f, rotateRadiusY = 1f;

    private Vector2 _centre;
    private float _angle;

    public float speed;
    public Transform target;

    private Vector3 zAxis = new Vector3(0, 0, -1);

    private float startX, startY;

    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();

        startX = transform.position.x;
        startY = transform.position.y;
        _centre = transform.position;
        transform.position = _centre + new Vector2(Mathf.Sin(_angle) * rotateRadiusX, Mathf.Cos(_angle) * rotateRadiusY);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)// && !animator.GetCurrentAnimatorStateInfo(0).IsName("appearance"))
        {
            _angle += rotateSpeed * Time.deltaTime;
            var x = Mathf.Sin(_angle) * rotateRadiusX;
            var y = Mathf.Cos(_angle) * rotateRadiusY;
            transform.position = _centre + new Vector2(x, y);
        }
        
    }

    void FixedUpdate()
    {
        if (isMoving)// && !animator.GetCurrentAnimatorStateInfo(0).IsName("appearance"))
        {
            transform.RotateAround(target.position, zAxis, speed);
        }
    }

    internal void ReturnToStart()
    {
        GetComponent<Animator>().SetTrigger("appearance");
        _angle = 0;
        transform.position = _centre + new Vector2(Mathf.Sin(_angle) * rotateRadiusX, Mathf.Cos(_angle) * rotateRadiusY);
        transform.eulerAngles = new Vector3(0,0,-90);
    }
}
