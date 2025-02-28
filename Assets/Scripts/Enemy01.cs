using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    private enum EnemyPattern
    {
        pattern01,
        pattern02,
        pattern03
    }

    [Header("Enemy Info")]
    [SerializeField] private int _health;
    [SerializeField] private EnemyPattern _enemyPattern;

    [Header("EnemyAssets")]
    [SerializeField] private Material[] _material;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _enemyModel;
    [SerializeField] private GameObject _enemyBullet;
    [SerializeField] private GameObject _weaponPosition;
    [SerializeField] private GameObject _bulletStorage;

    private bool _shootInLoop = true;
    private BoxCollider _collider;
    private int _enemyPatternNumber = 2;
    private Animator _anim;

    private Renderer _objectRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _bulletStorage = GameObject.Find("BulletStorage");
        _objectRenderer = transform.Find("SpaceFighter27").GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();
        _anim = GetComponent<Animator>();
        RunPatternNumber();
        //StartCoroutine("ShootBulletsLoop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RunPatternNumber()
    {
        switch (_enemyPattern)
        {
            case EnemyPattern.pattern01:
                _enemyPatternNumber = 0;
                break;
            case EnemyPattern.pattern02:
                _enemyPatternNumber = 1;
                break;
            case EnemyPattern.pattern03:
                _enemyPatternNumber = 2;
                break;
        }
        _anim.SetInteger("PatternNumber", _enemyPatternNumber);
        _anim.SetFloat("SpeedPlayback", 1.5f);
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
        GameObject Enemy = transform.parent.gameObject;
        Destroy(Enemy);
    }

    public void ShootWeapon()
    {
        GameObject bullet = Instantiate(_enemyBullet, _weaponPosition.transform.position, _weaponPosition.transform.rotation);
        bullet.transform.parent = _bulletStorage.transform;
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
