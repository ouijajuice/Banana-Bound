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
    public float sensitivity;
    private void Start()
    {
        line.SetUpLine(points);
        lineRenderer.startColor = defaultColor;
        lineRenderer.endColor = defaultColor;
    }

    private void Update()
    {
        float distance = Vector3.Distance(points[0].position, points[1].position);
        float t = Mathf.Clamp01(distance / sensitivity);

        Color currentColor = Color.Lerp(defaultColor, tensionColor, t);

        lineRenderer.startColor = currentColor;
        lineRenderer.endColor = currentColor;
    }

}
