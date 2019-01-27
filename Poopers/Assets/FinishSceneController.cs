using Assets;
using UnityEngine;

public class FinishSceneController : MonoBehaviour
{
    void Start()
    {
        var players = GameObject.FindGameObjectsWithTag(Tags.Player);
        foreach (var p in players)
        {
            p.SetActive(false);
        }
    }
}
