using Game.Scripts.Main.Runtime.UI.UICommon;
using UnityEngine;

namespace Game.Scripts.Main.Runtime.UI.UIForm
{
    public class FormComponentUserData
    {
        public FormComponent FormComponent { get; }
        public UIFormId FormId { get; }

        public FormComponentUserData(FormComponent formComponent, UIFormId uiFormId)
        {
            FormComponent = formComponent;
            FormId = uiFormId;
        }
    }
} 