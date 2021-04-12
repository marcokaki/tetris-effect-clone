using System.Net;
using UnityEngine;

public class Server : MonoBehaviour
{
    GatewayEngine cEngine;

    void Awake() {
        cEngine = new GatewayEngine();
    }

    void Update() {
        cEngine.Update();
    }

    public void Listen() {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        SocketAddress sockAdd = new SocketAddress(ipAddress, 11000);

        cEngine.Listen(sockAdd);
    }


    class GatewayEngine : NetEngine {
        public override void OnAccept(NESocket s) {

            base.OnAccept(s);

            GreetingPacket2 pkt = new GreetingPacket2(ClientPacketCmd.Greeting);
            pkt.s = ": Hello Greeting From Gateway";
            SendPacket(s, pkt);
        }
    }
}



public enum ClientPacketCmd : uint {
    Greeting,
    Greeting2,
}

public class GreetingPacket2 : NEPacket<ClientPacketCmd> {
    public string s;
    public GreetingPacket2(ClientPacketCmd cmd) : base(cmd) { }
    protected override void serialize(ref BinSerializer se) => se.io(ref s);
    protected override void deserialize(ref BinDeserializer se) => se.io(ref s);
}

public class GreetingPacket3 : NEPacket<ClientPacketCmd> {
    public int i;
    public GreetingPacket3(ClientPacketCmd cmd) : base(cmd) { }
    protected override void serialize(ref BinSerializer se) => se.io(ref i);
    protected override void deserialize(ref BinDeserializer se) => se.io(ref i);
}



