using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NESocket {
    public enum State { none, connecting, connected, disconnected };
    public State state = State.none;
    public Socket _socket;

    enum RecvPhase { none, lenRecved };
    RecvPhase _phase = RecvPhase.none;
    PacketHeader _hdr;
    byte[] _recvBuf;

    int _maxClient;


    public NESocket(SocketAddress addr, ProtocolType type) {
        _socket = new Socket(addr.iPAddress.AddressFamily, SocketType.Stream, type);
    }

    NESocket(Socket s) => _socket = s;

    public void setMaxClient(int value) => _maxClient = value;

    public NESocket Accept() {
        return new NESocket(_socket.Accept());
    }

    public byte[] OnRecv(out PacketHeader hdr) {

        hdr = null;

        if (_socket.Available == 0) {
            Close();
            return null;
        }

        if (_phase == RecvPhase.none && _socket.Available > PacketHeader.lenSize) {

            try {
                _recvBuf = new byte[PacketHeader.lenSize];
                _hdr = new PacketHeader();

                _socket.Receive(_recvBuf);

                var se = new BinDeserializer(_recvBuf);
                se.io_fixed(ref _hdr.len);
                
                _phase = RecvPhase.lenRecved;
            }
            catch (System.Exception e) {
                Close();
                Debug.Log(e);

            }            
        }
        
        if (_phase == RecvPhase.lenRecved && _socket.Available >= _hdr.len - PacketHeader.lenSize) {
            
            try {
                var temp = new byte[_hdr.len];
                System.Buffer.BlockCopy(_recvBuf, 0, temp, 0, _recvBuf.Length);
                _recvBuf = temp;

                _socket.Receive(_recvBuf, PacketHeader.lenSize, _hdr.len - PacketHeader.lenSize, SocketFlags.None);

                var se = new BinDeserializer(_recvBuf);
                _hdr.deserialize(ref se);
                hdr = _hdr;

                _hdr = null;
                _phase = RecvPhase.none;

                return _recvBuf;
            }
            catch (System.Exception e) {
                Close();
                Debug.Log(e);
            }
        }
        return null;
    }

    public void Close() {
        state = State.disconnected;
        _socket.Shutdown(SocketShutdown.Both);
        _socket.Close();
    }
}

public class SocketAddress {

    public IPAddress iPAddress;
    public int port;

    public SocketAddress(IPAddress ip, int portValue) {

        iPAddress = ip;
        port = portValue;
    }
}



