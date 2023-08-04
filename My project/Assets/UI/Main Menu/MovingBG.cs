using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBG : MonoBehaviour
{

    public float rotationSpeed = 2f;

    void Update()
    {
        transform.Rotate(0,0,rotationSpeed * Time.deltaTime);
    }
}
