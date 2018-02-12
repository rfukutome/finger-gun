using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private Text _ammoText;
    [SerializeField] private Text _coinText;
    [SerializeField] private Image _coinImage;

    public void UpdateAmmo(int argCount)
    {
        _ammoText.text = "AMMO : " + argCount;
    }

    public void SetCoinAmount(int value)
    {
        if (value > 0)
        {
            _coinText.text = "x" + value;
            _coinImage.enabled = true;
        }
        else
        {
            _coinImage.enabled = false;
            _coinText.text = "";
        }
    }
}
