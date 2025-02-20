using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Material[] _material;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _enemyModel;

    private Renderer _objectRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _objectRenderer = transform.Find("SpaceFighter27").GetComponent<Renderer>();        
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
        //put damage incoming as a value
        //subtract health
        //tell me how much health is left
        //do damage stuff so I know I got damaged
    }

    public void DestroyEnemy()
    {
        _explosion.Play();
        _enemyModel.GetComponent<MeshRenderer>().enabled = false;
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
