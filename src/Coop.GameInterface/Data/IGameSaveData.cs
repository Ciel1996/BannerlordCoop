﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coop.GameInterface.Data
{
    public interface IGameSaveData
    {
        byte[] Data { get; }
    }
}
