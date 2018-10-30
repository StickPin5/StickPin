using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour {

    public float speed = 3;
    private float one = 1;
    public bool IsRotate = true;
    private void Start()
    {
        StartCoroutine(changeRotation());
    }
    // Update is called once per frame
    void Update () {
        //transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime));
	}
    private void FixedUpdate()
    {
        if (IsRotate)
        {
            transform.GetComponent<Rigidbody2D>().AddTorque(speed);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator changeRotation()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            one = -one;
            speed = Random.Range(1,5);
            speed = speed * one;
            //Debug.LogError("speed   " + speed);
        }
    }
}
