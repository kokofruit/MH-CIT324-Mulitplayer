using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    private Vector2 movementVector;

    public void OnMove(InputValue inputValue)
    {
        movementVector = inputValue.Get<Vector2>();
    }

    private void Update()
    {
        Vector3 movementDirecion = new Vector3(movementVector.x, movementVector.y, 0);
        transform.Translate(movementDirecion * moveSpeed * Time.deltaTime);
    }
}
