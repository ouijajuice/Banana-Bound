using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScriptB : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    [SerializeField]
    private LineScript line;

    private void Start()
    {
        line.SetUpLine(points);
    }
}
