using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    #region Varibles
    [SerializeField]
    private float speed;

    protected Vector2 direction;
    private Animator animator;
    #endregion

    #region Unity methods

    void Start()
    {
        direction = Vector2.down;
        animator = GetComponent<Animator>();
    }

     void Update()
    {
        GetInput();
        Animate(direction);
        Move();
    }


    public void Move()
    {

        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void GetInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }

    }

    public void Animate(Vector2 dir)
    {
        animator.SetFloat("x", dir.x);
        animator.SetFloat("y", dir.x);
    }
    #endregion
}
