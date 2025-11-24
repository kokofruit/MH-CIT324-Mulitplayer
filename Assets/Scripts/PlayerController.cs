using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movementVector;
    private Text pointsText;
    private NetworkVariable<int> points = new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        pointsText = GetComponentInChildren<Text>();
        points.OnValueChanged += OnPointsChanged;
    }

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

    public void OnPointsChanged(int oldValue, int newValue)
    {
        pointsText.text = newValue.ToString();
        Debug.Log($"Points text updated!");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered. IsOwner: {IsOwner}; IsSpawned: {IsSpawned}.");

        if (!IsOwner) return;

        if (other.CompareTag("PickUp"))
        {
            points.Value += 1;
            Debug.Log($"Points incremented to {points.Value}");
        }

        // i added this because my unity was whining and complaining
        if (IsServer) other.GetComponent<NetworkObject>().Despawn();
    }

    private void OnDestroy()
    {
        points.OnValueChanged -= OnPointsChanged;
    }
}
