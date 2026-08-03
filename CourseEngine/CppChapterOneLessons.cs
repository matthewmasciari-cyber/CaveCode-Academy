namespace CaveCode.CourseEngine;

public static class CppChapterOneLessons
{
    public const int PlayableModuleCount = 8;

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
                new[] { "main", "start", "iostream", "return" },
                0,
                "The operating system begins the program by calling main.",
                "include <iostream>\n\nint main()\n{\n    return 0\n}",
                "The library directive needs # at the beginning, and the return statement needs a semicolon.",
                "Rebuild the minimal Engine Foundry program from memory.",
                "Create another minimal program that includes the string library and returns success.",
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
                "std::___ __ \"Foundry online\\n\";",
                "What does the << operator do with std::cout?",
                new[] { "Sends a value into the output stream", "Reads keyboard input", "Compares two values", "Starts the program" },
                0,
                "With std::cout, << inserts the value on its right into the console output stream.",
                "std::cout < \"Foundry online\\n\"",
                "Console output needs two less-than symbols and the statement must end with a semicolon.",
                "Print Foundry online followed by a newline.",
                "Print Cooling system ready followed by a newline.",
                "std::cout << \"Cooling system ready\\n\";",
                "The diagnostic message board can now report foundry status."
            )
            {
                ConceptPoints = new[] { "std:: identifies the standard namespace.", "cout means character output.", "\\n begins a new console line." },
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
                new[] { "72 is a whole number", "72 is text", "72 is true or false", "int prints automatically" },
                0,
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
                "Numeric and Boolean types",
                "Choose the Right Engine Data Types",
                "C++ uses different types for different kinds of information. int stores whole numbers, double stores decimal values, and bool stores true or false.",
                "int bladeCount = 8;\ndouble efficiency = 94.5;\nbool inspectionPassed = true;",
                "int engineRpm = 1200;\ndouble coolantTemperature = 72.5;\nbool engineOnline = true;",
                "___ engineRpm = 1200;\n___ coolantTemperature = 72.5;\n___ engineOnline = true;",
                "Which type should store whether the engine is online?",
                new[] { "bool", "double", "int", "std::string" },
                0,
                "A yes-or-no state is represented directly by bool.",
                "double engineRpm = \"1200\";\nint coolantTemperature = 72.5;\nbool engineOnline = \"true\";",
                "Use int for whole RPM, double for the decimal temperature, and bool true without quotation marks.",
                "Recreate the RPM, coolant temperature, and online-state declarations.",
                "Store 8 blades, 94.5 percent efficiency, and a passed inspection.",
                "int bladeCount = 8;\ndouble efficiency = 94.5;\nbool inspectionPassed = true;",
                "RPM, decimal temperature, and power-state instruments are active."
            )
            {
                ConceptPoints = new[] { "int stores whole numbers.", "double stores decimal measurements.", "bool stores true or false without quotation marks." },
                EditorFileNameOverride = "EngineState.cpp"
            },
            new CourseLesson(
                "Chapter 1 · Foundry Foundations",
                "Text",
                "Label the Foundry Systems",
                "std::string stores text. Text values go inside quotation marks, and std:: identifies the string type from the standard library.",
                "std::string zoneName = \"Cooling Bay\";",
                "std::string systemName = \"Forge Core\";",
                "std::___ systemName = \"___\";",
                "Why does Forge Core need quotation marks?",
                new[] { "It is a text value", "It is a variable name", "It is a Boolean", "It is a comment" },
                0,
                "Quotation marks tell C++ that the characters form a string literal.",
                "string systemName = Forge Core;",
                "Use std::string and place the text value inside quotation marks.",
                "Create systemName with the value Forge Core.",
                "Create subsystemName with the value Turbine Bank.",
                "std::string subsystemName = \"Turbine Bank\";",
                "The foundry now displays readable names for its systems."
            )
            {
                ConceptPoints = new[] { "std::string stores text.", "Quotation marks create a string literal.", "Descriptive names make system state easier to understand." },
                EditorFileNameOverride = "FoundryLabels.cpp"
            },
            new CourseLesson(
                "Chapter 1 · Foundry Foundations",
                "Input",
                "Read an Operator Command",
                "std::cin reads keyboard input. The >> operator extracts a value from the input stream and stores it in the variable on the right.",
                "int fanSpeed = 0;\nstd::cin >> fanSpeed;",
                "int targetRpm = 0;\nstd::cin >> targetRpm;",
                "int targetRpm = ___;\nstd::___ __ targetRpm;",
                "Where is the operator input stored?",
                new[] { "In targetRpm", "Inside std", "Inside the >> operator", "It is not stored" },
                0,
                "The extraction operator places the entered integer into targetRpm.",
                "int targetRpm = 0;\nstd::cin << targetRpm;",
                "Input uses >> because data moves from std::cin into the variable.",
                "Create targetRpm and read an operator value into it.",
                "Create a command string and read one word into it.",
                "std::string command;\nstd::cin >> command;",
                "The operator console can now send commands into the simulation."
            )
            {
                ConceptPoints = new[] { "std::cin is the standard input stream.", ">> extracts input into a variable.", "The variable type determines what kind of value can be read." },
                EditorFileNameOverride = "OperatorConsole.cpp"
            },
            new CourseLesson(
                "Chapter 1 · Foundry Foundations",
                "Operators",
                "Calculate Engine Output",
                "Arithmetic operators calculate new values. Compound assignment such as += performs a calculation and stores the result back in the same variable.",
                "int pressure = 40;\npressure += 5;",
                "int engineOutput = 60;\nengineOutput += 25;",
                "int engineOutput = ___;\nengineOutput __ 25;",
                "What is engineOutput after += 25?",
                new[] { "85", "60", "25", "6025" },
                0,
                "The operator adds 25 to the existing value of 60 and stores 85.",
                "int engineOutput = 60;\nengineOutput =+ 25;",
                "Compound addition is written +=. Reversing the symbols assigns positive 25 instead.",
                "Start engineOutput at 60 and add 25 with compound assignment.",
                "Start with 10 fuel cells and subtract 2 with compound assignment.",
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
                "std::string machine = \"Pump\";\nint pressure = 42;\nbool running = true;\n\nstd::cout << machine << \"\\n\";\nstd::cout << pressure << \"\\n\";\nstd::cout << running << \"\\n\";",
                "std::string systemName = \"Forge Core\";\nint engineTemperature = 72;\nint engineOutput = 85;\nbool engineOnline = true;\n\nstd::cout << systemName << \"\\n\";\nstd::cout << engineTemperature << \"\\n\";\nstd::cout << engineOutput << \"\\n\";\nstd::cout << engineOnline << \"\\n\";",
                "std::string systemName = \"___\";\nint engineTemperature = ___;\nint engineOutput = ___;\nbool engineOnline = ___;\n\nstd::cout << ___ << \"\\n\";\nstd::cout << ___ << \"\\n\";\nstd::cout << ___ << \"\\n\";\nstd::cout << ___ << \"\\n\";",
                "Which declaration stores the yes-or-no operating state?",
                new[] { "bool engineOnline", "int engineOutput", "std::string systemName", "int engineTemperature" },
                0,
                "engineOnline uses bool because it represents true or false.",
                "string systemName = Forge Core;\nint engineTemperature == 72;\nint engineOutput =+ 85;\nbool engineOnline = \"true\";\n\ncout < systemName;\ncout < engineTemperature;\ncout < engineOutput;\ncout < engineOnline;",
                "Repair the std namespaces, quotation marks, assignment operators, Boolean value, output operators, newline text, and semicolons.",
                "Rebuild the complete Forge Core dashboard from memory.",
                "Create and print a maintenance dashboard with a name, open-job count, and maintenance-mode state.",
                "std::string dashboardName = \"Maintenance\";\nint openJobs = 3;\nbool maintenanceMode = true;\n\nstd::cout << dashboardName << \"\\n\";\nstd::cout << openJobs << \"\\n\";\nstd::cout << maintenanceMode << \"\\n\";",
                "Chapter 1 complete: the first Engine Foundry dashboard is operational."
            )
            {
                ConceptPoints = new[] { "Programs combine several data types.", "Each output line can display a different value.", "The dashboard connects stored state to visible system feedback." },
                EditorFileNameOverride = "FoundryDashboard.cpp"
            }
        };

    public static CourseDefinition Definition(CourseManifest manifest) =>
        new(manifest, All);
}
