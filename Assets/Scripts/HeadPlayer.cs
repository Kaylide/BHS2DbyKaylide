using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPlayer : MonoBehaviour
{

    private GameObject _ball;
    // Use this for initialization
    void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
    }


    // Update is called once per frame
    void Update()
    {

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
                if (Character2DController.instance.isJump == true)
                {
                    _ball.GetComponent<Rigidbody2D>().velocity = new Vector2((_ball.GetComponent<Rigidbody2D>().velocity.x * Character2DController.instance.addForceHead_x)
                                                                             /200, 
                                                                             (_ball.GetComponent<Rigidbody2D>().velocity.y * Character2DController.instance.addForceHead_x)
                                                                             / 200);
                    if (_ball.transform.position.y >= -1.5f)
                    {
                        Character2DController.instance._animator.SetBool(Character2DController.instance.headShootHash, true);
                    }
                    if (Loadding.leftOrRight == 0)
                    {
                        if (_ball.GetComponent<Transform>().position.x < 0)
                        {
                            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Character2DController.instance.addForceHead_x, 0));
                        }
                        else
                        {
                            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Character2DController.instance.addForceHead_x, 170));
                        }
                    }
                    else
                    {
                        if (_ball.GetComponent<Transform>().position.x > 0)
                        {
                            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(Character2DController.instance.addForceHead_x, 0));

                        }
                        else
                        {
                            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(Character2DController.instance.addForceHead_x, 170));
                        }
                    }

                }
            }
        }
    }
}