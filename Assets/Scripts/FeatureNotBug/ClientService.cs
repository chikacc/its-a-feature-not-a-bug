using Cysharp.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;
using UnityEngine.Localization.SmartFormat.Extensions;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace FeatureNotBug;

public sealed class ClientService : IClientService {
    readonly NetworkManager _manager;
    readonly UnityTransport _transport;
    readonly PersistentVariablesSource _variablesSource;

    public ClientService(NetworkManager manager, UnityTransport transport, PersistentVariablesSource variablesSource) {
        _manager = manager;
        _transport = transport;
        _variablesSource = variablesSource;
    }

    public string JoinCode { get; private set; } = string.Empty;

    public async UniTask<bool> Start(string joinCode) {
        JoinCode = joinCode;
        _variablesSource["global"]["JoinCode"] = new StringVariable { Value = JoinCode };
        var relayService = RelayService.Instance;
        var allocation = await relayService.JoinAllocationAsync(joinCode);
        _transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);
        return _manager.StartClient();
    }

    public void Shutdown() {
        _manager.Shutdown();
    }
}
