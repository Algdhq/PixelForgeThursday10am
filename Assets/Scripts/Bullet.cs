using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("What Type of bullet is this?")]
    [SerializeField] private bool _harmEnemyBullet;

    [Header("BulletInfo")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _deathTimer;
    [SerializeField] private int _bulletDamage;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("KillMe", _deathTimer);
    }
    
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0, 5f * Time.deltaTime, 0);
        transform.Translate(Vector3.up * Time.deltaTime * _bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_harmEnemyBullet == true)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy01>().takeDamage(_bulletDamage);
                Destroy(this.gameObject);
            }
        }

        if (_harmEnemyBullet == false)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().TakeDamage(_bulletDamage);
                Destroy(this.gameObject);
            }
        }

    }

    void KillMe()
    {
        Destroy(this.gameObject);
    }
}
