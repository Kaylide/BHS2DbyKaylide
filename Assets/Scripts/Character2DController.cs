using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character2DController : MonoBehaviour
{
    public static Character2DController instance;

    public GameObject btnLeft, btnRight, btnJump, btnShoot, btnLowShoot;
    private float horizontalAxis = 0.0f;

    public float _velocity;

    private Rigidbody2D _rigidbody;

    public float _jumpForce;

    public bool canShoot, isJump;

    private GameObject _ball;

    public Transform checkGround;

    public bool grounded, isHeadShoot;

    public LayerMask ground_layers;


    public Animator _animator;
    public float addForce_x, addForce_y, addForceHead_x;
    public int
        celebrateHash,
    headShootHash;

    public Animator Anim { get { return _animator; } }
    public int moveHash;

    public int CelebrateHash { get { return celebrateHash; } }

    private bool isPointerDown = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start()
    {
        isHeadShoot = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        canShoot = false;
        _ball = GameObject.FindGameObjectWithTag("Ball");
        //Application.targetFrameRate = 60;
        grounded = false;
        celebrateHash = Animator.StringToHash("Celebrate");
        headShootHash = Animator.StringToHash("HeadShoot");
        moveHash = Animator.StringToHash("Move");
        int id = PlayerPrefs.GetInt("IDPlayer");

        addForce_x = PlayerPrefs.GetFloat("Shoot_x" + id);
        addForce_y = PlayerPrefs.GetFloat("Shoot_y" + id);
        _jumpForce = PlayerPrefs.GetFloat("Jump" + id);
        _velocity = PlayerPrefs.GetFloat("Speed" + id);
        addForceHead_x = PlayerPrefs.GetFloat("addForceHead_x" + id);

        //switch (Loadding.topPlayer)
        //{
        //    case 6:
        //        _velocity = 350f;
        //        _jumpForce = 7f;
        //        addForce_x = 41;
        //        addForce_y = 360;
        //        break;
        //    case 5:
        //        _velocity = 355;
        //        _jumpForce = 7.25f;
        //        addForce_x = 42;
        //        addForce_y = 365;
        //        break;
        //    case 4:
        //        _velocity = 360f;
        //        _jumpForce = 7.5f;
        //        addForce_x = 43;
        //        addForce_y = 370;
        //        break;
        //    case 3:
        //        _velocity = 365;
        //        _jumpForce = 7.75f;
        //        addForce_x = 44;
        //        addForce_y = 375;
        //        break;
        //    case 2:
        //        _velocity = 370f;
        //        _jumpForce = 8f;
        //        addForce_x = 45;
        //        addForce_y = 380;
        //        break;
        //    case 1:
        //        _velocity = 375f;
        //        _jumpForce = 8.25f;
        //        addForce_x = 46;
        //        addForce_y = 385;
        //        break;
        //}

        if (Loadding.leftOrRight == 0)
        {
            transform.position = new Vector2(5f, -2.908148f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));


        }
        else
        {
            transform.position = new Vector2(-5f, -2.908148f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));


        }
    }

    private void Update()
    {

        if (!GameController.Instance.Scored && !GameController.Instance.EndMatch)
        {
            if (!grounded)
            {
                isJump = true;
                _animator.SetBool(moveHash, false);
            }
            else
            {

                isJump = false;
                _animator.SetBool(headShootHash, false);

            }
        }

        if (grounded == true)
        {
            _rigidbody.gravityScale = 2f;
        }

        else
        {
            if (_rigidbody.velocity.y >= 0)
            {
                _rigidbody.gravityScale = 6.6f;
            }
            else
            {
                _rigidbody.gravityScale = 8.8f;
            }

        }

        // Bổ sung đoạn code sau để nhận giá trị từ các nút bàn phím
        horizontalAxis = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LowShoot();
        }

    }
    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(horizontalAxis * Time.deltaTime * _velocity, _rigidbody.velocity.y);
        grounded = Physics2D.OverlapCircle(checkGround.position, 0.25f, ground_layers);
    }

    public void Move(int value)
    {
        if (!GameController.Instance.Scored && !GameController.Instance.EndMatch)
        {
            if (isJump == false)
            {
                _animator.SetBool(moveHash, true);

            }
            horizontalAxis = value;
            if (value == 1)
            {
                btnRight.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
                btnRight.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
            }
            else
            {
                btnLeft.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
                btnLeft.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
            }
        }
    }

    public void StopMoveLeft()
    {
        btnLeft.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        _animator.SetBool(moveHash, false);
        horizontalAxis = 0.0f;
        btnLeft.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);

    }
    public void StopMoveRigh()
    {
        _animator.SetBool(moveHash, false);
        horizontalAxis = 0.0f;
        btnRight.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        btnRight.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);
    }

    public void StopJump()
    {
        btnJump.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        btnJump.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);
    }
    public void StopShoot()
    {
        btnShoot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        btnShoot.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);


    }

    public void StopLowShoot()
    {

        btnLowShoot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        btnLowShoot.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);
    }
    public void Jump()
    {
        isJump = true;
        btnJump.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
        btnJump.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
        if (!GameController.Instance.Scored && !GameController.Instance.EndMatch)
        {

            if (grounded)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 2f * _jumpForce);

            }
        }

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.jump.Play();
        }
    }



    public void Shoot()
    {
        btnShoot.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
        btnShoot.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
        if (!GameController.Instance.Scored && !GameController.Instance.EndMatch)
        {
            _animator.SetTrigger("Shoot");
            if (canShoot)
            {
                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.ballKick.Play();
                }
                _animator.SetBool(moveHash, false);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                if (Loadding.leftOrRight == 0)
                {
                    _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-addForce_x * Ball.instance.dirPlayerTarget, addForce_y));
                }
                else
                {
                    _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(addForce_x * Ball.instance.dirPlayerTarget, addForce_y));
                }
            }
        }
    }

    public void LowShoot()
    {
        btnLowShoot.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
        btnLowShoot.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
        if (!GameController.Instance.Scored && !GameController.Instance.EndMatch)
        {
            _animator.SetTrigger("Shoot");
            if (canShoot)
            {
                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.ballKick.Play();
                }
                _animator.SetBool(moveHash, false);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                if (Loadding.leftOrRight == 0)
                {
                    _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-addForce_x * Ball.instance.dirPlayerTarget, 0));
                }
                else
                {
                    _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(addForce_x * Ball.instance.dirPlayerTarget, 0));
                }
            }
        }
    }

    void OnDisable()
    {
        _animator.SetBool(headShootHash, false);
    }


}
