using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour {

	// Use this for initialization
	private void Start () {
        Debug.Log("This is the start console");
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("This is the update console");

    }
    private void OnMouseDown()
    {
        Debug.Log(this.gameObject + " was clicked, only world position returned is " + this.gameObject.transform.position.ToString());
        
    }
}
