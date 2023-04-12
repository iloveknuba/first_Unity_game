using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public GameObject tunnel;

    private void OnMouseDown()
    {
        tunnel.SetActive(true);
    }




}
