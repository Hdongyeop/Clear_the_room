using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public bool canDoubleJump;
    public bool endingAble;
}

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    [SerializeField] private int _directionX;

    public PlayerData playerData;
    public Transform mainPos;
    public Transform respawnPos;
    public Itemmanager itemManager;
    public Sprite[] sprites;
    public AudioClip[] audios;
    public float speed;
    public float maxSpeedX;
    public float maxSpeedY;
    public float jumpPower;
    public int curJumpCnt;
    public int maxJumpCnt;
    public bool isRoping;
    public bool isJumping;
    
    private void Awake()
    {
        // Reference
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        if(GameObject.FindWithTag("MainPos"))
            mainPos = GameObject.FindWithTag("MainPos").transform;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 0.6f, Color.red);
        StopMove();
        Jump();
        FallingDeath();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RaycastHit2D rayhit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("Floor"));

        if (rayhit)
        {
            _rigid.velocity = Vector2.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {           
        RaycastHit2D rayhit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("Floor"));
        
        if(rayhit)
        {
            isJumping = false;
            maxSpeedY = 15f;
            _spriteRenderer.sprite = sprites[0];
            curJumpCnt = 0;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            var item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.TYPE.Coin:
                    itemManager.data.curCoin += 1;
                    break;
                case Item.TYPE.Diamond:
                    itemManager.data.curDia += 1;
                    break;
                case Item.TYPE.Thing:
                    break;
            }
        }
    }
    
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0)
        {
            // 방향
            _directionX = (int) h;
            // 움직임
            _rigid.AddForce(Vector2.right * h * speed, ForceMode2D.Impulse);
            // 방향 전환
            if(h > 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else if(h < 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        // 최대속도(X)
        if (_rigid.velocity.x > maxSpeedX)
            _rigid.velocity = new Vector2(maxSpeedX, _rigid.velocity.y);
        else if (_rigid.velocity.x < -1f * maxSpeedX)
            _rigid.velocity = new Vector2(-1f * maxSpeedX, _rigid.velocity.y);
        
        // 최대속도(Y)
        if (_rigid.velocity.y > maxSpeedY)
            _rigid.velocity = new Vector2(_rigid.velocity.x, maxSpeedY);
        else if (_rigid.velocity.y < -1f * maxSpeedY)
            _rigid.velocity = new Vector2(_rigid.velocity.x, -1f * maxSpeedY);
    }
    
    void StopMove()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            _rigid.velocity = new Vector2(0f, _rigid.velocity.y);
        }
    }

    void Jump() // 점프했을 때
    {
        if (Input.GetButtonDown("Jump") && (curJumpCnt < maxJumpCnt))
        {
            isJumping = true;
            curJumpCnt++;
            if(jumpPower != 0)
                AudioPlay(0);
            _rigid.velocity = new Vector2(_rigid.velocity.x, 0f);
            _rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            _spriteRenderer.sprite = sprites[1];
            
            // 로프에 매달린 상태일 때
            if (isRoping)
            {
                _rigid.gravityScale = 5f;
                isRoping = false;
            }
        }
    }

    void AudioPlay(int n)
    {
        _audioSource.clip = audios[n];
        _audioSource.Play();
    }

    public void Respawn()
    {
        transform.position = respawnPos.position;
        _rigid.velocity = Vector2.zero;
    }

    void FallingDeath()
    {
        if (transform.position.y < -20)
        {
            Respawn();
        }
    }
    
    public void PlayerDataSave()
    {
        File.WriteAllText(Application.dataPath + "/PlayerData.json", JsonUtility.ToJson(playerData));
    }

    public void PlayerDataLoad()
    {
        string loadData = File.ReadAllText(Application.dataPath + "/PlayerData.json");
        PlayerData mData = JsonUtility.FromJson<PlayerData>(loadData);
        playerData = mData;
    }

    public PlayerData PlayerDataReturn()
    {
        string loadData = File.ReadAllText(Application.dataPath + "/PlayerData.json");
        PlayerData mData = JsonUtility.FromJson<PlayerData>(loadData);
        return mData;
    }
    
}
