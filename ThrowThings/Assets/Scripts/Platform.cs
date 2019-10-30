using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float minRespawnTime = 2f;

    [SerializeField]
    private float maxRespawnTime = 3f;

    [SerializeField]
    private float aliveTime = 3f;

    [SerializeField]
    private Transform root;

    private float nextRespawn;
    private float timeAlive;
    private bool respawning;

    private void Awake()
    {
        Disappear();
    }

    private void Appear()
    {
        timeAlive = 0f;
        respawning = false;
        root.localPosition = Vector3.zero;
        root.gameObject.SetActive(true);
    }

    private void Disappear()
    {
        root.gameObject.SetActive(false);
        respawning = true;
        nextRespawn = Time.time + Random.Range(minRespawnTime, maxRespawnTime);
    }

    private void Update()
    {
        if (respawning)
        {
            if (Time.time > nextRespawn)
            {
                Appear();
            }
        }
        else
        {
            timeAlive += Time.deltaTime;
            if (timeAlive > aliveTime - 1f)
            {
                float t = Time.time * 24f;
                root.localPosition = new Vector3(Mathf.PerlinNoise(t, 0f), 0f) * 0.1f;
            }

            if (timeAlive > aliveTime)
            {
                Disappear();
            }
        }
    }
}