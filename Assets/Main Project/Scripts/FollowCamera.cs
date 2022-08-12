using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        this.transform.position=new Vector3(player.transform.position.x+offset.x,player.transform.position.y+offset.y,player.transform.position.z+offset.z);
    }
}
