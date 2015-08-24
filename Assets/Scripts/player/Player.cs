using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController2D))]
[RequireComponent (typeof(Animator))]
public class Player : MonoBehaviour {

    public float gravity = -5f;
    public float jumpHeight = 3;

    public Light FlashlightLight;
    public Animator _shadowAnimator;
    public GUIText CelebrationText;

    Flashlight _flashlight;
    CharacterController2D _controller;
    Animator _animator;
    FallDeath _respawner;
    float _movespeed = 3;
    float _damping = 20f;
    bool canJump = false;
    bool canControl = true;
    bool SUPERCHARGED = false;

    void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();
        _respawner = GetComponent<FallDeath>();
        _flashlight = GetComponentInChildren<Flashlight>();
    }

	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        if (canControl) {
            ProcessMovementInput();
            ProcessActionInput();
        }
        else {
            _controller.move(_controller.velocity * Time.deltaTime);
        }
	}

    void ProcessActionInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _flashlight.PlaceBelow();
            if (SUPERCHARGED)
            {
                if (canJump) _shadowAnimator.Play("GrabCliff");
                else _shadowAnimator.Play("exposed");
            }
            else
            {
                if (canJump) _shadowAnimator.Play("GrabCliff");
                else _shadowAnimator.Play("exposed");
            }
        }
        else if (Input.GetKey(KeyCode.J))
        {
            _flashlight.PlaceBelow();
        }
        else if (Input.GetKey(KeyCode.K))
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

        if (canJump && Input.GetKeyDown(KeyCode.J))
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

        else if (other.gameObject.CompareTag("KidZone"))
        {
            float direction = other.gameObject.transform.position.x < transform.position.x ? 1 : -1;
            canControl = false;
            _controller.velocity = new Vector2(3 * direction, 2);
            StartCoroutine(DelayedRegainControl());
        }

        else if (other.gameObject.CompareTag("Platform"))
        {
            CheckPlatform(other);
        }

        else if (other.gameObject.CompareTag("Battery"))
        {
            other.gameObject.GetComponent<Battery>().PickedUp();
            jumpHeight = 6;
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
        if (!canControl) return;

        if (other.gameObject.CompareTag("Platform"))
        {
            CheckPlatform(other);
        }

        else if (other.gameObject.CompareTag("SpookZone"))
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Kid kid = other.GetComponentInParent<Kid>();
                kid.GetSpooked(transform.position);
            }
        }

        else if (other.gameObject.CompareTag("WinZone"))
        {
            _controller.velocity = Vector2.zero;
            canControl = false;
            CelebrationText.text = "CONGRATULATIONS! \nYOU DID IT!!!";
            CelebrationText.enabled = true;
        }
    }

    void CheckPlatform(Collider2D other)
    {
        if (Input.GetKey(KeyCode.K))
        {
            other.GetComponent<RevealPlatform>().Reveal();
        }
    }

    public void SetInControl(bool inControl)
    {
        canControl = inControl;
    }

    IEnumerator DelayedRegainControl(float controlTime = .5f)
    {
        float time = 0;
        while (time < controlTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        canControl = true;
    }

    IEnumerator DelayedRespawn(float respawnTime = .75f)
    {
        float time = 0;
        while (time < respawnTime)
        {
            time += Time.deltaTime;
            if (!canControl && time > respawnTime / 2)
            {
                canControl = true;
            }

            yield return null;
        }

        _respawner.MoveToCheckpoint();
    }
}
