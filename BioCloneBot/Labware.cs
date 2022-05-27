using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioCloneBot
{
    public class Labware
    {
        protected double[] dimensions;
        protected double[] start_location;
        public int row { get; set; }
        public int col { get; set; }
        public double maxVolume { get; set; }
        public double[,] volumes { get; set; }
        public string labwareType { get; set; }

        public Labware()
        {

        }
    }
}
