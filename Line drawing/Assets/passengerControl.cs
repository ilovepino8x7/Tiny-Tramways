using UnityEngine;

public class passengerControl : MonoBehaviour
{
    [HideInInspector]
    public int colour;
    [HideInInspector]
    public GameObject train;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (train != null)
        {
            transform.position = train.transform.position;
        }
    }
}
