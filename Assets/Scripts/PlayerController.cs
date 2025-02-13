using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player stuff")]
    [SerializeField] private float _speed01;
    [SerializeField] private GameObject _weaponPosition;
    [Header("Weapon stuff")]
    [SerializeField] private GameObject _bulletPreFab;
    [SerializeField] private GameObject _bulletStorage;
    [SerializeField] private float _cooldownTimer;
    private bool _canFire = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        FireWeapon();        
    }

    private void FireWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canFire == true)
            {
                GameObject bullet = Instantiate(_bulletPreFab, _weaponPosition.transform.position, Quaternion.identity);
                bullet.transform.parent = _bulletStorage.transform;
                _canFire = false;
                StartCoroutine("CooldownTimer");
            }
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

    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(_cooldownTimer);
        _canFire = true;
    }
}
