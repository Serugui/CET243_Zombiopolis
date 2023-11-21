using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIAMovement : MonoBehaviour

{
    [SerializeField]
    private float speed;
    private float changeDirectionCooldown;

    [SerializeField]
    private float wanderRadius;

    private Transform modelTransform; // assuming the model is a child of the GameObject
    private Transform player;
    private Vector3 targetDirection;
    private Quaternion modelInitialRotation;
    private bool isPlayerInRange = false;

    private void Awake()
    {
        modelTransform = transform.GetChild(0); // Assuming the model is a child of the GameObject
        modelInitialRotation = modelTransform.rotation;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has the tag "Player"

        // Set the initial random direction
        targetDirection = Random.insideUnitSphere * wanderRadius;
        targetDirection.y = 0; // Ensure movement is restricted to the ground plane

        // Initialize the cooldown
        changeDirectionCooldown = Random.Range(1f, 5f);
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            FleePlayer();
        }
        else
        {
            UpdateTargetDirection();
            Wander();
        }

        // You can add additional behaviors or checks here as needed.
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
    }

    private void HandleRandomDirectionChange()
    {
        changeDirectionCooldown -= Time.deltaTime;

        if (changeDirectionCooldown <= 0)
        {
            targetDirection = Random.insideUnitSphere * wanderRadius;
            targetDirection.y = 0; // Ensure movement is restricted to the ground plane

            changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < 10f) // Adjust the range as needed
        {
            isPlayerInRange = true;
            targetDirection = (transform.position - player.position).normalized;
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    private void Wander()
    {
        transform.position += targetDirection.normalized * speed * Time.deltaTime;
        RotateTowardsTarget(targetDirection.normalized);
    }

    private void FleePlayer()
    {
        // Move away from the player
        transform.position += targetDirection.normalized * -speed * Time.deltaTime;
        RotateTowardsTarget(targetDirection.normalized);
    }

    private void RotateTowardsTarget(Vector3 direction)
    {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
        modelTransform.rotation = modelInitialRotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}