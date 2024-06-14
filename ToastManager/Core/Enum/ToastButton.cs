using System;

namespace ToastManager.Core
{
    [Flags]
    public enum ToastButton
    {
        None = 0,
        Ok = 1,
        Yes = 1 << 1,
        No = 1 << 2,
        Cancel = 1 << 3,
    }
}
