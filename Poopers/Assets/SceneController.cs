using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private readonly System.Random random = new System.Random();
    private readonly List<GameObject> Obstacles = new List<GameObject>();
    public float LeftLanePositionX;
    public float CenterLaneLocationX;
    public float RightLaneLocationX;
    public float ObstacleGenerationPositionY;
    public float ObstacleGenerationTimeOffset;
    public float ObstacleGenerationRepeatRate;
    public GameObject Obstacle1;
    public GameObject Obstacle2;
    public GameObject Obstacle3;
    public GameObject Collectable;
    private GameObject ObstaclesObject;
    void Start()
    {
        ObstaclesObject = GameObject.FindGameObjectWithTag(Tags.Obstacle);
        Obstacles.Add(Obstacle1);
        Obstacles.Add(Obstacle2);
        Obstacles.Add(Obstacle3);
        InvokeRepeating("GenerateElements", ObstacleGenerationTimeOffset, ObstacleGenerationRepeatRate);
    }

    private void GenerateElements()
    {
        var elements = GenerateObstacles().Take(random.Next(0, 3)).ToList();
        elements.Add(Instantiate(Collectable));
        elements = elements.OrderBy(x => System.Guid.NewGuid().ToString()).ToList();
        elements[0].transform.position = new Vector2(LeftLanePositionX, ObstacleGenerationPositionY);
        elements[1].transform.position = new Vector2(CenterLaneLocationX, ObstacleGenerationPositionY);
        elements[2].transform.position = new Vector2(RightLaneLocationX, ObstacleGenerationPositionY);
        elements.ForEach(obj => obj.transform.parent = ObstaclesObject.transform.parent);
    }

    private IEnumerable<GameObject> GenerateObstacles()
    {
        while (true)
        {
            yield return Instantiate(GetRandomObstacle());
        }
    }

    private GameObject GetRandomObstacle()
    {
        return Obstacles[random.Next(0, Obstacles.Count)];
    }
}
