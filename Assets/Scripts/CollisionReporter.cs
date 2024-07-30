using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionReporter : MonoBehaviour
{
    [SerializeField] private bool isTrigger;

    private Collider collider;

    public UnityEvent<Transform> OnTriggerEntered = new UnityEvent<Transform>();
    public UnityEvent<Transform> OnTriggerStayed = new UnityEvent<Transform>();
    public UnityEvent<Transform> OnTriggerExited = new UnityEvent<Transform>();
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        isTrigger = collider.isTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isTrigger) return;
        
        OnTriggerEntered.Invoke(other.transform);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (!isTrigger) return;

        OnTriggerStayed.Invoke(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isTrigger) return;
        
        OnTriggerExited.Invoke(other.transform);
    }
}
