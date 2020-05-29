using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public LayerMask movementMask;
    Camera cam;
    PlayerMove move;
    public Animator animator;
    private float speed;
    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;
        move = GetComponent<PlayerMove>();
        speed = 1f;

    }

    // Update is called once per frame
    void Update() {
        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, speed * Input.GetAxis("Vertical") * Time.deltaTime); ;
        animator.SetFloat("verticale", Input.GetAxis("Vertical"));
        animator.SetFloat("horizontale", Input.GetAxis("Horizontal"));
        //if (Input.GetMouseButtonDown(0))
        //{
        //    animator.SetFloat("verticale", Input.GetAxis("Vertical"));
        //    animator.SetFloat("horizontale", Input.GetAxis("Horizontal"));
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if(Physics.Raycast(ray, out hit, 100, movementMask))
        //    {
        //        move.Move(hit.point);

        //        //Debug.Log("Hit" + hit.collider.name +" " + hit.point);
        //        //Move player to what hits

        //        //Stop focusing object


        //    }
        //}


    }
}
