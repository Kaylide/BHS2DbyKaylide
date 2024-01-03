using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public static AIPlayer instance;
    private GameObject _ball;
    public int topAI;
    private GameObject _player, _AI;
    private Rigidbody2D _rigidbody;
    public float _velocity;
    public float rangeOfDefense;
    public Transform defensePos;
    public bool canShoot;
    public Transform checkGround, checkHead;
    public bool grounded, canHead, isJump;
    public LayerMask ground_layers, ball_layer;
    public float _jumpForce;
    public Animator anim;
    public int CelebrateHash { get; set; }
    public int moveHash, headShoot;
    public int teamAI;
    public float x_dir_Head, y_dir_Head, x_dir_Shoot, y_dir_Shoot;

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
        //Application.targetFrameRate = 60;
        CelebrateHash = Animator.StringToHash("Celebrate");
        moveHash = Animator.StringToHash("Move");
        headShoot = Animator.StringToHash("HeadShoot");
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _AI = GameObject.FindGameObjectWithTag("AIPlayer");
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");

        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            teamAI = PlayerPrefs.GetInt("wcAI");
        }
        else
        {
            teamAI = PlayerPrefs.GetInt("teamAI");
        }
        if (Loadding.leftOrRight == 0)
        {
            transform.position = new Vector2(-5f, -2.908148f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            switch (teamAI)
            {
                case 1:
                    x_dir_Head = 30;
                    y_dir_Head = 375;

                    _velocity = 395f;
                    _jumpForce = 7.5f;
                    defensePos.position = new Vector2(-7.25f, 0);
                    break;
                case 2:

                    x_dir_Head = 22;
                    y_dir_Head = 350;

                    _velocity = 340f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(-6.25f, 0);
                    break;
                case 3:
                    x_dir_Head = 30;
                    y_dir_Head = 375;

                    _velocity = 400f;
                    _jumpForce = 8.5f;
                    defensePos.position = new Vector2(-7.5f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 4:
                    x_dir_Head = 29;
                    y_dir_Head = 370;

                    _velocity = 395f;
                    _jumpForce = 8.25f;
                    defensePos.position = new Vector2(-7.25f, 0);
                    break;
                case 5:
                    x_dir_Head = 27;
                    y_dir_Head = 360;

                    _velocity = 380f;
                    _jumpForce = 7.5f;
                    defensePos.position = new Vector2(-7f, 0);

                    break;
                case 6:
                    x_dir_Head = 27;
                    y_dir_Head = 360;

                    _velocity = 385f;
                    _jumpForce = 7.65f;
                    defensePos.position = new Vector2(-7f, 0);
                    break;
                case 7:
                    x_dir_Head = 23;
                    y_dir_Head = 355;

                    _velocity = 345f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(-6.25f, 0);
                    break;
                case 8:
                    x_dir_Head = 27;
                    y_dir_Head = 360;

                    _velocity = 370f;
                    _jumpForce = 6.8f;
                    defensePos.position = new Vector2(-6.5f, 0);
                    break;
                case 9:
                    x_dir_Head = 28;
                    y_dir_Head = 370;

                    _velocity = 390f;
                    _jumpForce = 7.65f;
                    defensePos.position = new Vector2(-7.25f, 0);
                    break;
                case 10:
                    x_dir_Head = 27;
                    y_dir_Head = 360;

                    _velocity = 365f;
                    _jumpForce = 6.75f;
                    defensePos.position = new Vector2(-6.75f, 0);
                    break;
                case 11:
                    x_dir_Head = 30;
                    y_dir_Head = 375;

                    _velocity = 400f;
                    _jumpForce = 8.5f;
                    defensePos.position = new Vector2(-7.5f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 12:
                    x_dir_Head = 30;
                    y_dir_Head = 375;

                    _velocity = 400f;
                    _jumpForce = 8.5f;
                    defensePos.position = new Vector2(-7.5f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);

                    break;
                case 13:
                    x_dir_Head = 23;
                    y_dir_Head = 355;

                    _velocity = 345f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(-6.25f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 14:
                    x_dir_Head = 25;
                    y_dir_Head = 355;

                    _velocity = 377.5f;
                    _jumpForce = 7.25f;
                    defensePos.position = new Vector2(-6.75f, 0);
                    break;
                case 15:
                    x_dir_Head = 24;
                    y_dir_Head = 355;

                    _velocity = 365f;
                    _jumpForce = 6.75f;
                    defensePos.position = new Vector2(-6.5f, 0);
                    break;
                case 16:
                    x_dir_Head = 24;
                    y_dir_Head = 350;

                    _velocity = 368f;
                    _jumpForce = 6.75f;
                    defensePos.position = new Vector2(-6.5f, 0);
                    break;
                case 17:
                    x_dir_Head = 22;
                    y_dir_Head = 350;

                    _velocity = 325f;
                    _jumpForce = 6f;
                    defensePos.position = new Vector2(-6f, 0);

                    break;
                case 18:
                    x_dir_Head = 25;
                    y_dir_Head = 350;

                    _velocity = 360f;
                    _jumpForce = 6.75f;
                    defensePos.position = new Vector2(-6.5f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 19:
                    x_dir_Head = 23;
                    y_dir_Head = 355;

                    _velocity = 330f;
                    _jumpForce = 6.25f;
                    defensePos.position = new Vector2(-6f, 0);
                    break;
                case 20:
                    x_dir_Head = 23;
                    y_dir_Head = 355;


                    _velocity = 350f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(-6.25f, 0);
                    break;
                case 21:
                    x_dir_Head = 25;
                    y_dir_Head = 365;

                    _velocity = 375f;
                    _jumpForce = 7f;
                    defensePos.position = new Vector2(-6.5f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 22:
                    x_dir_Head = 24;
                    y_dir_Head = 355;

                    _velocity = 350f;
                    _jumpForce = 6.6f;
                    defensePos.position = new Vector2(-6.5f, 0);
                    break;
                case 23:
                    x_dir_Head = 24;
                    y_dir_Head = 355;

                    _velocity = 355f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(-6.5f, 0);
                    break;
                case 24:
                    x_dir_Head = 27;
                    y_dir_Head = 370;

                    _velocity = 375f;
                    _jumpForce = 7.25f;
                    defensePos.position = new Vector2(-7f, 0);
                    break;
                case 25:
                    x_dir_Head = 29;
                    y_dir_Head = 375;

                    _velocity = 390f;
                    _jumpForce = 8f;
                    defensePos.position = new Vector2(-7.25f, 0);
                    break;
                case 26:
                    x_dir_Head = 27;
                    y_dir_Head = 370;

                    _velocity = 370f;
                    _jumpForce = 6.85f;
                    defensePos.position = new Vector2(-6.75f, 0);
                    break;
                case 27:
                    x_dir_Head = 29;
                    y_dir_Head = 370;

                    _velocity = 390f;
                    _jumpForce = 7.75f;
                    defensePos.position = new Vector2(-7.25f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 28:
                    x_dir_Head = 28;
                    y_dir_Head = 365;

                    _velocity = 375f;
                    _jumpForce = 7f;
                    defensePos.position = new Vector2(-6.75f, 0);
                    break;
                case 29:
                    x_dir_Head = 28;
                    y_dir_Head = 370;

                    _velocity = 380f;
                    _jumpForce = 7.5f;
                    defensePos.position = new Vector2(-6.75f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 30:
                    x_dir_Head = 25;
                    y_dir_Head = 355;

                    _velocity = 335f;
                    _jumpForce = 6.25f;
                    defensePos.position = new Vector2(-6f, 0);
                    break;
                case 31:
                    x_dir_Head = 29;
                    y_dir_Head = 375;

                    _velocity = 390f;
                    _jumpForce = 7.75f;
                    defensePos.position = new Vector2(-7f, 0);
                    break;
                case 32:
                    x_dir_Head = 23;
                    y_dir_Head = 365;

                    _velocity = 340f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(-6.25f, 0);
                    break;
                    //case 8:
                    //    _velocity = 340f;
                    //    _jumpForce = 6f;
                    //    defensePos.position = new Vector2(-6f, 0);
                    //    break;
                    //case 7:
                    //    _velocity = 345f;
                    //    _jumpForce = 6.25f;
                    //    defensePos.position = new Vector2(-6f, 0);
                    //    break;
                    //case 6:
                    //    _velocity = 350f;
                    //    _jumpForce = 6.5f;
                    //    defensePos.position = new Vector2(-6f, 0);
                    //    break;
                    //case 5:
                    //    _velocity = 355;
                    //    _jumpForce = 6.75f;
                    //    defensePos.position = new Vector2(-6.25f, 0);
                    //    break;
                    //case 4:
                    //    _velocity = 360f;
                    //    _jumpForce = 7f;
                    //    defensePos.position = new Vector2(-6.5f, 0);
                    //    break;
                    //case 3:
                    //    _velocity = 365;
                    //    _jumpForce = 7.5f;
                    //    defensePos.position = new Vector2(-7f, 0);
                    //    break;
                    //case 2:
                    //    _velocity = 370;
                    //    _jumpForce = 8f;
                    //    defensePos.position = new Vector2(-7.25f, 0);
                    //    break;
                    //case 1:
                    //_velocity = 375;
                    //_jumpForce = 8.5f;
                    //defensePos.position = new Vector2(-7.5f, 0);
                    //break;
            }
        }
        else
        {

            transform.position = new Vector2(5f, -2.908148f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

            switch (teamAI)
            {
                case 1:
                    x_dir_Head = 30;
                    y_dir_Head = 375;

                    _velocity = 395f;
                    _jumpForce = 7.5f;
                    defensePos.position = new Vector2(7.25f, 0);
                    break;
                case 2:

                    x_dir_Head = 22;
                    y_dir_Head = 350;

                    _velocity = 340f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(6.25f, 0);
                    break;
                case 3:
                    x_dir_Head = 30;
                    y_dir_Head = 375;

                    _velocity = 400f;
                    _jumpForce = 8.5f;
                    defensePos.position = new Vector2(7.5f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 4:
                    x_dir_Head = 29;
                    y_dir_Head = 370;

                    _velocity = 395f;
                    _jumpForce = 8.25f;
                    defensePos.position = new Vector2(7.25f, 0);
                    break;
                case 5:
                    x_dir_Head = 27;
                    y_dir_Head = 360;

                    _velocity = 380f;
                    _jumpForce = 7.5f;
                    defensePos.position = new Vector2(7f, 0);
                    break;
                case 6:
                    x_dir_Head = 27;
                    y_dir_Head = 360;

                    _velocity = 385f;
                    _jumpForce = 7.65f;
                    defensePos.position = new Vector2(7f, 0);
                    break;
                case 7:
                    x_dir_Head = 23;
                    y_dir_Head = 355;

                    _velocity = 345f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(6.25f, 0);
                    break;
                case 8:
                    x_dir_Head = 27;
                    y_dir_Head = 360;

                    _velocity = 370f;
                    _jumpForce = 6.8f;
                    defensePos.position = new Vector2(6.5f, 0);
                    break;
                case 9:
                    x_dir_Head = 28;
                    y_dir_Head = 370;

                    _velocity = 390f;
                    _jumpForce = 7.65f;
                    defensePos.position = new Vector2(7.25f, 0);
                    break;
                case 10:
                    x_dir_Head = 27;
                    y_dir_Head = 360;

                    _velocity = 365f;
                    _jumpForce = 6.75f;
                    defensePos.position = new Vector2(6.75f, 0);
                    break;
                case 11:
                    x_dir_Head = 30;
                    y_dir_Head = 375;

                    _velocity = 400f;
                    _jumpForce = 8.5f;
                    defensePos.position = new Vector2(7.5f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 12:
                    x_dir_Head = 30;
                    y_dir_Head = 375;

                    _velocity = 400f;
                    _jumpForce = 8.5f;
                    defensePos.position = new Vector2(7.5f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 13:
                    x_dir_Head = 23;
                    y_dir_Head = 355;

                    _velocity = 345f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(6.25f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 14:
                    x_dir_Head = 25;
                    y_dir_Head = 355;

                    _velocity = 377.5f;
                    _jumpForce = 7.25f;
                    defensePos.position = new Vector2(6.75f, 0);
                    break;
                case 15:
                    x_dir_Head = 24;
                    y_dir_Head = 355;

                    _velocity = 365f;
                    _jumpForce = 6.75f;
                    defensePos.position = new Vector2(6.5f, 0);
                    break;
                case 16:
                    x_dir_Head = 24;
                    y_dir_Head = 350;

                    _velocity = 368f;
                    _jumpForce = 6.75f;
                    defensePos.position = new Vector2(6.5f, 0);
                    break;
                case 17:
                    x_dir_Head = 22;
                    y_dir_Head = 350;

                    _velocity = 325f;
                    _jumpForce = 6f;
                    defensePos.position = new Vector2(6f, 0);

                    break;
                case 18:
                    x_dir_Head = 25;
                    y_dir_Head = 350;

                    _velocity = 360f;
                    _jumpForce = 6.75f;
                    defensePos.position = new Vector2(6.5f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 19:
                    x_dir_Head = 23;
                    y_dir_Head = 355;

                    _velocity = 330f;
                    _jumpForce = 6.25f;
                    defensePos.position = new Vector2(6f, 0);
                    break;
                case 20:
                    x_dir_Head = 23;
                    y_dir_Head = 355;


                    _velocity = 350f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(6.25f, 0);
                    break;
                case 21:
                    x_dir_Head = 25;
                    y_dir_Head = 365;

                    _velocity = 375f;
                    _jumpForce = 7f;
                    defensePos.position = new Vector2(6.5f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 22:
                    x_dir_Head = 24;
                    y_dir_Head = 355;

                    _velocity = 350f;
                    _jumpForce = 6.6f;
                    defensePos.position = new Vector2(6.5f, 0);
                    break;
                case 23:
                    x_dir_Head = 24;
                    y_dir_Head = 355;

                    _velocity = 355f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(6.5f, 0);
                    break;
                case 24:
                    x_dir_Head = 27;
                    y_dir_Head = 370;

                    _velocity = 375f;
                    _jumpForce = 7.25f;
                    defensePos.position = new Vector2(7f, 0);
                    break;
                case 25:
                    x_dir_Head = 29;
                    y_dir_Head = 375;

                    _velocity = 390f;
                    _jumpForce = 8f;
                    defensePos.position = new Vector2(7.25f, 0);
                    break;
                case 26:
                    x_dir_Head = 27;
                    y_dir_Head = 370;

                    _velocity = 370f;
                    _jumpForce = 6.85f;
                    defensePos.position = new Vector2(6.75f, 0);
                    break;
                case 27:
                    x_dir_Head = 29;
                    y_dir_Head = 370;

                    _velocity = 390f;
                    _jumpForce = 7.75f;
                    defensePos.position = new Vector2(7.25f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 28:
                    x_dir_Head = 28;
                    y_dir_Head = 365;

                    _velocity = 375f;
                    _jumpForce = 7f;
                    defensePos.position = new Vector2(6.75f, 0);
                    break;
                case 29:
                    x_dir_Head = 28;
                    y_dir_Head = 370;

                    _velocity = 380f;
                    _jumpForce = 7.5f;
                    defensePos.position = new Vector2(6.75f, 0);
                    transform.localScale = new Vector3(-1.2f, 1.2f, 1);
                    break;
                case 30:
                    x_dir_Head = 25;
                    y_dir_Head = 355;

                    _velocity = 335f;
                    _jumpForce = 6.25f;
                    defensePos.position = new Vector2(6f, 0);
                    break;
                case 31:
                    x_dir_Head = 29;
                    y_dir_Head = 375;

                    _velocity = 390f;
                    _jumpForce = 7.75f;
                    defensePos.position = new Vector2(7f, 0);
                    break;
                case 32:
                    x_dir_Head = 23;
                    y_dir_Head = 365;

                    _velocity = 340f;
                    _jumpForce = 6.5f;
                    defensePos.position = new Vector2(6.25f, 0);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

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


        if (!GameController.Instance.Scored && !GameController.Instance.EndMatch)
        {
            if (!grounded)
            {
                isJump = true;
            }
            else
            {
                isJump = false;
                anim.SetBool(headShoot, false);
            }


            if (_rigidbody.velocity.x != 0 && grounded)
            {
                if (isJump == false)
                {
                    anim.SetBool(moveHash, true);

                }
            }
            else
            {
                anim.SetBool(moveHash, false);
            }

            if (GameController.goalsConceded > GameController.goals)
            {
                switch (Loadding.topAI)
                {
                    case 8:
                        rangeOfDefense = 19f;
                        break;
                    case 7:
                        rangeOfDefense = 19f;
                        break;
                    case 6:
                        rangeOfDefense = 15f;
                        break;
                    case 5:
                        rangeOfDefense = 15f;
                        break;
                    case 4:
                        rangeOfDefense = 15f;
                        break;
                    case 3:
                        rangeOfDefense = 15f;
                        break;
                    case 2:
                        rangeOfDefense = 16f;
                        break;
                    case 1:
                        rangeOfDefense = 19f;
                        break;
                }

            }
            else
            {
                switch (Loadding.topAI)
                {
                    case 8:
                        rangeOfDefense = 19f;
                        break;
                    case 7:
                        rangeOfDefense = 19f;
                        break;
                    case 6:
                        rangeOfDefense = 18f;
                        break;
                    case 5:
                        rangeOfDefense = 17f;
                        break;
                    case 4:
                        rangeOfDefense = 16f;
                        break;
                    case 3:
                        rangeOfDefense = 17f;
                        break;
                    case 2:
                        rangeOfDefense = 18f;
                        break;
                    case 1:
                        rangeOfDefense = 19f;
                        break;
                }
            }

            if (Loadding.leftOrRight == 0)
            {
                if (Mathf.Abs(_ball.transform.position.x - transform.position.x)
                       < Mathf.Abs(_ball.transform.position.x - _player.transform.position.x)
                    && _ball.transform.position.x > transform.position.x
                    && _ball.transform.position.y < -1f)
                {
                    _rigidbody.velocity = new Vector2(_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                }

                else
                {
                    if (Mathf.Abs(_ball.transform.position.x - transform.position.x) <= rangeOfDefense)
                    {
                        float _directionMove = (_ball.transform.position.x > transform.position.x) ? 1f : -1f;
                        if (_ball.transform.position.y > -0.5f)
                        {
                            if (transform.position.x > defensePos.position.x)
                                _rigidbody.velocity = new Vector2(-_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                            else _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                        }
                        else
                        {
                            _rigidbody.velocity = new Vector2(_velocity * _directionMove * Time.deltaTime, _rigidbody.velocity.y);
                        }
                    }

                    else if (transform.position.x <= defensePos.position.x)
                    {
                        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                    }
                    else
                        _rigidbody.velocity = new Vector2(-_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                }

            }
            else
            {
                if (Mathf.Abs(_ball.transform.position.x - transform.position.x)
                       < Mathf.Abs(_ball.transform.position.x - _player.transform.position.x)
                    && _ball.transform.position.x < transform.position.x
                    && _ball.transform.position.y < -1f)
                {
                    _rigidbody.velocity = new Vector2(-_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                }

                else
                {
                    if (Mathf.Abs(_ball.transform.position.x - transform.position.x) <= rangeOfDefense)
                    {
                        float _directionMove = (_ball.transform.position.x < transform.position.x) ? 1f : -1f;
                        if (_ball.transform.position.y > -0.5f)
                        {
                            if (transform.position.x < defensePos.position.x)
                                _rigidbody.velocity = new Vector2(_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                            else _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                        }
                        else
                        {
                            _rigidbody.velocity = new Vector2(-_velocity * Time.deltaTime * _directionMove, _rigidbody.velocity.y);
                        }
                    }

                    else if (transform.position.x >= defensePos.position.x)
                    {
                        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                    }
                    else
                        _rigidbody.velocity = new Vector2(_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                }

            }


            if (canShoot)
            {
                Shoot();
            }


            if (canHead && _ball.transform.position.y > -0.65f && grounded)
            {
                Jump();

            }


        }

    }

    public void SetupDenfence()
    {

    }

    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(checkGround.position, 0.2f, ground_layers);

    }


    void Shoot()
    {
        if (!GameController.Instance.Scored && !GameController.Instance.EndMatch)
        {

            anim.SetTrigger("Shoot");

            if (canShoot)
            {
                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.ballKick.Play();
                }
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                anim.SetBool(moveHash, false);
                if (Loadding.leftOrRight == 0)
                {
                    if (Mathf.Abs(_player.transform.position.x - GameController.Instance.targetRight.transform.position.x)
                       > Mathf.Abs(_AI.transform.position.x - GameController.Instance.targetRight.transform.position.x)
                       || (Character2DController.instance.grounded == false && Character2DController.instance.checkGround.position.y >= _ball.transform.position.y))
                    {
                        _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(500, 0));
                    }
                    else
                    {
                        switch (Loadding.topAI)
                        {
                            case 8:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(33 * Ball.instance.dirAITarget, 335));
                                break;
                            case 7:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(34f * Ball.instance.dirAITarget, 340));
                                break;
                            case 6:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(35 * Ball.instance.dirAITarget, 345));
                                break;
                            case 5:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(36f * Ball.instance.dirAITarget, 350));
                                break;
                            case 4:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(37 * Ball.instance.dirAITarget, 360));
                                break;
                            case 3:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(38 * Ball.instance.dirAITarget, 370));
                                break;
                            case 2:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(39 * Ball.instance.dirAITarget, 380));
                                break;
                            case 1:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(40 * Ball.instance.dirAITarget, 400));
                                break;

                        }

                    }
                }
                else
                {
                    if (Mathf.Abs(_player.transform.position.x - GameController.Instance.targetLeft.transform.position.x)
                        > Mathf.Abs(_AI.transform.position.x - GameController.Instance.targetLeft.transform.position.x)
                        || (Character2DController.instance.grounded == false && Character2DController.instance.checkGround.position.y >= _ball.transform.position.y))
                    {
                        _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 0));
                    }
                    else
                    {
                        switch (Loadding.topAI)
                        {
                            case 8:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-33 * Ball.instance.dirAITarget, 335));
                                break;
                            case 7:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-34f * Ball.instance.dirAITarget, 340));
                                break;
                            case 6:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-35 * Ball.instance.dirAITarget, 345));
                                break;
                            case 5:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-36f * Ball.instance.dirAITarget, 350));
                                break;
                            case 4:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-37 * Ball.instance.dirAITarget, 360));
                                break;
                            case 3:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-38 * Ball.instance.dirAITarget, 370));
                                break;
                            case 2:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-39 * Ball.instance.dirAITarget, 380));
                                break;
                            case 1:
                                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-40 * Ball.instance.dirAITarget, 400));
                                break;
                        }
                    }

                }
            }
        }


    }



    void Jump()
    {

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.jump.Play();
        }
        isJump = true;
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 1.8f * _jumpForce);

    }


    void OnDisable()
    {
        anim.SetBool(headShoot, false);
    }


}
