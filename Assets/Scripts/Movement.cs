using System;
using UnityEngine;


public class Movement : MonoBehaviour
{
    //cached
    private Rigidbody _rocketRb;

    //config
    [SerializeField] private float rocketThrust = 1000f;
    [SerializeField] private float rotatePerSecond = 90f;
    [SerializeField] private ParticleSystem _particalUp;
    [SerializeField] private ParticleSystem _particalLeft;
    [SerializeField] private ParticleSystem _particalRight;
    private AudioSource _soundOfRocket;
    private void Start()
    {
        _rocketRb = GetComponent<Rigidbody>();
        _soundOfRocket= GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    private void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotatePerSecond);
            if (!_particalRight.isPlaying)
            {
                _particalRight.Play();
            }
        }
            
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotatePerSecond); 
            if (!_particalLeft.isPlaying)
            {
                _particalLeft.Play();
            }
        }
        else
        {
            _particalRight.Stop();
            _particalLeft.Stop();
        }
            
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rocketRb.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
            if (!_particalUp.isPlaying)
            {
                _particalUp.Play();
                _soundOfRocket.Play(); 
            }
        }
        else
        {
            _particalUp.Stop();
            _soundOfRocket.Pause();
        }
            
    }

    private void ApplyRotation(float rotatePerSecond)
    {
        _rocketRb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotatePerSecond * Time.deltaTime);
        _rocketRb.freezeRotation = false;
    }
}
