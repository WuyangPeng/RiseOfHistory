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
        private sealed class DecimalProcessor : Game.Scripts.Main.Editor.BuildEvent.Generator.DataTableProcessor.GenericDataProcessor<decimal>
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
                    return "decimal";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "decimal",
                    "system.decimal"
                };
            }

            public override decimal Parse(string value)
            {
                return decimal.Parse(value);
            }

            public override void WriteToStream(Game.Scripts.Main.Editor.BuildEvent.Generator.DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                binaryWriter.Write(Parse(value));
            }
        }
    }
}
