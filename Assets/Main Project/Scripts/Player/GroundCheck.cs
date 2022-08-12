using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded=false;
    private void OnCollisionEnter2D(Collision2D other) 
    {
         if(other.gameObject.CompareTag("Platform"))
        {
            isGrounded=true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) 
    {
         if(other.gameObject.CompareTag("Platform"))
        {
            isGrounded=false;
        }
    }
    
       
    
}
