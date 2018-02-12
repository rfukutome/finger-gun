using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] AudioSource _winAudio;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (Input.GetButtonDown("Action"))
            {
                if (player.subtractCoins(1))
                {
                    _winAudio.Play();
                }
                else
                {
                    Debug.Log("Not enough coins");
                }
            }
        }
    }
}
