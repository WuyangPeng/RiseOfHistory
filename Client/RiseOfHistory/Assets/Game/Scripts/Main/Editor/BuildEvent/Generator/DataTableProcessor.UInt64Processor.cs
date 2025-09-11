//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.IO;

namespace Game.Scripts.Main.Editor.BuildEvent.Generator
{
    public sealed partial class DataTableProcessor
    {
        private sealed class UInt64Processor : DataTableProcessor.GenericDataProcessor<ulong>
        {
            public override bool IsSystem
            {
                get
                {
                    return true;
                }
            }

            public override string LanguageKeyword
            {
                get
                {
                    return "ulong";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "ulong",
                    "uint64",
                    "system.uint64"
                };
            }

            public override ulong Parse(string value)
            {
                return ulong.Parse(value);
            }

            public override void WriteToStream(Game.Scripts.Main.Editor.BuildEvent.Generator.DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                binaryWriter.Write7BitEncodedUInt64(Parse(value));
            }
        }
    }
}
