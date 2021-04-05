using System;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector3.left);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector3.right);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private void Move(Vector3 direction)
    {
        _rigidbody.velocity = direction * _speed;
    }
}
