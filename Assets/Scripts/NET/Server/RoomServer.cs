using System;
using System.Net;
using UnityEngine;

public class RoomServer : MonoBehaviour
{
    RoomEngine engine;

    void Awake() => engine = new RoomEngine();

    void Update() => engine.Update();

    public void Listen() {
        IPHostEntry host      = Dns.GetHostEntry("localhost");
        IPAddress ipAddress   = host.AddressList[0];
        SocketAddress sockAdd = new SocketAddress(ipAddress, 11000);

        engine.Listen(sockAdd);
    }

    class RoomEngine : NetEngine {
        public override void OnAccept(NESocket s) {

            var loginPkt = new LoginPacket() { id = (byte)s._id };
            SendAll(loginPkt);

            foreach(var existingSock in connectSocks) {
                if (existingSock == s) continue;
                var existingLoginPkt = new LoginPacket() { id = (byte)existingSock._id };
                SendPacket(s, existingLoginPkt);
            }
        }

        public override void OnRecvPacket(NESocket s, PacketHeader hdr, Span<byte> buf) {
            var cmd = (CSideCmd)hdr.cmd;
            switch (cmd) {

                case CSideCmd.playField: {
                    var pkt = new PlayFieldPacket();
                    pkt.readFromBuffer(buf);
                    pkt.id = (byte)s._id;
                        
                    SendAll(pkt);
                }
                    break;

                case CSideCmd.lineClear: {
                    var pkt = new LineClearPacket();
                    pkt.readFromBuffer(buf);
                    pkt.id = (byte)s._id;

                    SendAll(pkt);
                }
                    break;

                case CSideCmd.debugString: {
                    var pkt = new StringPacket();
                    pkt.readFromBuffer(buf);
                }
                    break;

                case CSideCmd.nextPiece: {
                    var pkt = new NextPiecePacket();
                    pkt.readFromBuffer(buf);
                    pkt.id = (byte)s._id;

                    SendAll(pkt);
                }
                    break;
            }
        }
    }
}
