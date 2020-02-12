using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;

    public float maxTimer = 5;
    float timer = 0;

    private int direction = 1;
    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            timer = maxTimer;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180 * direction, transform.eulerAngles.z);
        }
        timer -= Time.deltaTime;   
        var t = transform;
        t.position += t.forward * (direction * (Time.deltaTime * speed));
    }
}
