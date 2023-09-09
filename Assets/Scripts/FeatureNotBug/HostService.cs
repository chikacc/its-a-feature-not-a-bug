using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;

namespace FeatureNotBug;

public sealed class HostService : IHostService {
    const int MaxConnections = 10;

    readonly NetworkManager _manager;
    readonly UnityTransport _transport;

    public HostService(NetworkManager manager, UnityTransport transport) {
        _manager = manager;
        _transport = transport;
    }

    public string JoinCode { get; private set; } = string.Empty;

    public async UniTask<bool> Start(CancellationToken cancellationToken) {
        var relayService = RelayService.Instance;
        var allocation = await relayService.CreateAllocationAsync(MaxConnections).AsUniTask()
            .AttachExternalCancellation(cancellationToken);
        JoinCode = await relayService.GetJoinCodeAsync(allocation.AllocationId).AsUniTask()
            .AttachExternalCancellation(cancellationToken);
        _transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);
        return _manager.StartHost();
    }

    public void Shutdown() {
        _manager.Shutdown();
    }
}
