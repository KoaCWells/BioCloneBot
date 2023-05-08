//X motor driver pins
#define X_STEP 54
#define X_DIR 55
#define X_ENABLE 38

//Y motor driver pins
#define Y_STEP 60
#define Y_DIR 61
#define Y_ENABLE 56

//Z motor driver pins
#define Z_STEP 46
#define Z_DIR 48
#define Z_ENABLE 62

//PUMP motor driver pins
#define P_STEP 26
#define P_DIR 28
#define P_ENABLE 24

//stepper motor limit switch detection for hardware interrupt/homing
#define X_LIMIT 3
#define Y_LIMIT 14
#define Z_LIMIT 18
#define P_LIMIT 2 //uses X_MAX_PIN from Marlin firmware

//x delay values
int xShort[] = {
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
  
int xMedium[] = {
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

int xLong[] = {
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
int yShort[] = {
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
  
int yMedium[] = {
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

int yLong[] = {
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

// int yLong[] = {
//   400, 350, 300, 250, 200, 195, 190, 185, 180, 175, 
//   170, 165, 160, 155, 150, 145, 140, 135, 130, 125, 
//   120, 115, 110, 105, 100, 95, 90, 85, 80, 75, 
//   70, 65, 60, 55, 50, 45, 40, 40, 40, 40, 
//   40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
//   40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
//   40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
//   40, 40, 40, 40, 40, 40, 40, 40, 40, 40,
//   40, 40, 40, 40, 45, 50, 55, 50, 65, 70,
//   75, 80, 85, 90, 95, 100, 105, 110, 115, 120,
//   125, 130, 135, 140, 145, 150, 155, 160, 165, 170,
//   175, 180, 185, 190, 195, 200, 250, 300, 350, 400
//   };


  //z delay values
  int zShort[] = {
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
  
int zMedium[] = {
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

int zLong[] = {
  120, 115, 110, 105, 100, 95, 90, 85, 80, 75, 
  70, 65, 60, 55, 50, 45, 40, 39, 38, 37, 
  36, 35, 34, 33, 32, 31, 30, 30, 30, 30, 
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 
  30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 
  30, 30, 35, 40, 45, 50, 55, 60, 65, 70, 
  75, 80, 85, 90, 95, 100, 105, 110, 115, 120
  };

double distPerStepXY = 0.025;
double distPerStepZ = 0.005;

void setup() {
  Serial.begin(9600);
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

  pinMode(X_LIMIT, INPUT_PULLUP);
  pinMode(Y_LIMIT, INPUT_PULLUP);
  pinMode(Z_LIMIT, INPUT_PULLUP);
  pinMode(P_LIMIT, INPUT_PULLUP);

  digitalWrite(X_ENABLE, LOW);
  digitalWrite(Y_ENABLE, LOW);
  digitalWrite(Z_ENABLE, LOW);
  digitalWrite(P_ENABLE, LOW);

//  Serial.begin(115200);
}

void loop() {
  digitalWrite(P_DIR, HIGH);
  while(digitalRead(P_LIMIT)){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(100);
    digitalWrite(P_STEP,LOW);
    delayMicroseconds(100);
  }
  delay(50);
  digitalWrite(P_DIR, LOW);
  while(!digitalRead(P_LIMIT)){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(100);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(100);
  }
  //back off until pump ejector is flush with base
  for(int i = 0; i < 800; i++){
    digitalWrite(P_STEP, HIGH);
    delayMicroseconds(100);
    digitalWrite(P_STEP, LOW);
    delayMicroseconds(100);
  }
  digitalWrite(P_ENABLE, HIGH);
  // digitalWrite(P_DIR, HIGH);
  // for(int i = 0; i < 2000; i ++){
  //   digitalWrite(P_STEP, HIGH);
  //   delayMicroseconds(100);
  //   digitalWrite(P_STEP, LOW);
  //   delayMicroseconds(100);
  // }
  // moveWithAcceleration('1', '0', '0', 0.0, 380.0, 0.0);
  // delay(1000);
  // moveWithAcceleration('0', '1', '0', 0.0, 380.0, 0.0);
  // digitalWrite(X_ENABLE, HIGH);
  // digitalWrite(Y_ENABLE, HIGH);
}

void moveWithAcceleration(char x_dir, char y_dir, char z_dir, double x_dist, double y_dist, double z_dist){
  int loopDelay = 0;
  int loop = 0;
  int currentDelayArray[120];
  
  //set x direction
  if(x_dir == '0'){
    digitalWrite(X_DIR, LOW);
  }
  else if(x_dir == '1'){
    digitalWrite(X_DIR, HIGH);
  }
  
  //set x acceleration (use which delay array)
  if(300.0 <= x_dist && x_dist <= 400.0){
    memcpy(currentDelayArray, xLong, sizeof(xLong));
  }
  else if(50.0 <= x_dist && x_dist < 300.0){
    memcpy(currentDelayArray, xMedium, sizeof(xMedium));
  }
  else{
    memcpy(currentDelayArray, xShort, sizeof(xShort));
  }

  int num_of_steps_x = floor(x_dist/distPerStepXY);
  loop = num_of_steps_x/(sizeof(currentDelayArray)/sizeof(currentDelayArray[0]));

  for(unsigned int i=0; i<sizeof(currentDelayArray)/sizeof(currentDelayArray[0]); i++){
    int delay = currentDelayArray[i];
    for(int j = 0; j < loop; j++){
      digitalWrite(X_STEP, HIGH);
      delayMicroseconds(delay);
      digitalWrite(X_STEP, LOW);
      delayMicroseconds(delay);
    }
  }

  //set y direction
  if(y_dir == '0'){
      digitalWrite(Y_DIR, LOW);
  }
  else if(y_dir == '1'){
    digitalWrite(Y_DIR, HIGH);
  }

  //set y acceleration (use which delay array)
  if(300.0 <= y_dist && y_dist <= 400.0){
    memcpy(currentDelayArray, yShort, sizeof(yLong));
  }
  else if(50.0 <= y_dist && y_dist < 300.0){
    memcpy(currentDelayArray, yMedium, sizeof(yMedium));
  }
  else{
    memcpy(currentDelayArray, yShort, sizeof(yShort));
  }

  int num_of_steps_y = floor(y_dist/(distPerStepXY));
  loop = num_of_steps_y/(sizeof(currentDelayArray)/(sizeof(currentDelayArray[0])));

  for(unsigned int i=0; i<sizeof(currentDelayArray)/sizeof(currentDelayArray[0]); i++){
    int delay = currentDelayArray[i];
    for(int j = 0; j < loop; j++){
      digitalWrite(Y_STEP, HIGH);
      delayMicroseconds(delay);
      digitalWrite(Y_STEP, LOW);
      delayMicroseconds(delay);
    }
  }

  //set z direction
  if(z_dir == '0'){
    digitalWrite(Z_DIR, LOW);
  }
  else if(z_dir == '1'){
    digitalWrite(Z_DIR, HIGH);
  }

  //set z acceleration (use which delay array)
  if(325 <= z_dist && z_dist <= 400){
    memcpy(currentDelayArray, zLong, sizeof(zLong));
  }
  else if(250 <= z_dist && z_dist < 325){
    memcpy(currentDelayArray, zMedium, sizeof(zMedium));
  }
  else{
    memcpy(currentDelayArray, zShort, sizeof(zShort));
  }

  int num_of_steps_z = floor(z_dist/distPerStepZ);
  loop = num_of_steps_z/(sizeof(currentDelayArray)/sizeof(currentDelayArray[0]));

  for(unsigned int i=0; i<sizeof(currentDelayArray)/sizeof(currentDelayArray[0]); i++){
    int delay = currentDelayArray[i];
    for(int j = 0; j < loop; j++){
      digitalWrite(Z_STEP, HIGH);
      delayMicroseconds(delay);
      digitalWrite(Z_STEP, LOW);
      delayMicroseconds(delay);
    }
  }
}