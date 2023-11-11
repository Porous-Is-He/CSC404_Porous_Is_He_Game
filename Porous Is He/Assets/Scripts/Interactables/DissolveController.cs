using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    // To use this, the object to be burned must be a child of an empty object parent
    // This script is attached to the parent
    // Attach the objects mesh renderer to the mesh variable.
    // This object must also have the material on it using the shaders/disintegrate
    // make sure that on the material, alpha clipping is enabled
    // To dissintegrate, call StartCoroutine(Dissolve())

    // Refer to JennysPlayground Scene for an example


    public MeshRenderer mesh;
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;


    private Material[] materials;

    void Start()
    {
        if (mesh != null)
            materials = mesh.materials;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Dissolve()
    {
        if (materials.Length > 0)
        {
            float counter = 0;
            while (materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for(int i = 0; i< materials.Length; i++)
                {
                    materials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
