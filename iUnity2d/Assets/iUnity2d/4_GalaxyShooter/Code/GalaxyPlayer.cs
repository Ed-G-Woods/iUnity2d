using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyPlayer : MonoBehaviour {

    public float MoveSpeed = 5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        InpputHandle();

	}

    private void InpputHandle()
    {
        float HInput = Input.GetAxis("Horizontal");
        float VInput = Input.GetAxis("Vertical");

        Vector3 MoveD = new Vector3(HInput, VInput, 0).normalized;
        transform.Translate(MoveD*Time.deltaTime*MoveSpeed);
    }
}
