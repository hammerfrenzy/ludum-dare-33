using UnityEngine;
using System.Collections;

public class RevealPlatform : MonoBehaviour {

    MonsterPlatform platform;

	// Use this for initialization
	void Start () {
        platform = GetComponentInParent<MonsterPlatform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reveal()
    {
        Debug.Log("Revealing");
        platform.Reveal();
    }

    public void Hide()
    {
        Debug.Log("Hiding");
    }

}
