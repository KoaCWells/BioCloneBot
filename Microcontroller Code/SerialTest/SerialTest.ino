String confirmation_message = "#pong%";
String host_message = "";
char message_character = ' ';
bool message_complete = false;
bool message_begin = false;
int test;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  while(message_complete == false){
    message_character = Serial.read();
    if(message_character == '#'){
      while(message_complete == false)
      {
        message_character = Serial.read();
        //Serial.write(message_character);
        if(message_character == '%')
        {
          message_complete = true;
          digitalWrite(13, HIGH);
          for(int i = 0; i < confirmation_message.length(); i++)
          {
            Serial.print(confirmation_message[i]);
          }
        }
        else if(message_character != ' ')
        {
          host_message += message_character;
        }
      }
    }
  }
}

void loop() {
  host_message = "";
  message_begin = false;
  message_complete = false;
  Serial.flush();
  //waits for message from serial to establish connection with host computer
  while(message_complete == false){
    test = Serial.read();
    if(test != -1)
    {
      message_character = test;
      Serial.write(message_character);
      if(message_character == '#')
      {
        message_begin == true;
      }
      

      if(message_begin == true)
      {
        if(message_character != '%' && message_character != '#')
        {
          host_message += message_character;
        }
        else if(message_character == '%')
        {
          message_complete = true;
          for(int i = 0; i < host_message.length(); i++)
          {
            Serial.write(host_message[i]);
          }
        }
      }
    }
  }
}
//    if(message_character == '#'){
//      while(message_complete == false)
//      {
//        test = Serial.read();
//        if(test != -1)
//        {
//          message_character = test;
//          Serial.write(message_character);
//          if(message_character == '%')
//          {
//            message_complete = true;
//            confirmation_message = "#deeznuts%";
//            //Serial.write("#deeznuts%");
//            if(host_message == "help")
//            {
//              for(int i = 0; i < confirmation_message.length(); i++)
//              {
//                Serial.write(confirmation_message[i]);
//                //Serial.write(host_message);
//              }
//            }
//          }
//          else if(message_character != '#' && message_character != '%')
//          {
//            host_message += message_character;
//            //Serial.write(message_character);
//          }
//        }
//      }
//    }
