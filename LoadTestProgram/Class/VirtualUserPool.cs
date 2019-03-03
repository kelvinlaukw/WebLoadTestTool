using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadTestProgram.Class;

namespace LoadTestProgram.Class
{
    class VirtualUserPool
    {
        public int deferStartTime = 0;
        public Boolean repeatTask = false;
        public VirtualUser[] VirtualUserList;

        public VirtualUserPool(VirtualUser[] VirtualUserList, int deferStartTime, Boolean repeatTask)
        {
            this.VirtualUserList = VirtualUserList;
            this.deferStartTime = deferStartTime;
            this.repeatTask = repeatTask;
        }
    }
}
