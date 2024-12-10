using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScriptB : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    [SerializeField]
    private LineScript line;
    [SerializeField] private LineRenderer lineRenderer;
    public Color defaultColor;
    public Color tensionColor;
    private void Start()
    {
        line.SetUpLine(points);
        lineRenderer.SetColors(defaultColor,defaultColor);
    }

    private void Update()
    {
        float distance = Vector2.Distance(points[0].position, points[1].position);
        lineRenderer.SetColors(Color.Lerp(defaultColor,tensionColor,distance), Color.Lerp(defaultColor, tensionColor, distance));
    }

}
