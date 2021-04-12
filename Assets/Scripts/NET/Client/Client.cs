using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Client : MonoBehaviour
{
    ClientEngine engine;

    void Awake() => engine = new ClientEngine();
    void Update() => engine.Update();

    public void Connect() {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        SocketAddress sockAdd = new SocketAddress(ipAddress, 11000);

        engine.Connect(sockAdd);
    }


    class ClientEngine : NetEngine {
        public override void OnRecvPacket(NESocket s, PacketHeader hdr, System.Span<byte> buf) {
            var cmd = (ClientPacketCmd)hdr.cmd;

            switch (cmd) {
                case ClientPacketCmd.Greeting: {
                    var pkt = new GreetingPacket2(cmd);
                    pkt.readFromBuffer(buf);

                    Debug.Log(pkt.packetHeader.len);
                    Debug.Log(pkt.s);

                    break;
                }

                case ClientPacketCmd.Greeting2: {
                    var pkt = new GreetingPacket3(cmd);
                    pkt.readFromBuffer(buf);

                    Debug.Log(pkt.packetHeader.len);
                    Debug.Log(pkt.i);

                    break;

                }
            }
        }
    }
}





