using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform mainplayer;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainplayer == null)
            return;

        transform.position = new Vector3(mainplayer.position.x + offset.x, mainplayer.position.y + offset.y, transform.position.z);
    }
}
