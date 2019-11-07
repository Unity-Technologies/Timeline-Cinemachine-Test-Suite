using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{

    public GameObject fireEffect;
    public Transform MuzzleLocation;

    public void Fire()
    {
        if (Application.isPlaying)
            Instantiate(fireEffect, MuzzleLocation.position, Quaternion.identity);
    }
}
