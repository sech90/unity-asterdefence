using UnityEngine;
using System.Collections;

public class TestStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector2 v1 = new Vector2(10, 0);
        Vector2 v2 = new Vector2(5, 5);

        float dot = Vector2.Dot(v2, v1);
        Debug.Log("Dot is: "+dot);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
