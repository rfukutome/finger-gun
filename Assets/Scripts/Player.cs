using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private AudioClip _coinClip;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _hitMarker;
    [SerializeField] private AudioSource _gunSound;
    [SerializeField] private GameObject _weapon;

    private UIManager _uiManager;
    private int _coinCount;
    private CharacterController _controller;
    private float _gravity = 9.81f;
    [SerializeField] private bool _reloading;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;
    private bool _hasGun;
	// Use this for initialization
	void Start () {
        currentAmmo = maxAmmo;
        _coinCount = 0;
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.FindObjectOfType<UIManager>();
        _uiManager.UpdateAmmo(currentAmmo);
        _uiManager.SetCoinAmount(_coinCount);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _reloading = false;
        _hasGun = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
        else
        {
            _muzzleFlash.SetActive(false);
            _gunSound.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.R) && !_reloading)
        {
            StartCoroutine(Reload());
            _reloading = true;
        }
        Movement();
	}

    void Shoot()
    {
        if (_hasGun)
        {
            currentAmmo--;
            _uiManager.UpdateAmmo(currentAmmo);
            if (!_gunSound.isPlaying)
            {
                _gunSound.Play();
            }
            _muzzleFlash.SetActive(true);
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                GameObject hitMarker = Instantiate(_hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitMarker, 0.3f);

                Destructable crate = hitInfo.transform.gameObject.GetComponent<Destructable>();
                if(crate!= null)
                {
                    crate.DestroyCrate();
                }
            }
        }
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Movement
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction.y -= _gravity;

        direction = transform.transform.TransformDirection(direction);
        _controller.Move(_speed * direction * Time.deltaTime);
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        _reloading = false;
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
    }

    public void AddCoin(int value)
    {
        AudioSource.PlayClipAtPoint(_coinClip, this.transform.position);
        _coinCount += value;
        _uiManager.SetCoinAmount(_coinCount);
    }
    
    public bool subtractCoins(int amount)
    {
        if(amount > _coinCount)
        {
            return false;
        }
        else
        {
            _coinCount -= amount;
            _weapon.SetActive(true);
            _hasGun = true;
            _uiManager.SetCoinAmount(_coinCount);
            return true;
        }
    }
}
