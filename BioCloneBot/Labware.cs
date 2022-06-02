using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioCloneBot
{
    public class Labware
    {
        protected int row;
        protected int col;
        protected double maxVolume;
        protected double reservoirSeparation;
        protected double[] dimensions;
        protected double[] topLeftCorner;
        protected double[] startLocation;
        protected double[,] volumes;
        protected string labwareType;
        public Labware()
        {

        }
        public double MaxVolume 
        {
            get { return maxVolume; }
        }
        public double ReservoirSeparation
        {
            get { return reservoirSeparation; }
        }
        public double[] Dimensions
        {
            get { return dimensions; }
        }
        public double[] TopLeftCorner
        {
            get { return topLeftCorner; }
            set { topLeftCorner = value; }
        }
        public double[] StartLocation
        {
            get { return startLocation; }
            set { startLocation = value; }
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
    }
}
