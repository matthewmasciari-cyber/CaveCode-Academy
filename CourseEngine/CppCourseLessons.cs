namespace CaveCode.CourseEngine;

/// <summary>
/// Full C++ Engine Foundry curriculum: 5 chapters × 8 modules = 40 lessons.
/// </summary>
public static class CppCourseLessons
{
    public const int PlayableModuleCount = 40;
    public const int ChapterCount = 5;
    public const int ModulesPerChapter = 8;

    public static IReadOnlyList<CourseLesson> All { get; } =
        new[]
        {
            new CourseLesson(
                "Chapter 1 · Foundry Foundations",
                "Program structure",
                "Boot the Engine Foundry",
                "Every C++ executable begins in the main function. The #include line makes a library available, braces group the function body, semicolons finish statements, and return 0 reports that the program ended successfully.",
                "#include <iostream>\n\nint main()\n{\n    return 0;\n}",
                "#include <iostream>\n\nint main()\n{\n    return 0;\n}",
                "#include <___>\n\nint ___()\n{\n    return ___;\n}",
                "Which function is the starting point of a normal C++ program?",
                new[] { "iostream", "start", "return", "main" },
                3,
                "The operating system begins the program by calling main.",
                "include <iostream>\n\nint main()\n{\n    return 0\n}",
                "The library directive needs # at the beginning, and the return statement needs a semicolon.",
                "Rebuild the minimal Engine Foundry program. Use the exact library iostream, the exact function name main, and return the exact value 0.",
                "Create another minimal program using the string library. Use the exact function name main and return the exact value 0.",
                "#include <string>\n\nint main()\n{\n    return 0;\n}",
                "The Forge Core startup sequence is now online."
            )
            {
                ConceptPoints = new[] { "main is the program entry point.", "#include makes a standard library available.", "Braces group code and semicolons end statements." },
                EditorFileNameOverride = "EngineTraining.cpp"
            },
            new CourseLesson(
                "Chapter 1 · Foundry Foundations",
                "Console output",
                "Send a Foundry Status Message",
                "std::cout sends information to the console. The << operator pushes each value into the output stream, quotation marks create text, and \\n moves the terminal to a new line.",
                "std::cout << \"Cooling ready\\n\";",
                "std::cout << \"Foundry online\\n\";",
                "std::___ << \"___\\n\";",
                "What does the << operator do with std::cout?",
                new[] { "Starts the program", "Compares two values", "Sends a value into the output stream", "Reads keyboard input" },
                2,
                "With std::cout, << inserts the value on its right into the console output stream.",
                "std::cout < \"Foundry online\\n\"",
                "Console output needs two less-than symbols and the statement must end with a semicolon.",
                "Use the exact identifiers std and cout as std::cout. Print the exact text Foundry online\\n.",
                "Use the exact identifiers std and cout as std::cout. Print the exact text Cooling system ready\\n.",
                "std::cout << \"Cooling system ready\\n\";",
                "The diagnostic message board can now report foundry status."
            )
            {
                ConceptPoints = new[] { "std:: identifies the standard namespace.", "cout means character output.", "\n begins a new console line." },
                EditorFileNameOverride = "EngineTraining.cpp"
            },
            new CourseLesson(
                "Chapter 1 · Foundry Foundations",
                "Variables",
                "Store the Engine Temperature",
                "A variable is named storage. The declaration states the type and name, the equals sign assigns a starting value, and the semicolon ends the statement.",
                "int coolantLevel = 90;",
                "int engineTemperature = 72;",
                "___ engineTemperature = ___;",
                "Why is int appropriate for the value 72?",
                new[] { "72 is text", "int prints automatically", "72 is true or false", "72 is a whole number" },
                3,
                "int stores whole numbers without a decimal portion.",
                "int engineTemperature == 72;",
                "A declaration assigns its value with one equals sign, not the equality comparison operator.",
                "Create engineTemperature with the starting value 72.",
                "Create targetRpm with the whole-number value 1200.",
                "int targetRpm = 1200;",
                "The thermal sensor now supplies a live temperature value."
            )
            {
                ConceptPoints = new[] { "int stores whole numbers.", "The variable name explains what the value represents.", "One equals sign assigns a value." },
                EditorFileNameOverride = "EngineState.cpp"
            },
            new CourseLesson(
                "Chapter 1 · Foundry Foundations",
                "Numeric types",
                "Choose the Right Engine Data Types",
                "Different values need different types. int stores whole numbers, double stores decimals, and bool stores true or false states.",
                "int count = 3;\ndouble rate = 1.5;\nbool ready = false;",
                "int engineRpm = 1200;\ndouble coolantTemperature = 72.5;\nbool engineOnline = true;",
                "___ engineRpm = ___;\n___ coolantTemperature = ___;\n___ engineOnline = ___;",
                "Which type should store whether the engine is online?",
                new[] { "double", "int", "bool", "std::string" },
                2,
                "bool is the correct type for yes-or-no states such as online or offline.",
                "double engineRpm = \"1200\";\nint coolantTemperature = 72.5;\nbool engineOnline = \"true\";",
                "Keep numeric types numeric and Boolean values unquoted as true or false.",
                "Declare engineRpm as 1200, coolantTemperature as 72.5, and engineOnline as true.",
                "Declare bladeCount as 6, oilPressure as 31.2, and pumpActive as false.",
                "int bladeCount = 6;\ndouble oilPressure = 31.2;\nbool pumpActive = false;",
                "RPM, temperature, and power-state instrumentation are connected."
            )
            {
                ConceptPoints = new[] { "Match each value to the correct type.", "true and false are Boolean literals.", "Decimals need double." },
                EditorFileNameOverride = "EngineState.cpp"
            },
            new CourseLesson(
                "Chapter 1 · Foundry Foundations",
                "Text",
                "Label the Foundry Systems",
                "std::string stores text. Include the string header when you work with string objects, and put character sequences inside quotation marks.",
                "std::string label = \"Pump A\";",
                "std::string systemName = \"Forge Core\";",
                "std::___ systemName = \"___\";",
                "Why does Forge Core need quotation marks?",
                new[] { "It is a variable name", "It is a Boolean", "It is a comment", "It is a text value" },
                3,
                "Quotation marks create a string literal for human-readable text.",
                "string systemName = Forge Core;",
                "Use std::string and place the text inside quotation marks.",
                "Create systemName with the text Forge Core.",
                "Create zoneLabel with the text Cooling Bay.",
                "std::string zoneLabel = \"Cooling Bay\";",
                "The foundry systems now have readable labels."
            )
            {
                ConceptPoints = new[] { "std::string stores text.", "String literals use double quotes.", "Names without quotes are identifiers." },
                EditorFileNameOverride = "EngineState.cpp"
            },
            new CourseLesson(
                "Chapter 1 · Foundry Foundations",
                "Input",
                "Read an Operator Command",
                "std::cin reads values from the keyboard. The >> operator extracts the next input value and stores it in a variable.",
                "int mode = 0;\nstd::cin >> mode;",
                "int targetRpm = 0;\nstd::cin >> targetRpm;",
                "int targetRpm = ___;\nstd::___ >> targetRpm;",
                "Where is the operator input stored?",
                new[] { "Inside std", "It is not stored", "Inside the >> operator", "In targetRpm" },
                3,
                "The >> operator writes the typed value into the variable on its right.",
                "int targetRpm = 0;\nstd::cin << targetRpm;",
                "Input uses >> with std::cin, not <<.",
                "Read an integer into targetRpm after initializing it to 0.",
                "Read an integer into requestedMode after initializing it to 0.",
                "int requestedMode = 0;\nstd::cin >> requestedMode;",
                "The operator command channel is accepting target RPM values."
            )
            {
                ConceptPoints = new[] { "std::cin is the standard input stream.", ">> extracts input into a variable.", "Initialize variables before reading when the design requires it." },
                EditorFileNameOverride = "OperatorPanel.cpp"
            },
            new CourseLesson(
                "Chapter 1 · Foundry Foundations",
                "Operators",
                "Calculate Engine Output",
                "Arithmetic operators calculate new values. Compound assignment such as += performs a calculation and stores the result back in the same variable.",
                "int pressure = 40;\npressure += 5;",
                "int engineOutput = 60;\nengineOutput += 25;",
                "int engineOutput = ___;\nengineOutput ___ 25;",
                "What is engineOutput after += 25?",
                new[] { "25", "60", "85", "6025" },
                2,
                "The operator adds 25 to the existing value of 60 and stores 85.",
                "int engineOutput = 60;\nengineOutput =+ 25;",
                "Compound addition is written +=. Reversing the symbols assigns positive 25 instead.",
                "Start engineOutput at 60 and add 25 with compound assignment.",
                "Create the exact int variable fuelCells with 10, then subtract 2 from fuelCells with compound assignment.",
                "int fuelCells = 10;\nfuelCells -= 2;",
                "The power calculator now reports the foundry output level."
            )
            {
                ConceptPoints = new[] { "+= adds and saves the result.", "-= subtracts and saves the result.", "Expressions can update live simulation values." },
                EditorFileNameOverride = "PowerCalculator.cpp"
            },
            new CourseLesson(
                "Chapter 1 · Foundry Foundations",
                "Foundation challenge",
                "Build the First Foundry Dashboard",
                "A useful program combines several types and output statements. This dashboard stores the system identity and live values, then sends them to the terminal in a readable sequence.",
                "std::string machine = \"Pump\";\nint pressure = 42;\nbool running = true;\n\nstd::cout << machine << \"\\n\";",
                "std::string systemName = \"Forge Core\";\nint engineTemperature = 72;\nint engineOutput = 85;\nbool engineOnline = true;\n\nstd::cout << systemName << \"\\n\";\nstd::cout << engineTemperature << \"\\n\";\nstd::cout << engineOutput << \"\\n\";\nstd::cout << engineOnline << \"\\n\";",
                "std::string systemName = \"___\";\nint engineTemperature = ___;\nint engineOutput = ___;\nbool engineOnline = ___;\n\nstd::cout << systemName << \"\\n\";\nstd::cout << engineTemperature << \"\\n\";\nstd::cout << engineOutput << \"\\n\";\nstd::cout << engineOnline << \"\\n\";",
                "Which declaration stores the yes-or-no operating state?",
                new[] { "int engineOutput", "int engineTemperature", "bool engineOnline", "std::string systemName" },
                2,
                "bool engineOnline stores whether the engine is online.",
                "string systemName = Forge Core;\nint engineTemperature == 72;\nint engineOutput =+ 85;\nbool engineOnline = \"true\";",
                "Use std::string with quotes, single = for assignment, += style only when intended, and true without quotes.",
                "Build the dashboard with Forge Core, 72, 85, true, and print each value on its own line.",
                "Build a smaller panel with Pump A, 40, and false, printing each on its own line.",
                "std::string systemName = \"Pump A\";\nint pressure = 40;\nbool running = false;\n\nstd::cout << systemName << \"\\n\";\nstd::cout << pressure << \"\\n\";\nstd::cout << running << \"\\n\";",
                "The first Foundry dashboard is online."
            )
            {
                ConceptPoints = new[] { "Combine types into one readable report.", "Print each live value clearly.", "Chapter 1 skills work together." },
                EditorFileNameOverride = "FoundryDashboard.cpp"
            },
            new CourseLesson(
                "Chapter 2 · Control Systems",
                "Conditions",
                "Protect the Engine from Overheating",
                "Relational operators compare values and produce bool results. An if statement runs a block only when its condition is true.",
                "if (pressure > 100)\n{\n    status = 1;\n}",
                "if (engineTemperature > 90)\n{\n    alarmActive = true;\n}",
                "if (engineTemperature ___ 90)\n{\n    alarmActive = ___;\n}",
                "When does the if block run?",
                new[] { "When the condition is true", "Only when the condition is false", "Always", "Only once at compile time" },
                0,
                "The statements inside the braces run only when the condition evaluates to true.",
                "if engineTemperature > 90\n{\n    alarmActive = true;\n}",
                "The condition must be inside parentheses after if.",
                "If engineTemperature is greater than 90, set alarmActive to true.",
                "If oilPressure is greater than 50, set valveOpen to true.",
                "if (oilPressure > 50)\n{\n    valveOpen = true;\n}",
                "Overheat protection is armed on the thermal sensor."
            )
            {
                ConceptPoints = new[] { "> compares greater than.", "if controls optional actions.", "Braces group the guarded statements." },
                EditorFileNameOverride = "SafetyMonitor.cpp"
            },
            new CourseLesson(
                "Chapter 2 · Control Systems",
                "Alternative paths",
                "Choose Safe, Warning, or Shutdown",
                "else if chains additional tests. else handles every remaining case after earlier conditions fail.",
                "if (level >= 80)\n{\n    mode = 2;\n}\nelse if (level >= 40)\n{\n    mode = 1;\n}\nelse\n{\n    mode = 0;\n}",
                "if (engineTemperature >= 100)\n{\n    status = \"Shutdown\";\n}\nelse if (engineTemperature >= 85)\n{\n    status = \"Warning\";\n}\nelse\n{\n    status = \"Safe\";\n}",
                "if (engineTemperature >= ___)\n{\n    status = \"Shutdown\";\n}\nelse if (engineTemperature >= ___)\n{\n    status = \"Warning\";\n}\nelse\n{\n    status = \"___\";\n}",
                "Which branch runs when temperature is 70?",
                new[] { "Shutdown", "None of them", "Safe", "Warning" },
                2,
                "70 is below both thresholds, so the final else assigns Safe.",
                "if (engineTemperature >= 100)\n{\n    status = \"Shutdown\";\n}\nelse (engineTemperature >= 85)\n{\n    status = \"Warning\";\n}",
                "Additional conditions use else if, not else followed only by parentheses.",
                "Create Shutdown, Warning, and Safe statuses with thresholds 100 and 85.",
                "Create High, Medium, and Low for pressure with thresholds 60 and 30.",
                "if (pressure >= 60)\n{\n    status = \"High\";\n}\nelse if (pressure >= 30)\n{\n    status = \"Medium\";\n}\nelse\n{\n    status = \"Low\";\n}",
                "The foundry can now choose Safe, Warning, or Shutdown."
            )
            {
                ConceptPoints = new[] { "else if adds more tests.", "else catches remaining cases.", "Order matters in threshold checks." },
                EditorFileNameOverride = "SafetyMonitor.cpp"
            },
            new CourseLesson(
                "Chapter 2 · Control Systems",
                "Logical operators",
                "Verify Every Start Permission",
                "&& requires every condition to be true. || is true when at least one condition is true. ! flips a Boolean value.",
                "bool ready = hasFuel && hatchClosed;",
                "bool canStart = hasFuel && pressureOk && !emergencyStop;",
                "bool canStart = hasFuel ___ pressureOk ___ emergencyStop;",
                "When is A && B true?",
                new[] { "Only when both are false", "When either is true", "Whenever A is a number", "Only when both are true" },
                3,
                "Logical AND requires all connected conditions to be true.",
                "bool canStart = hasFuel & pressureOk & !emergencyStop;",
                "Boolean AND uses &&, not a single &.",
                "Combine hasFuel, pressureOk, and not emergencyStop into canStart.",
                "Create canVent using fanReady AND !ductBlocked.",
                "bool canVent = fanReady && !ductBlocked;",
                "Start permission now checks every interlock."
            )
            {
                ConceptPoints = new[] { "&& means AND.", "|| means OR.", "! negates a Boolean." },
                EditorFileNameOverride = "InterlockPanel.cpp"
            },
            new CourseLesson(
                "Chapter 2 · Control Systems",
                "Switch statements",
                "Select an Engine Operating Mode",
                "switch selects among discrete values of one expression. Each case label marks a branch, and break prevents fall-through.",
                "switch (code)\n{\ncase 1:\n    mode = \"A\";\n    break;\ndefault:\n    mode = \"X\";\n    break;\n}",
                "switch (modeCode)\n{\ncase 1:\n    modeName = \"Idle\";\n    break;\ncase 2:\n    modeName = \"Run\";\n    break;\ndefault:\n    modeName = \"Unknown\";\n    break;\n}",
                "switch (___)\n{\ncase 1:\n    modeName = \"Idle\";\n    break;\ncase 2:\n    modeName = \"Run\";\n    break;\ndefault:\n    modeName = \"Unknown\";\n    break;\n}",
                "What does break do inside a case?",
                new[] { "Required only in default", "Deletes the variable", "Restarts the program", "Stops the switch from continuing into the next case" },
                3,
                "break ends the switch so execution does not fall into the following case.",
                "switch (modeCode)\n{\ncase 1:\n    modeName = \"Idle\";\ncase 2:\n    modeName = \"Run\";\n}",
                "Include break after each case unless fall-through is intentional.",
                "Map modeCode 1 to Idle, 2 to Run, and everything else to Unknown.",
                "Map alertCode 1 to Info, 2 to Critical, and everything else to None.",
                "switch (alertCode)\n{\ncase 1:\n    alertName = \"Info\";\n    break;\ncase 2:\n    alertName = \"Critical\";\n    break;\ndefault:\n    alertName = \"None\";\n    break;\n}",
                "Operating modes can be selected from discrete codes."
            )
            {
                ConceptPoints = new[] { "switch branches on one value.", "case labels mark options.", "break prevents fall-through." },
                EditorFileNameOverride = "ModeSelector.cpp"
            },
            new CourseLesson(
                "Chapter 2 · Control Systems",
                "While loops",
                "Keep the Cooling Pump Running",
                "A while loop repeats a block as long as its condition stays true. The condition is checked before each iteration.",
                "while (count < 3)\n{\n    count++;\n}",
                "while (coolantTemp > 80)\n{\n    coolantTemp -= 2;\n}",
                "while (coolantTemp ___ 80)\n{\n    coolantTemp ___ 2;\n}",
                "When does a while loop stop?",
                new[] { "When the variable is an int", "After exactly one run", "When its condition becomes false", "Only when the program ends" },
                2,
                "The loop continues while the condition is true and stops when it becomes false.",
                "while coolantTemp > 80\n{\n    coolantTemp -= 2;\n}",
                "The while condition must be written inside parentheses.",
                "While coolantTemp is greater than 80, subtract 2 each pass.",
                "While tankLevel is greater than 10, subtract 1 each pass.",
                "while (tankLevel > 10)\n{\n    tankLevel -= 1;\n}",
                "The cooling pump loop can run until temperature is safe."
            )
            {
                ConceptPoints = new[] { "while checks before each loop.", "Update variables inside the body.", "Avoid infinite loops by changing the condition." },
                EditorFileNameOverride = "CoolingLoop.cpp"
            },
            new CourseLesson(
                "Chapter 2 · Control Systems",
                "For loops",
                "Inspect Every Turbine Blade",
                "A for loop packages initialization, condition, and update in one header. It is ideal when the number of passes is known.",
                "for (int i = 0; i < 3; i++)\n{\n    total += i;\n}",
                "for (int blade = 0; blade < 6; blade++)\n{\n    inspectedCount++;\n}",
                "for (int blade = ___; blade < ___; blade++)\n{\n    inspectedCount++;\n}",
                "What does blade++ do at the end of each pass?",
                new[] { "Stops the loop", "Prints blade", "Resets blade to 0", "Adds one to blade" },
                3,
                "The update expression blade++ increases the index after each iteration.",
                "for (int blade = 0; blade < 6; blade)\n{\n    inspectedCount++;\n}",
                "The update step should increment the loop variable, commonly with ++.",
                "Inspect 6 blades starting at 0, incrementing inspectedCount each time.",
                "Inspect 4 valves starting at 0, incrementing checkedCount each time.",
                "for (int valve = 0; valve < 4; valve++)\n{\n    checkedCount++;\n}",
                "Every turbine blade can be inspected in sequence."
            )
            {
                ConceptPoints = new[] { "for is ideal for counted loops.", "The index often starts at 0.", "The condition controls how long the loop runs." },
                EditorFileNameOverride = "InspectionBay.cpp"
            },
            new CourseLesson(
                "Chapter 2 · Control Systems",
                "Scope",
                "Keep Control Variables in Their Zones",
                "A variable's scope is the region where its name is visible. Variables declared inside braces belong to that block.",
                "int outer = 1;\n{\n    int inner = 2;\n}",
                "int plantId = 7;\nif (plantId > 0)\n{\n    int localMask = 1;\n    statusCode = plantId + localMask;\n}",
                "int plantId = ___;\nif (plantId > 0)\n{\n    int localMask = ___;\n    statusCode = plantId + localMask;\n}",
                "Where is localMask visible?",
                new[] { "Only inside the if block", "Everywhere in the file", "Only after the program ends", "Only inside main's parameters" },
                0,
                "localMask is declared inside the if block, so it exists only there.",
                "int plantId = 7;\nif (plantId > 0)\n{\n    int localMask = 1;\n}\nstatusCode = plantId + localMask;",
                "Do not use a block-local variable outside the braces where it was declared.",
                "Set plantId to 7 and inside the if create localMask 1 to compute statusCode.",
                "Set bayId to 3 and inside the if create localOffset 2 to compute routeCode.",
                "int bayId = 3;\nif (bayId > 0)\n{\n    int localOffset = 2;\n    routeCode = bayId + localOffset;\n}",
                "Control variables now stay inside their proper zones."
            )
            {
                ConceptPoints = new[] { "Block scope limits visibility.", "Declare variables close to use.", "Outer names remain visible in inner blocks." },
                EditorFileNameOverride = "ControlScope.cpp"
            },
            new CourseLesson(
                "Chapter 2 · Control Systems",
                "Enums and state",
                "Create a Reliable Engine State Machine",
                "An enum defines a set of named integral states. Using names instead of raw numbers makes mode logic easier to read.",
                "enum class Mode { Off, On };\nMode m = Mode::Off;",
                "enum class EngineState { Off, Idle, Running };\nEngineState state = EngineState::Idle;",
                "enum class EngineState { Off, Idle, Running };\nEngineState state = EngineState::___;",
                "Why use an enum class for engine modes?",
                new[] { "Enums replace main", "Named states are clearer than magic numbers", "Enums cannot be compared", "Enums run faster than int always" },
                1,
                "Enum names document intent better than unexplained integers.",
                "enum class EngineState { Off, Idle, Running };\nEngineState state = Idle;",
                "Scoped enumerators are accessed with EngineState:: before the name.",
                "Declare EngineState and set state to Idle.",
                "Declare PumpState with Off and On, and set pump to On.",
                "enum class PumpState { Off, On };\nPumpState pump = PumpState::On;",
                "The engine state machine has readable named modes."
            )
            {
                ConceptPoints = new[] { "enum class creates scoped states.", "Use Type::Value to refer to a state.", "States replace unexplained numbers." },
                EditorFileNameOverride = "StateMachine.cpp"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Collections",
                "Functions",
                "Build a Reusable Startup Function",
                "A function packages a named task. void means the function does not return a value. Call the function by writing its name followed by parentheses.",
                "void Pulse()\n{\n    std::cout << \"ok\\n\";\n}",
                "void StartFoundry()\n{\n    std::cout << \"Foundry starting\\n\";\n}",
                "void ___()\n{\n    std::cout << \"Foundry starting\\n\";\n}",
                "What does void mean here?",
                new[] { "The function cannot run", "The function returns text", "The function is main", "The function does not return a value" },
                3,
                "void marks a function that performs work without giving back a result value.",
                "void StartFoundry\n{\n    std::cout << \"Foundry starting\\n\";\n}",
                "Function definitions need () after the name even when there are no parameters.",
                "Write StartFoundry so it prints Foundry starting\\n.",
                "Write StopFoundry so it prints Foundry stopping\\n.",
                "void StopFoundry()\n{\n    std::cout << \"Foundry stopping\\n\";\n}",
                "Startup is now a reusable foundry function."
            )
            {
                ConceptPoints = new[] { "Functions group reusable steps.", "Call them by name.", "void means no returned value." },
                EditorFileNameOverride = "Startup.cpp"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Collections",
                "Parameters",
                "Send Settings into a Function",
                "Parameters receive input values when a function is called. Each parameter has a type and a name used inside the function body.",
                "void SetLimit(int max)\n{\n    limit = max;\n}",
                "void SetTargetRpm(int rpm)\n{\n    targetRpm = rpm;\n}",
                "void SetTargetRpm(int ___)\n{\n    targetRpm = ___;\n}",
                "How does rpm get its value?",
                new[] { "It is always 0", "It is chosen by the compiler randomly", "From the argument passed at the call site", "It comes from std::cout" },
                2,
                "The caller supplies an argument, which initializes the parameter.",
                "void SetTargetRpm(rpm)\n{\n    targetRpm = rpm;\n}",
                "Parameters need a type before the name, such as int rpm.",
                "Write SetTargetRpm that copies rpm into targetRpm.",
                "Write SetMaxTemp that copies temp into maxTemp.",
                "void SetMaxTemp(int temp)\n{\n    maxTemp = temp;\n}",
                "Settings can be sent into foundry functions cleanly."
            )
            {
                ConceptPoints = new[] { "Parameters accept inputs.", "Types must be declared.", "Names are used inside the body." },
                EditorFileNameOverride = "Startup.cpp"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Collections",
                "Return values",
                "Return a Calculated Efficiency",
                "A non-void function returns a value with return. The return type in the header must match the value being returned.",
                "int Double(int n)\n{\n    return n * 2;\n}",
                "int Efficiency(int output, int input)\n{\n    return (output * 100) / input;\n}",
                "int Efficiency(int output, int input)\n{\n    return (output * ___) / ___;\n}",
                "What does return do in this function?",
                new[] { "Creates a new variable in main", "Ends the whole program always", "Prints to the console", "Sends a result back to the caller" },
                3,
                "return provides the function's result to the code that called it.",
                "int Efficiency(int output, int input)\n{\n    (output * 100) / input;\n}",
                "Computed results must be returned with a return statement.",
                "Return efficiency as (output * 100) / input.",
                "Return percent as (part * 100) / whole.",
                "int Percent(int part, int whole)\n{\n    return (part * 100) / whole;\n}",
                "Efficiency calculations can return live results."
            )
            {
                ConceptPoints = new[] { "Return types are declared up front.", "return delivers the answer.", "Callers can store the result." },
                EditorFileNameOverride = "Metrics.cpp"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Collections",
                "Overloading",
                "Support Multiple Sensor Formats",
                "Function overloading allows the same function name with different parameter lists. The compiler chooses the overload that matches the arguments.",
                "int Read(int id) { return id; }\ndouble Read(double id) { return id; }",
                "int SensorReading(int code)\n{\n    return code;\n}\ndouble SensorReading(double code)\n{\n    return code;\n}",
                "int SensorReading(int code)\n{\n    return ___;\n}\ndouble SensorReading(double code)\n{\n    return ___;\n}",
                "How can two functions share the name SensorReading?",
                new[] { "They have different parameter types", "They must both return void", "They must be inside main", "C++ forbids that" },
                0,
                "Overloads are distinguished by parameter lists, not only by return type.",
                "int SensorReading(int code)\n{\n    return code;\n}\nint SensorReading(int code)\n{\n    return code;\n}",
                "Overloads must differ in parameters so the compiler can tell them apart.",
                "Provide int and double overloads of SensorReading that return the code.",
                "Provide int and double overloads of Scale that return the value.",
                "int Scale(int value)\n{\n    return value;\n}\ndouble Scale(double value)\n{\n    return value;\n}",
                "Sensors can be read in more than one numeric format."
            )
            {
                ConceptPoints = new[] { "Same name, different parameters.", "The compiler picks the match.", "Useful for related operations." },
                EditorFileNameOverride = "Sensors.cpp"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Collections",
                "Arrays",
                "Store a Fixed Sensor Bank",
                "A built-in array holds a fixed number of elements of one type. Indexes usually start at 0.",
                "int values[3] = {1, 2, 3};",
                "int sensorBank[4] = {10, 20, 30, 40};",
                "int sensorBank[___] = {10, 20, 30, 40};",
                "What is the index of the first element?",
                new[] { "0", "4", "10", "1" },
                0,
                "C++ arrays are zero-based, so the first element is at index 0.",
                "int sensorBank[4] = {10, 20, 30, 40}",
                "The array declaration and initializer need a terminating semicolon.",
                "Create sensorBank with four readings 10, 20, 30, and 40.",
                "Create heatCells with three readings 5, 6, and 7.",
                "int heatCells[3] = {5, 6, 7};",
                "A fixed sensor bank is ready for inspection."
            )
            {
                ConceptPoints = new[] { "Array size is fixed.", "Indexes start at 0.", "All elements share one type." },
                EditorFileNameOverride = "SensorBank.cpp"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Collections",
                "Vectors",
                "Grow the Maintenance Queue",
                "std::vector is a resizable sequence from the standard library. push_back adds an element at the end.",
                "std::vector<int> q;\nq.push_back(3);",
                "std::vector<int> maintenanceQueue;\nmaintenanceQueue.push_back(42);",
                "std::vector<int> maintenanceQueue;\nmaintenanceQueue.___(42);",
                "What does push_back do?",
                new[] { "Adds a value at the end of the vector", "Clears the vector", "Sorts the vector", "Removes the first value" },
                0,
                "push_back appends a new element after the current last item.",
                "std::vector<int> maintenanceQueue;\nmaintenanceQueue.push(42);",
                "The vector member that appends is push_back.",
                "Create maintenanceQueue and push_back 42.",
                "Create alertQueue and push_back 7.",
                "std::vector<int> alertQueue;\nalertQueue.push_back(7);",
                "The maintenance queue can grow as work arrives."
            )
            {
                ConceptPoints = new[] { "vector grows as needed.", "push_back appends.", "Include <vector> in full programs." },
                EditorFileNameOverride = "MaintenanceQueue.cpp"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Collections",
                "References and const",
                "Share Data Without Unnecessary Copies",
                "A reference is another name for an existing object. const signals that the value should not be modified through that name.",
                "void Touch(int& n) { n++; }",
                "void ApplyBoost(int& power)\n{\n    power += 10;\n}",
                "void ApplyBoost(int___ power)\n{\n    power += 10;\n}",
                "Why use int& for power?",
                new[] { "It makes a copy always", "It converts power to text", "It freezes the value", "Changes affect the original variable" },
                3,
                "A reference parameter lets the function modify the caller's variable.",
                "void ApplyBoost(int power)\n{\n    power += 10;\n}",
                "Without &, power is a copy and the caller's variable would not change.",
                "Write ApplyBoost that adds 10 to power through a reference.",
                "Write Drain that subtracts 5 from tank through a reference.",
                "void Drain(int& tank)\n{\n    tank -= 5;\n}",
                "Settings can be updated in place without extra copies."
            )
            {
                ConceptPoints = new[] { "& means reference.", "References alias existing objects.", "const can mark read-only access." },
                EditorFileNameOverride = "BoostControl.cpp"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Collections",
                "Integrated collections",
                "Build the Foundry Resource Manager",
                "Combine a fixed array of base stock with a vector queue of outgoing jobs for a small resource manager.",
                "int stock[2] = {1, 2};\nstd::vector<int> jobs;\njobs.push_back(9);",
                "int stock[3] = {5, 5, 5};\nstd::vector<int> jobs;\njobs.push_back(101);",
                "int stock[___] = {5, 5, 5};\nstd::vector<int> jobs;\njobs.___(101);",
                "Which structure grows when a new job arrives?",
                new[] { "The int type", "The return keyword", "jobs vector", "stock array" },
                2,
                "stock is fixed-size; jobs can grow with push_back.",
                "int stock[3] = {5, 5, 5};\nstd::vector<int> jobs;\njobs.push(101);",
                "Use push_back on the vector and keep the array size matched to its initializer.",
                "Create stock of three 5s and push job 101.",
                "Create stock of two 8s and push job 3.",
                "int stock[2] = {8, 8};\nstd::vector<int> jobs;\njobs.push_back(3);",
                "The Foundry resource manager tracks stock and jobs."
            )
            {
                ConceptPoints = new[] { "Arrays hold fixed banks.", "Vectors hold growing queues.", "Use each structure for its strength." },
                EditorFileNameOverride = "ResourceManager.cpp"
            },
            new CourseLesson(
                "Chapter 4 · Objects and Memory",
                "Structs",
                "Group Sensor Data into a Struct",
                "A struct groups related fields under one type so sensor readings travel together.",
                "struct Point { int x; int y; };",
                "struct SensorSample\n{\n    int id;\n    double value;\n};",
                "struct SensorSample\n{\n    ___ id;\n    ___ value;\n};",
                "What is SensorSample?",
                new[] { "A single int", "A type that groups related fields", "A function", "A preprocessor directive" },
                1,
                "struct defines a composite type made of named members.",
                "struct SensorSample\n{\n    int id\n    double value;\n};",
                "Each member declaration needs a semicolon.",
                "Define SensorSample with int id and double value.",
                "Define Pulse with int code and bool active.",
                "struct Pulse\n{\n    int code;\n    bool active;\n};",
                "Sensor readings can move as one structured sample."
            )
            {
                ConceptPoints = new[] { "struct groups fields.", "Members have types and names.", "Instances store actual values." },
                EditorFileNameOverride = "SensorSample.cpp"
            },
            new CourseLesson(
                "Chapter 4 · Objects and Memory",
                "Classes",
                "Design an Engine Component Class",
                "A class is a blueprint for objects. Members can include data and, in later modules, functions that operate on that data.",
                "class Part\n{\npublic:\n    int id;\n};",
                "class EngineComponent\n{\npublic:\n    int serial;\n    bool online;\n};",
                "class EngineComponent\n{\npublic:\n    int ___;\n    bool ___;\n};",
                "What does the class keyword define?",
                new[] { "A single variable only", "A comment block", "A type for creating objects", "A loop" },
                2,
                "class introduces a user-defined type used to create objects.",
                "class EngineComponent\n{\n    int serial;\n    bool online;\n};",
                "For this lesson, keep serial and online in the public section so they are accessible.",
                "Create EngineComponent with public serial and online fields.",
                "Create Valve with public id and open fields.",
                "class Valve\n{\npublic:\n    int id;\n    bool open;\n};",
                "Engine components now have a class blueprint."
            )
            {
                ConceptPoints = new[] { "class defines object structure.", "public members are accessible.", "Objects are instances of the class." },
                EditorFileNameOverride = "EngineComponent.cpp"
            },
            new CourseLesson(
                "Chapter 4 · Objects and Memory",
                "Constructors",
                "Initialize Components Safely",
                "A constructor runs when an object is created. It initializes members to a valid starting state.",
                "class Part\n{\npublic:\n    int id;\n    Part(int n) { id = n; }\n};",
                "class EngineComponent\n{\npublic:\n    int serial;\n    bool online;\n    EngineComponent(int n)\n    {\n        serial = n;\n        online = false;\n    }\n};",
                "class EngineComponent\n{\npublic:\n    int serial;\n    bool online;\n    EngineComponent(int n)\n    {\n        serial = ___;\n        online = ___;\n    }\n};",
                "When does the constructor run?",
                new[] { "Only if the object is const", "Only during #include", "Only when main ends", "When a new object is created" },
                3,
                "Creating an object automatically calls a matching constructor.",
                "class EngineComponent\n{\npublic:\n    int serial;\n    bool online;\n    void EngineComponent(int n)\n    {\n        serial = n;\n        online = false;\n    }\n};",
                "Constructors use the class name and do not use a return type such as void.",
                "Construct EngineComponent so serial becomes n and online starts false.",
                "Construct Valve so id becomes n and open starts false.",
                "class Valve\n{\npublic:\n    int id;\n    bool open;\n    Valve(int n)\n    {\n        id = n;\n        open = false;\n    }\n};",
                "New components start from a known safe state."
            )
            {
                ConceptPoints = new[] { "Constructors initialize objects.", "Name matches the class.", "No return type is used." },
                EditorFileNameOverride = "EngineComponent.cpp"
            },
            new CourseLesson(
                "Chapter 4 · Objects and Memory",
                "Encapsulation",
                "Protect Internal Engine State",
                "private members are accessible only inside the class. public methods provide a controlled interface.",
                "class Tank\n{\nprivate:\n    int level;\npublic:\n    int GetLevel() { return level; }\n};",
                "class EngineComponent\n{\nprivate:\n    int serial;\npublic:\n    int GetSerial()\n    {\n        return serial;\n    }\n};",
                "class EngineComponent\n{\nprivate:\n    int serial;\npublic:\n    int GetSerial()\n    {\n        return ___;\n    }\n};",
                "Why make serial private?",
                new[] { "So it uses less memory always", "So main can edit it directly", "To protect it from uncontrolled outside changes", "So the class cannot compile without it" },
                2,
                "Encapsulation hides internal data and exposes safe operations.",
                "class EngineComponent\n{\nprivate:\n    int serial;\npublic:\n    int GetSerial()\n    {\n        return Serial;\n    }\n};",
                "Member names are case-sensitive; return the private serial field.",
                "Return serial from GetSerial while keeping serial private.",
                "Return level from GetLevel while keeping level private.",
                "class Tank\n{\nprivate:\n    int level;\npublic:\n    int GetLevel()\n    {\n        return level;\n    }\n};",
                "Internal engine state is protected behind a public getter."
            )
            {
                ConceptPoints = new[] { "private hides data.", "public methods form the interface.", "Encapsulation improves safety." },
                EditorFileNameOverride = "EngineComponent.cpp"
            },
            new CourseLesson(
                "Chapter 4 · Objects and Memory",
                "Inheritance",
                "Create Specialized Component Types",
                "Inheritance lets a derived class reuse and extend a base class. The colon syntax names the base type.",
                "class Pump : public Part\n{\npublic:\n    int flow;\n};",
                "class Turbine : public EngineComponent\n{\npublic:\n    int bladeCount;\n};",
                "class Turbine : public ___\n{\npublic:\n    int bladeCount;\n};",
                "What does Turbine inherit from EngineComponent?",
                new[] { "Nothing; inheritance is cosmetic", "Only the main function", "The base members and interface available to derived classes", "Only preprocessor macros" },
                2,
                "A public derived class inherits the usable public interface of its base.",
                "class Turbine public EngineComponent\n{\npublic:\n    int bladeCount;\n};",
                "Use a colon before public BaseClass in the derived class head.",
                "Derive Turbine from EngineComponent and add bladeCount.",
                "Derive Pump from EngineComponent and add flowRate.",
                "class Pump : public EngineComponent\n{\npublic:\n    int flowRate;\n};",
                "Specialized component types extend the shared base."
            )
            {
                ConceptPoints = new[] { "Derived classes extend bases.", "Reuse common fields and behavior.", "Add only what is specialized." },
                EditorFileNameOverride = "Turbine.cpp"
            },
            new CourseLesson(
                "Chapter 4 · Objects and Memory",
                "Polymorphism",
                "Update Components Through One Interface",
                "A virtual function can be overridden in derived classes. Calling it through a base interface selects the derived behavior.",
                "class Base { public: virtual int Code() { return 0; } };",
                "class EngineComponent\n{\npublic:\n    virtual int TypeCode()\n    {\n        return 1;\n    }\n};",
                "class EngineComponent\n{\npublic:\n    ___ int TypeCode()\n    {\n        return 1;\n    }\n};",
                "What does virtual enable?",
                new[] { "Derived classes can override the function", "The function cannot be called", "The function becomes private", "The program skips main" },
                0,
                "virtual allows dynamic dispatch to an overriding derived implementation.",
                "class EngineComponent\n{\npublic:\n    int TypeCode()\n    {\n        return 1;\n    }\n};",
                "Mark TypeCode virtual so derived types can override it.",
                "Declare virtual TypeCode that returns 1.",
                "Declare virtual Channel that returns 2.",
                "class EngineComponent\n{\npublic:\n    virtual int Channel()\n    {\n        return 2;\n    }\n};",
                "Components can be updated through one virtual interface."
            )
            {
                ConceptPoints = new[] { "virtual supports overrides.", "One interface, many behaviors.", "Useful for system-wide updates." },
                EditorFileNameOverride = "Polymorphism.cpp"
            },
            new CourseLesson(
                "Chapter 4 · Objects and Memory",
                "Pointers",
                "Understand Addresses and Pointers",
                "A pointer stores the address of another object. The * in a declaration marks a pointer type, and & takes an address.",
                "int n = 3;\nint* p = &n;",
                "int rpm = 1200;\nint* rpmPtr = &rpm;",
                "int rpm = ___;\nint* rpmPtr = ___rpm;",
                "What does &rpm mean?",
                new[] { "A comment", "The address of rpm", "A string conversion", "Multiply rpm" },
                1,
                "The address-of operator & produces a pointer to the object.",
                "int rpm = 1200;\nint* rpmPtr = rpm;",
                "Assign the address of rpm with &rpm, not the value alone.",
                "Point rpmPtr at rpm after setting rpm to 1200.",
                "Point tempPtr at temp after setting temp to 72.",
                "int temp = 72;\nint* tempPtr = &temp;",
                "The foundry can track values by address when needed."
            )
            {
                ConceptPoints = new[] { "Pointers hold addresses.", "& takes an address.", "* declares a pointer type." },
                EditorFileNameOverride = "Pointers.cpp"
            },
            new CourseLesson(
                "Chapter 4 · Objects and Memory",
                "Smart pointers",
                "Manage Component Ownership",
                "std::unique_ptr owns a dynamically allocated object and destroys it automatically when the smart pointer leaves scope.",
                "std::unique_ptr<int> p = std::make_unique<int>(3);",
                "std::unique_ptr<int> serial = std::make_unique<int>(1001);",
                "std::unique_ptr<int> serial = std::make_unique<int>(___);",
                "What is an advantage of unique_ptr?",
                new[] { "It helps manage ownership and cleanup", "It replaces the need for main", "It disables all functions", "It makes numbers larger" },
                0,
                "unique_ptr ties object lifetime to ownership, reducing leaks from forgotten delete.",
                "std::unique_ptr<int> serial = new int(1001);",
                "Prefer std::make_unique to create a unique_ptr safely.",
                "Create unique_ptr serial owning int 1001.",
                "Create unique_ptr ticket owning int 55.",
                "std::unique_ptr<int> ticket = std::make_unique<int>(55);",
                "Component ownership can be managed with smart pointers."
            )
            {
                ConceptPoints = new[] { "unique_ptr owns one object.", "make_unique creates it.", "Ownership prevents many leaks." },
                EditorFileNameOverride = "Ownership.cpp"
            },
            new CourseLesson(
                "Chapter 5 · Real-Time Engine Systems",
                "Headers and source",
                "Split the Engine into Maintainable Files",
                "Large programs split declarations into headers and definitions into source files. #ifndef include guards prevent duplicate inclusion.",
                "#ifndef ENGINE_H\n#define ENGINE_H\nvoid Start();\n#endif",
                "#ifndef FOUNDRY_H\n#define FOUNDRY_H\nvoid StartFoundry();\n#endif",
                "#ifndef ___\n#define ___\nvoid StartFoundry();\n#endif",
                "Why use an include guard?",
                new[] { "To prevent the header from being processed twice", "To start main automatically", "To disable std::cout", "To make ints become doubles" },
                0,
                "Guards ensure a header's declarations are only introduced once per translation unit.",
                "#define FOUNDRY_H\nvoid StartFoundry();\n#endif",
                "A complete guard uses #ifndef, #define, and #endif around the declarations.",
                "Guard the header FOUNDRY_H and declare StartFoundry.",
                "Guard the header PUMP_H and declare StartPump.",
                "#ifndef PUMP_H\n#define PUMP_H\nvoid StartPump();\n#endif",
                "Foundry declarations can live in a clean header."
            )
            {
                ConceptPoints = new[] { "Headers declare interfaces.", "Source files define bodies.", "Guards stop double inclusion." },
                EditorFileNameOverride = "Foundry.h"
            },
            new CourseLesson(
                "Chapter 5 · Real-Time Engine Systems",
                "Components",
                "Assemble an Entity Component System",
                "An entity can be represented by an id, with separate component values stored alongside it for flexible composition.",
                "int entity = 1;\nint health = 10;",
                "int entityId = 7;\nint heatComponent = 40;",
                "int entityId = ___;\nint heatComponent = ___;",
                "What does entityId represent here?",
                new[] { "A C++ keyword", "A required name for main", "The identity of the simulated object", "A preprocessor flag" },
                2,
                "The id identifies the entity while components store its data.",
                "int entityId = 7\nint heatComponent = 40;",
                "Each declaration needs its own semicolon.",
                "Create entityId 7 and heatComponent 40.",
                "Create entityId 3 and fuelComponent 9.",
                "int entityId = 3;\nint fuelComponent = 9;",
                "Entity and component data can be assembled for simulation."
            )
            {
                ConceptPoints = new[] { "Entities are identities.", "Components hold data.", "Systems process combinations." },
                EditorFileNameOverride = "EcsCore.cpp"
            },
            new CourseLesson(
                "Chapter 5 · Real-Time Engine Systems",
                "Timing",
                "Measure Frame Time",
                "Real-time systems track how much time passes each frame. A simple model stores the previous time and computes a delta.",
                "double delta = now - previous;",
                "double frameDelta = currentTime - previousTime;",
                "double frameDelta = currentTime ___ previousTime;",
                "What is frameDelta?",
                new[] { "The program's version number", "Always zero", "The elapsed time since the previous frame", "The number of classes" },
                2,
                "Subtracting the previous timestamp from the current one yields the frame duration.",
                "double frameDelta = currentTime + previousTime;",
                "Delta time is a difference, not a sum.",
                "Compute frameDelta from currentTime minus previousTime.",
                "Compute step from nowTime minus lastTime.",
                "double step = nowTime - lastTime;",
                "Frame timing is available to the engine loop."
            )
            {
                ConceptPoints = new[] { "Track previous timestamps.", "Delta drives motion steps.", "Stable timing smooths simulation." },
                EditorFileNameOverride = "FrameTimer.cpp"
            },
            new CourseLesson(
                "Chapter 5 · Real-Time Engine Systems",
                "Game loops",
                "Run the Real-Time Engine Loop",
                "A real-time loop continues while the simulation is running, updating state each pass.",
                "while (running)\n{\n    Update();\n}",
                "while (engineRunning)\n{\n    UpdateFoundry();\n}",
                "while (___)\n{\n    UpdateFoundry();\n}",
                "What keeps the loop repeating?",
                new[] { "UpdateFoundry returns void", "The class keyword", "The file name", "engineRunning stays true" },
                3,
                "The loop condition controls whether another frame should run.",
                "while engineRunning\n{\n    UpdateFoundry();\n}",
                "Put the condition in parentheses after while.",
                "Loop while engineRunning and call UpdateFoundry each pass.",
                "Loop while simActive and call StepSim each pass.",
                "while (simActive)\n{\n    StepSim();\n}",
                "The real-time foundry loop is running."
            )
            {
                ConceptPoints = new[] { "Loops drive frames.", "Update each pass.", "Stop when the running flag clears." },
                EditorFileNameOverride = "EngineLoop.cpp"
            },
            new CourseLesson(
                "Chapter 5 · Real-Time Engine Systems",
                "Events",
                "Send Events Between Systems",
                "Events carry a code that other systems can interpret. A simple queue stores event codes until they are handled.",
                "std::vector<int> events;\nevents.push_back(1);",
                "std::vector<int> eventQueue;\neventQueue.push_back(200);",
                "std::vector<int> eventQueue;\neventQueue.___(200);",
                "What does event code 200 represent in this design?",
                new[] { "A required C++ keyword", "A queued message for another system to handle", "The end of main", "A compiler error code only" },
                1,
                "The integer is an event identifier placed on the queue for later handling.",
                "std::vector<int> eventQueue;\neventQueue.push(200);",
                "Use push_back to enqueue the event code.",
                "Enqueue event 200 on eventQueue.",
                "Enqueue event 9 on signalQueue.",
                "std::vector<int> signalQueue;\nsignalQueue.push_back(9);",
                "Systems can communicate through queued events."
            )
            {
                ConceptPoints = new[] { "Events decouple systems.", "Queues store pending work.", "Handlers interpret codes." },
                EditorFileNameOverride = "EventBus.cpp"
            },
            new CourseLesson(
                "Chapter 5 · Real-Time Engine Systems",
                "Simulation",
                "Update Motion and Machine Physics",
                "A simple physics step updates position using velocity and delta time.",
                "position += velocity * dt;",
                "position += velocity * frameDelta;",
                "position ___ velocity * frameDelta;",
                "What does this update represent?",
                new[] { "Moving position forward by velocity over the frame", "Stopping the loop", "Deleting velocity", "Declaring a class" },
                0,
                "Position advances by the distance traveled during frameDelta.",
                "position = velocity * frameDelta;",
                "Use += to apply the delta onto the existing position.",
                "Advance position by velocity * frameDelta.",
                "Advance depth by rate * step.",
                "depth += rate * step;",
                "Motion and machine physics advance each frame."
            )
            {
                ConceptPoints = new[] { "Integrate velocity over time.", "Use frame delta.", "Repeat each loop pass." },
                EditorFileNameOverride = "PhysicsStep.cpp"
            },
            new CourseLesson(
                "Chapter 5 · Real-Time Engine Systems",
                "Persistence",
                "Save and Restore the Foundry",
                "Persistence stores important values so a session can resume. A simple model copies live state into save slots.",
                "int saveHeat = heat;",
                "int savedTemperature = engineTemperature;\nint savedOutput = engineOutput;",
                "int savedTemperature = ___;\nint savedOutput = ___;",
                "What is the purpose of savedTemperature?",
                new[] { "To delete engineTemperature", "To remember the live temperature for later restore", "To create a thread", "To print a blank line" },
                1,
                "Saved fields hold a snapshot of values you may restore later.",
                "int savedTemperature == engineTemperature;\nint savedOutput == engineOutput;",
                "Use assignment = to copy values into save slots.",
                "Copy engineTemperature and engineOutput into saved fields.",
                "Copy tankLevel and pumpRpm into saved fields.",
                "int savedLevel = tankLevel;\nint savedRpm = pumpRpm;",
                "Foundry state can be saved for restore."
            )
            {
                ConceptPoints = new[] { "Snapshot important values.", "Restore by copying back.", "Persistence supports long sessions." },
                EditorFileNameOverride = "SaveState.cpp"
            },
            new CourseLesson(
                "Chapter 5 · Real-Time Engine Systems",
                "Final integration",
                "Launch the Complete Engine Foundry",
                "The final challenge combines state, a running flag, and a loop that updates output while the foundry is online.",
                "bool running = true;\nint output = 0;\nwhile (running)\n{\n    output += 1;\n    running = false;\n}",
                "bool engineRunning = true;\nint engineOutput = 0;\nwhile (engineRunning)\n{\n    engineOutput += 5;\n    engineRunning = false;\n}",
                "bool engineRunning = ___;\nint engineOutput = ___;\nwhile (engineRunning)\n{\n    engineOutput += ___;\n    engineRunning = ___;\n}",
                "What ends this sample loop after one update?",
                new[] { "The compiler ignores while", "engineOutput becomes text", "main is deleted", "engineRunning is set to false" },
                3,
                "Clearing the running flag causes the while condition to fail on the next check.",
                "bool engineRunning = true;\nint engineOutput = 0;\nwhile (engineRunning)\n{\n    engineOutput += 5;\n}",
                "Provide a way for the loop condition to become false, such as setting engineRunning to false.",
                "Start running with output 0, add 5 once, then stop the loop.",
                "Start active with total 0, add 2 once, then stop the loop.",
                "bool active = true;\nint total = 0;\nwhile (active)\n{\n    total += 2;\n    active = false;\n}",
                "The complete Engine Foundry training path is online."
            )
            {
                ConceptPoints = new[] { "Combine flags, values, and loops.", "Integrate prior skills.", "Ship a coherent startup sequence." },
                EditorFileNameOverride = "FoundryLaunch.cpp"
            }
        };
}
