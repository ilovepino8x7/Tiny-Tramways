using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (drawing && lr != null)
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lr.positionCount = 2;
            lr.SetPosition(1, mouse);
        }
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
        lr.positionCount = 1;
        lr.SetPosition(0, start);
    }

}
