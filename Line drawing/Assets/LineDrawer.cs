using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer lr;
    private EdgeCollider2D ed;
    public GameObject Line;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            makeLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lr.positionCount = 2;
            lr.SetPosition(1,end);
            Vector2[] points = {lr.GetPosition(0), lr.GetPosition(1)};
            ed.points = points;
        }
    }
    private void makeLine()
    {
        GameObject line = Instantiate(Line);
        lr = line.GetComponent<LineRenderer>();
        ed = line.GetComponent<EdgeCollider2D>();
        lr.positionCount = 0;
        Vector2 start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lr.positionCount = 1;
        lr.SetPosition(0,start);
    }
}
