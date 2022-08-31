﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInterface.Services.Example.Messages
{
    public readonly struct ExampleCommand
    {

    }

    /// <remarks>
    /// Typically a reply to a command
    /// Do not worry about error handling on commands
    /// as it will be assumed that every command 100%
    /// works.
    /// </remarks>
    public readonly struct ExampleEvent
    {

    }
}
