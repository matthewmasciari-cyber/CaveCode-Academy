namespace CaveCode.CourseEngine;

public static class JavaScriptCourseLessons
{
    public const int PlayableModuleCount = 40;

    public static IReadOnlyList<CourseLesson> All { get; } =
        new[]
        {
            new CourseLesson(
                "Chapter 1 · Forge Boot-Up",
                "console.log",
                "Light the Console",
                "JavaScript talks through the console. console.log prints so you can see what the forge is thinking.",
                "console.log(\"Forge online\");",
                "console.log(\"Forge online\");",
                "console.___(\"Forge online\");",
                "What does console.log do?",
                new[] { "Delete a file", "Print a message to the console", "Create an HTML tag", "Style a button" },
                1,
                "console.log writes a value out for you to inspect.",
                "Console.log(\"Forge online\");",
                "Use console.log lowercase.",
                "Log the text Forge online.",
                "Log the text Web Forge ready.",
                "console.log(\"Web Forge ready\");",
                "Console lamp is on."
            )
            {
                ConceptPoints = new[] { "console.log prints messages.", "Quotes make a string.", "Semicolons end statements here." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 1 · Forge Boot-Up",
                "Strings",
                "Name the Arcade",
                "Text is a string. Wrap it in quotes: \"Neon Arcade\".",
                "console.log(\"Neon Arcade\");",
                "console.log(\"Neon Arcade\");",
                "console.log(___);",
                "Which is a string?",
                new[] { "Neon Arcade", "\"Neon Arcade\"", "console", "log" },
                1,
                "Quotes mark a string value.",
                "console.log(Neon Arcade);",
                "Strings need quotes.",
                "Log Neon Arcade.",
                "Log Pixel Lab.",
                "console.log(\"Pixel Lab\");",
                "Arcade nameplate installed."
            )
            {
                ConceptPoints = new[] { "Strings hold text.", "Quotes delimit strings.", "console.log can print strings." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 1 · Forge Boot-Up",
                "Variables let",
                "Stash a Score",
                "let score = 0; creates a changeable box for points.",
                "let score = 0;\nconsole.log(score);",
                "let score = 0;\nconsole.log(score);",
                "___ score = 0;\nconsole.log(score);",
                "Which keyword starts a changeable variable?",
                new[] { "var only", "let", "def", "pinMode" },
                1,
                "let declares a variable you can reassign.",
                "Let score = 0;",
                "let is lowercase.",
                "Declare score 0 and log it.",
                "Declare lives 3 and log it.",
                "let lives = 3;\nconsole.log(lives);",
                "Score register online."
            )
            {
                ConceptPoints = new[] { "let declares variables.", "Names should be clear.", "Log to verify." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 1 · Forge Boot-Up",
                "const",
                "Lock the Title",
                "const title = \"Web Forge\"; for values that should not be reassigned.",
                "const title = \"Web Forge\";\nconsole.log(title);",
                "const title = \"Web Forge\";\nconsole.log(title);",
                "___ title = \"Web Forge\";\nconsole.log(title);",
                "const is best when?",
                new[] { "Value changes every frame", "Binding should stay fixed", "You need GPIO", "You hate strings" },
                1,
                "const prevents reassignment of that binding.",
                "const title = Web Forge;",
                "Strings still need quotes.",
                "const title Web Forge and log.",
                "const mode Easy and log.",
                "const mode = \"Easy\";\nconsole.log(mode);",
                "Title locked on the cabinet."
            )
            {
                ConceptPoints = new[] { "const for fixed bindings.", "Quotes for text.", "Prefer const until you need change." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 1 · Forge Boot-Up",
                "Numbers",
                "Add the Combo",
                "Numbers need no quotes. score = score + 10; bumps points.",
                "let score = 0;\nscore = score + 10;\nconsole.log(score);",
                "let score = 0;\nscore = score + 10;\nconsole.log(score);",
                "let score = 0;\nscore = score ___ 10;\nconsole.log(score);",
                "After adding 10 from 0, score is?",
                new[] { "0", "10", "\"10\"", "undefined" },
                1,
                "Numeric addition updates the number.",
                "let score = 0;\nscore = score + 10\nconsole.log(score);",
                "Semicolon after assignment.",
                "score 0, add 10, log.",
                "score 5, add 5, log.",
                "let score = 5;\nscore = score + 5;\nconsole.log(score);",
                "Combo counter ticks up."
            )
            {
                ConceptPoints = new[] { "Numbers are unquoted.", "+ adds numbers.", "Reassign to update." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 1 · Forge Boot-Up",
                "Booleans",
                "Power Switch",
                "true and false are booleans — on/off values.",
                "let powered = true;\nconsole.log(powered);",
                "let powered = true;\nconsole.log(powered);",
                "let powered = ___;\nconsole.log(powered);",
                "Boolean values are?",
                new[] { "yes and no", "true and false", "1 and 2 only", "on and off only" },
                1,
                "Booleans are true/false.",
                "let powered = True;",
                "Lowercase true in JS.",
                "powered true and log.",
                "powered false and log.",
                "let powered = false;\nconsole.log(powered);",
                "Power flag is live."
            )
            {
                ConceptPoints = new[] { "Booleans are true/false.", "They drive if decisions.", "No quotes around true/false." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 1 · Forge Boot-Up",
                "Comments",
                "Leave a Sticky Note",
                "// starts a single-line comment the engine ignores.",
                "// Player score starts at zero\nlet score = 0;",
                "// Player score starts at zero\nlet score = 0;",
                "___ Player score starts at zero\nlet score = 0;",
                "Single-line JS comment marker?",
                new[] { "#", "//", "<!-- only", "rem" },
                1,
                "// begins a JS line comment.",
                "# Player score\nlet score = 0;",
                "JS uses // not #.",
                "Comment then let score = 0.",
                "Comment High score then let best = 100.",
                "// High score\nlet best = 100;",
                "Sticky note applied."
            )
            {
                ConceptPoints = new[] { "// comments ignored.", "Explain briefly.", "Keep notes near code." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 1 · Forge Boot-Up",
                "Chapter review",
                "Boot Sequence Complete",
                "Chapter 1: console.log, strings, let, const, numbers, booleans, comments.",
                "const title = \"Web Forge\";\nlet score = 0;\nscore = score + 1;\nconsole.log(title);\nconsole.log(score);",
                "const title = \"Web Forge\";\nlet score = 0;\nscore = score + 1;\nconsole.log(title);\nconsole.log(score);",
                "const title = \"Web Forge\";\nlet score = 0;\nscore = score + 1;\nconsole.___(title);\nconsole.log(score);",
                "Changeable vs fixed?",
                new[] { "let and const", "log and print", "true and 1", "html and css" },
                0,
                "let can change; const should not be reassigned.",
                "const title = \"Web Forge\"\nlet score = 0;",
                "Need semicolons in this lab.",
                "title const Web Forge, score 0, +1, log both.",
                "title const Arcade, score 10, +5, log both.",
                "const title = \"Arcade\";\nlet score = 10;\nscore = score + 5;\nconsole.log(title);\nconsole.log(score);",
                "Chapter 1 complete."
            )
            {
                ConceptPoints = new[] { "Boot kit values and output.", "let vs const.", "Next: decisions." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 2 · Values and Decisions",
                "if true",
                "if true",
                "Run a branch when a condition is true.",
                "let powered = true;\nif (powered) {\n  console.log(\"Humming\");\n}",
                "let powered = true;\nif (powered) {\n  console.log(\"Humming\");\n}",
                "let powered = true;\n___ (powered) {\n  console.log(\"Humming\");\n}",
                "What does if control?",
                new[] { "CSS only", "Whether a block runs", "File uploads", "GPU clocks" },
                1,
                "if decides whether its block runs.",
                "let powered = true\nif (powered) {\n  console.log(\"Humming\");\n}",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let powered = true;\nif (powered) {\n  console.log(\"Humming\");\n}",
                "Lab: if true."
            )
            {
                ConceptPoints = new[] { "if gates optional behavior.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 2 · Values and Decisions",
                "else",
                "else",
                "Backup branch when the condition is false.",
                "let powered = false;\nif (powered) {\n  console.log(\"Humming\");\n} else {\n  console.log(\"Silent\");\n}",
                "let powered = false;\nif (powered) {\n  console.log(\"Humming\");\n} else {\n  console.log(\"Silent\");\n}",
                "let powered = false;\n___ (powered) {\n  console.log(\"Humming\");\n} else {\n  console.log(\"Silent\");\n}",
                "else runs when?",
                new[] { "Always", "The if condition is false", "Never", "Only on click" },
                1,
                "else is the false path.",
                "let powered = false\nif (powered) {\n  console.log(\"Humming\");\n} else {\n  console.log(\"Silent\");\n}",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let powered = false;\nif (powered) {\n  console.log(\"Humming\");\n} else {\n  console.log(\"Silent\");\n}",
                "Lab: else."
            )
            {
                ConceptPoints = new[] { "else covers the other path.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 2 · Values and Decisions",
                "Comparison",
                "Comparison",
                "Use === for strict equality.",
                "let score = 10;\nif (score === 10) {\n  console.log(\"Perfect\");\n}",
                "let score = 10;\nif (score === 10) {\n  console.log(\"Perfect\");\n}",
                "let score = 10;\n___ (score === 10) {\n  console.log(\"Perfect\");\n}",
                "=== tests?",
                new[] { "Assignment", "Equality", "Import", "Sleep" },
                1,
                "=== is strict equality.",
                "let score = 10\nif (score = 10) {\n  console.log(\"Perfect\");\n}",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let score = 10;\nif (score === 10) {\n  console.log(\"Perfect\");\n}",
                "Lab: Comparison."
            )
            {
                ConceptPoints = new[] { "=== compares without loose surprises.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 2 · Values and Decisions",
                "Greater than",
                "Greater than",
                "Bonus when score beats a threshold.",
                "let score = 12;\nif (score > 10) {\n  console.log(\"Bonus\");\n}",
                "let score = 12;\nif (score > 10) {\n  console.log(\"Bonus\");\n}",
                "let score = 12;\n___ (score > 10) {\n  console.log(\"Bonus\");\n}",
                "score > 10 is true when?",
                new[] { "score is 10", "score is greater than 10", "score is 0", "always" },
                1,
                "> means greater than.",
                "let score = 12\nif (score > 10) {\n  console.log(\"Bonus\");\n}",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let score = 12;\nif (score > 10) {\n  console.log(\"Bonus\");\n}",
                "Lab: Greater than."
            )
            {
                ConceptPoints = new[] { "Comparisons power gates.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 2 · Values and Decisions",
                "else if",
                "else if",
                "Chain score ranks.",
                "let score = 7;\nif (score >= 10) {\n  console.log(\"Gold\");\n} else if (score >= 5) {\n  console.log(\"Silver\");\n} else {\n  console.log(\"Bronze\");\n}",
                "let score = 7;\nif (score >= 10) {\n  console.log(\"Gold\");\n} else if (score >= 5) {\n  console.log(\"Silver\");\n} else {\n  console.log(\"Bronze\");\n}",
                "let score = 7;\n___ (score >= 10) {\n  console.log(\"Gold\");\n} else if (score >= 5) {\n  console.log(\"Silver\");\n} else {\n  console.log(\"Bronze\");\n}",
                "else if is for?",
                new[] { "Ignoring scores", "Extra conditions in a chain", "CSS", "DOM only" },
                1,
                "else if adds more branches.",
                "let score = 7\nif (score >= 10) {\n  console.log(\"Gold\");\n} else if (score >= 5) {\n  console.log(\"Silver\");\n} else {\n  console.log(\"Bronze\");\n}",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let score = 7;\nif (score >= 10) {\n  console.log(\"Gold\");\n} else if (score >= 5) {\n  console.log(\"Silver\");\n} else {\n  console.log(\"Bronze\");\n}",
                "Lab: else if."
            )
            {
                ConceptPoints = new[] { "else if handles middle cases.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 2 · Values and Decisions",
                "Logical and",
                "Logical and",
                "Both sides true with &&.",
                "let powered = true;\nlet credits = 1;\nif (powered && credits > 0) {\n  console.log(\"Play\");\n}",
                "let powered = true;\nlet credits = 1;\nif (powered && credits > 0) {\n  console.log(\"Play\");\n}",
                "let powered = true;\nlet credits = 1;\n___ (powered && credits > 0) {\n  console.log(\"Play\");\n}",
                "&& means?",
                new[] { "Either", "Both must be true", "Neither", "XOR" },
                1,
                "Both sides of && must be true.",
                "let powered = true\nlet credits = 1;\nif (powered && credits > 0) {\n  console.log(\"Play\");\n}",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let powered = true;\nlet credits = 1;\nif (powered && credits > 0) {\n  console.log(\"Play\");\n}",
                "Lab: Logical and."
            )
            {
                ConceptPoints = new[] { "&& requires both.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 2 · Values and Decisions",
                "Logical or",
                "Logical or",
                "Either path with ||.",
                "let hasCoin = false;\nlet hasCard = true;\nif (hasCoin || hasCard) {\n  console.log(\"Accepted\");\n}",
                "let hasCoin = false;\nlet hasCard = true;\nif (hasCoin || hasCard) {\n  console.log(\"Accepted\");\n}",
                "let hasCoin = false;\nlet hasCard = true;\n___ (hasCoin || hasCard) {\n  console.log(\"Accepted\");\n}",
                "|| means?",
                new[] { "Both required", "Either may be true", "Always false", "Assignment" },
                1,
                "Either side of || can pass.",
                "let hasCoin = false\nlet hasCard = true;\nif (hasCoin || hasCard) {\n  console.log(\"Accepted\");\n}",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let hasCoin = false;\nlet hasCard = true;\nif (hasCoin || hasCard) {\n  console.log(\"Accepted\");\n}",
                "Lab: Logical or."
            )
            {
                ConceptPoints = new[] { "|| allows either.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 2 · Values and Decisions",
                "Decision lab",
                "Decision lab",
                "If score >= 5 log Win else Try again.",
                "let score = 5;\nif (score >= 5) {\n  console.log(\"Win\");\n} else {\n  console.log(\"Try again\");\n}",
                "let score = 5;\nif (score >= 5) {\n  console.log(\"Win\");\n} else {\n  console.log(\"Try again\");\n}",
                "let score = 5;\n___ (score >= 5) {\n  console.log(\"Win\");\n} else {\n  console.log(\"Try again\");\n}",
                ">= means?",
                new[] { "Less only", "Greater or equal", "Not equal", "Divide" },
                1,
                ">= is greater than or equal.",
                "let score = 5\nif (score >= 5) {\n  console.log(\"Win\");\n} else {\n  console.log(\"Try again\");\n}",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let score = 5;\nif (score >= 5) {\n  console.log(\"Win\");\n} else {\n  console.log(\"Try again\");\n}",
                "Lab: Decision lab."
            )
            {
                ConceptPoints = new[] { "Decisions complete chapter 2.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Events",
                "Function declare",
                "Function declare",
                "function spark() packages reusable action.",
                "function spark() {\n  console.log(\"Zap\");\n}\nspark();",
                "function spark() {\n  console.log(\"Zap\");\n}\nspark();",
                "___ spark() {\n  console.log(\"Zap\");\n}\nspark();",
                "A function is for?",
                new[] { "CSS only", "Reusable behavior", "Replacing HTML", "DNS" },
                1,
                "Functions name reusable blocks.",
                "function spark() {\n  console.log(\"Zap\")\n}\nspark();",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "function spark() {\n  console.log(\"Zap\");\n}\nspark();",
                "Lab: Function declare."
            )
            {
                ConceptPoints = new[] { "Functions group steps.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Events",
                "Parameters",
                "Parameters",
                "Pass a name into the function.",
                "function greet(name) {\n  console.log(\"Hi \" + name);\n}\ngreet(\"Pilot\");",
                "function greet(name) {\n  console.log(\"Hi \" + name);\n}\ngreet(\"Pilot\");",
                "___ greet(name) {\n  console.log(\"Hi \" + name);\n}\ngreet(\"Pilot\");",
                "Parameters are?",
                new[] { "CSS classes", "Inputs to a function", "HTML tags", "Servers" },
                1,
                "Parameters accept arguments.",
                "function greet(name) {\n  console.log(\"Hi \" + name)\n}\ngreet(\"Pilot\");",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "function greet(name) {\n  console.log(\"Hi \" + name);\n}\ngreet(\"Pilot\");",
                "Lab: Parameters."
            )
            {
                ConceptPoints = new[] { "Parameters receive input.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Events",
                "Return",
                "Return",
                "Send a value back to the caller.",
                "function double(n) {\n  return n * 2;\n}\nconsole.log(double(4));",
                "function double(n) {\n  return n * 2;\n}\nconsole.log(double(4));",
                "___ double(n) {\n  return n * 2;\n}\nconsole.log(double(4));",
                "return does what?",
                new[] { "Deletes n", "Sends a value back", "Opens a port", "Skips JS" },
                1,
                "return provides the result.",
                "function double(n) {\n  return n * 2\n}\nconsole.log(double(4));",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "function double(n) {\n  return n * 2;\n}\nconsole.log(double(4));",
                "Lab: Return."
            )
            {
                ConceptPoints = new[] { "return hands results out.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Events",
                "Arrow feel",
                "Arrow feel",
                "const ping = () => { ... } short style.",
                "const ping = () => {\n  console.log(\"Ping\");\n};\nping();",
                "const ping = () => {\n  console.log(\"Ping\");\n};\nping();",
                "const ping = () => {\n  console.___(\"Ping\");\n};\nping();",
                "Arrow functions use?",
                new[] { "=>", "==>", "-->", "<-" },
                0,
                "=> marks an arrow function.",
                "const ping = () => {\n  console.log(\"Ping\")\n};\nping();",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "const ping = () => {\n  console.log(\"Ping\");\n};\nping();",
                "Lab: Arrow feel."
            )
            {
                ConceptPoints = new[] { "Arrows are common modern style.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Events",
                "Event idea",
                "Event idea",
                "Functions shine as event handlers.",
                "function onStart() {\n  console.log(\"Round start\");\n}\nonStart();",
                "function onStart() {\n  console.log(\"Round start\");\n}\nonStart();",
                "___ onStart() {\n  console.log(\"Round start\");\n}\nonStart();",
                "Why name onStart?",
                new[] { "Random", "Describes the event role", "Required by CPU", "CSS" },
                1,
                "Handler names document intent.",
                "function onStart() {\n  console.log(\"Round start\")\n}\nonStart();",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "function onStart() {\n  console.log(\"Round start\");\n}\nonStart();",
                "Lab: Event idea."
            )
            {
                ConceptPoints = new[] { "Handlers wait for action.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Events",
                "Multiple calls",
                "Multiple calls",
                "Call spark twice.",
                "function spark() {\n  console.log(\"Zap\");\n}\nspark();\nspark();",
                "function spark() {\n  console.log(\"Zap\");\n}\nspark();\nspark();",
                "___ spark() {\n  console.log(\"Zap\");\n}\nspark();\nspark();",
                "Calling twice means?",
                new[] { "One run", "Two runs", "Zero runs", "Compile error always" },
                1,
                "Each call runs the body.",
                "function spark() {\n  console.log(\"Zap\")\n}\nspark();\nspark();",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "function spark() {\n  console.log(\"Zap\");\n}\nspark();\nspark();",
                "Lab: Multiple calls."
            )
            {
                ConceptPoints = new[] { "Reuse beats copy-paste.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Events",
                "Score helper",
                "Score helper",
                "addScore returns new total.",
                "function addScore(score, points) {\n  return score + points;\n}\nconsole.log(addScore(3, 2));",
                "function addScore(score, points) {\n  return score + points;\n}\nconsole.log(addScore(3, 2));",
                "___ addScore(score, points) {\n  return score + points;\n}\nconsole.log(addScore(3, 2));",
                "addScore(3, 2) returns?",
                new[] { "32", "5", "3", "2" },
                1,
                "3 + 2 is 5.",
                "function addScore(score, points) {\n  return score + points\n}\nconsole.log(addScore(3, 2));",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "function addScore(score, points) {\n  return score + points;\n}\nconsole.log(addScore(3, 2));",
                "Lab: Score helper."
            )
            {
                ConceptPoints = new[] { "Helpers tidy game math.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 3 · Functions and Events",
                "Function lab",
                "Function lab",
                "buzz logs Buzz and is called.",
                "function buzz() {\n  console.log(\"Buzz\");\n}\nbuzz();",
                "function buzz() {\n  console.log(\"Buzz\");\n}\nbuzz();",
                "___ buzz() {\n  console.log(\"Buzz\");\n}\nbuzz();",
                "How do you run buzz?",
                new[] { "buzz;", "buzz()", "call buzz", "run.buzz" },
                1,
                "Call with buzz().",
                "function buzz() {\n  console.log(\"Buzz\")\n}\nbuzz();",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "function buzz() {\n  console.log(\"Buzz\");\n}\nbuzz();",
                "Lab: Function lab."
            )
            {
                ConceptPoints = new[] { "Chapter 3 complete.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 4 · DOM Arcade",
                "getElementById",
                "getElementById",
                "Find a page element by id.",
                "const panel = document.getElementById(\"status\");\nconsole.log(panel);",
                "const panel = document.getElementById(\"status\");\nconsole.log(panel);",
                "const panel = document.___(\"status\");\nconsole.log(panel);",
                "getElementById uses?",
                new[] { "Class only", "id attribute", "File name", "Port" },
                1,
                "It matches the id attribute.",
                "const panel = document.getElementById(\"status\")\nconsole.log(panel);",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "const panel = document.getElementById(\"status\");\nconsole.log(panel);",
                "Lab: getElementById."
            )
            {
                ConceptPoints = new[] { "ids locate nodes.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 4 · DOM Arcade",
                "textContent",
                "textContent",
                "Change text inside an element.",
                "const panel = document.getElementById(\"status\");\npanel.textContent = \"Ready\";",
                "const panel = document.getElementById(\"status\");\npanel.textContent = \"Ready\";",
                "const panel = document.___(\"status\");\npanel.textContent = \"Ready\";",
                "textContent changes?",
                new[] { "The visible text", "Only CSS", "The URL", "RAM only" },
                0,
                "It sets the element's text.",
                "const panel = document.getElementById(\"status\")\npanel.textContent = \"Ready\";",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "const panel = document.getElementById(\"status\");\npanel.textContent = \"Ready\";",
                "Lab: textContent."
            )
            {
                ConceptPoints = new[] { "textContent updates labels.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 4 · DOM Arcade",
                "click listener",
                "click listener",
                "Run code on click.",
                "const btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  console.log(\"Clicked\");\n});",
                "const btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  console.log(\"Clicked\");\n});",
                "const btn = document.___(\"start\");\nbtn.addEventListener(\"click\", () => {\n  console.log(\"Clicked\");\n});",
                "addEventListener listens for?",
                new[] { "Compiles", "User or system events", "Only servers", "CSS files" },
                1,
                "It registers event handlers.",
                "const btn = document.getElementById(\"start\")\nbtn.addEventListener(\"click\", () => {\n  console.log(\"Clicked\");\n});",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "const btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  console.log(\"Clicked\");\n});",
                "Lab: click listener."
            )
            {
                ConceptPoints = new[] { "Events connect UI to logic.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 4 · DOM Arcade",
                "Toggle text",
                "Toggle text",
                "Click sets status to GO.",
                "const btn = document.getElementById(\"start\");\nconst panel = document.getElementById(\"status\");\nbtn.addEventListener(\"click\", () => {\n  panel.textContent = \"GO\";\n});",
                "const btn = document.getElementById(\"start\");\nconst panel = document.getElementById(\"status\");\nbtn.addEventListener(\"click\", () => {\n  panel.textContent = \"GO\";\n});",
                "const btn = document.___(\"start\");\nconst panel = document.getElementById(\"status\");\nbtn.addEventListener(\"click\", () => {\n  panel.textContent = \"GO\";\n});",
                "This pattern needs?",
                new[] { "A button and a handler", "Only Python", "A database", "GPU" },
                0,
                "Select nodes then listen.",
                "const btn = document.getElementById(\"start\")\nconst panel = document.getElementById(\"status\");\nbtn.addEventListener(\"click\", () => {\n  panel.textContent = \"GO\";\n});",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "const btn = document.getElementById(\"start\");\nconst panel = document.getElementById(\"status\");\nbtn.addEventListener(\"click\", () => {\n  panel.textContent = \"GO\";\n});",
                "Lab: Toggle text."
            )
            {
                ConceptPoints = new[] { "Clicks rewrite the HUD.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 4 · DOM Arcade",
                "classList add",
                "classList add",
                "Add a CSS class from JS.",
                "const panel = document.getElementById(\"status\");\npanel.classList.add(\"lit\");",
                "const panel = document.getElementById(\"status\");\npanel.classList.add(\"lit\");",
                "const panel = document.___(\"status\");\npanel.classList.add(\"lit\");",
                "classList.add does?",
                new[] { "Removes HTML", "Adds a CSS class name", "Opens Wi-Fi", "Stops JS" },
                1,
                "It adds a class to the element.",
                "const panel = document.getElementById(\"status\")\npanel.classList.add(\"lit\");",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "const panel = document.getElementById(\"status\");\npanel.classList.add(\"lit\");",
                "Lab: classList add."
            )
            {
                ConceptPoints = new[] { "classList bridges JS and CSS.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 4 · DOM Arcade",
                "Create element",
                "Create element",
                "Create and attach a new tag.",
                "const chip = document.createElement(\"span\");\nchip.textContent = \"+1\";\ndocument.body.appendChild(chip);",
                "const chip = document.createElement(\"span\");\nchip.textContent = \"+1\";\ndocument.body.appendChild(chip);",
                "const chip = document.createElement(\"span\");\nchip.textContent = \"+1\";\ndocument.body.appendChild(chip);",
                "createElement makes?",
                new[] { "A new DOM node", "A zip file", "A thread", "A CSS file only" },
                0,
                "It constructs an element.",
                "const chip = document.createElement(\"span\")\nchip.textContent = \"+1\";\ndocument.body.appendChild(chip);",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "const chip = document.createElement(\"span\");\nchip.textContent = \"+1\";\ndocument.body.appendChild(chip);",
                "Lab: Create element."
            )
            {
                ConceptPoints = new[] { "Scripts can spawn UI.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 4 · DOM Arcade",
                "querySelector",
                "querySelector",
                "Select with a CSS-style query.",
                "const panel = document.querySelector(\"#status\");\npanel.textContent = \"Online\";",
                "const panel = document.querySelector(\"#status\");\npanel.textContent = \"Online\";",
                "const panel = document.querySelector(\"#status\");\npanel.textContent = \"Online\";",
                "#status means?",
                new[] { "A class", "An id selector", "A tag must", "A server" },
                1,
                "# targets an id.",
                "const panel = document.querySelector(\"#status\")\npanel.textContent = \"Online\";",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "const panel = document.querySelector(\"#status\");\npanel.textContent = \"Online\";",
                "Lab: querySelector."
            )
            {
                ConceptPoints = new[] { "querySelector is flexible.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 4 · DOM Arcade",
                "DOM lab",
                "DOM lab",
                "Click start sets status to Live.",
                "const btn = document.getElementById(\"start\");\nconst panel = document.getElementById(\"status\");\nbtn.addEventListener(\"click\", () => {\n  panel.textContent = \"Live\";\n});",
                "const btn = document.getElementById(\"start\");\nconst panel = document.getElementById(\"status\");\nbtn.addEventListener(\"click\", () => {\n  panel.textContent = \"Live\";\n});",
                "const btn = document.___(\"start\");\nconst panel = document.getElementById(\"status\");\nbtn.addEventListener(\"click\", () => {\n  panel.textContent = \"Live\";\n});",
                "Which pairs find + update?",
                new[] { "getElementById and textContent", "print and input", "sleep and delay", "pinMode and HIGH" },
                0,
                "Find the node then set text.",
                "const btn = document.getElementById(\"start\")\nconst panel = document.getElementById(\"status\");\nbtn.addEventListener(\"click\", () => {\n  panel.textContent = \"Live\";\n});",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "const btn = document.getElementById(\"start\");\nconst panel = document.getElementById(\"status\");\nbtn.addEventListener(\"click\", () => {\n  panel.textContent = \"Live\";\n});",
                "Lab: DOM lab."
            )
            {
                ConceptPoints = new[] { "DOM arcade skills unlocked.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 5 · Mini Games and Polish",
                "Click counter",
                "Click counter",
                "Each click adds 1 to score and shows it.",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  panel.textContent = \"Score \" + score;\n});",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  panel.textContent = \"Score \" + score;\n});",
                "let ___ = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  panel.textContent = \"Score \" + score;\n});",
                "score = score + 1 does?",
                new[] { "Resets score", "Increments score", "Deletes score", "Logs HTML" },
                1,
                "It increases score by one.",
                "let score = 0\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  panel.textContent = \"Score \" + score;\n});",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  panel.textContent = \"Score \" + score;\n});",
                "Lab: Click counter."
            )
            {
                ConceptPoints = new[] { "State plus DOM seeds a game.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 5 · Mini Games and Polish",
                "Win at 5",
                "Win at 5",
                "At score 5 show You win.",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 5) {\n    panel.textContent = \"You win\";\n  } else {\n    panel.textContent = \"Score \" + score;\n  }\n});",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 5) {\n    panel.textContent = \"You win\";\n  } else {\n    panel.textContent = \"Score \" + score;\n  }\n});",
                "let ___ = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 5) {\n    panel.textContent = \"You win\";\n  } else {\n    panel.textContent = \"Score \" + score;\n  }\n});",
                "Win checks often use?",
                new[] { "Random CSS", "A threshold comparison", "FTP", "GPIO" },
                1,
                "Compare score to a target.",
                "let score = 0\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 5) {\n    panel.textContent = \"You win\";\n  } else {\n    panel.textContent = \"Score \" + score;\n  }\n});",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 5) {\n    panel.textContent = \"You win\";\n  } else {\n    panel.textContent = \"Score \" + score;\n  }\n});",
                "Lab: Win at 5."
            )
            {
                ConceptPoints = new[] { "Thresholds create win conditions.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 5 · Mini Games and Polish",
                "Disable button",
                "Disable button",
                "After enough clicks, disable the button.",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 3) {\n    btn.disabled = true;\n    panel.textContent = \"Done\";\n  }\n});",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 3) {\n    btn.disabled = true;\n    panel.textContent = \"Done\";\n  }\n});",
                "let ___ = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 3) {\n    btn.disabled = true;\n    panel.textContent = \"Done\";\n  }\n});",
                "btn.disabled = true means?",
                new[] { "Enable forever", "Stop accepting clicks", "Delete the button file", "Green CSS only" },
                1,
                "Disabled blocks further clicks.",
                "let score = 0\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 3) {\n    btn.disabled = true;\n    panel.textContent = \"Done\";\n  }\n});",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 3) {\n    btn.disabled = true;\n    panel.textContent = \"Done\";\n  }\n});",
                "Lab: Disable button."
            )
            {
                ConceptPoints = new[] { "disabled stops extra input.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 5 · Mini Games and Polish",
                "Reset idea",
                "Reset idea",
                "A function sets score back to 0.",
                "let score = 5;\nfunction reset() {\n  score = 0;\n}\nreset();\nconsole.log(score);",
                "let score = 5;\nfunction reset() {\n  score = 0;\n}\nreset();\nconsole.log(score);",
                "let ___ = 5;\nfunction reset() {\n  score = 0;\n}\nreset();\nconsole.log(score);",
                "After reset(), score is?",
                new[] { "5", "0", "undefined", "10" },
                1,
                "reset assigns 0.",
                "let score = 5\nfunction reset() {\n  score = 0;\n}\nreset();\nconsole.log(score);",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let score = 5;\nfunction reset() {\n  score = 0;\n}\nreset();\nconsole.log(score);",
                "Lab: Reset idea."
            )
            {
                ConceptPoints = new[] { "Resets reopen the round.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 5 · Mini Games and Polish",
                "Random flavor",
                "Random flavor",
                "Math.random() gives 0–1 variety.",
                "const roll = Math.random();\nconsole.log(roll);",
                "const roll = Math.random();\nconsole.log(roll);",
                "const roll = Math.random();\nconsole.___(roll);",
                "Math.random() returns?",
                new[] { "Always 1", "A float from 0 to 1", "An HTMLElement", "A string only" },
                1,
                "It returns a float in [0, 1).",
                "const roll = Math.random()\nconsole.log(roll);",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "const roll = Math.random();\nconsole.log(roll);",
                "Lab: Random flavor."
            )
            {
                ConceptPoints = new[] { "Random spices outcomes.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 5 · Mini Games and Polish",
                "Coin flip text",
                "Coin flip text",
                "Random branch Heads or Tails.",
                "if (Math.random() < 0.5) {\n  console.log(\"Heads\");\n} else {\n  console.log(\"Tails\");\n}",
                "if (Math.random() < 0.5) {\n  console.log(\"Heads\");\n} else {\n  console.log(\"Tails\");\n}",
                "if (Math.random() < 0.5) {\n  console.___(\"Heads\");\n} else {\n  console.log(\"Tails\");\n}",
                "This pattern demonstrates?",
                new[] { "Only networking", "Chance with if/else", "SQL joins", "CSS grid" },
                1,
                "Random + branch = chance.",
                "if (Math.random() < 0.5) {\n  console.log(\"Heads\")\n} else {\n  console.log(\"Tails\");\n}",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "if (Math.random() < 0.5) {\n  console.log(\"Heads\");\n} else {\n  console.log(\"Tails\");\n}",
                "Lab: Coin flip text."
            )
            {
                ConceptPoints = new[] { "Chance games need branches.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 5 · Mini Games and Polish",
                "HUD polish",
                "HUD polish",
                "Fixed title with live score on click.",
                "const title = \"Arcade\";\nlet score = 0;\nconst panel = document.getElementById(\"status\");\npanel.textContent = title;\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  panel.textContent = title + \" · \" + score;\n});",
                "const title = \"Arcade\";\nlet score = 0;\nconst panel = document.getElementById(\"status\");\npanel.textContent = title;\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  panel.textContent = title + \" · \" + score;\n});",
                "const title = \"Arcade\";\nlet ___ = 0;\nconst panel = document.getElementById(\"status\");\npanel.textContent = title;\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  panel.textContent = title + \" · \" + score;\n});",
                "title should be?",
                new[] { "Reassigned every click", "Often const", "A boolean", "A GPIO pin" },
                1,
                "Labels fit const well.",
                "const title = \"Arcade\"\nlet score = 0;\nconst panel = document.getElementById(\"status\");\npanel.textContent = title;\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  panel.textContent = title + \" · \" + score;\n});",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "const title = \"Arcade\";\nlet score = 0;\nconst panel = document.getElementById(\"status\");\npanel.textContent = title;\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  panel.textContent = title + \" · \" + score;\n});",
                "Lab: HUD polish."
            )
            {
                ConceptPoints = new[] { "Mix const labels with let scores.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            },
            new CourseLesson(
                "Chapter 5 · Mini Games and Polish",
                "Forge finale",
                "Forge finale",
                "At 10 clicks show Legend.",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 10) {\n    panel.textContent = \"Legend\";\n  } else {\n    panel.textContent = \"Score \" + score;\n  }\n});",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 10) {\n    panel.textContent = \"Legend\";\n  } else {\n    panel.textContent = \"Score \" + score;\n  }\n});",
                "let ___ = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 10) {\n    panel.textContent = \"Legend\";\n  } else {\n    panel.textContent = \"Score \" + score;\n  }\n});",
                "This finale combines?",
                new[] { "State, events, DOM, decisions", "Only SQL", "Only Python sleep", "Only pinMode" },
                0,
                "All chapter skills show up.",
                "let score = 0\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 10) {\n    panel.textContent = \"Legend\";\n  } else {\n    panel.textContent = \"Score \" + score;\n  }\n});",
                "Check operators and semicolons.",
                "Rebuild this example.",
                "Keep the same structure with a small label tweak if needed.",
                "let score = 0;\nconst panel = document.getElementById(\"status\");\nconst btn = document.getElementById(\"start\");\nbtn.addEventListener(\"click\", () => {\n  score = score + 1;\n  if (score >= 10) {\n    panel.textContent = \"Legend\";\n  } else {\n    panel.textContent = \"Score \" + score;\n  }\n});",
                "Lab: Forge finale."
            )
            {
                ConceptPoints = new[] { "Finale mini-arcade pattern.", "Practice the pattern until it feels natural.", "Read errors from the top." },
                EditorFileNameOverride = "forge.js"
            }
        };
}
