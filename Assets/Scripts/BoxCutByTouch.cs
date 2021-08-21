using System;
using UnityEngine;

public class BoxCutByTouch : MonoBehaviour
{

    [SerializeField] GameObject touchedObject;
    [SerializeField] Transform[] planes;
    [SerializeField] float cutPlace = .3f;
    private void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.collider != null && hit.transform.gameObject.CompareTag("Target"))
                {

                    touchedObject = hit.transform.gameObject;
                    SetCutPlanes();
                    RayFire.RayfireShatter shatter = touchedObject.GetComponent<RayFire.RayfireShatter>();
                    shatter.previewScale = 0.1f;
                    shatter.scalePreview = false;
                    shatter.scalePreview = true;
                    shatter.Fragment();

                    Debug.Log("Touched " + touchedObject.transform.name);
                }
            }
        }
    }


    private void SetCutPlanes()
    {
        bool minMaxY = true;
        bool minMaxX = true;
        bool minMaxZ = true;
        foreach (Transform plane in planes)
        {
            Vector3 angles = plane.eulerAngles;
            if (angles == Vector3.zero)
            {
                SetPlanes(plane, minMaxY, 'y');
                minMaxY = !minMaxY;
                continue;
            }
            if (angles == new Vector3(0, 0, 90))
            {
                SetPlanes(plane, minMaxX, 'x');
                minMaxX = !minMaxX;
                continue;
            }
            if (angles == new Vector3(0, 90, 90))
            {
                SetPlanes(plane, minMaxZ, 'z');
                minMaxZ = !minMaxZ;
                continue;
            }
        }
    }

    private void SetPlanes(Transform plane, bool minMax, char verticee)
    {
        Vector3 verticePosition;
        Vector3 max = Vector3.zero;
        Vector3 min = new Vector3(0, 5);
        foreach (Vector3 vertice in touchedObject.GetComponent<MeshFilter>().mesh.vertices)
        {
            if (verticee.Equals('y'))
            {
                if (vertice.y > max.y)
                {
                    max = vertice;
                    Debug.Log("MaxY: " + touchedObject.transform.TransformPoint(max));
                }

                if (vertice.y < min.y)
                {
                    min = vertice;
                    Debug.Log("MinY: " + touchedObject.transform.TransformPoint(min));
                }
            }

            if (verticee.Equals('x'))
            {
                if (vertice.x > max.x)
                {
                    max = vertice;
                    Debug.Log("MaxX: " + touchedObject.transform.TransformPoint(max));
                }

                if (vertice.x < min.x)
                {
                    min = vertice;
                    Debug.Log("MinX: " + touchedObject.transform.TransformPoint(min));
                }
            }

            if (verticee.Equals('z'))
            {
                if (vertice.z > max.z)
                {
                    max = vertice;
                    Debug.Log("MaxZ: " + touchedObject.transform.TransformPoint(max));
                }

                if (vertice.z < min.z)
                {
                    min = vertice;
                    Debug.Log("MinZ: " + touchedObject.transform.TransformPoint(min));
                }
            }

        }

        if (minMax)
        {
            verticePosition = max;
        }
        else
        {
            verticePosition = min;
        }

        plane.position = touchedObject.transform.TransformPoint(verticePosition * (cutPlace));
    }


}
