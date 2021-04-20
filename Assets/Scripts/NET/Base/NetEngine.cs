using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;


public class NetEngine {
    readonly List<byte> sendBuff = new List<byte>();
    protected readonly List<NESocket> listenSocks  = new List<NESocket>();
    protected readonly List<NESocket> connectSocks = new List<NESocket>();

    int connectedCount;

    public void Update() {

        for (int i = 0; i < listenSocks.Count;) {
            NESocket listener = listenSocks[i];

            if (listener.state == NESocket.State.disconnected) {
                listenSocks.UnsortedRemoveAt(i);
                OnDisconnect(listener);
                continue;
            }
            i++;

            try {

                if (listener._socket.Poll(0, SelectMode.SelectRead)) {

                    NESocket connectSock;
                    connectSocks.Add(connectSock = listener.Accept());

                    connectSock._socket.Blocking = false;
                    connectSock.state = NESocket.State.connected;
                    connectSock.setSocketID(connectedCount++);
                    OnAccept(connectSock);
                }
            }
            catch(System.Exception e) {
                Debug.Log(e);
                listener.Close();
            }
        }

        for (int i = 0; i < connectSocks.Count;) {
            NESocket s = connectSocks[i];

            if (s == null || s.state == NESocket.State.disconnected) {
                connectSocks.UnsortedRemoveAt(i);
                OnDisconnect(s);
                continue;
            }
            i++;

            bool readable = s._socket.Poll(0, SelectMode.SelectRead );
            bool writable = s._socket.Poll(0, SelectMode.SelectWrite);

            if (s.state == NESocket.State.connecting && readable) {
                s.state = NESocket.State.connected;
                OnConnect(s);
            }

            if (s.state != NESocket.State.connected)
                continue;

            if (readable) OnRecv(s);
        }
    }

    public NESocket Listen(SocketAddress addr) {
        
        var listener = new NESocket(addr, ProtocolType.Tcp);

        try {
            var localEndPt = new IPEndPoint(addr.iPAddress, addr.port);
            listener._socket.Blocking = false;
            listener._socket.Bind(localEndPt);
            listener._socket.Listen(10);
            listenSocks.Add(listener);
            return listener;
        }
        catch {
            listener.Close();
        }
        return null;
    }

    public void StopListen() { }

    public NESocket Connect(SocketAddress addr) {
        var remoteEP = new IPEndPoint(addr.iPAddress, addr.port);
        var socket = new NESocket(addr, ProtocolType.Tcp);
        connectSocks.Add(socket);

        try {
            socket._socket.Blocking = false;            
            socket._socket.Connect(remoteEP);            
            socket.state = NESocket.State.connected;
            OnConnect(socket);

            return socket;
        }
        catch (System.Exception e) {
            if(e is SocketException sE && sE.ErrorCode == 10035) {
                socket.state = NESocket.State.connecting;
            }
            else {
                socket.Close();
            }
        }
        return null;
    }

    public virtual void OnAccept    (NESocket s) { Debug.Log("accepted"); }
    public virtual void OnConnect   (NESocket s) { Debug.Log("connected"); }
    public virtual void OnDisconnect(NESocket s) { Debug.Log("disconnected"); }
    public virtual void OnRecvPacket(NESocket s, PacketHeader hdr, System.Span<byte> buf) {; }

    void OnRecv(NESocket s) {
        if (s.state != NESocket.State.connected) 
            return;

        byte[] recvBuf = s.OnRecv(out PacketHeader hdr);

        if (recvBuf != null) {
            OnRecvPacket(s, hdr, recvBuf);
        }
    }

    public int SendPacket<cmd>(NESocket s, NEPacket<cmd> nEPacket) where cmd : System.Enum {
        if (s.state != NESocket.State.connected || !s._socket.Poll(0, SelectMode.SelectWrite)) {
            Debug.Log("packet cannot send");
            return 0;
        }

        nEPacket.writeToBuffer(sendBuff);
        return s._socket.Send(sendBuff.ToArray());
/*
        try {
            nEPacket.writeToBuffer(sendBuff);
            return s._socket.Send(sendBuff.ToArray());
        }
        catch(System.Exception e) {
            Debug.Log(e);
            return 0;
        }*/
    }

    public int SendAllExcept<cmd>(NESocket exceptS, NEPacket<cmd> nEPacket) where cmd : System.Enum {
        int byteSend = 0;
        foreach(var sock in connectSocks) {
            if (exceptS == sock) continue;
            byteSend += SendPacket(sock, nEPacket);
        }
        return byteSend;
    }

    public int SendAll<cmd>(NEPacket<cmd> nEPacket) where cmd : System.Enum {
        int byteSend = 0;
        foreach(var sock in connectSocks) {
            byteSend += SendPacket(sock, nEPacket);
        }
        return byteSend;
    }
}


