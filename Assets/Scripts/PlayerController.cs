using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Health Info")]
    [SerializeField] private int _health;
    [SerializeField] private Material[] _material;
    [Header("Player stuff")]
    [SerializeField] private float _speed01;
    [SerializeField] private GameObject _weaponPosition;
    [Header("Weapon stuff")]
    [SerializeField] private GameObject _bulletPreFab;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private GameObject _bulletStorage;
    [SerializeField] private float _cooldownTimer;
    [Header("Death stuff")]
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _playerModel;

    private bool _canFire = true;
    private Renderer _objectRenderer;
    private BoxCollider _collider;
    private bool _isDead;

    

    // Start is called before the first frame update
    void Start()
    {
        _objectRenderer = transform.Find("Space_Fighter_06").GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDead == false)
        {
            Movement();
            FireWeapon();
            LaunchBomb();
        }        
    }

    private void FireWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canFire == true)
            {
                GameObject bullet = Instantiate(_bulletPreFab, _weaponPosition.transform.position, _weaponPosition.transform.rotation);
                bullet.transform.parent = _bulletStorage.transform;
                _canFire = false;
                StartCoroutine("CooldownTimer");
            }
        }
    }

    private void LaunchBomb()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject bomb = Instantiate(_bomb, _weaponPosition.transform.position, Quaternion.identity);
            bomb.transform.parent = _bulletStorage.transform;
        }
    }

    public void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 currentPosition = transform.position;
        transform.Translate(new Vector3(horizontal, vertical, 0)
            * Time.deltaTime * _speed01);

        float clampX = Mathf.Clamp(transform.position.x, -9.5f, 9.5f);
        float clampY = Mathf.Clamp(transform.position.y, -5, 3);

        transform.position = new Vector3(clampX, clampY, 0);

    }
    
    public void TakeDamage(int value)
    {
        Debug.Log("take damage value is " + value);
        _health -= value;
        StartCoroutine("DamageBlink");

        if (_health <= 0)
        {
            DestroyPlayer();
        }
    }

    public void DestroyPlayer()
    {
        _explosion.Play();
        _playerModel.GetComponent<MeshRenderer>().enabled = false;
        _collider.enabled = false;
        _isDead = true;
    }

    //revive player

    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(_cooldownTimer);
        _canFire = true;
    }

    IEnumerator DamageBlink()
    {
        int count = 0;
        while (count < 3)
        {
            _objectRenderer.material = _material[1];
            count++;
            yield return new WaitForSeconds(0.02f);
            _objectRenderer.material = _material[0];
            yield return new WaitForSeconds(0.02f);
        }
    }
}
