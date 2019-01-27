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
    private GameObject ObstaclesObject;
    private GameObject Player;
    private bool invokeRepeating = true;

    public static GameManager GameManager = new GameManager();
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

    private void Start()
    {
        GameManager.Instance.OnGameOver += OnGameOver;
        GameManager.Instance.OnRestartGame += OnRestartGame;
        Player = GameObject.FindGameObjectWithTag(Tags.Player);
        ObstaclesObject = GameObject.FindGameObjectWithTag(Tags.Collidables);
        Obstacles.Add(Obstacle1);
        Obstacles.Add(Obstacle2);
        Obstacles.Add(Obstacle3);
        InvokeRepeating("GenerateCollidables", ObstacleGenerationTimeOffset, ObstacleGenerationRepeatRate);
    }

    private void OnRestartGame(object sender, EventArgs e)
    {
        invokeRepeating = true;
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        invokeRepeating = false;
    }

    private void Update()
    {
        DestroyUnncessaryCollidables();
    }

    private void DestroyUnncessaryCollidables()
    {
        var collidables = GetObjectsInLayer(GameObject.FindGameObjectWithTag(Tags.Collidables), Layers.Collidables);
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
        if (!invokeRepeating)
            return;

        var elements = GenerateObstacles().Take(random.Next(0, 3)).ToList();
        elements.AddRange(GenerateCollectables().Take(3 - elements.Count).ToList());
        elements = elements.OrderBy(x => Guid.NewGuid().ToString()).ToList();
        elements[0].transform.position = new Vector3(LeftLanePositionX, ObstacleGenerationPositionY, Player.transform.position.z);
        elements[1].transform.position = new Vector3(CenterLaneLocationX, ObstacleGenerationPositionY, Player.transform.position.z);
        elements[2].transform.position = new Vector3(RightLaneLocationX, ObstacleGenerationPositionY, Player.transform.position.z);
        elements.ForEach(obj =>
        {
            obj.transform.parent = ObstaclesObject.transform;
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
        var obstacle = Obstacles[random.Next(0, Obstacles.Count)];
        return obstacle;
    }

    private static List<GameObject> GetObjectsInLayer(GameObject root, int layer)
    {
        var results = root.transform
            .GetComponentsInChildren(typeof(Transform))
            .Where(child => child.gameObject.layer == layer)
            .Select(child => child.gameObject)
            .ToList();
        return results;
    }
}
