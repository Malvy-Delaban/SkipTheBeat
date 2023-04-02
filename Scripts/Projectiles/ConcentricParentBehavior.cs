using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentricParentBehavior : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.Rotate(0, 0, 10f * rotationSpeed * Time.deltaTime); 
    }
}