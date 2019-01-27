using System;
using Assets;
using Assets.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public uint positionDelta;
    private PositionState position;

    void Start()
    {
        GameManager.Instance.OnGameOver += OnGameOver;
        GameManager.Instance.OnRestartGame += OnRestartGame;
        animator = GetComponent<Animator>();
        position = PositionState.Center;
    }

    private void OnRestartGame(object sender, EventArgs e)
    {
        animator.SetTrigger("RestartGame");
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        animator.SetTrigger("GameOver");
    }

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && (position == PositionState.Center || position == PositionState.Right))
        {
            position = position == PositionState.Center ? PositionState.Left : PositionState.Center;
            transform.position = new Vector2(transform.position.x - positionDelta, transform.position.y);
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && (position == PositionState.Center || position == PositionState.Left))
        {
            position = position == PositionState.Center ? PositionState.Right : PositionState.Center;
            transform.position = new Vector2(transform.position.x + positionDelta, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.Obstacle)
        {
            GameManager.Instance.EndGame();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == Tags.Collectable)
        {
            GameManager.Instance.AddScore(1);
            Destroy(collision.gameObject);
        }
    }
}
