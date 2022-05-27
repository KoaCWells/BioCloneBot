using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioCloneBot
{
    internal class Platform
    {
        private int labwareCount;
        private double volumeInTip;
        private double[] trashLocation;
        private double[] pumpPosition;
        private bool tipAttached;
        private bool[] labwareOccupied;
        private Labware[] labwares;

        //to be added at a later time
        //private Thermocycler thermocycler;

        public Platform()
        {
            labwareCount = 4;
            volumeInTip = 0.0;
            trashLocation = new double[2] { 292.5, 78.5 };
            pumpPosition = new double[2] { 0.0, 0.0 };
            tipAttached = false;

            for( int i = 0; i < labwareCount; i++)
            {
                labwareOccupied[i] = false;
                labwares[i] = null;
            }
        }

        public double VolumeInTip
        {
            get { return volumeInTip; }
            set { volumeInTip = value; }
        }
        public double[] TrashLocation
        {
            get { return trashLocation; }
        }

        public double[] PumpPosition
        {
            get { return pumpPosition; }
            set { pumpPosition = value; }
        }

        public bool TipAttached
        {
            get { return tipAttached; }
            set { tipAttached = value; }
        }

        public void AddLabware(int position, string labwareType, bool value)
        {
            if(labwareType == "wellplate")
            {
                labwares[position] = new Wellplate();
            }
            else if(labwareType == "tubestand")
            {
                labwares[position] = new Tubestand();
            }
            else if(labwareType == "tipbox")
            {
                labwares[position] = new Tipbox();
            }
            labwareOccupied[position] = value;
        }

        public void RemoveLabware(int position, bool value)
        {
            labwares[position] = null;
            labwareOccupied[position] = false;
        }
    }
}
