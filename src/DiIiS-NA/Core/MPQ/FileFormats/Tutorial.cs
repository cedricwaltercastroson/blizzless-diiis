﻿//Blizzless Project 2022
using System.Collections.Generic;
using CrystalMpq;
using DiIiS_NA.Core.MPQ.FileFormats.Types;
using DiIiS_NA.GameServer.Core.Types.SNO;
using DiIiS_NA.GameServer.Core.Types.Math;
using Gibbed.IO;

namespace DiIiS_NA.Core.MPQ.FileFormats
{
    [FileFormat(SNOGroup.Tutorial)]
    public class Tutorial : FileFormat
    {
        public Header Header { get; private set; }
        public int Flags { get; private set; }
        public int Label { get; private set; }
        public int TimeBeforeFading { get; private set; }
        public int RecurTime { get; private set; }
        public int OccurrencesUntilLearned { get; private set; }
        public int ArrowPosition { get; private set; }
        public Vector2D Offset { get; private set; }
        public int Pad { get; private set; }

        public Tutorial(MpqFile file)
        {
            var stream = file.Open();
            this.Header = new Header(stream);
            this.Flags = stream.ReadValueS32();
            this.Label = stream.ReadValueS32();
            this.TimeBeforeFading = stream.ReadValueS32();
            this.RecurTime = stream.ReadValueS32();
            this.OccurrencesUntilLearned = stream.ReadValueS32();
            this.ArrowPosition = stream.ReadValueS32();
            this.Offset = new Vector2D(stream);
            this.Pad = stream.ReadValueS32();
            stream.Close();
        }
    }
}
