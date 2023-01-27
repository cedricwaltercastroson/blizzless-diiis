﻿using DiIiS_NA.GameServer.ClientSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiIiS_NA.GameServer.MessageSystem.Message.Definitions.Base
{
    
    [Message(new[] {
        Opcodes.PlayerHeroStatePlayerFlags,Opcodes.PlayerHeroStateWaypoints, Opcodes.PlayerDWordDataMessage2 })]
    public class PlayerDWordDataMessage : GameMessage, ISelfHandler
    {
        public int Field0;
        public int Field1;

        public PlayerDWordDataMessage() { }
        public PlayerDWordDataMessage(int opcode) : base(opcode) { }

        public void Handle(GameClient client)
        {

        }

        public override void Parse(GameBitBuffer buffer)
        {
            Field0 = buffer.ReadInt(32);
            Field1 = buffer.ReadInt(32);
        }

        public override void Encode(GameBitBuffer buffer)
        {
            buffer.WriteInt(32, Field0);
            buffer.WriteInt(32, Field1);
        }

        public override void AsText(StringBuilder b, int pad)
        {
            b.Append(' ', pad);
            b.AppendLine("PlayerDWordDataMessage:");
            b.Append(' ', pad++);
            b.AppendLine("{");
            b.Append(' ', pad); b.AppendLine("Field0: 0x" + Field0.ToString("X8") + " (" + Field0 + ")");
            b.Append(' ', pad); b.AppendLine("Field1: 0x" + Field1.ToString("X8") + " (" + Field1 + ")");
            b.Append(' ', --pad);
            b.AppendLine("}");
        }
    }
}
