using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [HideInInspector]
    public int journeys = 0;
    public TMP_Text jtext;
    private bool spawning = false;
    public GameObject start;
    private bool started = false;
    private bool done = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (started && !done)
        {
            done = true;
            makeStation();
            makeStation();
        }
        if (!spawning)
        {
            spawning = true;
            Invoke(nameof(makeStation), 8);
        }
        checkDrawing();
        jtext.text = journeys.ToString();
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
                newStat.transform.GetChild(1).GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
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
                newStat.transform.GetChild(1).GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
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
                newStat.transform.GetChild(1).GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
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
        spawning = false;
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
    public void StartGame()
    {
        start.SetActive(false);
        started = true;
    }
    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SpawnTrains()
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag("lino");
        foreach (GameObject line in lines)
        {
            line.GetComponent<lineScript>().SpawnTrain();
        }
    }
}
