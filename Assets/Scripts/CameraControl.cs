using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



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
    Material[] matts;

    string[] NoGoTags = { "Player", "Floor", "Enemy", "Item"};


    //Vector3 dir;
    //private RaycastHit hitInfo;
    //LayerMask _layermask;

    void Update()
    {


        float distToCamera = Vector3.Distance(transform.position, target.transform.position);
        Vector3 dirToCamera =transform.position - target.transform.position;
        //RaycastHit[] hits = hits = Physics.RaycastAll(transform.position,transform.forward, 50f);

        RaycastHit[] hits = Physics.RaycastAll(target.transform.position, dirToCamera, distToCamera);






        foreach (RaycastHit h in hits)
        {
            Color tempcolor;
            //if (h.collider.gameObject.tag != "Player" && h.collider.gameObject.tag != "Floor" && h.collider.gameObject.tag != "Enemy" && h.collider.gameObject.tag != "Item" && h.collider.GetComponent<Renderer>() != null)
            //{

            //}
            if (NoGoTags.Contains(h.collider.gameObject.tag))
            {

            }
            else if (h.collider.GetComponent<Renderer>() != null)
            {
                Debug.Log(h.collider.GetComponent<Renderer>().material);
                matts = h.collider.GetComponentInChildren<Renderer>().materials;
                for (var i = 0; i < h.collider.GetComponentInChildren<Renderer>().materials.Length; i++)
                {
                    MaterialExtensions.ToFadeMode(h.collider.GetComponentInChildren<Renderer>().materials[i]);

                    tempcolor = h.collider.GetComponentInChildren<Renderer>().materials[i].color;
                    tempcolor.a = .15f;
                    h.collider.GetComponentInChildren<Renderer>().materials[i].color = tempcolor;

                    StartCoroutine(ChangeBack(h.collider.GetComponentInChildren<Renderer>().materials[i], 2f));
                }

            }

        }


        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);


    }


     IEnumerator ChangeBack(Material material, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        MaterialExtensions.ToOpaqueMode(material);
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


public static class MaterialExtensions
{
    public static void ToOpaqueMode(this Material material)
    {
        material.SetOverrideTag("RenderType", "");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }

    public static void ToFadeMode(this Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }
}