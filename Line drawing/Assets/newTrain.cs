using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class newTrain : MonoBehaviour
{
    private bool going = true;
    private bool collecting = false;
    private int holding;
    private int red;
    private int green;
    public GameObject line;
    public GameObject station1; // path[0]
    public GameObject station2; // path[1]
    public List<GameObject> passengers;
    public LogicManager ls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ls = GameObject.FindWithTag("logos").GetComponent<LogicManager>();
        transform.position = line.GetComponent<lineScript>().path[0];
        passengers = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!collecting)
        {
            if (going)
            {
                transform.position = Vector2.MoveTowards(transform.position, line.GetComponent<lineScript>().path[1], 3 * Time.deltaTime);
                if (Vector2.Distance(transform.position, line.GetComponent<lineScript>().path[1]) <= 0.1f)
                {
                    going = false;
                    StartCoroutine(collectPassengers(station2));
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, line.GetComponent<lineScript>().path[0], 3 * Time.deltaTime);
                if (Vector2.Distance(transform.position, line.GetComponent<lineScript>().path[0]) <= 0.1f)
                {
                    going = true;
                    StartCoroutine(collectPassengers(station1));
                }
            }
        }
        if (line == null)
        {
            Destroy(gameObject);
        }

    }
    private IEnumerator collectPassengers(GameObject station)
    {
        collecting = true;
        DropPassengers(station);
        if (passengers.Count <= 7)
        {
            passengers.AddRange(station.GetComponent<stationScript>().GetPassengers());
            station.GetComponent<stationScript>().GivePassengers();
            yield return new WaitForSeconds(2);
            foreach (GameObject pass in passengers)
            {
                pass.GetComponent<passengerControl>().train = gameObject;
            }
            collecting = false;
        }
        else
        {
            collecting = false;
            yield break;
        }
    }
    private void DropPassengers(GameObject station)
    {
        int targ = station.GetComponent<stationScript>().colour;
        for (int i = passengers.Count - 1; i >= 0; i--)
        {
            GameObject passenger = passengers[i];
            if (passenger != null && passenger.GetComponent<passengerControl>().colour == targ)
            {
                passengers.RemoveAt(i);
                Destroy(passenger);
                ls.journeys++;
            }
        }
    }

}
