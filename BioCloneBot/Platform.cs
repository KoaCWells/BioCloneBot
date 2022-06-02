using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioCloneBot
{
    public class Platform
    {
        private int labwareCount;
        private int[] selectedPosition;
        private double volumeInTip;
        private double tipCapacity;
        private double xMax;
        private double yMax;
        private double zMax;
        private double[] trashLocation;
        private double[] pumpPosition;
        private bool tipAttached;
        private bool[] labwareOccupied;
        private Labware[] labwares;
        private List<Operation> operations;

        //to be added at a later time
        //private Thermocycler thermocycler;
        public Platform(int labwareCount)
        {
            this.labwareCount = labwareCount;
            selectedPosition = new int[2] { -1, -1 };
            volumeInTip = 0.0;
            tipCapacity = 0.0;
            xMax = 406.0;
            yMax = 406.0;
            zMax = 60.0;
            trashLocation = new double[2] { 292.5, 78.5 };
            pumpPosition = new double[2] { -1.0, -1.0 };
            tipAttached = false;
            labwareOccupied = new bool[this.labwareCount];
            labwares = new Labware[this.labwareCount];
            operations = new List<Operation>();

            for ( int i = 0; i < this.labwareCount; i++)
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
        public double TipCapacity
        {
            get { return tipCapacity; }
            set { tipCapacity = value; }
        }
        public double XMax
        {
            get { return xMax; }
 
        }
        public double YMax
        {
            get { return yMax; }

        }
        public double ZMax
        {
            get { return zMax; }

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
        public int[] SelectedPosition
        {
            get { return selectedPosition; }
            set { selectedPosition = value; }
        }
        public bool TipAttached
        {
            get { return tipAttached; }
            set { tipAttached = value; }
        }
        public bool[] LabwareOccupied
        {
            get { return labwareOccupied; }
        }
        public Labware[] Labwares
        {
            get { return labwares; }
        }
        public List<Operation> Operations
        {
            get { return operations; }
        }
        public void AddLabware(int position, string labwareType)
        {
            double[] topLeftCorner = new double[2];

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
            labwareOccupied[position] = true;

            //Sets distance from home point (0,0) to the top left corner of the labware
            if(position == 0)
            {
                topLeftCorner[0] = 52.0;
                topLeftCorner[1] = 136.0;
                labwares[position].TopLeftCorner = topLeftCorner;
            }
            else if(position == 1)
            {
                topLeftCorner[0] = 228.0;
                topLeftCorner[1] = 136.0;
                labwares[position].TopLeftCorner = topLeftCorner;
            }
            else if(position == 2)
            {
                topLeftCorner[0] = 52.0;
                topLeftCorner[1] = 286.25;
                labwares[position].TopLeftCorner = topLeftCorner;
            }
            else if(position == 3)
            {
                topLeftCorner[0] = 228.0;
                topLeftCorner[1] = 286.25;
                labwares[position].TopLeftCorner = topLeftCorner;
            }
        }
        public void RemoveLabware(int position)
        {
            labwares[position] = null;
            labwareOccupied[position] = false;
        }
    }
}
