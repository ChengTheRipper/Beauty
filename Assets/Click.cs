using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{

    GameManager gameManager;
    Object obj;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnClikeButton()
    {


        if (gameManager.Changes == 1)
        {
             obj = GameObject.Find("BackGround");
            
            //SpriteRenderer sprite1 = obj.GetComponentInChildren<SpriteRenderer>();
            //sprite1.sprite = Resources.Load<Sprite>("BackGround/bg5");
        }
    }
}
