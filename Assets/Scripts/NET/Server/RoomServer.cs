﻿using System;
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
            var greetingPkt = new StringPacket() { s = "Greeting from server, your id is: " + s._id };
            var loginPkt    = new LoginPacket () { id = (byte)s._id };

            SendPacket   (s, greetingPkt);
            SendAllExcept(s, loginPkt);
        }

        public override void OnRecvPacket(NESocket s, PacketHeader hdr, Span<byte> buf) {
            var cmd = (CSideCmd)hdr.cmd;
            switch (cmd) {

                case CSideCmd.playField: {
                    var pkt = new PlayFieldPacket();
                    pkt.readFromBuffer(buf);
                    pkt.id = (byte)s._id;

                    SendAllExcept(s, pkt);
                }
                    break;

                case CSideCmd.lineClear: {
                    var pkt = new LineClearPacket();
                    pkt.readFromBuffer(buf);
                    pkt.id = (byte)s._id;

                    SendAllExcept(s, pkt);
                }
                    break;

                case CSideCmd.debugString: {
                    var pkt = new StringPacket();
                    pkt.readFromBuffer(buf);
                }
                    break;
            }
        }
    }
}






