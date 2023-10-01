using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _screenBorder;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Camera _camera;
    private Animator _animator;

    public KeyManager km;

    [SerializeField]
    private AudioClip _leftFootstepSound; // Left footstep sound clip
    [SerializeField]
    private AudioClip _rightFootstepSound; // Right footstep sound clip
    private AudioSource _footstepAudioSource;

    public AudioClip keyCollectSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;      //get the main camera
        _animator = GetComponent<Animator>();

        // Initialize footstep audio source
        _footstepAudioSource = gameObject.AddComponent<AudioSource>();
        _footstepAudioSource.spatialBlend = 1f; // 3D audio
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
        SetAnimation();
    }

    private void SetAnimation()
    {
        bool isMoving = _movementInput != Vector2.zero;
        _animator.SetBool("IsMoving", isMoving);
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
                    _smoothedMovementInput,
                    _movementInput,
                    ref _movementInputSmoothVelocity,
                    0.1f);

        _rigidbody.velocity = _smoothedMovementInput * _speed;

        // Play footstep sounds if moving
        if (_rigidbody.velocity.magnitude > 0.1f && !_footstepAudioSource.isPlaying)
        {
            PlayRandomFootstepSound();
        }

        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position); //get the screen position of the player and convert it to screen cordinate

        if ((screenPosition.x < _screenBorder && _rigidbody.velocity.x < 0) ||
            (screenPosition.x > _camera.pixelWidth - _screenBorder && _rigidbody.velocity.x > 0))
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }

        if ((screenPosition.y < _screenBorder && _rigidbody.velocity.y < 0) ||
            (screenPosition.y > _camera.pixelHeight - _screenBorder && _rigidbody.velocity.y > 0))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        }
    }

    private void RotateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rigidbody.MoveRotation(rotation);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            // Play the key collection sound
            if (keyCollectSound != null && _audioSource != null)
            {
                _audioSource.PlayOneShot(keyCollectSound);
            }
            // Debug.Log("Key Collected");
            Destroy(other.gameObject);
            km.keyCount++;

        }
    }

    private void PlayRandomFootstepSound()
    {
        AudioClip footstepSound = Random.Range(0f, 1f) < 0.5f ? _leftFootstepSound : _rightFootstepSound;
        _footstepAudioSource.PlayOneShot(footstepSound);
    }
}