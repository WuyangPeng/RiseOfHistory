namespace Game.Scripts.Main.Runtime.UI.UICommon
{
    /// <summary>
    /// 界面编号。
    /// </summary>
    public enum UIFormId : byte
    {
        Undefined = 0,

        /// <summary>
        /// 弹出框。
        /// </summary>
        DialogForm = 1,

        /// <summary>
        /// 主菜单。
        /// </summary>
        MenuForm = 100,

        /// <summary>
        /// 设置。
        /// </summary>
        SettingForm = 101,

        /// <summary>
        /// 关于。
        /// </summary>
        AboutForm = 102,

        /// <summary>
        /// 载入。
        /// </summary>
        LoadForm = 103,


        /// <summary>
        /// 下部菜单。
        /// </summary>
        BottomForm = 203,

        /// <summary>
        /// 上部菜单。
        /// </summary>
        UpperForm = 204,

        /// <summary>
        /// 左部菜单。
        /// </summary>
        LeftForm = 205,

        /// <summary>
        /// 左部菜单。
        /// </summary>
        RightForm = 206,
    }
}
