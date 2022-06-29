using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioCloneBot
{
    public class Operation
    {
        private List<string> steps = new List<string>();
        private string command;
        private double volumeMoved;
        private double xLocation;
        private double yLocation;
        private double zLocation;
        private double xDest;
        private double yDest;
        private double zDest;
        private double xDir;
        private double yDir;
        private double zDir;
        private int labwarePosition;
        private int[] selectedReservoirPosition; //row and column of labware selected
        private Labware labware; //source or destination labware

        public List<string> Steps
        {
            get { return steps; }
            set { steps = value; }
        }

        public Operation(string command)
        {
            this.command = command;
            
            if(this.command == "home")
            {
                steps.Add("#0000%");
                steps.Add("#0001110012.00033.00000.00%");
                steps.Add("#0011025.00%");
                xLocation = 0.0;
                yLocation = 0.0;
                zLocation = 0.0;
            }
        }

        public Operation(string command, double xLocation, double yLocation, double zLocation, 
            double xDest, double yDest, double zDest, int labwarePosition, int[] selectedReservoirPosition, Labware labware)
        {
            this.command = command;
            if (this.command == "gettip")
            {
                this.command = command;
                this.xLocation = xLocation;
                this.yLocation = yLocation;
                this.zLocation = zLocation;
                this.xDest = xDest;
                this.yDest = yDest;
                this.zDest = zDest;
                //for saving operations only
                this.labwarePosition = labwarePosition;
                this.selectedReservoirPosition = selectedReservoirPosition;
                this.labware = labware;

                movePump(xDest, yDest, zLocation);
                xLocation = xDest;
                yLocation = yDest;
                movePump(xLocation, yLocation, zDest - 30.0);
                zLocation = zDest - 20.0;
                movePump(xLocation, yLocation, zLocation + 1.0);
                zLocation = zLocation + 1.0;
                movePump(xLocation, yLocation, 0.0);
                zLocation = 0.0;
            }
        }

        public Operation(string command, double xLocation, double yLocation, double zLocation, 
            double xDest, double yDest)
        {
            this.command = command;
            if (this.command == "removetip")
            {
                this.command = command;
                this.xLocation = xLocation;
                this.yLocation = yLocation;
                this.zLocation = zLocation;
                this.xDest = xDest;
                this.yDest = yDest;
                this.zDest = -1.0;

                //movePump(xLocation, yLocation, 0.0);
                //zLocation = 0.0;
                movePump(xDest, yDest, zLocation);
                xLocation = xDest;
                yLocation = yDest;
                movePump(xLocation, yLocation, 40.0);
                zLocation = 40.0;
                removeTip();
                movePump(xLocation, yLocation, 0.0);
                zLocation = 0.0;
                aspirateVolume(25);
            }
        }
        //aspirate/dispense device operation
        public Operation(string command, double volumeMoved, double xLocation, double yLocation, double zLocation,
            double xDest, double yDest, double zDest, int labwarePosition, int[] selectedReservoirPosition, Labware labware)
        {
            this.command = command;
            if (command == "aspirate")
            {
                this.volumeMoved = volumeMoved;
                this.xLocation = xLocation;
                this.yLocation = yLocation;
                this.zLocation = zLocation;
                this.xDest = xDest;
                this.yDest = yDest;
                this.zDest = zDest;

                //for saving operations only
                this.labwarePosition = labwarePosition;
                this.selectedReservoirPosition = selectedReservoirPosition;
                this.labware = labware;

                movePump(xDest, yDest, zLocation);
                xLocation = xDest;
                yLocation = yDest;
                movePump(xLocation, yLocation, zDest);
                zLocation = zDest;
                aspirateVolume(volumeMoved);
                movePump(xLocation, yLocation, 0.0);
                zLocation = 0.0;
            }
            else if(command == "dispense")
            {
                this.volumeMoved = volumeMoved;
                this.xLocation = xLocation;
                this.yLocation = yLocation;
                this.zLocation = zLocation;
                this.xDest = xDest;
                this.yDest = yDest;
                this.zDest = zDest;

                //for saving operations only
                this.labwarePosition = labwarePosition;
                this.selectedReservoirPosition = selectedReservoirPosition;
                this.labware = labware;

                movePump(xDest, yDest, zLocation);
                xLocation = xDest;
                yLocation = yDest;
                movePump(xLocation, yLocation, zDest);
                zLocation = zDest;
                dispenseVolume(volumeMoved);
                movePump(xLocation, yLocation, 0.0);
                zLocation = 0.0;
            }
        }

        private void movePump(double xDest, double yDest, double zDest)
        {
            string x = "";
            string y = "";
            string z = "";
            int xDir = 0;
            int yDir = 0;
            int zDir = 0;
            double xTravel = 0;
            double yTravel = 0;
            double zTravel = 0;

            if (xDest > xLocation)
            {
                xDir = 1;
                xTravel = xDest - xLocation;
                x = convertToTwoDecimalDouble(xTravel);
            }
            else if (xDest < xLocation)
            {
                xDir = 0;
                xTravel = xLocation - xDest;
                x = convertToTwoDecimalDouble(xTravel);
            }
            else
            {
                xDir = 0;
                xTravel = 0;
                x = "000.00";
            }
            if (yDest > yLocation)
            {
                yDir = 1;
                yTravel = yDest - yLocation;
                y = convertToTwoDecimalDouble(yTravel);
            }
            else if (yDest < yLocation)
            {
                yDir = 0;
                yTravel = yLocation - yDest;
                y = convertToTwoDecimalDouble(yTravel);
            }
            else
            {
                yDir = 0;
                yTravel = 0;
                y = "000.00";
            }

            if (zDest > zLocation)
            {
                zDir = 1;
                zTravel = zDest - zLocation;
                z = convertToTwoDecimalDouble(zTravel);
            }
            else if (zDest < zLocation)
            {
                zDir = 0;
                zTravel = zLocation - zDest;
                z = convertToTwoDecimalDouble(zTravel);
            }
            else
            {
                zDir = 0;
                zTravel = 0;
                z = "000.00";
            }
            xLocation = xDest;
            yLocation = yDest;
            zLocation = zDest;

            steps.Add("#0001" + xDir + yDir + zDir + x + y + z + "%");
        }
        private void removeTip()
        {
            steps.Add("#0010%");
        }
        private void aspirateVolume(double volumeAspirated)
        {
            string volume = convertToTwoDecimalDouble(volumeAspirated);
            steps.Add("#0011" + volume + "%");
        }
        private void dispenseVolume(double volumeDispensed)
        {
            string volume = convertToTwoDecimalDouble(volumeDispensed);
            steps.Add("#0100" + volume + "%");
        }

        private string convertToTwoDecimalDouble(double input)
        {
            string conversion = input.ToString();
            string leftSide = "";
            string rightSide = "";
            char character = ' ';
            int decimalLocation = 0;
            bool passedDecimal = false;

            for (int i = 0; i < conversion.Length; i++)
            {
                character = conversion[i];
                if (character == '.')
                {
                    decimalLocation = i;
                    passedDecimal = true;
                }
                else if (character != '.' && passedDecimal == false)
                {
                    leftSide += conversion[i];
                }
                else if(passedDecimal == true)
                {
                    rightSide += conversion[i];
                }
            }

            if(leftSide.Length == 1)
            {
                leftSide = "00" + leftSide;
            }
            else if(leftSide.Length == 2)
            {
                leftSide = "0" + leftSide;
            }

            if(rightSide.Length == 0)
            {
                rightSide += "00";
            }
            else if(rightSide.Length == 1)
            {
                rightSide += "0";
            }

            conversion = leftSide + "." + rightSide;
            return conversion;
        }
    }
}
