/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AIPathFinding: MonoBehaviour {

    public Transform target;
    public float distanceToTarget;

    public float updateRate = 5f;
    float speed1 = 1.5f;
    private Seeker seeker;
    private Rigidbody2D rb;
    public Path path;
    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;
    Vector3 nextPoint;
    public float nextWayPointDistance = 0.7f;
    private int currentWaypoint = 0;
    GameObject player;
    public bool alerted = false;
    public bool stopped = false;
	// Use this for initialization
	void Start () {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        seeker.StartPath(transform.position, target.position,OnPathComplete);
        StartCoroutine(UpdatePath());
	}
    IEnumerator UpdatePath()
    {
        if(target==null)
        { 
            //return false;
        }
        seeker.StartPath(transform.position, target.position,OnPathComplete);
        yield return new WaitForSeconds(10f / updateRate);
        StartCoroutine(UpdatePath());
    }
    public void stopPathfinding()
    {
        StartCoroutine(UpdatePath());
    }
    public Vector3 returnNextPoint()
    {
        return nextPoint; 
    }
    public void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

	// Update is called once per frame
	void Update () {
		if(target==null)
        {

        }
        distanceToTarget = Vector3.Distance(this.transform.position, target.position);
	    if(target==null)
        {
            return;
        }
        if(path==null)
        {
            return;
        }
        if(currentWaypoint>=path.vectorPath.Count)
        {
            if(pathIsEnded)
            {
                return;
            }
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        if(target.gameObject!=this.gameObject)
        {
            this.transform.position += dir * Time.deltaTime * speed1;
        }
        if(currentWaypoint+1<path.GetTotalLength())
        {
            nextPoint = path.vectorPath[currentWaypoint + 1];
        }
        else
        {
            nextPoint = path.vectorPath[currentWaypoint];
        }
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if(dist<nextWayPointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
*/