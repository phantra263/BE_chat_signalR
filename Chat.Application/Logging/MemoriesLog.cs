using System;

namespace Chat.Application.Logging
{
    public class MemoriesLog : IMemoriesLog
    {
        public double GetResourceMemories(string unit)
        {
            var memoryBefore = GC.GetTotalMemory(false);
            var result = 0.0;
            switch (unit)
            {
                case "kb":
                    result = memoryBefore / 1024;
                    break;
                case "mb":
                    result = memoryBefore / 1024 / 1024;
                    break;
                case "gb":
                    result = memoryBefore / 1024 / 1024 / 1024;
                    break;
                default:
                    result = memoryBefore;
                    break;
            }

            return result;
        }
    }
}
