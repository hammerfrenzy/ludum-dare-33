using UnityEngine;
using System.Collections;

public class Kid : MonoBehaviour {

    public Transform PatrolPointA;
    public Transform PatrolPointB;
    public float Movespeed = 1;
    public AudioClip Scream1;
    public AudioClip Scream2;
    public AudioClip Scream3;

    Vector2 target;
    bool moveLeft = true;
    bool patrol = true;

	// Use this for initialization
	void Start () {
        target = PatrolPointA.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!patrol) return;

        if (Vector2.Distance(target, transform.position) < 0.1f)
        {
            Flip();
        }

        if (moveLeft) target = PatrolPointA.position;
        else target = PatrolPointB.position;

        float direction = moveLeft ? -1 : 1;
        Vector2 moveVector = new Vector2(Movespeed * Time.deltaTime * direction, 0);
        transform.Translate(moveVector);
	}

    public void GetSpooked(Vector2 spookerPosition)
    {
        AudioSource.PlayClipAtPoint(RandomScreamClip(), transform.position);
        float direction = spookerPosition.x < transform.position.x ? 1 : -1;
        GetComponent<Collider2D>().isTrigger = true;
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.freezeRotation = false;
        body.velocity = new Vector2(3 * direction, 4);
        body.angularVelocity = Random.Range(-180f, 180f);
        Destroy(gameObject, 5f);
    }

    AudioClip RandomScreamClip()
    {
        int random = Random.Range(0, 3);
        if (random == 0) return Scream1;
        else if (random == 1) return Scream2;
        else return Scream3;
    }

    void Flip()
    {
        moveLeft = !moveLeft;
        if (moveLeft && transform.localScale.x < 0) FlipScale();
        else if (!moveLeft && transform.localScale.x > 0) FlipScale();
    }

    void FlipScale()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
