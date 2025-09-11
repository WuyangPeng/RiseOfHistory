using ProtoBuf;

namespace Game.Scripts.Main.Runtime.Network
{
    public abstract class PacketBase : GameFramework.Network.Packet, IExtensible
    {
        private IExtension m_ExtensionObject = null;

        public abstract PacketType PacketType
        {
            get;
        }

        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref m_ExtensionObject, createIfMissing);
        }
    }
}
