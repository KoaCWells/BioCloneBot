/*****************************************************************************
* __________.__       _________ .__                      __________        __   
* \______   \__| ____ \_   ___ \|  |   ____   ____   ____\______   \ _____/  |_ 
*  |    |  _/  |/  _ \/    \  \/|  |  /  _ \ /    \_/ __ \|    |  _//  _ \   __\
*  |    |   \  (  <_> )     \___|  |_(  <_> )   |  \  ___/|    |   (  <_> )  |  
*  |______  /__|\____/ \______  /____/\____/|___|  /\___  >______  /\____/|__|  
*         \/                  \/                 \/     \/       \/             
* ASCII art source: https://patorjk.com/software/taag/#p=display&f=Graffiti&t=BioCloneBot
* 
* -Author: Koa Wells
* -Github: TBD
* -Contact: koa.wells@gmail.com
* 
* This code was designed to be easily modified by those who want to expand upon 
* the BioCloneBot device.
******************************************************************************/
#include <LiquidCrystal.h>
#include <math.h>

//Refer to Pololu website for documentation on DRV8825 stepper motor https://www.pololu.com/product/2133
#define AXIS_STEPS 200 //Ender 3 Pro step angle 1.8 degrees/200 steps
#define PUMP_STEPS 400//Pump stepper motor angle 0.9 degrees/400 steps
/**************************************************************
* Arduino pins for controlling each stepper motor
* -MS0, MS1, and MS2 are for setting microstep resolution of the driver
* -SLEEP is for enabling/disabling the steeper motor - LOW for sleep/HIGH for enable
* -STEP is the pin for the PWM signal for making the motor move
* -DIR for controlling direction of the stepper motor
* 
* 3-bits for changing microstep resolution (MS2, MS1, MS0 pins)
* -000 - full step
* -001 - half step
* -010 - 1/4 step
* -011 - 1/8 step
* -100 - 1/16 step
* -101/110/111 - 1/32 step
***************************************************************/
//X motor driver pins
#define X_MS0 23
#define X_MS1 25
#define X_MS2 27
#define X_SLEEP 29
#define X_STEP 31
#define X_DIR 33

//Y motor driver pins
#define Y_MS0 22
#define Y_MS1 24
#define Y_MS2 26
#define Y_SLEEP 28
#define Y_STEP 30
#define Y_DIR 32

//Z motor driver pins
#define Z_MS0 35
#define Z_MS1 37
#define Z_MS2 39
#define Z_SLEEP 41
#define Z_STEP 43
#define Z_DIR 45

//PUMP motor driver pins
#define P_MS0 34
#define P_MS1 36
#define P_MS2 38
#define P_SLEEP 40
#define P_STEP 42
#define P_DIR 44

//stepper motor limit switch detection for hardware interrupt/homing
#define X_LIMIT 18
#define Y_LIMIT 19
#define Z_LIMIT 20
#define P_LIMIT 21

//maximum volume for syringe aspiration
#define X_MAX 400
#define Y_MAX 400
#define Z_MAX 100
#define MAX_VOL 200 

//LCD screen for feedback and troubleshooting
#define VDD 14
#define RS 15
#define EN 16
#define D4 47
#define D5 49
#define D6 51
#define D7 53

/**************************************************************
* Feedback LEDs
* -MOVE for indicating X/Y/Z motion
* -PUMP for indicating pump motion
* -CONN for indicating successful serial connection with host PC
***************************************************************/
#define MOVE_LED 46
#define PUMP_LED 48
#define CONN_LED 50

//LCD object definition
LiquidCrystal lcd(RS, EN, D4, D5, D6, D7);

/* Global variables for device control
 * axis_step_delay - time in microseconds in between HIGH/LOW pulses for the X,Y,Z motors
 * pump_step_delay - time in microseconds in between HIGH/LOW pulses for the P motors
 * axis_rev_steps - steps per revolution of 3-axis motors
 * pump_rev_steps - steps per revolution of pump motor
 * homing - 0 if device is not in homing mode; limit switches are for emergency stops/1 if device is in homing mode
 * tip_attached - 0 if the pump does not have a tip/1 if the pump does have a tip
 * x_step_dist - distance traveled per step in the X-axis
 * y_step_dist - distance traveled per step in the Y-axis
 * z_step_dist - distance traveled per step in the Z-axis
 * p_step_dist - distance traveled per step in the P-axis
 * aspiriate_vol - volume to be aspirated by pump
 * dispense_vol - volume to be dispensed by pump + 50 ul of leading air gap
 * x_dist - pump travel destination on x-axis
 * y_dist - pump travel destination on y-axis
 * z_dist - pump travel destination on z-axis
 * host_message - raw message from the host including 4-bit command and command parameters
 * curr_command - first 4 bits of host_message for determining what function the device should perform
 * prev_command - previous command that was run
 */
int axis_step_delay;
int pump_step_delay;
int axis_rev_steps;
int pump_rev_steps;
int homing = 0;
double x_step_dist;
double y_step_dist;
double z_step_dist;
double p_step_dist;
double aspirate_vol;
double dispense_vol;
double x_dist;
double y_dist;
double z_dist;
char distance[5];
char *ptr_end;
String host_message;
String curr_command;
String prev_command;

void setup() {
  Serial.begin(9600);

  //sets all stepper motor control pins to output
  for(int i = 22; i <= 45; i++){
    pinMode(i, OUTPUT);
  }

  digitalWrite(P_MS0, LOW);
  digitalWrite(P_MS1, LOW);
  digitalWrite(P_MS2, LOW);
  pump_step_delay = 1000; //time delay between HIGH/LOW digital control signal in microseconds
  pump_rev_steps = 400; //steps per revolution
  p_step_dist = 157E-6; //mm per step
  
  //defines indicator LEDs: MOVE for carriage movement, PUMP for pump movement, and CONN for a successful serial connection
  pinMode(MOVE_LED,OUTPUT);
  pinMode(PUMP_LED,OUTPUT);
  pinMode(CONN_LED,OUTPUT);
  
  //define limit switch pins and pullup to high voltage
  pinMode(X_LIMIT, INPUT_PULLUP);
  pinMode(Y_LIMIT, INPUT_PULLUP);
  pinMode(Z_LIMIT, INPUT_PULLUP);
  pinMode(P_LIMIT, INPUT_PULLUP);

  //defines output pin for powering the LCD dadta lines
  pinMode(VDD, OUTPUT);

  //sets default direction of stepper motors to point towards the limit switches
  digitalWrite(X_DIR, LOW);
  digitalWrite(Y_DIR, LOW);
  digitalWrite(Z_DIR, LOW);
  digitalWrite(P_DIR, LOW);

  //attach interupts for the limit switches
  attachInterrupt(digitalPinToInterrupt(X_LIMIT), xLimitPressed, FALLING);
  attachInterrupt(digitalPinToInterrupt(Y_LIMIT), yLimitPressed, FALLING);
  attachInterrupt(digitalPinToInterrupt(Z_LIMIT), zLimitPressed, FALLING);
  attachInterrupt(digitalPinToInterrupt(P_LIMIT), pLimitPressed, FALLING);

  digitalWrite(VDD, HIGH);
  
  // set up the LCD's number of columns and rows:
  lcd.begin(16, 2);
  // Print a message to the LCD.
  lcd.print("Waiting for host connection.");
    
   //waits for message from serial to establish connection with host computer
  while(true){
    host_message = Serial.readStringUntil('%');
    if(host_message == "ping"){
      lcd.setCursor(0, 0);
      lcd.clear();
      lcd.print("Host connected.");
      digitalWrite(CONN_LED, HIGH);
      Serial.print("pong");
      break;
    }
  }
}

void loop() {
  while(Serial.available() > 0){    
    host_message = Serial.readStringUntil('%');
    lcd.setCursor(0,1);
    lcd.print(host_message);

    //decodes first 4-bits of host_message to determine command
    for(int i = 0; i < 4; i++){
      curr_command += host_message[i];
    }
    
    if(curr_command == "0000"){ //home motors
      homeCarriage();
      Serial.print("homed");
    }
    else if(curr_command == "0001"){ //move pump to x, y, z location
      for(int i = 6; i < 13; i++){
        distance[i-6]= host_message[i];
      }
      x_dist = strtod(distance, &ptr_end);

      for(int i = 13; i < 19; i++){
        distance[i-13]= host_message[i];
      }
      y_dist = strtod(distance, &ptr_end);

      for(int i = 19; i < 25; i++){
        distance[i-19]= host_message[i];
      }
      z_dist = strtod(distance, &ptr_end);
      
      //x_dist = host_message[7]*100 + host_message[8]*10 + host_message[9]*1 + host_message[11]*.1 + host_message[12]*.01;
      //y_dist = host_message[13]*100 + host_message[14]*10 + host_message[15]*1 + host_message[17]*.1 + host_message[18]*.01;
      //z_dist = host_message[19]*100 + host_message[20]*10 + host_message[21]*1 + host_message[23]*.1 + host_message[24]*.01;
      lcd.clear();
      lcd.setCursor(0,1);
      lcd.print(host_message.charAt(4));
      movePump(host_message.charAt(4),host_message.charAt(5),host_message.charAt(6),x_dist,y_dist,z_dist);
      Serial.print("moved");
    }
    else if(curr_command == "0010"){ //get tip
      getTip();
      Serial.print("loaded");
    }
    else if(curr_command == "0011"){ //remove tip
      ejectTip();
      Serial.print("unloaded");
    }
    else if(curr_command == "0100"){ //aspirate volume
      for(int i = 4; i < 10; i++){
        aspirate_vol += curr_command[i]; //000.00
      }
      aspirate(aspirate_vol);
      Serial.print("aspirated");
    }
    else if(curr_command == "0101"){ //dispense volume
      dispense(0);
      Serial.print("dispensed");
    }
    else if(curr_command == "1111"){ //disable stepper motors
      disableMotors();
      Serial.print("disabled");
    }
  }
  prev_command = curr_command;
  curr_command = "";
}

/*
 * List of possible device commands
 * -0000 home device: homes x, y, z, and p stepper motors
 * -0001 move pump: move carriage to x, y, z location (XXX.XX location in mm)
 * -0010 aspirate volume: aspirate volume in ul (XXX.XX ul)
 * -0011 dispense volume: dispense volume in ul + 50 ul (XXX.XX ul)
 * -1110 enable stepper motors: sets sleep pin of all motor drivers to HIGH
 * -1111 disable stepper motors: sets sleep pin of all motor drivers to LOW
 */

 /*homeCarriage
  * homeCarriage resets all stepper motors to their homed position.
  * Works by enabling each motor driver by setting each corresponding
  * SLEEP pin to HIGH, then setting the DIR pin to match the direction
  * of the limit switch. Each motor then moves until the limit switch
  * is pressed and backs off until the switch is no longer pressed.
  * This is the homed position.
  */
void homeCarriage(){
  enableMotors(); //enables all stepper motor drivers
  setMicrostepping(32); //sets microstepping of all drivers to 1/32
  homing = 1; //disables limit switch overrupts for the purpose of homing
  int count = 0;
  //sets initial direction of all motors towards their limit switches
  digitalWrite(X_DIR, LOW);
  digitalWrite(Y_DIR, LOW);
  digitalWrite(Z_DIR, LOW);
  digitalWrite(P_DIR, HIGH);

  //home x-axis
  while(digitalRead(X_LIMIT)){
    digitalWrite(X_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(X_STEP,LOW);
    delayMicroseconds(axis_step_delay);
  }

//  lcd.clear();
//  lcd.setCursor(0,1);
//  lcd.print(count);
  digitalWrite(X_DIR, HIGH);
  while(!digitalRead(X_LIMIT)){
    digitalWrite(X_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(X_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  digitalWrite(X_DIR, LOW);

  //home y-axis
  while(digitalRead(Y_LIMIT)){
    digitalWrite(Y_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Y_STEP,LOW);
    delayMicroseconds(axis_step_delay);
  }
  digitalWrite(Y_DIR, HIGH);
  while(!digitalRead(Y_LIMIT)){
    digitalWrite(Y_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Y_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  digitalWrite(Y_DIR, HIGH);

  //home z-axis
  while(digitalRead(Z_LIMIT)){
    digitalWrite(Z_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Z_STEP,LOW);
    delayMicroseconds(axis_step_delay);
  }
  digitalWrite(Z_DIR, HIGH);
  while(!digitalRead(Z_LIMIT)){
    digitalWrite(Z_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Z_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  digitalWrite(Z_DIR, LOW);

  //home p-axis
  while(digitalRead(P_LIMIT)){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP,LOW);
    delayMicroseconds(pump_step_delay);
  }
  digitalWrite(P_DIR, LOW);
  while(!digitalRead(P_LIMIT)){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }
  //back off until pump ejector is flush with base
  for(int i = 0; i < 800; i++){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }

  digitalWrite(P_DIR, LOW);

  //sets homing to 0 re-enabling the normal functionality of the limit switches as emergency stops
  //and sets carriage location to (0.0, 0.0, 0.0) and syringe volume to 0
  homing = 0;
}

void movePump(int x_direction, int y_direction, int z_direction, double x_dist, double y_dist, double z_dist){
//  digitalWrite(Z_DIR, LOW);
//  while(!digitalRead(Z_LIMIT)){
//    digitalWrite(Z_STEP, HIGH);
//    delayMicroseconds(axis_step_delay);
//    digitalWrite(Z_STEP, LOW);
//    delayMicroseconds(axid_step_delay);
//  }
  digitalWrite(X_DIR, x_direction - '0');
  for(int i = 0; i < (x_dist/.1); i++){
    for(int j = 0; j < 16; j++){
      digitalWrite(X_STEP, HIGH);
      delayMicroseconds(axis_step_delay);
      digitalWrite(X_STEP, LOW);
      delayMicroseconds(axis_step_delay);
    }
  }

  digitalWrite(Y_DIR, y_direction - '0');
  for(int i = 0; i < (y_dist/.1); i++){
    for(int j = 0; j < 16; j++){
      digitalWrite(Y_STEP, HIGH);
      delayMicroseconds(axis_step_delay);
      digitalWrite(Y_STEP, LOW);
      delayMicroseconds(axis_step_delay);
    }
  }

  digitalWrite(Z_DIR, z_direction - '0');
  for(int i = 0; i < (z_dist/.1); i++){
    for(int j = 0; j < 8; j++){
      digitalWrite(Z_STEP, HIGH);
      delayMicroseconds(axis_step_delay);
      digitalWrite(Z_STEP, LOW);
      delayMicroseconds(axis_step_delay);
    }
  }
}

void getTip(){
  delay(10);
}

void ejectTip(){
  //move to max Z
  //move to center of tip dispenser
  //move 
  delay(10);
}

void aspirate(double volume){
  digitalWrite(P_DIR, HIGH);
  delay(10);
}

void dispense(double volume){
  delay(10);
}

//adjusts microstepping, timing, and steps/revolution for each motor
void setMicrostepping(int step_size){
  switch(step_size){
    case 1: //000 - full step
      digitalWrite(X_MS0, LOW); //23 - PA1
      digitalWrite(X_MS1, LOW); //25 - PA3
      digitalWrite(X_MS2, LOW); //27 - PA5
      digitalWrite(Y_MS0, LOW); //22 - PA0
      digitalWrite(Y_MS1, LOW); //24 - PA2
      digitalWrite(Y_MS2, LOW); //26 - PA4
      digitalWrite(Z_MS0, LOW); //35 - PC2
      digitalWrite(Z_MS1, LOW); //37 - PC0
      digitalWrite(Z_MS2, LOW); //39 - PG2
      axis_step_delay = 2000;
      axis_rev_steps = 200;
      x_step_dist = 2000E-7;
      y_step_dist = 2000E-7;
      z_step_dist = 4000E-5;
      break;
    case 2: //001 - half step
      digitalWrite(X_MS0, LOW);
      digitalWrite(X_MS1, LOW);
      digitalWrite(X_MS2, HIGH);
      digitalWrite(Y_MS0, LOW);
      digitalWrite(Y_MS1, LOW);
      digitalWrite(Y_MS2, HIGH);
      digitalWrite(Z_MS0, LOW);
      digitalWrite(Z_MS1, LOW);
      digitalWrite(Z_MS2, HIGH);
      axis_step_delay = 1000;
      pump_step_delay = 500;
      axis_rev_steps = 400;
      pump_rev_steps = 800;
      x_step_dist = 1000E-7;
      y_step_dist = 1000E-7;
      z_step_dist = 2000E-5;
      break;
    case 4: //010 - quarter step
      digitalWrite(X_MS0, LOW);
      digitalWrite(X_MS1, HIGH);
      digitalWrite(X_MS2, LOW);
      digitalWrite(Y_MS0, LOW);
      digitalWrite(Y_MS1, HIGH);
      digitalWrite(Y_MS2, LOW);
      digitalWrite(Z_MS0, LOW);
      digitalWrite(Z_MS1, HIGH);
      digitalWrite(Z_MS2, LOW);
      axis_step_delay = 500;
      pump_step_delay = 250;
      axis_rev_steps = 800;
      pump_rev_steps = 1600;
      x_step_dist = 500E-7;
      y_step_dist = 500E-7;
      z_step_dist = 1000E-5;
      break;
    case 8: //010 - 1/8 step
      digitalWrite(X_MS0, LOW);
      digitalWrite(X_MS1, HIGH);
      digitalWrite(X_MS2, HIGH);
      digitalWrite(Y_MS0, LOW);
      digitalWrite(Y_MS1, HIGH);
      digitalWrite(Y_MS2, HIGH);
      digitalWrite(Z_MS0, LOW);
      digitalWrite(Z_MS1, HIGH);
      digitalWrite(Z_MS2, HIGH);
      axis_step_delay = 250;
      pump_step_delay = 125;
      axis_rev_steps = 1600;
      pump_rev_steps = 3200;
      x_step_dist = 250E-7;
      y_step_dist = 250E-7;
      z_step_dist = 500E-5;
      break;
    case 16: //011 - 1/16 step
      digitalWrite(X_MS0, HIGH);
      digitalWrite(X_MS1, LOW);
      digitalWrite(X_MS2, LOW);
      digitalWrite(Y_MS0, HIGH);
      digitalWrite(Y_MS1, LOW);
      digitalWrite(Y_MS2, LOW);
      digitalWrite(Z_MS0, HIGH);
      digitalWrite(Z_MS1, LOW);
      digitalWrite(Z_MS2, LOW);
      axis_step_delay = 125;
      axis_rev_steps = 3200;
      x_step_dist = 125E-7;
      y_step_dist = 125E-7;
      z_step_dist = 250E-5;
      break;
    case 32: //100 - 1/32nd step
      digitalWrite(X_MS0, HIGH);
      digitalWrite(X_MS1, HIGH);
      digitalWrite(X_MS2, HIGH);
      digitalWrite(Y_MS0, HIGH);
      digitalWrite(Y_MS1, HIGH);
      digitalWrite(Y_MS2, HIGH);
      digitalWrite(Z_MS0, HIGH);
      digitalWrite(Z_MS1, HIGH);
      digitalWrite(Z_MS2, HIGH);
      axis_step_delay = 63;
      axis_rev_steps = 6400;
      x_step_dist = 63E-7;
      y_step_dist = 63E-7;
      z_step_dist = 125E-5;
      break;
  }
}

void enableMotors(){
  digitalWrite(X_SLEEP, HIGH);
  digitalWrite(Y_SLEEP, HIGH);
  digitalWrite(Z_SLEEP, HIGH);
  digitalWrite(P_SLEEP, HIGH);
}

void disableMotors(){
  digitalWrite(X_SLEEP, LOW);
  digitalWrite(Y_SLEEP, LOW);
  digitalWrite(Z_SLEEP, LOW);
  digitalWrite(P_SLEEP, LOW);
}

void xLimitPressed(){
  if(homing == 0){
    digitalWrite(X_SLEEP, LOW);
  }
}

void yLimitPressed(){
  if(homing == 0){
    digitalWrite(Y_SLEEP, LOW);
  }
}

void zLimitPressed(){
  if(homing == 0){
    digitalWrite(Z_SLEEP, LOW);
  }
}

void pLimitPressed(){
  if(homing == 0){
    digitalWrite(P_SLEEP, LOW);
  }
}
/*
//    //aspirate
//    else if(curr_command[0] == '0'){
//      for(int i = 1; i < curr_command.length(); i++){
//        volume += curr_command.charAt(i);
//      }
//      
//      digitalWrite(31, HIGH);
//      digitalWrite(STEP_ENABLE, HIGH);
//      //volume = curr_command.charAt(1)*100 + curr_command.charAt(2)*10 + curr_command.charAt(3)*1;
//      steps = volume.toInt()*24;
//      lcd.setCursor(0, 1);
//      lcd.print("Vol" + String(volume) + "Step" + String(steps));
//      stepper.move(steps);
//      digitalWrite(STEP_ENABLE, LOW);
//      digitalWrite(31, LOW);
//    }
//    //dispense
//    else if(curr_command[0] == '1'){
//      for(int i = 1; i < curr_command.length(); i++){
//        volume += curr_command.charAt(i);
//      }
//      //volume = curr_command.charAt(1)*100 + curr_command.charAt(2)*10 + curr_command.charAt(3)*1 + 50;
//      steps = volume.toInt()*24 + 50;
//      lcd.setCursor(0, 1);
//      lcd.print("Vol" + String(volume) + "Step" + String(steps));
//      digitalWrite(35, HIGH);
//      digitalWrite(STEP_ENABLE, HIGH);
//      stepper.move(-steps);
//      digitalWrite(STEP_ENABLE, LOW);
//      digitalWrite(35, LOW);
//      while(steps > 0 ){
//
//        //if(digitalRead(SWITCH_INPUT) == LOW){
//        if(digitalRead(LOW) == LOW){
//          steps = 0;
//          stepper.move(1000);
//        }
//        else if(steps > 50){
//          stepper.move(-50);
//          steps = steps - 50;
//        }
//        else{
//          stepper.move(steps);
//          steps = 0;
//        }
//      }
*/
