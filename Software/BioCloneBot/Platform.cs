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
        private int numberOfOperations;
        private int[] selectedPosition;
        private double volumeInTip;
        private double tipCapacity;
        private double xLocation;
        private double yLocation;
        private double zLocation;
        private double xMax;
        private double yMax;
        private double zMax;
        private double[] trashLocation;
        private bool tipAttached;
        private bool[] labwareOccupied;
        private Labware[] labwares;
        private List<string> protocolList;
        private List<Operation> operations;

        public Platform(int labwareCount)
        {
            this.labwareCount = labwareCount;
            numberOfOperations = 0;
            selectedPosition = new int[2] { -1, -1 };
            volumeInTip = 0.00;
            tipCapacity = 0.00;
            xLocation = -1.0;
            yLocation = -1.0;
            zLocation = -1.0;
            xMax = 406.00;
            yMax = 406.00;
            zMax = 135.00;
            trashLocation = new double[2] { 292.00, 329.25 };
            tipAttached = false;
            labwareOccupied = new bool[this.labwareCount];
            labwares = new Labware[this.labwareCount];
            protocolList = new List<string>();
            operations = new List<Operation>();

            for (int i = 0; i < this.labwareCount; i++)
            {
                labwareOccupied[i] = false;
                labwares[i] = null;
            }
        }
        public int LabwareCount
        {
            get { return labwareCount; }
        }
        public int NumberOfOperations
        {
            get { return numberOfOperations; }
            set { numberOfOperations = value; }
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
        public double XLocation
        {
            get { return xLocation; }
            set { xLocation = value; }
        }
        public double YLocation
        {
            get { return yLocation; }
            set { yLocation = value; }
        }
        public double ZLocation
        {
            get { return zLocation; }
            set { zLocation = value; }
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
        public List<string> ProtocolList
        {
            get { return protocolList; }
            set { protocolList = value; }
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
            //topLeftCorner[0] refers to the x location
            //topLeftCorner[1] refers to the y location

            //labware position 1
            if (position == 0)
            {
                topLeftCorner[0] = 52.10;
                topLeftCorner[1] = 245.00;
                labwares[position].TopLeftCorner = topLeftCorner;
            }
            //labware position 2
            else if (position == 1)
            {
                topLeftCorner[0] = 228.00;
                topLeftCorner[1] = 243.80;
                labwares[position].TopLeftCorner = topLeftCorner;
            }
            //labware position 3
            else if (position == 2)
            {
                topLeftCorner[0] = 51.50;
                topLeftCorner[1] = 121.25;
                labwares[position].TopLeftCorner = topLeftCorner;
            }
            //labware position 4
            else if (position == 3)
            {
                topLeftCorner[0] = 227.50;
                topLeftCorner[1] = 119.75;
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
