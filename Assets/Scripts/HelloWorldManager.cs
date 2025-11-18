using Unity.Netcode;
using UnityEngine;

public class HelloWorldManager : MonoBehaviour
{
    private NetworkManager networkManager;

    private void Awake()
    {
        networkManager = GetComponent<NetworkManager>();
    }

    private void OnGUI()
    {
        // Set up area to add UI elements
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));

        // If we're not a client or server, we need buttons to start one of them
        if (!networkManager.IsClient && !networkManager.IsServer) StartButtons();
        else StatusLabels();

        GUILayout.EndArea();
    }

    private void StartButtons()
    {
        // Creates the buttons
        if (GUILayout.Button("Host")) networkManager.StartHost();
        if (GUILayout.Button("Client")) networkManager.StartClient();
        if (GUILayout.Button("Server")) networkManager.StartServer();
    }

    private void StatusLabels()
    {
        // Determines the mode for the label
        var mode = networkManager.IsHost ? "Host" : networkManager.IsServer ? "Server" : "Client";

        // Creates a label for each window
        GUILayout.Label("Transport: " + networkManager.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }
}
