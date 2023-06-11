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
******************************************************************************/
#include <math.h>

#define AXIS_STEPS 200 //Ender 3 Pro motor step angle 1.8 degrees/200 steps
#define PUMP_STEPS 400 //Pump stepper motor angle 0.9 degrees/400 steps

//X/Y/Z/P motor driver pins
#define X_STEP 54
#define X_DIR 55
#define X_ENABLE 38
#define Y_STEP 60
#define Y_DIR 61
#define Y_ENABLE 56
#define Z_STEP 46
#define Z_DIR 48
#define Z_ENABLE 62
#define P_STEP 26
#define P_DIR 28
#define P_ENABLE 24

//limit switch input pinss
#define X_LIMIT 3  //X- header
#define Y_LIMIT 14 //Y- header
#define Z_LIMIT 18 //Z- header
#define P_LIMIT 2  //Z+ header

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
int homing;
int message_character;

double xy_step_dist;
double z_step_dist;
double p_step_dist;
double volume;
double mix_count;
double x_dist;
double y_dist;
double z_dist;
double vol_per_step;
char x_dir;
char y_dir;
char z_dir;
char operation_dist[6];
char operation_vol[6];
char mix_qty[3];
char *ptr_end;
char *ptr_end2;
String host_message;
String confirmation_message;
String curr_command;
String prev_command;
bool message_complete;

int x_short[] = {
  120, 119, 118, 117, 116, 115, 114, 113, 112, 111,
  110, 109, 108, 107, 106, 105, 104, 103, 102, 101,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  101, 102, 103, 104, 105, 106, 107, 108, 109, 110,
  111, 112, 113, 114, 115, 116, 117, 118, 119, 120
};
  
int x_medium[] = {
  120, 115, 110, 105, 100, 95, 90, 85, 80, 75,
  70, 65, 60, 55, 50, 45, 44, 43, 42, 41, 
  40, 39, 38, 37, 36, 35, 34, 33, 32, 31, 
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
  41, 42, 43, 44, 45, 50, 55, 60, 65, 70, 
  75, 80, 85, 90, 95, 100, 105, 110, 115, 120
  };

int x_long[] = {
  120, 115, 110, 105, 100, 95, 90, 85, 80, 75, 
  70, 65, 60, 55, 50, 45, 40, 35, 30, 25, 
  22, 20, 19, 18, 17, 16, 15, 15, 15, 15, 
  15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 
  15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 
  15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 
  15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 
  15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 
  15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 
  15, 15 ,15, 15, 16, 17, 18, 19, 20, 22, 
  25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 
  75, 80, 85, 90, 95, 100, 105, 110, 115, 120
  };

//y delay values
int y_short[] = {
  200, 190, 180, 170, 160, 150, 140, 130, 120, 110,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
  110, 120, 130, 140, 150, 160, 170, 180, 190, 200
};
  
int y_medium[] = {
  120, 115, 110, 105, 100, 95, 90, 85, 80, 75,
  70, 65, 60, 55, 50, 45, 44, 43, 42, 41, 
  40, 39, 38, 37, 36, 35, 34, 33, 32, 31, 
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
  41, 42, 43, 44, 45, 50, 55, 60, 65, 70, 
  75, 80, 85, 90, 95, 100, 105, 110, 115, 120
  };

int y_long[] = {
  400, 350, 300, 250, 200, 150, 100, 95, 90, 85, 
  80, 75, 72, 70, 68, 66, 64, 62, 60, 58, 
  56, 54, 52, 50, 48, 46, 44, 42, 40, 38, 
  36, 34, 32, 30, 30, 30, 30, 30, 30, 30, 
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
  30, 30, 30, 30, 30, 30, 30, 32, 34, 36,
  38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 
  58, 60, 62, 64, 66, 68, 70, 72, 75, 80, 
  85, 90, 95, 100, 150, 200, 250, 300, 350, 400
  };

  //z delay values
int z_medium[] = {
  120, 115, 110, 105, 100, 95, 90, 85, 80, 75,
  70, 65, 60, 55, 50, 45, 44, 43, 42, 41, 
  40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 
  40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
  40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
  40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
  40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
  40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
  40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
  40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
  41, 42, 43, 44, 45, 50, 55, 60, 65, 70, 
  75, 80, 85, 90, 95, 100, 105, 110, 115, 120
  };

int z_long[] = {
  120, 115, 110, 105, 100, 95, 90, 85, 80, 75, 
  70, 69, 68, 67, 66, 65, 64, 63, 62, 61, 
  60, 59, 58, 57, 56, 55, 54, 53, 52, 51, 
  50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 
  50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 
  50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 
  50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 
  50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 
  50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 
  51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 
  61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 
  75, 80, 85, 90, 95, 100, 105, 110, 115, 120
  };

void setup() {
  Serial.begin(9600);

  //controls speed of each axis during constant velocity movements
  axis_step_delay = 110;
  pump_step_delay = 35;

  //steps per revolution
  axis_rev_steps = 12800;
  pump_rev_steps = 400; 

  //mm per step
  xy_step_dist = 0.025;
  z_step_dist = 0.005;
  p_step_dist = 0.000625;
  //0.041667 for 500 uL syringe
  //0.020774 for 250 uL syringe
  //0.004166 for 50 uL gastight syringe
  vol_per_step = 0.000521;

  //sets all stepper motor control pins to output
  pinMode(X_STEP, OUTPUT);
  pinMode(X_DIR, OUTPUT);
  pinMode(X_ENABLE, OUTPUT);
  pinMode(Y_STEP, OUTPUT);
  pinMode(Y_DIR, OUTPUT);
  pinMode(Y_ENABLE, OUTPUT);
  pinMode(Z_STEP, OUTPUT);
  pinMode(Z_DIR, OUTPUT);
  pinMode(Z_ENABLE, OUTPUT);
  pinMode(P_STEP, OUTPUT);
  pinMode(P_DIR, OUTPUT);
  pinMode(P_ENABLE, OUTPUT);

  //define limit switch pins and pullup to high voltage
  pinMode(X_LIMIT, INPUT_PULLUP);
  pinMode(Y_LIMIT, INPUT_PULLUP);
  pinMode(Z_LIMIT, INPUT_PULLUP);
  pinMode(P_LIMIT, INPUT_PULLUP);

  //enables stepper motors
  digitalWrite(X_ENABLE, HIGH);
  digitalWrite(Y_ENABLE, HIGH);
  digitalWrite(Z_ENABLE, HIGH);
  digitalWrite(P_ENABLE, HIGH);

  //sets default direction of stepper motors to point towards the limit switches
  digitalWrite(X_DIR, HIGH);
  digitalWrite(Y_DIR, HIGH);
  digitalWrite(Z_DIR, HIGH);
  digitalWrite(P_DIR, HIGH);

  homing = 0;
}

void loop() {
  /*
 * List of possible device commands
 * -0000 home device: homes x, y, z, and p stepper motors
 * -0001 move pump: move carriage to x, y, z location (XXX.XX location in mm)
 * -0010 remove tip: remove tip and leave room for trailing air gap (25.0uL)
 * -0011 aspirate volume: aspirate volume in ul (XXX.XX ul)
 * -0100 dispense volume: dispense volume in ul + 25 ul (XXX.XX ul)
 * -0101 mix: mix volume XXX number of times
 * -1110 enable stepper motors: sets sleep pin of all motor drivers to HIGH
 * -1111 disable stepper motors: sets sleep pin of all motor drivers to LOW
 */
  while(Serial.available() > 0){
    Serial.flush();
    curr_command = "";
    x_dist = 0.0;
    y_dist = 0.0;
    z_dist = 0.0;
    volume = 0.0;
    mix_count = 0.0;
    
    host_message = receiveHostMessage();

    if(host_message == "ping"){
      confirmation_message = "#pong%";
      for(int i = 0; i < confirmation_message.length(); i++){
        Serial.write(confirmation_message[i]);
      }
    }

    //decodes first 4 characters of host_message to determine command
    for(int i = 0; i < 4; i++){
      curr_command += host_message[i];
    }
    
    if(curr_command == "0000"){ //home motors
      homeCarriage();
      sendCompletionMessage();
    }
    else if(curr_command == "0001"){ //move pump to x, y, z location
      x_dir = host_message.charAt(4);
      y_dir = host_message.charAt(5);
      z_dir = host_message.charAt(6);
      
      for(int i = 7; i < 13; i++){
        operation_dist[i-7] = host_message[i];
      }
      x_dist = strtod(operation_dist, &ptr_end);

      for(int i = 13; i < 19; i++){
        operation_dist[i-13]= host_message[i];
      }
      y_dist = strtod(operation_dist, &ptr_end);

      for(int i = 19; i < 25; i++){
        operation_dist[i-19]= host_message[i];
      }
      z_dist = strtod(operation_dist, &ptr_end);

      movePumpWithAcceleration(x_dir,y_dir,z_dir,x_dist,y_dist,z_dist);
      //movePump(x_dir,y_dir,z_dir,x_dist,y_dist,z_dist);
      sendCompletionMessage();
    }
    else if(curr_command == "0010"){ //remove tip
      removeTip();
      sendCompletionMessage();
    }
    else if(curr_command == "0011"){ //aspirate volume
      for(int i = 4; i < 10; i++){
        operation_vol[i-4] = host_message[i];
      }
      volume = strtod(operation_vol, &ptr_end);
      aspirate(volume);
      sendCompletionMessage();
    }
    else if(curr_command == "0100"){  //dispense volume
      for(int i = 4; i < 10; i++){
        operation_vol[i-4] = host_message[i];
      }
      volume = strtod(operation_vol, &ptr_end);
      dispense(volume);
      sendCompletionMessage();
    }
    else if(curr_command == "0101"){  //mix volume
      for(int i = 4; i < 7; i++){
        if(i-4 == 0){
          mix_count += 100*(host_message[i] - '0');
        }
        else if(i-4 == 1){
          mix_count += 10*(host_message[i] - '0');
        }
        else if(i-4 == 2){
          mix_count += 1*(host_message[i] - '0');
        }
      }

      for(int i = 7; i < 12; i++){
        operation_vol[i-7] = host_message[i];
      }
      volume = strtod(operation_vol, &ptr_end2);
      mix(mix_count, volume);
      sendCompletionMessage();
    }
    else if(curr_command == "1111"){ //disable stepper motors
      disableMotors();
      sendCompletionMessage();
    }
  }
  prev_command = curr_command;
}

 /*homeCarriage
  * homeCarriage resets all stepper motors to their homed position.
  * Works by enabling each motor driver by setting each corresponding
  * SLEEP pin to HIGH, then setting the DIR pin to match the direction
  * of the limit switch. Each motor then moves until the limit switch
  * is pressed and backs off until the switch is no longer pressed.
  */
void homeCarriage(){
  enableMotors();

  //disables limit switch overrupts for the purpose of homing
  homing = 1; 
  int count = 0;
  
  //sets initial direction of all motors towards their limit switches
  digitalWrite(X_DIR, HIGH);
  digitalWrite(Y_DIR, HIGH);
  digitalWrite(Z_DIR, HIGH);
  digitalWrite(P_DIR, HIGH);

  //home x-axis
  //move towards limit switch until it is pressed
  while(digitalRead(X_LIMIT)){
    digitalWrite(X_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(X_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  delay(100);
  //move away from limit switch until it isn't pressed
  digitalWrite(X_DIR, LOW);
  while(!digitalRead(X_LIMIT)){
    digitalWrite(X_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(X_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  //move away from limit switch just a bit more
  for(int i = 0; i < 20; i++){
    digitalWrite(X_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(X_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  digitalWrite(X_DIR, HIGH);

  //home y-axis
  //move towards limit switch until it is pressed
  while(digitalRead(Y_LIMIT)){
    digitalWrite(Y_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Y_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  delay(100);
  //move away from limit switch until it isn't pressed
  digitalWrite(Y_DIR, LOW);
  while(!digitalRead(Y_LIMIT)){
    digitalWrite(Y_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Y_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  //move away from limit switch a little bit more
  for(int i = 0; i < 20; i++){
    digitalWrite(Y_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Y_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  digitalWrite(Y_DIR, HIGH);

  //home z-axis
  //move towards limit switch until it is pressed
  while(digitalRead(Z_LIMIT)){
    digitalWrite(Z_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Z_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  delay(100);
  //move away from limit switch until it isn't pressed
  digitalWrite(Z_DIR, LOW);
  while(!digitalRead(Z_LIMIT)){
    digitalWrite(Z_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Z_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  //move away from limit switch a little bit more
  for(int i = 0; i < 10; i++){
    digitalWrite(Z_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Z_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
  digitalWrite(Z_DIR, HIGH);

  //home p-axis
  while(digitalRead(P_LIMIT)){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }
  delay(50);
  digitalWrite(P_DIR, LOW);
  while(!digitalRead(P_LIMIT)){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }

  // aspirate to create trailing air gap and remove backlas
  for(int i = 0; i < 9.3/vol_per_step; i++){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }

  digitalWrite(P_DIR, LOW);
  movePump('0', '0', '0', 11.5, 28.50, 0.0);
  //sets homing to 0 re-enabling the normal functionality of the limit switches as emergency stops
  //and sets carriage location to (0.0, 0.0, 0.0) and syringe volume to 0
  homing = 0;
}

void movePump(char x_dir, char y_dir, char z_dir, double x_dist, double y_dist, double z_dist){
  if(x_dir == '0'){
    digitalWrite(X_DIR, LOW);
  }
  else if(x_dir == '1'){
    digitalWrite(X_DIR, HIGH);
  }

  for(double i = 0; i < (floor(x_dist/xy_step_dist)); i++){
    digitalWrite(X_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(X_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }

  if(y_dir == '0'){
    digitalWrite(Y_DIR, LOW);
  }
  else if(y_dir == '1'){
    digitalWrite(Y_DIR, HIGH);
  }

  for(double i = 0; i < (floor(y_dist/xy_step_dist)); i++){
    digitalWrite(Y_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Y_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }

  if(z_dir == '0'){
    digitalWrite(Z_DIR, LOW);
  }
  else if(z_dir == '1'){
    digitalWrite(Z_DIR, HIGH);
  }
  
  digitalWrite(Z_DIR, z_dir - '0');

  for(double i = 0; i < (z_dist/z_step_dist); i++){
    digitalWrite(Z_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Z_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
}

void movePumpWithAcceleration(char x_dir, char y_dir, char z_dir, double x_dist, double y_dist, double z_dist){
  int loop_count = sizeof(x_short)/sizeof(x_short[0]);
  int loop_delay = 0;
  float loop_dist_with_decimals = 0;
  int loop_dist = 0;
  int final_loop_dist = 0;
  int delay = 9999;
  int delay_array[loop_count];
  
  if(x_dist > 0.0){
    //set x direction
    if(x_dir == '0'){
      digitalWrite(X_DIR, LOW);
    }
    else if(x_dir == '1'){
      digitalWrite(X_DIR, HIGH);
    }

    if(x_dist < 10.0){
      for(unsigned int i = 0; i < floor(x_dist/xy_step_dist); i++){
        digitalWrite(X_STEP, HIGH);
        delayMicroseconds(100);
        digitalWrite(X_STEP, LOW);
        delayMicroseconds(100);
      }
    }
    else{
      //set x acceleration (use which delay array)
      if(300.0 <= x_dist && x_dist <= 401.0){
        memcpy(delay_array, x_long, sizeof(x_long));
      }
      else if(50.0 <= x_dist && x_dist < 300.0){
        memcpy(delay_array, x_medium, sizeof(x_medium));
      }
      else if(10.0 <= x_dist && x_dist < 50.0){
        memcpy(delay_array, x_short, sizeof(x_short));
      }

      unsigned int num_of_steps_x = floor(x_dist/xy_step_dist);
      loop_dist_with_decimals = (float)num_of_steps_x/loop_count;
      loop_dist = floor(loop_dist_with_decimals);
      final_loop_dist = (loop_dist_with_decimals - loop_dist)*loop_count;

      for(int i = 0; i < loop_count/2; i++){
        delay = delay_array[i];
        for(int j = 0; j < loop_dist; j++){
          digitalWrite(X_STEP, HIGH);
          delayMicroseconds(delay);
          digitalWrite(X_STEP, LOW);
          delayMicroseconds(delay);
        }
      }

      delay = delay_array[loop_count/2];
      for(int i = 0; i < final_loop_dist; i++)
      {
          digitalWrite(X_STEP, HIGH);
          delayMicroseconds(delay);
          digitalWrite(X_STEP, LOW);
          delayMicroseconds(delay);
      }

      for(int i = loop_count/2; i < loop_count; i++){
        delay = delay_array[i];
        for(int j = 0; j < loop_dist; j++){
          digitalWrite(X_STEP, HIGH);
          delayMicroseconds(delay);
          digitalWrite(X_STEP, LOW);
          delayMicroseconds(delay);
        }
      }
    }
  }

  if(y_dist > 0.0){
    //set y direction
    if(y_dir == '0'){
      digitalWrite(Y_DIR, LOW);
    }
    else if(y_dir == '1'){
      digitalWrite(Y_DIR, HIGH);
    }

    if(y_dist < 10.0){
      for(unsigned int i = 0; i < floor(y_dist/xy_step_dist); i++){
        digitalWrite(Y_STEP, HIGH);
        delayMicroseconds(100);
        digitalWrite(Y_STEP, LOW);
        delayMicroseconds(100);
      }
    }
    else{
      //set y acceleration (use which delay array)
      if(300.0 <= y_dist && y_dist <= 401.0){
        memcpy(delay_array, y_long, sizeof(y_long));
      }
      else if(70.0 <= y_dist && y_dist < 300.0){
        memcpy(delay_array, y_medium, sizeof(y_medium));
      }
      else if(10.0 <= y_dist && y_dist < 70.0){
        memcpy(delay_array, y_short, sizeof(y_short));
      }

      unsigned int num_of_steps_y = floor(y_dist/xy_step_dist);
      loop_dist_with_decimals = (float)num_of_steps_y/(sizeof(delay_array)/sizeof(delay_array[0]));
      loop_dist = floor(loop_dist_with_decimals);
      final_loop_dist = (loop_dist_with_decimals - loop_dist)*loop_count;

      for(int i = 0; i < loop_count/2; i++){
        delay = delay_array[i];
        for(int j = 0; j < loop_dist; j++){
          digitalWrite(Y_STEP, HIGH);
          delayMicroseconds(delay);
          digitalWrite(Y_STEP, LOW);
          delayMicroseconds(delay);
        }
      }

      delay = delay_array[loop_count/2];
      for(int i = 0; i < final_loop_dist; i++)
      {
          digitalWrite(Y_STEP, HIGH);
          delayMicroseconds(delay);
          digitalWrite(Y_STEP, LOW);
          delayMicroseconds(delay);
      }

      for(int i = loop_count/2; i < loop_count; i++){
        delay = delay_array[i];
        for(int j = 0; j < loop_dist; j++){
          digitalWrite(Y_STEP, HIGH);
          delayMicroseconds(delay);
          digitalWrite(Y_STEP, LOW);
          delayMicroseconds(delay);
        }
      }
    }
  }

  if(z_dist > 0.0){
    //set z direction
    if(z_dir == '0'){
      digitalWrite(Z_DIR, LOW);
    }
    else if(z_dir == '1'){
      digitalWrite(Z_DIR, HIGH);
    }

    if(z_dist < 25.0){
      for(unsigned int i = 0; i < floor(z_dist/z_step_dist); i++){
        digitalWrite(Z_STEP, HIGH);
        delayMicroseconds(100);
        digitalWrite(Z_STEP, LOW);
        delayMicroseconds(100);
      }
    }

    else{
      //set z acceleration (use which delay array)
      if(75.0 <= z_dist && z_dist <= 136.0){
        memcpy(delay_array, z_long, sizeof(z_long));
      }
      else if(25.0 <= z_dist && z_dist < 75.0){
        memcpy(delay_array, z_medium, sizeof(z_medium));
      }

      unsigned int num_of_steps_z = floor(z_dist/z_step_dist);
      loop_dist_with_decimals = (float)num_of_steps_z/loop_count;
      loop_dist = floor(loop_dist_with_decimals);
      final_loop_dist = (loop_dist_with_decimals - loop_dist)*loop_count;

      for(int i = 0; i < loop_count/2; i++){
        delay = delay_array[i];
        for(int j = 0; j < loop_dist; j++){
          digitalWrite(Z_STEP, HIGH);
          delayMicroseconds(delay);
          digitalWrite(Z_STEP, LOW);
          delayMicroseconds(delay);
        }
      }

      delay = delay_array[loop_count/2];
      for(int i = 0; i < final_loop_dist; i++)
      {
          digitalWrite(Z_STEP, HIGH);
          delayMicroseconds(delay);
          digitalWrite(Z_STEP, LOW);
          delayMicroseconds(delay);
      }

      for(int i = loop_count/2; i < loop_count; i++){
        delay = delay_array[i];
        for(int j = 0; j < loop_dist; j++){
          digitalWrite(Z_STEP, HIGH);
          delayMicroseconds(delay);
          digitalWrite(Z_STEP, LOW);
          delayMicroseconds(delay);
        }
      }
    }
  }
}

void removeTip(){
  homing = 1;
  //dispense until tip comes off
  digitalWrite(P_DIR, HIGH);
  while(digitalRead(P_LIMIT)){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP,LOW);
    delayMicroseconds(pump_step_delay);
  }
  delay(50);
  //back off until limit switch isn't pressed
  digitalWrite(P_DIR, LOW);
  while(!digitalRead(P_LIMIT)){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }
  //back off until pump ejector is flush with base
  for(int i = 0; i < 4.3/vol_per_step; i++){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }

  homing = 0;
}

void aspirate(double volume){
  homing = 1;
  digitalWrite(P_DIR, LOW);
  
  double num_of_steps = floor(volume/vol_per_step);
  int num_of_loops = ceil(num_of_steps/65535);
  double final_loop_dist = num_of_steps - (num_of_loops - 1)*65535;

  for(int i = 0; i < num_of_loops; i++){
    if(i < num_of_loops - 1){
      for(unsigned int j = 0; j < 65536; j++){
        digitalWrite(P_STEP, HIGH);
        delayMicroseconds(pump_step_delay);
        digitalWrite(P_STEP, LOW);
        delayMicroseconds(pump_step_delay);
      }
    }
    else{
        for(unsigned int j = 0; j < final_loop_dist; j++){
          digitalWrite(P_STEP, HIGH);
          delayMicroseconds(pump_step_delay);
          digitalWrite(P_STEP, LOW);
          delayMicroseconds(pump_step_delay);
      }
    }
  }
  homing = 0;
}

void dispense(double volume){
  homing = 1;
  digitalWrite(P_DIR, HIGH);

  double num_of_steps = floor(volume/vol_per_step);
  int num_of_loops = ceil(num_of_steps/65535);
  double final_loop_dist = num_of_steps - (num_of_loops - 1)*65535;

  for(int i = 0; i < num_of_loops; i++){
    if(i < num_of_loops - 1){
      for(unsigned int j = 0; j < 65536; j++){
        digitalWrite(P_STEP, HIGH);
        delayMicroseconds(pump_step_delay);
        digitalWrite(P_STEP, LOW);
        delayMicroseconds(pump_step_delay);
      }
    }
    else{
        for(unsigned int j = 0; j < final_loop_dist; j++){
          digitalWrite(P_STEP, HIGH);
          delayMicroseconds(pump_step_delay);
          digitalWrite(P_STEP, LOW);
          delayMicroseconds(pump_step_delay);
      }
    }
  }
  homing = 0;
}

void mix(double mix_count, double volume)
{
  double num_of_steps = floor(volume/vol_per_step);
  int num_of_loops = ceil(num_of_steps/65535);
  double final_loop_dist = num_of_steps - (num_of_loops - 1)*65535;

  for(int num_of_mixes = 0; num_of_mixes < mix_count; num_of_mixes++){
    digitalWrite(P_DIR, LOW);
    for(int i = 0; i < num_of_loops; i++){
      if(i < num_of_loops - 1){
        for(unsigned int j = 0; j < 65536; j++){
          digitalWrite(P_STEP, HIGH);
          delayMicroseconds(pump_step_delay);
          digitalWrite(P_STEP, LOW);
          delayMicroseconds(pump_step_delay);
        }
      }
      else{
        for(unsigned int j = 0; j < final_loop_dist; j++){
          digitalWrite(P_STEP, HIGH);
          delayMicroseconds(pump_step_delay);
          digitalWrite(P_STEP, LOW);
          delayMicroseconds(pump_step_delay);
        }
      }
    }
    digitalWrite(P_DIR, HIGH);
    for(int i = 0; i < num_of_loops; i++){
      if(i < num_of_loops - 1){
        for(unsigned int j = 0; j < 65536; j++){
          digitalWrite(P_STEP, HIGH);
          delayMicroseconds(pump_step_delay);
          digitalWrite(P_STEP, LOW);
          delayMicroseconds(pump_step_delay);
        }
      }
      else{
        for(unsigned int j = 0; j < final_loop_dist; j++){
          digitalWrite(P_STEP, HIGH);
          delayMicroseconds(pump_step_delay);
          digitalWrite(P_STEP, LOW);
          delayMicroseconds(pump_step_delay);
        }
      }
    }
  }
}

void enableMotors(){
  digitalWrite(X_ENABLE, LOW);
  digitalWrite(Y_ENABLE, LOW);
  digitalWrite(Z_ENABLE, LOW);
  digitalWrite(P_ENABLE, LOW);
}

void disableMotors(){
  digitalWrite(X_ENABLE, HIGH);
  digitalWrite(Y_ENABLE, HIGH);
  digitalWrite(Z_ENABLE, HIGH);
  digitalWrite(P_ENABLE, HIGH);
}

void startSerialConnection(){
  bool message_begin = false;
  bool message_complete = false;
  host_message = "";
  confirmation_message = "#pong%";
  message_character = -1;
  Serial.flush();
  //waits for message from serial to establish connection with host computer
  
  while(message_complete == false){
    message_character = Serial.read();
    if(message_character != -1){
      if((char)message_character == '#'){
        message_begin = true;
      }
      
      if(message_begin == true){
        if((char) message_character != '#' && (char)message_character != '%'){
          host_message += (char)message_character;
        }
        else{
          if(host_message == "ping"){
            message_complete = true;
            for(int i = 0; i < confirmation_message.length(); i++){
              Serial.write(confirmation_message[i]);
            }
          }
        }
      }
    }
  }
}

void sendCompletionMessage(){
  String completion_message = "#complete%";
  for(int i = 0; i < completion_message.length(); i++){
    Serial.write(completion_message[i]);
  }
}

String receiveHostMessage(){
  bool message_begin = false;
  bool message_complete = false;
  host_message = "";
  confirmation_message = "#pong%";
  message_character = -1;
  Serial.flush();
  //waits for message from serial to establish connection with host computer
  
  while(message_complete == false){
    message_character = Serial.read();
    if(message_character != -1){
      if((char)message_character == '#'){
        message_begin = true;
      }
      
      if(message_begin == true){
        if((char) message_character != '#' && (char)message_character != '%'){
          host_message += (char)message_character;
        }
        else if((char)message_character == '%'){
          message_complete = true;
        }
      }
    }
  }
  return host_message;
}
