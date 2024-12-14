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
    private float jumpSpeed = 0;
    private float gravity = -9.81f;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private Camera camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private float xCameraBounds = 60f;
    [SerializeField] private int maxHealth;
    private int health;
    private Vector3 respawnPos = new Vector3(0,0,0);//You cannot die before reaching a proper checkpoint
    
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
        FindAnyObjectByType<RespawnSystem>().Respawn.AddListener(Respawn);
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
    }

    private void Movement()
    {
        //_moveVector = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;//initial way of showing movement
        _moveVector = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal"); //easier to explain after by using the forward and right vectors
        _moveVector.Normalize();
        

        if (jumpSpeed < 0 && _controller.isGrounded)
        {
            jumpSpeed = 0;

        }

        if (_controller.isGrounded && Input.GetButton("Jump"))
        {
            Debug.Log("jumping");
            jumpSpeed += (float)Math.Sqrt(jumpHeight * -2 * gravity);
        }

        jumpSpeed += gravity * Time.deltaTime * 1.25f;
        _controller.Move(new Vector3(_moveVector.x * speed, jumpSpeed , _moveVector.z *speed)*Time.deltaTime);
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


    public void killPlayer ()//This just kills the player
    {
        FindObjectOfType<RespawnSystem>().onDeath();
    }

    public void Respawn()//Moves the player back to the last touched checkpoint
    {
        _controller.enabled = false;
        gameObject.transform.position = respawnPos;
        _controller.enabled = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Checkpoint")//Checks if the thing the player touched is a checkpoint
        {
            respawnPos = collision.gameObject.transform.position;//Updates the position the player teleports to when respawning
        }
    }
    private void LateUpdate()
    {

        
    }
}
