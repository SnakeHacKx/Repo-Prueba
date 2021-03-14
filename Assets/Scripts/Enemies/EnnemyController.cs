﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnnemyController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Velocidad de movimiento del enemigo")]
    private float speed = 1;

    private Rigidbody2D _rigidbody;

    private bool isMoving;

    [Tooltip("Tiempo que tarda el enemigo entre un paso y el siguiente (pasos sucesivas)")]
    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    [Tooltip("Tiempo que tarda el enemigo en dar un paso")]
    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    public Vector2 directionToMove;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        // hace aleatorio el arranque de cada enemigo, lo cual hace más diverso
        // el movimiento de los enemigos
        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            timeToMakeStepCounter -= Time.deltaTime;
            _rigidbody.velocity = directionToMove * speed;

            // Cuando se queda sin tiempo de movimiento, paramos
            // al enemigo
            if(timeToMakeStepCounter < 0)
            {
                isMoving = false;
                timeBetweenStepsCounter = timeBetweenSteps;
                _rigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            timeBetweenStepsCounter -= Time.deltaTime;

            // Cuando se queda sin tiempo de estar quieto, arrancamos
            // al enemigo para que de un paso
            if(timeBetweenStepsCounter < 0)
            {
                isMoving = true;
                timeToMakeStepCounter = timeToMakeStep;
                directionToMove = new Vector2(Random.Range(-1, 2), 0);
            }
        }
    }
}