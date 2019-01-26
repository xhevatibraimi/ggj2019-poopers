using Assets;
using System;
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
    public float ObstacleLowerBound;
    public float CollidablesVelocity;

    public GameObject Obstacle1;
    public GameObject Obstacle2;
    public GameObject Obstacle3;
    public GameObject Collectable;
    private GameObject ObstaclesObject;

    private void Start()
    {
        ObstaclesObject = GameObject.FindGameObjectWithTag(Tags.Obstacles);
        Obstacles.Add(Obstacle1);
        Obstacles.Add(Obstacle2);
        Obstacles.Add(Obstacle3);
        InvokeRepeating("GenerateCollidables", ObstacleGenerationTimeOffset, ObstacleGenerationRepeatRate);
    }

    private void Update()
    {
        DestroyUnncessaryCollidables();
    }

    private void DestroyUnncessaryCollidables()
    {
        var collidables = GameObject.FindGameObjectsWithTag(Tags.Collidable);
        foreach (var collidable in collidables)
        {
            if (collidable.transform.position.y < ObstacleLowerBound)
            {
                Destroy(collidable);
            }
        }
    }

    private void GenerateCollidables()
    {
        var elements = GenerateObstacles().Take(random.Next(0, 3)).ToList();
        elements.AddRange(GenerateCollectables().Take(3 - elements.Count).ToList());
        elements = elements.OrderBy(x => Guid.NewGuid().ToString()).ToList();
        elements[0].transform.position = new Vector2(LeftLanePositionX, ObstacleGenerationPositionY);
        elements[1].transform.position = new Vector2(CenterLaneLocationX, ObstacleGenerationPositionY);
        elements[2].transform.position = new Vector2(RightLaneLocationX, ObstacleGenerationPositionY);
        elements.ForEach(obj =>
        {
            obj.transform.parent = ObstaclesObject.transform;
            obj.tag = Tags.Collidable;
            obj.AddComponent<Rigidbody2D>();

            var rigidbody = obj.GetComponent<Rigidbody2D>();
            rigidbody.velocity = Vector2.down * CollidablesVelocity;
            rigidbody.gravityScale = 0;
            rigidbody.mass = 0;
            rigidbody.freezeRotation = true;
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        });
    }

    private IEnumerable<GameObject> GenerateObstacles()
    {
        while (true)
        {
            yield return Instantiate(GetRandomObstacle());
        }
    }

    private IEnumerable<GameObject> GenerateCollectables()
    {
        while (true)
        {
            yield return Instantiate(Collectable);
        }
    }

    private GameObject GetRandomObstacle()
    {
        return Obstacles[random.Next(0, Obstacles.Count)];
    }
}
