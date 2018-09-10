using System.Collections.Generic;
using System.ComponentModel;

namespace CodefirstEntityFramework
{
    public class Branch
    {
        public int Id { get; set; }
        [DisplayName("Branş Adı")]
        public string Name { get; set; }

        public List<Teacher> Teachers { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}