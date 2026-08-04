#!/usr/bin/env python3
from pathlib import Path
import base64, html, json, re, shutil, sys

HOME=Path("Pages/Home.razor")
INDEX=Path("wwwroot/index.html")
CSS=Path("wwwroot/css/cavecode-static-language-panels.css")
JS=Path("wwwroot/js/cavecode-static-language-icons.js")
BACKUP=Path(".replace-learning-paths-section-backup")
MARKER="CAVECODE_REPLACED_LEARNING_PATHS_SECTION_V1"

CARDS=json.loads('[{"class": "available csharp-card", "mark": "C#", "status": "<span class=\\"status available-status\\">Available now</span>", "title": "C# Cave Adventure", "desc": "Build a growing cave-exploration game while mastering C# foundations, logic, collections, classes, and combat.", "resume": "<CourseResume Course=\\"csharp\\" />", "tags": ["40 modules", "8-stage practice", "Live game preview"], "action": "<NavLink class=\\"path-action\\" href=\\"/csharp\\">Enter the cave →</NavLink>", "uses": [["gamepad-2", "Unity game development"], ["monitor", "Windows desktop applications"], ["globe-2", "ASP.NET websites and web APIs"], ["cloud", "Enterprise and cloud software"], ["smartphone", "Cross-platform apps with .NET MAUI"], ["blocks", "Tools, simulations, and business systems"]], "examples": "RimWorld, Stardew Valley, Hollow Knight, Cities: Skylines, Lethal Company, Subnautica"}, {"class": "available python-card", "mark": "Py", "status": "<span class=\\"status available-status\\">Available now</span>", "title": "Python Automation Quest", "desc": "Restore an underground facility while learning Python through sensors, alarms, sequences, data, files, and Raspberry Pi concepts.", "resume": "<CourseResume Course=\\"python\\" />", "tags": ["40 modules", "Automation simulation", "Optional hardware path"], "action": "<NavLink class=\\"path-action\\" href=\\"/python\\">Enter the control room →</NavLink>", "uses": [["bot", "Artificial intelligence and machine learning"], ["settings-2", "Automation and scripting"], ["chart-column-big", "Data analysis and visualization"], ["building-2", "Building automation and BMS integrations"], ["shield-check", "Cybersecurity and administration tools"], ["wrench", "Robotics and Raspberry Pi projects"]], "examples": "Instagram, Dropbox, Home Assistant, TensorFlow, PyTorch"}, {"class": "available cpp-card", "mark": "C++", "status": "<span class=\\"status available-status\\">Available now</span>", "title": "C++ Engine Foundry", "desc": "Build a real-time engine workshop while learning program structure, variables, input, output, operators, debugging, and systems thinking.", "resume": "", "tags": ["40 modules", "Engine simulation", "Chapter 1 playable"], "action": "<NavLink class=\\"path-action\\" href=\\"/cpp\\">Enter the foundry →</NavLink>", "uses": [["gamepad-2", "AAA game development"], ["hammer", "Unreal Engine and custom game engines"], ["cpu", "Operating systems and system software"], ["plug-zap", "Embedded systems and device software"], ["rocket", "High-performance applications"], ["bot", "Robotics, graphics, and real-time simulation"]], "examples": "Baldur\'s Gate 3, Elden Ring, Counter-Strike 2, Half-Life 2, Cyberpunk 2077"}, {"class": "available htmlcss-card", "mark": "HTML", "status": "<span class=\\"status available-status\\">Available now</span>", "title": "HTML & CSS Workshop", "desc": "Build the structure and visual systems behind polished websites, responsive interfaces, landing pages, dashboards, and browser-game menus.", "resume": "", "tags": ["40 modules", "Live browser preview", "Chapter 1 playable"], "action": "<NavLink class=\\"path-action\\" href=\\"/html-css\\">Enter the workshop →</NavLink>", "uses": [["layout-template", "Website structure and page layout"], ["palette", "Visual design, themes, and animation"], ["smartphone", "Responsive phone and tablet layouts"], ["panels-top-left", "User interfaces and landing pages"], ["mail", "Styled email templates"], ["gamepad-2", "Browser-game menus and HUDs"]], "examples": "The visible structure and styling behind nearly every website"}, {"class": "locked javascript-card", "mark": "JS", "status": "<span class=\\"status locked-status\\"><LockMark Compact=\\"true\\" /> Coming soon</span>", "title": "JavaScript Web Forge", "desc": "Create interactive websites and browser games while learning the language of the modern web.", "resume": "", "tags": ["Web apps", "Browser games", "Interfaces"], "action": "<button class=\\"path-action locked-action\\" type=\\"button\\" disabled>Course in development</button>", "uses": [["globe-2", "Interactive websites"], ["gamepad-2", "Browser games"], ["panels-top-left", "Front-end web applications"], ["server", "Node.js servers and APIs"], ["smartphone", "Mobile and desktop apps"], ["blocks", "Browser extensions and interface tools"]], "examples": "Google Maps web features, Discord web, Netflix interfaces, interactive pages across the web"}, {"class": "locked sql-card", "mark": "SQL", "status": "<span class=\\"status locked-status\\"><LockMark Compact=\\"true\\" /> Coming soon</span>", "title": "SQL Database Dungeon", "desc": "Master queries by managing players, items, quests, and persistent world data.", "resume": "", "tags": ["Queries", "Databases", "Game data"], "action": "<button class=\\"path-action locked-action\\" type=\\"button\\" disabled>Course in development</button>", "uses": [["database", "Storing and retrieving application data"], ["chart-column-big", "Reports, dashboards, and analytics"], ["gamepad-2", "Player accounts, inventories, and save data"], ["building-2", "Business and enterprise databases"], ["server", "Website and application backends"], ["search", "Searching, filtering, and combining large data sets"]], "examples": "PostgreSQL, Microsoft SQL Server, MySQL, SQLite, Oracle Database"}, {"class": "locked typescript-card", "mark": "TS", "status": "<span class=\\"status locked-status\\"><LockMark Compact=\\"true\\" /> Coming soon</span>", "title": "TypeScript Application Architect", "desc": "Scale JavaScript into dependable applications with types, reusable systems, and safer team workflows.", "resume": "", "tags": ["Typed JavaScript", "Large applications", "Modern frameworks"], "action": "<button class=\\"path-action locked-action\\" type=\\"button\\" disabled>Course in development</button>", "uses": [["building-2", "Large, maintainable web applications"], ["atom", "React, Angular, and Vue projects"], ["server", "Node.js servers and APIs"], ["smartphone", "Cross-platform desktop and mobile apps"], ["cloud", "Cloud services and development tools"], ["flask-conical", "Safer team projects with type checking"]], "examples": "Visual Studio Code, Angular, Deno, modern web-development tooling"}, {"class": "locked java-card", "mark": "Java", "status": "<span class=\\"status locked-status\\"><LockMark Compact=\\"true\\" /> Coming soon</span>", "title": "Java Enterprise Expedition", "desc": "Build durable applications while learning the language behind Android systems, business software, and large backends.", "resume": "", "tags": ["Android", "Enterprise", "Back-end systems"], "action": "<button class=\\"path-action locked-action\\" type=\\"button\\" disabled>Course in development</button>", "uses": [["smartphone", "Android applications"], ["building-2", "Enterprise business software"], ["server", "Back-end services and APIs"], ["cloud", "Large cloud and distributed systems"], ["gamepad-2", "Desktop games and tools"], ["wrench", "Build tools and developer platforms"]], "examples": "Minecraft: Java Edition, IntelliJ IDEA, Jenkins, Hadoop"}, {"class": "locked go-card", "mark": "Go", "status": "<span class=\\"status locked-status\\"><LockMark Compact=\\"true\\" /> Coming soon</span>", "title": "Go Cloud Command", "desc": "Build fast network services and infrastructure tools while learning clear, practical concurrent programming.", "resume": "", "tags": ["Cloud", "DevOps", "Distributed systems"], "action": "<button class=\\"path-action locked-action\\" type=\\"button\\" disabled>Course in development</button>", "uses": [["cloud", "Cloud infrastructure"], ["server", "Fast web APIs and services"], ["terminal-square", "DevOps and command-line tools"], ["network", "Networking and distributed systems"], ["package", "Containers and deployment platforms"], ["chart-no-axes-combined", "Monitoring and backend systems"]], "examples": "Docker, Kubernetes, Terraform, Prometheus"}, {"class": "locked rust-card", "mark": "Rust", "status": "<span class=\\"status locked-status\\"><LockMark Compact=\\"true\\" /> Coming soon</span>", "title": "Rust Systems Frontier", "desc": "Explore memory-safe systems programming through reliable tools, embedded projects, WebAssembly, and performance work.", "resume": "", "tags": ["Memory safety", "Systems", "WebAssembly"], "action": "<button class=\\"path-action locked-action\\" type=\\"button\\" disabled>Course in development</button>", "uses": [["zap", "Fast and memory-safe system software"], ["terminal-square", "Command-line tools"], ["globe-2", "WebAssembly applications"], ["plug-zap", "Embedded and low-level development"], ["server", "Reliable servers and networking"], ["gamepad-2", "Game engines and performance-heavy tools"]], "examples": "Firefox components, ripgrep, parts of Deno, Linux kernel support"}]')
CSS_TEXT=base64.b64decode('Ci5zdGF0aWMtbGFuZ3VhZ2UtcGFuZWx7ZGlzcGxheTpncmlkO2dhcDoxMnB4O21hcmdpbjoxOHB4IDAgMjBweDtwYWRkaW5nOjE1cHg7YmFja2dyb3VuZDpsaW5lYXItZ3JhZGllbnQoMTQ1ZGVnLGNvbG9yLW1peChpbiBzcmdiLHZhcigtLWFjY2VudC1zdXJmYWNlKSA3MiUsdmFyKC0tc3VyZmFjZSkpLHZhcigtLXN1cmZhY2Utc29mdCkpO2JvcmRlcjoxcHggc29saWQgdmFyKC0tYWNjZW50LWJvcmRlcik7Ym9yZGVyLXJhZGl1czoxMnB4fQouc3RhdGljLWxhbmd1YWdlLXBhbmVsX190aXRsZXtkaXNwbGF5OmZsZXg7YWxpZ24taXRlbXM6Y2VudGVyO2dhcDo4cHg7bWFyZ2luOjA7Y29sb3I6dmFyKC0tdGV4dCk7Zm9udC1zaXplOjExcHg7Zm9udC13ZWlnaHQ6OTUwO2xldHRlci1zcGFjaW5nOi4wNzVlbTt0ZXh0LXRyYW5zZm9ybTp1cHBlcmNhc2V9Ci5zdGF0aWMtbGFuZ3VhZ2UtcGFuZWxfX3RpdGxlLWljb24sLnN0YXRpYy1sYW5ndWFnZS1wYW5lbF9faWNvbntkaXNwbGF5OmlubGluZS1ncmlkO3BsYWNlLWl0ZW1zOmNlbnRlcjtjb2xvcjp2YXIoLS1hY2NlbnQpO2JhY2tncm91bmQ6Y29sb3ItbWl4KGluIHNyZ2IsdmFyKC0tYWNjZW50KSAxMiUsdmFyKC0tc3VyZmFjZS1yYWlzZWQpKTtib3JkZXI6MXB4IHNvbGlkIHZhcigtLWFjY2VudC1ib3JkZXIpO2JveC1zaGFkb3c6MCA2cHggMTRweCBjb2xvci1taXgoaW4gc3JnYix2YXIoLS1hY2NlbnQtZ2xvdykgMTQlLHRyYW5zcGFyZW50KX0KLnN0YXRpYy1sYW5ndWFnZS1wYW5lbF9fdGl0bGUtaWNvbnt3aWR0aDoyNHB4O2hlaWdodDoyNHB4O2JvcmRlci1yYWRpdXM6N3B4fS5zdGF0aWMtbGFuZ3VhZ2UtcGFuZWxfX2ljb257d2lkdGg6MjlweDtoZWlnaHQ6MjlweDtib3JkZXItcmFkaXVzOjhweH0KLnN0YXRpYy1sYW5ndWFnZS1wYW5lbF9fdGl0bGUtaWNvbiBzdmd7d2lkdGg6MTRweDtoZWlnaHQ6MTRweH0uc3RhdGljLWxhbmd1YWdlLXBhbmVsX19pY29uIHN2Z3t3aWR0aDoxNXB4O2hlaWdodDoxNXB4O3N0cm9rZS13aWR0aDoyLjF9Ci5zdGF0aWMtbGFuZ3VhZ2UtcGFuZWxfX2xpc3R7ZGlzcGxheTpncmlkO2dyaWQtdGVtcGxhdGUtY29sdW1uczpyZXBlYXQoMixtaW5tYXgoMCwxZnIpKTtnYXA6OHB4IDExcHg7cGFkZGluZzowO21hcmdpbjowO2xpc3Qtc3R5bGU6bm9uZX0KLnN0YXRpYy1sYW5ndWFnZS1wYW5lbF9faXRlbXtkaXNwbGF5OmdyaWQ7Z3JpZC10ZW1wbGF0ZS1jb2x1bW5zOjI5cHggbWlubWF4KDAsMWZyKTtnYXA6OHB4O2FsaWduLWl0ZW1zOmNlbnRlcjtjb2xvcjp2YXIoLS10ZXh0LW11dGVkKTtmb250LXNpemU6MTFweDtmb250LXdlaWdodDo3MjA7bGluZS1oZWlnaHQ6MS4zNX0KLnN0YXRpYy1sYW5ndWFnZS1wYW5lbF9fZXhhbXBsZXN7bWluLWhlaWdodDozNnB4O3BhZGRpbmctdG9wOjEwcHg7bWFyZ2luOjA7Y29sb3I6dmFyKC0tdGV4dC1tdXRlZCk7Ym9yZGVyLXRvcDoxcHggc29saWQgdmFyKC0tYm9yZGVyKTtmb250LXNpemU6MTBweDtsaW5lLWhlaWdodDoxLjU1fQouc3RhdGljLWxhbmd1YWdlLXBhbmVsX19leGFtcGxlcyBzdHJvbmd7Y29sb3I6dmFyKC0tdGV4dCk7Zm9udC13ZWlnaHQ6OTAwfS5wYXRoLWdyaWQtLXN0YXRpY3thbGlnbi1pdGVtczpzdHJldGNofS5wYXRoLWdyaWQtLXN0YXRpYyAucGF0aC1jYXJke2Rpc3BsYXk6ZmxleDtoZWlnaHQ6MTAwJTtmbGV4LWRpcmVjdGlvbjpjb2x1bW59LnBhdGgtZ3JpZC0tc3RhdGljIC5wYXRoLWNhcmQgLnBhdGgtYWN0aW9ue21hcmdpbi10b3A6YXV0b30KQG1lZGlhIChtYXgtd2lkdGg6NjIwcHgpey5zdGF0aWMtbGFuZ3VhZ2UtcGFuZWxfX2xpc3R7Z3JpZC10ZW1wbGF0ZS1jb2x1bW5zOjFmcn19Cg==').decode()
JS_TEXT=base64.b64decode('CigoKT0+eyJ1c2Ugc3RyaWN0Ijtjb25zdCByPSgpPT53aW5kb3cubHVjaWRlPy5jcmVhdGVJY29ucz8uKHthdHRyczp7ImFyaWEtaGlkZGVuIjoidHJ1ZSJ9fSk7Y29uc3Qgcz0oKT0+e3IoKTtjb25zdCBvPW5ldyBNdXRhdGlvbk9ic2VydmVyKCgpPT57Y2xlYXJUaW1lb3V0KHMudCk7cy50PXNldFRpbWVvdXQociw2MCl9KTtvLm9ic2VydmUoZG9jdW1lbnQuZG9jdW1lbnRFbGVtZW50LHtjaGlsZExpc3Q6dHJ1ZSxzdWJ0cmVlOnRydWV9KX07ZG9jdW1lbnQucmVhZHlTdGF0ZT09PSJsb2FkaW5nIj9kb2N1bWVudC5hZGRFdmVudExpc3RlbmVyKCJET01Db250ZW50TG9hZGVkIixzLHtvbmNlOnRydWV9KTpzKCl9KSgpOwo=').decode()

def find_root():
    p=Path.cwd().resolve()
    while p.parent!=p and not (p/"CaveCode.csproj").is_file():
        p=p.parent
    if not (p/"CaveCode.csproj").is_file():
        raise RuntimeError("Run from /workspaces/CaveCode-Academy.")
    return p

def backup(base, rel):
    src=base/rel
    dst=base/BACKUP/rel
    dst.parent.mkdir(parents=True,exist_ok=True)
    if src.exists() and not dst.exists():
        shutil.copy2(src,dst)

def card_markup(card):
    rows=[]
    for icon_name,label in card["uses"][:6]:
        rows += [
            '                            <li class="static-language-panel__item">',
            '                                <span class="static-language-panel__icon" aria-hidden="true"><i data-lucide="'+html.escape(icon_name)+'"></i></span>',
            '                                <span>'+html.escape(label)+'</span>',
            '                            </li>',
        ]
    tags=['                        <span>'+html.escape(tag)+'</span>' for tag in card["tags"]]
    lines=[
        '                <article class="path-card '+card["class"]+'">',
        '                    <div class="path-topline">',
        '                        <span class="language-mark">'+html.escape(card["mark"])+'</span>',
        '                        '+card["status"],
        '                    </div>',
        '                    <h3>'+html.escape(card["title"])+'</h3>',
        '                    <p>'+html.escape(card["desc"])+'</p>',
    ]
    if card["resume"]:
        lines.append('                    '+card["resume"])
    lines += [
        '                    <section class="static-language-panel">',
        '                        <h4 class="static-language-panel__title">',
        '                            <span class="static-language-panel__title-icon" aria-hidden="true"><i data-lucide="sparkles"></i></span>',
        '                            What this language is used for',
        '                        </h4>',
        '                        <ul class="static-language-panel__list">',
    ]
    lines += rows
    lines += [
        '                        </ul>',
        '                        <p class="static-language-panel__examples"><strong>Famous examples:</strong> '+html.escape(card["examples"])+'</p>',
        '                    </section>',
        '                    <div class="skill-tags">',
    ]
    lines += tags
    lines += [
        '                    </div>',
        '                    '+card["action"],
        '                </article>',
    ]
    return "\n".join(lines)

def section_markup():
    return "\n".join([
        '        <section class="path-section" id="paths">',
        '            <!-- '+MARKER+' -->',
        '            <div class="section-heading learning-paths-heading-centered-v2">',
        '                <div>',
        '                    <h2>Choose what you want to build</h2>',
        '                </div>',
        '                <span class="path-count">4 available · 6 in development · 10 total</span>',
        '            </div>',
        '',
        '            <div class="path-grid path-grid--static">',
        "\n\n".join(card_markup(card) for card in CARDS),
        '            </div>',
        '        </section>',
    ])

def replace_section(text):
    start=text.find('<section class="path-section" id="paths">')
    if start<0:
        raise RuntimeError("Could not find Learning Paths section start.")
    token_re=re.compile(r"<section\b|</section>",re.I)
    depth=0
    end=-1
    for match in token_re.finditer(text,start):
        token=match.group(0).lower()
        depth += 1 if token.startswith("<section") else -1
        if depth==0:
            end=match.end()
            break
    if end<0:
        raise RuntimeError("Could not find Learning Paths section end.")
    return text[:start]+section_markup()+text[end:]

def patch_index(text):
    text=re.sub(r'^[ \t]*<script[^>]+learning-path-(?:discovery|real-world-uses)\.js[^>]*></script>[ \t]*\r?\n?',"",text,flags=re.M|re.I)
    text=re.sub(r'^[ \t]*<link[^>]+learning-path-(?:discovery|real-world-uses)\.css[^>]*>[ \t]*\r?\n?',"",text,flags=re.M|re.I)
    if "cavecode-static-language-panels.css" not in text:
        anchor='<link href="CaveCode.styles.css" rel="stylesheet" />'
        if anchor not in text:
            raise RuntimeError("Missing CSS anchor.")
        text=text.replace(anchor,anchor+'\n    <link rel="stylesheet" href="css/cavecode-static-language-panels.css?v=1" />',1)
    if "cavecode-static-language-icons.js" not in text:
        anchor="</body>"
        if anchor not in text:
            raise RuntimeError("Missing body anchor.")
        text=text.replace(anchor,'    <script src="js/cavecode-static-language-icons.js?v=1"></script>\n'+anchor,1)
    return text

def main():
    base=find_root()
    for rel in (HOME,INDEX,CSS,JS):
        backup(base,rel)
    home_path=base/HOME
    index_path=base/INDEX
    home=replace_section(home_path.read_text(encoding="utf-8"))
    index=patch_index(index_path.read_text(encoding="utf-8"))
    checks={
        "marker":MARKER in home,
        "ten cards":home.count('<article class="path-card ')==10,
        "sixty rows":home.count('static-language-panel__item')==60,
        "no old paragraphs":'class="path-examples"' not in home,
        "css linked":"cavecode-static-language-panels.css" in index,
        "js linked":"cavecode-static-language-icons.js" in index,
        "lucide retained":"lucide" in index.lower(),
    }
    failed=[k for k,v in checks.items() if not v]
    if failed:
        raise RuntimeError("Validation failed: "+", ".join(failed))
    home_path.write_text(home,encoding="utf-8",newline="\n")
    index_path.write_text(index,encoding="utf-8",newline="\n")
    (base/CSS).write_text(CSS_TEXT.strip()+"\n",encoding="utf-8",newline="\n")
    (base/JS).write_text(JS_TEXT.strip()+"\n",encoding="utf-8",newline="\n")
    print("Learning Paths section replaced successfully.")
    print("  - 10 uniform cards")
    print("  - 6 Lucide rows per card")
    print("  - theme-colored icons")
    print("  - famous examples")
    print("  - no runtime discovery dependency")
    print("Backup:",str(BACKUP)+"/")
    print("Next: dotnet build && dotnet run")

if __name__=="__main__":
    try:
        main()
    except Exception as error:
        print("ERROR:",error,file=sys.stderr)
        raise SystemExit(1)
