using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] CardSprite;


    public  int gridRows = 2;
    public  int gridCols = 4;
    public  float offsetX = 2f;
    public  float offsetY = 2.5f;


    // Use this for initialization
    void Start () {

        Vector3 startPos = originalCard.transform.position;

        for (int y =0;y<gridCols;y++)
        {
            for (int x=0;x<gridRows;x++)
            {
                MemoryCard card;
                if (y==0&&x==0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int id = Random.Range(0, CardSprite.Length);
                card.setCard(id, CardSprite[id]);

                float posX = x * offsetX + startPos.x;
                float posY = y * offsetY + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
