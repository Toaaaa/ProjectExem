using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAnimatorSet : MonoBehaviour
{
    public SkillProjectileShot projectileShot;

    private void Awake()
    {
        Animator animator = GetComponent<Animator>();
        projectileShot.SetExternalAnimator(animator);
    }
}
