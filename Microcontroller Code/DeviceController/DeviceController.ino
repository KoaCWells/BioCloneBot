#include <Arduino.h>
#include "DRV8825.h"
#include "BasicStepperDriver.h"
#include <LiquidCrystal.h>

//Refer to Pololu website for documentation on DRV8825 stepper motor https://www.pololu.com/product/2133
#define MOTOR_STEPS 400 //motor step angle 0.9 degrees
#define RPM 100 //RPM of the motor; 100-200 seems to be the sweet spot

//3-bits for changing microstep resolution
// 000 - full step
// 001 - half step
// 010 - 1/4 step
// 011 - 1/8 step
// 100 - 1/16 step
// 101/110/111 - 1/32 step
#define MS0 0
#define MS1 0
#define MS2 0

//Arduino pins for controlling each stepper motor
//MS0, MS1, and MS2 are for setting microstep resolution of the driver
//SLEEP is for enabling/disabling the steeper motor - LOW for sleep/HIGH for enable
//STEP is the pin for the PWM signal for making the motor move
//DIR for controlling direction of the stepper motor

//X motor driver
#define X_MS0 23
#define X_MS1 25
#define X_MS2 27
#define X_SLEEP 29
#define X_STEP 31
#define X_DIR 33

//Y motor driver
#define Y_MS0 22
#define Y_MS1 24
#define Y_MS2 26
#define Y_SLEEP 28
#define Y_DIR 30
#define Y_STEP 32

//Z motor driver
#define Z_MS0 35
#define Z_MS1 37
#define Z_MS2 39
#define Z_SLEEP 41
#define Z_DIR 43
#define Z_STEP 45

//PUMP motor driver
#define P_MS0 34
#define P_MS1 36
#define P_MS2 38
#define P_SLEEP 40
#define P_DIR 42
#define P_STEP 44

//Stepper motors object definition
//BasicStepperDriver stepper(MOTOR_STEPS, X_DIR, STEP);
//BasicStepperDriver stepper(MOTOR_STEPS, Y_DIR, STEP);
//BasicStepperDriver stepper(MOTOR_STEPS, Z_DIR, STEP);
//BasicStepperDriver stepper(MOTOR_STEPS, P_DIR, STEP);

//LCD screen for feedback and troubleshooting
#define VDD 14
#define RS 15
#define EN 16
#define D4 47
#define D5 49
#define D6 51
#define D7 53

//Feedback LEDs
//MOVE for indicating X/Y/Z motion
//PUMP for indicating pump motion
//CONN for indicating successful serial connection with host PC
#define MOVE_LED 46
#define PUMP_LED 48
#define CONN_LED 50

//LCD object definition
LiquidCrystal lcd(RS, EN, D4, D5, D6, D7);

String new_command;
String prev_command;
String volume;
int steps;

void setup() {
  Serial.begin(9600);
  delay(1000);
//  pinMode(STEP_ENABLE, OUTPUT);
  pinMode(MOVE_LED,OUTPUT);
  pinMode(PUMP_LED,OUTPUT);
  pinMode(CONN_LED,OUTPUT);
  //pinMode(SYRINGE_MOTOR_TRIGGER, INPUT);
  //digitalWrite(STEP_ENABLE, LOW);
//  stepper.begin(RPM, MICROSTEPS);

//  attachInterrupt(digitalPinToInterrupt(SYRINGE_MOTOR_TRIGGER), syringeSwitchPressed, LOW);

    // set up the LCD's number of columns and rows:
  lcd.begin(16, 2);
  // Print a message to the LCD.
  lcd.print("");
}

void loop() {
  while(Serial.available() > 0){    
    new_command = Serial.readStringUntil('%');
    lcd.setCursor(0, 0);
    lcd.print(new_command);
    
    if(new_command == "ping"){
      digitalWrite(33, HIGH);
      Serial.print("pong");
    }
    //aspirate
    else if(new_command[0] == '0'){
      for(int i = 1; i < new_command.length(); i++){
        volume += new_command.charAt(i);
      }
      
      digitalWrite(31, HIGH);
//      digitalWrite(STEP_ENABLE, HIGH);
      //volume = new_command.charAt(1)*100 + new_command.charAt(2)*10 + new_command.charAt(3)*1;
      steps = volume.toInt()*24;
      lcd.setCursor(0, 1);
      lcd.print("Vol" + String(volume) + "Step" + String(steps));
//      stepper.move(steps);
//      digitalWrite(STEP_ENABLE, LOW);
      digitalWrite(31, LOW);
    }
    //dispense
    else if(new_command[0] == '1'){
      for(int i = 1; i < new_command.length(); i++){
        volume += new_command.charAt(i);
      }
      //volume = new_command.charAt(1)*100 + new_command.charAt(2)*10 + new_command.charAt(3)*1 + 50;
      steps = volume.toInt()*24 + 50;
      lcd.setCursor(0, 1);
      lcd.print("Vol" + String(volume) + "Step" + String(steps));
      digitalWrite(35, HIGH);
//      digitalWrite(STEP_ENABLE, HIGH);
//      stepper.move(-steps);
//      digitalWrite(STEP_ENABLE, LOW);
      digitalWrite(35, LOW);
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
    }
  }
  volume = "";
  steps = 0;
  prev_command = new_command;
  new_command = "";
}

void syringeSwitchPressed(){
//  digitalWrite(STEP_ENABLE, LOW);
  steps = 0;
}

void setMicroSteps(char input, int m0, int m1, int m2){
  switch(input){
    case 'x':
      break;
    case 'y':
      break;
    case 'z':
      break;
    case 'p':
      break;
  }
}
