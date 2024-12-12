using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))] //this is a great thing to show people as it shows them how to make sure components will set up on objects.
public class FPSController : MonoBehaviour
{
    private float _xRotation;
    private Vector3 _moveVector;
    private CharacterController _controller;
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight;
    private float JumpSpeed = 0;
    private float gravity = -9.81f;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private Camera camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private float xCameraBounds = 60f;
    [SerializeField] private int maxHealth;
    private int health;
    
    #region Smoothing code
    private Vector2 _currentMouseDelta;
    private Vector2 _currentMouseVelocity;
    [SerializeField] private float smoothTime = .1f;
    
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
        Jumping();
    }

    private void Movement()
    {
        //_moveVector = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;//initial way of showing movement
        _moveVector = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal"); //easier to explain after by using the forward and right vectors
        _moveVector.Normalize();
        

        if (JumpSpeed < 0 && _controller.isGrounded)
        {
            JumpSpeed = 0;

        }

        if (_controller.isGrounded && Input.GetButton("Jump"))
        {
            Debug.Log("jumping");
            JumpSpeed += (float)Math.Sqrt(jumpHeight * -2 * gravity);
        }

        JumpSpeed += gravity * Time.deltaTime * 1.25f;
        _controller.Move(new Vector3(_moveVector.x * speed, JumpSpeed , _moveVector.z *speed)*Time.deltaTime);
    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        Vector2 targetDelta = new Vector2(mouseX, mouseY);
        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetDelta, ref _currentMouseVelocity, smoothTime);
        _xRotation -= _currentMouseDelta.y;
        _xRotation = Mathf.Clamp(_xRotation, -xCameraBounds, xCameraBounds);
        transform.Rotate(Vector3.up * _currentMouseDelta.x);
        camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }

    private void Jumping()
    {
        
    }

    public void takeDamage (int damage)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {

        }
    }
    private void LateUpdate()
    {

        
    }
}
