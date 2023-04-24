using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
   
    public GameObject highlight;

 
    private void OnMouseEnter()
    {
        var squares = GameObject.FindGameObjectsWithTag("Player");
        foreach (var square in squares)
        {
           
            var row = (int)square.transform.position.y;
            var col = (int)square.transform.position.x;
            if (Player.Instance.IsNearToHero(row, col))
            {
                highlight.SetActive(true);
            }

        }
    }
    private void OnMouseExit()
    {
        highlight.SetActive(false); 
    }
    


 

}
