using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    public float RotateSpeed = 1f;
    public float RotateRadiusX = 1f;
    public float RotateRadiusY = 1f;

    private Vector2 _centre;
    private float _angle;

    public float speed;
    public Transform target;

    private Vector3 zAxis = new Vector3(0, 0, -1);

    private float startX, startY;

    // Start is called before the first frame update
    void Awake()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        _centre = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _angle += RotateSpeed * Time.deltaTime;
        var x = Mathf.Sin(_angle) * RotateRadiusX;
        var y = Mathf.Cos(_angle) * RotateRadiusY;
        transform.position = _centre + new Vector2(x, y);
    }

    void FixedUpdate()
    {
        transform.RotateAround(target.position, zAxis, speed);
    }

    internal void ReturnToStart()
    {
        transform.position = new Vector2(startX, startY);
        _angle = 0;
        _centre = transform.position;
        transform.eulerAngles = new Vector3(0,0,-90);
    }
}
