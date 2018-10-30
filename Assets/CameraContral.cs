using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContral : MonoBehaviour {
    public GameObject player;
    public float vol=0.1f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (player.gameObject.transform.position.y - this.transform.position.y >= 4.0f || player.gameObject.transform.position.y - this.transform.position.y <= -4.0f || player.gameObject.transform.position.x - this.transform.position.x >= 4.0f || player.gameObject.transform.position.x - this.transform.position.x <= -4.0f)
            roomMove();
        //Debug.Log(player.gameObject.transform.position.y - this.transform.position.y);
    }

    void roomMove()
    {
        if (player.gameObject.transform.position.y - this.transform.position.y >= 4.0f)
        { this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + vol, -4.5f); }
        if (player.gameObject.transform.position.y - this.transform.position.y <= -4.0f)
        { this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - vol, -4.5f); }
        if (player.gameObject.transform.position.x - this.transform.position.x >= 4.0f)
        { this.transform.position = new Vector3(this.transform.position.x + vol, this.transform.position.y, -4.5f); }
        if (player.gameObject.transform.position.x - this.transform.position.x <= -4.0f)
        { this.transform.position = new Vector3(this.transform.position.x - vol, this.transform.position.y, -4.5f); }
        
    }
}
