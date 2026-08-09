namespace CaveCode.CourseEngine;

public static class RaspiCourseLessons
{
    public const int PlayableModuleCount = 40;

    public static IReadOnlyList<CourseLesson> All { get; } =
        new[]
        {
            new CourseLesson(
                "Chapter 1 · Pi Script Foundations",
                "Script entry",
                "Boot the Pi Lab",
                "Raspberry Pi Python labs run as scripts. Import what you need, then run statements top to bottom. Later chapters add GPIO.",
                "print(\"Pi lab online\")",
                "print(\"Pi lab online\")",
                "print(\"___\")",
                "What does print do here?",
                new[] { "Configure a pin", "Show text output", "Read GPIO", "Sleep the CPU" },
                1,
                "print sends text to the console/serial monitor style output.",
                "Print(\"Pi lab online\")",
                "Use lowercase print in Python.",
                "Print the text Pi lab online.",
                "Print the text Maker lab ready.",
                "print(\"Maker lab ready\")",
                "Pi script shell online."
            )
            {
                ConceptPoints = new[] { "Scripts run top to bottom.", "print writes console output.", "Imports come before GPIO use." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 1 · Pi Script Foundations",
                "Import time",
                "Bring in time",
                "import time makes time.sleep available for pauses measured in seconds.",
                "import time\nprint(\"ready\")",
                "import time\nprint(\"ready\")",
                "import ___\nprint(\"ready\")",
                "Which import unlocks sleep timing?",
                new[] { "gpio", "time", "board", "led" },
                1,
                "The time module provides sleep.",
                "Import time\nprint(\"ready\")",
                "import is lowercase.",
                "Import time and print ready.",
                "Import time and print sleeping.",
                "import time\nprint(\"sleeping\")",
                "time module available."
            )
            {
                ConceptPoints = new[] { "import loads a library.", "time.sleep pauses in seconds.", "Import before use." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 1 · Pi Script Foundations",
                "sleep",
                "Pause with sleep",
                "time.sleep(1) pauses about one second. Pi labs use seconds, not milliseconds.",
                "import time\ntime.sleep(1)\nprint(\"done\")",
                "import time\ntime.sleep(1)\nprint(\"done\")",
                "import time\ntime.___(1)\nprint(\"done\")",
                "time.sleep(1) waits about?",
                new[] { "1 ms", "1 second", "1 minute", "17 loops" },
                1,
                "sleep argument is seconds on the Pi.",
                "import time\ntime.Sleep(1)",
                "sleep is lowercase.",
                "Import time, sleep 1, print done.",
                "Import time, sleep 0.5, print done.",
                "import time\ntime.sleep(0.5)\nprint(\"done\")",
                "Sleep timing online."
            )
            {
                ConceptPoints = new[] { "sleep uses seconds.", "Pauses block the script briefly.", "Combine with blink patterns later." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 1 · Pi Script Foundations",
                "Variables",
                "Name a pin number",
                "led_pin = 17 stores the BCM pin number you will drive later.",
                "led_pin = 17\nprint(led_pin)",
                "led_pin = 17\nprint(led_pin)",
                "led_pin = ___\nprint(led_pin)",
                "Why store 17 in led_pin?",
                new[] { "Faster Wi-Fi", "Clear pin naming", "Required by print", "Disables GPIO" },
                1,
                "Named pin variables document intent.",
                "led_pin == 17",
                "Assignment uses a single =.",
                "Set led_pin to 17 and print it.",
                "Set led_pin to 27 and print it.",
                "led_pin = 27\nprint(led_pin)",
                "Pin variable ready."
            )
            {
                ConceptPoints = new[] { "Names document pins.", "BCM numbers are common on Pi.", "Reuse the name later with GPIO." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 1 · Pi Script Foundations",
                "Comments",
                "Document the script",
                "# starts a comment in Python. The runtime ignores it.",
                "# Status LED on BCM 17\nled_pin = 17",
                "# Status LED on BCM 17\nled_pin = 17",
                "___ Status LED on BCM 17\nled_pin = 17",
                "Which marker starts a Python comment?",
                new[] { "//", "#", "/*", "rem" },
                1,
                "# begins a single-line Python comment.",
                "// Status LED\nled_pin = 17",
                "Python uses #, not //.",
                "Comment about BCM 17 then set led_pin = 17.",
                "Comment Status lamp then led_pin = 17.",
                "# Status lamp\nled_pin = 17",
                "Comments online."
            )
            {
                ConceptPoints = new[] { "# comments are ignored.", "Describe pin intent.", "Keep comments short." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 1 · Pi Script Foundations",
                "while True",
                "Repeat forever",
                "while True: repeats a block. Use sleep inside so the loop does not spin too hot.",
                "import time\nwhile True:\n    print(\"tick\")\n    time.sleep(1)",
                "import time\nwhile True:\n    print(\"tick\")\n    time.sleep(1)",
                "import time\nwhile ___:\n    print(\"tick\")\n    time.sleep(1)",
                "while True means?",
                new[] { "Run once", "Repeat until broken", "Only on boot", "GPIO only" },
                1,
                "True keeps the loop repeating until stopped.",
                "import time\nwhile true:",
                "True is capitalized in Python.",
                "Loop print tick every 1 second forever.",
                "Loop print beat every 0.5 seconds forever.",
                "import time\nwhile True:\n    print(\"beat\")\n    time.sleep(0.5)",
                "Forever loop online."
            )
            {
                ConceptPoints = new[] { "while True repeats.", "sleep paces the loop.", "This mirrors Arduino loop()." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 1 · Pi Script Foundations",
                "Indentation",
                "Respect the block",
                "Python uses indentation to mark blocks under while and if.",
                "import time\nwhile True:\n    print(\"ok\")\n    time.sleep(1)",
                "import time\nwhile True:\n    print(\"ok\")\n    time.sleep(1)",
                "import time\nwhile True:\n____print(\"ok\")\n    time.sleep(1)",
                "What marks a block under while True?",
                new[] { "Braces only", "Indentation", "Semicolons", "goto" },
                1,
                "Indentation defines the loop body.",
                "import time\nwhile True:\nprint(\"ok\")",
                "Indent the body under while.",
                "while True body prints ok and sleeps 1.",
                "while True body prints run and sleeps 1.",
                "import time\nwhile True:\n    print(\"run\")\n    time.sleep(1)",
                "Indentation check passed."
            )
            {
                ConceptPoints = new[] { "Indent loop bodies.", "Consistent spaces matter.", "Blocks end when indent returns." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 1 · Pi Script Foundations",
                "Chapter review",
                "Close Chapter 1",
                "Chapter 1: print, import time, sleep, variables, comments, while True, indentation.",
                "import time\nled_pin = 17\nwhile True:\n    print(led_pin)\n    time.sleep(1)",
                "import time\nled_pin = 17\nwhile True:\n    print(led_pin)\n    time.sleep(1)",
                "import time\nled_pin = 17\nwhile True:\n    print(led_pin)\n    time.___(1)",
                "Pi sleep units are?",
                new[] { "Milliseconds only", "Seconds", "Clock edges", "UART bits" },
                1,
                "time.sleep uses seconds.",
                "import time\nwhile True\n    print(1)",
                "while True needs a colon.",
                "Import time, led_pin 17, loop print and sleep 1.",
                "Same with sleep 0.25.",
                "import time\nled_pin = 17\nwhile True:\n    print(led_pin)\n    time.sleep(0.25)",
                "Chapter 1 complete. Pi script spine is ready for GPIO."
            )
            {
                ConceptPoints = new[] { "Chapter 1 is the script spine.", "while True is the repeat engine.", "Next: drive real pins." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "GPIO setup idea",
                "GPIO setup idea",
                "from gpiozero import LED then led = LED(17) claims BCM 17 as an LED.",
                "from gpiozero import LED\nled = LED(17)",
                "from gpiozero import LED\nled = LED(17)",
                "from gpiozero import ___\nled = LED(17)",
                "In gpiozero, LED(17) uses which pin numbering style commonly?",
                new[] { "Only physical header count", "BCM pin numbers", "Arduino pin 13 only", "I2C address" },
                1,
                "gpiozero LED() typically uses BCM numbers.",
                "from gpiozero import LED\nled = LED(17)",
                "Method names are lowercase.",
                "Rebuild this output example.",
                "Use BCM 27 instead of 17 when a single LED pin appears.",
                "from gpiozero import LED\nled = LED(27)",
                "Pi output: GPIO setup idea."
            )
            {
                ConceptPoints = new[] { "gpiozero wraps pin setup for common devices.", "gpiozero simplifies device pins.", "on/off/blink map to digital levels." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "LED on",
                "LED on",
                "led.on() drives the LED high/on.",
                "from gpiozero import LED\nled = LED(17)\nled.on()",
                "from gpiozero import LED\nled = LED(17)\nled.on()",
                "from gpiozero import ___\nled = LED(17)\nled.on()",
                "In gpiozero, LED(17) uses which pin numbering style commonly?",
                new[] { "Only physical header count", "BCM pin numbers", "Arduino pin 13 only", "I2C address" },
                1,
                "gpiozero LED() typically uses BCM numbers.",
                "from gpiozero import LED\nled = LED(17)\nled.On()",
                "Method names are lowercase.",
                "Rebuild this output example.",
                "Use BCM 27 instead of 17 when a single LED pin appears.",
                "from gpiozero import LED\nled = LED(27)\nled.on()",
                "Pi output: LED on."
            )
            {
                ConceptPoints = new[] { "on() is the digital HIGH equivalent for an LED device.", "gpiozero simplifies device pins.", "on/off/blink map to digital levels." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "LED off",
                "LED off",
                "led.off() turns the LED off.",
                "from gpiozero import LED\nled = LED(17)\nled.on()\nled.off()",
                "from gpiozero import LED\nled = LED(17)\nled.on()\nled.off()",
                "from gpiozero import ___\nled = LED(17)\nled.on()\nled.off()",
                "In gpiozero, LED(17) uses which pin numbering style commonly?",
                new[] { "Only physical header count", "BCM pin numbers", "Arduino pin 13 only", "I2C address" },
                1,
                "gpiozero LED() typically uses BCM numbers.",
                "from gpiozero import LED\nled = LED(17)\nled.On()\nled.off()",
                "Method names are lowercase.",
                "Rebuild this output example.",
                "Use BCM 27 instead of 17 when a single LED pin appears.",
                "from gpiozero import LED\nled = LED(27)\nled.on()\nled.off()",
                "Pi output: LED off."
            )
            {
                ConceptPoints = new[] { "off() clears the output.", "gpiozero simplifies device pins.", "on/off/blink map to digital levels." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Blink helper",
                "Blink helper",
                "led.blink(on_time=0.5, off_time=0.5) pulses the LED.",
                "from gpiozero import LED\nled = LED(17)\nled.blink(on_time=0.5, off_time=0.5)",
                "from gpiozero import LED\nled = LED(17)\nled.blink(on_time=0.5, off_time=0.5)",
                "from gpiozero import ___\nled = LED(17)\nled.blink(on_time=0.5, off_time=0.5)",
                "In gpiozero, LED(17) uses which pin numbering style commonly?",
                new[] { "Only physical header count", "BCM pin numbers", "Arduino pin 13 only", "I2C address" },
                1,
                "gpiozero LED() typically uses BCM numbers.",
                "from gpiozero import LED\nled = LED(17)\nled.blink(On_time=0.5, off_time=0.5)",
                "Method names are lowercase.",
                "Rebuild this output example.",
                "Use BCM 27 instead of 17 when a single LED pin appears.",
                "from gpiozero import LED\nled = LED(27)\nled.blink(on_time=0.5, off_time=0.5)",
                "Pi output: Blink helper."
            )
            {
                ConceptPoints = new[] { "blink encodes the classic pattern.", "gpiozero simplifies device pins.", "on/off/blink map to digital levels." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Manual blink",
                "Manual blink",
                "Loop: on, sleep, off, sleep.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.on()\n    sleep(0.5)\n    led.off()\n    sleep(0.5)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.on()\n    sleep(0.5)\n    led.off()\n    sleep(0.5)",
                "from gpiozero import ___\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.on()\n    sleep(0.5)\n    led.off()\n    sleep(0.5)",
                "In gpiozero, LED(17) uses which pin numbering style commonly?",
                new[] { "Only physical header count", "BCM pin numbers", "Arduino pin 13 only", "I2C address" },
                1,
                "gpiozero LED() typically uses BCM numbers.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.On()\n    sleep(0.5)\n    led.off()\n    sleep(0.5)",
                "Method names are lowercase.",
                "Rebuild this output example.",
                "Use BCM 27 instead of 17 when a single LED pin appears.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(27)\nwhile True:\n    led.on()\n    sleep(0.5)\n    led.off()\n    sleep(0.5)",
                "Pi output: Manual blink."
            )
            {
                ConceptPoints = new[] { "Manual blink shows the same idea as Arduino.", "gpiozero simplifies device pins.", "on/off/blink map to digital levels." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Two LEDs",
                "Two LEDs",
                "led_a on 17, led_b on 27; turn a on and b off.",
                "from gpiozero import LED\nled_a = LED(17)\nled_b = LED(27)\nled_a.on()\nled_b.off()",
                "from gpiozero import LED\nled_a = LED(17)\nled_b = LED(27)\nled_a.on()\nled_b.off()",
                "from gpiozero import ___\nled_a = LED(17)\nled_b = LED(27)\nled_a.on()\nled_b.off()",
                "In gpiozero, LED(17) uses which pin numbering style commonly?",
                new[] { "Only physical header count", "BCM pin numbers", "Arduino pin 13 only", "I2C address" },
                1,
                "gpiozero LED() typically uses BCM numbers.",
                "from gpiozero import LED\nled_a = LED(17)\nled_b = LED(27)\nled_a.On()\nled_b.off()",
                "Method names are lowercase.",
                "Rebuild this output example.",
                "Use BCM 27 instead of 17 when a single LED pin appears.",
                "from gpiozero import LED\nled_a = LED(27)\nled_b = LED(27)\nled_a.on()\nled_b.off()",
                "Pi output: Two LEDs."
            )
            {
                ConceptPoints = new[] { "Each LED object owns one pin.", "gpiozero simplifies device pins.", "on/off/blink map to digital levels." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Toggle",
                "Toggle",
                "led.toggle() flips the current state.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.toggle()\n    sleep(0.5)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.toggle()\n    sleep(0.5)",
                "from gpiozero import ___\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.toggle()\n    sleep(0.5)",
                "In gpiozero, LED(17) uses which pin numbering style commonly?",
                new[] { "Only physical header count", "BCM pin numbers", "Arduino pin 13 only", "I2C address" },
                1,
                "gpiozero LED() typically uses BCM numbers.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.toggle()\n    sleep(0.5)",
                "Method names are lowercase.",
                "Rebuild this output example.",
                "Use BCM 27 instead of 17 when a single LED pin appears.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(27)\nwhile True:\n    led.toggle()\n    sleep(0.5)",
                "Pi output: Toggle."
            )
            {
                ConceptPoints = new[] { "toggle alternates without tracking a variable.", "gpiozero simplifies device pins.", "on/off/blink map to digital levels." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 2 · Digital Output",
                "Output lab",
                "Output lab",
                "LED 17 manual blink at 0.2 s.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.on()\n    sleep(0.2)\n    led.off()\n    sleep(0.2)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.on()\n    sleep(0.2)\n    led.off()\n    sleep(0.2)",
                "from gpiozero import ___\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.on()\n    sleep(0.2)\n    led.off()\n    sleep(0.2)",
                "In gpiozero, LED(17) uses which pin numbering style commonly?",
                new[] { "Only physical header count", "BCM pin numbers", "Arduino pin 13 only", "I2C address" },
                1,
                "gpiozero LED() typically uses BCM numbers.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.On()\n    sleep(0.2)\n    led.off()\n    sleep(0.2)",
                "Method names are lowercase.",
                "Rebuild this output example.",
                "Use BCM 27 instead of 17 when a single LED pin appears.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(27)\nwhile True:\n    led.on()\n    sleep(0.2)\n    led.off()\n    sleep(0.2)",
                "Pi output: Output lab."
            )
            {
                ConceptPoints = new[] { "Chapter 2 output complete.", "gpiozero simplifies device pins.", "on/off/blink map to digital levels." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Button input",
                "Button input",
                "from gpiozero import Button then button = Button(2).",
                "from gpiozero import Button\nbutton = Button(2)",
                "from gpiozero import Button\nbutton = Button(2)",
                "from gpiozero import ___\nbutton = Button(2)",
                "button.is_pressed tells you?",
                new[] { "CPU temp", "Whether the button is held", "Wi-Fi RSSI", "PWM duty" },
                1,
                "is_pressed reflects the current switch level/state.",
                "from gpiozero import Button\nbutton = Button(2)",
                "Use is_pressed with underscore style.",
                "Rebuild this input example.",
                "Use LED pin 27 instead of 17 when present.",
                "from gpiozero import Button\nbutton = Button(2)",
                "Pi input: Button input."
            )
            {
                ConceptPoints = new[] { "Button wraps a GPIO input.", "Buttons are inputs.", "Drive LEDs from input decisions." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "is_pressed",
                "is_pressed",
                "button.is_pressed is True while the switch is held.",
                "from gpiozero import Button\nbutton = Button(2)\nprint(button.is_pressed)",
                "from gpiozero import Button\nbutton = Button(2)\nprint(button.is_pressed)",
                "from gpiozero import ___\nbutton = Button(2)\nprint(button.is_pressed)",
                "button.is_pressed tells you?",
                new[] { "CPU temp", "Whether the button is held", "Wi-Fi RSSI", "PWM duty" },
                1,
                "is_pressed reflects the current switch level/state.",
                "from gpiozero import Button\nbutton = Button(2)\nprint(button.isPressed)",
                "Use is_pressed with underscore style.",
                "Rebuild this input example.",
                "Use LED pin 27 instead of 17 when present.",
                "from gpiozero import Button\nbutton = Button(2)\nprint(button.is_pressed)",
                "Pi input: is_pressed."
            )
            {
                ConceptPoints = new[] { "is_pressed is a live level-style property.", "Buttons are inputs.", "Drive LEDs from input decisions." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Button to LED",
                "Button to LED",
                "While pressed, led.on(); else led.off().",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    if button.is_pressed:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    if button.is_pressed:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "from gpiozero import LED, ___\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    if button.is_pressed:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "button.is_pressed tells you?",
                new[] { "CPU temp", "Whether the button is held", "Wi-Fi RSSI", "PWM duty" },
                1,
                "is_pressed reflects the current switch level/state.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    if button.isPressed:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "Use is_pressed with underscore style.",
                "Rebuild this input example.",
                "Use LED pin 27 instead of 17 when present.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(27)\nbutton = Button(2)\nwhile True:\n    if button.is_pressed:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "Pi input: Button to LED."
            )
            {
                ConceptPoints = new[] { "Connect input decisions to outputs.", "Buttons are inputs.", "Drive LEDs from input decisions." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "wait_for_press",
                "wait_for_press",
                "button.wait_for_press() blocks until a press happens.",
                "from gpiozero import Button\nbutton = Button(2)\nbutton.wait_for_press()\nprint(\"pressed\")",
                "from gpiozero import Button\nbutton = Button(2)\nbutton.wait_for_press()\nprint(\"pressed\")",
                "from gpiozero import ___\nbutton = Button(2)\nbutton.wait_for_press()\nprint(\"pressed\")",
                "button.is_pressed tells you?",
                new[] { "CPU temp", "Whether the button is held", "Wi-Fi RSSI", "PWM duty" },
                1,
                "is_pressed reflects the current switch level/state.",
                "from gpiozero import Button\nbutton = Button(2)\nbutton.wait_for_press()\nprint(\"pressed\")",
                "Use is_pressed with underscore style.",
                "Rebuild this input example.",
                "Use LED pin 27 instead of 17 when present.",
                "from gpiozero import Button\nbutton = Button(2)\nbutton.wait_for_press()\nprint(\"pressed\")",
                "Pi input: wait_for_press."
            )
            {
                ConceptPoints = new[] { "wait helpers pause until an edge/condition.", "Buttons are inputs.", "Drive LEDs from input decisions." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "when_pressed",
                "when_pressed",
                "Assign a handler: button.when_pressed = led.on",
                "from gpiozero import LED, Button\nled = LED(17)\nbutton = Button(2)\nbutton.when_pressed = led.on",
                "from gpiozero import LED, Button\nled = LED(17)\nbutton = Button(2)\nbutton.when_pressed = led.on",
                "from gpiozero import LED, ___\nled = LED(17)\nbutton = Button(2)\nbutton.when_pressed = led.on",
                "button.is_pressed tells you?",
                new[] { "CPU temp", "Whether the button is held", "Wi-Fi RSSI", "PWM duty" },
                1,
                "is_pressed reflects the current switch level/state.",
                "from gpiozero import LED, Button\nled = LED(17)\nbutton = Button(2)\nbutton.when_pressed = led.on",
                "Use is_pressed with underscore style.",
                "Rebuild this input example.",
                "Use LED pin 27 instead of 17 when present.",
                "from gpiozero import LED, Button\nled = LED(27)\nbutton = Button(2)\nbutton.when_pressed = led.on",
                "Pi input: when_pressed."
            )
            {
                ConceptPoints = new[] { "Callbacks respond to events.", "Buttons are inputs.", "Drive LEDs from input decisions." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Pull-up default",
                "Pull-up default",
                "gpiozero Button uses a pull-up by default on many setups.",
                "from gpiozero import Button\nbutton = Button(2, pull_up=True)",
                "from gpiozero import Button\nbutton = Button(2, pull_up=True)",
                "from gpiozero import ___\nbutton = Button(2, pull_up=True)",
                "button.is_pressed tells you?",
                new[] { "CPU temp", "Whether the button is held", "Wi-Fi RSSI", "PWM duty" },
                1,
                "is_pressed reflects the current switch level/state.",
                "from gpiozero import Button\nbutton = Button(2, pull_up=True)",
                "Use is_pressed with underscore style.",
                "Rebuild this input example.",
                "Use LED pin 27 instead of 17 when present.",
                "from gpiozero import Button\nbutton = Button(2, pull_up=True)",
                "Pi input: Pull-up default."
            )
            {
                ConceptPoints = new[] { "Pull-ups stop floating inputs.", "Buttons are inputs.", "Drive LEDs from input decisions." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Hold idea",
                "Hold idea",
                "while button.is_pressed: keep LED on.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    led.value = 1 if button.is_pressed else 0\n    sleep(0.05)",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    led.value = 1 if button.is_pressed else 0\n    sleep(0.05)",
                "from gpiozero import LED, ___\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    led.value = 1 if button.is_pressed else 0\n    sleep(0.05)",
                "button.is_pressed tells you?",
                new[] { "CPU temp", "Whether the button is held", "Wi-Fi RSSI", "PWM duty" },
                1,
                "is_pressed reflects the current switch level/state.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    led.value = 1 if button.isPressed else 0\n    sleep(0.05)",
                "Use is_pressed with underscore style.",
                "Rebuild this input example.",
                "Use LED pin 27 instead of 17 when present.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(27)\nbutton = Button(2)\nwhile True:\n    led.value = 1 if button.is_pressed else 0\n    sleep(0.05)",
                "Pi input: Hold idea."
            )
            {
                ConceptPoints = new[] { "Hold tracking follows continuous press.", "Buttons are inputs.", "Drive LEDs from input decisions." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 3 · Digital Input",
                "Input lab",
                "Input lab",
                "Button 2 lights LED 17 while pressed.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    if button.is_pressed:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    if button.is_pressed:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "from gpiozero import LED, ___\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    if button.is_pressed:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "button.is_pressed tells you?",
                new[] { "CPU temp", "Whether the button is held", "Wi-Fi RSSI", "PWM duty" },
                1,
                "is_pressed reflects the current switch level/state.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nwhile True:\n    if button.isPressed:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "Use is_pressed with underscore style.",
                "Rebuild this input example.",
                "Use LED pin 27 instead of 17 when present.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(27)\nbutton = Button(2)\nwhile True:\n    if button.is_pressed:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "Pi input: Input lab."
            )
            {
                ConceptPoints = new[] { "Chapter 3 input complete.", "Buttons are inputs.", "Drive LEDs from input decisions." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "State variable",
                "State variable",
                "state = 0; flip between 0 and 1 each loop.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nstate = 0\nwhile True:\n    state = 1 - state\n    led.value = state\n    sleep(0.5)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nstate = 0\nwhile True:\n    state = 1 - state\n    led.value = state\n    sleep(0.5)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nstate = 0\nwhile ___:\n    state = 1 - state\n    led.value = state\n    sleep(0.5)",
                "A latch is best described as?",
                new[] { "Temporary noise", "Memory of an event until cleared", "Only PWM", "A pull-up resistor" },
                1,
                "Latches store that something happened until reset.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nstate = 0\nwhile true:\n    state = 1 - state\n    led.value = state\n    sleep(0.5)",
                "Python uses True with capital T.",
                "Rebuild this state example.",
                "Use sleep 0.1 where a single sleep pacing exists if obvious, else keep structure.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nstate = 0\nwhile True:\n    state = 1 - state\n    led.value = state\n    sleep(0.1)",
                "Pi state: State variable."
            )
            {
                ConceptPoints = new[] { "State variables remember the last mode.", "State organizes multi-step logic.", "Flags and latches are core patterns." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Flag gate",
                "Flag gate",
                "active = True gates whether blinking runs.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nactive = True\nwhile True:\n    if active:\n        led.on()\n        sleep(0.2)\n        led.off()\n        sleep(0.2)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nactive = True\nwhile True:\n    if active:\n        led.on()\n        sleep(0.2)\n        led.off()\n        sleep(0.2)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nactive = True\nwhile ___:\n    if active:\n        led.on()\n        sleep(0.2)\n        led.off()\n        sleep(0.2)",
                "A latch is best described as?",
                new[] { "Temporary noise", "Memory of an event until cleared", "Only PWM", "A pull-up resistor" },
                1,
                "Latches store that something happened until reset.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nactive = true\nwhile true:\n    if active:\n        led.on()\n        sleep(0.2)\n        led.off()\n        sleep(0.2)",
                "Python uses True with capital T.",
                "Rebuild this state example.",
                "Use sleep 0.1 where a single sleep pacing exists if obvious, else keep structure.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nactive = True\nwhile True:\n    if active:\n        led.on()\n        sleep(0.2)\n        led.off()\n        sleep(0.2)",
                "Pi state: Flag gate."
            )
            {
                ConceptPoints = new[] { "Flags enable or disable whole behaviors.", "State organizes multi-step logic.", "Flags and latches are core patterns." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Phase machine",
                "Phase machine",
                "phase 0 turns on; phase 1 turns off.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nphase = 0\nwhile True:\n    if phase == 0:\n        led.on()\n        phase = 1\n    else:\n        led.off()\n        phase = 0\n    sleep(0.3)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nphase = 0\nwhile True:\n    if phase == 0:\n        led.on()\n        phase = 1\n    else:\n        led.off()\n        phase = 0\n    sleep(0.3)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nphase = 0\nwhile ___:\n    if phase == 0:\n        led.on()\n        phase = 1\n    else:\n        led.off()\n        phase = 0\n    sleep(0.3)",
                "A latch is best described as?",
                new[] { "Temporary noise", "Memory of an event until cleared", "Only PWM", "A pull-up resistor" },
                1,
                "Latches store that something happened until reset.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nphase = 0\nwhile true:\n    if phase == 0:\n        led.on()\n        phase = 1\n    else:\n        led.off()\n        phase = 0\n    sleep(0.3)",
                "Python uses True with capital T.",
                "Rebuild this state example.",
                "Use sleep 0.1 where a single sleep pacing exists if obvious, else keep structure.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nphase = 0\nwhile True:\n    if phase == 0:\n        led.on()\n        phase = 1\n    else:\n        led.off()\n        phase = 0\n    sleep(0.3)",
                "Pi state: Phase machine."
            )
            {
                ConceptPoints = new[] { "Phases order multi-step patterns.", "State organizes multi-step logic.", "Flags and latches are core patterns." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Timeout idea",
                "Timeout idea",
                "Record start with time.time() and stop after 2 seconds.",
                "from gpiozero import LED\nimport time\nled = LED(17)\nled.on()\nstart = time.time()\nwhile time.time() - start < 2:\n    pass\nled.off()",
                "from gpiozero import LED\nimport time\nled = LED(17)\nled.on()\nstart = time.time()\nwhile time.time() - start < 2:\n    pass\nled.off()",
                "from gpiozero import LED\nimport time\nled = LED(17)\nled.on()\nstart = time.time()\nwhile time.time() - start < 2:\n    pass\nled.off()",
                "A latch is best described as?",
                new[] { "Temporary noise", "Memory of an event until cleared", "Only PWM", "A pull-up resistor" },
                1,
                "Latches store that something happened until reset.",
                "from gpiozero import LED\nimport time\nled = LED(17)\nled.on()\nstart = time.time()\nwhile time.time() - start < 2:\n    pass\nled.off()",
                "Python uses True with capital T.",
                "Rebuild this state example.",
                "Use sleep 0.1 where a single sleep pacing exists if obvious, else keep structure.",
                "from gpiozero import LED\nimport time\nled = LED(17)\nled.on()\nstart = time.time()\nwhile time.time() - start < 2:\n    pass\nled.off()",
                "Pi state: Timeout idea."
            )
            {
                ConceptPoints = new[] { "Timeouts end a timed activity.", "State organizes multi-step logic.", "Flags and latches are core patterns." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Counter steps",
                "Counter steps",
                "count increases each loop; blink pattern changes at thresholds.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\ncount = 0\nwhile True:\n    count = count + 1\n    led.value = 1 if count % 2 == 0 else 0\n    sleep(0.25)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\ncount = 0\nwhile True:\n    count = count + 1\n    led.value = 1 if count % 2 == 0 else 0\n    sleep(0.25)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\ncount = 0\nwhile ___:\n    count = count + 1\n    led.value = 1 if count % 2 == 0 else 0\n    sleep(0.25)",
                "A latch is best described as?",
                new[] { "Temporary noise", "Memory of an event until cleared", "Only PWM", "A pull-up resistor" },
                1,
                "Latches store that something happened until reset.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\ncount = 0\nwhile true:\n    count = count + 1\n    led.value = 1 if count % 2 == 0 else 0\n    sleep(0.25)",
                "Python uses True with capital T.",
                "Rebuild this state example.",
                "Use sleep 0.1 where a single sleep pacing exists if obvious, else keep structure.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\ncount = 0\nwhile True:\n    count = count + 1\n    led.value = 1 if count % 2 == 0 else 0\n    sleep(0.25)",
                "Pi state: Counter steps."
            )
            {
                ConceptPoints = new[] { "Counters drive sequenced behavior.", "State organizes multi-step logic.", "Flags and latches are core patterns." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Latch",
                "Latch",
                "Once pressed, latched stays True and LED stays on.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nlatched = False\nwhile True:\n    if button.is_pressed:\n        latched = True\n    led.value = 1 if latched else 0\n    sleep(0.05)",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nlatched = False\nwhile True:\n    if button.is_pressed:\n        latched = True\n    led.value = 1 if latched else 0\n    sleep(0.05)",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nlatched = False\nwhile ___:\n    if button.is_pressed:\n        latched = True\n    led.value = 1 if latched else 0\n    sleep(0.05)",
                "A latch is best described as?",
                new[] { "Temporary noise", "Memory of an event until cleared", "Only PWM", "A pull-up resistor" },
                1,
                "Latches store that something happened until reset.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nlatched = False\nwhile true:\n    if button.is_pressed:\n        latched = true\n    led.value = 1 if latched else 0\n    sleep(0.05)",
                "Python uses True with capital T.",
                "Rebuild this state example.",
                "Use sleep 0.1 where a single sleep pacing exists if obvious, else keep structure.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nlatched = False\nwhile True:\n    if button.is_pressed:\n        latched = True\n    led.value = 1 if latched else 0\n    sleep(0.05)",
                "Pi state: Latch."
            )
            {
                ConceptPoints = new[] { "Latches remember events.", "State organizes multi-step logic.", "Flags and latches are core patterns." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "Duty pattern",
                "Duty pattern",
                "Longer on than off for a different blink feel.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.on()\n    sleep(0.8)\n    led.off()\n    sleep(0.2)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.on()\n    sleep(0.8)\n    led.off()\n    sleep(0.2)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile ___:\n    led.on()\n    sleep(0.8)\n    led.off()\n    sleep(0.2)",
                "A latch is best described as?",
                new[] { "Temporary noise", "Memory of an event until cleared", "Only PWM", "A pull-up resistor" },
                1,
                "Latches store that something happened until reset.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile true:\n    led.on()\n    sleep(0.8)\n    led.off()\n    sleep(0.2)",
                "Python uses True with capital T.",
                "Rebuild this state example.",
                "Use sleep 0.1 where a single sleep pacing exists if obvious, else keep structure.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    led.on()\n    sleep(0.8)\n    led.off()\n    sleep(0.2)",
                "Pi state: Duty pattern."
            )
            {
                ConceptPoints = new[] { "Duty is the on fraction of the cycle.", "State organizes multi-step logic.", "Flags and latches are core patterns." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 4 · Timing and State",
                "State lab",
                "State lab",
                "Toggle LED each 0.4 s using a state variable.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nstate = 0\nwhile True:\n    state = 1 - state\n    led.value = state\n    sleep(0.4)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nstate = 0\nwhile True:\n    state = 1 - state\n    led.value = state\n    sleep(0.4)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nstate = 0\nwhile ___:\n    state = 1 - state\n    led.value = state\n    sleep(0.4)",
                "A latch is best described as?",
                new[] { "Temporary noise", "Memory of an event until cleared", "Only PWM", "A pull-up resistor" },
                1,
                "Latches store that something happened until reset.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nstate = 0\nwhile true:\n    state = 1 - state\n    led.value = state\n    sleep(0.4)",
                "Python uses True with capital T.",
                "Rebuild this state example.",
                "Use sleep 0.1 where a single sleep pacing exists if obvious, else keep structure.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nstate = 0\nwhile True:\n    state = 1 - state\n    led.value = state\n    sleep(0.1)",
                "Pi state: State lab."
            )
            {
                ConceptPoints = new[] { "Chapter 4 state tools online.", "State organizes multi-step logic.", "Flags and latches are core patterns." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 5 · Sensors and Projects",
                "Read a value",
                "Read a value",
                "sensor_value = 512  # stand-in for an analog/MCP reading in this lab",
                "sensor_value = 512\nprint(sensor_value)",
                "sensor_value = 512\nprint(sensor_value)",
                "sensor_value = 512\n___(sensor_value)",
                "A threshold in these labs is used to?",
                new[] { "Format disks", "Turn a numeric reading into an action", "Set Wi-Fi SSID", "Compile C++" },
                1,
                "Thresholds compare readings to a trip point.",
                "sensor_value = 512\nprint(sensor_value)",
                "Check comparison direction.",
                "Rebuild this sensor/project example.",
                "Use TRIP 500 when TRIP appears.",
                "sensor_value = 512\nprint(sensor_value)",
                "Pi project: Read a value."
            )
            {
                ConceptPoints = new[] { "Labs can start with variables before real ADC hardware.", "Combine sensors, buttons, and LEDs.", "Constants document trip points." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 5 · Sensors and Projects",
                "Threshold",
                "Threshold",
                "If sensor_value > 500: led.on() else led.off().",
                "from gpiozero import LED\nled = LED(17)\nsensor_value = 640\nif sensor_value > 500:\n    led.on()\nelse:\n    led.off()",
                "from gpiozero import LED\nled = LED(17)\nsensor_value = 640\nif sensor_value > 500:\n    led.on()\nelse:\n    led.off()",
                "from gpiozero import LED\nled = LED(17)\nsensor_value = 640\nif sensor_value > 500:\n    led.___()\nelse:\n    led.off()",
                "A threshold in these labs is used to?",
                new[] { "Format disks", "Turn a numeric reading into an action", "Set Wi-Fi SSID", "Compile C++" },
                1,
                "Thresholds compare readings to a trip point.",
                "from gpiozero import LED\nled = LED(17)\nsensor_value = 640\nif sensor_value < 500:\n    led.on()\nelse:\n    led.off()",
                "Check comparison direction.",
                "Rebuild this sensor/project example.",
                "Use TRIP 500 when TRIP appears.",
                "from gpiozero import LED\nled = LED(17)\nsensor_value = 640\nif sensor_value > 500:\n    led.on()\nelse:\n    led.off()",
                "Pi project: Threshold."
            )
            {
                ConceptPoints = new[] { "Thresholds turn numbers into actions.", "Combine sensors, buttons, and LEDs.", "Constants document trip points." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 5 · Sensors and Projects",
                "Loop threshold",
                "Loop threshold",
                "Each loop, pretend a reading and drive the LED.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    sensor_value = 640\n    if sensor_value > 500:\n        led.on()\n    else:\n        led.off()\n    sleep(0.2)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    sensor_value = 640\n    if sensor_value > 500:\n        led.on()\n    else:\n        led.off()\n    sleep(0.2)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    sensor_value = 640\n    if sensor_value > 500:\n        led.___()\n    else:\n        led.off()\n    sleep(0.2)",
                "A threshold in these labs is used to?",
                new[] { "Format disks", "Turn a numeric reading into an action", "Set Wi-Fi SSID", "Compile C++" },
                1,
                "Thresholds compare readings to a trip point.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    sensor_value = 640\n    if sensor_value < 500:\n        led.on()\n    else:\n        led.off()\n    sleep(0.2)",
                "Check comparison direction.",
                "Rebuild this sensor/project example.",
                "Use TRIP 500 when TRIP appears.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nwhile True:\n    sensor_value = 640\n    if sensor_value > 500:\n        led.on()\n    else:\n        led.off()\n    sleep(0.2)",
                "Pi project: Loop threshold."
            )
            {
                ConceptPoints = new[] { "Real sensors would replace the constant.", "Combine sensors, buttons, and LEDs.", "Constants document trip points." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 5 · Sensors and Projects",
                "Night light logic",
                "Night light logic",
                "Low reading means dark → LED on.",
                "from gpiozero import LED\nled = LED(17)\nreading = 300\nif reading < 400:\n    led.on()\nelse:\n    led.off()",
                "from gpiozero import LED\nled = LED(17)\nreading = 300\nif reading < 400:\n    led.on()\nelse:\n    led.off()",
                "from gpiozero import LED\nled = LED(17)\nreading = 300\nif reading < 400:\n    led.___()\nelse:\n    led.off()",
                "A threshold in these labs is used to?",
                new[] { "Format disks", "Turn a numeric reading into an action", "Set Wi-Fi SSID", "Compile C++" },
                1,
                "Thresholds compare readings to a trip point.",
                "from gpiozero import LED\nled = LED(17)\nreading = 300\nif reading < 400:\n    led.on()\nelse:\n    led.off()",
                "Check comparison direction.",
                "Rebuild this sensor/project example.",
                "Use TRIP 500 when TRIP appears.",
                "from gpiozero import LED\nled = LED(17)\nreading = 300\nif reading < 400:\n    led.on()\nelse:\n    led.off()",
                "Pi project: Night light logic."
            )
            {
                ConceptPoints = new[] { "Night lights invert bright-equals-on.", "Combine sensors, buttons, and LEDs.", "Constants document trip points." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 5 · Sensors and Projects",
                "Alarm blink",
                "Alarm blink",
                "If reading high, blink fast; else off.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nreading = 800\nwhile True:\n    if reading > 700:\n        led.on()\n        sleep(0.1)\n        led.off()\n        sleep(0.1)\n    else:\n        led.off()\n        sleep(0.2)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nreading = 800\nwhile True:\n    if reading > 700:\n        led.on()\n        sleep(0.1)\n        led.off()\n        sleep(0.1)\n    else:\n        led.off()\n        sleep(0.2)",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nreading = 800\nwhile True:\n    if reading > 700:\n        led.___()\n        sleep(0.1)\n        led.off()\n        sleep(0.1)\n    else:\n        led.off()\n        sleep(0.2)",
                "A threshold in these labs is used to?",
                new[] { "Format disks", "Turn a numeric reading into an action", "Set Wi-Fi SSID", "Compile C++" },
                1,
                "Thresholds compare readings to a trip point.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nreading = 800\nwhile True:\n    if reading < 700:\n        led.on()\n        sleep(0.1)\n        led.off()\n        sleep(0.1)\n    else:\n        led.off()\n        sleep(0.2)",
                "Check comparison direction.",
                "Rebuild this sensor/project example.",
                "Use TRIP 500 when TRIP appears.",
                "from gpiozero import LED\nfrom time import sleep\nled = LED(17)\nreading = 800\nwhile True:\n    if reading > 500:\n        led.on()\n        sleep(0.1)\n        led.off()\n        sleep(0.1)\n    else:\n        led.off()\n        sleep(0.2)",
                "Pi project: Alarm blink."
            )
            {
                ConceptPoints = new[] { "Alarms add urgency with fast blink.", "Combine sensors, buttons, and LEDs.", "Constants document trip points." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 5 · Sensors and Projects",
                "Button override",
                "Button override",
                "Button press forces LED on; else use threshold.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nreading = 300\nwhile True:\n    if button.is_pressed:\n        led.on()\n    elif reading < 400:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nreading = 300\nwhile True:\n    if button.is_pressed:\n        led.on()\n    elif reading < 400:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nreading = 300\nwhile True:\n    if button.is_pressed:\n        led.___()\n    elif reading < 400:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "A threshold in these labs is used to?",
                new[] { "Format disks", "Turn a numeric reading into an action", "Set Wi-Fi SSID", "Compile C++" },
                1,
                "Thresholds compare readings to a trip point.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nreading = 300\nwhile True:\n    if button.is_pressed:\n        led.on()\n    elif reading < 400:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "Check comparison direction.",
                "Rebuild this sensor/project example.",
                "Use TRIP 500 when TRIP appears.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nreading = 300\nwhile True:\n    if button.is_pressed:\n        led.on()\n    elif reading < 400:\n        led.on()\n    else:\n        led.off()\n    sleep(0.05)",
                "Pi project: Button override."
            )
            {
                ConceptPoints = new[] { "Projects combine inputs.", "Combine sensors, buttons, and LEDs.", "Constants document trip points." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 5 · Sensors and Projects",
                "Named constants",
                "Named constants",
                "TRIP = 600 documents the alarm threshold.",
                "from gpiozero import LED\nled = LED(17)\nTRIP = 600\nreading = 650\nif reading > TRIP:\n    led.on()\nelse:\n    led.off()",
                "from gpiozero import LED\nled = LED(17)\nTRIP = 600\nreading = 650\nif reading > TRIP:\n    led.on()\nelse:\n    led.off()",
                "from gpiozero import LED\nled = LED(17)\nTRIP = 600\nreading = 650\nif reading > TRIP:\n    led.___()\nelse:\n    led.off()",
                "A threshold in these labs is used to?",
                new[] { "Format disks", "Turn a numeric reading into an action", "Set Wi-Fi SSID", "Compile C++" },
                1,
                "Thresholds compare readings to a trip point.",
                "from gpiozero import LED\nled = LED(17)\nTRIP = 600\nreading = 650\nif reading < TRIP:\n    led.on()\nelse:\n    led.off()",
                "Check comparison direction.",
                "Rebuild this sensor/project example.",
                "Use TRIP 500 when TRIP appears.",
                "from gpiozero import LED\nled = LED(17)\nTRIP = 500\nreading = 650\nif reading > TRIP:\n    led.on()\nelse:\n    led.off()",
                "Pi project: Named constants."
            )
            {
                ConceptPoints = new[] { "Named thresholds read clearly.", "Combine sensors, buttons, and LEDs.", "Constants document trip points." },
                EditorFileNameOverride = "gpio_lab.py"
            },
            new CourseLesson(
                "Chapter 5 · Sensors and Projects",
                "Maker project",
                "Maker project",
                "LED 17, button 2, TRIP 600, force on when pressed else threshold blink.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nTRIP = 600\nreading = 650\nwhile True:\n    if button.is_pressed:\n        led.on()\n    elif reading > TRIP:\n        led.on()\n        sleep(0.12)\n        led.off()\n        sleep(0.12)\n    else:\n        led.off()\n        sleep(0.1)",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nTRIP = 600\nreading = 650\nwhile True:\n    if button.is_pressed:\n        led.on()\n    elif reading > TRIP:\n        led.on()\n        sleep(0.12)\n        led.off()\n        sleep(0.12)\n    else:\n        led.off()\n        sleep(0.1)",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nTRIP = 600\nreading = 650\nwhile True:\n    if button.is_pressed:\n        led.___()\n    elif reading > TRIP:\n        led.on()\n        sleep(0.12)\n        led.off()\n        sleep(0.12)\n    else:\n        led.off()\n        sleep(0.1)",
                "A threshold in these labs is used to?",
                new[] { "Format disks", "Turn a numeric reading into an action", "Set Wi-Fi SSID", "Compile C++" },
                1,
                "Thresholds compare readings to a trip point.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nTRIP = 600\nreading = 650\nwhile True:\n    if button.is_pressed:\n        led.on()\n    elif reading < TRIP:\n        led.on()\n        sleep(0.12)\n        led.off()\n        sleep(0.12)\n    else:\n        led.off()\n        sleep(0.1)",
                "Check comparison direction.",
                "Rebuild this sensor/project example.",
                "Use TRIP 500 when TRIP appears.",
                "from gpiozero import LED, Button\nfrom time import sleep\nled = LED(17)\nbutton = Button(2)\nTRIP = 500\nreading = 650\nwhile True:\n    if button.is_pressed:\n        led.on()\n    elif reading > TRIP:\n        led.on()\n        sleep(0.12)\n        led.off()\n        sleep(0.12)\n    else:\n        led.off()\n        sleep(0.1)",
                "Pi project: Maker project."
            )
            {
                ConceptPoints = new[] { "Chapter 5 mini project complete.", "Combine sensors, buttons, and LEDs.", "Constants document trip points." },
                EditorFileNameOverride = "gpio_lab.py"
            }
        };
}
