using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerManager : MonoBehaviour, ICSideRecviable {

    OtherPlayer[] otherPlayersPrefab;
    Dictionary<byte, OtherPlayer> otherPlayers;

    int currentOtherPlayerCount;

    void Awake() {
        otherPlayersPrefab = GetComponentsInChildren<OtherPlayer>();
        otherPlayers = new Dictionary<byte, OtherPlayer>();
        NetManager.Instance.Client.RegisterListener(this, CSideCmd.login, CSideCmd.playField, CSideCmd.lineClear, CSideCmd.nextPiece);
    }

    void ICSideRecviable.OnRecv(NEPacket<CSideCmd> pkt) {
        switch((CSideCmd)pkt.packetHeader.cmd) {

            case CSideCmd.login: {
                var p = pkt as LoginPacket;
                otherPlayers.Add(p.id, otherPlayersPrefab[currentOtherPlayerCount++]);
                otherPlayers[p.id].OnRecv(p);
            }
                break;

            case CSideCmd.playField: {
                var p = pkt as PlayFieldPacket;
                otherPlayers[p.id].OnRecv(p);
            }
                break;

            case CSideCmd.lineClear: {
                var p = pkt as LineClearPacket;
                otherPlayers[p.id].OnRecv(p);
            }
                break;
            case CSideCmd.nextPiece: {
                var p = pkt as NextPiecePacket;
                otherPlayers[p.id].OnRecv(p);
            }
                break;
        }
    }
}
