using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    private int hp = 5;
    public Text hpText;

	// Use this for initialization
	void Start () {
        hpText = GetComponent<Text>(); 
	}
	
	// Update is called once per frame
	void Update () {
        hpText.text = "Health : " + hp;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hp--;
        }
	}
}
