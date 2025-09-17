namespace BTAF.Lib.NativeWrapper
{
    internal enum ServiceInfoLevel : uint
    {
        Description = 1,
        FailureActions = 2,
        DelayedAutoStartInfo = 3,
        FailureActionsFlag = 4,
        ServiceSidInfo = 5,
        RequiredPrivilegesInfo = 6,
        PreshutdownInfo = 7,
        TriggerInfo = 8,
        PreferredNode = 9,
        LaunchProtected = 12
    }

}
