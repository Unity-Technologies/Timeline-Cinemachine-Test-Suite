using UnityEngine;
using System.Collections;

public class PointLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if ( Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Rotate( 0,Time.deltaTime*100,0);
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Rotate(0,Time.deltaTime*-100,0);
		}
	
	}
}
