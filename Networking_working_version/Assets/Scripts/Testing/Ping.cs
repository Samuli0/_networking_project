using System;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
/*///
public class Ping : MonoBehaviour
{

    private float Pingu;
    public float Pingret {  get; private set; }
    public int Pingu2;
    ulong empty = 1;

    private RpcTest rp;

    private void Awake()
    {
        SendPingToServer();
    }

    public void SendPingToServer()
    {
        Pingu = Time.time;
        Pingu2 = (int)Pingu;
        rp.ServerOnlyRpc(Pingu2, empty);
    }

}
//*///

//*/// ----------------------- full vibe code since having issues
using Unity.Netcode;
using UnityEngine;

public class Ping : NetworkBehaviour
{
    public float Pingret { get; private set; }

    private float pingStartTime;
    private bool waitingForResponse;

    public override void OnNetworkSpawn()
    {
        if (IsClient && IsOwner)
        {
            InvokeRepeating(nameof(SendPingToServer), 1f, 1f);
        }
    }

    private void SendPingToServer()
    {
        if (!IsSpawned || waitingForResponse)
            return;

        pingStartTime = Time.realtimeSinceStartup;
        waitingForResponse = true;

        Debug.Log("Sending ping...");

        PingServerRpc();
    }

    [Rpc(SendTo.Server)]
    private void PingServerRpc(RpcParams rpcParams = default)
    {
        ulong senderId = rpcParams.Receive.SenderClientId;

        Debug.Log($"Server received ping from client {senderId}");

        PingResponseRpc(
            RpcTarget.Single(senderId, RpcTargetUse.Temp)
        );
    }

    [Rpc(SendTo.SpecifiedInParams)]
    private void PingResponseRpc(RpcParams rpcParams = default)
    {
        Pingret =
            (Time.realtimeSinceStartup - pingStartTime) * 1000f;

        waitingForResponse = false;

        Debug.Log($"Ping: {Pingret:F1} ms");
    }

    public override void OnNetworkDespawn()
    {
        CancelInvoke(nameof(SendPingToServer));
        waitingForResponse = false;
    }
}
//*///