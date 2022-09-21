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
int homing;
int message_character;

double x_step_dist;
double y_step_dist;
double z_step_dist;
double p_step_dist;
double aspirate_vol;
double dispense_vol;
double mix_vol;
double mix_count;
double x_dist;
double y_dist;
double z_dist;
char x_dir;
char y_dir;
char z_dir;
char distance[6];
char volume[6];
char mix_qty[3];
char *ptr_end;
char *ptr_end2;
String host_message;
String confirmation_message;
String curr_command;
String prev_command;
bool message_complete;

void setup() {
  Serial.begin(9600);

  pump_step_delay = 500; //time delay between HIGH/LOW digital control signal in microseconds
  pump_rev_steps = 400; //steps per revolution
  p_step_dist = 157E-6; //mm per step

  //sets all stepper motor control pins to output
  for(int i = 22; i <= 45; i++){
    pinMode(i, OUTPUT);
  }

  digitalWrite(P_MS0, LOW);
  digitalWrite(P_MS1, LOW);
  digitalWrite(P_MS2, LOW);

  //defines indicator LEDs: MOVE for carriage movement, PUMP for pump movement, and CONN for a successful serial connection
  pinMode(MOVE_LED,OUTPUT);
  pinMode(PUMP_LED,OUTPUT);
  pinMode(CONN_LED,OUTPUT);
  
  //define limit switch pins and pullup to high voltage
  pinMode(X_LIMIT, INPUT_PULLUP);
  pinMode(Y_LIMIT, INPUT_PULLUP);
  pinMode(Z_LIMIT, INPUT_PULLUP);
  pinMode(P_LIMIT, INPUT_PULLUP);

  //defines output pin for powering the LCD data lines
  pinMode(VDD, OUTPUT);
  digitalWrite(VDD, HIGH);

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

  setMicrostepping(32); //sets microstepping of all 3-axis drivers to 1/32
  homing = 0;
  // set up the LCD's number of columns and rows:
  lcd.begin(16, 2);
  lcd.print("Waiting for host connection.");
  startSerialConnection();
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
    aspirate_vol = 0.0;
    dispense_vol = 0.0;
    mix_vol = 0.0;
    mix_count = 0.0;
    
    lcd.setCursor(0,1);
    host_message = receiveHostMessage();
    lcd.print(host_message);

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
        distance[i-7] = host_message[i];
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

      movePump(x_dir,y_dir,z_dir,x_dist,y_dist,z_dist);
      sendCompletionMessage();
    }
    else if(curr_command == "0010"){ //remove tip
      removeTip();
      sendCompletionMessage();
    }
    else if(curr_command == "0011"){ //aspirate volume
      for(int i = 4; i < 10; i++){
        volume[i-4] = host_message[i];
      }
      aspirate_vol = strtod(volume, &ptr_end);
      aspirate(aspirate_vol);
      sendCompletionMessage();
    }
    else if(curr_command == "0100"){  //dispense volume
      for(int i = 4; i < 10; i++){
        volume[i-4] = host_message[i];
      }
      dispense_vol = strtod(volume, &ptr_end);
      dispense(dispense_vol);
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
        volume[i-7] = host_message[i];
      }
      mix_vol = strtod(volume, &ptr_end2);
      lcd.clear();
      lcd.setCursor(0,1);
      lcd.print(mix_count);
      mix(mix_count, mix_vol);
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
  * This is the homed position.
  */
void homeCarriage(){
  enableMotors(); //enables all stepper motor drivers

  homing = 1; //disables limit switch overrupts for the purpose of homing
  int count = 0;
  //sets initial direction of all motors towards their limit switches
  digitalWrite(X_DIR, LOW);
  digitalWrite(Y_DIR, LOW);
  digitalWrite(Z_DIR, LOW);
  digitalWrite(P_DIR, HIGH);

  //home x-axis
  //move towards limit switch until it is pressed
  while(digitalRead(X_LIMIT)){
    digitalWrite(X_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(X_STEP,LOW);
    delayMicroseconds(axis_step_delay);
  }
  delay(100);
  //move away from limit switch until it isn't pressed
  digitalWrite(X_DIR, HIGH);
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
  digitalWrite(X_DIR, LOW);

  //home y-axis
  //move towards limit switch until it is pressed
  while(digitalRead(Y_LIMIT)){
    digitalWrite(Y_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Y_STEP,LOW);
    delayMicroseconds(axis_step_delay);
  }
  delay(100);
  //move away from limit switch until it isn't pressed
  digitalWrite(Y_DIR, HIGH);
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
  digitalWrite(Y_DIR, LOW);

  //home z-axis
  //move towards limit switch until it is pressed
  while(digitalRead(Z_LIMIT)){
    digitalWrite(Z_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Z_STEP,LOW);
    delayMicroseconds(axis_step_delay);
  }
  delay(100);
  //move away from limit switch until it isn't pressed
  digitalWrite(Z_DIR, HIGH);
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
  digitalWrite(Z_DIR, LOW);

  //home p-axis
  while(digitalRead(P_LIMIT)){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP,LOW);
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
  //back off until pump ejector is flush with base
  for(int i = 0; i < 800; i++){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }
  //back off pump for 25.0 uL trailing air gap
  for(int i = 0; i < 25.0/0.020774; i++){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }

  digitalWrite(P_DIR, LOW);
  movePump('1', '1', '0', 012.00, 033.00, 0.0);
  //sets homing to 0 re-enabling the normal functionality of the limit switches as emergency stops
  //and sets carriage location to (0.0, 0.0, 0.0) and syringe volume to 0
  homing = 0;
}

void movePump(char x_direction, char y_direction, char z_direction, double x_dist, double y_dist, double z_dist){
  if(x_direction == '0'){
      digitalWrite(X_DIR, LOW);
  }
  else if(x_direction == '1'){
    digitalWrite(X_DIR, HIGH);
  }

  for(double i = 0; i < (x_dist/.00625); i++){
    digitalWrite(X_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(X_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }

  if(y_direction == '0'){
      digitalWrite(Y_DIR, LOW);
  }
  else if(y_direction == '1'){
    digitalWrite(Y_DIR, HIGH);
  }

  for(double i = 0; i < (y_dist/.00625); i++){
    digitalWrite(Y_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Y_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }

  if(z_direction == '0'){
      digitalWrite(Z_DIR, LOW);
  }
  else if(z_direction == '1'){
    digitalWrite(Z_DIR, HIGH);
  }
  
  digitalWrite(Z_DIR, z_direction - '0');
  lcd.setCursor(1,0);
  lcd.clear();
  lcd.print(z_dist);

  for(double i = 0; i < (z_dist/.00125); i++){
    digitalWrite(Z_STEP, HIGH);
    delayMicroseconds(axis_step_delay);
    digitalWrite(Z_STEP, LOW);
    delayMicroseconds(axis_step_delay);
  }
}

void removeTip(){
  //move to max Z
  //move to center of tip dispenser
  //move
  homing = 1;
  digitalWrite(P_DIR, HIGH);
  while(digitalRead(P_LIMIT)){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP,LOW);
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
  //back off until pump ejector is flush with base
  for(int i = 0; i < 800; i++){
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
  //.041667 for 500 uL syringe
  //0.020774 for 250 uL syringe
  for(int i = 0; i < volume/0.020774; i++){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }
  homing = 0;
}

void dispense(double volume){
  homing = 1;
  digitalWrite(P_DIR, HIGH);
  for(int i = 0; i < volume/0.020774; i++){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }
  homing = 0;
}

void mix(double mix_count, double volume)
{
  for(int i = 0; i < mix_count; i++){
    digitalWrite(P_DIR, LOW);
    for(int j = 0; j < volume/0.020774; j++){
      digitalWrite(P_STEP, HIGH);
      delayMicroseconds(pump_step_delay);
      digitalWrite(P_STEP, LOW);
      delayMicroseconds(pump_step_delay);
    }
    digitalWrite(P_DIR, HIGH);
    for(int j = 0; j < volume/0.020774; j++){
      digitalWrite(P_STEP, HIGH);
      delayMicroseconds(pump_step_delay);
      digitalWrite(P_STEP, LOW);
      delayMicroseconds(pump_step_delay);
    }
  }
  digitalWrite(P_DIR, HIGH);
  for(int i = 0; i < 25.0/0.020774; i++){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(pump_step_delay);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(pump_step_delay);
  }
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
      axis_rev_steps = 400;
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
      //pump_step_delay = 250;
      axis_rev_steps = 800;
      //pump_rev_steps = 1600;
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
      //pump_step_delay = 125;
      axis_rev_steps = 1600;
      //pump_rev_steps = 3200;
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
      //axis_step_delay = 125;
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

void startSerialConnection(){
  bool message_begin = false;
  bool message_complete = false;
  host_message = "";
  confirmation_message = "#pong%";
  message_character = -1;
  Serial.flush();
  lcd.setCursor(0,1);
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
            digitalWrite(CONN_LED, HIGH);
            lcd.clear();
            lcd.setCursor(0,0);
            lcd.print("Host Connected.");
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
  lcd.setCursor(0,1);
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
void xLimitPressed(){
  if(homing == 0){
    //digitalWrite(X_SLEEP, LOW);
  }
}

void yLimitPressed(){
  if(homing == 0){
    //digitalWrite(Y_SLEEP, LOW);
  }
}

void zLimitPressed(){
  if(homing == 0){
    //digitalWrite(Z_SLEEP, LOW);
  }
}

void pLimitPressed(){
  if(homing == 0){
    digitalWrite(P_SLEEP, LOW);
  }
}
