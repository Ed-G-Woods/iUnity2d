using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayer : MonoBehaviour {

    public float Jumpforce = 12f;
    public float speed = 250f;
    private Rigidbody2D _body;
    private GameObject _playerSprite;
    private Animator _playerAnimator;
    private BoxCollider2D _box;

    private MoveingPlatform mp = null;

	// Use this for initialization
	void Start () {
        _box = GetComponent<BoxCollider2D>();
        _body = GetComponent<Rigidbody2D>();
        _playerSprite = transform.Find("Playersprite").gameObject;
        _playerAnimator = _playerSprite.GetComponent<Animator>(); 
	}
	
	// Update is called once per frame
	void Update () {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(min.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(max.x, min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        Vector3 pscale =Vector3.one;
        bool grounded = false;
        if (hit!=null)
        {
            grounded = true;

            mp = hit.GetComponent<MoveingPlatform>();
            if (mp)
            {
                transform.parent = mp.transform;
                pscale = mp.transform.localScale;
            }
            else
            {
                transform.parent = null;
                pscale = Vector3.one;
            }
        }



        if (Input.GetKeyDown(KeyCode.UpArrow)&& grounded)
        {
            _body.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
        }

        _playerAnimator.SetFloat("Speed", Mathf.Abs(deltaX));
        if (/*!Mathf.Approximately(deltaX,0)*/deltaX!=0)
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX)/ pscale.x, 1/ pscale.y, 1);
        }
        //transform.localScale = new Vector3(Mathf.Sign(deltaX) / pscale.x, 1 / pscale.y, 1/ pscale.z);

    }
}
