using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private Transform _gunOffset;

    [SerializeField]
    private float _timeBetweenShots; //delay

    private bool _fireContinuously;
    private bool _fireSingle;
    private float _lastFireTime;

    //for the audio
    [SerializeField]
    public float ClipLength = 1f;
     [SerializeField]
    private AudioSource _audioSource; // Reference to the AudioSource component


    private void Start()
    {   
         _audioSource.Stop(); // Make sure the audio source is stopped at the start
        _lastFireTime = -_timeBetweenShots; //set to negative so that the player can fire immediately
    }

    void Update()
    {
        if (_fireContinuously || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= _timeBetweenShots) //if grate, then can pass bullet
            {
                FireBullet();

                _lastFireTime = Time.time; //set to current game time
                _fireSingle = false;
                // Play the bullet sound
                StartCoroutine(PlayBulletSound());
            }
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation); //create a new bullet
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        
        rigidbody.velocity = _bulletSpeed * transform.up;
    }

    //for the bulletSound()
    private IEnumerator PlayBulletSound()
    {
        _audioSource.Play(); // Play the firing sound
        yield return new WaitForSeconds(_timeBetweenShots); // Wait for the specified delay
        _audioSource.Stop(); // Stop the firing sound
    }

    private void OnFire(InputValue inputValue)
    {
        _fireContinuously = inputValue.isPressed;

        if (inputValue.isPressed)
        {
            _fireSingle = true;
        }
    }
}
