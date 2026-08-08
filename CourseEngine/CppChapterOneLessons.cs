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
                new[] { "iostream", "return", "start", "main" },
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
                "std::___ __ \"Foundry online\\n\";",
                "What does the << operator do with std::cout?",
                new[] { "Compares two values", "Reads keyboard input", "Starts the program", "Sends a value into the output stream" },
                3,
                "With std::cout, << inserts the value on its right into the console output stream.",
                "std::cout < \"Foundry online\\n\"",
                "Console output needs two less-than symbols and the statement must end with a semicolon.",
                "Use the exact identifiers std and cout as std::cout. Print the exact text Foundry online\\n.",
                "Use the exact identifiers std and cout as std::cout. Print the exact text Cooling system ready\\n.",
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
                new[] { "int prints automatically", "72 is a whole number", "72 is true or false", "72 is text" },
                1,
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
                new[] { "double", "int", "bool", "std::string" },
                2,
                "A yes-or-no state is represented directly by bool.",
                "double engineRpm = \"1200\";\nint coolantTemperature = 72.5;\nbool engineOnline = \"true\";",
                "Use int for whole RPM, double for the decimal temperature, and bool true without quotation marks.",
                "Create the exact variables engineRpm with 1200, coolantTemperature with 72.5, and engineOnline with true.",
                "Create the exact variables bladeCount with 8, efficiency with 94.5, and inspectionPassed with true.",
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
                new[] { "It is a Boolean", "It is a text value", "It is a variable name", "It is a comment" },
                1,
                "Quotation marks tell C++ that the characters form a string literal.",
                "string systemName = Forge Core;",
                "Use std::string and place the text value inside quotation marks.",
                "Create the exact std::string variable systemName with the value Forge Core.",
                "Create the exact std::string variable subsystemName with the value Turbine Bank.",
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
                new[] { "Inside the >> operator", "In targetRpm", "It is not stored", "Inside std" },
                1,
                "The extraction operator places the entered integer into targetRpm.",
                "int targetRpm = 0;\nstd::cin << targetRpm;",
                "Input uses >> because data moves from std::cin into the variable.",
                "Create the exact int variable targetRpm with the starting value 0, then use the exact identifiers std and cin as std::cin to read into targetRpm.",
                "Create the exact std::string variable command, then use the exact identifiers std and cin as std::cin to read one word into command.",
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
                new[] { "60", "25", "6025", "85" },
                3,
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
                "std::string machine = \"Pump\";\nint pressure = 42;\nbool running = true;\n\nstd::cout << machine << \"\\n\";\nstd::cout << pressure << \"\\n\";\nstd::cout << running << \"\\n\";",
                "std::string systemName = \"Forge Core\";\nint engineTemperature = 72;\nint engineOutput = 85;\nbool engineOnline = true;\n\nstd::cout << systemName << \"\\n\";\nstd::cout << engineTemperature << \"\\n\";\nstd::cout << engineOutput << \"\\n\";\nstd::cout << engineOnline << \"\\n\";",
                "std::string systemName = \"___\";\nint engineTemperature = ___;\nint engineOutput = ___;\nbool engineOnline = ___;\n\nstd::cout << ___ << \"\\n\";\nstd::cout << ___ << \"\\n\";\nstd::cout << ___ << \"\\n\";\nstd::cout << ___ << \"\\n\";",
                "Which declaration stores the yes-or-no operating state?",
                new[] { "int engineOutput", "std::string systemName", "bool engineOnline", "int engineTemperature" },
                2,
                "engineOnline uses bool because it represents true or false.",
                "string systemName = Forge Core;\nint engineTemperature == 72;\nint engineOutput =+ 85;\nbool engineOnline = \"true\";\n\ncout < systemName;\ncout < engineTemperature;\ncout < engineOutput;\ncout < engineOnline;",
                "Repair the std namespaces, quotation marks, assignment operators, Boolean value, output operators, newline text, and semicolons.",
                "Rebuild the exact Forge Core dashboard. Create std::string systemName with Forge Core, int engineTemperature with 72, int engineOutput with 85, and bool engineOnline with true. Use the exact identifiers std and cout as std::cout to print systemName, engineTemperature, engineOutput, and engineOnline, with the exact newline text \\n after every value.",
                "Create the exact maintenance dashboard. Create std::string dashboardName with Maintenance, int openJobs with 3, and bool maintenanceMode with true. Use the exact identifiers std and cout as std::cout to print dashboardName, openJobs, and maintenanceMode, with the exact newline text \\n after every value.",
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
