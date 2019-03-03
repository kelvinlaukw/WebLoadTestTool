using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestProgram.Class
{
    class UserGroup
    {
        public int groupId;
        public int noOfUser;
        public int deferStartTime;
        public Boolean repeatTask;

        public UserGroup(int groupId, int noOfUser, int deferStartTime, Boolean repeatTask)
        {
            this.groupId = groupId;
            this.noOfUser = noOfUser;
            this.deferStartTime = deferStartTime;
            this.repeatTask = repeatTask;
        }
    }
}
