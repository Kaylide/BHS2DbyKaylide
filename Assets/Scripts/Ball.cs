using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float angleOrientBall, dirPlayerTarget, dirAITarget;
    public static Ball instance;
    public Vector2 vtor1;
    public GameObject _effect;
    private GameObject player, _AIPlayer;
    public Rigidbody2D rigid2D;
    public GameObject particSystem;
    public bool isUp, isDown, isColBody;
    public GameObject Goal_Effect;
    public Transform goalPos;

    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {


        rigid2D.AddForce(new Vector2(0, 150));
        player = GameObject.FindGameObjectWithTag("Player");
        _AIPlayer = GameObject.FindGameObjectWithTag("AIPlayer");
        GetComponent<SpriteRenderer>().sprite = SelectTeam.instance.sp_ball[PlayerPrefs.GetInt("indexBall", 0)];
    }
    private void FixedUpdate()
    {

    }
    private void Update()
    {
        if (Loadding.leftOrRight == 0)
        {
            dirPlayerTarget = Mathf.Abs(transform.position.x - GameController.Instance.targetLeft.transform.position.x);
            dirAITarget = Mathf.Abs(transform.position.x - GameController.Instance.targetRight.transform.position.x);
        }
        else
        {
            dirPlayerTarget = Mathf.Abs(transform.position.x - GameController.Instance.targetRight.transform.position.x);
            dirAITarget = Mathf.Abs(transform.position.x - GameController.Instance.targetLeft.transform.position.x);
        }

       
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Head1")
        {
            if (!GameController.Instance.Scored)
            {
                rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0);
                Instantiate(_effect, transform.position, transform.rotation);


            }
        }
        if (col.gameObject.tag == "Head2")
        {
            if (!GameController.Instance.Scored)
            {
                rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0);
                Instantiate(_effect, transform.position, transform.rotation);


            }

        }

        if (col.gameObject.tag == "UpCol")
        {
            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.ballHit.Play();
            }
        }

        if (col.gameObject.tag == "Wall")
        {
            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.ballHit.Play();
            }
        }
        if (col.gameObject.tag == "Body2" && col.gameObject.tag == "Body1")
        {
            isColBody = true;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "CheckHead")
        {
            AIPlayer.instance.canHead = true;
        }

        if (col.tag == "BehindCol")
        {
            rigid2D.velocity = new Vector2(0, 0);
            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.ballHit.Play();
            }
        }
        if (!GameController.Instance.Scored && !GameController.Instance.EndMatch)
        {
            if (col.tag == "RightNet")
            {
                

                Instantiate(Goal_Effect, goalPos.position, Quaternion.identity);

                if (Loadding.leftOrRight == 0)
                {
                    GameController.Instance.ScoredAgainst(false);
                }
                else
                {
                    GameController.Instance.ScoredAgainst(true);
                }

                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.goal1.Play();
                    SoundManager.Instance.goal2.Play();
                }
            }
            if (col.tag == "LeftNet")
            {
                
                Instantiate(Goal_Effect, goalPos.position, Quaternion.identity);

                if (Loadding.leftOrRight == 0)
                {
                    GameController.Instance.ScoredAgainst(true);
                }
                else
                {
                    GameController.Instance.ScoredAgainst(false);
                }

                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.goal1.Play();
                    SoundManager.Instance.goal2.Play();
                }
            }

           

            if (col.gameObject.tag == "Player")
            {

                Character2DController.instance.canShoot = true;
                //GameObject _orientBall = Instantiate(orientBall, transform.position, Quaternion.identity);

            }

            if (col.gameObject.tag == "Shoot2")
            {
                if (Character2DController.instance.canShoot == true && transform.position.y < 1.75f 
                    && AIPlayer.instance.grounded == true && Character2DController.instance.grounded == true && isColBody == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 1, 0), 60 * Time.deltaTime);
                    _AIPlayer.GetComponent<AIPlayer>().canShoot = false;

                }
                else
                {
                    _AIPlayer.GetComponent<AIPlayer>().canShoot = true;
                }

            }
        }

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "CheckHead")
        {
            AIPlayer.instance.canHead = false;
        }


        if (col.gameObject.tag == "Player")
        {
            
            Character2DController.instance.canShoot = false;
        }


        if (col.gameObject.tag == "Shoot2")
        {
            _AIPlayer.GetComponent<AIPlayer>().canShoot = false;
        }
    }
}
