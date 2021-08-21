using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] RayFire.RayfireGun gun;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    gun.transform.position = Camera.main.transform.position;
                    target.position = hit.point;
                    gun.Burst();
                    gun.StopShooting();
                }
            }
        }
    }
}
