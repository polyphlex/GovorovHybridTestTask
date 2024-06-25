using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator anim;

    /// <summary>
    /// ��������� �������� ������
    /// </summary>
    public void JumpTrigger()
    {
        anim.SetTrigger("Jump");
    }

    /// <summary>
    /// �������� ��������� � �������� ��� ���������� �������� ��������
    /// </summary>
    /// <param name="horizontalVelocity">�������������� ��������</param>
    /// <param name="verticalVelocity">������������ ��������</param>
    public void SendVelocity(float horizontalVelocity, float verticalVelocity)
    {
        anim.SetFloat("VerticalVelocity", verticalVelocity);
        anim.SetFloat("HorizontalVelocity", horizontalVelocity);
    }

    /// <summary>
    /// ������������ �������������� ������� � ����������� �� �����������
    /// </summary>
    public void FlipHandling(float inputVelocity)
    {
        //����� ���� �������� � �������� �� ���� ������
        this.gameObject.transform.localScale = new Vector3(Mathf.Sign(inputVelocity) * 1, 1);
    }

}
