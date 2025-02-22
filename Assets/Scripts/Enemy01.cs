using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Material[] _material;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _enemyModel;
    [SerializeField] private GameObject _enemyBullet;
    [SerializeField] private GameObject _weaponPosition;
    [SerializeField] private GameObject _bulletStorage;

    private bool _shootInLoop = true;
    private BoxCollider _collider;

    private Renderer _objectRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _objectRenderer = transform.Find("SpaceFighter27").GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();
        StartCoroutine("ShootBulletsLoop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int value)
    {
        _health -= value;
        Debug.Log("My health is " + _health);
        StartCoroutine("DamageBlink");
        if (_health <= 0)
        {
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        _explosion.Play();
        _enemyModel.GetComponent<MeshRenderer>().enabled = false;
        _collider.enabled = false;
        Invoke("RemoveEnemyFromScene", 2f);
    }

    public void RemoveEnemyFromScene()
    {
        Destroy(this.gameObject);
    }

    IEnumerator ShootBulletsLoop()
    {
        while(_shootInLoop == true)
        {
            GameObject bullet = Instantiate(_enemyBullet, _weaponPosition.transform.position, _weaponPosition.transform.rotation);
            bullet.transform.parent = _bulletStorage.transform;
            yield return new WaitForSeconds(2.0f);
        }        
    }

    IEnumerator DamageBlink()
    {
        int count = 0;
        while(count < 3)
        {
            _objectRenderer.material = _material[1];
            count++;
            yield return new WaitForSeconds(0.02f);
            _objectRenderer.material = _material[0];
            yield return new WaitForSeconds(0.02f);
        }
    }
}
