using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController2D))]
[RequireComponent (typeof(Animator))]
public class Player : MonoBehaviour {

    public float gravity = -25f;

    public Light FlashlightLight;

    Flashlight _flashlight;
    CharacterController2D _controller;
    Animator _animator;
    float _movespeed = 3;

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
        if (Input.GetKey(KeyCode.F)) _flashlight.PlaceBehind();
        else if (Input.GetKeyUp(KeyCode.F)) _flashlight.Reset();
    }

    void ProcessMovementInput()
    {
        float input = Input.GetAxis("Horizontal");
        float movement = input * Time.deltaTime * _movespeed;
        _controller.move(new Vector3(movement, gravity * Time.deltaTime, 0));

        if (movement > 0 && transform.localScale.x < 0f)
            FlipScale();
        else if (movement < 0 && transform.localScale.x > 0f)
            FlipScale();

        _animator.SetFloat("speed", Mathf.Abs(input));
    }

    void FlipScale()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        float yRotation = transform.localScale.x == -1 ? 270 : 90;
        FlashlightLight.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                other.GetComponent<RevealPlatform>().Reveal();
            }
        }
    }
}
