using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButtom : MonoBehaviour {
    [SerializeField] private GameObject TargetObject;
    [SerializeField] private string TargetMessage;

    private SpriteRenderer spriteRender;
    private bool isMouseOver;

	// Use this for initialization
	void Start () {
        spriteRender = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        spriteRender.color = Color.yellow;
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        spriteRender.color = Color.white;
        isMouseOver = false;
    }

    private void OnMouseDown()
    {
        spriteRender.color = Color.red;
    }

    private void OnMouseUp()
    {
        if (isMouseOver)
        {
            spriteRender.color = Color.yellow;
        }
        else
        {
            spriteRender.color = Color.white;
        }

        if (TargetObject!=null)
        {
            TargetObject.SendMessage(TargetMessage);
        }
       
    }
}
