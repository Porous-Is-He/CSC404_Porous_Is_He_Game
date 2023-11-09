using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] private float weight;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetWeight() => weight;

    public void AddWeight(float add)
    {
        weight += add;
    }
}
