using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D mainCollider;
    [SerializeField] private CircleCollider2D stompCollider;

    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private SpriteRenderer spRend;

    [SerializeField] private Transform LeftCastStart, LeftCastEnd, RightCastStart, RightCastEnd;

    private bool alive = true;

    [SerializeField] private float enemyVelocity;
    private int velocitySign = -1;

    private void FixedUpdate()
    {
        var lHit = Physics2D.Linecast(LeftCastStart.position, LeftCastEnd.position);
        var rHit = Physics2D.Linecast(RightCastStart.position, RightCastEnd.position);

        //защита от дёргания на одном блоке
        if (lHit.collider == null && rHit.collider == null)
        {
            velocitySign = 0;
        }

        if (velocitySign == 0 && lHit.collider != null)
        {
            velocitySign = -1;
            spRend.flipX = false;
        }

        if (lHit.collider == null && velocitySign == -1) ChangeDirection(); // || lHit.collider.gameObject.tag != "Ground"

        if (rHit.collider == null && velocitySign == 1) ChangeDirection(); // || rHit.collider.gameObject.tag != "Ground"

        rig.velocity = new Vector2(enemyVelocity * velocitySign, rig.velocity.y);
    }

    /// <summary>
    /// обработка смэрти врага
    /// </summary>
    private void DeathSequence()
    {
        alive = false;
        //print("Death happens");
        rig.constraints = RigidbodyConstraints2D.None;
        mainCollider.enabled = false;
        spRend.DOFade(0, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && alive)
        {
            PlayerController.instance.AddBounceOff();
            DeathSequence();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Instadeath")
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeDirection()
    {
        velocitySign = velocitySign * -1;
        spRend.flipX = !spRend.flipX;
    }
    
}
