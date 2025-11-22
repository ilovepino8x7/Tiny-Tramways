using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

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
    private int passengers = 8;
    private int colour = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    public int GetPassengers()
    {
        return passengers;
    }

}
