using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

    public float shieldMaxHp = 40.0f;

    private bool _hasShieldUp = false;

    public bool HasShieldUp
    {
        get { return _hasShieldUp; }
        set { _hasShieldUp = value; }
    }

    Vector2 rawInput;

    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }
    private void Start()
    {
        InitBounds();
    }

    void Update()
    {
        PlayerMove();
        if (_hasShieldUp == true)

        {
            ShieldsUp();
        }
    }

    void PlayerMove()
    {
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();

        // limit the player to not leave the screen
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
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
