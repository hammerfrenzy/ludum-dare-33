using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour {

    public float WobbleDistance = .2f;
    public float Speed = 20;
    public GUIText CelebrationText;

    Vector3 startPosition;
    float angle = 0;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        angle += Speed * Time.deltaTime;
        if (angle > 360) angle -= 360;

        transform.position = startPosition + new Vector3(0, WobbleDistance * (Mathf.Sin(angle)));
	}

    public void PickedUp()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        CelebrationText.text = "SUPERCHARGED!!!!";
        CelebrationText.enabled = true;
        CelebrationText.gameObject.GetComponent<RainbowText>().TextTriggered();
        Destroy(gameObject);
    }
}
