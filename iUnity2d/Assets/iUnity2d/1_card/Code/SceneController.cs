using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] CardSprite;
    [SerializeField] private TextMesh ScoreText;

    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;

    public  int gridRows = 2;
    public  int gridCols = 4;
    public  float offsetX = 2f;
    public  float offsetY = 2.5f;

    private int score;

    public bool isCanReveal { get { return secondRevealed == null; } }

    public void CardReveal(MemoryCard card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(MatchResult());
        }

    }

    private IEnumerator MatchResult()
    {
        if (firstRevealed.id==secondRevealed.id)
        {
            score++;
            ScoreText.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            firstRevealed.unReveal();
            secondRevealed.unReveal();
        }
        firstRevealed = null;
        secondRevealed = null;
    }
    

    private void ShuffleArray(ref int[] origin)
    {
        int temp;
        for (int i =0;i<origin.Length;i++)
        {
            temp = origin[i];
            int index = Random.Range(i, origin.Length);
            origin[i] = origin[index];
            origin[index] = temp;
        }
    }

    // Use this for initialization
    void Start () {

        Vector3 startPos = originalCard.transform.position;

        int[] ids = { 0,0,1,1,2,2,3,3};
        ShuffleArray(ref ids);

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

                int number = y * gridRows + x;
                int id = ids[number];
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

    public void Restart()
    {
        SceneManager.LoadScene("CardMain");
    }
}
