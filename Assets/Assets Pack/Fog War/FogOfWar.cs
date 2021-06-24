using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] GameObject fogOfWarPlane;
    [SerializeField] Transform playerRef;
    [SerializeField] LayerMask fogLayer;
    [SerializeField] float Radio;
    private float radiosSpr{get{return Radio* Radio; }}
    private Mesh mesh;
    private Vector3[] verticesVar;
    private Color[] colors;
    void Start()
    {
       // Initialize();
    }
    void Update()
    {
        
    }






    /*// Update is called once per frame
    void Update()
    {
        Ray r = new Ray(transform.position, playerRef.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, fogLayer, QueryTriggerInteraction.Collide))
        { for (int i = 0; i < verticesVar.Length; i++)
            {
                    Vector3 v = fogOfWarPlane.transform.TransformPoint(verticesVar[i]);
                    float dist=Vector3.SqrMagnitude(v-hit.point);
                    if (dist < radiosSpr)
                    {
                        float alpha = Mathf.Min(colors[i].a, dist / radiosSpr);
                        colors[i].a = alpha;
                    }
            }
            UpdateColor();
            Debug.Log("pintar de transparente");
        }
    }
    void Initialize()//pintar todado los vertises de la niebla de guerra de negro
    {
        mesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        verticesVar = mesh.vertices;
        colors = new Color[verticesVar.Length];
        for (int i=0; i< colors.Length; i++)
        {
            colors[i] = Color.black;
        }
        UpdateColor();
    }
    void UpdateColor()
    {
        mesh.colors =colors;
        Debug.Log("pintar funcion");
    }*/
}
