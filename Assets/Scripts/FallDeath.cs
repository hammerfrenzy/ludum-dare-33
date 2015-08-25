using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class FallDeath : MonoBehaviour {

    public Transform currentCheckpoint;
    public Camera2DFollow _cameraFollow;
    public AudioClip Splat;

    Player _player;
    CharacterController2D _controller;

	// Use this for initialization
	void Start () {
        _player = GetComponent<Player>();
        _controller = GetComponent<CharacterController2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("SpawnAtCheckpoint"))
        {
            AudioSource.PlayClipAtPoint(Splat, transform.position);
            StartCoroutine(DelayedMoveToCheckpoint());
        }
    }

    public void MoveToCheckpoint()
    {
        transform.position = currentCheckpoint.position;
        _player.SetInControl(false);
        StartCoroutine(ReturnControlAfterTime());
        _controller.velocity = Vector2.zero;
        _cameraFollow.SnapToTarget();
    }

    IEnumerator DelayedMoveToCheckpoint()
    {
        float time = 0;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        MoveToCheckpoint();
    }

    IEnumerator ReturnControlAfterTime(float waitTime = .25f)
    {
        float time = 0;
        while (time < waitTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        _player.SetInControl(true);
    }
}
