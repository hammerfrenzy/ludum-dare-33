using UnityEngine;
using System.Collections;

public class FallDeath : MonoBehaviour {

    public Transform currentCheckpoint;

    CharacterController2D _controller;

	// Use this for initialization
	void Start () {
        _controller = GetComponent<CharacterController2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("SpawnAtCheckpoint"))
            StartCoroutine(MoveToCheckpoint(gameObject));
    }

    IEnumerator MoveToCheckpoint(GameObject gameObject)
    {
        float time = 0;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.position = currentCheckpoint.position;
        _controller.rigidBody2D.velocity = Vector2.zero;
    }
}
