using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
    void OnTriggerEnter( Collider other)
    {
        int a;

        if (other.gameObject.tag == "buildtag")
        {
            a = 3;
        }
        else if (other.gameObject.tag == "stagetag")
        {
            a = 3;
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
