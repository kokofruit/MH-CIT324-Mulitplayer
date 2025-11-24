using Unity.Netcode;
using UnityEngine;

public class PickUpSpawner : NetworkBehaviour
{
    public GameObject pickUpPrefab;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // spawn 4 pickups
            SpawnPickUpAt(new Vector3(2, 2, 0));
            SpawnPickUpAt(new Vector3(2, 4, 0));
            SpawnPickUpAt(new Vector3(-2, -2, 0));
            SpawnPickUpAt(new Vector3(-2, -4, 0));
        }
    }

    public void SpawnPickUpAt(Vector3 position)
    {
        // instantiate new pickup at provided position
        GameObject newPickUp = Instantiate(pickUpPrefab, position, Quaternion.identity);
        // call netcode function for spawning object
        newPickUp.GetComponent<NetworkObject>().Spawn();
    }
}
