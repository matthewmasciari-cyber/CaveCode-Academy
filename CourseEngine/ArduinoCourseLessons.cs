namespace CaveCode.CourseEngine;

public static class ArduinoCourseLessons
{
    public const int PlayableModuleCount = 40;

    public static IReadOnlyList<CourseLesson> All { get; } =
        new[]
        {
            new CourseLesson(
                "Chapter 1 · Sketch Foundations",
                "Sketch structure",
                "Power the Maker Lab",
                "An Arduino program is a sketch. setup() runs once at start. loop() runs over and over.",
                "void setup()\n{\n}\n\nvoid loop()\n{\n}",
                "void setup()\n{\n}\n\nvoid loop()\n{\n}",
                "void ___()\n{\n}\n\nvoid ___()\n{\n}",
                "Which function runs again and again after startup?",
                new[] { "setup", "loop", "main", "pinMode" },
                1,
                "loop repeats for the life of the sketch. setup runs once.",
                "void setup()\n{\n}\n\nvoid Loop()\n{\n}",
                "Function names are case-sensitive. Use loop in lowercase.",
                "Write an empty sketch with setup and loop.",
                "Write the same empty sketch structure again for a second board profile.",
                "void setup()\n{\n}\n\nvoid loop()\n{\n}",
                "Board skeleton online. Ready for pins."
            )
            {
                ConceptPoints = new[] { "setup runs once at power-up.", "loop repeats continuously.", "Sketches use a fixed entry pattern." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 1 · Sketch Foundations",
                "pinMode output",
                "Claim a Digital Pin",
                "pinMode(pin, OUTPUT) configures a pin as an output so digitalWrite can control it.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n}",
                "void setup()\n{\n  ___(13, OUTPUT);\n}\n\nvoid loop()\n{\n}",
                "Which call sets pin direction?",
                new[] { "digitalWrite", "pinMode", "delay", "analogRead" },
                1,
                "pinMode configures the pin. digitalWrite changes its level later.",
                "void setup()\n{\n  pinMode(13, output);\n}\n\nvoid loop()\n{\n}",
                "OUTPUT must be uppercase in this lab style.",
                "In setup, set pin 13 to OUTPUT. Keep an empty loop.",
                "Configure pin 8 as OUTPUT in setup with an empty loop.",
                "void setup()\n{\n  pinMode(8, OUTPUT);\n}\n\nvoid loop()\n{\n}",
                "Pin 13 reserved as an output channel."
            )
            {
                ConceptPoints = new[] { "pinMode sets direction.", "OUTPUT means the pin will drive a load.", "Call pinMode in setup before using the pin." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 1 · Sketch Foundations",
                "digitalWrite",
                "Drive the Pin HIGH",
                "digitalWrite(pin, HIGH) drives an output high. digitalWrite(pin, LOW) drives it low.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n  digitalWrite(13, HIGH);\n}\n\nvoid loop()\n{\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n  digitalWrite(13, HIGH);\n}\n\nvoid loop()\n{\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n  ___(13, HIGH);\n}\n\nvoid loop()\n{\n}",
                "What does digitalWrite(13, HIGH) do?",
                new[] { "Read pin 13", "Set pin 13 high", "Wait 13 ms", "Clear memory" },
                1,
                "digitalWrite sets the output level of a pin already configured as OUTPUT.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n  digitalWrite(13, high);\n}\n\nvoid loop()\n{\n}",
                "HIGH should be uppercase in this lab style.",
                "Set pin 13 OUTPUT and drive it HIGH in setup.",
                "Set pin 13 OUTPUT and drive it LOW in setup.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n  digitalWrite(13, LOW);\n}\n\nvoid loop()\n{\n}",
                "Lab LED driven high on pin 13."
            )
            {
                ConceptPoints = new[] { "HIGH and LOW are output levels.", "digitalWrite changes a pin after pinMode.", "Put one-shot setup writes in setup()." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 1 · Sketch Foundations",
                "delay",
                "Hold a Level with delay",
                "delay(ms) pauses the sketch for milliseconds. Early labs use it for visible timing.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(1000);\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(1000);\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  ___(1000);\n}",
                "delay(1000) waits about how long?",
                new[] { "1 millisecond", "1 second", "10 seconds", "13 cycles" },
                1,
                "1000 milliseconds is one second.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  Delay(1000);\n}",
                "Use delay in lowercase.",
                "pinMode 13 OUTPUT; loop drives HIGH then delay 1000.",
                "Same structure but delay 500 after HIGH.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(500);\n}",
                "Timing online. delay holds the high level."
            )
            {
                ConceptPoints = new[] { "delay pauses execution.", "Argument is milliseconds.", "Early labs use delay for visible blink timing." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 1 · Sketch Foundations",
                "Blink pattern",
                "Classic Blink",
                "Classic blink: HIGH, delay, LOW, delay, repeat in loop.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(500);\n  digitalWrite(13, LOW);\n  delay(500);\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(500);\n  digitalWrite(13, LOW);\n  delay(500);\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(500);\n  digitalWrite(13, ___);\n  delay(500);\n}",
                "After HIGH and delay, the next level is usually?",
                new[] { "HIGH again", "LOW", "INPUT", "OUTPUT" },
                1,
                "Blink alternates HIGH and LOW with delays between.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(500);\n  digitalWrite(13, LOW);\n  delay(500)\n}",
                "The last delay needs a semicolon.",
                "Classic blink: 500 ms on, 500 ms off on pin 13.",
                "Blink with 250 ms on and 250 ms off on pin 13.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(250);\n  digitalWrite(13, LOW);\n  delay(250);\n}",
                "LED blink pattern running on the lab board."
            )
            {
                ConceptPoints = new[] { "Blink alternates HIGH and LOW.", "Equal delays make a steady flash.", "loop repeats the pattern forever." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 1 · Sketch Foundations",
                "Comments",
                "Document the Sketch",
                "// starts a single-line comment. The compiler ignores comments.",
                "// Built-in LED on pin 13\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n}",
                "// Built-in LED on pin 13\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n}",
                "___ Built-in LED on pin 13\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n}",
                "Which marker starts a single-line comment?",
                new[] { "#", "//", "/* only", "rem" },
                1,
                "// begins a single-line comment in Arduino C++.",
                "/ Built-in LED on pin 13\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}",
                "Use two slashes, not one.",
                "Add a // comment above setup, then pin 13 OUTPUT and HIGH in loop.",
                "Comment // Status lamp, pinMode 13 OUTPUT, loop writes HIGH.",
                "// Status lamp\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n}",
                "Comment documented. Lab notes online."
            )
            {
                ConceptPoints = new[] { "// starts a line comment.", "Comments are not executed.", "Describe pins and intent briefly." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 1 · Sketch Foundations",
                "Named pins",
                "Name the LED Pin",
                "const int ledPin = 13; then use ledPin in pinMode and digitalWrite.",
                "const int ledPin = 13;\n\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n}",
                "const int ledPin = 13;\n\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n}",
                "const int ___ = 13;\n\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n}",
                "Why use const int ledPin = 13?",
                new[] { "Faster CPU", "Clearer pin naming", "Required by loop", "Disables delay" },
                1,
                "A named pin constant documents meaning and centralizes the number.",
                "const int ledPin = 13\n\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}",
                "The const declaration needs a semicolon.",
                "Declare const int ledPin = 13; use it in pinMode and digitalWrite HIGH.",
                "Declare const int lampPin = 13; OUTPUT in setup; HIGH in loop.",
                "const int lampPin = 13;\n\nvoid setup()\n{\n  pinMode(lampPin, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(lampPin, HIGH);\n}",
                "Named pin constant active in the lab sketch."
            )
            {
                ConceptPoints = new[] { "const int names a pin.", "Reuse the name in pinMode and digitalWrite.", "Change the number in one place later." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 1 · Sketch Foundations",
                "Chapter review",
                "Close Chapter 1",
                "Chapter 1 assembled setup/loop, pinMode, digitalWrite, delay, comments, and named pins.",
                "const int ledPin = 13;\n\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(200);\n  digitalWrite(ledPin, LOW);\n  delay(200);\n}",
                "const int ledPin = 13;\n\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(200);\n  digitalWrite(ledPin, LOW);\n  delay(200);\n}",
                "const int ledPin = 13;\n\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(___);\n  digitalWrite(ledPin, LOW);\n  delay(___);\n}",
                "Which pair is the sketch entry pattern?",
                new[] { "setup and loop", "main and init", "start and run", "begin and end" },
                0,
                "Arduino sketches use setup once and loop repeatedly.",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT)\n}",
                "pinMode line needs a semicolon.",
                "Named blink: ledPin 13, 200 ms HIGH and LOW.",
                "Named blink on ledPin 13 with 100 ms HIGH and LOW.",
                "const int ledPin = 13;\n\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(100);\n  digitalWrite(ledPin, LOW);\n  delay(100);\n}",
                "Chapter 1 complete. Maker lab board is blinking on command."
            )
            {
                ConceptPoints = new[] { "Chapter 1 is the sketch spine.", "Pins need direction then levels.", "delay creates visible timing for early labs." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Steady LED",
                "Steady LED",
                "Hold an output steady HIGH after setup so the lamp stays on.",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n  digitalWrite(ledPin, HIGH);\n}\nvoid loop()\n{\n}",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n  digitalWrite(ledPin, HIGH);\n}\nvoid loop()\n{\n}",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, ___);\n  digitalWrite(ledPin, HIGH);\n}\nvoid loop()\n{\n}",
                "What does pinMode(..., OUTPUT) enable?",
                new[] { "Reading analog", "Driving a pin high or low", "USB upload", "Serial only" },
                1,
                "OUTPUT allows digitalWrite to drive the pin.",
                "const int ledPin = 13\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n  digitalWrite(ledPin, HIGH);\n}\nvoid loop()\n{\n}",
                "Check for a missing semicolon.",
                "Rebuild the example for this module.",
                "Rebuild with pin 12 instead of 13 where a single LED pin is used.",
                "const int ledPin = 12;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n  digitalWrite(ledPin, HIGH);\n}\nvoid loop()\n{\n}",
                "Output lab: Steady LED."
            )
            {
                ConceptPoints = new[] { "digitalWrite in setup for a steady on state.", "digitalWrite needs pinMode first.", "loop can refresh levels continuously." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Blink control",
                "Blink control",
                "Change only the delay values to control blink speed.",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(100);\n  digitalWrite(ledPin, LOW);\n  delay(100);\n}",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(100);\n  digitalWrite(ledPin, LOW);\n  delay(100);\n}",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, ___);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(100);\n  digitalWrite(ledPin, LOW);\n  delay(100);\n}",
                "What does pinMode(..., OUTPUT) enable?",
                new[] { "Reading analog", "Driving a pin high or low", "USB upload", "Serial only" },
                1,
                "OUTPUT allows digitalWrite to drive the pin.",
                "const int ledPin = 13\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(100);\n  digitalWrite(ledPin, LOW);\n  delay(100);\n}",
                "Check for a missing semicolon.",
                "Rebuild the example for this module.",
                "Rebuild with pin 12 instead of 13 where a single LED pin is used.",
                "const int ledPin = 12;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(100);\n  digitalWrite(ledPin, LOW);\n  delay(100);\n}",
                "Output lab: Blink control."
            )
            {
                ConceptPoints = new[] { "Shorter delays blink faster.", "digitalWrite needs pinMode first.", "loop can refresh levels continuously." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Multi-step pattern",
                "Multi-step pattern",
                "HIGH, short delay, LOW, longer delay for an asymmetric flash.",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(100);\n  digitalWrite(ledPin, LOW);\n  delay(900);\n}",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(100);\n  digitalWrite(ledPin, LOW);\n  delay(900);\n}",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, ___);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(100);\n  digitalWrite(ledPin, LOW);\n  delay(900);\n}",
                "What does pinMode(..., OUTPUT) enable?",
                new[] { "Reading analog", "Driving a pin high or low", "USB upload", "Serial only" },
                1,
                "OUTPUT allows digitalWrite to drive the pin.",
                "const int ledPin = 13\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(100);\n  digitalWrite(ledPin, LOW);\n  delay(900);\n}",
                "Check for a missing semicolon.",
                "Rebuild the example for this module.",
                "Rebuild with pin 12 instead of 13 where a single LED pin is used.",
                "const int ledPin = 12;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(100);\n  digitalWrite(ledPin, LOW);\n  delay(900);\n}",
                "Output lab: Multi-step pattern."
            )
            {
                ConceptPoints = new[] { "Unequal delays create a heartbeat-style pattern.", "digitalWrite needs pinMode first.", "loop can refresh levels continuously." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Active-low LED",
                "Active-low LED",
                "Some LEDs light when the pin is LOW. Drive LOW to turn on.",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n  digitalWrite(ledPin, LOW);\n}\nvoid loop()\n{\n}",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n  digitalWrite(ledPin, LOW);\n}\nvoid loop()\n{\n}",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, ___);\n  digitalWrite(ledPin, LOW);\n}\nvoid loop()\n{\n}",
                "What does pinMode(..., OUTPUT) enable?",
                new[] { "Reading analog", "Driving a pin high or low", "USB upload", "Serial only" },
                1,
                "OUTPUT allows digitalWrite to drive the pin.",
                "const int ledPin = 13\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n  digitalWrite(ledPin, LOW);\n}\nvoid loop()\n{\n}",
                "Check for a missing semicolon.",
                "Rebuild the example for this module.",
                "Rebuild with pin 12 instead of 13 where a single LED pin is used.",
                "const int ledPin = 12;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n  digitalWrite(ledPin, LOW);\n}\nvoid loop()\n{\n}",
                "Output lab: Active-low LED."
            )
            {
                ConceptPoints = new[] { "Active-low means LOW is the on level.", "digitalWrite needs pinMode first.", "loop can refresh levels continuously." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Two outputs",
                "Two outputs",
                "Control two pins: LED on 13 and a second lamp on 12.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n  pinMode(12, OUTPUT);\n  digitalWrite(13, HIGH);\n  digitalWrite(12, LOW);\n}\nvoid loop()\n{\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n  pinMode(12, OUTPUT);\n  digitalWrite(13, HIGH);\n  digitalWrite(12, LOW);\n}\nvoid loop()\n{\n}",
                "void setup()\n{\n  pinMode(13, ___);\n  pinMode(12, OUTPUT);\n  digitalWrite(13, HIGH);\n  digitalWrite(12, LOW);\n}\nvoid loop()\n{\n}",
                "What does pinMode(..., OUTPUT) enable?",
                new[] { "Reading analog", "Driving a pin high or low", "USB upload", "Serial only" },
                1,
                "OUTPUT allows digitalWrite to drive the pin.",
                "void setup()\n{\n  pinMode(13, OUTPUT)\n  pinMode(12, OUTPUT);\n  digitalWrite(13, HIGH);\n  digitalWrite(12, LOW);\n}\nvoid loop()\n{\n}",
                "Check for a missing semicolon.",
                "Rebuild the example for this module.",
                "Rebuild with pin 12 instead of 13 where a single LED pin is used.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n  pinMode(12, OUTPUT);\n  digitalWrite(13, HIGH);\n  digitalWrite(12, LOW);\n}\nvoid loop()\n{\n}",
                "Output lab: Two outputs."
            )
            {
                ConceptPoints = new[] { "Each pin needs its own pinMode and digitalWrite.", "digitalWrite needs pinMode first.", "loop can refresh levels continuously." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Duty feel",
                "Duty feel",
                "Long HIGH, short LOW approximates a brighter blink feel with delay.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(800);\n  digitalWrite(13, LOW);\n  delay(200);\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(800);\n  digitalWrite(13, LOW);\n  delay(200);\n}",
                "void setup()\n{\n  pinMode(13, ___);\n}\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(800);\n  digitalWrite(13, LOW);\n  delay(200);\n}",
                "What does pinMode(..., OUTPUT) enable?",
                new[] { "Reading analog", "Driving a pin high or low", "USB upload", "Serial only" },
                1,
                "OUTPUT allows digitalWrite to drive the pin.",
                "void setup()\n{\n  pinMode(13, OUTPUT)\n}\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(800);\n  digitalWrite(13, LOW);\n  delay(200);\n}",
                "Check for a missing semicolon.",
                "Rebuild the example for this module.",
                "Rebuild with pin 12 instead of 13 where a single LED pin is used.",
                "void setup()\n{\n  pinMode(12, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(12, HIGH);\n  delay(800);\n  digitalWrite(12, LOW);\n  delay(200);\n}",
                "Output lab: Duty feel."
            )
            {
                ConceptPoints = new[] { "Duty cycle is the fraction of time spent HIGH.", "digitalWrite needs pinMode first.", "loop can refresh levels continuously." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Status LED",
                "Status LED",
                "Use pin 13 as a status lamp that stays HIGH while the sketch runs.",
                "const int statusPin = 13;\nvoid setup()\n{\n  pinMode(statusPin, OUTPUT);\n  digitalWrite(statusPin, HIGH);\n}\nvoid loop()\n{\n}",
                "const int statusPin = 13;\nvoid setup()\n{\n  pinMode(statusPin, OUTPUT);\n  digitalWrite(statusPin, HIGH);\n}\nvoid loop()\n{\n}",
                "const int statusPin = 13;\nvoid setup()\n{\n  pinMode(statusPin, ___);\n  digitalWrite(statusPin, HIGH);\n}\nvoid loop()\n{\n}",
                "What does pinMode(..., OUTPUT) enable?",
                new[] { "Reading analog", "Driving a pin high or low", "USB upload", "Serial only" },
                1,
                "OUTPUT allows digitalWrite to drive the pin.",
                "const int statusPin = 13\nvoid setup()\n{\n  pinMode(statusPin, OUTPUT);\n  digitalWrite(statusPin, HIGH);\n}\nvoid loop()\n{\n}",
                "Check for a missing semicolon.",
                "Rebuild the example for this module.",
                "Rebuild with pin 12 instead of 13 where a single LED pin is used.",
                "const int statusPin = 12;\nvoid setup()\n{\n  pinMode(statusPin, OUTPUT);\n  digitalWrite(statusPin, HIGH);\n}\nvoid loop()\n{\n}",
                "Output lab: Status LED."
            )
            {
                ConceptPoints = new[] { "A status LED confirms the board is running your sketch.", "digitalWrite needs pinMode first.", "loop can refresh levels continuously." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Output lab",
                "Output lab",
                "Combine named pin, OUTPUT, and a 300 ms blink.",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(300);\n  digitalWrite(ledPin, LOW);\n  delay(300);\n}",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(300);\n  digitalWrite(ledPin, LOW);\n  delay(300);\n}",
                "const int ledPin = 13;\nvoid setup()\n{\n  pinMode(ledPin, ___);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(300);\n  digitalWrite(ledPin, LOW);\n  delay(300);\n}",
                "What does pinMode(..., OUTPUT) enable?",
                new[] { "Reading analog", "Driving a pin high or low", "USB upload", "Serial only" },
                1,
                "OUTPUT allows digitalWrite to drive the pin.",
                "const int ledPin = 13\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(300);\n  digitalWrite(ledPin, LOW);\n  delay(300);\n}",
                "Check for a missing semicolon.",
                "Rebuild the example for this module.",
                "Rebuild with pin 12 instead of 13 where a single LED pin is used.",
                "const int ledPin = 12;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(ledPin, HIGH);\n  delay(300);\n  digitalWrite(ledPin, LOW);\n  delay(300);\n}",
                "Output lab: Output lab."
            )
            {
                ConceptPoints = new[] { "Chapter 2 output patterns lock in.", "digitalWrite needs pinMode first.", "loop can refresh levels continuously." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Read a pin",
                "Read a pin",
                "pinMode(pin, INPUT) then digitalRead(pin) returns HIGH or LOW.",
                "int v;\nvoid setup()\n{\n  pinMode(2, INPUT);\n}\nvoid loop()\n{\n  v = digitalRead(2);\n}",
                "int v;\nvoid setup()\n{\n  pinMode(2, INPUT);\n}\nvoid loop()\n{\n  v = digitalRead(2);\n}",
                "int v;\nvoid setup()\n{\n  pinMode(2, INPUT);\n}\nvoid loop()\n{\n  v = ___(2);\n}",
                "digitalRead returns?",
                new[] { "Only floats", "HIGH or LOW", "Pin mode", "Milliseconds" },
                1,
                "digitalRead returns the digital level of the pin.",
                "int v;\nvoid setup()\n{\n  pinMode(2, INPUT);\n}\nvoid loop()\n{\n  v = digitalRead(2);\n}",
                "Comparison in conditions often uses ==.",
                "Rebuild this input example.",
                "Use pin 3 instead of pin 2 for the input.",
                "int v;\nvoid setup()\n{\n  pinMode(3, INPUT);\n}\nvoid loop()\n{\n  v = digitalRead(3);\n}",
                "Input lab: Read a pin."
            )
            {
                ConceptPoints = new[] { "digitalRead samples the pin level.", "Configure direction before reading.", "Buttons often use INPUT_PULLUP." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Button level",
                "Button level",
                "Wire a button to pin 2; read it and copy the level to the LED on 13.",
                "void setup()\n{\n  pinMode(2, INPUT);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, digitalRead(2));\n}",
                "void setup()\n{\n  pinMode(2, INPUT);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, digitalRead(2));\n}",
                "void setup()\n{\n  pinMode(2, INPUT);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, ___(2));\n}",
                "digitalRead returns?",
                new[] { "Only floats", "HIGH or LOW", "Pin mode", "Milliseconds" },
                1,
                "digitalRead returns the digital level of the pin.",
                "void setup()\n{\n  pinMode(2, INPUT);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, digitalRead(2));\n}",
                "Comparison in conditions often uses ==.",
                "Rebuild this input example.",
                "Use pin 3 instead of pin 2 for the input.",
                "void setup()\n{\n  pinMode(3, INPUT);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, digitalRead(3));\n}",
                "Input lab: Button level."
            )
            {
                ConceptPoints = new[] { "Mirror input to output for a live follow.", "Configure direction before reading.", "Buttons often use INPUT_PULLUP." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Pull-up idea",
                "Pull-up idea",
                "INPUT_PULLUP enables the internal pull-up so an open button reads HIGH.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n}\nvoid loop()\n{\n}",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n}\nvoid loop()\n{\n}",
                "void setup()\n{\n  pinMode(2, ___);\n}\nvoid loop()\n{\n}",
                "digitalRead returns?",
                new[] { "Only floats", "HIGH or LOW", "Pin mode", "Milliseconds" },
                1,
                "digitalRead returns the digital level of the pin.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n}\nvoid loop()\n{\n}",
                "Comparison in conditions often uses ==.",
                "Rebuild this input example.",
                "Use pin 3 instead of pin 2 for the input.",
                "void setup()\n{\n  pinMode(3, INPUT_PULLUP);\n}\nvoid loop()\n{\n}",
                "Input lab: Pull-up idea."
            )
            {
                ConceptPoints = new[] { "INPUT_PULLUP avoids a floating pin.", "Configure direction before reading.", "Buttons often use INPUT_PULLUP." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Edge vs level",
                "Edge vs level",
                "If digitalRead is LOW, turn the LED on. Level check each loop.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(2, ___);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "digitalRead returns?",
                new[] { "Only floats", "HIGH or LOW", "Pin mode", "Milliseconds" },
                1,
                "digitalRead returns the digital level of the pin.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) = LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Comparison in conditions often uses ==.",
                "Rebuild this input example.",
                "Use pin 3 instead of pin 2 for the input.",
                "void setup()\n{\n  pinMode(3, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(3) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Input lab: Edge vs level."
            )
            {
                ConceptPoints = new[] { "Level checks run every loop iteration.", "Configure direction before reading.", "Buttons often use INPUT_PULLUP." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Debounce idea",
                "Debounce idea",
                "After detecting a press, delay briefly before reading again.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n    delay(50);\n  }\n}",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n    delay(50);\n  }\n}",
                "void setup()\n{\n  pinMode(2, ___);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n    delay(50);\n  }\n}",
                "digitalRead returns?",
                new[] { "Only floats", "HIGH or LOW", "Pin mode", "Milliseconds" },
                1,
                "digitalRead returns the digital level of the pin.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) = LOW)\n  {\n    digitalWrite(13, HIGH);\n    delay(50);\n  }\n}",
                "Comparison in conditions often uses ==.",
                "Rebuild this input example.",
                "Use pin 3 instead of pin 2 for the input.",
                "void setup()\n{\n  pinMode(3, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(3) == LOW)\n  {\n    digitalWrite(13, HIGH);\n    delay(50);\n  }\n}",
                "Input lab: Debounce idea."
            )
            {
                ConceptPoints = new[] { "A short delay reduces bounce chatter in simple labs.", "Configure direction before reading.", "Buttons often use INPUT_PULLUP." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Button to LED",
                "Button to LED",
                "Press (LOW with pull-up) lights the LED; release turns it off.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(2, ___);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "digitalRead returns?",
                new[] { "Only floats", "HIGH or LOW", "Pin mode", "Milliseconds" },
                1,
                "digitalRead returns the digital level of the pin.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) = LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Comparison in conditions often uses ==.",
                "Rebuild this input example.",
                "Use pin 3 instead of pin 2 for the input.",
                "void setup()\n{\n  pinMode(3, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(3) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Input lab: Button to LED."
            )
            {
                ConceptPoints = new[] { "INPUT_PULLUP buttons are often active LOW.", "Configure direction before reading.", "Buttons often use INPUT_PULLUP." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Hold detect",
                "Hold detect",
                "While the button stays LOW, keep the LED HIGH.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, digitalRead(2) == LOW ? HIGH : LOW);\n}",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, digitalRead(2) == LOW ? HIGH : LOW);\n}",
                "void setup()\n{\n  pinMode(2, ___);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, digitalRead(2) == LOW ? HIGH : LOW);\n}",
                "digitalRead returns?",
                new[] { "Only floats", "HIGH or LOW", "Pin mode", "Milliseconds" },
                1,
                "digitalRead returns the digital level of the pin.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, digitalRead(2) = LOW ? HIGH : LOW);\n}",
                "Comparison in conditions often uses ==.",
                "Rebuild this input example.",
                "Use pin 3 instead of pin 2 for the input.",
                "void setup()\n{\n  pinMode(3, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  digitalWrite(13, digitalRead(3) == LOW ? HIGH : LOW);\n}",
                "Input lab: Hold detect."
            )
            {
                ConceptPoints = new[] { "Hold means the condition stays true across loops.", "Configure direction before reading.", "Buttons often use INPUT_PULLUP." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Input lab",
                "Input lab",
                "INPUT_PULLUP on 2, LED on 13, LED on only while button is pressed.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(2, ___);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "digitalRead returns?",
                new[] { "Only floats", "HIGH or LOW", "Pin mode", "Milliseconds" },
                1,
                "digitalRead returns the digital level of the pin.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) = LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Comparison in conditions often uses ==.",
                "Rebuild this input example.",
                "Use pin 3 instead of pin 2 for the input.",
                "void setup()\n{\n  pinMode(3, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(3) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Input lab: Input lab."
            )
            {
                ConceptPoints = new[] { "Chapter 3 input patterns complete.", "Configure direction before reading.", "Buttons often use INPUT_PULLUP." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "State variable",
                "State variable",
                "int ledState = LOW; flip it each loop with digitalWrite.",
                "int ledState = LOW;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  ledState = (ledState == LOW) ? HIGH : LOW;\n  digitalWrite(13, ledState);\n  delay(500);\n}",
                "int ledState = LOW;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  ledState = (ledState == LOW) ? HIGH : LOW;\n  digitalWrite(13, ledState);\n  delay(500);\n}",
                "int ledState = LOW;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  ledState = (ledState == LOW) ? HIGH : LOW;\n  digitalWrite(13, ledState);\n  delay(500);\n}",
                "What does a state variable help with?",
                new[] { "USB speed", "Remembering values across loops", "Pin voltage only", "Compiler errors" },
                1,
                "State persists between loop iterations.",
                "int ledState = LOW;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  ledState = (ledState = LOW) ? HIGH : LOW;\n  digitalWrite(13, ledState);\n  delay(500);\n}",
                "Use == inside conditions.",
                "Rebuild this timing/state example.",
                "Change 500 to 250 where a 500 ms threshold appears, else keep structure.",
                "int ledState = LOW;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  ledState = (ledState == LOW) ? HIGH : LOW;\n  digitalWrite(13, ledState);\n  delay(250);\n}",
                "State lab: State variable."
            )
            {
                ConceptPoints = new[] { "State variables remember values across loop turns.", "State machines structure multi-step behavior.", "millis supports non-blocking timing." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Mode flags",
                "Mode flags",
                "bool active = true; only blink while active is true.",
                "bool active = true;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (active)\n  {\n    digitalWrite(13, HIGH);\n    delay(200);\n    digitalWrite(13, LOW);\n    delay(200);\n  }\n}",
                "bool active = true;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (active)\n  {\n    digitalWrite(13, HIGH);\n    delay(200);\n    digitalWrite(13, LOW);\n    delay(200);\n  }\n}",
                "___ active = true;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (active)\n  {\n    digitalWrite(13, HIGH);\n    delay(200);\n    digitalWrite(13, LOW);\n    delay(200);\n  }\n}",
                "What does a state variable help with?",
                new[] { "USB speed", "Remembering values across loops", "Pin voltage only", "Compiler errors" },
                1,
                "State persists between loop iterations.",
                "bool active = true;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (active)\n  {\n    digitalWrite(13, HIGH);\n    delay(200);\n    digitalWrite(13, LOW);\n    delay(200);\n  }\n}",
                "Use == inside conditions.",
                "Rebuild this timing/state example.",
                "Change 500 to 250 where a 500 ms threshold appears, else keep structure.",
                "bool active = true;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (active)\n  {\n    digitalWrite(13, HIGH);\n    delay(200);\n    digitalWrite(13, LOW);\n    delay(200);\n  }\n}",
                "State lab: Mode flags."
            )
            {
                ConceptPoints = new[] { "Flags gate whole behaviors.", "State machines structure multi-step behavior.", "millis supports non-blocking timing." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Non-blocking idea",
                "Non-blocking idea",
                "Track lastToggle with millis() instead of only delay.",
                "unsigned long lastToggle = 0;\nint ledState = LOW;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (millis() - lastToggle >= 500)\n  {\n    lastToggle = millis();\n    ledState = (ledState == LOW) ? HIGH : LOW;\n    digitalWrite(13, ledState);\n  }\n}",
                "unsigned long lastToggle = 0;\nint ledState = LOW;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (millis() - lastToggle >= 500)\n  {\n    lastToggle = millis();\n    ledState = (ledState == LOW) ? HIGH : LOW;\n    digitalWrite(13, ledState);\n  }\n}",
                "unsigned long lastToggle = 0;\nint ledState = LOW;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (___() - lastToggle >= 500)\n  {\n    lastToggle = millis();\n    ledState = (ledState == LOW) ? HIGH : LOW;\n    digitalWrite(13, ledState);\n  }\n}",
                "What does a state variable help with?",
                new[] { "USB speed", "Remembering values across loops", "Pin voltage only", "Compiler errors" },
                1,
                "State persists between loop iterations.",
                "unsigned long lastToggle = 0;\nint ledState = LOW;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (millis() - lastToggle >= 500)\n  {\n    lastToggle = millis();\n    ledState = (ledState = LOW) ? HIGH : LOW;\n    digitalWrite(13, ledState);\n  }\n}",
                "Use == inside conditions.",
                "Rebuild this timing/state example.",
                "Change 500 to 250 where a 500 ms threshold appears, else keep structure.",
                "unsigned long lastToggle = 0;\nint ledState = LOW;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (millis() - lastToggle >= 250)\n  {\n    lastToggle = millis();\n    ledState = (ledState == LOW) ? HIGH : LOW;\n    digitalWrite(13, ledState);\n  }\n}",
                "State lab: Non-blocking idea."
            )
            {
                ConceptPoints = new[] { "millis() enables timing without blocking forever.", "State machines structure multi-step behavior.", "millis supports non-blocking timing." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Two-phase blink",
                "Two-phase blink",
                "Use a state 0/1 machine: 0 drives HIGH, 1 drives LOW.",
                "int phase = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (phase == 0)\n  {\n    digitalWrite(13, HIGH);\n    delay(300);\n    phase = 1;\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n    delay(300);\n    phase = 0;\n  }\n}",
                "int phase = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (phase == 0)\n  {\n    digitalWrite(13, HIGH);\n    delay(300);\n    phase = 1;\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n    delay(300);\n    phase = 0;\n  }\n}",
                "int phase = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (phase == 0)\n  {\n    digitalWrite(13, HIGH);\n    delay(300);\n    phase = 1;\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n    delay(300);\n    phase = 0;\n  }\n}",
                "What does a state variable help with?",
                new[] { "USB speed", "Remembering values across loops", "Pin voltage only", "Compiler errors" },
                1,
                "State persists between loop iterations.",
                "int phase = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (phase = 0)\n  {\n    digitalWrite(13, HIGH);\n    delay(300);\n    phase = 1;\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n    delay(300);\n    phase = 0;\n  }\n}",
                "Use == inside conditions.",
                "Rebuild this timing/state example.",
                "Change 500 to 250 where a 500 ms threshold appears, else keep structure.",
                "int phase = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (phase == 0)\n  {\n    digitalWrite(13, HIGH);\n    delay(300);\n    phase = 1;\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n    delay(300);\n    phase = 0;\n  }\n}",
                "State lab: Two-phase blink."
            )
            {
                ConceptPoints = new[] { "Phases sequence multi-step patterns.", "State machines structure multi-step behavior.", "millis supports non-blocking timing." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Sequence steps",
                "Sequence steps",
                "step goes 0,1,2 with different delays per step.",
                "int step = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (step == 0) { digitalWrite(13, HIGH); delay(100); step = 1; }\n  else if (step == 1) { digitalWrite(13, LOW); delay(100); step = 2; }\n  else { digitalWrite(13, HIGH); delay(400); step = 0; }\n}",
                "int step = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (step == 0) { digitalWrite(13, HIGH); delay(100); step = 1; }\n  else if (step == 1) { digitalWrite(13, LOW); delay(100); step = 2; }\n  else { digitalWrite(13, HIGH); delay(400); step = 0; }\n}",
                "int step = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (step == 0) { digitalWrite(13, HIGH); delay(100); step = 1; }\n  else if (step == 1) { digitalWrite(13, LOW); delay(100); step = 2; }\n  else { digitalWrite(13, HIGH); delay(400); step = 0; }\n}",
                "What does a state variable help with?",
                new[] { "USB speed", "Remembering values across loops", "Pin voltage only", "Compiler errors" },
                1,
                "State persists between loop iterations.",
                "int step = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (step = 0) { digitalWrite(13, HIGH); delay(100); step = 1; }\n  else if (step = 1) { digitalWrite(13, LOW); delay(100); step = 2; }\n  else { digitalWrite(13, HIGH); delay(400); step = 0; }\n}",
                "Use == inside conditions.",
                "Rebuild this timing/state example.",
                "Change 500 to 250 where a 500 ms threshold appears, else keep structure.",
                "int step = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (step == 0) { digitalWrite(13, HIGH); delay(100); step = 1; }\n  else if (step == 1) { digitalWrite(13, LOW); delay(100); step = 2; }\n  else { digitalWrite(13, HIGH); delay(400); step = 0; }\n}",
                "State lab: Sequence steps."
            )
            {
                ConceptPoints = new[] { "Steps encode ordered actions.", "State machines structure multi-step behavior.", "millis supports non-blocking timing." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Timeout",
                "Timeout",
                "After onFor ms at HIGH, force LOW.",
                "unsigned long started = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n  digitalWrite(13, HIGH);\n  started = millis();\n}\nvoid loop()\n{\n  if (millis() - started >= 2000)\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "unsigned long started = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n  digitalWrite(13, HIGH);\n  started = millis();\n}\nvoid loop()\n{\n  if (millis() - started >= 2000)\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "unsigned long started = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n  digitalWrite(13, HIGH);\n  started = ___();\n}\nvoid loop()\n{\n  if (millis() - started >= 2000)\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "What does a state variable help with?",
                new[] { "USB speed", "Remembering values across loops", "Pin voltage only", "Compiler errors" },
                1,
                "State persists between loop iterations.",
                "unsigned long started = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n  digitalWrite(13, HIGH);\n  started = millis();\n}\nvoid loop()\n{\n  if (millis() - started >= 2000)\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Use == inside conditions.",
                "Rebuild this timing/state example.",
                "Change 500 to 250 where a 500 ms threshold appears, else keep structure.",
                "unsigned long started = 0;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n  digitalWrite(13, HIGH);\n  started = millis();\n}\nvoid loop()\n{\n  if (millis() - started >= 2000)\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "State lab: Timeout."
            )
            {
                ConceptPoints = new[] { "Timeouts end a timed on period.", "State machines structure multi-step behavior.", "millis supports non-blocking timing." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Latch",
                "Latch",
                "Once a button goes LOW, latch LED HIGH until reset logic clears it.",
                "bool latched = false;\nvoid setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW) { latched = true; }\n  digitalWrite(13, latched ? HIGH : LOW);\n}",
                "bool latched = false;\nvoid setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW) { latched = true; }\n  digitalWrite(13, latched ? HIGH : LOW);\n}",
                "___ latched = false;\nvoid setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW) { latched = true; }\n  digitalWrite(13, latched ? HIGH : LOW);\n}",
                "What does a state variable help with?",
                new[] { "USB speed", "Remembering values across loops", "Pin voltage only", "Compiler errors" },
                1,
                "State persists between loop iterations.",
                "bool latched = false;\nvoid setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) = LOW) { latched = true; }\n  digitalWrite(13, latched ? HIGH : LOW);\n}",
                "Use == inside conditions.",
                "Rebuild this timing/state example.",
                "Change 500 to 250 where a 500 ms threshold appears, else keep structure.",
                "bool latched = false;\nvoid setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW) { latched = true; }\n  digitalWrite(13, latched ? HIGH : LOW);\n}",
                "State lab: Latch."
            )
            {
                ConceptPoints = new[] { "Latches remember an event until cleared.", "State machines structure multi-step behavior.", "millis supports non-blocking timing." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "State lab",
                "State lab",
                "Combine a bool flag and blink while the flag is true.",
                "bool runBlink = true;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (runBlink)\n  {\n    digitalWrite(13, HIGH);\n    delay(150);\n    digitalWrite(13, LOW);\n    delay(150);\n  }\n}",
                "bool runBlink = true;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (runBlink)\n  {\n    digitalWrite(13, HIGH);\n    delay(150);\n    digitalWrite(13, LOW);\n    delay(150);\n  }\n}",
                "___ runBlink = true;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (runBlink)\n  {\n    digitalWrite(13, HIGH);\n    delay(150);\n    digitalWrite(13, LOW);\n    delay(150);\n  }\n}",
                "What does a state variable help with?",
                new[] { "USB speed", "Remembering values across loops", "Pin voltage only", "Compiler errors" },
                1,
                "State persists between loop iterations.",
                "bool runBlink = true;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (runBlink)\n  {\n    digitalWrite(13, HIGH);\n    delay(150);\n    digitalWrite(13, LOW);\n    delay(150);\n  }\n}",
                "Use == inside conditions.",
                "Rebuild this timing/state example.",
                "Change 500 to 250 where a 500 ms threshold appears, else keep structure.",
                "bool runBlink = true;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (runBlink)\n  {\n    digitalWrite(13, HIGH);\n    delay(150);\n    digitalWrite(13, LOW);\n    delay(150);\n  }\n}",
                "State lab: State lab."
            )
            {
                ConceptPoints = new[] { "Chapter 4 state tools are online.", "State machines structure multi-step behavior.", "millis supports non-blocking timing." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 5 · Analog and Projects",
                "analogRead",
                "analogRead",
                "analogRead(A0) returns 0-1023 on classic Uno-style boards.",
                "int sensorValue;\nvoid setup()\n{\n  Serial.begin(9600);\n}\nvoid loop()\n{\n  sensorValue = analogRead(A0);\n}",
                "int sensorValue;\nvoid setup()\n{\n  Serial.begin(9600);\n}\nvoid loop()\n{\n  sensorValue = analogRead(A0);\n}",
                "int sensorValue;\nvoid setup()\n{\n  Serial.begin(9600);\n}\nvoid loop()\n{\n  sensorValue = ___(A0);\n}",
                "analogRead on a classic Uno-style A0 returns roughly?",
                new[] { "0 or 1 only", "0 to 1023", "Only HIGH", "Milliseconds" },
                1,
                "Classic 10-bit ADC readings span 0-1023.",
                "int sensorValue;\nvoid setup()\n{\n  Serial.begin(9600);\n}\nvoid loop()\n{\n  sensorValue = analogRead(A0);\n}",
                "Check the comparison direction for your intent.",
                "Rebuild this analog/project example.",
                "Use trip threshold 500 if present, else keep structure.",
                "int sensorValue;\nvoid setup()\n{\n  Serial.begin(9500);\n}\nvoid loop()\n{\n  sensorValue = analogRead(A0);\n}",
                "Analog lab: analogRead."
            )
            {
                ConceptPoints = new[] { "Analog pins report a range, not only HIGH/LOW.", "Thresholds turn sensors into actions.", "Projects combine prior chapter skills." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 5 · Analog and Projects",
                "Map range",
                "Map range",
                "map(value, 0, 1023, 0, 255) scales a sensor into a smaller range.",
                "int raw;\nint scaled;\nvoid setup()\n{\n}\nvoid loop()\n{\n  raw = analogRead(A0);\n  scaled = map(raw, 0, 1023, 0, 255);\n}",
                "int raw;\nint scaled;\nvoid setup()\n{\n}\nvoid loop()\n{\n  raw = analogRead(A0);\n  scaled = map(raw, 0, 1023, 0, 255);\n}",
                "int raw;\nint scaled;\nvoid setup()\n{\n}\nvoid loop()\n{\n  raw = ___(A0);\n  scaled = map(raw, 0, 1023, 0, 255);\n}",
                "analogRead on a classic Uno-style A0 returns roughly?",
                new[] { "0 or 1 only", "0 to 1023", "Only HIGH", "Milliseconds" },
                1,
                "Classic 10-bit ADC readings span 0-1023.",
                "int raw;\nint scaled;\nvoid setup()\n{\n}\nvoid loop()\n{\n  raw = analogRead(A0);\n  scaled = map(raw, 0, 1023, 0, 255);\n}",
                "Check the comparison direction for your intent.",
                "Rebuild this analog/project example.",
                "Use trip threshold 500 if present, else keep structure.",
                "int raw;\nint scaled;\nvoid setup()\n{\n}\nvoid loop()\n{\n  raw = analogRead(A0);\n  scaled = map(raw, 0, 1023, 0, 255);\n}",
                "Analog lab: Map range."
            )
            {
                ConceptPoints = new[] { "map rescales one numeric range into another.", "Thresholds turn sensors into actions.", "Projects combine prior chapter skills." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 5 · Analog and Projects",
                "Threshold",
                "Threshold",
                "If analogRead(A0) > 500, turn LED on pin 13 HIGH.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) > 500)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) > 500)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (___(A0) > 500)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "analogRead on a classic Uno-style A0 returns roughly?",
                new[] { "0 or 1 only", "0 to 1023", "Only HIGH", "Milliseconds" },
                1,
                "Classic 10-bit ADC readings span 0-1023.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) < 500)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Check the comparison direction for your intent.",
                "Rebuild this analog/project example.",
                "Use trip threshold 500 if present, else keep structure.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) > 500)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Analog lab: Threshold."
            )
            {
                ConceptPoints = new[] { "Thresholds turn continuous sensors into decisions.", "Thresholds turn sensors into actions.", "Projects combine prior chapter skills." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 5 · Analog and Projects",
                "Sensor bar",
                "Sensor bar",
                "Store analogRead in a variable and use it for a threshold lamp.",
                "int level;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  level = analogRead(A0);\n  digitalWrite(13, level > 600 ? HIGH : LOW);\n}",
                "int level;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  level = analogRead(A0);\n  digitalWrite(13, level > 600 ? HIGH : LOW);\n}",
                "int level;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  level = ___(A0);\n  digitalWrite(13, level > 600 ? HIGH : LOW);\n}",
                "analogRead on a classic Uno-style A0 returns roughly?",
                new[] { "0 or 1 only", "0 to 1023", "Only HIGH", "Milliseconds" },
                1,
                "Classic 10-bit ADC readings span 0-1023.",
                "int level;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  level = analogRead(A0);\n  digitalWrite(13, level < 600 ? HIGH : LOW);\n}",
                "Check the comparison direction for your intent.",
                "Rebuild this analog/project example.",
                "Use trip threshold 500 if present, else keep structure.",
                "int level;\nvoid setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  level = analogRead(A0);\n  digitalWrite(13, level > 500 ? HIGH : LOW);\n}",
                "Analog lab: Sensor bar."
            )
            {
                ConceptPoints = new[] { "Named level values keep thresholds readable.", "Thresholds turn sensors into actions.", "Projects combine prior chapter skills." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 5 · Analog and Projects",
                "Night light",
                "Night light",
                "When it is dark (low reading), turn the LED on.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) < 400)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) < 400)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (___(A0) < 400)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "analogRead on a classic Uno-style A0 returns roughly?",
                new[] { "0 or 1 only", "0 to 1023", "Only HIGH", "Milliseconds" },
                1,
                "Classic 10-bit ADC readings span 0-1023.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) < 400)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Check the comparison direction for your intent.",
                "Rebuild this analog/project example.",
                "Use trip threshold 500 if present, else keep structure.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) < 400)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Analog lab: Night light."
            )
            {
                ConceptPoints = new[] { "Night lights invert the usual bright-equals-on idea.", "Thresholds turn sensors into actions.", "Projects combine prior chapter skills." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 5 · Analog and Projects",
                "Alarm pattern",
                "Alarm pattern",
                "If sensor is high, blink rapidly; else keep LED LOW.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) > 700)\n  {\n    digitalWrite(13, HIGH);\n    delay(100);\n    digitalWrite(13, LOW);\n    delay(100);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) > 700)\n  {\n    digitalWrite(13, HIGH);\n    delay(100);\n    digitalWrite(13, LOW);\n    delay(100);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (___(A0) > 700)\n  {\n    digitalWrite(13, HIGH);\n    delay(100);\n    digitalWrite(13, LOW);\n    delay(100);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "analogRead on a classic Uno-style A0 returns roughly?",
                new[] { "0 or 1 only", "0 to 1023", "Only HIGH", "Milliseconds" },
                1,
                "Classic 10-bit ADC readings span 0-1023.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) < 700)\n  {\n    digitalWrite(13, HIGH);\n    delay(100);\n    digitalWrite(13, LOW);\n    delay(100);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Check the comparison direction for your intent.",
                "Rebuild this analog/project example.",
                "Use trip threshold 500 if present, else keep structure.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) > 500)\n  {\n    digitalWrite(13, HIGH);\n    delay(100);\n    digitalWrite(13, LOW);\n    delay(100);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Analog lab: Alarm pattern."
            )
            {
                ConceptPoints = new[] { "Alarms combine thresholds with attention-getting blink.", "Thresholds turn sensors into actions.", "Projects combine prior chapter skills." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 5 · Analog and Projects",
                "Combined lab",
                "Combined lab",
                "Button on 2 (pull-up) forces LED on; else use light threshold on A0.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else if (analogRead(A0) < 400)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else if (analogRead(A0) < 400)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else if (___(A0) < 400)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "analogRead on a classic Uno-style A0 returns roughly?",
                new[] { "0 or 1 only", "0 to 1023", "Only HIGH", "Milliseconds" },
                1,
                "Classic 10-bit ADC readings span 0-1023.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else if (analogRead(A0) < 400)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Check the comparison direction for your intent.",
                "Rebuild this analog/project example.",
                "Use trip threshold 500 if present, else keep structure.",
                "void setup()\n{\n  pinMode(2, INPUT_PULLUP);\n  pinMode(13, OUTPUT);\n}\nvoid loop()\n{\n  if (digitalRead(2) == LOW)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else if (analogRead(A0) < 400)\n  {\n    digitalWrite(13, HIGH);\n  }\n  else\n  {\n    digitalWrite(13, LOW);\n  }\n}",
                "Analog lab: Combined lab."
            )
            {
                ConceptPoints = new[] { "Projects combine digital and analog reads.", "Thresholds turn sensors into actions.", "Projects combine prior chapter skills." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 5 · Analog and Projects",
                "Maker project",
                "Maker project",
                "Named pins, threshold on A0, LED on ledPin, blink when above trip.",
                "const int ledPin = 13;\nconst int trip = 650;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) > trip)\n  {\n    digitalWrite(ledPin, HIGH);\n    delay(120);\n    digitalWrite(ledPin, LOW);\n    delay(120);\n  }\n  else\n  {\n    digitalWrite(ledPin, LOW);\n  }\n}",
                "const int ledPin = 13;\nconst int trip = 650;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) > trip)\n  {\n    digitalWrite(ledPin, HIGH);\n    delay(120);\n    digitalWrite(ledPin, LOW);\n    delay(120);\n  }\n  else\n  {\n    digitalWrite(ledPin, LOW);\n  }\n}",
                "const int ledPin = 13;\nconst int trip = 650;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  if (___(A0) > trip)\n  {\n    digitalWrite(ledPin, HIGH);\n    delay(120);\n    digitalWrite(ledPin, LOW);\n    delay(120);\n  }\n  else\n  {\n    digitalWrite(ledPin, LOW);\n  }\n}",
                "analogRead on a classic Uno-style A0 returns roughly?",
                new[] { "0 or 1 only", "0 to 1023", "Only HIGH", "Milliseconds" },
                1,
                "Classic 10-bit ADC readings span 0-1023.",
                "const int ledPin = 13;\nconst int trip = 650;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) < trip)\n  {\n    digitalWrite(ledPin, HIGH);\n    delay(120);\n    digitalWrite(ledPin, LOW);\n    delay(120);\n  }\n  else\n  {\n    digitalWrite(ledPin, LOW);\n  }\n}",
                "Check the comparison direction for your intent.",
                "Rebuild this analog/project example.",
                "Use trip threshold 500 if present, else keep structure.",
                "const int ledPin = 13;\nconst int trip = 500;\nvoid setup()\n{\n  pinMode(ledPin, OUTPUT);\n}\nvoid loop()\n{\n  if (analogRead(A0) > trip)\n  {\n    digitalWrite(ledPin, HIGH);\n    delay(120);\n    digitalWrite(ledPin, LOW);\n    delay(120);\n  }\n  else\n  {\n    digitalWrite(ledPin, LOW);\n  }\n}",
                "Analog lab: Maker project."
            )
            {
                ConceptPoints = new[] { "Chapter 5 closes with a full mini project pattern.", "Thresholds turn sensors into actions.", "Projects combine prior chapter skills." },
                EditorFileNameOverride = "Sketch.ino"
            }
        };
}
