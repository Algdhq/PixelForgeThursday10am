using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _deathTimer;


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



    void KillMe()
    {
        Destroy(this.gameObject);
    }
}
