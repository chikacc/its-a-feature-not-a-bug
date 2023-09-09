using System;
using Unity.Netcode;

namespace FeatureNotBug;

public struct Card : INetworkSerializable, IEquatable<Card> {
    public uint Id;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
        if (serializer.IsReader) {
            var reader = serializer.GetFastBufferReader();
            reader.ReadValueSafe(out Id);
        } else {
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(Id);
        }
    }

    public bool Equals(Card other) {
        return Id == other.Id;
    }
}
