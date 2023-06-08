using FMODUnity;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [Header("Scripts Dependencies")]
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public PlayerController controller;
    [Header("Audio")]
    [SerializeField] public StudioEventEmitter bangSFX;
    [SerializeField] public StudioEventEmitter finalAttack2SFX;
    [SerializeField] public StudioEventEmitter punch3Attack;
    [SerializeField] public StudioEventEmitter runningAttackHitSFX;
    [SerializeField] public StudioEventEmitter playerHurtSFX;
    [SerializeField] public StudioEventEmitter playerJumpSFX;
    [SerializeField] public StudioEventEmitter playerKnockDownSFX;
    [SerializeField] public StudioEventEmitter finalAttackSmallSFX;
    [SerializeField] public StudioEventEmitter superAttackSFX;
    [Header("Body Components")]
    [SerializeField] public new Rigidbody2D rigidbody2D;
    [Header("Special Effects")]
    [SerializeField] public GameObject bloodSplash;
    [SerializeField] public GameObject attack3FX;
    [SerializeField] public GameObject finalAttackFX;
    [SerializeField] public GameObject kickDust;
    [SerializeField] public GameObject dustTray;
    [SerializeField] public GameObject dustOnFall;
    [SerializeField] public GameObject metallShine;
    [SerializeField] public GameObject hitMarks;
    [SerializeField] public GameObject superAttackEffect;
    [SerializeField] public GameObject runSmoke;
    public void DisableMovement()
    {
       playerMovement.enabled = false;
       rigidbody2D.bodyType = RigidbodyType2D.Static;
    }
    public void EnableMovement()
    {
        playerMovement.enabled = true;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
    public void Attack3FXOn()
    {
        attack3FX.SetActive(true);
    }

    public void Attack3FXOff()
    {
        attack3FX.SetActive(false);
    }

    public void FinalAttackFXOn()
    {
        finalAttackFX.SetActive(true);
    }

    public void FinalAttackFXOff()
    {
        finalAttackFX.SetActive(false);
    }

    public void FinalKickDustOn()
    {
        kickDust.SetActive(true);
    }

    public void FinalKickDustOff()
    {
        kickDust.SetActive(false);
    }

    public void FinalPunchDustTrayOn()
    {
        dustTray.SetActive(true);
    }

    public void FinalPunchDustTrayOff()
    {
        dustTray.SetActive(false);
    }

    public void DustOnFallOn()
    {
        dustOnFall.SetActive(true);
    }

    public void DustOnFallOff()
    {
        dustOnFall.SetActive(false);
    }

    public void BloodSplashOn()
    {
        bloodSplash.SetActive(true);
    }

    public void BloodSplashOff()
    {
        bloodSplash.SetActive(false);
    }

    public void MetallShineOn()
    {
        metallShine.SetActive(true);
    }

    public void MetallShineOff()
    {
        metallShine.SetActive(false);
    }

    public void AttackOn()
    {
        playerMovement.isAttacking= true;
        playerMovement.isJumping= true;
    }

    public void AttackOff()
    {
        playerMovement.isAttacking= false;
        playerMovement.isJumping= false;
    }

    public void HitMarksOn()
    {
        hitMarks.SetActive(true);
    }

    public void HitMarksOff()
    {
        hitMarks.SetActive(false);
    }

    public void SuperAttackOn()
    {
        playerMovement.superMove = true;
    }

    public void SuperAttackOff()
    {
        playerMovement.superMove = false;
    }

    public void SuperAttackEfectOn()
    {
        superAttackEffect.SetActive(true);
    }

    public void SuperAttackEffectOff()
    {
        superAttackEffect.SetActive(false);
    }

    public void RunningSmokeEfectOn()
    {
        runSmoke.SetActive(true);
    }

    public void RunningSmokeEffectOff()
    {
        runSmoke.SetActive(false);
    }

    public void RunningAttackOn()
    {
        playerMovement.isRunningAttack= true;
    }

    public void RunningAttackOff()
    {
        playerMovement.isRunningAttack = false;
    }
    public void BangSXFOn()
    {
        bangSFX.Play();
    }
    public void FinalAttack2SFX()
    {
        finalAttack2SFX.Play();
    }

    public void Punch3SFX()
    {
        punch3Attack.Play();
    }

    public void RunningAttackSFX()
    {
        runningAttackHitSFX.Play();
    }

    public void PlayerHurtSFX()
    {
       playerHurtSFX.Play();
    }

    public void PlayerJumpSFX()
    {
        playerJumpSFX.Play();
    }

    public void PlayerKnockDownSFX()
    {
        playerKnockDownSFX.Play();
    }
    public void FinalAttackSmallSFX()
    {
        finalAttackSmallSFX.Play();
    }

    public void SuperAttackSFX()
    {
        superAttackSFX.Play();
    }
}
