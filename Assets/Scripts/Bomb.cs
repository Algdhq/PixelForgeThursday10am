using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _bombTimer;
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private MeshRenderer _bombModel;

    private bool _stopMoving;
    private SphereCollider _sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        StartCoroutine("BombTimer");
    }

    // Update is called once per frame
    void Update()
    {
        if (_stopMoving != true)
        {
            transform.Translate(Vector3.up * Time.deltaTime * _speed);
        }        
    }

    public void CancelBomb()
    {
        StopCoroutine("BombTimer");
        Debug.Log("Bomb is cancelled");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ExplodeBomb();
            _sphereCollider.radius = 5;
            other.GetComponent<Enemy01>().takeDamage(50);
            StopCoroutine("BombTimer");
        }
    }

    public void ExplodeBomb()
    {
        _explosion.Play();
        _stopMoving = true;
        _bombModel.enabled = false;
    }

    IEnumerator BombTimer()
    {
        Debug.Log("Bomb Started");
        yield return new WaitForSeconds(_bombTimer);
        Debug.Log("Bomb Exploded");
        ExplodeBomb();
    }
}
