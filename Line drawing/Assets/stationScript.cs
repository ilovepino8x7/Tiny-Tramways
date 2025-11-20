using Unity.VisualScripting;
using UnityEngine;

public class stationScript : MonoBehaviour
{
    private LineRenderer lr;
    private EdgeCollider2D ed;
    public GameObject Line;
    public LineDrawer ld;
    private GameObject closestToEnd;
    private bool drawing;
    private bool checking;
    private float maxDist = 1.5f;
    public lineScript ls;
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
            if (ls != null)
            {
                ls.SetEnd();
            }
        }
    }
    private void makeLine()
    {
        GameObject line = Instantiate(Line);
        ls = line.GetComponent<lineScript>();
        ls.maxDist = maxDist;
        ls.origin = transform.gameObject;
        lr = line.GetComponent<LineRenderer>();
        ed = line.GetComponent<EdgeCollider2D>();
        lr.positionCount = 0;
        Vector2 start = transform.position;
        lr.positionCount = 1;
        lr.SetPosition(0, start);
    }

}
