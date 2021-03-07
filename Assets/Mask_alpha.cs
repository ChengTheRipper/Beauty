using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mask_alpha : MonoBehaviour {

    // Use this for initialization
    public float alpha;
	void Start () {

        GetComponent<UnityEngine.UI.Image>().material.color = new Color(1, 1, 1, alpha);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
