using UnityEngine;
using UnityEngine.InputSystem;

public class lineScript : MonoBehaviour
{
    [HideInInspector]
    public GameObject origin;
    private LineRenderer lr;
    private EdgeCollider2D ed;
    private GameObject closest = null;
    [HideInInspector]
    public float maxDist;
    private bool drawing = true;
    public LogicManager ls;
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
        
        if (lr.positionCount >= 2)
        {
            Vector2[] points = { lr.GetPosition(0), lr.GetPosition(1) };
            ed.points = points;
        }
        if (Input.GetMouseButton(0) && !drawing && !ls.oneDrawing)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouse2d = new Vector2(mousePos.x, mousePos.y);
            Collider2D hit = Physics2D.OverlapCircle(mouse2d, 0.2f);
            if (hit != null && hit.gameObject == transform.gameObject)
            {
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
            drawing = false;
            Vector2 end = closest.transform.position;
            lr.positionCount = 2;
            lr.SetPosition(1, end);
            Vector2[] points = { lr.GetPosition(0), lr.GetPosition(1) };
            ed.points = points;
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (closest != null)
        {
            if (other.gameObject.tag == "station" && other.gameObject != origin)
            {
                Vector2 lineEnd = lr.GetPosition(lr.positionCount - 1);
                if (Vector2.Distance(other.transform.position, origin.transform.position) <= Vector2.Distance(closest.transform.position, origin.transform.position))
                {
                    closest = other.transform.gameObject;
                }

            }
        }
        else
        {
            if (other.gameObject.tag == "station" && other.gameObject != origin)
            {
                closest = other.transform.gameObject;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == closest)
        {
            closest = null;
        }
    }
    
}
