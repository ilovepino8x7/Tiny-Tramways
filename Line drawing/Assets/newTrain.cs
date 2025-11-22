using System.Collections;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = line.GetComponent<lineScript>().path[0];
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
                    StartCoroutine(collectPassengers());
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, line.GetComponent<lineScript>().path[0], 3 * Time.deltaTime);
                if (Vector2.Distance(transform.position, line.GetComponent<lineScript>().path[0]) <= 0.1f)
                {
                    going = true;
                    StartCoroutine(collectPassengers());
                }
            }
        }

        if (line == null)
        {
            Destroy(gameObject);
        }
        Debug.LogWarning(holding);
    }
    private IEnumerator collectPassengers()
    {
        collecting = true;
        holding += station2.GetComponent<stationScript>().GetPassengers();
        yield return new WaitForSeconds(2);
        collecting = false;
    }

}
