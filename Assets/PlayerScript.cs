using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Vector3 forward = new Vector3(0, 0, -1);
    [SerializeField] private Vector3 back = new Vector3(0, 0, 1);
    [SerializeField] private Vector3 left = new Vector3(1, 0, 0);
    [SerializeField] private Vector3 right = new Vector3(-1, 0, 0);
    [SerializeField] private float speed = 10.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(forward * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(left * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(back * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(right * speed);
        }
    }
}