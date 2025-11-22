/*using System;
using System.Collections;
using UnityEngine;

public class trainMovement : MonoBehaviour
{
    private lineScript line;
    private bool going = true;
    private int holding;
    private bool collecting = false;
    private bool moving = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = transform.parent.GetComponent<lineScript>();
        transform.position = line.path[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && !collecting)
        {
            if (going)
            {
                transform.position = Vector2.MoveTowards(transform.position, line.path[1], 3 * Time.deltaTime);
                if (Vector2.Distance(transform.position, line.path[1]) <= 0.1f)
                {
                    going = false;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, line.path[0], 3 * Time.deltaTime);
                if (Vector2.Distance(transform.position, line.path[0]) <= 0.1f)
                {
                    going = true;
                }
            }
        }

    }
    private IEnumerator collectPassengers(int time, GameObject station)
    {
        collecting = true;
        moving = false;
        Debug.LogError("collecting");
        yield return new WaitForSeconds(time);
        moving = true;
        holding += station.transform.gameObject.GetComponent<stationScript>().passengers;
        station.transform.gameObject.GetComponent<stationScript>().passengers = 0;
        collecting = false;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "station" && coll.gameObject.GetComponent<stationScript>().passengers >= 1 && !collecting)
        {
            if (!collecting)
            {
                StartCoroutine(collectPassengers(2, coll.transform.gameObject));
            }
        }
    }
}
*/