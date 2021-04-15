using System.Collections.Generic;
using System.Net;
using UnityEngine;

public enum CSideCmd : uint {
    login,
    playField,
    lineClear,
    debugString,
}

public interface ICSideRecviable {
    void OnRecv(NEPacket<CSideCmd> pkt);
}

public class CSideRecvChannel {
    static int cmdCount => Util.EnumCount<CSideCmd>();

    public List<ICSideRecviable>[] listenerMap;

    public CSideRecvChannel() {
        listenerMap = new List<ICSideRecviable>[cmdCount];
        for(int i = 0; i < cmdCount; i++) {
            listenerMap[i] = new List<ICSideRecviable>();
        }
    }

    public void RegisterListener(ICSideRecviable listener, params CSideCmd[] cmds) {
        foreach(var cmd in cmds) {
            listenerMap[(int)cmd].Add(listener);
        }
    }

    public void UnregisterListener(ICSideRecviable listener, params CSideCmd[] cmds) {
        //remove;
    }

    public void OnRecv(PacketHeader hdr, System.Span<byte> buf) {
        var cmd = (CSideCmd)hdr.cmd;

        NEPacket<CSideCmd> pkt = null;

        switch (cmd) {
            case CSideCmd.login:
                pkt = new LoginPacket();
                break;

            case CSideCmd.playField: 
                pkt = new PlayFieldPacket();                
                break;

            case CSideCmd.lineClear: 
                pkt = new LineClearPacket();                
                break;
        }

        if (pkt == null) return;
        pkt.readFromBuffer(buf);
        var listeners = listenerMap[(int)cmd];

        foreach (var listener in listeners) {
            listener.OnRecv(pkt);
        }        
    }
}


public class Client : MonoBehaviour {
    ClientEngine engine;

    void Awake() {
        engine = new ClientEngine();
    }

    public void Update() => engine.Update();

    public void Connect() {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        SocketAddress sockAdd = new SocketAddress(ipAddress, 11000);

        engine.Connect(sockAdd);
    }

    public void SendToServer(NEPacket<CSideCmd> pkt) => engine.SendPacket(pkt);

    public void RegisterListener(ICSideRecviable listener, params CSideCmd[] cmds) {
        engine.channel.RegisterListener(listener, cmds);
    }

    class ClientEngine : NetEngine {
        public readonly CSideRecvChannel channel = new CSideRecvChannel();
        NESocket _serverSock;

        public override void OnConnect(NESocket s) {
            _serverSock = s;
            var pkt = new StringPacket() { s = "Greeting from client" };
            SendPacket(s, pkt);
        }

        public override void OnRecvPacket(NESocket s, PacketHeader hdr, System.Span<byte> buf) {

            if((CSideCmd)hdr.cmd == CSideCmd.debugString) {
                var pkt = new StringPacket();
                pkt.readFromBuffer(buf);
                Debug.Log(pkt.s);
            }
            else {
                channel.OnRecv(hdr, buf);
            }
        }

        public void SendPacket(NEPacket<CSideCmd> pkt) {
            if (_serverSock == null) return;
            SendPacket(_serverSock, pkt);
        }
    }
}

#region CSidePacket

public class LoginPacket : NEPacket<CSideCmd> {//sent from server;
    public byte id;
    public LoginPacket() : base(CSideCmd.login) { }
    protected override void serialize  (ref BinSerializer   se) {
        se.io(ref id);
    }
    protected override void deserialize(ref BinDeserializer se) {
        se.io(ref id);
    }
}
public class PlayFieldPacket : NEPacket<CSideCmd> {
    public byte    id;
    public Piece   piece;
    public int[][] tileMap;

    public PlayFieldPacket() : base(CSideCmd.playField) { }
    
    protected override void serialize  (ref BinSerializer   se) {
        se.io(ref id);
        se.io(ref piece);
        se.io(ref tileMap);
    }
    protected override void deserialize(ref BinDeserializer se) {
        se.io(ref id);
        se.io(ref piece);
        se.io(ref tileMap);
    }
}
public class LineClearPacket : NEPacket<CSideCmd> {
    public byte  id;
    public int   score;
    public byte  level;
    public short lines;

    public LineClearPacket() : base(CSideCmd.lineClear) { }
    protected override void serialize  (ref BinSerializer   se) {
        se.io(ref id);  
        se.io(ref score);
        se.io(ref level);
        se.io(ref lines);
    }
    protected override void deserialize(ref BinDeserializer se) {
        se.io(ref id);
        se.io(ref score);
        se.io(ref level);
        se.io(ref lines);
    }
}
public class StringPacket : NEPacket<CSideCmd> {
    public string s;
    public StringPacket() : base(CSideCmd.debugString) { }
    protected override void serialize  (ref BinSerializer   se) => se.io(ref s);
    protected override void deserialize(ref BinDeserializer se) => se.io(ref s);
}
#endregion





