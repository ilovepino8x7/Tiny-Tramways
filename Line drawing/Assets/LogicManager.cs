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
        choice = Random.Range(0, spawns.Length - 1);
        if (spawns[choice].GetComponent<spawnPointScript>().occupied == false)
        {
            Instantiate(station, spawns[choice].position, Quaternion.identity);
            spawns[choice].GetComponent<spawnPointScript>().occupied = true;
        }
        else
        {
            makeStation();
        }
    }
    private void checkDrawing()
    {
        oneDrawing = false;
        GameObject[] stations = GameObject.FindGameObjectsWithTag("station");
        foreach(GameObject station in stations)
        {
            if (station.GetComponent<stationScript>().drawing == true)
            {
                oneDrawing = true;
                return;
            }
        }
    }
    
}
