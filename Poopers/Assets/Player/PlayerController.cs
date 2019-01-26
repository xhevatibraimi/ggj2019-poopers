using Assets.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public uint positionDelta;
    private PositionState position;

    void Start()
    {
        animator = GetComponent<Animator>();
        position = PositionState.Center;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("StartRun");
            animator.SetTrigger("StartRun");
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            Debug.Log("StopRun");
            animator.SetTrigger("StopRun");
        }
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
}
