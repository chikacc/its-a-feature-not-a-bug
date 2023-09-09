using System;
using Unity.Collections;
using Unity.Netcode;

namespace FeatureNotBug;

public struct Chat : INetworkSerializable, IEquatable<Chat> {
    public uint Id;
    public ulong PlayerId;
    public FixedString512Bytes Message;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
        if (serializer.IsReader) {
            var reader = serializer.GetFastBufferReader();
            reader.ReadValueSafe(out Id);
            reader.ReadValueSafe(out PlayerId);
            reader.ReadValueSafe(out Message);
        } else {
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(Id);
            writer.WriteValueSafe(PlayerId);
            writer.WriteValueSafe(Message);
        }
    }

    public bool Equals(Chat other) {
        return Id == other.Id;
    }
}
