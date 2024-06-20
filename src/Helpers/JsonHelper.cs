using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingCombo.src.Helpers
{

    public class RankRange
    {
        public float NONE { get; set; }
        public float C { get; set; }
        public float B { get; set; }
        public float A { get; set; }
        public float S { get; set; }
        public float SS { get; set; }
    }



    public class Config
    {
        public int[] KeycodeEscaper { get; set; }
        public string SelectedFrontSet {  get; set; }

        public RankRange RankRange { get; set; }

    }
}
