﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurrency
{
    public static class Chapter1
    {

        public static async Task DoSomethingAsync()
        {
            int val = 13;
            val *= 2;
            await Task.Delay(TimeSpan.FromSeconds(1));

            Trace.WriteLine(val);

        }
    }
}
