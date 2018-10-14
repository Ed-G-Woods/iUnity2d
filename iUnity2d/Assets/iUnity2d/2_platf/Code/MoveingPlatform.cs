using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPlatform : MonoBehaviour {
    public Vector3 finishePos = Vector3.zero;
    public float speed = 0.5f;

    private Vector3 StartPos;
    private float trackPercent=0;
    private int direction = 1; 

	// Use this for initialization
	void Start () {
        StartPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        trackPercent += speed * Time.deltaTime * direction;
        float x = (finishePos.x - StartPos.x) * trackPercent+ StartPos.x;
        float y = (finishePos.y - StartPos.y) * trackPercent+ StartPos.y;
        transform.position = new Vector3(x, y, StartPos.z);

        if ((trackPercent>0.9&&direction==1)||(trackPercent<0.1&&direction==-1))
        {
            direction *= -1;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, finishePos);
    }
}
