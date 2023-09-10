using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessHandler : MonoBehaviour
{
    public Transform Childrent;
    public float timeMax = 2;
    public float timeMin = 1;
    private bool Check = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.ItemTag) && !Check)
        {
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.GapLen,1);
            Check = true;
            other.transform.parent = Childrent;
            other.transform.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(RandomHander(other.gameObject, Random.Range(timeMin, timeMax)));
        }
        else if (other.CompareTag(ConstName.Rac) && !Check)
        {
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.GapLen, 1);
            Check = true;
            other.transform.parent = Childrent;
            other.transform.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(RandomHander(other.gameObject,3));
        }
    }
    private IEnumerator RandomHander(GameObject g,float time)
    {
        yield return new WaitForSeconds(time);
       
        g.transform.parent = null;
        g.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(1);
        Check = false;
    }
}
