using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHit : MonoBehaviour {
    private int hp = 50;
    public animEvent tempEvent;
    public Text HP;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Destroy();
    }

    void Destroy()
    {
        if (hp <= 0) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "hitTrigger")
        {
            other.gameObject.SetActive(false);
            hp-= tempEvent.tempWeapon.energy/2;
            Debug.Log("伤害"+tempEvent.tempWeapon.energy);
            HP.text = "敌人血量："+hp;
        }
    }
}
