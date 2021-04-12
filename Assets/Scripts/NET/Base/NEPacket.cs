using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHeader { 
    public static int lenSize = sizeof(ushort);
    public ushort len;
    public uint cmd;

    public void serialize(ref BinSerializer se) {
        se.io_fixed(ref len);
        se.io(ref cmd);
    }

    public void deserialize(ref BinDeserializer se) {
        se.io_fixed(ref len);
        se.io(ref cmd);
    }
}

public class NEPacket<PacketCmd> where PacketCmd : System.Enum{

    public readonly PacketHeader packetHeader;

    public NEPacket(PacketCmd cmd) {
        packetHeader = new PacketHeader();
        packetHeader.cmd = (uint)(object)cmd;
    }

    protected virtual void serialize  (ref BinSerializer   se) {;}
    protected virtual void deserialize(ref BinDeserializer se) {;}

    public void writeToBuffer(List<byte> buf) {

        buf.Clear();

        var se = new BinSerializer(buf);

        packetHeader.serialize(ref se);
        serialize(ref se);
        packetHeader.len = (ushort)buf.Count;
        se.io_fixed(ref packetHeader.len, 0);
    }

    public void readFromBuffer(System.ReadOnlySpan<byte> buf) {
        var se = new BinDeserializer(buf);
        packetHeader.deserialize(ref se);
        deserialize(ref se);
    }
}
