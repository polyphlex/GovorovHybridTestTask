using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private PlayerMotor playerMotor;
    [SerializeField] private PlayerAnimations playerAnim;

    [SerializeField] SpriteRenderer[] spriteRenderers;

    private bool IsControllable = true;
    private bool isInvulnerable = false;
    [SerializeField] private float invulnDurationSeconds;
    [SerializeField] private float flashDurationSeconds;

    private void Start()
    {
        instance = this;
    }

    public void DisablePlayerInput()
    {
        IsControllable = false;
        playerMotor.KillVelocity();
    }

    private void Update()
    {
        if (IsControllable)
        {
            InputRoutine();
        }
        

        playerAnim.SendVelocity(playerMotor.HorizontalVelocityMagnitude, playerMotor.VerticalVelocityMagnitude);
    }

    /// <summary>
    /// Обработка ввода и событение событий
    /// </summary>
    private void InputRoutine()
    {
        float tempHorAxisInput = Input.GetAxis("Horizontal");
        float tempVerAxisInput = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            JumpMechanic(playerMotor.JumpForceDefault, true);
        }

        playerMotor.SetVelocity(tempHorAxisInput);


        playerAnim.FlipHandling(tempHorAxisInput);
    }

    /// <summary>
    /// Дефолтный прыжок по кнопке
    /// </summary>
    /// <param name="forceValue">сила прыжка</param>
    private void JumpMechanic(float forceValue, bool requireGroundCheck)
    {
        playerMotor.SetJump(forceValue, requireGroundCheck);
        playerAnim.JumpTrigger();
    }

    /// <summary>
    /// спрыжок с головы врага (аля марио)
    /// </summary>
    public void AddBounceOff()
    {
        JumpMechanic(playerMotor.JumpForceBounceoff, false);
    }

    /// <summary>
    /// трамплинный прыжок
    /// </summary>
    public void AddTrampoline()
    {
        JumpMechanic(playerMotor.JumpForceTrampoline,false);
    }

    private IEnumerator ReceiveDamage()
    {
        
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            MakeFlashes(Color.red);
            if (GlobalValueDispatcher.instance.PlayerLivesAmount == 1)
            {
                FadeOut(2f);
                GlobalFlowControl.instance.GameOver();
                Destroy(this.gameObject);
            }

            GlobalValueDispatcher.instance.RemoveLife();
            yield return new WaitForSeconds(invulnDurationSeconds);
            isInvulnerable = false;

        }
        
    }

    private void Instadeath()
    {
        GlobalFlowControl.instance.GameOver();
    }

    private void MushroomTaken()
    {
        GlobalValueDispatcher.instance.AddLife();
        MakeFlashes(Color.green);
    }

    private void MakeFlashes(Color flashColor)
    {

        StartCoroutine(MakeFlashesRoutine(flashColor));
    }

    private IEnumerator MakeFlashesRoutine(Color flashColor)
    {
        int loopNum = Mathf.CeilToInt(invulnDurationSeconds / flashDurationSeconds);

        //print("loopnum " + loopNum);

        for (int i = 1; i < loopNum; i++)
        {
            //print("i" + i);
            foreach (var item in spriteRenderers)
            {
                item.DOColor(flashColor, flashDurationSeconds);
            }

            yield return new WaitForSeconds(flashDurationSeconds / 2);

            foreach (var item in spriteRenderers)
            {
                item.DOColor(Color.white, flashDurationSeconds);
            }

            yield return new WaitForSeconds(flashDurationSeconds / 2);

        }
    }

    private void FadeOut(float duration)
    {
        foreach (var item in spriteRenderers)
        {
            item.DOFade(0f, duration);
        }
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.gameObject.tag)
        {
            case "Damagers":
                print("DAMAGING");
               StartCoroutine(ReceiveDamage());
                break;

            case "Instadeath":
                print("INSTADEATH");
                Instadeath();
                break;
            case "Mushroom":
                MushroomTaken();
                Destroy(collision.collider.gameObject);
                break;
        }
    }
}
