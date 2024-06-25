using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMotor : MonoBehaviour
{

    [SerializeField] private CircleCollider2D groundTrigger;
    [SerializeField] private Rigidbody2D playerRig;

    [SerializeField] private float speedMod;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    private float jumpForceDefault = 12000f;
    public float JumpForceDefault
    {
        get { return jumpForceDefault; }
    }

    private float jumpForceTrampoline = 17000f;
    public float JumpForceTrampoline
    {
        get { return jumpForceTrampoline; }
    }

    private float jumpForceBounceoff = 12000f;

    public float JumpForceBounceoff
    {
        get { return jumpForceBounceoff; }
    }


    public bool IsGrounded = false;

    private Vector2 generalVelocity = Vector2.zero;

    [SerializeField] private float horVelMagnitude;
    public float HorizontalVelocityMagnitude
    {
        get { return horVelMagnitude; }
    }

    [SerializeField] private float verVelMagnitude;
    public float VerticalVelocityMagnitude
    {
        get { return verVelMagnitude; }
    }

    public void KillVelocity()
    {
        playerRig.velocity = Vector2.zero;    
    }

    private void Update()
    {
        horVelMagnitude = Mathf.Abs(playerRig.velocity.x);
        verVelMagnitude = Mathf.Abs(playerRig.velocity.y);
    }

    /// <summary>
    /// Механика движения
    /// </summary>
    /// <param name="newVel">Инпут с горизонтальной оси, флоат</param>
    public void SetVelocity(float inputVelocity)
    {
        Vector3 targetVelocity = new Vector2(inputVelocity * 10f, playerRig.velocity.y);
        playerRig.velocity = Vector2.SmoothDamp(playerRig.velocity, targetVelocity, ref generalVelocity, movementSmoothing);
    }

    /// <summary>
    /// Физическое выполнение прыжка
    /// </summary>
    /// <param name="force">сила прыжка</param>
    /// <param name="requireGroundCheck">требуется ли проверка на приземленность?</param>
    public void SetJump(float force, bool requireGroundCheck)
    {
        //я бы над этим подумал получше
        if (requireGroundCheck && IsGrounded)
        {
            playerRig.AddRelativeForce(new Vector2(0f, force));
        }
        else
        {
            playerRig.AddRelativeForce(new Vector2(0f, force));
        }
        
    }

    


    //============= Блок коллизии с землёй ================
    // простите за небольшой бойлерплейт

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = false;
        }
    }
}
