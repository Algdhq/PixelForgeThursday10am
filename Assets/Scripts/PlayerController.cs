using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed01;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
               
    }

    public void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 currentPosition = transform.position;
        transform.Translate(new Vector3(horizontal, vertical, 0)
            * Time.deltaTime * _speed01);

        //Debug.Log(currentPosition);

        if (currentPosition.x > 9.5f)
        {
            currentPosition.x = 9.5f;
            transform.position = currentPosition;
        }
        if (currentPosition.x < -9.5f)
        {
            currentPosition.x = -9.5f;
            transform.position = currentPosition;
        }
        if (currentPosition.y > 3)
        {
            currentPosition.y = 3;
            transform.position = currentPosition;
        }
        if (currentPosition.y < -5)
        {
            currentPosition.y = -5;
            transform.position = currentPosition;
        }
    }
}
