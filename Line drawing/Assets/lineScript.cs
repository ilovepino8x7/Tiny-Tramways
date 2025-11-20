using UnityEngine;

public class lineScript : MonoBehaviour
{
    [HideInInspector]
    public GameObject origin;
    private LineRenderer lr;
    private EdgeCollider2D ed;
    private GameObject closest = null;
    [HideInInspector]
    public float maxDist;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
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

    }
    public void SetEnd()
    {
        if (closest != null)
        {
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
                if (Vector2.Distance(other.transform.position, lineEnd) <= Vector2.Distance(closest.transform.position, lineEnd))
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
}
