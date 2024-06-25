using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickWithShroom : BrickBlock
{
    [SerializeField] private GameObject shroomPrefab;

    protected override void CollisionHappened(Collision2D collision)
    {
        
        base.CollisionHappened(collision);
        Instantiate(shroomPrefab, this.transform.position, this.transform.rotation);
    }
}
