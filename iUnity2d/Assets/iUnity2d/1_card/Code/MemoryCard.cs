using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour {
    //[SerializeField] private GameObject cardback;
    private GameObject cardback;
    [SerializeField] private SceneController controller;

    private int _id;
    public int id { get { return _id; } }

    public void setCard(int id , Sprite sprite)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

	// Use this for initialization
	void Start () {
        cardback = transform.Find("cardback").gameObject;   //find child
        GameObject.Find("car");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if (cardback.activeSelf&&controller.isCanReveal)
        {
            cardback.SetActive(false);
            controller.CardReveal(this);
        }
    }

    public void unReveal()
    {
        cardback.SetActive(true);
    }
}
