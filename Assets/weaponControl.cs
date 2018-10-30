using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponControl : MonoBehaviour {
    public float ran;
    public float ad_x;
    public float ad_y;
    public float x;
    public float y;
    
    public GameObject player;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.transform.position = new Vector2(player.transform.position.x + (ran + ad_y + y), player.transform.position.y );
            gameObject.transform.rotation = new Quaternion(0.0f,0.0f,0.7f,0.7f);
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameObject.transform.position = new Vector2(player.transform.position.x - (ran - ad_y + y) , player.transform.position.y );
            gameObject.transform.rotation = new Quaternion(0.0f, 0.0f, 0.7f, -0.7f);
            //Debug.Log(this.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            gameObject.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + (ran + ad_x+x));
            gameObject.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - (ran - ad_x+x));
            gameObject.transform.rotation = new Quaternion(0.0f, 0.0f, 1.0f, 0.0f);
        }
        //攻击动画播放
        
    }
}
