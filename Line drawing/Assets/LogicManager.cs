using UnityEngine;

public class LogicManager : MonoBehaviour
{
    private LineRenderer lr;
    private EdgeCollider2D ed;
    public GameObject Line;
    public GameObject[] stations;
    public Transform[] spawns;
    private int choice = 0;
    public GameObject station;
    [HideInInspector]
    public bool oneDrawing;
    private int stationCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftCommand))
        {
            makeStation();
        }
        checkDrawing();
    }
    private void makeStation()
    {
        if (stationCount == 0)
        {
            choice = Random.Range(0, spawns.Length - 1);
            if (spawns[choice].GetComponent<spawnPointScript>().occupied == false)
            {
                GameObject newStat = Instantiate(station, spawns[choice].position, Quaternion.identity);
                newStat.GetComponent<SpriteRenderer>().color = Color.red;
                stationCount++;
                spawns[choice].GetComponent<spawnPointScript>().occupied = true;
            }
            else
            {
                makeStation();
            }
        }
        else if (stationCount == 1)
        {
            choice = Random.Range(0, spawns.Length - 1);
            if (spawns[choice].GetComponent<spawnPointScript>().occupied == false)
            {
                GameObject newStat = Instantiate(station, spawns[choice].position, Quaternion.identity);
                newStat.GetComponent<SpriteRenderer>().color = Color.green;
                stationCount++;
                spawns[choice].GetComponent<spawnPointScript>().occupied = true;
            }
            else
            {
                makeStation();
            }
        }
        else
        {
            choice = Random.Range(0, spawns.Length - 1);
            if (spawns[choice].GetComponent<spawnPointScript>().occupied == false)
            {
                GameObject newStat = Instantiate(station, spawns[choice].position, Quaternion.identity);
                if (Random.Range(0,2) == 0)
                {
                    newStat.GetComponent<SpriteRenderer>().color = Color.red;
                }
                else
                {
                    newStat.GetComponent<SpriteRenderer>().color = Color.green;
                }
                stationCount++;
                spawns[choice].GetComponent<spawnPointScript>().occupied = true;
            }
            else
            {
                makeStation();
            }
        }
    }
    private void checkDrawing()
    {
        oneDrawing = false;
        GameObject[] stations = GameObject.FindGameObjectsWithTag("station");
        foreach (GameObject station in stations)
        {
            if (station.GetComponent<stationScript>().drawing == true)
            {
                oneDrawing = true;
                return;
            }
        }
    }

}
