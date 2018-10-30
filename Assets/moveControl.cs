using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveControl : MonoBehaviour {
    public float vol=0.02f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x - vol, gameObject.transform.position.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + vol, gameObject.transform.position.y);
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + vol);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - vol);
        }
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
