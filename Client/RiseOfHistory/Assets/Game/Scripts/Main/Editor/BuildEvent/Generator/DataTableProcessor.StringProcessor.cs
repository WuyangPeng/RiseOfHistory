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
        private sealed class StringProcessor : DataTableProcessor.GenericDataProcessor<string>
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
                    return "string";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "string",
                    "system.string"
                };
            }

            public override string Parse(string value)
            {
                return value;
            }

            public override void WriteToStream(Game.Scripts.Main.Editor.BuildEvent.Generator.DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                binaryWriter.Write(Parse(value));
            }
        }
    }
}
