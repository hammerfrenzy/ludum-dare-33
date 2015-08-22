using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController2D))]
[RequireComponent (typeof(Animator))]
public class Player : MonoBehaviour {

    public float gravity = -25f;

    public Light FlashlightLight;

    CharacterController2D _controller;
    Animator _animator;
    float _movespeed = 3;

	// Use this for initialization
	void Start () {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    void ProcessInput()
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
}
