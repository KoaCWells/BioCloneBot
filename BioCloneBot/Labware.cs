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
        protected double[] startLocation;
        protected int row;
        protected int col;
        protected double maxVolume;
        protected double[,] volumes;
        protected string labwareType;

        public double MaxVolume 
        {
            get { return maxVolume; }
        }

        public double[,] Volumes 
        { 
            get { return volumes; }
            set { volumes = value; }
        }
        public string LabwareType 
        {
            get { return labwareType; }
        }

        public Labware()
        {

        }
    }
}
