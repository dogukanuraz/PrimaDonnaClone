using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator playerAnimator;
    


    public void PlayerRunAnim()
    {
        playerAnimator.SetBool("isRunning", true);
    }

    public void PlayerIdleAnim()
    {
        playerAnimator.SetBool("isRunning", false);
    }

    public void PlayerAttack()
    {
        StartCoroutine(SetAnim("isAttacking"));
    }

    public void PodiumWalk()
    {
        StartCoroutine(SetAnim("isPodium"));
    }

    public void Charge()
    {
        StartCoroutine(SetAnim("isFinished"));
    }

    IEnumerator SetAnim(string animName)
    {

        playerAnimator.SetBool(animName, true);
        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool(animName, false);
    }

}
