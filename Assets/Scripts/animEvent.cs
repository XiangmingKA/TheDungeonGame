using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 enum Weapon
{
    Sword,Knife,Spare,Axe
}

public struct weapon
{
    public int energy;
    public string type;
}

public class animEvent : MonoBehaviour { 
    public weapon tempWeapon;

    public Text weaponUI;

    public int count1 = 0;
    public int count2 = 0;

    public string weaponStr1 = "IsSword1";
    public string weaponStr2 = "IsSword2";
    Weapon weapon = Weapon.Sword;

    public GameObject trigger1;
    public GameObject trigger2;

    public GameObject[] tri=new GameObject[7];

    public bool IsWeapon1 = false;
    public bool IsWeapon2 = false;

    public Animator anim;

    public int IsWeapon1Id = -1;
    public int IsWeapon2Id = -1;

    AnimatorOverrideController overrideController;


    // Use this for initialization
    public void change()
    {
        switch (weapon)
        {
            case Weapon.Sword:
                weaponStr1 = "IsSword1";
                weaponStr2 = "IsSword2";
                break;
            case Weapon.Knife:
                weaponStr1 = "IsKnife1";
                weaponStr2 = "IsKnife2";
                break;
            case Weapon.Axe:
                weaponStr1 = "IsAxe1";
                weaponStr2 = "IsAxe2";
                break;
            case Weapon.Spare:
                weaponStr1 = "IsSpare1";
                weaponStr2 = "IsSpare2";
                break;
        }
        IsWeapon1Id = Animator.StringToHash(weaponStr1);
        IsWeapon2Id = Animator.StringToHash(weaponStr2);
    }

    public void Sele()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            weapon = Weapon.Sword;
            //tempWeapon.setWeapon(4, "Sword");
            tempWeapon.energy = 4;
            tempWeapon.type = "Sword";
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            weapon = Weapon.Knife;
            //tempWeapon.setWeapon(3, "Knife");
            tempWeapon.energy = 4;
            tempWeapon.type = "Knife";
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            weapon = Weapon.Axe;
            //tempWeapon.setWeapon(5, "Axe");
            tempWeapon.energy = 6;
            tempWeapon.type = "Axe";
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            weapon = Weapon.Spare;
            //tempWeapon.setWeapon(3, "Spare");
            tempWeapon.energy = 4;
            tempWeapon.type = "Spare";
        }
        change();
        hurtSele();
    }

    public void hurtSele()
    {
        switch(weaponStr1)
        {
            case "IsSword1": trigger1 = tri[0]; trigger2 = tri[1]; break;
            case "IsKnife1": trigger1 = tri[2]; trigger2 = tri[3]; break;
            case "IsAxe1": trigger1 = tri[4]; trigger2 = tri[4]; break;
            case "IsSpare1": trigger1 = tri[5]; trigger2 = tri[6]; break;
        }

    }

    public void UIupdate()
    {
        weaponUI.text = "武器："+tempWeapon.type+"\n武器威力：" +tempWeapon.energy;
    }

    public void creatAre1()
    {
        trigger1.gameObject.SetActive(true);
    }

    public void creatAre2()
    {
        trigger2.gameObject.SetActive(true);
    }

    public void desAre1()
    {
        trigger1.gameObject.SetActive(false);
    }

    public void desAre2()
    {
        trigger2.gameObject.SetActive(false);
    }

    void Awake () {
        IsWeapon1Id = Animator.StringToHash(weaponStr1);
        IsWeapon2Id = Animator.StringToHash(weaponStr2);

        overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
    }

    private void Start()
    {
        //tempWeapon.setWeapon(3, "sword");
        tempWeapon.energy = 4;
        tempWeapon.type = "Sword";
        UIupdate();
        //Debug.Log("开始"+tempWeapon.energy);
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy" )
        {
            count1++;
            if (count1 >= 5)
            {
                tempWeapon.energy -= 2;count1 = 0;
                UIupdate();
                Debug.Log("武器变强"+2);
            }    
        }
        if(other.gameObject.tag == "enemy" )
        {
            count2++;
            if (count2 >= 3)
            {
                tempWeapon.energy += 2; count2 = 0;
                UIupdate();
                Debug.Log("武器变弱"+2);
            }
        }
    }*/

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.J))
        {
            IsWeapon1 = true;
        }
        else
        {
            IsWeapon1 = false;
        }

        if (Input.GetKey(KeyCode.K))
        {
            IsWeapon2 = true;
        }
        else
        {
            IsWeapon2 = false;
        }

        anim.SetBool(IsWeapon1Id, IsWeapon1);
        anim.SetBool(IsWeapon2Id, IsWeapon2);
        Sele();
        UIupdate();
        //Debug.Log("中途" + tempWeapon.energy);
    }
}
