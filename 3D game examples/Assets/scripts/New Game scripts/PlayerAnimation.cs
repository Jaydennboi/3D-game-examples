using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player _characterMovement;
    private Animator _characterAnimator;
    // Start is called before the first frame update
    void Start()
    {
         _characterAnimator = GetComponent<Animator>();
        _characterMovement = GameObject.Find("player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerWalking();
        PlayerJumping();
    }

     public void PlayerJumping()
    {
        if(_characterMovement.IsPlayerOnGround())
        {
            _characterAnimator.SetBool("IsOnGround", true);
        }
        else
        {
            _characterAnimator.SetBool("IsOnGround", false);
        }
    }

     private void PlayerWalking()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            _characterAnimator.SetBool("IsWalking", true);
        
        }
        else
        {
            _characterAnimator.SetBool("IsWalking", false);
        }
    }
}
