using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollower : MonoBehaviour
{

    [SerializeField] private GameObject targetGameobject;
    [SerializeField] private float movementSmoothing;

    // Update is called once per frame
    void Update()
    {
        if (targetGameobject != null)
        {
            this.gameObject.transform.DOMoveX(targetGameobject.transform.position.x, movementSmoothing);
        }
        
    }
}
