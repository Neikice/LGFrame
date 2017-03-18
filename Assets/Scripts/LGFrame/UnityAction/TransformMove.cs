using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMove : MonoBehaviour
{

    public Transform target;
    public Vector3 ponit;
    public float speed;

    private void Start()
    {

    }

    public void SetToTarget()
    {
        this.transform.position = this.target.position;
    }

    public void SetToPoint()
    {
        this.transform.position = this.ponit;
    }

    public void MoveToTarget()
    {
        this.Move(this.target.position);
    }

    public void MoveToPoint()
    {
        this.Move(this.ponit);
    }

    void Move(Vector3 ponit)
    {
        var temp = (ponit - this.transform.position).normalized * Time.deltaTime * speed;
        this.transform.position = this.transform.position + temp;
    }
}
