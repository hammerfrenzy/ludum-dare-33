using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController2D))]
[RequireComponent (typeof(Animator))]
public class Player : MonoBehaviour {

    public float gravity = -5f;
    public float jumpHeight = 3;

    public Light FlashlightLight;
    public Animator _shadowAnimator;

    Flashlight _flashlight;
    CharacterController2D _controller;
    Animator _animator;
    float _movespeed = 3;
    float _damping = 20f;
    bool canJump = false;

    void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();
        _flashlight = GetComponentInChildren<Flashlight>();
    }

	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        ProcessMovementInput();
        ProcessActionInput();
	}

    void ProcessActionInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _flashlight.PlaceBelow();
            if (canJump) _shadowAnimator.Play("GrabCliff");
            else _shadowAnimator.Play("exposed");
        }
        else if (Input.GetKey(KeyCode.C))
        {
            _flashlight.PlaceBelow();
        }
        else if (Input.GetKey(KeyCode.F))
        {
            _flashlight.PlaceBehind();
        }
        else
        {
            _flashlight.Reset();
        }
    }

    void ProcessMovementInput()
    {
        Vector3 velocity = _controller.velocity;

        if (_controller.isGrounded) velocity.y = 0;

        float normalizedHorizontalSpeed = 0;
        if (GoRight())
        {
            normalizedHorizontalSpeed = 1;
            if (transform.localScale.x < 0) FlipScale();
        }
        else if (GoLeft())
        {
            normalizedHorizontalSpeed = -1;
            if (transform.localScale.x > 0) FlipScale();
        }

        if (canJump && Input.GetKeyDown(KeyCode.C))
        {
            velocity.y = Mathf.Sqrt(2 * jumpHeight * -gravity);
        }
 
        velocity.x = Mathf.Lerp(velocity.x, normalizedHorizontalSpeed * _movespeed, Time.deltaTime * _damping);
        velocity.y += gravity * Time.deltaTime;

        _controller.move(velocity * Time.deltaTime);
        _animator.SetFloat("speed", Mathf.Abs(velocity.x));
    }

    bool GoLeft()
    {
        return (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
    }

    bool GoRight()
    {
        return (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
    }

    void FlipScale()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jump"))
        {
            canJump = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jump"))
        {
            canJump = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                other.GetComponent<RevealPlatform>().Reveal();
            }
        }
    }
}
