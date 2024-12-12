using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    public GameObject[] enableThese;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < enableThese.Length; i++)
            {
                enableThese[i].SetActive(true);
            }
        }
        Destroy(gameObject);
    }
}
