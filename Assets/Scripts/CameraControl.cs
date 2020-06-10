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


    private GameObject hitObject;
    RaycastHit oldHit = new RaycastHit();


    private void Start()
    {

    }

    void Update()
    {


        float distToCamera = Vector3.Distance(transform.position, target.transform.position);
        Vector3 dirToCamera = transform.position - target.transform.position;
        RaycastHit[] hits = Physics.RaycastAll(target.transform.position, dirToCamera, distToCamera);
        
        //float radius = 1f;
        //RaycastHit[] hits = Physics.SphereCastAll(target.transform.position, radius, dirToCamera, distToCamera);

        foreach (RaycastHit h in hits)
        {
            Color tempcolor;
            if (NoGoTags.Contains(h.collider.gameObject.tag))
            {

            }
            else if (h.collider.GetComponent<Renderer>() != null)
            {


                if (oldHit.collider == null)
                {
                    oldHit = h;
                }
                if (oldHit.transform.gameObject.GetInstanceID() == h.transform.gameObject.GetInstanceID())
                {
                    for (var i = 0; i < h.collider.GetComponentInChildren<Renderer>().materials.Length; i++)
                    {
                        MaterialExtensions.ToFadeMode(oldHit.collider.GetComponentInChildren<Renderer>().materials[i]);
                        tempcolor = oldHit.collider.GetComponentInChildren<Renderer>().materials[i].color;
                        tempcolor.a = .15f;
                        oldHit.collider.GetComponentInChildren<Renderer>().materials[i].color = tempcolor;
                    }
                }
                else
                {
                    for (var j = 0; j < oldHit.collider.GetComponentInChildren<Renderer>().materials.Length; j++)
                    {
                        StartCoroutine(ChangeBack(oldHit.collider.GetComponentInChildren<Renderer>().materials[j], 5f));
                    }
                }
                oldHit = h;

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
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
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