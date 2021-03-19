using System;
using System.Collections.Generic;
using System.Text;

namespace Prb.Compositie.Voorbeeld1.Core.Entities
{
    public class Group
    {
        public string GroupName { get; set; }
        public Group()
        { }
        public Group(string groupName)
        {
            GroupName = groupName;
        }
        public override string ToString()
        {
            return $"{GroupName}";
        }
    }
}
