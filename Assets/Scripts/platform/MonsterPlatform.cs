using UnityEngine;
using System.Collections;

public class MonsterPlatform : MonoBehaviour {

    public AudioClip RevealSound;
    public AudioClip HideSound;
    public float MaxAlpha = .5f;

    SpriteRenderer sprite;
    bool isRevealed = false;
    bool canBeRevealed = true;

	// Use this for initialization
	void Start () {
	    sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Hide()
    {
        if (isRevealed)
        {
            AudioSource.PlayClipAtPoint(HideSound, transform.position);
            StopAllCoroutines();
            isRevealed = false;
            sprite.color = new Color(1, 1, 1, 0);
            gameObject.layer = LayerMask.NameToLayer("Trigger");
        }
    }

    public void Reveal()
    {
        if (!isRevealed && canBeRevealed)
        {
            AudioSource.PlayClipAtPoint(RevealSound, transform.position, .75f);
            StartCoroutine(RevealOverTime(.75f));
        }
    }

    IEnumerator RevealOverTime(float revealTime)
    {
        isRevealed = true;
        gameObject.layer = LayerMask.NameToLayer("Default");
        float time = 0;
        while (time < revealTime)
        {
            time += Time.deltaTime;
            float alpha = (time / revealTime) * MaxAlpha;
            sprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        sprite.color = new Color(1, 1, 1, MaxAlpha);
    }

    public void SetCanBeRevealed(bool canReveal)
    {
        canBeRevealed = canReveal;
    }
}
