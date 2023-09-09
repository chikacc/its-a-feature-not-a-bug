using Unity.Netcode;
using UnityEngine;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [DisallowMultipleComponent]
    [AddComponentMenu("Feature Not Bug/Chat Repository")]
    public sealed class ChatRepository : NetworkBehaviour, IChatRepository {
        public readonly NetworkList<Chat> Chats = new();

        public int Count => Chats.Count;
        public void Add(Chat chat) => Chats.Add(chat);
    }
}
