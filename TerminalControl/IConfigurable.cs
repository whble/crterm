﻿using System;

namespace TerminalControl
{
    public interface IConfigurable
    {
        string Name { get; }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class ConfigItem : Attribute
    {
    }
}