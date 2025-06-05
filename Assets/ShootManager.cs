using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public DetectManager DManager;
    public GameObject Ball;

    public float upMin = 0.5f, upMax = 3.0f;
    public float forwardMin = 0.5f, forwardMax = 3.0f;
    public float Amplitude = 2.0f;
    private float random_up, random_forward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            DManager.totalNum += 1;

            GameObject b = Instantiate(Ball, transform.position, transform.rotation);

            b.GetComponent<TriggerOnCollider>().DManager = DManager;
            
            random_up = Random.Range(upMin, upMax);
            random_forward = Random.Range(forwardMin, forwardMax);

            Debug.Log((b.transform.forward * random_forward + b.transform.up * random_up) * Amplitude);
            b.GetComponent<Rigidbody>().AddForce((b.transform.forward * random_forward + b.transform.up * random_up) * Amplitude);
        }

        
    }
}
