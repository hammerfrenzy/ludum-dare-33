using UnityEngine;
using System.Collections;

public class FallDeath : MonoBehaviour {

    public Transform currentCheckpoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) 
    {
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
    }
}
