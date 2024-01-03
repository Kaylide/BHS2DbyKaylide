using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetHead : MonoBehaviour
{
    private GameObject _ball;

    // Use this for initialization
    void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void Update()
    {
        Debug.Log("neeeeeeeeeeeeeeeeeettttttt" + gameObject);


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!GameController.Instance.Scored)
        {
            if (col.gameObject.tag == "Ball")
            {
                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.ballKick.Play();
                }
                if (AIPlayer.instance.isJump == true)
                {
                    AIPlayer.instance.anim.SetBool(AIPlayer.instance.headShoot, true);

                    _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    if (Loadding.leftOrRight == 0)
                    {
                        if (_ball.GetComponent<Transform>().position.x > 0)
                        {
                            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(350, 0));

                        }
                        else
                        {
                            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(AIPlayer.instance.x_dir_Head * Ball.instance.dirAITarget, AIPlayer.instance.y_dir_Head));
                        }
                    }
                    else
                    {
                        if (_ball.GetComponent<Transform>().position.x < 0)
                        {
                            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-350, 0));

                        }
                        else
                        {
                            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-AIPlayer.instance.x_dir_Head * Ball.instance.dirAITarget, AIPlayer.instance.y_dir_Head));
                        }
                    }
                }
            }
        }
    }
}
