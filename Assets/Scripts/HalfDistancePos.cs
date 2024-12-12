using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDistancePos : MonoBehaviour
{
    public Transform transformOne;
    public Transform transformTwo;
    // Update is called once per frame
    void Update()
    {
        Vector2 midpoint = (transformOne.position + transformTwo.position) / 2;
        this.transform.position = midpoint;
    }
}
