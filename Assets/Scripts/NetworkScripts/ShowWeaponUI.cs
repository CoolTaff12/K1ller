using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowWeaponUI : MonoBehaviour
{
    public GameObject[] weaponsShow;
    public bool isBook;
    public bool isCross;
    public bool isSword;
    public bool isStick;

    // Use this for initialization
    void Start ()
    {
        //Make sure that when the scene starts, you hide all the colors on GUI and make the bool false;
        weaponsShow[0].SetActive(false);
        weaponsShow[1].SetActive(false);
        weaponsShow[2].SetActive(false);
        weaponsShow[3].SetActive(false);
        isBook = false;
        isCross = false;
        isSword = false;
        isStick = false;
    }

    //If player hits a collider which has Trigger On.
    void OnTriggerEnter(Collider col)
    {
        //If the player hits a gameobject named the "Book"
        if (col.gameObject.name == "Book")
        {
            isBook = true;
            //Calls out the void called WeaponsPicked, to enable the image.
            WeaponsPicked();
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "Cross")
        {
            isCross = true;
            WeaponsPicked();
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "Sword")
        {
            isSword = true;
            WeaponsPicked();
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "Stick")
        {
            isStick = true;
            WeaponsPicked();
            Destroy(col.gameObject);
        }
    }

    void WeaponsPicked()
    {
        //When Book is picked
        if(isBook == true)
        {
            weaponsShow[0].SetActive(true);
        }
        //When Cross is picked
        if (isCross == true)
        {
            weaponsShow[1].SetActive(true);
        }
        //When Sword is picked
        if (isSword == true)
        {
            weaponsShow[2].SetActive(true);
        }
        //When Stick is picked
        if (isStick == true)
        {
            weaponsShow[3].SetActive(true);
        }
    }

}
