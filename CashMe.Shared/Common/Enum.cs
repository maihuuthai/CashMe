﻿using System.ComponentModel;

namespace CashMe.Shared.Common
{
    public enum StatusCashout
    {
       [Description("Cancel")]
        Cancel = 0,
        [Description("Complete")]
        Complete = 1,
        [Description("Pending")]
        Pending = 2,
    }
    public enum StatusMessage
    {
        [Description("Chưa đọc")]
        Unread = 0,
        [Description("Đã đọc")]
        Read = 1,
    }
    public enum CookieSecureOption
    {
        SameAsRequest,
        Never,
        Always
    }
}

