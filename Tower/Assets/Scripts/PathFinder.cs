using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public GameObject endPoint;
    GameObject pointParent;
    public GameObject finalPointParent;
    float disBetweenPoints = 2.1f;
    private int pointCount;
    public GameObject currentPoint;
    private int index = 0;
    Color color = new Color(0.2f, 1f, 0.2f, 1f);

    // Start is called before the first frame update
    void Start()
    {

        pointParent = GameObject.FindWithTag("Point");
    }

    // Update is called once per frame
    void Update()
    {   

        if(currentPoint == null)
        Debug.Log("null");
        transform.LookAt(currentPoint.transform);
        transform.position += transform.forward * .05f;
        if(Vector3.Distance(currentPoint.transform.position, transform.position) < .1)
        {
            currentPoint.GetComponent<Renderer>().material.SetColor("_Color", color);
            FindNextPoint();
        }
        
            
    }
    private void FindNextPoint()
    {
        
        float temp = 100000;
        bool reached = false;
        for(int i = 0; i < pointParent.transform.childCount; i++)
        {
            if(Vector3.Distance(pointParent.transform.GetChild(i).position, transform.position) < disBetweenPoints
            && Vector3.Distance(endPoint.transform.position, pointParent.transform.GetChild(i).position) < temp)
            {
                reached = true;
                index = i;
                //tempPoint.transform.position = pointParent.transform.GetChild(i).gameObject.transform.position;
                temp = Vector3.Distance(endPoint.transform.position, pointParent.transform.GetChild(i).position);
            }
        }
        if(reached)
        {
            pointCount++;
            Debug.Log("nextPoint");
            currentPoint = pointParent.transform.GetChild(index).gameObject;
            pointParent.transform.GetChild(index).transform.parent = finalPointParent.transform;
        }
        else
        {
            Destroy(gameObject);
            EnemyFollower.turnOn = true;
        }
    }
    
}
