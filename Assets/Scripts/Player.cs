using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float _speed;
    private CharacterController _controller;
    private float _gravity = 9.81f;

	// Use this for initialization
	void Start () {
        _controller = GetComponent<CharacterController>();	
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Movement
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction.y -= _gravity;

        direction = transform.transform.TransformDirection(direction);
        _controller.Move(_speed*direction*Time.deltaTime);
	}
}
