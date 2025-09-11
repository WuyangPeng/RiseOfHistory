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
        private sealed class CharProcessor : Game.Scripts.Main.Editor.BuildEvent.Generator.DataTableProcessor.GenericDataProcessor<char>
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
                    return "char";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "char",
                    "system.char"
                };
            }

            public override char Parse(string value)
            {
                return char.Parse(value);
            }

            public override void WriteToStream(Game.Scripts.Main.Editor.BuildEvent.Generator.DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                binaryWriter.Write(Parse(value));
            }
        }
    }
}
