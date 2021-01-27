using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingBallBehaviour : MonoBehaviour
{
    public float Speed;

    new Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        
        GetComponent<ConstantForce>().force = movement * Speed * Time.deltaTime;
        GetComponent<Rigidbody>().AddForce(movement * Speed * Time.deltaTime);
    }
}
