using System;
using System.Threading;

namespace HC.Core.Services
{
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random _random;

        public static Random Random => _random ??=
            new Random(unchecked(Environment.TickCount * 0x1F + Thread.CurrentThread.ManagedThreadId));
    }
}