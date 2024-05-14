using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    GameObject player;
    public GameObject zombie;
    public float speed = 5f;
    public Animator anim;
    public int HPplayer = 1000;
    public int HPzombie = 3000;
    float dist;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        dist = Vector3.Distance(player.transform.position, this.transform.position);
        Animation();
    }

    void Move() {
        if (player == null)
            return;
        transform.LookAt(player.transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Attack() {
        HPplayer--;
        if (HPplayer <= 0)
            EndGame();
    }

    void Blood() {
        HPzombie--;
        if (HPzombie <= 0) {
            anim.SetTrigger("Dead");
            zombie.SetActive(false);
        }
    }

    void EndGame() {
        Time.timeScale = 0;
    }

    void Animation() {
        if (dist <= 30f) {
            speed = 10f;
            Blood();
            anim.SetBool("Run", true);
            if (dist <= 3f)
            {
                anim.SetTrigger("Attack");
                Attack();
            } 
            else
                Move(); 
        } 
        else
        {
            Move();
            speed = 5f;
            anim.SetBool("Run", false);
        }
    }
}