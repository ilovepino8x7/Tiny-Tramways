using System;
using UnityEngine;

public class trainMovement : MonoBehaviour
{
    private lineScript line;
    private bool going = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = transform.parent.GetComponent<lineScript>();
        transform.position = line.path[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (going)
        {
            transform.position = Vector2.MoveTowards(transform.position, line.path[1], 5 * Time.deltaTime);
            if (Vector2.Distance(transform.position, line.path[1]) <= 0.1f)
            {
                Debug.LogWarning("At point1");
                going = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, line.path[0], 5 * Time.deltaTime);
            if (Vector2.Distance(transform.position, line.path[0]) <= 0.1f)
            {
                Debug.LogWarning("At point0");
                going = true;
            }
        }
    }
}
