using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class lineScript : MonoBehaviour
{
    [HideInInspector]
    public GameObject origin;
    [HideInInspector]
    public Vector2[] path;
    private LineRenderer lr;
    private EdgeCollider2D ed;
    private GameObject closest = null;
    [HideInInspector]
    public float maxDist;
    private bool drawing = true;
    public LogicManager ls;
    public GameObject train;
    private List<GameObject> options = new List<GameObject>();
    private GameObject endd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        ls = GameObject.FindWithTag("logos").GetComponent<LogicManager>();
        ed = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drawing)
        {
            SetClosest();
            Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 point2d = new Vector2(cursor.x, cursor.y);
            lr.SetPosition(1, point2d);
            ed.points = new Vector2[] { lr.GetPosition(0), lr.GetPosition(1) };
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnTrain();
        }
        if (Input.GetMouseButton(0) && !drawing && !ls.oneDrawing)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouse2d = new Vector2(mousePos.x, mousePos.y);
            Collider2D hit = Physics2D.OverlapCircle(mouse2d, 0.2f);
            if (hit != null && hit.gameObject == transform.gameObject)
            {
                origin.GetComponent<stationScript>().connected.Remove(closest);
                Destroy(transform.gameObject);
            }
        }
        foreach (Touch touch in Input.touches)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 mouse2d = new Vector2(mousePos.x, mousePos.y);
            Collider2D hit = Physics2D.OverlapCircle(mouse2d, 0.2f);
            if (hit != null && hit.gameObject == transform.gameObject)
            {
                Destroy(transform.gameObject);
            }
        }
    }
    public void SetEnd()
    {
        if (closest != null)
        {
            endd = closest;
            origin.GetComponent<stationScript>().connected.Add(closest);
            drawing = false;
            Vector2 end = closest.transform.position;
            lr.positionCount = 2;
            lr.SetPosition(1, end);
            Vector2[] points = { lr.GetPosition(0), lr.GetPosition(1) };
            path = new Vector2[] { lr.GetPosition(0), lr.GetPosition(1) };
            ed.points = points;
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "station" && !options.Contains(other.gameObject))
        {
            options.Add(other.gameObject);
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "station" && options.Contains(other.gameObject))
        {
            options.Remove(other.gameObject);
        }
    }

    public void SpawnTrain()
    {
        if (endd != null)
        {
            Vector2 direction = lr.GetPosition(1) - lr.GetPosition(0);
            float ingle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (ingle < 0)
            {
                ingle += 360;
            }
            GameObject entrain = Instantiate(train, transform.position, Quaternion.Euler(0, 0, ingle));
            entrain.GetComponent<newTrain>().line = transform.gameObject;
            entrain.GetComponent<newTrain>().station1 = origin;
            entrain.GetComponent<newTrain>().station2 = endd;
        }
    }
    private void SetClosest()
    {
        closest = null;
        foreach (GameObject station in options)
        {
            if (origin.GetComponent<stationScript>().connected.Contains(station))
            {
                continue;
            }
            if (station == origin)
            {
                continue;
            }
            else if (closest == null)
            {
                closest = station;
            }
            else
            {
                if (Vector2.Distance(station.transform.position, origin.transform.position) <= Vector2.Distance(closest.transform.position, origin.transform.position))
                {
                    closest = station;
                }
            }
        }
    }
}


