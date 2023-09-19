using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] //to edit that using unity
    private float _speed = 3f;  //for the speed

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField] 
    private float _screenBorder;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput; //to make the movement smooth
    private Vector2 _movementInputSmoothVelocity; //to know the direction of the player

    private Camera _camera;

    private void Awake()
    { //when the scene first initalised
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main; //get the main camera

    }

    private void FixedUpdate()
    { //frequency of the phy6 system

        SetPlayerVelocity();
        // RotationInDirectionOfInput();
    }

    private void SetPlayerVelocity()
    {
        //(current value, target value, ref current velocity, smooth time)
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            0.15f);

        _rigidbody.velocity = _smoothedMovementInput * _speed;

        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        //use camera to determiine if the player is still in the scene
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position); //transform give the world position, we need to converte it in to cam pos

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



    private void RotationInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        { //when the player is not moving
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput); //transform.forward is the z axis to keep it in the current valaue
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation,
                                                            targetRotation,
                                                            _rotationSpeed * Time.deltaTime); //to rotate the player  
                                                                                              //Time.deltaTime is the time between each frame  
            _rigidbody.MoveRotation(rotation); //apply rotaion
        }
    }

    private void OnMove(InputValue movementValue)
    {
        _movementInput = movementValue.Get<Vector2>();
    }
}
