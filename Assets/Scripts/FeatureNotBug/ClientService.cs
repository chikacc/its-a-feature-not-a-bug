using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;

namespace FeatureNotBug;

public sealed class ClientService : IClientService {
    readonly NetworkManager _manager;
    readonly UnityTransport _transport;

    public ClientService(NetworkManager manager, UnityTransport transport) {
        _manager = manager;
        _transport = transport;
    }

    public string JoinCode { get; private set; } = string.Empty;

    public async UniTask<bool> Start(string joinCode, CancellationToken cancellationToken) {
        JoinCode = joinCode;
        var relayService = RelayService.Instance;
        var allocation = await relayService.JoinAllocationAsync(joinCode).AsUniTask()
            .AttachExternalCancellation(cancellationToken);
        _transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);
        return _manager.StartClient();
    }

    public void Shutdown() {
        _manager.Shutdown();
    }
}
