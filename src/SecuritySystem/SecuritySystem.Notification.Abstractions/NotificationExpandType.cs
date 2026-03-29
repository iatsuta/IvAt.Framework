namespace SecuritySystem.Notification;

/// <summary>
/// Константы, описывающие правила для вычисления принципалов по роле
/// </summary>
public enum NotificationExpandType
{
    DirectOrEmpty = 0,

    Direct = 1,

    DirectOrFirstParent = 2,

    DirectOrFirstParentOrEmpty = 3,

    All = 4
}


//[Flags]
//public enum NotificationExpandType
//{
//    Direct = 0,

//    Empty = 1,

//    FirstParent = 2,

//    All = Direct | Empty | FirstParent
//}
