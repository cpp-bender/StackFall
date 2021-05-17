using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController :MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        playerData.CanHit = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerData.CanHit = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            playerData.CanHit = false;
        }
    }
    private void FixedUpdate()
    {
        if (playerData.CanHit)
        {
            MoveDown();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!playerData.CanHit)
        {
            MoveUp();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!playerData.CanHit)
        {
            MoveUp();
        }
    }

    private void MoveUp()
    {
        body.velocity = Vector3.up * playerData.Speed * Time.deltaTime;
    }

    private void MoveDown()
    {
        body.velocity = Vector3.down * playerData.Speed * Time.fixedDeltaTime;
    }
}
