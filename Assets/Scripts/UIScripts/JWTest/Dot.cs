using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //print("进来了");
            /*ContactPoint contactPoint = collision.GetContacts[0];
            Vector3 newDir = Vector3.zero;
            Vector3 curDir = transform.TransformDirection(Vector3.forward);
            newDir = Vector3.Reflect(curDir, contactPoint.normal);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, newDir);
            transform.rotation = rotation;*/
            //反射
            //ContactPoint2D contactPoint = collision.contacts[0];

        }
    }
}
