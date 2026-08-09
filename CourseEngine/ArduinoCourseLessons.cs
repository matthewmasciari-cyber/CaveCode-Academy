namespace CaveCode.CourseEngine;

public static class ArduinoCourseLessons
{
    public const int PlayableModuleCount = 8;

    public static IReadOnlyList<CourseLesson> All { get; } =
        new[]
        {
            new CourseLesson(
                "Chapter 1 · Sketch Foundations",
                "Sketch structure",
                "Power the Maker Lab",
                "An Arduino program is a sketch. setup() runs once at start. loop() runs over and over. That split is the heartbeat of every board program you will write in this lab.",
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
                ConceptPoints = new[] { "setup runs once at power-up.", "loop repeats continuously.", "Sketches are C++ programs with a fixed entry pattern." },
                EditorFileNameOverride = "Sketch.ino"
            },
            new CourseLesson(
                "Chapter 1 · Sketch Foundations",
                "pinMode output",
                "Claim a Digital Pin",
                "Before you drive an LED, tell the board the pin direction. pinMode(pin, OUTPUT) configures a pin as an output so digitalWrite can control it.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n}",
                "void setup()\n{\n  ___(13, OUTPUT);\n}\n\nvoid loop()\n{\n}",
                "Which call sets pin direction?",
                new[] { "digitalWrite", "pinMode", "delay", "analogRead" },
                1,
                "pinMode configures the pin. digitalWrite changes its level later.",
                "void setup()\n{\n  pinMode(13, output);\n}\n\nvoid loop()\n{\n}",
                "OUTPUT must be uppercase in standard Arduino style used here.",
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
                "digitalWrite(pin, HIGH) drives an output pin to the high level (often ~5V or 3.3V depending on the board). digitalWrite(pin, LOW) drives it low. For a typical LED on pin 13, HIGH means on.",
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
                "delay(ms) pauses the sketch for a number of milliseconds. It is simple timing for early labs. During delay, the board waits before running the next line.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(1000);\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(1000);\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  ___(1000);\n}",
                "delay(1000) waits about how long?",
                new[] { "1 millisecond", "1 second", "10 seconds", "13 cycles" },
                1,
                "1000 milliseconds is one second.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  Delay(1000);\n}",
                "Use delay in lowercase.",
                "Blink setup: pin 13 OUTPUT, loop drives HIGH then delay 1000.",
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
                "The classic blink sketch turns the LED on, waits, turns it off, waits, and repeats inside loop. That four-step rhythm is the first living pattern on the board.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(500);\n  digitalWrite(13, LOW);\n  delay(500);\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(500);\n  digitalWrite(13, LOW);\n  delay(500);\n}",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(500);\n  digitalWrite(13, ___);\n  delay(500);\n}",
                "In a blink loop, after HIGH and delay, the next level is usually?",
                new[] { "HIGH again", "LOW", "INPUT", "OUTPUT" },
                1,
                "Blink alternates HIGH and LOW with delays between.",
                "void setup()\n{\n  pinMode(13, OUTPUT);\n}\n\nvoid loop()\n{\n  digitalWrite(13, HIGH);\n  delay(500);\n  digitalWrite(13, LOW);\n  delay(500)\n}",
                "The last delay needs a semicolon.",
                "Build the classic blink: 500 ms on, 500 ms off on pin 13.",
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
                "Comments explain intent. // starts a single-line comment. The compiler ignores comments. Use them above setup blocks or beside pin numbers so the lab stays readable.",
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
                "A named constant makes sketches clearer. const int ledPin = 13; then use ledPin in pinMode and digitalWrite so you are not hunting magic numbers.",
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
                "Chapter 1 assembled the sketch spine: setup and loop, pinMode, digitalWrite, delay, comments, and named pins. Transfer asks for a small variation so the pattern sticks.",
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
            }
        };
}
