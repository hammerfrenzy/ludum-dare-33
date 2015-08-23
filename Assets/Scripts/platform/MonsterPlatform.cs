using UnityEngine;
using System.Collections;

public class MonsterPlatform : MonoBehaviour {

    SpriteRenderer sprite;
    bool isRevealed = false;

	// Use this for initialization
	void Start () {
	    sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reveal()
    {
        if (!isRevealed) StartCoroutine(RevealOverTime(.75f));
    }

    IEnumerator RevealOverTime(float revealTime)
    {
        Debug.Log("Starting Reveal");
        float time = 0;
        while (time < revealTime)
        {
            time += Time.deltaTime;
            float alpha = (time / revealTime) * .5f;
            sprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        sprite.color = new Color(1, 1, 1, .5f);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
