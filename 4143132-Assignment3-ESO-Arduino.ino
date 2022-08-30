// --- Dimitar Prisadnikov ---
// --- 4143132 ---
// --- PCB-04 ---

// --- All assignment requiremtns are met ---
// --- There are no known bugs in the code ---

#include "Display.h";
#include "DHT11.h"

//LEDs
const int LED_RED = 4;
const int LED_GREEN = 5;
const int LED_BLUE = 6;
const int LED_YELLOW = 7;

//Input
const int LBUTTON  = 9;
const int RBUTTON  = 8;
const int Potentiometer = 14;
const int LDR = A2;
const int tempSens = A1;

//By default, when the program is started, the hazard mode is disabled (=0).
int hazardMode = 0;

//Used for controlling the button presses.
int LlastButtonState = HIGH;
int RlastButtonState = HIGH;
int LbuttonState;
int RbuttonState;
int counterLeft = 0;
int counterRight = 0;
int blinkerCounter = 0;

//By default, the steering is not done unless the driver steers the wheel when a blinker is on.
bool steeringDone = false;

//These two counters make sure that the headlights ON/OFF messages are displayed only when there is a change.
int counter_headlights_1 = 0;
int counter_headlights_2 = 0;

//Used for controlling the LEDs in both normal and hazard mode.
int ledState = LOW;

//The time elapsed since a certain event happened; needed for millis().
unsigned long previousTimeBlinked = 0; // Light/blue LED.
unsigned long previousTime_LDR = 0; // Light sensor.
unsigned long previousTime_tempSens = 0; // Temp senosr.
unsigned long previousTimeLights = 0; // Headlights; flashing prevention.
unsigned long currentTime = 0; //The time elapsed since the program was started.

void setup()
{
  pinMode(LED_RED, OUTPUT);
  pinMode(LED_GREEN, OUTPUT);
  pinMode(LED_BLUE, OUTPUT);
  pinMode(LED_YELLOW, OUTPUT);
  pinMode(LBUTTON, INPUT_PULLUP);
  pinMode(RBUTTON, INPUT_PULLUP);
  pinMode(Potentiometer, INPUT);
  Serial.begin(9600);
  Display.show(""); //Clears the display once the system is started.
}

void useSensors() //Gets the temp and light sensors values.
{

  if (currentTime - previousTime_tempSens >= 5000) //Refreshes every 5 seconds.
  {
    float temperature = DHT11.getTemperature(); //Converts the ASCII reading to float.
    Serial.print(temperature);
    Serial.println("  C");

    previousTime_tempSens = currentTime;
  }

  if (currentTime - previousTime_LDR >= 100) //Checks if there is lights every 0.1 seconds.
  {
    int light = analogRead(LDR);
    unsigned long timer = millis();

    if (((light < 100) && timer - previousTimeLights >= 5000)) //Prevents oscillation - in order for the headlights to turn on the light level must stay above the set limit for 5 seconds.
    {
      digitalWrite(LED_GREEN, HIGH); //Turn on the green LED.
      previousTimeLights = timer;

      counter_headlights_1 = 0;
      counter_headlights_2++;
      if (counter_headlights_2 == 1)
      {
        Serial.println("Headlights ON"); //The message is printed only when the headlights state is changed.
      }

    }
    if ((light > 100) && timer - previousTimeLights >= 5000)
    {
      digitalWrite(LED_GREEN, LOW); //Turn off the green LED.
      previousTimeLights = timer;

      counter_headlights_1++;
      counter_headlights_2 = 0;

      if (counter_headlights_1 == 1)
      {
        Serial.println("Headlights OFF"); //The message is printed only when the headlights state is changed.
      }
    }
  }
}

void leftClick() //Left button press - the left (yellow) blinker is activated.
{
  LbuttonState = digitalRead(LBUTTON);
  if (LbuttonState != LlastButtonState)
  {
    LbuttonState = digitalRead(LBUTTON);
    if (LbuttonState == LOW)
    {
      if ((counterRight == 1) && (blinkerCounter == 1)) //Turns off the other blinker (if it is active).
      {
        counterRight = 0;
        blinkerCounter = 0;
      }

      //Counts how many time the buttn was pressed. 0 - it has not been pressed, 1 - pressed once. If it has been pressed twice we give it value 0 since the blinker should be off.
      counterLeft++;
      blinkerCounter++;

      if (blinkerCounter == 1) //First button press.
      {
        Serial.println("Left turn");
        Display.show("--.."); //The display shows which of the two blinkers is active.
      }
    }

    LlastButtonState = LbuttonState;
  }

  if (blinkerCounter == 2) //Second button press - the blinker is off.
  {

    steeringDone = false;
    blinkerCounter = 0;
    Display.show("");
  }
}

void rightClick() //Right button press - the rightt (blue) blinker is activated.
{
  RbuttonState = digitalRead(RBUTTON);
  if (RbuttonState != RlastButtonState)
  {
    RbuttonState = digitalRead(RBUTTON);
    if (RbuttonState == LOW)
    {
      if ((counterLeft == 1) && (blinkerCounter == 1)) //Turns off the other blinker (if it is active).

      {
        counterLeft = 0;
        blinkerCounter = 0;
      }

      //Counts how many time the buttn was pressed. 0 - it has not been pressed, 1 - pressed once. If it has been pressed twice we give it value 0 since the blinker should be off.
      counterRight++;
      blinkerCounter++;

      if (blinkerCounter == 1) //First button press.
      {
        Serial.println("Right turn");
        Display.show("..--"); //The display shows which of two the blinkers is active.
      }
    }

    RlastButtonState = RbuttonState;
  }


  if (blinkerCounter == 2) //Second button press - the blinker is off.
  {
    steeringDone = false;
    blinkerCounter = 0;
    Display.show("");
  }
}



void NormalModeBlinkers() //Normal mode.
{

  if (counterRight == 1) //The button was pressed once.
  {
    unsigned long timer = millis();

    if (timer - previousTimeBlinked >= 500) //The LED turns on and off every 0.5 seconds.
    {
      if (ledState == LOW)
      {
        ledState = HIGH;
      }
      else
      {
        ledState = LOW;
      }

      digitalWrite(LED_BLUE, ledState);
      previousTimeBlinked = timer;

    }
    rightSteering(); //Checks if the blinker should be checked due to the driver steering with the wheel.
  }


  if (counterLeft == 1) //The button was pressed once.
  {
    unsigned long timer = millis();

    if (timer - previousTimeBlinked >= 500) //The LED turns on and off every 0.5 seconds.
    {
      if (ledState == LOW)
      {
        ledState = HIGH;
      }
      else
      {
        ledState = LOW;
      }

      digitalWrite(LED_YELLOW, ledState);
      previousTimeBlinked = timer;

    }
    leftSteering(); //Checks if the blinker should be checked due to the driver steering with the wheel.
  }


  if (((counterRight != 1) && (counterRight != 0)) || (counterLeft != 0)) //If the left button was pressed (!=0), the blue LED is turned off and the right counter is set to 0 (the blinker is off).
  {
    digitalWrite(LED_BLUE, LOW);
    counterRight = 0;
  }

  if (((counterLeft != 1) && (counterLeft != 0)) || (counterRight != 0)) //If the right button was pressed (!=0), the yellow LED is turned off and the left counter is set to 0 (the blinker is off).
  {
    digitalWrite(LED_YELLOW, LOW);
    counterLeft = 0;
  }
}

void HazardModeBlinkers() //Hazard (alarm) mode.
{
  unsigned long timer = millis();

  if (timer - previousTimeBlinked >= 1000) //The LEDs turn on and off alltogether evert 1 second.
  {
    if (ledState == LOW)
    {
      ledState = HIGH;
    }
    else
    {
      ledState = LOW;
    }

    digitalWrite(LED_BLUE, ledState);
    digitalWrite(LED_YELLOW, ledState);
    digitalWrite(LED_RED, ledState);
    previousTimeBlinked = timer;

  }

  //Blinkers can still be activated and disabled by both button press and steering, however, in hazard mode the LED state is not changed - all 3 LEDs keep turning on and off.
  leftSteering();
  rightSteering();
}

void readSerial()
{
  if (Serial.available())
  {
    String line = Serial.readStringUntil('\n');

    if (line == "normal") //If there is such input, the normal mode is activated (it is active by default when the program is started).
    {
      hazardMode = 0;
      Serial.println("Normal Mode");
      digitalWrite(LED_RED, LOW); //Turns off the red LED in case it was previously active due to hazard mode being on.
    }
    if (line == "hazard") //If there is such input, the hazard mode is activated.
    {
      hazardMode = 1;
      Serial.println("ALARM MODE");
    }

  }
}

//Gets the potentiometer value.
int POT_angle() {
  int POT = analogRead(Potentiometer);
  int angle;
  angle = map(POT, 0, 1023, 10, -10);
  return angle;
}

void leftSteering() //Turns off the blinker if the steering wheel was used to turn.
{
  int angle = POT_angle(); //Gets the pot sensor readings.

  if (angle > 4) //Has the driver steered enough to left?
  {
    steeringDone = true; //Ff he did so, the steering can be considered done.
  }
  if ((steeringDone == true) && (angle > -2) && (angle < 2)) //Once the steering wheel is straight in the center or a little bit in left/right (+-2), the blinker is disabled.
  {
    counterLeft = 0;
    blinkerCounter = 0;
    digitalWrite(LED_YELLOW, LOW);
    steeringDone = false;
    Display.show("");

  }
}


void rightSteering() //Turns off the blinker if the steering wheel was used to turn.
{
  int angle = POT_angle(); //Gets the pot sensor readings.

  if (angle < -4) //Has the driver steered enough to right?
  {
    steeringDone = true; //If he did so, the steering can be considered done.
  }
  if ((steeringDone == true) && (angle > -2) && (angle < 2)) //Once the steering wheel is straight in the center or a little bit in left/right (+-2), the blinker is disabled.
  {
    counterRight = 0;
    blinkerCounter = 0;
    digitalWrite(LED_BLUE, LOW);
    steeringDone = false;
    Display.show("");

  }
}


void loop() {

  currentTime = millis(); //Gets the time elapsed since the program was started.

  useSensors();
  readSerial();

  if (hazardMode == 0)
  {

    rightClick();
    leftClick();

    NormalModeBlinkers();
  }


  //Blinkers can be switched on and off even when the hazard mode is on, however, the LEDs state will not be changed.
  //Still, the driver will be able to see which blinker is active on the Arduino RichShield Display.
  if (hazardMode == 1)
  {
    rightClick();
    leftClick();

    HazardModeBlinkers();

    if (((counterRight != 1) && (counterRight != 0)) || (counterLeft != 0))
    {
      counterRight = 0;
    }

    if (((counterLeft != 1) && (counterLeft != 0)) || (counterRight != 0))
    {
      counterLeft = 0;
    }
  }


}
