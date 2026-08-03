namespace CaveCode.CourseEngine;

public sealed record CppRoadmapModule(
    int Number,
    string Chapter,
    string Topic,
    string Title,
    string Summary,
    string ProjectUpgrade,
    string EditorFileName,
    string SystemArea,
    string[] Concepts
);

public static class CppCourseRoadmap
{
    public static IReadOnlyList<CppRoadmapModule> Modules { get; } =
        new[]
        {
            new CppRoadmapModule(
                1,
                "Chapter 1 · Foundry Foundations",
                "Program structure",
                "Boot the Engine Foundry",
                "Meet a minimal C++ program and learn how an engine process starts.",
                "Power the foundry console and bring the central engine core online.",
                "EngineTraining.cpp",
                "Core startup",
                new[] { "main()", "#include", "statements", "program entry point" }
            ),
            new CppRoadmapModule(
                2,
                "Chapter 1 · Foundry Foundations",
                "Console output",
                "Send a Foundry Status Message",
                "Use the standard output stream to display readable engine status.",
                "Activate the foundry diagnostic message board.",
                "EngineTraining.cpp",
                "Diagnostics",
                new[] { "std::cout", "stream insertion", "string literals", "newlines" }
            ),
            new CppRoadmapModule(
                3,
                "Chapter 1 · Foundry Foundations",
                "Variables",
                "Store the Engine Temperature",
                "Create named storage for changing engine values.",
                "Connect the thermal sensor to the live status panel.",
                "EngineState.cpp",
                "Thermal sensor",
                new[] { "declarations", "initialization", "assignment", "identifiers" }
            ),
            new CppRoadmapModule(
                4,
                "Chapter 1 · Foundry Foundations",
                "Numeric types",
                "Choose the Right Engine Data Types",
                "Compare integers, floating-point values, and booleans.",
                "Add RPM, temperature, and power-state instrumentation.",
                "EngineState.cpp",
                "Instrumentation",
                new[] { "int", "double", "float", "bool" }
            ),
            new CppRoadmapModule(
                5,
                "Chapter 1 · Foundry Foundations",
                "Text",
                "Label the Foundry Systems",
                "Store and display system names with standard strings.",
                "Label each machine bank and engine subsystem.",
                "FoundryLabels.cpp",
                "System labels",
                new[] { "std::string", "text values", "concatenation", "namespaces" }
            ),
            new CppRoadmapModule(
                6,
                "Chapter 1 · Foundry Foundations",
                "Input",
                "Read an Operator Command",
                "Accept basic input and use it to change a simulated system.",
                "Connect the operator terminal to the ignition controls.",
                "OperatorConsole.cpp",
                "Operator console",
                new[] { "std::cin", "input variables", "prompts", "user interaction" }
            ),
            new CppRoadmapModule(
                7,
                "Chapter 1 · Foundry Foundations",
                "Operators",
                "Calculate Engine Output",
                "Combine values using arithmetic and assignment operators.",
                "Compute the foundry power output and load percentage.",
                "PowerCalculator.cpp",
                "Power calculation",
                new[] { "arithmetic operators", "precedence", "compound assignment", "expressions" }
            ),
            new CppRoadmapModule(
                8,
                "Chapter 1 · Foundry Foundations",
                "Integrated foundations",
                "Build the First Foundry Dashboard",
                "Combine output, variables, input, and calculations in one program.",
                "Complete the first operational Engine Foundry dashboard.",
                "FoundryDashboard.cpp",
                "Foundry dashboard",
                new[] { "program flow", "state", "input/output", "calculated values" }
            ),
            new CppRoadmapModule(
                9,
                "Chapter 2 · Control Systems",
                "Conditions",
                "Protect the Engine from Overheating",
                "Use if statements to react to dangerous engine temperatures.",
                "Install the first automatic thermal shutdown.",
                "ThermalSafety.cpp",
                "Thermal safety",
                new[] { "if", "comparison operators", "conditions", "decision paths" }
            ),
            new CppRoadmapModule(
                10,
                "Chapter 2 · Control Systems",
                "Alternative paths",
                "Choose Safe, Warning, or Shutdown",
                "Build multiple control paths with else-if and else.",
                "Add three-state warning lights to the foundry.",
                "ThermalSafety.cpp",
                "Warning states",
                new[] { "else if", "else", "branch order", "exclusive paths" }
            ),
            new CppRoadmapModule(
                11,
                "Chapter 2 · Control Systems",
                "Logical operators",
                "Verify Every Start Permission",
                "Combine several safety signals into one start decision.",
                "Wire guards, cooling, and emergency-stop permissions together.",
                "StartInterlock.cpp",
                "Start interlock",
                new[] { "&&", "||", "!", "compound conditions" }
            ),
            new CppRoadmapModule(
                12,
                "Chapter 2 · Control Systems",
                "Switch statements",
                "Select an Engine Operating Mode",
                "Use switch to route commands through distinct operating modes.",
                "Add OFF, IDLE, RUN, and SERVICE modes.",
                "OperatingMode.cpp",
                "Mode selector",
                new[] { "switch", "case", "break", "default" }
            ),
            new CppRoadmapModule(
                13,
                "Chapter 2 · Control Systems",
                "While loops",
                "Keep the Cooling Pump Running",
                "Repeat an action while a simulated condition remains true.",
                "Animate the cooling loop until temperature returns to normal.",
                "CoolingLoop.cpp",
                "Cooling loop",
                new[] { "while", "loop conditions", "state changes", "termination" }
            ),
            new CppRoadmapModule(
                14,
                "Chapter 2 · Control Systems",
                "For loops",
                "Inspect Every Turbine Blade",
                "Repeat a known number of checks with a counting loop.",
                "Scan and illuminate each turbine blade in sequence.",
                "BladeInspection.cpp",
                "Blade scanner",
                new[] { "for", "loop counter", "increment", "iteration" }
            ),
            new CppRoadmapModule(
                15,
                "Chapter 2 · Control Systems",
                "Scope",
                "Keep Control Variables in Their Zones",
                "Understand where variables exist and where they can be used.",
                "Separate local machine controls from shared foundry state.",
                "ControlZones.cpp",
                "Control zones",
                new[] { "block scope", "local variables", "lifetime", "shadowing" }
            ),
            new CppRoadmapModule(
                16,
                "Chapter 2 · Control Systems",
                "Enums and state",
                "Create a Reliable Engine State Machine",
                "Represent clear operating states with an enumeration.",
                "Complete startup, run, fault, and shutdown sequencing.",
                "EngineStateMachine.cpp",
                "State machine",
                new[] { "enum class", "state transitions", "switch", "control logic" }
            ),
            new CppRoadmapModule(
                17,
                "Chapter 3 · Functions and Collections",
                "Functions",
                "Build a Reusable Startup Function",
                "Move repeated work into a named reusable function.",
                "Turn the startup sequence into a reusable control routine.",
                "StartupFunctions.cpp",
                "Startup routine",
                new[] { "function definition", "function call", "return type", "reuse" }
            ),
            new CppRoadmapModule(
                18,
                "Chapter 3 · Functions and Collections",
                "Parameters",
                "Send Settings into a Function",
                "Pass operating values into reusable engine routines.",
                "Allow startup to receive target RPM and temperature.",
                "StartupFunctions.cpp",
                "Startup settings",
                new[] { "parameters", "arguments", "type matching", "function inputs" }
            ),
            new CppRoadmapModule(
                19,
                "Chapter 3 · Functions and Collections",
                "Return values",
                "Return a Calculated Efficiency",
                "Send a result back from a function to its caller.",
                "Display live efficiency from a dedicated routine.",
                "Efficiency.cpp",
                "Efficiency monitor",
                new[] { "return", "result values", "function output", "composition" }
            ),
            new CppRoadmapModule(
                20,
                "Chapter 3 · Functions and Collections",
                "Overloading",
                "Support Multiple Sensor Formats",
                "Use one function name with different parameter lists.",
                "Accept whole-number and decimal sensor readings.",
                "SensorFormatting.cpp",
                "Sensor formatting",
                new[] { "function overloading", "signatures", "type selection", "compile-time choice" }
            ),
            new CppRoadmapModule(
                21,
                "Chapter 3 · Functions and Collections",
                "Arrays",
                "Store a Fixed Sensor Bank",
                "Keep a known group of sensor readings in an array.",
                "Connect eight fixed temperature probes to the dashboard.",
                "SensorBank.cpp",
                "Sensor bank",
                new[] { "std::array", "indices", "fixed size", "iteration" }
            ),
            new CppRoadmapModule(
                22,
                "Chapter 3 · Functions and Collections",
                "Vectors",
                "Grow the Maintenance Queue",
                "Store a changing collection with a standard vector.",
                "Add and remove maintenance jobs during operation.",
                "MaintenanceQueue.cpp",
                "Maintenance queue",
                new[] { "std::vector", "push_back", "size", "dynamic collections" }
            ),
            new CppRoadmapModule(
                23,
                "Chapter 3 · Functions and Collections",
                "References and const",
                "Share Data Without Unnecessary Copies",
                "Use references and const to pass information efficiently and safely.",
                "Let monitoring systems inspect state without changing it.",
                "EngineTelemetry.cpp",
                "Telemetry",
                new[] { "references", "const", "pass by reference", "read-only access" }
            ),
            new CppRoadmapModule(
                24,
                "Chapter 3 · Functions and Collections",
                "Integrated collections",
                "Build the Foundry Resource Manager",
                "Combine functions and collections into a practical resource system.",
                "Track fuel cells, spare parts, sensors, and maintenance work.",
                "ResourceManager.cpp",
                "Resource manager",
                new[] { "functions", "vectors", "arrays", "data processing" }
            ),
            new CppRoadmapModule(
                25,
                "Chapter 4 · Objects and Memory",
                "Structs",
                "Group Sensor Data into a Struct",
                "Package related values into one meaningful data type.",
                "Represent each sensor with a name, reading, and alarm state.",
                "SensorRecord.cpp",
                "Sensor records",
                new[] { "struct", "members", "custom types", "grouped data" }
            ),
            new CppRoadmapModule(
                26,
                "Chapter 4 · Objects and Memory",
                "Classes",
                "Design an Engine Component Class",
                "Combine state and behavior inside a reusable class.",
                "Create the first reusable component in the foundry.",
                "EngineComponent.cpp",
                "Component model",
                new[] { "class", "objects", "methods", "member variables" }
            ),
            new CppRoadmapModule(
                27,
                "Chapter 4 · Objects and Memory",
                "Constructors",
                "Initialize Components Safely",
                "Use constructors to create valid objects from the beginning.",
                "Require every component to start with an ID, name, and capacity.",
                "EngineComponent.cpp",
                "Component initialization",
                new[] { "constructors", "initializer lists", "object creation", "valid state" }
            ),
            new CppRoadmapModule(
                28,
                "Chapter 4 · Objects and Memory",
                "Encapsulation",
                "Protect Internal Engine State",
                "Control how outside code reads and changes component data.",
                "Prevent unsafe direct changes to RPM and temperature.",
                "ProtectedComponent.cpp",
                "State protection",
                new[] { "private", "public", "getters", "controlled mutation" }
            ),
            new CppRoadmapModule(
                29,
                "Chapter 4 · Objects and Memory",
                "Inheritance",
                "Create Specialized Component Types",
                "Build specialized machines from a shared component foundation.",
                "Derive turbines, pumps, and generators from one base type.",
                "ComponentTypes.cpp",
                "Component hierarchy",
                new[] { "inheritance", "base class", "derived class", "shared behavior" }
            ),
            new CppRoadmapModule(
                30,
                "Chapter 4 · Objects and Memory",
                "Polymorphism",
                "Update Components Through One Interface",
                "Use virtual behavior to control different machine types uniformly.",
                "Run every foundry component through one update pipeline.",
                "ComponentPipeline.cpp",
                "Update pipeline",
                new[] { "virtual", "override", "base references", "runtime dispatch" }
            ),
            new CppRoadmapModule(
                31,
                "Chapter 4 · Objects and Memory",
                "Pointers",
                "Understand Addresses and Pointers",
                "Explore memory addresses and pointer-based access safely.",
                "Inspect the foundry memory map and component addresses.",
                "MemoryMap.cpp",
                "Memory map",
                new[] { "address-of", "pointer", "dereference", "null pointer" }
            ),
            new CppRoadmapModule(
                32,
                "Chapter 4 · Objects and Memory",
                "Smart pointers",
                "Manage Component Ownership",
                "Use smart pointers to represent ownership and automatic cleanup.",
                "Complete a safe dynamic component registry.",
                "ComponentRegistry.cpp",
                "Ownership registry",
                new[] { "std::unique_ptr", "std::shared_ptr", "ownership", "RAII" }
            ),
            new CppRoadmapModule(
                33,
                "Chapter 5 · Real-Time Engine Systems",
                "Headers and source files",
                "Split the Engine into Maintainable Files",
                "Separate declarations from implementation across a real project.",
                "Organize the foundry into engine, component, and simulation files.",
                "EngineCore.h / EngineCore.cpp",
                "Project structure",
                new[] { "header files", "source files", "include guards", "translation units" }
            ),
            new CppRoadmapModule(
                34,
                "Chapter 5 · Real-Time Engine Systems",
                "Components",
                "Assemble an Entity Component System",
                "Compose game and simulation entities from focused components.",
                "Create configurable machines from reusable behavior blocks.",
                "EntitySystem.cpp",
                "Entity system",
                new[] { "composition", "entities", "components", "system processing" }
            ),
            new CppRoadmapModule(
                35,
                "Chapter 5 · Real-Time Engine Systems",
                "Timing",
                "Measure Frame Time",
                "Track elapsed time so the simulation updates consistently.",
                "Add a frame clock and performance monitor.",
                "FrameClock.cpp",
                "Frame clock",
                new[] { "std::chrono", "elapsed time", "delta time", "frame rate" }
            ),
            new CppRoadmapModule(
                36,
                "Chapter 5 · Real-Time Engine Systems",
                "Game loops",
                "Run the Real-Time Engine Loop",
                "Coordinate input, updates, and rendering in a continuous loop.",
                "Bring the Engine Foundry simulation fully online.",
                "EngineLoop.cpp",
                "Engine loop",
                new[] { "game loop", "input", "update", "render" }
            ),
            new CppRoadmapModule(
                37,
                "Chapter 5 · Real-Time Engine Systems",
                "Events",
                "Send Events Between Systems",
                "Decouple systems with messages and event handlers.",
                "Broadcast alarms, shutdowns, repairs, and operator commands.",
                "EventBus.cpp",
                "Event bus",
                new[] { "events", "callbacks", "subscribers", "decoupling" }
            ),
            new CppRoadmapModule(
                38,
                "Chapter 5 · Real-Time Engine Systems",
                "Simulation",
                "Update Motion and Machine Physics",
                "Apply time-based updates to moving and rotating systems.",
                "Animate turbines, conveyors, and cooling flow.",
                "FoundryPhysics.cpp",
                "Simulation physics",
                new[] { "velocity", "position updates", "delta time", "simulation state" }
            ),
            new CppRoadmapModule(
                39,
                "Chapter 5 · Real-Time Engine Systems",
                "Persistence",
                "Save and Restore the Foundry",
                "Write engine state to storage and rebuild it later.",
                "Preserve machine configuration and simulation progress.",
                "FoundrySave.cpp",
                "Save system",
                new[] { "file streams", "serialization", "loading", "error handling" }
            ),
            new CppRoadmapModule(
                40,
                "Chapter 5 · Real-Time Engine Systems",
                "Final integration",
                "Launch the Complete Engine Foundry",
                "Integrate every major system into one maintainable C++ simulation.",
                "Finish the full real-time Engine Foundry project.",
                "EngineFoundry.cpp",
                "Full foundry",
                new[] { "architecture", "integration", "testing", "performance" }
            )
        };

    public static CppRoadmapModule Get(int moduleIndex)
    {
        if (moduleIndex < 0 || moduleIndex >= Modules.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(moduleIndex),
                moduleIndex,
                "The C++ roadmap module index is invalid.");
        }

        return Modules[moduleIndex];
    }
}
