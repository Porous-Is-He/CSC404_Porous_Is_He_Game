using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject moveCamera;
    public GameObject aimCamera;
    public Canvas crosshair;

    private bool switched;

    public float speedV = 2.0f;
    public float speedH = 2.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        switched = false;
    }

    // Update is called once per frame
    void Update()
    {
        //rotation = transform.rotation;
        if (Input.GetKey(KeyCode.Mouse1)) {
            if (switched)
            {
                rotationX = transform.eulerAngles.x;
                rotationY = transform.eulerAngles.y;
                aimCamera.transform.eulerAngles = new Vector3(rotationX, rotationY, 0.0f);

                moveCamera.SetActive(false);
                aimCamera.SetActive(true);
                crosshair.enabled = true;
                gameObject.GetComponent<MoverScript>().enabled = false;
                switched = false;

            }
            AimCamera();

        } else if (!switched)
        {
            moveCamera.SetActive(true);
            aimCamera.SetActive(false);
            crosshair.enabled = false;
            gameObject.GetComponent<MoverScript>().enabled = true;
            switched = true;

        }
    }

    private void AimCamera()
    {
        rotationX -= speedV * Input.GetAxis("Mouse Y");
        rotationY += speedH * Input.GetAxis("Mouse X");

        if (rotationX > 0.0f)
        {
            rotationX = 0.0f;
        }
        else if (rotationX < -60.0f)
        {
            rotationX = -60.0f;
        }

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}
