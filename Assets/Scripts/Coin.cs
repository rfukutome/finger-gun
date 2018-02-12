using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {


    [SerializeField]private int value;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (Input.GetButtonDown("Action"))
            {
                player.AddCoin(value);
                Destroy(this.gameObject);
            }
        }
    }


}
