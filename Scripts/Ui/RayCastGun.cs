using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastGun : MonoBehaviour
{
    public Vector3 target;
    private int layerMask;
    private void Start()
    {
         layerMask = LayerMask.GetMask("line", "wall", "Default");
    }
   
    public Vector3 SetRayCast(out BiaDan bd)
    {
        
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            target = hit.point;
            if(hit.collider.gameObject.CompareTag(ConstName.BiaDan))
            {
                bd = hit.collider.gameObject.GetComponent<BiaDan>();
                return hit.point;
            }
            bd = null;
            return hit.point;
        }
        bd = null;
        return Vector3.zero;
    }
    public Vector3 SetRayCast2()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
