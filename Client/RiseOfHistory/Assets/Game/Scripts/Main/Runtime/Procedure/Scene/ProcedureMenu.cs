using Game.Scripts.Main.Runtime.Game;
using Game.Scripts.Main.Runtime.SaveData;
using Game.Scripts.Main.Runtime.UI.UICommon;
using Game.Scripts.Main.Runtime.UI.UIForm;
using GameFramework;
using GameFramework.FileSystem;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = Game.Scripts.Main.Runtime.Base.GameEntry;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Game.Scripts.Main.Runtime.Procedure.Scene
{
    public class ProcedureMenu : ProcedureBase
    {
        private int m_NextSceneId = 0;
        private readonly FormComponent formComponent = new FormComponent();
        private readonly List<HeadData> headData = new List<HeadData>();

        public override bool UseNativeDialog => false;

        public readonly int SaveMaxCount = 2;

        public void LoadGame()
        {
            m_NextSceneId = GameEntry.Config.GetInt("Scene.Home");
        }

        public void StartGame()
        {
            m_NextSceneId = GameEntry.Config.GetInt("Scene.Create");
        }

        public void OpenUIForm(UIFormId form)
        {
            formComponent.OpenUIForm(form);
        }

        public void RemoveUIForm(UIFormId form)
        {
            formComponent.RemoveUIForm(form);
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_NextSceneId = 0;

            formComponent.AddForm(UIFormId.MenuForm);
            formComponent.OnEnter(procedureOwner);

            GameEntry.ModuleComponent.ResetModule();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            formComponent.OnLeave(procedureOwner, isShutdown);
        }


        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_NextSceneId <= 0) return;
            procedureOwner.SetData<VarInt32>("NextSceneId", m_NextSceneId);
            procedureOwner.SetData<VarByte>("GameMode", (byte)GameMode.Survival);
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }

        public void LoadHeadData()
        {
            for (var i = 0; i < SaveMaxCount; ++i)
            {
                var fileSystems = GameEntry.FileSystemComponent.CreateFileSystem("GameSaves/" + i, "HeadData.idx");

                var bytes = fileSystems?.ReadFile("GameSaves");

                if (bytes == null)
                {
                    continue;
                }

                var json = Encoding.UTF8.GetString(bytes);
                var data = Utility.Json.ToObject<HeadData>(json);
                headData.Add(data);
            }
        }

        public bool HasHeadData(int index)
        {
            return headData.Any(data => data.Index == index);
        }
    }
}
