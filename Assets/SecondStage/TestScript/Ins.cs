using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ins : MonoBehaviour
{
    public GameObject cube;
    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject a = Instantiate(cube);
            a.GetComponent<Father>().abs = a.gameObject.AddComponent<ShenYouHuanJing>();

        }
    }
}
