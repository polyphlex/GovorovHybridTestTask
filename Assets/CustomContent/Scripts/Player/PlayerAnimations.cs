using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator anim;

    /// <summary>
    /// Запустить анимацию прыжка
    /// </summary>
    public void JumpTrigger()
    {
        anim.SetTrigger("Jump");
    }

    /// <summary>
    /// Отправка скоростей в аниматор для управления анимками движения
    /// </summary>
    /// <param name="horizontalVelocity">горизонтальная скорость</param>
    /// <param name="verticalVelocity">вертикальная скорость</param>
    public void SendVelocity(float horizontalVelocity, float verticalVelocity)
    {
        anim.SetFloat("VerticalVelocity", verticalVelocity);
        anim.SetFloat("HorizontalVelocity", horizontalVelocity);
    }

    /// <summary>
    /// Обрабатывает отзеркаливание спрайта в зависимости от направления
    /// </summary>
    public void FlipHandling(float inputVelocity)
    {
        //берем знак велосити и умножаем на него пэрент
        this.gameObject.transform.localScale = new Vector3(Mathf.Sign(inputVelocity) * 1, 1);
    }

}
