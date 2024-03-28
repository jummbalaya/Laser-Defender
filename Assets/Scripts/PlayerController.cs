using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8.0f;

    [SerializeField] float paddingLeft = 1f;
    [SerializeField] float paddingRight = 1f;
    [SerializeField] float paddingTop = 5.0f;
    [SerializeField] float paddingBottom = 2.0f;
    [SerializeField] public float shieldHP;
    [SerializeField] GameObject[] ShieldSprites;
    [SerializeField] private TrailRenderer trailRenderer;

    [SerializeField] GameObject playerClone;

   

    public float shieldMaxHp = 40.0f;

    private Vector2 rawInput;
    private Vector2 minBounds;
    private Vector2 maxBounds;

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
    private float dashSpeed;
    private float normalSpeed;
    bool buttonPressed = false;

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
        InitBounds();
        normalSpeed = moveSpeed;
    }

    void Update()
    {
        PlayerMove();

        if (_hasShieldUp == true)
        {
            ShieldsUp();
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

        public void PlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.Space) || buttonPressed)
        {
            if (!isDashing && dashCooldownTimer <= 0)
            {
                StartCoroutine(Dash());
            }
            
        }

        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();

        // limit the player to not leave the screen
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
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

    public void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

    // init the bounding vectors that will limit the player to not leave the screen
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
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
