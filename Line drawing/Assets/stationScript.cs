using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class stationScript : MonoBehaviour
{
    private LineRenderer lr;
    private EdgeCollider2D ed;
    public GameObject Line;
    public LogicManager ls;
    private GameObject closestToEnd;
    [HideInInspector]
    public bool drawing;
    private bool checking;
    private float maxDist = 1.5f;
    public lineScript lsc;
    [HideInInspector]
    public List<GameObject> connected = new List<GameObject>();
    [HideInInspector]
    private List<GameObject> passengers;
    [HideInInspector]
    public int colour = 0;
    [HideInInspector]
    public int red = 5;
    [HideInInspector]
    public int green = 3;
    public GameObject passenger;
    private bool spawning = false;
    public TMP_Text pass;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ls = GameObject.FindWithTag("logos").GetComponent<LogicManager>();
        pass = transform.GetChild(1).GetChild(0).gameObject.GetComponent<TMP_Text>();
        passengers = new List<GameObject>();
        if (GetComponent<SpriteRenderer>().color == Color.red)
        {
            colour = 0;
        }
        else
        {
            colour = 1;
        }
    }
    void Update()
    {
        if (passengers.Count >= 8)
        {
            ls.EndGame();
        }
        if (!spawning && passengers.Count <= 7)
        {
            spawning = true;
            Invoke(nameof(spawnPassenger), 6);
        }
        pass.text = passengers.Count.ToString();
    }
    void OnMouseDown()
    {
        drawing = true;
        makeLine();
    }
    void OnMouseUp()
    {
        if (drawing)
        {
            drawing = false;
            if (lsc != null)
            {
                lsc.SetEnd();
            }
        }
    }
    private void makeLine()
    {
        GameObject line = Instantiate(Line);
        lsc = line.GetComponent<lineScript>();
        lsc.maxDist = maxDist;
        lsc.origin = transform.gameObject;
        lr = line.GetComponent<LineRenderer>();
        ed = line.GetComponent<EdgeCollider2D>();
        lr.positionCount = 0;
        Vector2 start = transform.position;
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        Vector2 offset = start + new Vector2(0.4f, 0);
        lr.SetPosition(1, offset);
    }
    public List<GameObject> GetPassengers()
    {
        return passengers;
    }
    public void GivePassengers()
    {
        passengers.Clear();
    }

    private void spawnPassenger()
    {
        GameObject p = Instantiate(passenger, transform.position, Quaternion.identity);
        if (Random.Range(0, 2) == 0)
        {
            p.GetComponent<SpriteRenderer>().color = Color.red;
            p.GetComponent<passengerControl>().colour = 0;
        }
        else
        {
            p.GetComponent<SpriteRenderer>().color = Color.green;
            p.GetComponent<passengerControl>().colour = 1;
        }
        if (p.GetComponent<passengerControl>().colour == 0)
        {
            p.transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y);
        }
        else
        {
            p.transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y);
        }
        passengers.Add(p);
        spawning = false;
    }
}
