using System.Collections.Generic;
using Laaab6.Controllers;

namespace Laaab6.ClientApp.Model
{
    public class BackpackTask
    {
        public string TaskName { get; set; }
        public int BackpackWeight { get; set; }
        public List<Thing> Things { get; set; }
    }
}
