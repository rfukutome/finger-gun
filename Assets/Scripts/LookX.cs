using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float mouseX = Input.GetAxis("Mouse X");

        // Rotation
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += mouseX;
        transform.localEulerAngles = newRotation;
    }
}
