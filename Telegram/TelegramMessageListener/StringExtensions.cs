﻿using System;

namespace TelegramMessageListener;

public static class StringExtensions
{
    /// <summary>
    /// Truncates string so that it is no longer than the specified number of characters.
    /// </summary>
    /// <param name="str">String to truncate.</param>
    /// <param name="length">Maximum string length.</param>
    /// <returns>Original string or a truncated one if the original was too long.</returns>
    public static string Truncate(this string str, int length)
    {
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be >= 0");
        }

        if (str == null)
        {
            return null;
        }

        if (str.Length < length)
            return str;

        return str.Substring(0, length - 3) + "...";
    }
}