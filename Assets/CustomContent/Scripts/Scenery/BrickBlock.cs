using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlock : BaseBlock
{
    [SerializeField] private GameObject particles;
    [SerializeField] private float velocityToBreak = 5;

    protected override void CollisionHappened(Collision2D collision)
    {
        //print(collision.rigidbody + " rig; " + collision.otherRigidbody + " oth rig; " + collision.gameObject.name);
        
        if (collision.rigidbody.velocity.magnitude >= velocityToBreak &&
            collision.gameObject.tag == "Player")
        {
            Instantiate(particles, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        
    }
}
