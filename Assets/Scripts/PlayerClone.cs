using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClone : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] public float shieldHP;
    [SerializeField] GameObject[] ShieldSprites;
    [SerializeField] private TrailRenderer trailRenderer;

    public float shieldMaxHp = 40.0f;

    private Shooter shooter;

    private bool _hasShieldUp = false;

    public bool HasShieldUp
    {
        get { return _hasShieldUp; }
        set { _hasShieldUp = value; }
    }

    private float dashSpeedMultiplier = 2.0f;
    private float dashDuration = 0.2f;
    private float dashCooldown = 2.0f;
    private float dashCooldownTimer = 0f;
    private bool isDashing = false;
    private float normalSpeed;

    public float GetDashCooldown()
    {
        return dashCooldown;
    }

    public float GetDashCooldownTimer()
    {
        return dashCooldownTimer;
    }


    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }
    private void Start()
    {
        //InitBounds();
        normalSpeed = moveSpeed;
    }

    void Update()
    {
        //PlayerMove();

        if (_hasShieldUp == true)
        {
            ShieldsUp();
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && dashCooldownTimer <= 0)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        moveSpeed *= dashSpeedMultiplier;
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashDuration);

        trailRenderer.emitting = false;
        moveSpeed = normalSpeed;
        isDashing = false;

        dashCooldownTimer = dashCooldown;
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

    public void ShieldsUp()
    {
        float maxShieldHP = 40.0f;

        if (shieldHP == maxShieldHP)
        {
            ShieldSpriteActive(0);
        }
        if (shieldHP <= maxShieldHP / 2)
        {
            ShieldSpriteActive(1);
        }
        if (shieldHP <= maxShieldHP / 3)
        {
            ShieldSpriteActive(2);
        }
        if (shieldHP <= 0)
        {
            ShieldSpriteActive(-1);
        }


    }

    public void ShieldSpriteActive(int index)
    {
        for (int i = 0; i < ShieldSprites.Length; i++)
        {
            ShieldSprites[i].SetActive(false);
        }
        if (index < 0)
        {
            return;
        }
        else
        {
            ShieldSprites[index].SetActive(true);
        }
    }
}
