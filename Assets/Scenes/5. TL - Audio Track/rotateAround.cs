using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAround : MonoBehaviour
{

    public GameObject target;

    public float distanceFromTarget = 1;
    public float speed = 0.01f;

    float x = 0;
    float z = 0;

    // Update is called once per frame
    void Update()
    {
        x += speed;
        z += speed;

        transform.position = target.transform.position + new Vector3(Mathf.Sin(x) * distanceFromTarget, 0, Mathf.Cos(z) * distanceFromTarget);
    }
}
