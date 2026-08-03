(function () {
    const KEY = "cavecode.minigames.v2";
    const LEGACY = "cavecode.minigames.v1";

    const names = ["Aria","Borin","Cassia","Darius","Elara","Finn","Garrick","Hazel","Iris","Jasper","Kael","Luna","Mira","Nolan","Orin","Petra","Quinn","Rowan","Soren","Talia","Ulric","Vera","Willow","Xander","Yara","Zane","Alden","Briar","Corin","Dahlia","Ember","Freya","Galen","Helena","Isla","Joren","Kira","Leona","Marek","Nadia","Oren","Piper","Riven","Selene","Theron","Uma","Vale","Wren"];
    const creatures = ["slime","caveBat","stoneGolem","fireDragon","frostDragon","goblinScout","goblinMiner","skeletonGuard","crystalSpider","shadowWolf","lavaWorm","caveTroll","mushroomBeast","spectralKnight","rockCrab","ancientMimic","stormHawk","forestSpirit","sandSerpent","ironBeetle","voidSerpent","emberFox","crystalWyrm","graniteGuardian","moonMoth","thornCrawler","ashGolem","frostHound"];
    const resources = ["stone","wood","ironOre","copperOre","silverOre","goldOre","coal","obsidian","quartz","crystals","feathers","dragonEggs","dragonScales","healingHerbs","manaDust","torchOil","rope","cloth","leather","arrows","ancientCoins","emeralds","rubies","sapphires","mushrooms","bones","shells","magicRunes","steelBars","enchantedWood","moonShards","sunStones","phoenixFeathers","cavePearls","shadowEssence","frostCrystals","emberSeeds","starMetal","glowMoss","crystalDust","ironKeys","silverKeys","mapFragments","potionVials","golemCores","dragonTeeth","ancientTablets","runeStones"];
    const items = ["ironPickaxe","crystalPickaxe","bronzeSword","fireSword","frostBow","woodenShield","towerShield","leatherBoots","ironHelmet","healingPotion","manaPotion","staminaPotion","antidote","torch","lantern","grapplingHook","compass","caveMap","ancientKey","silverKey","repairHammer","runeBlade","stormBow","dragonShield","climbingRope","miningDrill","crystalLantern","smokeBomb","fireBomb","iceCharm","guardianRing","travelPack"];
    const locations = ["Crystal Hollow","Ember Cavern","Moonlit Mine","Dragon's Vault","Shattered Passage","Forgotten Depths","Ironroot Tunnel","Obsidian Chamber","Echoing Grotto","Sunken Ruins","Mushroom Forest","Frozen Chasm","Goblin Workshop","Ancient Foundry","Shadow Gate","Starfall Quarry","Whispering Shaft","Cinder Hall","Granite Keep","Silverwater Cave","Hollow Crown","Redstone Gallery","Vault of Winds","Deepwell","Crystal Bridge","Ashen Descent","Rune Chamber"];

    const equipment = ["transfer_pump","cooling_pump","exhaust_fan","supply_fan","air_handler","control_valve","isolation_valve","tank_sensor","temperature_sensor","pressure_transmitter","flow_meter","door_switch","emergency_relay","warning_light","backup_generator","compressor","chiller","boiler","conveyor","mixing_motor","circulation_pump","sump_pump","relief_fan","dosing_pump","heat_exchanger","freezer_unit","cooling_tower","vacuum_pump","level_switch","vibration_sensor","motor_starter","safety_relay","exhaust_damper","intake_damper","water_meter","steam_valve","filter_monitor","battery_bank"];
    const sensors = ["tank_level","room_temperature","line_pressure","flow_rate","motor_current","fan_speed","valve_position","oxygen_level","humidity","vibration","runtime_hours","battery_voltage","alarm_count","filter_pressure","coolant_temperature","water_temperature","steam_pressure","freezer_temperature","chemical_level","pump_speed","air_quality","conductivity","ph_reading","turbidity","energy_usage","oil_pressure","bearing_temperature","system_demand"];
    const areas = ["North Mechanical Room","Cooling Plant","Wastewater Room","Electrical Vault","Boiler Room","Tank Farm","Loading Bay","Laboratory Wing","Emergency Generator Room","Utility Tunnel","Control Room","Freezer Corridor","Roof Equipment Area","Pump Gallery","Chemical Storage Area","South Mechanical Room","Process Water Room","Air Compressor Room","Main Switchgear Room","Clean Utility Corridor","Chiller Yard","Receiving Dock","Maintenance Shop","Central Plant","Research Annex"];
    const operators = ["Maya","Daniel","Priya","Marcus","Elena","Jordan","Noah","Sophia","Ethan","Amara","Luis","Grace","Owen","Nina","Caleb","Avery","Rosa","Miles","Leah","Theo","Camila","Jonah","Sasha","Mateo","Riley","Imani","Derek","Chloe"];

    const CS = [
        ["int",1,"declare"],["double",1,"declare"],["bool",1,"declare"],["string",1,"declare"],["add",1,"update"],["debug",1,"debug"],
        ["threshold",2,"condition"],["and",2,"condition"],["or",2,"condition"],["range",2,"condition"],["ifelse",2,"decision"],["craft",2,"decision"],
        ["void",3,"method"],["return",3,"method"],["parameter",3,"method"],["move",3,"movement"],["clamp",3,"movement"],["sequence",3,"method"],
        ["array",4,"collection"],["list",4,"collection"],["remove",4,"collection"],["contains",4,"collection"],["foreach",4,"loop"],["dictionary",4,"collection"],
        ["class",5,"object"],["constructor",5,"object"],["damage",5,"combat"],["object",5,"object"],["combat",5,"combat"],["state",5,"combat"]
    ];

    const PY = [
        ["number",1,"declare"],["decimal",1,"declare"],["bool",1,"declare"],["string",1,"declare"],["add",1,"update"],["debug",1,"debug"],
        ["threshold",2,"condition"],["and",2,"condition"],["or",2,"condition"],["range",2,"condition"],["ifelse",2,"decision"],["safety",2,"decision"],
        ["for",3,"loop"],["while",3,"loop"],["count",3,"loop"],["break",3,"loop"],["continue",3,"loop"],["sequence",3,"loop"],
        ["list",4,"collection"],["dict",4,"collection"],["append",4,"collection"],["contains",4,"collection"],["average",4,"data"],["filter",4,"data"],
        ["function",5,"function"],["return",5,"function"],["file",5,"file"],["class",5,"object"],["relay",5,"automation"],["emergency",5,"automation"]
    ];

    const pick = list => list[Math.floor(Math.random() * list.length)];
    const num = (a,b) => Math.floor(Math.random() * (b-a+1)) + a;
    const cap = value => String(value).replace(/[_-]+(.)/g,(_,c)=>c.toUpperCase()).replace(/^(.)/,c=>c.toUpperCase());
    const compact = code => String(code||"").toLowerCase().replace(/\s+/g,"");
    const lines = code => String(code||"").replace(/\r\n/g,"\n").split("\n").filter(x=>x.trim()).length;
    const hash = code => {
        let h=2166136261, v=String(code||"").trim();
        for(let i=0;i<v.length;i++){h^=v.charCodeAt(i);h=Math.imul(h,16777619);}
        return (h>>>0).toString(36);
    };

    function empty(course) {
        return {
            course, bestScore:0,lastScore:0,totalRuns:0,completedRuns:0,failedRuns:0,
            totalXpEarned:0,totalCrystalsEarned:0,totalValidatedLines:0,
            activeRun:false,runComplete:false,runFailed:false,endlessMode:false,
            difficulty:"standard",roomNumber:1,roomsTotal:5,score:0,streak:0,mistakes:0,
            primaryResource:100,secondaryResource:0,threat:0,
            hintUsed:false,hintPercent:0,hintReveal:"",hintPenalty:0,
            runHintsUsed:0,abandonedRuns:0,scenario:null,
            clearedVariations:{},clearedTemplates:{},codeHashes:{},
            history:{templates:[],terms:[],entities:[],numbers:[],types:[]}
        };
    }

    function normalize(base,saved,legacy) {
        const s=saved||{}, l=legacy||{};
        return {
            ...base,...s,
            bestScore:Number(s.bestScore??l.bestScore??0),
            lastScore:Number(s.lastScore??l.lastScore??0),
            totalRuns:Number(s.totalRuns??l.totalRuns??0),
            totalXpEarned:Number(s.totalXpEarned??l.totalXpEarned??0),
            totalCrystalsEarned:Number(s.totalCrystalsEarned??l.totalCrystalsEarned??0),
            clearedVariations:s.clearedVariations||{},
            clearedTemplates:s.clearedTemplates||{},
            codeHashes:s.codeHashes||{},
            history:{
                templates:s.history?.templates||[],
                terms:s.history?.terms||[],
                entities:s.history?.entities||[],
                numbers:s.history?.numbers||[],
                types:s.history?.types||[]
            }
        };
    }

    function load() {
        try {
            const s=JSON.parse(localStorage.getItem(KEY)||"{}");
            const l=JSON.parse(localStorage.getItem(LEGACY)||"{}");
            return {csharp:normalize(empty("csharp"),s.csharp,l.csharp),python:normalize(empty("python"),s.python,l.python)};
        } catch { return {csharp:empty("csharp"),python:empty("python")}; }
    }

    function save(state) {
        localStorage.setItem(KEY,JSON.stringify(state));
        window.dispatchEvent(new CustomEvent("cavecode-minigames-changed",{detail:hub(state)}));
    }

    function chapters(course) {
        try {
            const p=JSON.parse(localStorage.getItem(`cavecode.${course}.progress.v1`)||"null");
            const f=p?.moduleCompleted??p?.ModuleCompleted??[];
            let c=0;
            for(let i=1;i<=5;i++) if(f[i*8-1]===true)c++;
            return c;
        } catch { return 0; }
    }

    function view(state,course) {
        const v=state[course], c=chapters(course);
        return {...v,completedChapters:c,unlockedChapters:Math.max(1,c),endlessUnlocked:c>=5};
    }

    const hub = state => ({cSharp:view(state,"csharp"),python:view(state,"python")});

    function spec(all=[],any=[],none=[]) { return {all,any,none}; }
    function stripValidationComments(code, course) {
        let value = String(code || "");

        if (course === "python") {
            return value
                .split(/\r?\n/)
                .map(line => {
                    let quote = null;

                    for (let i = 0; i < line.length; i++) {
                        const character = line[i];

                        if (
                            (character === '"' || character === "'") &&
                            line[i - 1] !== "\\"
                        ) {
                            quote =
                                quote === character
                                    ? null
                                    : quote || character;
                        }

                        if (character === "#" && !quote) {
                            return line.slice(0, i);
                        }
                    }

                    return line;
                })
                .join("\n");
        }

        value = value.replace(
            /\/\*[\s\S]*?\*\//g,
            ""
        );

        return value
            .split(/\r?\n/)
            .map(line => {
                let quote = null;

                for (let i = 0; i < line.length - 1; i++) {
                    const character = line[i];

                    if (
                        (character === '"' || character === "'") &&
                        line[i - 1] !== "\\"
                    ) {
                        quote =
                            quote === character
                                ? null
                                : quote || character;
                    }

                    if (
                        line[i] === "/" &&
                        line[i + 1] === "/" &&
                        !quote
                    ) {
                        return line.slice(0, i);
                    }
                }

                return line;
            })
            .join("\n");
    }

    function keywordCaseIsValid(code, course) {
        const tokens =
            String(code || "").match(/[A-Za-z_]\w*/g) || [];

        if (course === "python") {
            const canonical = {
                def: "def",
                return: "return",
                for: "for",
                while: "while",
                if: "if",
                elif: "elif",
                else: "else",
                in: "in",
                and: "and",
                or: "or",
                not: "not",
                break: "break",
                continue: "continue",
                import: "import",
                from: "from",
                with: "with",
                as: "as",
                class: "class",
                pass: "pass",
                true: "True",
                false: "False",
                none: "None"
            };

            return tokens.every(token => {
                const expected =
                    canonical[token.toLowerCase()];

                return !expected || token === expected;
            });
        }

        const canonical = {
            int: "int",
            double: "double",
            bool: "bool",
            string: "string",
            void: "void",
            class: "class",
            public: "public",
            private: "private",
            protected: "protected",
            return: "return",
            new: "new",
            if: "if",
            else: "else",
            while: "while",
            foreach: "foreach",
            true: "true",
            false: "false",
            var: "var",
            ref: "ref"
        };

        return tokens.every(token => {
            const expected =
                canonical[token.toLowerCase()];

            return !expected || token === expected;
        });
    }

    function mergedKeywordsAreValid(code, course) {
        const value = String(code || "");

        if (course === "python") {
            return !/(^|[^A-Za-z0-9_])(def|return|for|while|if|elif|import|from|with|class)(?=[A-Za-z_])/m
                .test(value);
        }

        return !/(^|[^A-Za-z0-9_])(int|double|bool|string|void|class|public|private|protected|return|new)(?=[A-Za-z_])/m
            .test(value);
    }

    function indentationIsValid(code, course) {
        if (course !== "python") {
            return true;
        }

        const lines = String(code || "")
            .replace(/\t/g, "    ")
            .split(/\r?\n/);

        for (let index = 0; index < lines.length; index++) {
            const line = lines[index];

            if (!line.trim() || !line.trimEnd().endsWith(":")) {
                continue;
            }

            const currentIndent =
                line.length - line.trimStart().length;
            let nextIndex = index + 1;

            while (
                nextIndex < lines.length &&
                !lines[nextIndex].trim()
            ) {
                nextIndex++;
            }

            if (nextIndex >= lines.length) {
                return false;
            }

            const nextLine = lines[nextIndex];
            const nextIndent =
                nextLine.length - nextLine.trimStart().length;

            if (nextIndent <= currentIndent) {
                return false;
            }
        }

        return true;
    }

    function fragmentsInOrder(value, ordered) {
        let position = 0;

        for (const fragment of ordered || []) {
            const target = compact(fragment);
            const found = value.indexOf(
                target,
                position
            );

            if (found < 0) {
                return false;
            }

            position = found + target.length;
        }

        return true;
    }

    function valid(code, s, course) {
        const clean =
            stripValidationComments(
                code,
                course
            );
        const v = compact(clean);

        if (
            !keywordCaseIsValid(clean, course) ||
            !mergedKeywordsAreValid(clean, course) ||
            !indentationIsValid(clean, course)
        ) {
            return false;
        }

        const fragmentsPass =
            (s.all || []).every(
                x => v.includes(compact(x))
            ) &&
            (s.any || []).every(
                group => group.some(
                    x => v.includes(compact(x))
                )
            ) &&
            (s.none || []).every(
                x => !v.includes(compact(x))
            );

        if (!fragmentsPass) {
            return false;
        }

        if (
            s.ordered &&
            !fragmentsInOrder(
                v,
                s.ordered
            )
        ) {
            return false;
        }

        if (s.regexAll) {
            for (const pattern of s.regexAll) {
                if (
                    !(new RegExp(pattern, "i"))
                        .test(clean)
                ) {
                    return false;
                }
            }
        }

        return true;
    }

    function objective(d, direct, standard, advanced, expert) {
        return d==="training"?direct:d==="advanced"?(advanced||standard):d==="expert"?(expert||advanced||standard):standard;
    }
    function starter(d, training, standard, advanced) {
        return d==="expert"?"":d==="training"?(training||""):d==="advanced"?(advanced||""):(standard||"");
    }

    function scenario(meta,d) {
        return {
            id:meta.id,templateId:meta.templateId,chapter:meta.chapter,taskType:meta.taskType,
            title:meta.title,skill:meta.skill,brief:meta.brief,
            objective:objective(d,...meta.objectives),hint:meta.hint,
            starterCode:starter(d,...meta.starters),hintCode:(meta.starters&&meta.starters[0])||meta.hint||"",systemName:meta.systemName,
            visualIcon:meta.visualIcon||"◆",successStatus:meta.successStatus,
            primaryTerm:meta.primaryTerm,entity:meta.entity,numberValue:meta.numberValue,
            validator:meta.validator
        };
    }

    function cs(t,d,room) {
        const [k,ch,type]=t,n=pick(names),c=pick(creatures),r=pick(resources),r2=pick(resources.filter(x=>x!==r)),it=pick(items),loc=pick(locations);
        const a=num(2,75),b=num(2,40),start=num(50,180),cost=num(5,35),cut=num(15,90),boss=room===5?"Boss Room: ":"";
        const base={templateId:`cs-${k}`,chapter:ch,taskType:type,skill:type,entity:c,numberValue:a,visualIcon:room===5?"◉":"◆"};
        switch(k){
            case"int":return scenario({...base,id:`cs-int:${r}:${a}`,title:`${boss}Secure the ${r}`,brief:`${n} recovered ${a} ${r} in ${loc}.`,objectives:[`Create int ${r} = ${a}.`,`Store the whole-number ${r} count in ${r}.`,`Represent the recovered resource count.`,`Configure the whole-number inventory state.`],hint:`int ${r} = ${a};`,starters:[`int ${r} = 0;`,`// Store ${r}\n`,`// Inventory state\n`],systemName:"Inventory Relay",successStatus:`${a} ${r} secured`,primaryTerm:r,validator:spec([`int${r}=${a}`])},d);
            case"double":{const x=(Math.random()*15+.2).toFixed(1),v=`${r}Weight`;return scenario({...base,id:`cs-double:${v}:${x}`,title:`${boss}Calibrate ${r} Weight`,brief:`A ${r} sample weighs ${x} kilograms.`,objectives:[`Create double ${v} = ${x}.`,`Store the decimal weight in ${v}.`,`Preserve the fractional scale reading.`,`Configure the precise sample value.`],hint:`double ${v} = ${x};`,starters:[`double ${v} = 0;`,`// Store the weight\n`,`// Scale reading\n`],systemName:"Crystal Scale",successStatus:`${x} kg registered`,primaryTerm:v,validator:spec(["double",`${v}=${x}`])},d);}
            case"bool":{const v=`has${cap(it)}`;return scenario({...base,id:`cs-bool:${v}`,title:`${boss}Confirm the ${it}`,brief:`${n} acquired the ${it}.`,objectives:[`Create bool ${v} = true.`,`Store whether the explorer has the ${it}.`,`Represent the confirmed equipment state.`,`Configure the gate equipment input.`],hint:`bool ${v} = true;`,starters:[`bool ${v} = false;`,`// Store equipment state\n`,`// Gate input\n`],systemName:"Equipment Check",successStatus:`${it} confirmed`,primaryTerm:v,validator:spec([`bool${v}=true`])},d);}
            case"string":return scenario({...base,id:`cs-string:${loc}`,title:`${boss}Name the Expedition`,brief:`${n} discovered ${loc}.`,objectives:[`Create string currentZone = "${loc}".`,`Store the location name in currentZone.`,`Configure the new map label.`,`Record the named expedition zone.`],hint:"Text belongs in quotation marks.",starters:[`string currentZone = "";`,`// Store the location\n`,`// Map label\n`],systemName:"Map Terminal",successStatus:`${loc} mapped`,primaryTerm:"currentZone",validator:spec(["stringcurrentzone=",`"${loc.toLowerCase()}"`])},d);
            case"add":return scenario({...base,id:`cs-add:${r}:${a}:${b}`,title:`${boss}Update ${r}`,brief:`${n} had ${a} ${r} and found ${b} more.`,objectives:[`Start ${r} at ${a}, then add ${b}.`,`Update the existing ${r} total.`,`Apply the collection gain.`,`Reconcile the inventory change.`],hint:`Use += ${b}.`,starters:[`int ${r} = ${a};\n${r} += 0;`,`int ${r} = ${a};\n// Update\n`,`int ${r} = ${a};\n`],systemName:"Supply Counter",successStatus:`${a+b} ${r} available`,primaryTerm:r,validator:spec([`int${r}=${a}`],[[`${r}+=${b}`,`${r}=${r}+${b}`,`${r}=${a+b}`]])},d);
            case"debug":return scenario({...base,id:`cs-debug:${r}:${a}`,title:`${boss}Repair the Variable Type`,brief:`The terminal stores ${a} ${r} as Boolean data.`,objectives:[`Change bool to int for ${r}.`,`Correct the declaration so ${r} stores ${a}.`,`Repair the mismatched resource type.`,`Correct the invalid inventory state.`],hint:"Counts use int.",starters:[`bool ${r} = ${a};`,`bool ${r} = ${a};`,`bool ${r} = ${a};`],systemName:"Compiler Relay",successStatus:"Type mismatch corrected",primaryTerm:r,validator:spec([`int${r}=${a}`],[],[`bool${r}`])},d);
            case"threshold":return scenario({...base,id:`cs-threshold:${r}:${cut}`,title:`${boss}Open the Resource Gate`,brief:`The gate opens with at least ${cut} ${r}.`,objectives:[`Return ${r} >= ${cut}.`,`Implement the minimum-resource gate rule.`,`Enforce the stated threshold.`,`Authorize entry from the resource policy.`],hint:`Use >= ${cut}.`,starters:[`bool CanOpen(int ${r})\n{\n return false;\n}`,`bool CanOpen(int ${r})\n{\n // Rule\n}`,`// Implement CanOpen\n`],systemName:"Resource Gate",successStatus:"Threshold accepted",primaryTerm:r,validator:spec(["return",`${r}>=${cut}`])},d);
            case"and":return scenario({...base,id:`cs-and:${r}:${cut}:${it}`,title:`${boss}Authorize Dual Permissives`,brief:`Entry requires ${cut} ${r} and the ${it}.`,objectives:[`Return ${r} >= ${cut} && has${cap(it)}.`,`Combine both entry requirements.`,`Implement the two-permissive rule.`,`Enforce every entry requirement.`],hint:"Use &&.",starters:[`bool CanEnter(int ${r}, bool has${cap(it)})\n{\n return false;\n}`,`// Implement CanEnter\n`,`// Authorization rule\n`],systemName:"Dual Gate",successStatus:"Both permissives satisfied",primaryTerm:r,validator:spec([`${r}>=${cut}`,`has${it}`,"&&","return"])},d);
            case"or":return scenario({...base,id:`cs-or:${it}:${r2}`,title:`${boss}Choose an Escape Tool`,brief:`Cross with either ${it} or ${r2}.`,objectives:[`Return has${cap(it)} || has${cap(r2)}.`,`Allow either approved tool.`,`Implement alternative-equipment logic.`,`Authorize either valid state.`],hint:"Use ||.",starters:[`bool CanCross(bool has${cap(it)}, bool has${cap(r2)})\n{\n return false;\n}`,`// Implement CanCross\n`,`// Crossing rule\n`],systemName:"Chasm Bridge",successStatus:"Escape route ready",primaryTerm:it,validator:spec([`has${it}`,`has${r2}`,"||","return"])},d);
            case"range":{const hi=cut+num(20,60),v=pick(["health","mana","stamina","temperature"]);return scenario({...base,id:`cs-range:${v}:${cut}:${hi}`,title:`${boss}Validate the Safe Range`,brief:`${v} is safe from ${cut} through ${hi}.`,objectives:[`Return ${v} >= ${cut} && ${v} <= ${hi}.`,`Implement the inclusive safe range.`,`Validate both operating limits.`,`Enforce the complete envelope.`],hint:"Use two comparisons and &&.",starters:[`bool IsSafe(int ${v})\n{\n return false;\n}`,`// Implement IsSafe\n`,`// Range validation\n`],systemName:"Range Monitor",successStatus:"Safe range enforced",primaryTerm:v,validator:spec([`${v}>=${cut}`,`${v}<=${hi}`,"&&","return"])},d);}
            case"ifelse":{const v=pick(["health","mana","stamina","armor"]);return scenario({...base,id:`cs-ifelse:${v}:${cut}`,title:`${boss}Route the Encounter`,brief:`Call Defend when ${v} >= ${cut}; otherwise Retreat.`,objectives:[`Write if/else with both calls.`,`Branch between Defend and Retreat.`,`Implement both outcomes.`,`Route the encounter correctly.`],hint:"Both method calls need opposite branches.",starters:[`if (${v} >= ${cut})\n{\n}\nelse\n{\n}`,`// Choose Defend or Retreat\n`,`// Encounter branch\n`],systemName:"Tactical Router",successStatus:"Both paths configured",primaryTerm:v,validator:spec([`if(${v}>=${cut})`,"defend()","else","retreat()"])},d);}
            case"craft":return scenario({...base,id:`cs-craft:${r}:${a}:${r2}:${b}`,title:`${boss}Authorize Crafting`,brief:`Craft ${it} with ${a} ${r} and ${b} ${r2}.`,objectives:[`Return ${r} >= ${a} && ${r2} >= ${b}.`,`Implement the full recipe check.`,`Require every material.`,`Enforce the crafting policy.`],hint:"Use two >= comparisons and &&.",starters:[`bool CanCraft(int ${r}, int ${r2})\n{\n return false;\n}`,`// Implement CanCraft\n`,`// Recipe rule\n`],systemName:"Crafting Bench",successStatus:`${it} authorized`,primaryTerm:r,validator:spec([`${r}>=${a}`,`${r2}>=${b}`,"&&","return"])},d);
            case"void":return scenario({...base,id:`cs-void:${r}:${a}`,title:`${boss}Create a Collection Method`,brief:`A reusable method must add ${a} ${r}.`,objectives:[`Create void Collect${cap(r)} and add ${a}.`,`Implement reusable collection behavior.`,`Encapsulate the resource update.`,`Build the described reusable behavior.`],hint:`Use ${r} += ${a};`,starters:[`void Collect${cap(r)}()\n{\n ${r} += 0;\n}`,`void Collect${cap(r)}()\n{\n // Update\n}`,`// Create the method\n`],systemName:"Collection Routine",successStatus:"Method online",primaryTerm:r,validator:spec([`voidcollect${r}(`],[[`${r}+=${a}`,`${r}=${r}+${a}`]])},d);
            case"return":{const v=pick(["health","mana","stamina","armor"]);return scenario({...base,id:`cs-return:${v}`,title:`${boss}Return the Remaining ${cap(v)}`,brief:`Return ${v} after subtracting damage.`,objectives:[`Create int Remaining${cap(v)}(int ${v}, int damage) and return ${v} - damage.`,`Implement the remaining-value method.`,`Return the post-damage resource.`,`Complete the calculation contract.`],hint:"A non-void method needs return.",starters:[`int Remaining${cap(v)}(int ${v}, int damage)\n{\n return 0;\n}`,`// Create the method\n`,`// Resource calculation\n`],systemName:"Combat Calculator",successStatus:"Calculation ready",primaryTerm:v,validator:spec([`intremaining${v}(`,`return${v}-damage`])},d);}
            case"parameter":return scenario({...base,id:`cs-parameter:${r}`,title:`${boss}Accept Variable Amounts`,brief:`Different rooms yield different ${r} amounts.`,objectives:[`Create Add${cap(r)}(int amount) and add amount to ${r}.`,`Implement a parameterized resource method.`,`Generalize the update with a parameter.`,`Build reusable variable-amount behavior.`],hint:`Use ${r} += amount;`,starters:[`void Add${cap(r)}(int amount)\n{\n ${r} += 0;\n}`,`// Create Add${cap(r)}\n`,`// Parameterized update\n`],systemName:"Variable Collector",successStatus:"Parameterized method ready",primaryTerm:r,validator:spec([`voidadd${r}(intamount)`],[[`${r}+=amount`,`${r}=${r}+amount`]])},d);
            case"move":{const dir=pick([["right","x","++"],["left","x","--"],["up","y","++"],["down","y","--"]]);return scenario({...base,id:`cs-move:${dir[0]}:${loc}`,title:`${boss}Move ${dir[0]}`,brief:`Move one tile ${dir[0]} through ${loc}.`,objectives:[`Create Move${cap(dir[0])} and change ${dir[1]} by one.`,`Implement one-tile movement.`,`Apply the coordinate change.`,`Encode the movement behavior.`],hint:`Use ${dir[1]}${dir[2]};`,starters:[`void Move${cap(dir[0])}(ref int ${dir[1]})\n{\n}`,`// Implement movement\n`,`// Movement routine\n`],systemName:"Movement Rail",successStatus:`Moved ${dir[0]}`,primaryTerm:dir[1],validator:spec([`move${dir[0]}`],[[`${dir[1]}${dir[2]}`,`${dir[1]}+=${dir[2]==="++"?"1":"-1"}`]])},d);}
            case"clamp":{const max=num(6,14);return scenario({...base,id:`cs-clamp:${max}`,title:`${boss}Protect the Cave Boundary`,brief:`x must remain from 0 through ${max}.`,objectives:[`Clamp x from 0 to ${max}.`,`Keep x inside the inclusive boundary.`,`Implement both coordinate limits.`,`Prevent movement outside the world.`],hint:`Math.Clamp(x, 0, ${max}) is valid.`,starters:[`x = Math.Clamp(x, 0, 0);`,`// Keep x inside the cave\n`,`// Boundary protection\n`],systemName:"World Boundary",successStatus:"Boundary enforced",primaryTerm:"x",validator:spec([],[[`math.clamp(x,0,${max})`,`if(x<0)`]])},d);}
            case"sequence":return scenario({...base,id:`cs-sequence:${it}`,title:`${boss}Run the Escape Sequence`,brief:`Equip ${it}, open the gate, then move forward.`,objectives:[`Create EscapeSequence with all three calls in order.`,`Implement the ordered escape routine.`,`Preserve the required action sequence.`,`Encode the escape procedure.`],hint:"Put the calls one after another.",starters:[`void EscapeSequence()\n{\n}`,`// Implement EscapeSequence\n`,`// Escape routine\n`],systemName:"Escape Controller",successStatus:"Sequence armed",primaryTerm:it,validator:spec(["voidescapesequence()",`equip${it}()`,"opengate()","moveforward()"])},d);
            case"array":{const vals=[num(5,30),num(5,30),num(5,30),num(5,30)];return scenario({...base,id:`cs-array:${r}:${vals.join("-")}`,title:`${boss}Store Chamber Readings`,brief:`Chambers contain ${vals.join(", ")} ${r}.`,objectives:[`Create an int array with ${vals.join(", ")}.`,`Store all four readings in an array.`,`Represent the fixed dataset.`,`Configure the chamber collection.`],hint:"Use int[] and braces.",starters:[`int[] readings = { };`,`// Store all readings\n`,`// Fixed data\n`],systemName:"Chamber Array",successStatus:"Readings stored",primaryTerm:"readings",validator:spec(["int[]readings=",...vals.map(String)])},d);}
            case"list":return scenario({...base,id:`cs-list:${it}:${r}`,title:`${boss}Load the Expedition Pack`,brief:`Add ${it} and ${r} to inventory.`,objectives:[`Create List<string> inventory and Add both items.`,`Add both supplies to a flexible list.`,`Populate the required collection.`,`Configure the expedition inventory.`],hint:"Call Add twice.",starters:[`List<string> inventory = new();\n`,`List<string> inventory = new();\n// Add items\n`,`// Flexible inventory\n`],systemName:"Supply Cache",successStatus:"Supplies loaded",primaryTerm:"inventory",validator:spec(["list<string>inventory",`inventory.add("${it.toLowerCase()}")`,`inventory.add("${r.toLowerCase()}")`])},d);
            case"remove":return scenario({...base,id:`cs-remove:${it}`,title:`${boss}Remove Broken Equipment`,brief:`${it} is broken and must leave equipment.`,objectives:[`Call equipment.Remove("${it}").`,`Remove the broken item.`,`Apply the collection removal.`,`Reconcile the equipment list.`],hint:"Use Remove.",starters:[`equipment.Remove("");`,`// Remove ${it}\n`,`// Collection correction\n`],systemName:"Equipment Rack",successStatus:`${it} removed`,primaryTerm:"equipment",validator:spec([`equipment.remove("${it.toLowerCase()}")`])},d);
            case"contains":return scenario({...base,id:`cs-contains:${r}`,title:`${boss}Search the Treasure Cache`,brief:`Store whether treasure contains ${r}.`,objectives:[`Set hasTreasure from treasure.Contains("${r}").`,`Store the membership result.`,`Implement the presence check.`,`Configure the cache search.`],hint:"Use Contains.",starters:[`bool hasTreasure = false;`,`// Search treasure\n`,`// Membership test\n`],systemName:"Treasure Scanner",successStatus:"Search configured",primaryTerm:r,validator:spec(["boolhastreasure=",`treasure.contains("${r.toLowerCase()}")`])},d);
            case"foreach":return scenario({...base,id:`cs-foreach:${c}`,title:`${boss}Inspect Every Enemy`,brief:`Pass every enemy to InspectEnemy.`,objectives:[`Use foreach over enemies and call InspectEnemy(enemy).`,`Inspect every enemy.`,`Apply inspection across the collection.`,`Implement full enemy iteration.`],hint:"Use foreach.",starters:[`foreach (var enemy in enemies)\n{\n}`,`// Inspect every enemy\n`,`// Enemy iteration\n`],systemName:"Enemy Scanner",successStatus:"All enemies inspected",primaryTerm:"enemies",validator:spec(["foreach(","inenemies","inspectenemy(enemy)"])},d);
            case"dictionary":return scenario({...base,id:`cs-dictionary:${r}:${a}:${r2}:${b}`,title:`${boss}Map Supply Quantities`,brief:`Store ${r}: ${a} and ${r2}: ${b}.`,objectives:[`Create Dictionary<string,int> supplies with both pairs.`,`Store both named quantities.`,`Build the keyed supply map.`,`Configure the quantity lookup.`],hint:"Use string keys and int values.",starters:[`Dictionary<string, int> supplies = new();\n`,`// Build the supply map\n`,`// Keyed quantities\n`],systemName:"Supply Map",successStatus:"Lookup online",primaryTerm:"supplies",validator:spec(["dictionary<string,int>supplies",`"${r.toLowerCase()}"`,String(a),`"${r2.toLowerCase()}"`,String(b)])},d);
            case"class":return scenario({...base,id:`cs-class:${c}`,title:`${boss}Define the ${cap(c)}`,brief:`Model ${c} with Name, Health, and IsDefeated.`,objectives:[`Create class ${cap(c)} with the three properties.`,`Model the creature state.`,`Define the required entity contract.`,`Build the creature model.`],hint:"Use public properties.",starters:[`class ${cap(c)}\n{\n}`,`class ${cap(c)}\n{\n // Properties\n}`,`// Define the creature\n`],systemName:"Creature Registry",successStatus:"Creature model compiled",primaryTerm:c,validator:spec([`class${c}`,"stringname","inthealth","boolisdefeated"])},d);
            case"constructor":return scenario({...base,id:`cs-constructor:${c}:${start}`,title:`${boss}Initialize the ${cap(c)}`,brief:`New ${c} objects start with ${start} health.`,objectives:[`Add a constructor accepting name and assigning Health = ${start}.`,`Initialize name and starting health.`,`Implement object construction.`,`Complete the constructor contract.`],hint:"The constructor name matches the class.",starters:[`class ${cap(c)}\n{\n public ${cap(c)}(string name)\n {\n }\n}`,`// Add the constructor\n`,`// Object initialization\n`],systemName:"Spawn Controller",successStatus:"Constructor ready",primaryTerm:c,validator:spec([`public${c}(stringname)`,`health=${start}`])},d);
            case"damage":return scenario({...base,id:`cs-damage:${c}`,title:`${boss}Apply Combat Damage`,brief:`TakeDamage must reduce Health by damage.`,objectives:[`Create TakeDamage(int damage) and subtract damage.`,`Implement reusable damage behavior.`,`Apply the parameterized health reduction.`,`Complete the combat contract.`],hint:"Use Health -= damage.",starters:[`public void TakeDamage(int damage)\n{\n Health -= 0;\n}`,`// Implement TakeDamage\n`,`// Damage behavior\n`],systemName:"Damage Resolver",successStatus:"Damage method ready",primaryTerm:"Health",validator:spec(["voidtakedamage(intdamage)"],[["health-=damage","health=health-damage"]])},d);
            case"object":return scenario({...base,id:`cs-object:${c}:${n}:${start}`,title:`${boss}Spawn ${n}`,brief:`Create a ${cap(c)} named ${n} with ${start} health.`,objectives:[`Instantiate ${cap(c)} enemy and set Name and Health.`,`Create the required enemy object.`,`Initialize the encounter entity.`,`Spawn the described combat object.`],hint:"An object initializer is valid.",starters:[`${cap(c)} enemy = new ${cap(c)}\n{\n};`,`// Create the enemy\n`,`// Spawn entity\n`],systemName:"Enemy Spawner",successStatus:`${n} spawned`,primaryTerm:"enemy",validator:spec([`${c}enemy=new`,`name="${n.toLowerCase()}"`,`health=${start}`])},d);
            case"combat":return scenario({...base,id:`cs-combat:${c}:${cost}`,title:`${boss}Defeat the ${cap(c)}`,brief:`Attack while enemy.Health > 0 for ${cost} damage.`,objectives:[`Use while and enemy.TakeDamage(${cost}).`,`Repeat attacks until defeat.`,`Implement the combat loop.`,`Resolve the encounter through repeated damage.`],hint:"Use a while loop.",starters:[`while (enemy.Health > 0)\n{\n}`,`// Continue until defeated\n`,`// Combat loop\n`],systemName:"Combat Loop",successStatus:"Defeat routine ready",primaryTerm:"enemy.Health",validator:spec(["while(enemy.health>0)",`enemy.takedamage(${cost})`])},d);
            case"state":return scenario({...base,id:`cs-state:${c}`,title:`${boss}Set the Defeated State`,brief:`When Health <= 0, set IsDefeated true.`,objectives:[`Write the if and state assignment.`,`Update defeated state from health.`,`Implement the terminal state transition.`,`Resolve object state at zero health.`],hint:"Use <= 0.",starters:[`if (Health <= 0)\n{\n}`,`// Update IsDefeated\n`,`// Terminal state\n`],systemName:"Combat State",successStatus:"Defeat state ready",primaryTerm:"IsDefeated",validator:spec(["if(health<=0)","isdefeated=true"])},d);
        }
    }

    function py(t,d,room) {
        const [k,ch,type]=t,e=pick(equipment),e2=pick(equipment.filter(x=>x!==e)),v=pick(sensors),area=pick(areas),op=pick(operators);
        const a=num(2,95),b=num(2,45),start=num(50,180),cost=num(5,35),cut=num(20,140),boss=room===5?"Emergency: ":"";
        const base={templateId:`py-${k}`,chapter:ch,taskType:type,skill:type,entity:e,numberValue:a,visualIcon:room===5?"⚡":"⌁"};
        switch(k){
            case"number":return scenario({...base,id:`py-number:${v}:${a}`,title:`${boss}Restore ${v}`,brief:`${op} reports ${v} is ${a} in ${area}.`,objectives:[`Assign ${a} to ${v}.`,`Store the whole-number reading.`,`Represent the process value.`,`Configure the whole-number input.`],hint:`${v} = ${a}`,starters:[`${v} = 0`,`# Store the reading\n`,`# Process input\n`],systemName:"Sensor Feed",successStatus:"Reading restored",primaryTerm:v,validator:spec([`${v}=${a}`])},d);
            case"decimal":{const x=(Math.random()*170+.5).toFixed(1);return scenario({...base,id:`py-decimal:${v}:${x}`,title:`${boss}Preserve the Decimal Reading`,brief:`${v} is ${x}.`,objectives:[`Assign ${x} to ${v}.`,`Store the precise reading.`,`Preserve the fractional value.`,`Configure the precision input.`],hint:`${v} = ${x}`,starters:[`${v} = 0.0`,`# Store the precise reading\n`,`# Precision input\n`],systemName:"Precision Sensor",successStatus:"Decimal registered",primaryTerm:v,validator:spec([`${v}=${x}`])},d);}
            case"bool":return scenario({...base,id:`py-bool:${e}`,title:`${boss}Confirm Equipment State`,brief:`${op} confirms ${e} is running.`,objectives:[`Set ${e}_running = True.`,`Store the running state.`,`Represent the equipment state.`,`Configure the Boolean input.`],hint:"Python uses True.",starters:[`${e}_running = False`,`# Store the state\n`,`# Equipment state\n`],systemName:"Status Input",successStatus:`${e} marked running`,primaryTerm:`${e}_running`,validator:spec([`${e}_running=true`])},d);
            case"string":return scenario({...base,id:`py-string:${op}:${area}`,title:`${boss}Record the Operator`,brief:`${op} is assigned to ${area}.`,objectives:[`Set operator_name = "${op}".`,`Store the operator name.`,`Configure the operator label.`,`Record the named operator.`],hint:"Text belongs in quotes.",starters:[`operator_name = ""`,`# Store the operator\n`,`# Operator label\n`],systemName:"Operator Console",successStatus:`${op} signed in`,primaryTerm:"operator_name",validator:spec(["operator_name=",`"${op.toLowerCase()}"`])},d);
            case"add":return scenario({...base,id:`py-add:${v}:${a}:${b}`,title:`${boss}Update ${v}`,brief:`${v} begins at ${a} and increases by ${b}.`,objectives:[`Set ${v} to ${a}, then add ${b}.`,`Apply the reported increase.`,`Reconcile the updated value.`,`Apply the positive process change.`],hint:`Use += ${b}.`,starters:[`${v} = ${a}\n${v} += 0`,`${v} = ${a}\n# Update\n`,`${v} = ${a}\n`],systemName:"Trend Counter",successStatus:`Updated to ${a+b}`,primaryTerm:v,validator:spec([`${v}=${a}`],[[`${v}+=${b}`,`${v}=${v}+${b}`,`${v}=${a+b}`]])},d);
            case"debug":return scenario({...base,id:`py-debug:${v}:${a}`,title:`${boss}Repair the Sensor Assignment`,brief:`${a} is incorrectly stored as text.`,objectives:[`Remove quotes from ${v}.`,`Correct the reading to numeric data.`,`Repair the value representation.`,`Correct the invalid process state.`],hint:"Numbers do not need quotes.",starters:[`${v} = "${a}"`,`${v} = "${a}"`,`${v} = "${a}"`],systemName:"Input Repair",successStatus:"Numeric input restored",primaryTerm:v,validator:spec([`${v}=${a}`],[],[`${v}="${a}"`,`${v}='${a}'`])},d);
            case"threshold":return scenario({...base,id:`py-threshold:${v}:${cut}`,title:`${boss}Evaluate the Alarm Threshold`,brief:`Alarm when ${v} > ${cut}.`,objectives:[`Set alarm_active to ${v} > ${cut}.`,`Store the threshold comparison.`,`Implement the high-limit alarm.`,`Configure the process threshold policy.`],hint:`Use > ${cut}.`,starters:[`alarm_active = False`,`# Evaluate alarm\n`,`# High-limit alarm\n`],systemName:"Alarm Comparator",successStatus:"Threshold configured",primaryTerm:v,validator:spec(["alarm_active=",`${v}>${cut}`])},d);
            case"and":return scenario({...base,id:`py-and:${v}:${cut}:${e}`,title:`${boss}Commission the Start Permissive`,brief:`${e} needs ${v} > ${cut} and valve_open.`,objectives:[`Return both requirements with and.`,`Combine both start permissives.`,`Implement the complete start rule.`,`Enforce every stated permissive.`],hint:"Use and.",starters:[`def can_start(${v}, valve_open):\n    return False`,`# Implement can_start\n`,`# Start permissive\n`],systemName:"Start Interlock",successStatus:"Permissive ready",primaryTerm:v,validator:spec(["return",`${v}>${cut}`,"and","valve_open"])},d);
            case"or":return scenario({...base,id:`py-or:${v}:${cut}:${b}`,title:`${boss}Combine Alarm Sources`,brief:`Alarm when ${v} > ${cut} or alarm_count >= ${b}.`,objectives:[`Set alarm_active from both conditions using or.`,`Implement either-source activation.`,`Combine alternative triggers.`,`Configure the multi-source policy.`],hint:"Use or.",starters:[`alarm_active = False`,`# Combine alarm sources\n`,`# Multi-source alarm\n`],systemName:"Alarm Router",successStatus:"Sources combined",primaryTerm:v,validator:spec(["alarm_active=",`${v}>${cut}`,`alarm_count>=${b}`,"or"])},d);
            case"range":{const hi=cut+num(20,70);return scenario({...base,id:`py-range:${v}:${cut}:${hi}`,title:`${boss}Validate the Operating Range`,brief:`${v} is safe from ${cut} through ${hi}.`,objectives:[`Set safe to ${cut} <= ${v} <= ${hi}.`,`Implement the inclusive safe range.`,`Validate both limits.`,`Enforce the complete envelope.`],hint:"Use a chained comparison.",starters:[`safe = False`,`# Evaluate safe range\n`,`# Operating envelope\n`],systemName:"Range Monitor",successStatus:"Range configured",primaryTerm:v,validator:spec(["safe="],[[`${cut}<=${v}<=${hi}`,`${v}>=${cut}and${v}<=${hi}`]])},d);}
            case"ifelse":return scenario({...base,id:`py-ifelse:${v}:${cut}:${e}`,title:`${boss}Route the Equipment Command`,brief:`Start ${e} when ${v} >= ${cut}; otherwise stop_equipment().`,objectives:[`Write if/else with both calls.`,`Branch between start and stop.`,`Implement both outcomes.`,`Route the process command.`],hint:"Use colons after if and else.",starters:[`if ${v} >= ${cut}:\n    pass\nelse:\n    pass`,`# Route the command\n`,`# Equipment branch\n`],systemName:"Command Router",successStatus:"Both paths ready",primaryTerm:v,validator:spec([`if${v}>=${cut}:`,`start_${e}()`,"else:","stop_equipment()"])},d);
            case"safety":return scenario({...base,id:`py-safety:${v}:${cut}:${e}`,title:`${boss}Build the Safety Interlock`,brief:`${e} needs ${v} > ${cut}, valve_open, and not emergency_stop.`,objectives:[`Return all three requirements with and.`,`Implement the complete interlock.`,`Enforce every safety condition.`,`Configure the equipment policy.`],hint:"Use and twice and not once.",starters:[`def can_start(${v}, valve_open, emergency_stop):\n    return False`,`# Implement the interlock\n`,`# Complete permissive\n`],systemName:"Safety Interlock",successStatus:"Interlock commissioned",primaryTerm:v,validator:spec(["return",`${v}>${cut}`,"valve_open","notemergency_stop","and"])},d);
            case"for":return scenario({...base,id:`py-for:${e}`,title:`${boss}Inspect Every Device`,brief:`Pass every device to inspect_device.`,objectives:[`Use for device in equipment_list.`,`Inspect every device.`,`Apply inspection across the list.`,`Implement full device iteration.`],hint:"Use a for loop.",starters:[`for device in equipment_list:\n    pass`,`# Inspect every device\n`,`# Equipment iteration\n`],systemName:"Inspection Loop",successStatus:"All devices inspected",primaryTerm:"equipment_list",validator:spec(["fordeviceinequipment_list:","inspect_device(device)"])},d);
            case"while":return scenario({...base,id:`py-while:${v}:${cut}`,title:`${boss}Poll Until the Target`,brief:`Poll while ${v} < ${cut}.`,objectives:[`Use while ${v} < ${cut} and call read_sensor().`,`Repeat polling until target.`,`Implement the target loop.`,`Maintain polling under the process condition.`],hint:"Use while.",starters:[`while ${v} < ${cut}:\n    pass`,`# Poll until target\n`,`# Target loop\n`],systemName:"Polling Controller",successStatus:"Polling configured",primaryTerm:v,validator:spec([`while${v}<${cut}:`,"read_sensor()"])},d);
            case"count":return scenario({...base,id:`py-count:${cut}`,title:`${boss}Count Unsafe Readings`,brief:`Count readings above ${cut}.`,objectives:[`Loop through readings and increment unsafe_count above ${cut}.`,`Count all unsafe readings.`,`Implement threshold counting.`,`Produce the unsafe total.`],hint:"Use += 1 inside the if.",starters:[`unsafe_count = 0\nfor reading in readings:\n    if reading > ${cut}:\n        pass`,`# Count unsafe readings\n`,`# Threshold count\n`],systemName:"Safety Counter",successStatus:"Count logic ready",primaryTerm:"unsafe_count",validator:spec(["unsafe_count=0","forreadinginreadings:",`ifreading>${cut}:`],[["unsafe_count+=1","unsafe_count=unsafe_count+1"]])},d);
            case"break":return scenario({...base,id:`py-break:${e}`,title:`${boss}Stop at the First Fault`,brief:`Stop scanning when status equals "fault".`,objectives:[`Use if status == "fault": break.`,`End at the first fault.`,`Implement early termination.`,`Stop under the terminal condition.`],hint:"Use break.",starters:[`for status in statuses:\n    if status == "fault":\n        pass`,`# Stop at first fault\n`,`# Early termination\n`],systemName:"Fault Scanner",successStatus:"Fault termination ready",primaryTerm:"status",validator:spec(["forstatusinstatuses:",`ifstatus=="fault":`,"break"])},d);
            case"continue":return scenario({...base,id:`py-continue:${e}`,title:`${boss}Skip Offline Devices`,brief:`Skip devices whose status is "offline".`,objectives:[`Use continue for offline devices.`,`Skip offline devices.`,`Implement the exclusion.`,`Bypass every offline device.`],hint:"Use continue.",starters:[`for device in devices:\n    if device["status"] == "offline":\n        pass`,`# Skip offline devices\n`,`# Device exclusion\n`],systemName:"Device Processor",successStatus:"Offline devices bypassed",primaryTerm:"device",validator:spec(["fordeviceindevices:",`ifdevice["status"]=="offline":`,"continue"])},d);
            case"sequence":return scenario({...base,id:`py-sequence:${e}:${e2}`,title:`${boss}Run the Startup Sequence`,brief:`Start ${e}, wait 2 seconds, then start ${e2}.`,objectives:[`Call all three actions in order.`,`Implement the equipment sequence.`,`Preserve the startup order.`,`Encode the commissioning sequence.`],hint:"Put the calls one after another.",starters:[`# Startup sequence\n`,`# Run the ordered sequence\n`,`# Commissioning sequence\n`],systemName:"Sequence Controller",successStatus:"Startup order ready",primaryTerm:e,validator:spec([`start_${e}()`,"sleep(2)",`start_${e2}()`])},d);
            case"list":{const vals=[num(60,90),num(60,90),num(60,90),num(60,90)];return scenario({...base,id:`py-list:${v}:${vals.join("-")}`,title:`${boss}Store the Reading Set`,brief:`Readings are ${vals.join(", ")}.`,objectives:[`Create readings with ${vals.join(", ")}.`,`Store all four values in a list.`,`Represent the dataset.`,`Configure the process collection.`],hint:"Use square brackets.",starters:[`readings = []`,`# Store readings\n`,`# Process dataset\n`],systemName:"Reading Buffer",successStatus:"Readings stored",primaryTerm:"readings",validator:spec(["readings=[",...vals.map(String)])},d);}
            case"dict":return scenario({...base,id:`py-dict:${e}:${e2}`,title:`${boss}Map Equipment Status`,brief:`${e} is running and ${e2} is stopped.`,objectives:[`Create statuses with both pairs.`,`Store both states in a dictionary.`,`Build the keyed status map.`,`Configure the named lookup.`],hint:"Use braces and key-value pairs.",starters:[`statuses = {}`,`# Build the status map\n`,`# Keyed states\n`],systemName:"Status Map",successStatus:"Lookup online",primaryTerm:"statuses",validator:spec(["statuses={",`"${e}"`,'"running"',`"${e2}"`,'"stopped"'])},d);
            case"append":return scenario({...base,id:`py-append:${v}:${a}`,title:`${boss}Append the New Reading`,brief:`A new ${v} reading of ${a} arrived.`,objectives:[`Append ${a} to readings.`,`Add the new reading.`,`Apply the dataset update.`,`Reconcile the reading buffer.`],hint:`readings.append(${a})`,starters:[`readings.append(0)`,`# Add the reading\n`,`# Dataset update\n`],systemName:"Reading Buffer",successStatus:"Reading appended",primaryTerm:"readings",validator:spec([`readings.append(${a})`])},d);
            case"contains":return scenario({...base,id:`py-contains:${e}`,title:`${boss}Confirm Device Registration`,brief:`Store whether ${e} is in active_equipment.`,objectives:[`Set registered = "${e}" in active_equipment.`,`Store the membership result.`,`Implement the presence check.`,`Configure registration testing.`],hint:"Use in.",starters:[`registered = False`,`# Check registration\n`,`# Membership test\n`],systemName:"Asset Scanner",successStatus:"Registration check ready",primaryTerm:"registered",validator:spec(["registered=",`"${e}"inactive_equipment`])},d);
            case"average":return scenario({...base,id:`py-average:${v}`,title:`${boss}Calculate the Process Average`,brief:`Calculate the average of readings.`,objectives:[`Set average = sum(readings) / len(readings).`,`Calculate the reading average.`,`Aggregate the dataset.`,`Produce the process average.`],hint:"Divide sum by length.",starters:[`average = 0`,`# Calculate average\n`,`# Dataset mean\n`],systemName:"Trend Analyzer",successStatus:"Average ready",primaryTerm:"average",validator:spec(["average=","sum(readings)","len(readings)","/"])},d);
            case"filter":return scenario({...base,id:`py-filter:${cut}`,title:`${boss}Filter Unsafe Values`,brief:`Keep only readings above ${cut}.`,objectives:[`Create unsafe_readings with a list comprehension.`,`Filter the dataset.`,`Build the threshold subset.`,`Produce the unsafe-value collection.`],hint:`[reading for reading in readings if reading > ${cut}]`,starters:[`unsafe_readings = []`,`# Filter readings\n`,`# Threshold subset\n`],systemName:"Safety Filter",successStatus:"Unsafe subset ready",primaryTerm:"unsafe_readings",validator:spec(["unsafe_readings=[","forreadinginreadings",`ifreading>${cut}`])},d);
            case"function":return scenario({...base,id:`py-function:${e}`,title:`${boss}Create the Start Function`,brief:`Create start_equipment(name) and print name.`,objectives:[`Define the function and print name.`,`Implement reusable start behavior.`,`Encapsulate the named action.`,`Build the reusable routine.`],hint:"Use def and the name parameter.",starters:[`def start_equipment(name):\n    pass`,`# Create start_equipment\n`,`# Reusable start routine\n`],systemName:"Function Library",successStatus:"Start function ready",primaryTerm:"start_equipment",validator:spec(["defstart_equipment(name):","print(","name"])},d);
            case"return":return scenario({...base,id:`py-return:${v}`,title:`${boss}Calculate Flow Rate`,brief:`Return volume divided by minutes.`,objectives:[`Create calculate_rate(volume, minutes).`,`Implement the rate calculation.`,`Return the process ratio.`,`Complete the calculation contract.`],hint:"return volume / minutes",starters:[`def calculate_rate(volume, minutes):\n    return 0`,`# Create calculate_rate\n`,`# Process calculation\n`],systemName:"Rate Calculator",successStatus:"Rate function ready",primaryTerm:"calculate_rate",validator:spec(["defcalculate_rate(volume,minutes):","returnvolume/minutes"])},d);
            case"file":return scenario({...base,id:`py-file:${v}`,title:`${boss}Append the Alarm Log`,brief:`Append "High ${v}" to alarms.log.`,objectives:[`Open alarms.log in append mode and write the message.`,`Append the alarm message.`,`Implement persistent logging.`,`Record the incident in the facility log.`],hint:'Use open("alarms.log", "a").',starters:[`with open("alarms.log", "a") as log:\n    pass`,`# Append the alarm\n`,`# Persistent log\n`],systemName:"Alarm Logger",successStatus:"Alarm recorded",primaryTerm:"alarms.log",validator:spec(["withopen(","alarms.log",'"a"',"log.write(",`high${v}`])},d);
            case"class":return scenario({...base,id:`py-class:${e}`,title:`${boss}Model the Device`,brief:`Equipment needs name, status, and runtime_hours.`,objectives:[`Create Equipment with __init__ assigning all three attributes.`,`Model the equipment state.`,`Implement the object contract.`,`Build the facility equipment model.`],hint:"Assign self.name, self.status, and self.runtime_hours.",starters:[`class Equipment:\n    def __init__(self, name, status, runtime_hours):\n        pass`,`# Define Equipment\n`,`# Device model\n`],systemName:"Asset Model",successStatus:"Equipment class ready",primaryTerm:"Equipment",validator:spec(["classequipment:","def__init__(","self.name=","self.status=","self.runtime_hours="])},d);
            case"relay":return scenario({...base,id:`py-relay:${e}`,title:`${boss}Control the Warning Relay`,brief:`If door_open, call relay_on; otherwise relay_off.`,objectives:[`Write the if/else relay behavior.`,`Control the relay from door state.`,`Implement both outputs.`,`Configure the simulated GPIO response.`],hint:"Use if door_open.",starters:[`if door_open:\n    pass\nelse:\n    pass`,`# Control the relay\n`,`# GPIO simulation\n`],systemName:"Warning Relay",successStatus:"Relay behavior ready",primaryTerm:"door_open",validator:spec(["ifdoor_open:","relay_on()","else:","relay_off()"])},d);
            case"emergency":return scenario({...base,id:`py-emergency:${e}:${e2}:${area}`,title:`${boss}Execute Emergency Response`,brief:`A gas alarm in ${area} requires stop_${e}, start_${e2}, warning_light_on, and log_event.`,objectives:[`Create emergency_response with all four calls.`,`Implement the complete response.`,`Preserve every required action.`,`Encode the emergency procedure.`],hint:"All four calls must appear.",starters:[`def emergency_response():\n    pass`,`# Implement emergency_response\n`,`# Emergency procedure\n`],systemName:"Emergency Controller",successStatus:"Emergency sequence armed",primaryTerm:"emergency_response",validator:spec(["defemergency_response(",`stop_${e}()`,`start_${e2}()`,"warning_light_on()","log_event("])},d);
        }
    }

    function remember(list,value,limit){list.unshift(value);while(list.length>limit)list.pop();}
    function generate(v,course){
        const unlocked=Math.max(1,chapters(course)), source=course==="python"?PY:CS;
        let available=source.filter(x=>x[1]<=unlocked);
        if(v.roomNumber===v.roomsTotal){
            const high=available.filter(x=>x[1]===unlocked);
            if(high.length)available=high;
        }
        let s;
        for(let i=0;i<50;i++){
            const t=pick(available);
            s=course==="python"?py(t,v.difficulty,v.roomNumber):cs(t,v.difficulty,v.roomNumber);
            if(!v.history.templates.includes(s.templateId)
                && !v.history.terms.slice(0,6).includes(s.primaryTerm)
                && !v.history.entities.slice(0,8).includes(s.entity)
                && !v.history.numbers.slice(0,5).includes(s.numberValue)
                && v.history.types.slice(0,2).filter(x=>x===s.taskType).length<2) break;
        }
        remember(v.history.templates,s.templateId,12);
        remember(v.history.terms,s.primaryTerm,6);
        remember(v.history.entities,s.entity,8);
        remember(v.history.numbers,s.numberValue,5);
        remember(v.history.types,s.taskType,3);
        return s;
    }

    const damage=d=>d==="training"?5:d==="advanced"?15:d==="expert"?20:10;
    const score=d=>d==="training"?600:d==="advanced"?1000:d==="expert"?1250:800;
    const xp=d=>d==="training"?60:d==="advanced"?100:d==="expert"?125:80;

    let guardedCourse = null;
    let pageHideBound = false;

    function requiredParts(scenario) {
        const specification = scenario?.validator || {};
        const parts = [];

        for (const item of specification.all || []) {
            parts.push(String(item));
        }

        for (const group of specification.any || []) {
            if (Array.isArray(group) && group.length > 0) {
                parts.push(String(group[0]));
            }
        }

        return parts.filter(Boolean);
    }

    function analyzeCode(value, scenario) {
        const code = String(value || "");
        const normalized = compact(code);
        const specification = scenario?.validator || {};
        const all = specification.all || [];
        const any = specification.any || [];

        let matched = 0;

        for (const item of all) {
            if (normalized.includes(compact(item))) {
                matched += 1;
            }
        }

        for (const group of any) {
            if (
                Array.isArray(group) &&
                group.some(item =>
                    normalized.includes(compact(item))
                )
            ) {
                matched += 1;
            }
        }

        const required = Math.max(1, all.length + any.length);
        const target = Math.max(
            12,
            requiredParts(scenario)
                .map(item => compact(item).length)
                .reduce((total, length) => total + length, 0)
        );

        return {
            currentCharacters:
                code.replace(/\s/g, "").length,
            targetCharacters: target,
            structuralAccuracy:
                Math.round(matched * 100 / required),
            matchedElements: matched,
            requiredElements: required
        };
    }

    function buildHintReveal(scenario, percent) {
        const parts = requiredParts(scenario);
        const count = Math.max(
            1,
            Math.ceil(parts.length * percent / 100)
        );

        return parts
            .slice(0, count)
            .join("\n");
    }

    function clearRoomHint(value) {
        value.hintUsed = false;
        value.hintPercent = 0;
        value.hintReveal = "";
        value.hintPenalty = 0;
    }

    function terminateActiveRun(course) {
        const state = load();
        const key = course === "python" ? "python" : "csharp";
        const value = state[key];

        if (!value.activeRun) {
            return;
        }

        value.activeRun = false;
        value.runComplete = false;
        value.runFailed = false;
        value.abandonedRuns =
            Number(value.abandonedRuns || 0) + 1;
        value.lastScore = value.score;
        value.scenario = null;
        value.score = 0;
        value.streak = 0;
        value.mistakes = 0;
        value.primaryResource = 100;
        value.secondaryResource = 0;
        value.threat = 0;
        value.runHintsUsed = 0;
        clearRoomHint(value);

        localStorage.setItem(KEY, JSON.stringify(state));
    }

    function pageHideHandler() {
        if (guardedCourse) {
            terminateActiveRun(guardedCourse);
        }
    }

    window.caveCodeMinigames = {
        getHubState(){return hub(load());},
        getCourseState(course){const s=load(),k=course==="python"?"python":"csharp";return view(s,k);},
        startRun(course,difficulty,endless){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k];
            v.activeRun=true;v.runComplete=false;v.runFailed=false;
            v.endlessMode=Boolean(endless)&&chapters(k)>=5;
            v.difficulty=["training","standard","advanced","expert"].includes(difficulty)?difficulty:"standard";
            v.roomNumber=1;v.roomsTotal=v.endlessMode?10:5;v.score=0;v.streak=0;v.mistakes=0;
            v.primaryResource=100;v.secondaryResource=0;v.threat=0;
            v.runHintsUsed=0;clearRoomHint(v);v.scenario=generate(v,k);
            save(s);return view(s,k);
        },
        validate(course,code){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k];
            if(!v.activeRun||!v.scenario)return{valid:false,heading:"No active run",message:"Start a run first.",systemStatus:"Inactive",state:view(s,k)};
            if(valid(code,v.scenario.validator,k))return{valid:true,heading:"System accepted the code",message:"The scenario logic passed validation.",systemStatus:v.scenario.successStatus,state:view(s,k)};
            const hit=damage(v.difficulty);v.mistakes++;v.streak=0;v.primaryResource=Math.max(0,v.primaryResource-hit);v.threat=Math.min(100,v.threat+hit);
            if(v.primaryResource<=0){v.activeRun=false;v.runFailed=true;v.failedRuns++;v.totalRuns++;v.lastScore=v.score;v.scenario=null;}
            save(s);
            return{valid:false,heading:v.runFailed?"Run failed":"System rejected the code",message:v.runFailed?"The run resource reached zero. Start a new campaign.":v.difficulty==="training"?v.scenario.hint:"One or more required structures, values, or operations are missing.",systemStatus:v.runFailed?"System failure":"Consequence applied",state:view(s,k)};
        },
        complete(course,code){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k],q=v.scenario;
            if(!v.activeRun||!q||!valid(code,q.validator,k))throw new Error("Scenario is not valid.");
            const fresh=!v.clearedVariations[q.id], first=!v.clearedTemplates[q.templateId];
            v.clearedVariations[q.id]=true;v.clearedTemplates[q.templateId]=true;
            const eventScore=Math.max(
                250,
                score(v.difficulty)
                    - v.mistakes * 75
                    - Number(v.hintPenalty || 0)
                    + (v.streak >= 2 ? 100 : 0)
            );
            let awardXp=fresh?xp(v.difficulty):20, crystals=fresh?(first?6:2):0;

            if (v.hintUsed) {
                awardXp = Math.max(
                    0,
                    awardXp - (v.difficulty === "training" ? 10 : 15)
                );
            }
            const h=hash(code), validated=fresh&&!v.codeHashes[h]?lines(code):0;
            v.codeHashes[h]=true;v.totalValidatedLines+=validated;v.score+=eventScore;v.streak++;v.secondaryResource+=Math.max(1,q.chapter);v.threat=Math.max(0,v.threat-8);
            const title=q.title;let runCompleted=false,perfectRun=false;
            if(v.roomNumber>=v.roomsTotal){
                runCompleted=true;
                perfectRun=v.mistakes===0&&Number(v.runHintsUsed||0)===0;
                awardXp+=100;crystals+=5;
                if(perfectRun){awardXp+=150;crystals+=5;}
                v.activeRun=false;v.runComplete=true;v.completedRuns++;v.totalRuns++;v.lastScore=v.score;v.bestScore=Math.max(v.bestScore,v.score);v.scenario=null;
            }else{
                v.roomNumber++;
                clearRoomHint(v);
                v.scenario=generate(v,k);
            }
            v.totalXpEarned+=awardXp;v.totalCrystalsEarned+=crystals;
            const rewardKey=`minigame-v2:${k}:${q.id}:${Date.now()}:${Math.random().toString(36).slice(2,8)}`;
            save(s);
            return{rewardKey,scenarioTitle:title,newVariation:fresh,firstTemplateClear:first,runCompleted,perfectRun,xpAwarded:awardXp,crystalsAwarded:crystals,validatedLines:validated,eventScore,state:view(s,k)};
        },
        analyze(course,code){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k];
            return analyzeCode(code,v.scenario);
        },
        useHint(course){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k];

            if(!v.activeRun||!v.scenario){
                return{allowed:false,message:"Start a run before requesting a hint.",reveal:"",revealPercent:0,scoreCost:0,xpCost:0,state:view(s,k)};
            }

            if(!["training","standard"].includes(v.difficulty)){
                return{allowed:false,message:"Hints are available only in Training and Standard.",reveal:"",revealPercent:0,scoreCost:0,xpCost:0,state:view(s,k)};
            }

            if(v.hintUsed){
                return{allowed:false,message:"The one hint for this room has already been used.",reveal:v.hintReveal,revealPercent:v.hintPercent,scoreCost:v.hintPenalty,xpCost:v.difficulty==="training"?10:15,state:view(s,k)};
            }

            const percent=v.difficulty==="training"?50:35;
            const scoreCost=v.difficulty==="training"?150:200;
            const xpCost=v.difficulty==="training"?10:15;

            v.hintUsed=true;
            v.hintPercent=percent;
            v.hintPenalty=scoreCost;
            v.hintReveal=buildHintReveal(v.scenario,percent);
            v.runHintsUsed=Number(v.runHintsUsed||0)+1;

            save(s);

            return{
                allowed:true,
                message:`Revealed ${percent}% of the required code structure. This room loses ${scoreCost} possible points and ${xpCost} XP. The run is no longer eligible for a perfect-run bonus.`,
                reveal:v.hintReveal,
                revealPercent:percent,
                scoreCost,
                xpCost,
                state:view(s,k)
            };
        },
        resetRun(course){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k];
            const difficulty=v.difficulty||"standard";
            const endless=Boolean(v.endlessMode)&&chapters(k)>=5;

            v.activeRun=true;
            v.runComplete=false;
            v.runFailed=false;
            v.endlessMode=endless;
            v.difficulty=difficulty;
            v.roomNumber=1;
            v.roomsTotal=endless?10:5;
            v.score=0;
            v.streak=0;
            v.mistakes=0;
            v.primaryResource=100;
            v.secondaryResource=0;
            v.threat=0;
            v.runHintsUsed=0;
            clearRoomHint(v);
            v.scenario=generate(v,k);

            save(s);
            return view(s,k);
        },
        bindExitGuard(course){
            guardedCourse=course==="python"?"python":"csharp";

            if(!pageHideBound){
                window.addEventListener("pagehide",pageHideHandler);
                pageHideBound=true;
            }

            return true;
        },
        unbindExitGuard(){
            guardedCourse=null;

            if(pageHideBound){
                window.removeEventListener("pagehide",pageHideHandler);
                pageHideBound=false;
            }

            return true;
        },
        endRun(course){
            const s=load(),k=course==="python"?"python":"csharp",v=s[k];

            if(v.activeRun){
                v.abandonedRuns=Number(v.abandonedRuns||0)+1;
                v.lastScore=v.score;
            }

            v.activeRun=false;
            v.runComplete=false;
            v.runFailed=false;
            v.scenario=null;
            v.score=0;
            v.streak=0;
            v.mistakes=0;
            v.primaryResource=100;
            v.secondaryResource=0;
            v.threat=0;
            v.runHintsUsed=0;
            clearRoomHint(v);

            save(s);
            return view(s,k);
        }
    };
})();


/* CAVECODE_MINIGAME_UX_REFINEMENT_V1 */
(function () {
    const KEY = "cavecode.minigames.v2";
    const api = window.caveCodeMinigames;

    if (!api || typeof api.analyze !== "function" ||
        typeof api.useHint !== "function") {
        console.error(
            "CaveCode minigame refinement could not attach."
        );
        return;
    }

    function normalizeCourse(course) {
        return course === "python" ? "python" : "csharp";
    }

    function loadState() {
        try {
            return JSON.parse(
                localStorage.getItem(KEY) ||
                '{"csharp":{},"python":{}}'
            );
        } catch {
            return { csharp: {}, python: {} };
        }
    }

    function saveState(state) {
        localStorage.setItem(KEY, JSON.stringify(state));

        window.dispatchEvent(
            new CustomEvent(
                "cavecode-minigames-changed"
            )
        );
    }

    function compact(value) {
        return String(value || "")
            .toLowerCase()
            .replace(/\s+/g, "");
    }

    function requiredParts(scenario) {
        const specification =
            scenario?.validator || {};
        const parts = [];

        for (const item of specification.all || []) {
            parts.push(String(item));
        }

        for (const group of specification.any || []) {
            if (Array.isArray(group) &&
                group.length > 0) {
                parts.push(String(group[0]));
            }
        }

        return parts.filter(Boolean);
    }

    function matchedElementCount(code, scenario, course) {
        const value = compact(stripComments(code, course));
        const specification =
            scenario?.validator || {};
        let matched = 0;

        for (const item of specification.all || []) {
            if (value.includes(compact(item))) {
                matched += 1;
            }
        }

        for (const group of specification.any || []) {
            if (
                Array.isArray(group) &&
                group.some(item =>
                    value.includes(compact(item))
                )
            ) {
                matched += 1;
            }
        }

        return matched;
    }

    function stripComments(code, course) {
        let value = String(code || "");

        if (course === "python") {
            value = value
                .split(/\r?\n/)
                .map(line => {
                    const index = line.indexOf("#");
                    return index >= 0
                        ? line.slice(0, index)
                        : line;
                })
                .join("\n");
        } else {
            value = value.replace(
                /\/\*[\s\S]*?\*\//g,
                ""
            );

            value = value
                .split(/\r?\n/)
                .map(line => {
                    const index = line.indexOf("//");
                    return index >= 0
                        ? line.slice(0, index)
                        : line;
                })
                .join("\n");
        }

        return value
            .split(/\r?\n/)
            .map(line => line.trimEnd())
            .filter(line => line.trim().length > 0)
            .join("\n")
            .trim();
    }

    function editorCharacterCount(code) {
        return String(code || "").length;
    }

    function solutionCharacterCount(code, course) {
        return stripComments(code, course).length;
    }

    function targetRange(scenario) {
        const compactTarget =
            requiredParts(scenario)
                .map(part => compact(part).length)
                .reduce(
                    (total, length) =>
                        total + length,
                    0
                );

        const minimum = Math.max(
            8,
            compactTarget
        );

        const maximum = Math.max(
            minimum + 6,
            Math.ceil(minimum * 1.30)
        );

        return {
            minimum,
            maximum
        };
    }

    function capitalizeWords(value) {
        return String(value || "")
            .replace(/([a-z])([A-Z])/g, "$1 $2")
            .replace(/[_-]+/g, " ")
            .replace(/\b\w/g, character =>
                character.toUpperCase()
            );
    }

    function readableFallback(scenario, course) {
        const parts = requiredParts(scenario);
        const first = compact(parts[0] || "");
        const term =
            scenario?.primaryTerm || "value";

        if (course === "csharp") {
            const types = [
                "string",
                "double",
                "bool",
                "int"
            ];

            for (const type of types) {
                if (
                    first.startsWith(
                        type +
                        compact(term)
                    )
                ) {
                    return `${type} ${term} =`;
                }
            }

            if (first.startsWith("return")) {
                return "return ";
            }

            if (first.startsWith("if(")) {
                return "if (condition)\n{\n    \n}";
            }

            if (first.startsWith("while(")) {
                return "while (condition)\n{\n    \n}";
            }

            if (first.startsWith("foreach(")) {
                return "foreach (var item in items)\n{\n    \n}";
            }
        } else {
            if (first.includes("=")) {
                return `${term} =`;
            }

            if (first.startsWith("return")) {
                return "return ";
            }

            if (first.startsWith("if")) {
                return "if condition:\n    ";
            }

            if (first.startsWith("while")) {
                return "while condition:\n    ";
            }

            if (first.startsWith("for")) {
                return "for item in items:\n    ";
            }

            if (first.startsWith("def")) {
                return "def function_name(parameters):\n    ";
            }
        }

        return scenario?.hint ||
            `Begin with ${capitalizeWords(term)}.`;
    }

    function cleanHintSource(source, course) {
        let value = String(source || "")
            .replace(/\r\n/g, "\n");

        const lines = value
            .split("\n")
            .filter(line => {
                const trimmed = line.trim();

                if (!trimmed) {
                    return false;
                }

                if (course === "python") {
                    return !trimmed.startsWith("#");
                }

                return !trimmed.startsWith("//");
            });

        return lines.join("\n").trim();
    }

    function revealSingleLine(line, percent) {
        const value = line.trim();

        if (percent <= 35) {
            const equalsIndex = value.indexOf("=");

            if (equalsIndex >= 0) {
                return value
                    .slice(0, equalsIndex + 1)
                    .trimEnd();
            }

            const braceIndex = value.indexOf("{");

            if (braceIndex >= 0) {
                return value
                    .slice(0, braceIndex)
                    .trimEnd();
            }

            return value;
        }

        return value;
    }

    function buildReadableHint(
        scenario,
        percent,
        course
    ) {
        let source = cleanHintSource(
            scenario?.hintCode ||
            scenario?.starterCode ||
            "",
            course
        );

        if (!source) {
            source = readableFallback(
                scenario,
                course
            );
        }

        const lines = source
            .split("\n")
            .filter(line =>
                line.trim().length > 0
            );

        if (lines.length <= 1) {
            return revealSingleLine(
                lines[0] || source,
                percent
            );
        }

        const lineCount = Math.max(
            1,
            Math.ceil(
                lines.length * percent / 100
            )
        );

        return lines
            .slice(0, lineCount)
            .join("\n");
    }

    api.analyze = function (course, code) {
        const key = normalizeCourse(course);
        const state = loadState();
        const scenario =
            state[key]?.scenario || null;
        const specification =
            scenario?.validator || {};
        const required = Math.max(
            1,
            (specification.all || []).length +
            (specification.any || []).length
        );
        const matched =
            matchedElementCount(
                code,
                scenario,
                key
            );
        const range = targetRange(scenario);
        const completion = Math.min(
            100,
            Math.round(
                matched * 100 / required
            )
        );
        const solutionCharacters =
            solutionCharacterCount(
                code,
                key
            );
        const editorCharacters =
            editorCharacterCount(code);

        return {
            currentCharacters:
                solutionCharacters,
            targetCharacters:
                range.maximum,
            structuralAccuracy:
                completion,
            matchedElements:
                matched,
            requiredElements:
                required,
            solutionCharacters,
            editorCharacters,
            targetMinimum:
                range.minimum,
            targetMaximum:
                range.maximum,
            completionPercent:
                completion
        };
    };

    api.useHint = function (course) {
        const key = normalizeCourse(course);
        const state = loadState();
        const value = state[key] || {};

        if (
            !value.activeRun ||
            !value.scenario
        ) {
            return {
                allowed: false,
                message:
                    "Start a run before requesting a hint.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        if (
            !["training", "standard"]
                .includes(value.difficulty)
        ) {
            return {
                allowed: false,
                message:
                    "Hints are available only in Training and Standard.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        if (value.hintUsed) {
            return {
                allowed: false,
                message:
                    "The one hint for this room has already been used.",
                reveal:
                    value.hintReveal || "",
                revealPercent:
                    Number(value.hintPercent || 0),
                scoreCost:
                    Number(value.hintPenalty || 0),
                xpCost:
                    value.difficulty === "training"
                        ? 10
                        : 15,
                state: value
            };
        }

        const percent =
            value.difficulty === "training"
                ? 50
                : 35;
        const scoreCost =
            value.difficulty === "training"
                ? 150
                : 200;
        const xpCost =
            value.difficulty === "training"
                ? 10
                : 15;

        value.hintUsed = true;
        value.hintPercent = percent;
        value.hintPenalty = scoreCost;
        value.hintReveal =
            buildReadableHint(
                value.scenario,
                percent,
                key
            );
        value.runHintsUsed =
            Number(value.runHintsUsed || 0) + 1;

        saveState(state);

        return {
            allowed: true,
            message:
                `Revealed ${percent}% of a readable solution structure. ` +
                `This room loses ${scoreCost} possible points and ${xpCost} XP. ` +
                "The run is no longer eligible for a perfect-run bonus.",
            reveal: value.hintReveal,
            revealPercent: percent,
            scoreCost,
            xpCost,
            state: value
        };
    };
    // Reformat a hint that was already used before this
    // refinement was installed.
    const initialState = loadState();
    let migratedHint = false;

    for (const key of ["csharp", "python"]) {
        const value = initialState[key];

        if (
            value?.activeRun &&
            value?.hintUsed &&
            value?.scenario
        ) {
            value.hintReveal =
                buildReadableHint(
                    value.scenario,
                    Number(value.hintPercent || 35),
                    key
                );

            migratedHint = true;
        }
    }

    if (migratedHint) {
        saveState(initialState);
    }

})();


/* CAVECODE_MISSING_CODE_HINT_ENGINE_V1 */
(function () {
    const KEY = "cavecode.minigames.v2";
    const api = window.caveCodeMinigames;

    if (!api) {
        console.error(
            "CaveCode missing-code hint engine could not attach."
        );
        return;
    }

    function courseKey(course) {
        return course === "python"
            ? "python"
            : "csharp";
    }

    function loadState() {
        try {
            return JSON.parse(
                localStorage.getItem(KEY) ||
                '{"csharp":{},"python":{}}'
            );
        } catch {
            return {
                csharp: {},
                python: {}
            };
        }
    }

    function saveState(state) {
        localStorage.setItem(
            KEY,
            JSON.stringify(state)
        );

        window.dispatchEvent(
            new CustomEvent(
                "cavecode-minigames-changed"
            )
        );
    }

    function compact(value) {
        return String(value || "")
            .toLowerCase()
            .replace(/\s+/g, "");
    }

    function titleCase(value) {
        return String(value || "")
            .replace(/([a-z])([A-Z])/g, "$1 $2")
            .replace(/[_-]+/g, " ")
            .replace(
                /\b\w/g,
                letter => letter.toUpperCase()
            );
    }

    function restoreQuotedText(
        value,
        scenario
    ) {
        const source = [
            scenario?.title,
            scenario?.brief,
            scenario?.objective,
            scenario?.hint,
            scenario?.starterCode,
            scenario?.hintCode
        ]
            .filter(Boolean)
            .join("\n");

        return String(value || "").replace(
            /(["'])(.*?)\1/g,
            (match, quote, phrase) => {
                if (!phrase) {
                    return match;
                }

                const index = source
                    .toLowerCase()
                    .indexOf(
                        phrase.toLowerCase()
                    );

                if (index < 0) {
                    return match;
                }

                return (
                    quote +
                    source.slice(
                        index,
                        index + phrase.length
                    ) +
                    quote
                );
            }
        );
    }

    function requirements(scenario) {
        const validator =
            scenario?.validator || {};
        const result = [];

        for (const item of validator.all || []) {
            result.push({
                choices: [String(item)]
            });
        }

        for (const group of validator.any || []) {
            if (
                Array.isArray(group) &&
                group.length > 0
            ) {
                result.push({
                    choices:
                        group.map(String)
                });
            }
        }

        return result;
    }

    function stripHintComments(code, course) {
        let value = String(code || "");

        if (course === "python") {
            return value
                .split(/\r?\n/)
                .map(line => {
                    const index = line.indexOf("#");

                    return index >= 0
                        ? line.slice(0, index)
                        : line;
                })
                .join("\n");
        }

        value = value.replace(
            /\/\*[\s\S]*?\*\//g,
            ""
        );

        return value
            .split(/\r?\n/)
            .map(line => {
                const index = line.indexOf("//");

                return index >= 0
                    ? line.slice(0, index)
                    : line;
            })
            .join("\n");
    }

    function requirementPresent(
        code,
        requirement,
        course
    ) {
        const value = compact(
            stripHintComments(
                code,
                course
            )
        );

        return requirement.choices.some(
            choice =>
                value.includes(
                    compact(choice)
                )
        );
    }

    function formatExpression(
        expression,
        course
    ) {
        let value = String(expression || "")
            .trim();

        value = value
            .replace(/&&/g, " && ")
            .replace(/\|\|/g, " || ")
            .replace(/\band\b/g, " and ")
            .replace(/\bor\b/g, " or ")
            .replace(/\bnot\b/g, "not ")
            .replace(/>=/g, " >= ")
            .replace(/<=/g, " <= ")
            .replace(/==/g, " == ")
            .replace(/!=/g, " != ")
            .replace(/\+=/g, " += ")
            .replace(/-=/g, " -= ")
            .replace(/\*=/g, " *= ")
            .replace(/\/=/g, " /= ")
            .replace(/(?<![<>=!+\-*/])=(?!=)/g, " = ")
            .replace(/(?<![<>=])>(?!=)/g, " > ")
            .replace(/(?<![<>=])<(?!=)/g, " < ")
            .replace(/\s+/g, " ")
            .trim();

        if (course === "csharp") {
            value = value
                .replace(/\.add\(/gi, ".Add(")
                .replace(/\.remove\(/gi, ".Remove(")
                .replace(/\.contains\(/gi, ".Contains(");
        }

        return value;
    }

    function formatTypedArguments(value) {
        return String(value || "")
            .replace(
                /\b(int|string|bool|double|float|decimal)([A-Za-z_]\w*)/g,
                "$1 $2"
            )
            .replace(/,/g, ", ");
    }

    function formatCSharp(
        raw,
        scenario
    ) {
        let value = restoreQuotedText(
            String(raw || "").trim(),
            scenario
        );

        if (!value) {
            return "";
        }

        if (/^["'].*["']$/.test(value)) {
            return value;
        }

        let match = value.match(
            /^class([A-Za-z_]\w*)$/
        );

        if (match) {
            return `class ${titleCase(match[1]).replace(/\s/g, "")}`;
        }

        match = value.match(
            /^(void|int|bool|double|string)([A-Za-z_]\w*)\((.*)$/
        );

        if (match) {
            const returnType = match[1];
            const methodName =
                titleCase(match[2])
                    .replace(/\s/g, "");
            const argumentsText =
                formatTypedArguments(
                    match[3]
                );

            return (
                `${returnType} ${methodName}(` +
                argumentsText
            );
        }

        match = value.match(
            /^(int|bool|double|string)([A-Za-z_]\w*)=(.*)$/
        );

        if (match) {
            const right =
                formatExpression(
                    match[3],
                    "csharp"
                );

            return (
                `${match[1]} ${match[2]} =` +
                (right ? ` ${right}` : "") +
                (right ? ";" : "")
            );
        }

        match = value.match(
            /^(int|bool|double|string)([A-Za-z_]\w*)$/
        );

        if (match) {
            return `${match[1]} ${match[2]}`;
        }

        match = value.match(
            /^return(.+)$/
        );

        if (match) {
            return (
                "return " +
                formatExpression(
                    match[1],
                    "csharp"
                ) +
                ";"
            );
        }

        match = value.match(
            /^(if|while)\((.*)\)$/
        );

        if (match) {
            return (
                `${match[1]} (` +
                formatExpression(
                    match[2],
                    "csharp"
                ) +
                ")"
            );
        }

        if (value.startsWith("foreach(")) {
            return "foreach (...)";
        }

        match = value.match(
            /^([A-Za-z_]\w*)(\+=|-=|\*=|\/=|=)(.+)$/
        );

        if (match) {
            return (
                `${match[1]} ${match[2]} ` +
                formatExpression(
                    match[3],
                    "csharp"
                ) +
                ";"
            );
        }

        const formatted =
            formatExpression(
                value,
                "csharp"
            );

        if (
            /\)$/.test(formatted) ||
            /^[A-Za-z_]\w*\./.test(formatted)
        ) {
            return formatted + ";";
        }

        return formatted;
    }

    function formatPython(
        raw,
        scenario
    ) {
        let value = restoreQuotedText(
            String(raw || "").trim(),
            scenario
        );

        if (!value) {
            return "";
        }

        if (/^["'].*["']$/.test(value)) {
            return value;
        }

        let match = value.match(
            /^def([A-Za-z_]\w*)\((.*)\):?$/
        );

        if (match) {
            return (
                `def ${match[1]}(` +
                match[2]
                    .replace(/,/g, ", ") +
                "):"
            );
        }

        match = value.match(
            /^(if|while)(.+):$/
        );

        if (match) {
            return (
                `${match[1]} ` +
                formatExpression(
                    match[2],
                    "python"
                ) +
                ":"
            );
        }

        match = value.match(
            /^for([A-Za-z_]\w*)in(.+):$/
        );

        if (match) {
            return (
                `for ${match[1]} in ` +
                formatExpression(
                    match[2],
                    "python"
                ) +
                ":"
            );
        }

        match = value.match(
            /^return(.+)$/
        );

        if (match) {
            return (
                "return " +
                formatExpression(
                    match[1],
                    "python"
                )
            );
        }

        match = value.match(
            /^([A-Za-z_]\w*)(\+=|-=|\*=|\/=|=)(.*)$/
        );

        if (match) {
            const right =
                formatExpression(
                    match[3],
                    "python"
                );

            return (
                `${match[1]} ${match[2]}` +
                (right ? ` ${right}` : "")
            );
        }

        return formatExpression(
            value,
            "python"
        );
    }

    function formatFragment(
        raw,
        course,
        scenario
    ) {
        return course === "python"
            ? formatPython(raw, scenario)
            : formatCSharp(raw, scenario);
    }

    function standardPartialReveal(
        formatted
    ) {
        const value =
            String(formatted || "").trim();

        if (!value) {
            return "";
        }

        const lines = value.split("\n");

        if (lines.length > 1) {
            return lines[0];
        }

        const assignment = value.match(
            /^(.*?)(\+=|-=|\*=|\/=|(?<![<>=!])=(?!=))(.*)$/
        );

        if (assignment) {
            return (
                assignment[1].trimEnd() +
                " " +
                assignment[2]
            ).trim();
        }

        const comparison = value.match(
            /^(return\s+.*?|if\s*\(.*?|while\s*\(.*?)(>=|<=|==|!=|>|<)(.*)$/
        );

        if (comparison) {
            return (
                comparison[1].trimEnd() +
                " " +
                comparison[2]
            ).trim();
        }

        return value;
    }

    function chooseMissingHint(
        scenario,
        currentCode,
        percent,
        course
    ) {
        const missing =
            requirements(scenario)
                .filter(requirement =>
                    !requirementPresent(
                        currentCode,
                        requirement,
                        course
                    )
                );

        if (missing.length === 0) {
            return {
                reveal: "",
                missingCount: 0,
                totalCount: 0
            };
        }

        const revealCount = Math.max(
            1,
            Math.ceil(
                missing.length *
                percent /
                100
            )
        );

        const reveals = [];

        for (const requirement of missing) {
            let selected = "";

            for (const choice of requirement.choices) {
                const formatted =
                    formatFragment(
                        choice,
                        course,
                        scenario
                    );

                if (
                    formatted &&
                    !compact(stripHintComments(currentCode, course))
                        .includes(
                            compact(formatted)
                        )
                ) {
                    selected = formatted;
                    break;
                }
            }

            if (!selected) {
                continue;
            }

            if (percent <= 35) {
                selected =
                    standardPartialReveal(
                        selected
                    );
            }

            if (
                selected &&
                !reveals.some(
                    existing =>
                        compact(existing) ===
                        compact(selected)
                )
            ) {
                reveals.push(selected);
            }

            if (
                reveals.length >=
                revealCount
            ) {
                break;
            }
        }

        return {
            reveal: reveals.join("\n"),
            missingCount:
                missing.length,
            totalCount:
                requirements(scenario).length
        };
    }

    api.useHint = function (
        course,
        currentCode
    ) {
        const key = courseKey(course);
        const state = loadState();
        const value = state[key] || {};

        if (
            !value.activeRun ||
            !value.scenario
        ) {
            return {
                allowed: false,
                message:
                    "Start a run before requesting a hint.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        if (
            !["training", "standard"]
                .includes(value.difficulty)
        ) {
            return {
                allowed: false,
                message:
                    "Hints are available only in Training and Standard.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        if (value.hintUsed) {
            return {
                allowed: false,
                message:
                    "The one hint for this room has already been used.",
                reveal:
                    value.hintReveal || "",
                revealPercent:
                    Number(
                        value.hintPercent || 0
                    ),
                scoreCost:
                    Number(
                        value.hintPenalty || 0
                    ),
                xpCost:
                    value.difficulty === "training"
                        ? 10
                        : 15,
                state: value
            };
        }

        const percent =
            value.difficulty === "training"
                ? 50
                : 35;
        const scoreCost =
            value.difficulty === "training"
                ? 150
                : 200;
        const xpCost =
            value.difficulty === "training"
                ? 10
                : 15;

        const generated =
            chooseMissingHint(
                value.scenario,
                currentCode || "",
                percent,
                key
            );

        if (!generated.reveal) {
            return {
                allowed: false,
                message:
                    "Every required structure already appears in the editor. Run the system to validate it; no hint cost was charged.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        value.hintUsed = true;
        value.hintPercent = percent;
        value.hintPenalty = scoreCost;
        value.hintReveal =
            generated.reveal;
        value.runHintsUsed =
            Number(
                value.runHintsUsed || 0
            ) + 1;

        saveState(state);

        return {
            allowed: true,
            message:
                `Revealed ${percent}% of the code structure that was still missing. ` +
                `Already-visible starter code was skipped. ` +
                `This room loses ${scoreCost} possible points and ${xpCost} XP. ` +
                "The run is no longer eligible for a perfect-run bonus.",
            reveal:
                value.hintReveal,
            revealPercent:
                percent,
            scoreCost,
            xpCost,
            state: value
        };
    };

    // Repair a hint already used in an active room by comparing
    // it with that room's starter code.
    const initialState = loadState();
    let changed = false;

    for (const key of ["csharp", "python"]) {
        const value = initialState[key];

        if (
            value?.activeRun &&
            value?.hintUsed &&
            value?.scenario
        ) {
            const generated =
                chooseMissingHint(
                    value.scenario,
                    value.scenario.starterCode || "",
                    Number(
                        value.hintPercent || 35
                    ),
                    key
                );

            if (
                generated.reveal &&
                generated.reveal !==
                    value.hintReveal
            ) {
                value.hintReveal =
                    generated.reveal;
                changed = true;
            }
        }
    }

    if (changed) {
        saveState(initialState);
    }
})();


/* CAVECODE_QUESTION_BANK_AUDIT_V1 */
(function () {
    const KEY = "cavecode.minigames.v2";
    const api = window.caveCodeMinigames;

    if (!api) {
        console.error(
            "CaveCode question-bank audit could not attach."
        );
        return;
    }

    function keyFor(course) {
        return course === "python"
            ? "python"
            : "csharp";
    }

    function loadState() {
        try {
            return JSON.parse(
                localStorage.getItem(KEY) ||
                '{"csharp":{},"python":{}}'
            );
        } catch {
            return {
                csharp: {},
                python: {}
            };
        }
    }

    function saveState(state) {
        localStorage.setItem(
            KEY,
            JSON.stringify(state)
        );

        window.dispatchEvent(
            new CustomEvent(
                "cavecode-minigames-changed"
            )
        );
    }

    function compactAudit(value) {
        return String(value || "")
            .toLowerCase()
            .replace(/\s+/g, "");
    }

    function className(value) {
        const text = String(value || "");

        return text
            ? text[0].toUpperCase() +
              text.slice(1)
            : "Creature";
    }

    function replaceDeep(value, from, to) {
        if (typeof value === "string") {
            return value.split(from).join(to);
        }

        if (Array.isArray(value)) {
            return value.map(item =>
                replaceDeep(item, from, to)
            );
        }

        if (
            value &&
            typeof value === "object"
        ) {
            for (const key of Object.keys(value)) {
                value[key] =
                    replaceDeep(
                        value[key],
                        from,
                        to
                    );
            }
        }

        return value;
    }

    function addExistingFieldStarter(
        scenario
    ) {
        if (
            !scenario.starterCode ||
            compactAudit(
                scenario.starterCode
            ).includes(
                `int${scenario.primaryTerm}=`
            )
        ) {
            return;
        }

        scenario.starterCode =
            `int ${scenario.primaryTerm} = 0;\n\n` +
            scenario.starterCode;
    }

    function auditScenario(
        scenario,
        course,
        difficulty
    ) {
        if (!scenario?.templateId) {
            return scenario;
        }

        const template =
            scenario.templateId;
        const parts =
            String(scenario.id || "")
                .split(":");

        // A numeric challenge must never use a state-like
        // door_status identifier.
        if (
            course === "python" &&
            scenario.primaryTerm === "door_status"
        ) {
            replaceDeep(
                scenario,
                "door_status",
                "alarm_count"
            );
            scenario.primaryTerm =
                "alarm_count";
        }

        switch (template) {
            case "cs-double":
                scenario.objective =
                    String(scenario.objective)
                        .replace(
                            /decimal weight/gi,
                            "fractional weight"
                        )
                        .replace(
                            /decimal value/gi,
                            "fractional value"
                        );
                scenario.hint =
                    "Use double for a fractional numeric value.";
                break;

            case "cs-debug": {
                const value =
                    scenario.numberValue;
                const term =
                    scenario.primaryTerm;

                scenario.brief =
                    `The code incorrectly declares ${term} as bool, then tries to assign the whole-number value ${value}.`;
                scenario.objective =
                    `Replace bool with int so ${term} can store ${value}.`;
                scenario.hint =
                    "A whole-number count uses int. A bool can represent only true or false.";
                break;
            }

            case "cs-or":
                scenario.title =
                    scenario.title.replace(
                        "Choose an Escape Tool",
                        "Authorize Either Escape Option"
                    );
                break;

            case "cs-clamp": {
                const maximum =
                    Number(parts.at(-1));

                scenario.brief =
                    `The x coordinate must remain inside the inclusive range 0 through ${maximum}.`;
                scenario.objective =
                    `Assign x = Math.Clamp(x, 0, ${maximum}).`;
                scenario.hint =
                    `Use x = Math.Clamp(x, 0, ${maximum});`;
                scenario.validator = {
                    all: [
                        `x=math.clamp(x,0,${maximum})`
                    ],
                    any: [],
                    none: []
                };
                break;
            }

            case "cs-sequence":
                scenario.brief =
                    String(scenario.brief) +
                    " Assume the three helper methods already exist.";
                scenario.validator.ordered =
                    [...(scenario.validator.all || [])];
                break;

            case "cs-void":
            case "cs-parameter":
                scenario.brief =
                    String(scenario.brief) +
                    ` Assume an existing int field named ${scenario.primaryTerm}.`;

                if (difficulty !== "expert") {
                    addExistingFieldStarter(
                        scenario
                    );
                }
                break;

            case "cs-remove":
                scenario.brief =
                    "An existing equipment list contains the named broken item. Remove that item from the list.";
                break;

            case "cs-contains":
                scenario.brief =
                    "An existing treasure collection may contain the named resource. Store the result of the membership check.";
                break;

            case "cs-foreach":
                scenario.brief =
                    "Assume enemies and InspectEnemy(enemy) already exist. Inspect every enemy in the collection.";
                break;

            case "cs-class": {
                const creature =
                    className(
                        scenario.primaryTerm
                    );

                scenario.brief =
                    `Define ${creature} with public Name, Health, and IsDefeated properties.`;
                scenario.objective =
                    `Create public auto-properties: string Name, int Health, and bool IsDefeated.`;
                scenario.hint =
                    "Each property should use public, its data type, and { get; set; }.";
                scenario.validator.regexAll = [
                    `public\\s+string\\s+Name\\s*\\{\\s*get\\s*;\\s*set\\s*;\\s*\\}`,
                    `public\\s+int\\s+Health\\s*\\{\\s*get\\s*;\\s*set\\s*;\\s*\\}`,
                    `public\\s+bool\\s+IsDefeated\\s*\\{\\s*get\\s*;\\s*set\\s*;\\s*\\}`
                ];
                break;
            }

            case "cs-constructor": {
                const creature =
                    className(parts[2]);
                const startingHealth =
                    Number(parts[3]);

                scenario.brief =
                    `Inside ${creature}, add a constructor that receives name, assigns Name = name, and sets Health = ${startingHealth}.`;
                scenario.objective =
                    `Create ${creature}(string name), then assign both Name and Health.`;
                scenario.hint =
                    "A constructor has the same name as its class and initializes the object's properties.";

                if (difficulty !== "expert") {
                    scenario.starterCode =
`class ${creature}
{
    public string Name { get; set; } = "";
    public int Health { get; set; }

    public ${creature}(string name)
    {
        // Assign Name and Health
    }
}`;
                }

                scenario.validator = {
                    all: [
                        `class${parts[2]}`,
                        `public${parts[2]}(stringname)`,
                        `name=name`,
                        `health=${startingHealth}`
                    ],
                    any: [],
                    none: [],
                    regexAll: [
                        `Name\\s*=\\s*name\\s*;`,
                        `Health\\s*=\\s*${startingHealth}\\s*;`
                    ]
                };
                break;
            }

            case "cs-damage":
                scenario.brief =
                    "Inside an existing creature class with a Health property, TakeDamage(int damage) must reduce Health by damage.";
                break;

            case "cs-object":
                scenario.brief =
                    String(scenario.brief) +
                    " Assume the creature class is already defined.";
                break;

            case "cs-combat":
                scenario.brief =
                    String(scenario.brief) +
                    " Assume enemy and TakeDamage already exist.";
                break;

            case "cs-state":
                scenario.brief =
                    "Inside an existing creature class, set IsDefeated to true when Health is zero or less.";
                break;

            case "py-decimal":
                scenario.title =
                    scenario.title.replace(
                        "Decimal Reading",
                        "Fractional Reading"
                    );
                scenario.objective =
                    String(scenario.objective)
                        .replace(
                            /precise reading/gi,
                            "floating-point reading"
                        )
                        .replace(
                            /precision input/gi,
                            "floating-point input"
                        );
                scenario.hint =
                    "Write the fractional number without quotation marks.";
                scenario.successStatus =
                    "Fractional reading registered";
                break;

            case "py-debug": {
                const value =
                    scenario.numberValue;
                const term =
                    scenario.primaryTerm;

                scenario.brief =
                    `The code puts the numeric reading ${value} inside quotation marks, so Python treats it as text.`;
                scenario.objective =
                    `Remove the quotation marks so ${term} stores the number ${value}.`;
                scenario.hint =
                    "Quoted digits are strings. Numeric values are written without quotes.";
                break;
            }

            case "py-for":
                scenario.brief =
                    "Assume equipment_list and inspect_device(device) already exist. Inspect every device in the list.";
                break;

            case "py-while": {
                const term =
                    scenario.primaryTerm;
                const threshold =
                    Number(parts.at(-1));

                scenario.brief =
                    `Keep reading the sensor while ${term} is below ${threshold}, and assign each new reading back to ${term}.`;
                scenario.objective =
                    `Use while ${term} < ${threshold}: and update ${term} with read_sensor().`;
                scenario.hint =
                    `Inside the loop, use ${term} = read_sensor().`;

                if (difficulty !== "expert") {
                    scenario.starterCode =
`while ${term} < ${threshold}:
    ${term} = read_sensor()`;
                }

                scenario.validator = {
                    all: [
                        `while${term}<${threshold}:`,
                        `${term}=read_sensor()`
                    ],
                    any: [],
                    none: []
                };
                break;
            }

            case "py-sequence": {
                const first =
                    parts[2];
                const second =
                    parts[3];

                scenario.brief =
                    `Start ${first}, wait two seconds with time.sleep(2), then start ${second}. Assume both start functions already exist.`;
                scenario.objective =
                    "Import time and place all three actions in the stated order.";
                scenario.hint =
                    "Use import time, then time.sleep(2) between the two start calls.";

                if (difficulty !== "expert") {
                    scenario.starterCode =
`import time

# Run the three actions in order`;
                }

                scenario.validator = {
                    all: [
                        "importtime",
                        `start_${first}()`,
                        "time.sleep(2)",
                        `start_${second}()`
                    ],
                    any: [],
                    none: [],
                    ordered: [
                        `start_${first}()`,
                        "time.sleep(2)",
                        `start_${second}()`
                    ]
                };
                break;
            }

            case "py-average":
                scenario.brief =
                    "Assume readings is a nonempty list. Calculate its arithmetic average.";
                break;

            case "py-function":
                scenario.brief =
                    "Define start_equipment(name) and print the supplied name parameter.";
                break;

            case "py-return":
                scenario.brief =
                    "Create a rate function that returns volume divided by minutes. Assume minutes is greater than zero.";
                scenario.objective =
                    "Define calculate_rate(volume, minutes) and return volume / minutes.";
                break;

            case "py-class":
                scenario.brief =
                    "Create Equipment so __init__ stores the supplied name, status, and runtime_hours values on self.";
                scenario.objective =
                    "Assign self.name = name, self.status = status, and self.runtime_hours = runtime_hours.";
                scenario.validator = {
                    all: [
                        "classequipment:",
                        "def__init__(self,name,status,runtime_hours):",
                        "self.name=name",
                        "self.status=status",
                        "self.runtime_hours=runtime_hours"
                    ],
                    any: [],
                    none: []
                };
                break;

            case "py-file":
                scenario.brief =
                    String(scenario.brief) +
                    " Write that exact message while the file is open in append mode.";
                break;

            case "py-emergency":
                scenario.brief =
                    String(scenario.brief) +
                    " Assume all four equipment and logging functions already exist.";
                break;
        }

        scenario.questionAuditVersion = 1;
        return scenario;
    }

    const originalGetCourseState =
        api.getCourseState.bind(api);
    const originalStartRun =
        api.startRun.bind(api);
    const originalComplete =
        api.complete.bind(api);
    const originalResetRun =
        typeof api.resetRun === "function"
            ? api.resetRun.bind(api)
            : null;

    function auditStoredCourse(course) {
        const state = loadState();
        const key = keyFor(course);
        const value = state[key];

        if (!value?.scenario) {
            return false;
        }

        auditScenario(
            value.scenario,
            key,
            value.difficulty
        );

        saveState(state);
        return true;
    }

    api.getCourseState = function (course) {
        auditStoredCourse(course);
        return originalGetCourseState(course);
    };

    api.startRun = function (
        course,
        difficulty,
        endless
    ) {
        originalStartRun(
            course,
            difficulty,
            endless
        );

        auditStoredCourse(course);
        return originalGetCourseState(course);
    };

    api.complete = function (course, code) {
        const result =
            originalComplete(course, code);

        auditStoredCourse(course);
        result.state =
            originalGetCourseState(course);

        return result;
    };

    if (originalResetRun) {
        api.resetRun = function (course) {
            originalResetRun(course);
            auditStoredCourse(course);

            return originalGetCourseState(
                course
            );
        };
    }

    auditStoredCourse("csharp");
    auditStoredCourse("python");
})();


/* CAVECODE_AUTHORITATIVE_MISSING_HINT_ENGINE_V2 */
(function () {
    const KEY = "cavecode.minigames.v2";
    const api = window.caveCodeMinigames;

    if (!api) {
        console.error(
            "CaveCode authoritative hint engine could not attach."
        );
        return;
    }

    function keyFor(course) {
        return course === "python" ? "python" : "csharp";
    }

    function loadState() {
        try {
            return JSON.parse(
                localStorage.getItem(KEY) ||
                '{"csharp":{},"python":{}}'
            );
        } catch {
            return { csharp: {}, python: {} };
        }
    }

    function saveState(state) {
        localStorage.setItem(KEY, JSON.stringify(state));
        window.dispatchEvent(
            new CustomEvent("cavecode-minigames-changed")
        );
    }

    function stripComments(code, course) {
        let value = String(code || "");

        if (course === "python") {
            return value
                .split(/\r?\n/)
                .map(line => {
                    const index = line.indexOf("#");
                    return index >= 0 ? line.slice(0, index) : line;
                })
                .join("\n");
        }

        value = value.replace(/\/\*[\s\S]*?\*\//g, "");

        return value
            .split(/\r?\n/)
            .map(line => {
                const index = line.indexOf("//");
                return index >= 0 ? line.slice(0, index) : line;
            })
            .join("\n");
    }

    function compact(value) {
        return String(value || "")
            .toLowerCase()
            .replace(/\s+/g, "");
    }

    function requirements(scenario) {
        const validator = scenario?.validator || {};
        const result = [];

        for (const item of validator.all || []) {
            result.push({ choices: [String(item)] });
        }

        for (const group of validator.any || []) {
            if (Array.isArray(group) && group.length > 0) {
                result.push({ choices: group.map(String) });
            }
        }

        return result;
    }

    function isPresent(code, requirement, course) {
        const value = compact(stripComments(code, course));

        return requirement.choices.some(
            choice => value.includes(compact(choice))
        );
    }

    function restoreQuotedText(value, scenario) {
        const source = [
            scenario?.brief,
            scenario?.objective,
            scenario?.hint,
            scenario?.starterCode,
            scenario?.hintCode
        ].filter(Boolean).join("\n");

        return String(value || "").replace(
            /(["'])(.*?)\1/g,
            (match, quote, phrase) => {
                if (!phrase) return match;

                const index = source
                    .toLowerCase()
                    .indexOf(phrase.toLowerCase());

                return index < 0
                    ? match
                    : quote + source.slice(index, index + phrase.length) + quote;
            }
        );
    }

    function spaceOperators(value) {
        return String(value || "")
            .replace(/>=/g, " >= ")
            .replace(/<=/g, " <= ")
            .replace(/==/g, " == ")
            .replace(/!=/g, " != ")
            .replace(/\+=/g, " += ")
            .replace(/-=/g, " -= ")
            .replace(/\*=/g, " *= ")
            .replace(/\/=/g, " /= ")
            .replace(/(?<![<>=!+\-*/])=(?!=)/g, " = ")
            .replace(/(?<![<>=])>(?!=)/g, " > ")
            .replace(/(?<![<>=])<(?!=)/g, " < ")
            .replace(/\s+/g, " ")
            .trim();
    }

    function formatCSharp(raw, scenario) {
        let value = restoreQuotedText(
            String(raw || "").trim(),
            scenario
        );

        if (!value) return "";

        let match = value.match(
            /^(int|double|bool|string)([A-Za-z_]\w*)=(.*)$/
        );

        if (match) {
            const right = spaceOperators(match[3]);

            return (
                `${match[1]} ${match[2]} =` +
                (right ? ` ${right};` : "")
            );
        }

        match = value.match(
            /^([A-Za-z_]\w*)(\+=|-=|\*=|\/=|=)(.*)$/
        );

        if (match) {
            const right = spaceOperators(match[3]);

            return (
                `${match[1]} ${match[2]}` +
                (right ? ` ${right};` : "")
            );
        }

        match = value.match(/^return(.+)$/);

        if (match) {
            return `return ${spaceOperators(match[1])};`;
        }

        match = value.match(/^(if|while)\((.*)\)$/);

        if (match) {
            return `${match[1]} (${spaceOperators(match[2])})`;
        }

        match = value.match(
            /^(void|int|double|bool|string)([A-Za-z_]\w*)\((.*)$/
        );

        if (match) {
            return `${match[1]} ${match[2]}(${match[3]}`;
        }

        if (/^["'].*["']$/.test(value)) {
            return value;
        }

        const formatted = spaceOperators(value);

        if (
            /\)$/.test(formatted) ||
            /^[A-Za-z_]\w*\./.test(formatted)
        ) {
            return formatted + ";";
        }

        return formatted;
    }

    function formatPython(raw, scenario) {
        let value = restoreQuotedText(
            String(raw || "").trim(),
            scenario
        );

        if (!value) return "";

        let match = value.match(
            /^([A-Za-z_]\w*)(\+=|-=|\*=|\/=|=)(.*)$/
        );

        if (match) {
            const right = spaceOperators(match[3]);

            return (
                `${match[1]} ${match[2]}` +
                (right ? ` ${right}` : "")
            );
        }

        match = value.match(/^return(.+)$/);

        if (match) {
            return `return ${spaceOperators(match[1])}`;
        }

        match = value.match(/^def([A-Za-z_]\w*)\((.*)\):?$/);

        if (match) {
            return `def ${match[1]}(${match[2]}):`;
        }

        match = value.match(/^(if|while)(.+):$/);

        if (match) {
            return `${match[1]} ${spaceOperators(match[2])}:`;
        }

        match = value.match(/^for([A-Za-z_]\w*)in(.+):$/);

        if (match) {
            return `for ${match[1]} in ${spaceOperators(match[2])}:`;
        }

        return spaceOperators(value);
    }

    function formatFragment(raw, course, scenario) {
        return course === "python"
            ? formatPython(raw, scenario)
            : formatCSharp(raw, scenario);
    }

    function partialForStandard(formatted) {
        const value = String(formatted || "").trim();

        const assignment = value.match(
            /^(.*?)(\+=|-=|\*=|\/=|(?<![<>=!])=(?!=))(.*)$/
        );

        if (assignment) {
            return (
                assignment[1].trimEnd() +
                " " +
                assignment[2]
            ).trim();
        }

        return value.split("\n")[0];
    }

    function generateMissingHint(
        scenario,
        currentCode,
        percent,
        course
    ) {
        const missing = requirements(scenario).filter(
            requirement => !isPresent(
                currentCode,
                requirement,
                course
            )
        );

        if (missing.length === 0) {
            return "";
        }

        const revealCount = Math.max(
            1,
            Math.ceil(missing.length * percent / 100)
        );

        const reveals = [];

        for (const requirement of missing) {
            let formatted = "";

            for (const choice of requirement.choices) {
                const candidate = formatFragment(
                    choice,
                    course,
                    scenario
                );

                if (
                    candidate &&
                    !compact(stripComments(currentCode, course))
                        .includes(compact(candidate))
                ) {
                    formatted = candidate;
                    break;
                }
            }

            if (!formatted) continue;

            if (percent <= 35) {
                formatted = partialForStandard(formatted);
            }

            if (
                !reveals.some(
                    item => compact(item) === compact(formatted)
                )
            ) {
                reveals.push(formatted);
            }

            if (reveals.length >= revealCount) {
                break;
            }
        }

        return reveals.join("\n");
    }

    api.hintEngineVersion = "missing-code-v2";

    api.getHintEngineVersion = function () {
        return api.hintEngineVersion;
    };

    api.useHint = function (course, currentCode) {
        const key = keyFor(course);
        const state = loadState();
        const value = state[key] || {};

        if (!value.activeRun || !value.scenario) {
            return {
                allowed: false,
                message: "Start a run before requesting a hint.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        if (!["training", "standard"].includes(value.difficulty)) {
            return {
                allowed: false,
                message: "Hints are available only in Training and Standard.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        if (value.hintUsed) {
            return {
                allowed: false,
                message: "The one hint for this room has already been used.",
                reveal: value.hintReveal || "",
                revealPercent: Number(value.hintPercent || 0),
                scoreCost: Number(value.hintPenalty || 0),
                xpCost: value.difficulty === "training" ? 10 : 15,
                state: value
            };
        }

        const percent =
            value.difficulty === "training" ? 50 : 35;
        const scoreCost =
            value.difficulty === "training" ? 150 : 200;
        const xpCost =
            value.difficulty === "training" ? 10 : 15;

        const reveal = generateMissingHint(
            value.scenario,
            currentCode || "",
            percent,
            key
        );

        if (!reveal) {
            return {
                allowed: false,
                message:
                    "Every required structure already appears in the editor. " +
                    "Run the challenge check; no hint cost was charged.",
                reveal: "",
                revealPercent: 0,
                scoreCost: 0,
                xpCost: 0,
                state: value
            };
        }

        value.hintUsed = true;
        value.hintPercent = percent;
        value.hintPenalty = scoreCost;
        value.hintReveal = reveal;
        value.runHintsUsed =
            Number(value.runHintsUsed || 0) + 1;

        saveState(state);

        return {
            allowed: true,
            message:
                `Revealed ${percent}% of the structure that was still missing. ` +
                `Already-visible starter code was skipped. ` +
                `This room loses ${scoreCost} possible points and ${xpCost} XP. ` +
                "The run is no longer eligible for a perfect-run bonus.",
            reveal,
            revealPercent: percent,
            scoreCost,
            xpCost,
            state: value
        };
    };

    const state = loadState();
    let changed = false;

    for (const key of ["csharp", "python"]) {
        const value = state[key];

        if (
            value?.activeRun &&
            value?.hintUsed &&
            value?.scenario
        ) {
            const repaired = generateMissingHint(
                value.scenario,
                value.scenario.starterCode || "",
                Number(value.hintPercent || 35),
                key
            );

            if (repaired && repaired !== value.hintReveal) {
                value.hintReveal = repaired;
                changed = true;
            }
        }
    }

    if (changed) {
        saveState(state);
    }

    console.info(
        "CaveCode hint engine active:",
        api.hintEngineVersion
    );
})();


/* CAVECODE_CHAPTER_MASTERY_CAMPAIGNS_V1 */
(function () {
    const KEY = "cavecode.minigames.v2";
    const api = window.caveCodeMinigames;

    if (!api) {
        console.error(
            "CaveCode chapter mastery campaigns could not attach."
        );
        return;
    }

    const original = {
        getCourseState: api.getCourseState.bind(api),
        getHubState: api.getHubState.bind(api),
        startRun: api.startRun.bind(api),
        complete: api.complete.bind(api),
        resetRun:
            typeof api.resetRun === "function"
                ? api.resetRun.bind(api)
                : null,
        endRun:
            typeof api.endRun === "function"
                ? api.endRun.bind(api)
                : null
    };

    const CHAPTER_NAMES = {
        csharp: [
            "",
            "Variables and Data Types",
            "Conditions and Decisions",
            "Methods and Movement",
            "Collections and Loops",
            "Classes and Objects"
        ],
        python: [
            "",
            "Variables and Data Types",
            "Conditions and Safety",
            "Loops and Sequences",
            "Collections and Data",
            "Functions and Automation"
        ]
    };

    const CONCEPTS = {
        "cs-int": "int whole numbers",
        "cs-double": "double fractional values",
        "cs-bool": "bool true/false state",
        "cs-string": "string text values",
        "cs-add": "+= compound addition",
        "cs-debug": "correcting a data type",
        "cs-threshold": "single comparison",
        "cs-and": "logical AND",
        "cs-or": "logical OR",
        "cs-range": "bounded numeric range",
        "cs-ifelse": "if / else branch",
        "cs-craft": "resource decision",
        "cs-void": "void method",
        "cs-return": "return value",
        "cs-parameter": "method parameter",
        "cs-move": "coordinate update",
        "cs-clamp": "Math.Clamp",
        "cs-sequence": "ordered method calls",
        "cs-array": "array declaration",
        "cs-list": "List<T>",
        "cs-remove": "list removal",
        "cs-contains": "collection membership",
        "cs-foreach": "foreach iteration",
        "cs-dictionary": "Dictionary<TKey,TValue>",
        "cs-class": "class properties",
        "cs-constructor": "constructor assignment",
        "cs-damage": "object method behavior",
        "cs-object": "object initializer",
        "cs-combat": "while combat loop",
        "cs-state": "object state transition",
        "py-number": "whole-number assignment",
        "py-decimal": "fractional numeric assignment",
        "py-bool": "Boolean state",
        "py-string": "text assignment",
        "py-add": "+= compound addition",
        "py-debug": "numeric data correction",
        "py-threshold": "single comparison",
        "py-and": "logical and",
        "py-or": "logical or",
        "py-range": "bounded numeric range",
        "py-ifelse": "if / else branch",
        "py-safety": "safety decision",
        "py-for": "for loop",
        "py-while": "while loop",
        "py-count": "loop counter",
        "py-break": "break control flow",
        "py-continue": "continue control flow",
        "py-sequence": "ordered equipment sequence",
        "py-list": "list declaration",
        "py-dict": "dictionary declaration",
        "py-append": "list append",
        "py-contains": "membership test",
        "py-average": "average calculation",
        "py-filter": "filtered list",
        "py-function": "function definition",
        "py-return": "function return value",
        "py-file": "file append",
        "py-class": "class initializer",
        "py-relay": "automation function",
        "py-emergency": "emergency sequence",
        "cs-mastery-subtract": "-= compound subtraction",
        "cs-mastery-reassign": "variable reassignment",
        "cs-mastery-not": "logical NOT",
        "cs-mastery-equality": "string equality comparison",
        "cs-mastery-call": "method invocation",
        "cs-mastery-transform": "parameter transformation",
        "cs-mastery-index": "list indexing",
        "cs-mastery-count": "collection Count",
        "cs-mastery-property": "object property assignment",
        "cs-mastery-object-call": "object method call",
        "py-mastery-subtract": "-= compound subtraction",
        "py-mastery-reassign": "variable reassignment",
        "py-mastery-not": "logical not",
        "py-mastery-equality": "string equality comparison",
        "py-mastery-range-loop": "range loop",
        "py-mastery-accumulate": "loop accumulation",
        "py-mastery-index": "list indexing",
        "py-mastery-dict-update": "dictionary value update",
        "py-mastery-function-parameter": "function parameter",
        "py-mastery-method-call": "object method call"
    };

    const FAMILIES = {
        "cs-int": "numeric declaration",
        "cs-double": "numeric declaration",
        "cs-bool": "state declaration",
        "cs-string": "text declaration",
        "cs-add": "arithmetic update",
        "cs-debug": "debugging",
        "cs-threshold": "numeric comparison",
        "cs-and": "combined Boolean logic",
        "cs-or": "combined Boolean logic",
        "cs-range": "numeric comparison",
        "cs-ifelse": "branch decision",
        "cs-craft": "branch decision",
        "cs-void": "method declaration",
        "cs-return": "return method",
        "cs-parameter": "method parameter",
        "cs-move": "coordinate update",
        "cs-clamp": "boundary control",
        "cs-sequence": "method invocation",
        "cs-array": "collection creation",
        "cs-list": "collection creation",
        "cs-remove": "collection mutation",
        "cs-contains": "collection query",
        "cs-foreach": "collection loop",
        "cs-dictionary": "keyed collection",
        "cs-class": "class definition",
        "cs-constructor": "object construction",
        "cs-damage": "object behavior",
        "cs-object": "object creation",
        "cs-combat": "combat loop",
        "cs-state": "object state",
        "py-number": "numeric assignment",
        "py-decimal": "numeric assignment",
        "py-bool": "state assignment",
        "py-string": "text assignment",
        "py-add": "arithmetic update",
        "py-debug": "debugging",
        "py-threshold": "numeric comparison",
        "py-and": "combined Boolean logic",
        "py-or": "combined Boolean logic",
        "py-range": "numeric comparison",
        "py-ifelse": "branch decision",
        "py-safety": "branch decision",
        "py-for": "counted loop",
        "py-while": "condition loop",
        "py-count": "condition loop",
        "py-break": "loop control",
        "py-continue": "loop control",
        "py-sequence": "ordered sequence",
        "py-list": "collection creation",
        "py-dict": "keyed collection",
        "py-append": "collection mutation",
        "py-contains": "collection query",
        "py-average": "data summary",
        "py-filter": "data filtering",
        "py-function": "function definition",
        "py-return": "return behavior",
        "py-file": "file operation",
        "py-class": "object model",
        "py-relay": "automation command",
        "py-emergency": "emergency sequence",
        "cs-mastery-subtract": "arithmetic update",
        "cs-mastery-reassign": "value reassignment",
        "cs-mastery-not": "Boolean negation",
        "cs-mastery-equality": "text comparison",
        "cs-mastery-call": "method invocation",
        "cs-mastery-transform": "return method",
        "cs-mastery-index": "collection indexing",
        "cs-mastery-count": "collection size",
        "cs-mastery-property": "object state",
        "cs-mastery-object-call": "object behavior",
        "py-mastery-subtract": "arithmetic update",
        "py-mastery-reassign": "value reassignment",
        "py-mastery-not": "Boolean negation",
        "py-mastery-equality": "text comparison",
        "py-mastery-range-loop": "counted loop",
        "py-mastery-accumulate": "accumulator loop",
        "py-mastery-index": "collection indexing",
        "py-mastery-dict-update": "collection mutation",
        "py-mastery-function-parameter": "function definition",
        "py-mastery-method-call": "automation command"
    };

    const caveNames = [
        "Ari", "Borin", "Cassia", "Darius", "Ember", "Freya",
        "Galen", "Hazel", "Iris", "Jasper", "Kira", "Luna",
        "Marek", "Nadia", "Orin", "Petra", "Quinn", "Rowan"
    ];
    const caveResources = [
        "amberKeys", "cavePearls", "dragonTeeth", "emberSeeds",
        "frostCrystals", "glowMoss", "ironTokens", "moonShards",
        "obsidianChips", "runeStones", "silverCoins", "starMetal"
    ];
    const caveLocations = [
        "Ashen Gallery", "Crystal Bridge", "Echo Vault",
        "Forgotten Mine", "Granite Hall", "Moonlit Passage",
        "Obsidian Gate", "Rune Chamber", "Silverwater Cave"
    ];
    const facilityItems = [
        "alarm_count", "battery_voltage", "coolant_level",
        "fan_speed", "filter_pressure", "flow_rate",
        "motor_current", "room_temperature", "runtime_hours",
        "steam_pressure", "tank_level", "water_temperature"
    ];
    const equipment = [
        "air_handler", "circulation_pump", "compressor",
        "cooling_fan", "dosing_pump", "exhaust_fan",
        "freezer_unit", "safety_relay", "supply_fan",
        "transfer_pump", "vacuum_pump", "warning_light"
    ];
    const operators = [
        "Amara", "Caleb", "Camila", "Daniel", "Elena", "Grace",
        "Imani", "Jordan", "Luis", "Maya", "Nina", "Owen",
        "Priya", "Riley", "Rosa", "Theo"
    ];

    function keyFor(course) {
        return course === "python" ? "python" : "csharp";
    }

    function clone(value) {
        return value == null
            ? value
            : JSON.parse(JSON.stringify(value));
    }

    function loadRaw() {
        try {
            return JSON.parse(
                localStorage.getItem(KEY) ||
                '{"csharp":{},"python":{}}'
            );
        } catch {
            return { csharp: {}, python: {} };
        }
    }

    function saveRaw(state) {
        localStorage.setItem(KEY, JSON.stringify(state));
        window.dispatchEvent(
            new CustomEvent(
                "cavecode-minigames-changed",
                {
                    detail:
                        typeof api.getHubState === "function"
                            ? api.getHubState()
                            : null
                }
            )
        );
    }

    function completedChapters(course) {
        try {
            const progress = JSON.parse(
                localStorage.getItem(
                    `cavecode.${keyFor(course)}.progress.v1`
                ) || "null"
            );
            const completed =
                progress?.moduleCompleted ??
                progress?.ModuleCompleted ??
                [];
            let count = 0;

            for (let chapter = 1; chapter <= 5; chapter++) {
                if (completed[chapter * 8 - 1] === true) {
                    count += 1;
                }
            }

            return count;
        } catch {
            return 0;
        }
    }

    function pick(list) {
        return list[Math.floor(Math.random() * list.length)];
    }

    function number(minimum, maximum) {
        return Math.floor(
            Math.random() * (maximum - minimum + 1)
        ) + minimum;
    }

    function shuffle(list) {
        const value = [...list];

        for (let index = value.length - 1; index > 0; index--) {
            const other = Math.floor(Math.random() * (index + 1));
            [value[index], value[other]] =
                [value[other], value[index]];
        }

        return value;
    }

    function starterFor(
        difficulty,
        training,
        standard,
        advanced
    ) {
        if (difficulty === "expert") return "";
        if (difficulty === "advanced") return advanced || "";
        if (difficulty === "standard") return standard || advanced || "";
        return training || standard || advanced || "";
    }

    function validator(all, any = []) {
        return { all, any, none: [] };
    }

    function scenarioBase(
        course,
        chapter,
        templateId,
        taskType,
        concept,
        data
    ) {
        return {
            id:
                `${templateId}:${Date.now()}:` +
                Math.random().toString(36).slice(2, 9),
            templateId,
            chapter,
            taskType,
            skill: taskType,
            concept,
            questionFamily:
                FAMILIES[templateId] || taskType,
            hintCode: data.hintCode || data.hint || "",
            visualIcon: course === "python" ? "⌁" : "◆",
            questionAuditVersion: 1,
            ...data
        };
    }

    function buildCustomScenario(course, chapter, slot, difficulty) {
        if (course === "csharp") {
            const name = pick(caveNames);
            const resource = pick(caveResources);
            const location = pick(caveLocations);
            const start = number(40, 130);
            const amount = number(4, 28);
            const finalValue = number(8, 95);

            if (chapter === 1 && slot === 0) {
                return scenarioBase(
                    course, chapter, "cs-mastery-subtract", "update",
                    CONCEPTS["cs-mastery-subtract"],
                    {
                        title: `Spend ${resource} at ${location}`,
                        brief:
                            `${name} begins with ${start} ${resource} and spends ${amount}.`,
                        objective:
                            `Declare int ${resource} = ${start}, then subtract ${amount} with -=.`,
                        hint: `Use ${resource} -= ${amount};`,
                        starterCode: starterFor(
                            difficulty,
                            `int ${resource} = ${start};\n${resource} -= 0;`,
                            `int ${resource} = ${start};\n// Subtract the amount`,
                            `// Update ${resource} after spending ${amount}`
                        ),
                        hintCode: `${resource} -= ${amount};`,
                        systemName: "Supply Ledger",
                        successStatus: `${start - amount} ${resource} remain`,
                        primaryTerm: resource,
                        entity: name,
                        numberValue: amount,
                        validator: validator(
                            [`int${resource}=${start}`],
                            [[
                                `${resource}-=${amount}`,
                                `${resource}=${resource}-${amount}`,
                                `${resource}=${start - amount}`
                            ]]
                        )
                    }
                );
            }

            if (chapter === 1 && slot === 1) {
                return scenarioBase(
                    course, chapter, "cs-mastery-reassign", "update",
                    CONCEPTS["cs-mastery-reassign"],
                    {
                        title: `Recalibrate the ${resource} Count`,
                        brief:
                            `${name} records ${start} ${resource}, then the final verified count becomes ${finalValue}.`,
                        objective:
                            `Declare int ${resource} = ${start}, then reassign ${resource} to ${finalValue}.`,
                        hint: "Use a second assignment without repeating the type.",
                        starterCode: starterFor(
                            difficulty,
                            `int ${resource} = ${start};\n${resource} = 0;`,
                            `int ${resource} = ${start};\n// Store the verified count`,
                            `// Declare and then reassign ${resource}`
                        ),
                        hintCode: `${resource} = ${finalValue};`,
                        systemName: "Inventory Reconciliation",
                        successStatus: `Verified count ${finalValue}`,
                        primaryTerm: resource,
                        entity: name,
                        numberValue: finalValue,
                        validator: validator([
                            `int${resource}=${start}`,
                            `${resource}=${finalValue}`
                        ])
                    }
                );
            }

            if (chapter === 2 && slot === 0) {
                const flag = `has${resource[0].toUpperCase()}${resource.slice(1)}`;
                return scenarioBase(
                    course, chapter, "cs-mastery-not", "condition",
                    CONCEPTS["cs-mastery-not"],
                    {
                        title: `Search ${location} When Supplies Are Missing`,
                        brief:
                            `${name} does not have the required ${resource}. Search only while ${flag} is false.`,
                        objective:
                            `Declare bool ${flag} = false and call SearchCave() inside if (!${flag}).`,
                        hint: `Use if (!${flag}).`,
                        starterCode: starterFor(
                            difficulty,
                            `bool ${flag} = false;\nif (!${flag})\n{\n    \n}`,
                            `bool ${flag} = false;\n// Check the opposite state`,
                            `// Search when ${flag} is false`
                        ),
                        hintCode: `if (!${flag})\n{\n    SearchCave();\n}`,
                        systemName: "Missing Supply Check",
                        successStatus: "Search condition armed",
                        primaryTerm: flag,
                        entity: name,
                        numberValue: 0,
                        validator: validator([
                            `bool${flag}=false`,
                            `if(!${flag})`,
                            "searchcave()"
                        ])
                    }
                );
            }

            if (chapter === 2 && slot === 1) {
                const mode = pick(["OPEN", "READY", "SAFE", "CLEAR"]);
                return scenarioBase(
                    course, chapter, "cs-mastery-equality", "condition",
                    CONCEPTS["cs-mastery-equality"],
                    {
                        title: `Verify the ${location} Gate Mode`,
                        brief:
                            `The gateMode text is ${mode}. Enter only when it equals that exact value.`,
                        objective:
                            `Declare string gateMode = "${mode}" and call EnterGate() inside an equality check.`,
                        hint: "Use == to compare string values.",
                        starterCode: starterFor(
                            difficulty,
                            `string gateMode = "${mode}";\nif (gateMode == "")\n{\n    \n}`,
                            `string gateMode = "${mode}";\n// Compare the mode`,
                            `// Enter only when gateMode equals "${mode}"`
                        ),
                        hintCode: `if (gateMode == "${mode}")\n{\n    EnterGate();\n}`,
                        systemName: "Gate Mode Interlock",
                        successStatus: `${mode} mode accepted`,
                        primaryTerm: "gateMode",
                        entity: location,
                        numberValue: 0,
                        validator: validator([
                            `stringgatemode="${mode.toLowerCase()}"`,
                            `if(gatemode=="${mode.toLowerCase()}")`,
                            "entergate()"
                        ])
                    }
                );
            }

            if (chapter === 3 && slot === 0) {
                const steps = number(2, 12);
                return scenarioBase(
                    course, chapter, "cs-mastery-call", "method",
                    CONCEPTS["cs-mastery-call"],
                    {
                        title: `Move Through ${location}`,
                        brief:
                            `${name} must advance exactly ${steps} steps using the existing MovePlayer method.`,
                        objective: `Call MovePlayer(${steps}).`,
                        hint: "Invoke the existing method and pass the step count.",
                        starterCode: starterFor(
                            difficulty,
                            `MovePlayer(0);`,
                            `// Call MovePlayer with ${steps}`,
                            `// Invoke the movement method`
                        ),
                        hintCode: `MovePlayer(${steps});`,
                        systemName: "Movement Command",
                        successStatus: `${steps} steps issued`,
                        primaryTerm: "MovePlayer",
                        entity: name,
                        numberValue: steps,
                        validator: validator([`moveplayer(${steps})`])
                    }
                );
            }

            if (chapter === 3 && slot === 1) {
                const multiplier = number(2, 5);
                return scenarioBase(
                    course, chapter, "cs-mastery-transform", "method",
                    CONCEPTS["cs-mastery-transform"],
                    {
                        title: `Multiply the ${resource} Reward`,
                        brief:
                            `Create a method that returns an amount multiplied by ${multiplier}.`,
                        objective:
                            `Write int MultiplyReward(int amount) and return amount * ${multiplier}.`,
                        hint: "Use the parameter in the return expression.",
                        starterCode: starterFor(
                            difficulty,
                            `int MultiplyReward(int amount)\n{\n    return 0;\n}`,
                            `int MultiplyReward(int amount)\n{\n    // Return the transformed amount\n}`,
                            `// Create MultiplyReward with an int parameter`
                        ),
                        hintCode:
                            `int MultiplyReward(int amount)\n{\n    return amount * ${multiplier};\n}`,
                        systemName: "Reward Transformer",
                        successStatus: "Reward method compiled",
                        primaryTerm: "MultiplyReward",
                        entity: resource,
                        numberValue: multiplier,
                        validator: validator([
                            "intmultiplyreward(intamount)",
                            `returnamount*${multiplier}`
                        ])
                    }
                );
            }

            if (chapter === 4 && slot === 0) {
                return scenarioBase(
                    course, chapter, "cs-mastery-index", "collection",
                    CONCEPTS["cs-mastery-index"],
                    {
                        title: `Read the First ${resource} Entry`,
                        brief:
                            `An existing List<string> inventory contains the expedition supplies. Store its first entry.`,
                        objective:
                            "Create string firstItem = inventory[0].",
                        hint: "The first list index is zero.",
                        starterCode: starterFor(
                            difficulty,
                            `string firstItem = "";`,
                            `// Read the first inventory item`,
                            `// Store inventory index zero`
                        ),
                        hintCode: "string firstItem = inventory[0];",
                        systemName: "Inventory Indexer",
                        successStatus: "First item selected",
                        primaryTerm: "firstItem",
                        entity: resource,
                        numberValue: 0,
                        validator: validator([
                            "stringfirstitem=inventory[0]"
                        ])
                    }
                );
            }

            if (chapter === 4 && slot === 1) {
                return scenarioBase(
                    course, chapter, "cs-mastery-count", "collection",
                    CONCEPTS["cs-mastery-count"],
                    {
                        title: "Count the Expedition Inventory",
                        brief:
                            "An existing inventory list contains every packed item. Store its Count in itemCount.",
                        objective:
                            "Create int itemCount = inventory.Count.",
                        hint: "List<T> exposes the Count property.",
                        starterCode: starterFor(
                            difficulty,
                            "int itemCount = 0;",
                            "// Store the number of inventory entries",
                            "// Read the collection Count"
                        ),
                        hintCode: "int itemCount = inventory.Count;",
                        systemName: "Inventory Counter",
                        successStatus: "Inventory counted",
                        primaryTerm: "itemCount",
                        entity: resource,
                        numberValue: 0,
                        validator: validator([
                            "intitemcount=inventory.count"
                        ])
                    }
                );
            }

            if (chapter === 5 && slot === 0) {
                const health = number(60, 180);
                return scenarioBase(
                    course, chapter, "cs-mastery-property", "object",
                    CONCEPTS["cs-mastery-property"],
                    {
                        title: `Set the Guardian Health at ${location}`,
                        brief:
                            `An existing enemy object must begin with ${health} Health.`,
                        objective: `Assign enemy.Health = ${health}.`,
                        hint: "Use dot notation to assign an object property.",
                        starterCode: starterFor(
                            difficulty,
                            "enemy.Health = 0;",
                            `// Set enemy Health to ${health}`,
                            "// Assign the object property"
                        ),
                        hintCode: `enemy.Health = ${health};`,
                        systemName: "Enemy State Controller",
                        successStatus: `Enemy health set to ${health}`,
                        primaryTerm: "enemy.Health",
                        entity: location,
                        numberValue: health,
                        validator: validator([
                            `enemy.health=${health}`
                        ])
                    }
                );
            }

            const damage = number(7, 34);
            return scenarioBase(
                course, chapter, "cs-mastery-object-call", "combat",
                CONCEPTS["cs-mastery-object-call"],
                {
                    title: `Strike the Guardian for ${damage}`,
                    brief:
                        `Call the existing enemy object's TakeDamage method with ${damage}.`,
                    objective: `Call enemy.TakeDamage(${damage}).`,
                    hint: "Use dot notation to invoke the object's method.",
                    starterCode: starterFor(
                        difficulty,
                        "enemy.TakeDamage(0);",
                        `// Apply ${damage} damage to enemy`,
                        "// Invoke the enemy method"
                    ),
                    hintCode: `enemy.TakeDamage(${damage});`,
                    systemName: "Combat Command",
                    successStatus: `${damage} damage issued`,
                    primaryTerm: "enemy.TakeDamage",
                    entity: pick(caveNames),
                    numberValue: damage,
                    validator: validator([
                        `enemy.takedamage(${damage})`
                    ])
                }
            );
        }

        const reading = pick(facilityItems);
        const device = pick(equipment);
        const operator = pick(operators);
        const start = number(40, 150);
        const amount = number(3, 28);
        const finalValue = number(8, 96);

        if (chapter === 1 && slot === 0) {
            return scenarioBase(
                course, chapter, "py-mastery-subtract", "update",
                CONCEPTS["py-mastery-subtract"],
                {
                    title: `Reduce ${reading}`,
                    brief:
                        `${operator} records ${reading} at ${start}, then it decreases by ${amount}.`,
                    objective:
                        `Assign ${start} to ${reading}, then subtract ${amount} with -=.`,
                    hint: `Use ${reading} -= ${amount}.`,
                    starterCode: starterFor(
                        difficulty,
                        `${reading} = ${start}\n${reading} -= 0`,
                        `${reading} = ${start}\n# Subtract the change`,
                        `# Update ${reading} after a decrease`
                    ),
                    hintCode: `${reading} -= ${amount}`,
                    systemName: "Trend Adjustment",
                    successStatus: `${reading} reduced to ${start - amount}`,
                    primaryTerm: reading,
                    entity: operator,
                    numberValue: amount,
                    validator: validator(
                        [`${reading}=${start}`],
                        [[
                            `${reading}-=${amount}`,
                            `${reading}=${reading}-${amount}`,
                            `${reading}=${start - amount}`
                        ]]
                    )
                }
            );
        }

        if (chapter === 1 && slot === 1) {
            return scenarioBase(
                course, chapter, "py-mastery-reassign", "update",
                CONCEPTS["py-mastery-reassign"],
                {
                    title: `Verify the Final ${reading}`,
                    brief:
                        `${operator} first enters ${start}, then confirms the corrected value is ${finalValue}.`,
                    objective:
                        `Assign ${start} to ${reading}, then reassign it to ${finalValue}.`,
                    hint: "Use the same variable name in a second assignment.",
                    starterCode: starterFor(
                        difficulty,
                        `${reading} = ${start}\n${reading} = 0`,
                        `${reading} = ${start}\n# Store the verified value`,
                        `# Assign and then reassign ${reading}`
                    ),
                    hintCode: `${reading} = ${finalValue}`,
                    systemName: "Reading Reconciliation",
                    successStatus: `${reading} verified at ${finalValue}`,
                    primaryTerm: reading,
                    entity: operator,
                    numberValue: finalValue,
                    validator: validator([
                        `${reading}=${start}`,
                        `${reading}=${finalValue}`
                    ])
                }
            );
        }

        if (chapter === 2 && slot === 0) {
            const flag = `${device}_running`;
            return scenarioBase(
                course, chapter, "py-mastery-not", "condition",
                CONCEPTS["py-mastery-not"],
                {
                    title: `Start ${device} When It Is Off`,
                    brief:
                        `${operator} confirms ${device} is not running. Start it only when ${flag} is false.`,
                    objective:
                        `Set ${flag} = False, then call start_${device}() inside if not ${flag}.`,
                    hint: `Use if not ${flag}:`,
                    starterCode: starterFor(
                        difficulty,
                        `${flag} = False\nif not ${flag}:\n    pass`,
                        `${flag} = False\n# Check the opposite state`,
                        `# Start ${device} when it is not running`
                    ),
                    hintCode:
                        `if not ${flag}:\n    start_${device}()`,
                    systemName: "Off-State Permissive",
                    successStatus: `${device} start permitted`,
                    primaryTerm: flag,
                    entity: device,
                    numberValue: 0,
                    validator: validator([
                        `${flag}=false`,
                        `ifnot${flag}:`,
                        `start_${device}()`
                    ])
                }
            );
        }

        if (chapter === 2 && slot === 1) {
            const mode = pick(["AUTO", "READY", "RUN", "SAFE"]);
            return scenarioBase(
                course, chapter, "py-mastery-equality", "condition",
                CONCEPTS["py-mastery-equality"],
                {
                    title: `Verify ${device} Mode`,
                    brief:
                        `The mode text is ${mode}. Enable ${device} only when it equals that value.`,
                    objective:
                        `Set mode = "${mode}" and call enable_${device}() inside an equality check.`,
                    hint: "Use == to compare text values.",
                    starterCode: starterFor(
                        difficulty,
                        `mode = "${mode}"\nif mode == "":\n    pass`,
                        `mode = "${mode}"\n# Compare the operating mode`,
                        `# Enable ${device} only in ${mode} mode`
                    ),
                    hintCode:
                        `if mode == "${mode}":\n    enable_${device}()`,
                    systemName: "Mode Interlock",
                    successStatus: `${mode} mode accepted`,
                    primaryTerm: "mode",
                    entity: device,
                    numberValue: 0,
                    validator: validator([
                        `mode="${mode.toLowerCase()}"`,
                        `ifmode=="${mode.toLowerCase()}":`,
                        `enable_${device}()`
                    ])
                }
            );
        }

        if (chapter === 3 && slot === 0) {
            const count = number(3, 9);
            return scenarioBase(
                course, chapter, "py-mastery-range-loop", "loop",
                CONCEPTS["py-mastery-range-loop"],
                {
                    title: `Run ${count} Inspection Cycles`,
                    brief:
                        `${operator} needs exactly ${count} calls to inspect_${device}().`,
                    objective:
                        `Use for cycle in range(${count}) and call inspect_${device}() inside the loop.`,
                    hint: "range(count) controls the number of iterations.",
                    starterCode: starterFor(
                        difficulty,
                        `for cycle in range(${count}):\n    pass`,
                        `# Repeat the inspection ${count} times`,
                        `# Build a range loop`
                    ),
                    hintCode:
                        `for cycle in range(${count}):\n    inspect_${device}()`,
                    systemName: "Inspection Scheduler",
                    successStatus: `${count} cycles scheduled`,
                    primaryTerm: "cycle",
                    entity: device,
                    numberValue: count,
                    validator: validator([
                        `forcycleinrange(${count}):`,
                        `inspect_${device}()`
                    ])
                }
            );
        }

        if (chapter === 3 && slot === 1) {
            const count = number(3, 7);
            return scenarioBase(
                course, chapter, "py-mastery-accumulate", "loop",
                CONCEPTS["py-mastery-accumulate"],
                {
                    title: `Accumulate ${count} Sensor Samples`,
                    brief:
                        `An existing readings list holds values. Add the first ${count} readings into total.`,
                    objective:
                        `Set total = 0, loop through readings[:${count}], and add each reading to total.`,
                    hint: "Initialize the accumulator before the loop.",
                    starterCode: starterFor(
                        difficulty,
                        `total = 0\nfor reading in readings[:${count}]:\n    total += 0`,
                        `total = 0\n# Add each selected reading`,
                        `# Build an accumulator loop`
                    ),
                    hintCode:
                        `for reading in readings[:${count}]:\n    total += reading`,
                    systemName: "Sample Accumulator",
                    successStatus: "Samples accumulated",
                    primaryTerm: "total",
                    entity: reading,
                    numberValue: count,
                    validator: validator([
                        "total=0",
                        `forreadinginreadings[:${count}]:`,
                        "total+=reading"
                    ])
                }
            );
        }

        if (chapter === 4 && slot === 0) {
            return scenarioBase(
                course, chapter, "py-mastery-index", "collection",
                CONCEPTS["py-mastery-index"],
                {
                    title: "Read the First Stored Sensor Value",
                    brief:
                        "An existing readings list contains stored process values. Save the first one as first_reading.",
                    objective:
                        "Assign first_reading = readings[0].",
                    hint: "Python list indexes begin at zero.",
                    starterCode: starterFor(
                        difficulty,
                        "first_reading = None",
                        "# Read the first list value",
                        "# Store readings index zero"
                    ),
                    hintCode: "first_reading = readings[0]",
                    systemName: "Reading Indexer",
                    successStatus: "First reading selected",
                    primaryTerm: "first_reading",
                    entity: reading,
                    numberValue: 0,
                    validator: validator([
                        "first_reading=readings[0]"
                    ])
                }
            );
        }

        if (chapter === 4 && slot === 1) {
            const status = pick(["RUNNING", "READY", "OFF", "ALARM"]);
            return scenarioBase(
                course, chapter, "py-mastery-dict-update", "collection",
                CONCEPTS["py-mastery-dict-update"],
                {
                    title: `Update ${device} Status`,
                    brief:
                        `An existing status dictionary must store ${status} under the ${device} key.`,
                    objective:
                        `Assign status["${device}"] = "${status}".`,
                    hint: "Use square brackets with the dictionary key.",
                    starterCode: starterFor(
                        difficulty,
                        `status["${device}"] = ""`,
                        `# Update the ${device} dictionary entry`,
                        "# Assign a dictionary value"
                    ),
                    hintCode:
                        `status["${device}"] = "${status}"`,
                    systemName: "Status Dictionary",
                    successStatus: `${device} marked ${status}`,
                    primaryTerm: "status",
                    entity: device,
                    numberValue: 0,
                    validator: validator([
                        `status["${device}"]="${status.toLowerCase()}"`
                    ])
                }
            );
        }

        if (chapter === 5 && slot === 0) {
            const factor = number(2, 5);
            return scenarioBase(
                course, chapter, "py-mastery-function-parameter", "function",
                CONCEPTS["py-mastery-function-parameter"],
                {
                    title: `Scale the ${reading} Reading`,
                    brief:
                        `Create a reusable function that returns its value parameter multiplied by ${factor}.`,
                    objective:
                        `Write def scale_reading(value): and return value * ${factor}.`,
                    hint: "Use the function parameter in the return expression.",
                    starterCode: starterFor(
                        difficulty,
                        "def scale_reading(value):\n    return 0",
                        "def scale_reading(value):\n    # Return the scaled value",
                        "# Define scale_reading with one parameter"
                    ),
                    hintCode:
                        `def scale_reading(value):\n    return value * ${factor}`,
                    systemName: "Reading Scaler",
                    successStatus: "Scaling function ready",
                    primaryTerm: "scale_reading",
                    entity: reading,
                    numberValue: factor,
                    validator: validator([
                        "defscale_reading(value):",
                        `returnvalue*${factor}`
                    ])
                }
            );
        }

        return scenarioBase(
            course, chapter, "py-mastery-method-call", "automation",
            CONCEPTS["py-mastery-method-call"],
            {
                title: `Start the ${device} Object`,
                brief:
                    `An existing ${device} object exposes a start() method. Invoke it.`,
                objective: `Call ${device}.start().`,
                hint: "Use dot notation to call the object's method.",
                starterCode: starterFor(
                    difficulty,
                    `${device}.start`,
                    `# Start the ${device} object`,
                    "# Invoke the equipment method"
                ),
                hintCode: `${device}.start()`,
                systemName: "Equipment Command",
                successStatus: `${device} start command sent`,
                primaryTerm: `${device}.start`,
                entity: operator,
                numberValue: 0,
                validator: validator([
                    `${device}.start()`
                ])
            }
        );
    }

    function ensureConcept(scenario) {
        if (!scenario) return scenario;

        scenario.concept =
            scenario.concept ||
            CONCEPTS[scenario.templateId] ||
            String(scenario.templateId || scenario.skill || "coding")
                .replace(/^cs-|^py-/, "")
                .replace(/[-_]+/g, " ");

        scenario.questionFamily =
            scenario.questionFamily ||
            FAMILIES[scenario.templateId] ||
            scenario.taskType ||
            "coding structure";

        scenario.hintCode =
            scenario.hintCode ||
            scenario.hint ||
            "";

        if (scenario.validator) {
            scenario.validator.none =
                scenario.validator.none || [];
        }

        return scenario;
    }

    function collectOriginalBuckets(
        course,
        difficulty,
        chapters
    ) {
        const key = keyFor(course);
        const previousRaw = localStorage.getItem(KEY);
        const buckets = Object.fromEntries(
            chapters.map(chapter => [chapter, new Map()])
        );
        const usedVariations = new Set();
        let attempts = 0;
        const maximumAttempts = Math.max(1200, chapters.length * 500);

        try {
            while (
                chapters.some(chapter => buckets[chapter].size < 6) &&
                attempts < maximumAttempts
            ) {
                attempts += 1;
                original.startRun(course, difficulty, false);

                const scenario =
                    loadRaw()[key]?.scenario;

                if (!scenario) continue;

                const chapter = Number(scenario.chapter || 0);
                const bucket = buckets[chapter];

                if (!bucket || bucket.size >= 6) continue;
                if (!scenario.templateId) continue;
                if (bucket.has(scenario.templateId)) continue;
                if (usedVariations.has(scenario.id)) continue;

                const prepared = ensureConcept(clone(scenario));
                bucket.set(prepared.templateId, prepared);
                usedVariations.add(prepared.id);
            }
        } finally {
            if (previousRaw === null) {
                localStorage.removeItem(KEY);
            } else {
                localStorage.setItem(KEY, previousRaw);
            }
        }

        for (const chapter of chapters) {
            if (buckets[chapter].size !== 6) {
                throw new Error(
                    `Could not build six original Chapter ${chapter} styles ` +
                    `after ${attempts} randomized attempts.`
                );
            }
        }

        return Object.fromEntries(
            chapters.map(chapter => [
                chapter,
                [...buckets[chapter].values()]
            ])
        );
    }

    function smartOrder(buckets, previousOrder) {
        const source = Object.values(buckets).flat();
        const prior = Array.isArray(previousOrder)
            ? previousOrder
            : [];
        const avoidAdjacentChapter =
            Object.keys(buckets).length > 1;

        function attempt(requireChapterChange) {
            let visited = 0;
            const maximumVisited = 250000;

            function search(remaining, ordered) {
                visited += 1;

                if (visited > maximumVisited) {
                    return null;
                }

                if (remaining.length === 0) {
                    return ordered;
                }

                const previous = ordered.at(-1);
                let candidates = remaining
                    .map((scenario, index) => ({
                        scenario,
                        index,
                        random: Math.random()
                    }))
                    .filter(item =>
                        !previous ||
                        item.scenario.questionFamily !==
                            previous.questionFamily
                    );

                if (previous && requireChapterChange) {
                    candidates = candidates.filter(item =>
                        item.scenario.chapter !== previous.chapter
                    );
                }

                if (candidates.length === 0) {
                    return null;
                }

                candidates.sort((left, right) => {
                    function pressure(item) {
                        const familyRemaining = remaining.filter(
                            scenario =>
                                scenario.questionFamily ===
                                item.scenario.questionFamily
                        ).length;
                        const chapterRemaining = remaining.filter(
                            scenario =>
                                scenario.chapter ===
                                item.scenario.chapter
                        ).length;
                        const samePositionPenalty =
                            prior[ordered.length] ===
                            item.scenario.templateId
                                ? 40
                                : 0;
                        const firstPenalty =
                            ordered.length === 0 &&
                            prior[0] === item.scenario.templateId
                                ? 60
                                : 0;

                        return (
                            familyRemaining * 100 +
                            chapterRemaining * 12 -
                            samePositionPenalty -
                            firstPenalty +
                            item.random
                        );
                    }

                    return pressure(right) - pressure(left);
                });

                for (const candidate of candidates) {
                    const nextRemaining = [...remaining];
                    nextRemaining.splice(candidate.index, 1);

                    const result = search(
                        nextRemaining,
                        [...ordered, candidate.scenario]
                    );

                    if (result) {
                        return result;
                    }
                }

                return null;
            }

            return search([...source], []);
        }

        let ordered = null;

        for (let retry = 0; retry < 8 && !ordered; retry++) {
            ordered = attempt(avoidAdjacentChapter);
        }

        // Family separation is the non-negotiable rule. If a future
        // chapter bank makes chapter alternation mathematically impossible,
        // preserve family separation and relax only the chapter boundary.
        if (!ordered) {
            for (let retry = 0; retry < 8 && !ordered; retry++) {
                ordered = attempt(false);
            }
        }

        if (!ordered) {
            throw new Error(
                "The question bank could not produce a no-similar-neighbor order."
            );
        }

        const matchesPrevious =
            prior.length === ordered.length &&
            ordered.every(
                (scenario, index) =>
                    scenario.templateId === prior[index]
            );

        if (matchesPrevious) {
            for (let retry = 0; retry < 8; retry++) {
                const alternative = attempt(avoidAdjacentChapter);

                if (
                    alternative &&
                    alternative.some(
                        (scenario, index) =>
                            scenario.templateId !== prior[index]
                    )
                ) {
                    ordered = alternative;
                    break;
                }
            }
        }

        return ordered;
    }

    function buildPlan(
        course,
        difficulty,
        mode,
        selectedChapter,
        completed,
        previousOrder
    ) {
        const chapters =
            mode === "focused"
                ? [selectedChapter]
                : Array.from(
                    { length: completed },
                    (_, index) => index + 1
                );

        const buckets = collectOriginalBuckets(
            course,
            difficulty,
            chapters
        );

        for (const chapter of chapters) {
            buckets[chapter].push(
                buildCustomScenario(
                    course,
                    chapter,
                    0,
                    difficulty
                ),
                buildCustomScenario(
                    course,
                    chapter,
                    1,
                    difficulty
                )
            );

            if (buckets[chapter].length !== 8) {
                throw new Error(
                    `Chapter ${chapter} did not produce eight unique styles.`
                );
            }
        }

        const ordered = smartOrder(buckets, previousOrder);
        const templateIds = ordered.map(item => item.templateId);

        if (new Set(templateIds).size !== ordered.length) {
            throw new Error(
                "A duplicate structural style entered the campaign plan."
            );
        }

        return ordered.map(ensureConcept);
    }

    function clearHintState(value) {
        value.hintUsed = false;
        value.hintPercent = 0;
        value.hintReveal = "";
        value.hintPenalty = 0;
    }

    function practiceLabel(course, mode, chapter, completed) {
        const key = keyFor(course);

        if (mode === "focused") {
            return `Chapter ${chapter}: ${CHAPTER_NAMES[key][chapter]}`;
        }

        return `Chapters 1–${completed} cumulative mastery`;
    }

    function augment(course, state) {
        const key = keyFor(course);
        const completed = completedChapters(key);
        const raw = loadRaw()[key] || {};
        const result = {
            ...(state || {}),
            completedChapters: completed,
            unlockedChapters: completed,
            practiceUnlocked: completed >= 1,
            practiceMode: raw.practiceMode || "",
            selectedChapter: Number(raw.selectedChapter || 0),
            practiceLabel:
                raw.practiceLabel ||
                (completed >= 1
                    ? `${completed * 8} unlocked structural styles`
                    : "Complete Chapter 1 to unlock"),
            runSeed: raw.runSeed || "",
            uniqueStyles:
                Number(raw.uniqueStyles || completed * 8)
        };

        if (result.scenario) {
            result.scenario = ensureConcept(result.scenario);
        }

        return result;
    }

    api.getCourseState = function (course) {
        return augment(
            course,
            original.getCourseState(course)
        );
    };

    api.getHubState = function () {
        const state = original.getHubState();

        return {
            cSharp: augment("csharp", state.cSharp),
            python: augment("python", state.python)
        };
    };

    api.startPractice = function (
        course,
        difficulty,
        mode,
        chapter
    ) {
        const key = keyFor(course);
        const completed = completedChapters(key);
        const normalizedMode =
            mode === "focused" ? "focused" : "mastery";
        const selected = Number(chapter || 0);

        if (completed < 1) {
            throw new Error(
                "Complete Chapter 1 before starting its minigame practice."
            );
        }

        if (
            normalizedMode === "focused" &&
            (selected < 1 || selected > completed)
        ) {
            throw new Error(
                `Chapter ${selected} practice is still locked.`
            );
        }

        const before = loadRaw()[key] || {};
        const plan = buildPlan(
            key,
            difficulty,
            normalizedMode,
            selected,
            completed,
            before.lastPracticeOrder
        );
        const seed =
            `${Date.now().toString(36)}-` +
            Math.random().toString(36).slice(2, 10);

        // Initialize a fully normalized base state through the current
        // audited engine, then replace its random room with the fixed plan.
        original.startRun(key, difficulty, false);

        const state = loadRaw();
        const value = state[key] || {};

        value.activeRun = true;
        value.runComplete = false;
        value.runFailed = false;
        value.endlessMode = false;
        value.difficulty =
            ["training", "standard", "advanced", "expert"]
                .includes(difficulty)
                ? difficulty
                : "standard";
        value.roomNumber = 1;
        value.roomsTotal = plan.length;
        value.score = 0;
        value.streak = 0;
        value.mistakes = 0;
        value.primaryResource = 100;
        value.secondaryResource = 0;
        value.threat = 0;
        value.runHintsUsed = 0;
        clearHintState(value);
        value.practiceMode = normalizedMode;
        value.selectedChapter =
            normalizedMode === "focused"
                ? selected
                : completed;
        value.practiceLabel = practiceLabel(
            key,
            normalizedMode,
            selected,
            completed
        );
        value.runSeed = seed;
        value.uniqueStyles = plan.length;
        value.practicePlanVersion = 1;
        value.practicePlanIndex = 0;
        value.practicePlan = clone(plan);
        value.lastPracticeOrder =
            plan.map(item => item.templateId);
        value.scenario = clone(plan[0]);

        saveRaw(state);

        return augment(
            key,
            original.getCourseState(key)
        );
    };

    // Any legacy non-endless launch now respects chapter completion and
    // starts cumulative mastery rather than the old five-room preview.
    api.startRun = function (course, difficulty, endless) {
        const completed = completedChapters(course);

        if (completed < 1) {
            throw new Error(
                "Complete Chapter 1 before starting a minigame run."
            );
        }

        return api.startPractice(
            course,
            difficulty,
            "mastery",
            completed
        );
    };

    api.complete = function (course, code) {
        const key = keyFor(course);
        const beforeState = loadRaw();
        const before = beforeState[key] || {};
        const planned =
            before.practicePlanVersion === 1 &&
            Array.isArray(before.practicePlan) &&
            before.practicePlan.length === Number(before.roomsTotal || 0);
        const plan = planned
            ? clone(before.practicePlan)
            : null;
        const previousIndex = Number(before.practicePlanIndex || 0);
        const metadata = planned
            ? {
                practiceMode: before.practiceMode,
                selectedChapter: before.selectedChapter,
                practiceLabel: before.practiceLabel,
                runSeed: before.runSeed,
                uniqueStyles: before.uniqueStyles,
                practicePlanVersion: before.practicePlanVersion,
                lastPracticeOrder: clone(before.lastPracticeOrder)
            }
            : null;

        const result = original.complete(key, code);

        if (!planned) {
            result.state = augment(key, result.state);
            return result;
        }

        const state = loadRaw();
        const value = state[key];

        Object.assign(value, metadata);
        value.practicePlan = plan;

        if (!result.runCompleted) {
            const nextIndex = previousIndex + 1;

            if (!plan[nextIndex]) {
                throw new Error(
                    "The saved mastery plan ended before the run counter."
                );
            }

            value.practicePlanIndex = nextIndex;
            value.scenario = clone(plan[nextIndex]);
            clearHintState(value);
        } else {
            value.practicePlanIndex = plan.length;
            value.scenario = null;
        }

        saveRaw(state);
        result.state = augment(
            key,
            original.getCourseState(key)
        );

        return result;
    };

    api.resetRun = function (course) {
        const key = keyFor(course);
        const state = loadRaw();
        const value = state[key] || {};

        if (value.practicePlanVersion === 1) {
            return api.startPractice(
                key,
                value.difficulty || "standard",
                value.practiceMode || "mastery",
                Number(value.selectedChapter || completedChapters(key))
            );
        }

        if (original.resetRun) {
            return augment(key, original.resetRun(key));
        }

        return api.startPractice(
            key,
            value.difficulty || "standard",
            "mastery",
            completedChapters(key)
        );
    };

    if (original.endRun) {
        api.endRun = function (course) {
            const key = keyFor(course);
            original.endRun(key);
            const state = loadRaw();
            const value = state[key] || {};

            value.practiceMode = "";
            value.selectedChapter = 0;
            value.practiceLabel = "";
            value.runSeed = "";
            value.uniqueStyles = completedChapters(key) * 8;
            value.practicePlanVersion = 0;
            value.practicePlanIndex = 0;
            value.practicePlan = [];

            saveRaw(state);
            return augment(
                key,
                original.getCourseState(key)
            );
        };
    }

    // Close an unfinished legacy five-room run once. New runs always use
    // the chapter plan, while completed scores and lifetime totals remain.
    const migrated = loadRaw();
    let migrationChanged = false;

    for (const key of ["csharp", "python"]) {
        const value = migrated[key];

        if (
            value?.activeRun &&
            value.practicePlanVersion !== 1
        ) {
            value.activeRun = false;
            value.runComplete = false;
            value.runFailed = false;
            value.lastScore = Number(value.score || 0);
            value.scenario = null;
            value.score = 0;
            value.streak = 0;
            value.mistakes = 0;
            value.primaryResource = 100;
            value.secondaryResource = 0;
            value.threat = 0;
            migrationChanged = true;
        }
    }

    if (migrationChanged) {
        saveRaw(migrated);
    }

    api.chapterMasteryVersion = "chapter-mastery-v1";
    api.getChapterMasteryVersion = function () {
        return api.chapterMasteryVersion;
    };

    console.info(
        "CaveCode chapter mastery campaigns active:",
        api.chapterMasteryVersion
    );
})();
