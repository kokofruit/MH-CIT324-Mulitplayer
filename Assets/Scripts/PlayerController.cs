using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    
    private Vector2 movementVector;

    public void OnMove(InputValue inputValue)
    {
        // capture movement
        movementVector = inputValue.Get<Vector2>();
    }

    private void Update()
    {
        // allow only the owner to facilitate updates
        if (!IsOwner) return;

        // apply movement
        Vector3 movementDirecion = new Vector3(movementVector.x, movementVector.y, 0);
        transform.Translate(movementDirecion * moveSpeed * Time.deltaTime);
    }
}
