using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //public float rotationSpeed = 10;

    public Transform target;

    public Vector3 offset;

    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    private float currentZoom = 10f;

    public float pitch = 2f;

    void Update()
    {


        //Debug.Log(rotation);
        //Debug.Log(transform.eulerAngles);

        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    void LateUpdate()
    {
        //Vector2 rotation = transform.eulerAngles;

        

        transform.position = target.position - offset * currentZoom;


        //rotation.y += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime; // Left Right, A & D keys
        //transform.rotation = Quaternion.Euler(0, target.transform.rotation.eulerAngles.y, 0);


        transform.LookAt(target.position + Vector3.up * pitch);




        //transform.localEulerAngles = rotation;
        //transform.eulerAngles = rotation;


    }

}
