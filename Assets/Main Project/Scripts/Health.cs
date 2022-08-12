using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
public class Health : MonoBehaviour
{
    public virtual void CheckIfWeDead()
    {
        Debug.Log("Checking if we are dead");
    }
}
