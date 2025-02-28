using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    [SerializeField] private Transform _thingToLookAt;

    // Start is called before the first frame update
    void Start()
    {
        _thingToLookAt = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_thingToLookAt);
    }
}
