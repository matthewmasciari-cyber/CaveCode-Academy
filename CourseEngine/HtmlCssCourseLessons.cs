namespace CaveCode.CourseEngine;

/// <summary>
/// Full HTML &amp; CSS Interface Workshop curriculum: 5 chapters × 8 modules = 40 lessons.
/// </summary>
public static class HtmlCssCourseLessons
{
    public const int PlayableModuleCount = 40;
    public const int ChapterCount = 5;
    public const int ModulesPerChapter = 8;

    public static IReadOnlyList<CourseLesson> All { get; } =
        new[]
        {
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Document structure",
                "Build the Workshop Page Skeleton",
                "Every HTML page needs a document type, an html root, a head for metadata, and a body for visible content.",
                "<!DOCTYPE html>\n<html>\n<head></head>\n<body></body>\n</html>",
                "<!DOCTYPE html>\n<html>\n<head></head>\n<body></body>\n</html>",
                "<!DOCTYPE ___>\n<html>\n<head></head>\n<body></body>\n</html>",
                "Which section holds the visible page content?",
                new[] { "DOCTYPE", "body", "head", "html only" },
                1,
                "The body contains what the user sees on the page.",
                "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\n</html",
                "Keep the body closed and the document tags balanced.",
                "Build the minimal skeleton with html, head, and body.",
                "Build a skeleton that also includes an empty title in head later; for now keep head and body empty.",
                "<!DOCTYPE html>\n<html>\n<head></head>\n<body></body>\n</html>",
                "The Workshop page skeleton is online."
            )
            {
                ConceptPoints = new[] { "DOCTYPE declares HTML5.", "head holds metadata.", "body holds visible content." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Text content",
                "Add Headings and Paragraphs",
                "Headings rank importance from h1 to h6. Paragraphs wrap normal text in p tags.",
                "<h1>Title</h1>\n<p>Intro text.</p>",
                "<h1>Interface Workshop</h1>\n<p>Build polished web layouts.</p>",
                "<h1>___</h1>\n<p>___</p>",
                "Which tag is the main page heading?",
                new[] { "DOCTYPE", "h6", "h1", "p" },
                2,
                "h1 is the top-level heading for the page title.",
                "<h1>Interface Workshop\n<p>Build polished web layouts.</p>",
                "Close each heading and paragraph tag.",
                "Add an h1 and a paragraph about the workshop.",
                "Add an h2 named Practice Bay and a short paragraph.",
                "<h2>Practice Bay</h2>\n<p>Try each layout skill.</p>",
                "The workshop now has readable text hierarchy."
            )
            {
                ConceptPoints = new[] { "h1 introduces the page.", "p holds body copy.", "Heading order should stay logical." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Links",
                "Connect the Workshop Navigation",
                "The a tag creates links. The href attribute sets the destination URL or page section.",
                "<a href=\"/home\">Home</a>",
                "<a href=\"/projects\">Projects</a>",
                "<a href=\"___\">Projects</a>",
                "Which attribute sets a link destination?",
                new[] { "class", "alt", "href", "src" },
                2,
                "href provides the hypertext reference for the link.",
                "<a>/projects</a>",
                "Put the destination in an href attribute.",
                "Link the text Projects to /projects.",
                "Link the text Home to /home.",
                "<a href=\"/home\">Home</a>",
                "Workshop navigation links are connected."
            )
            {
                ConceptPoints = new[] { "a creates links.", "href is the destination.", "Link text should be clear." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Images",
                "Place an Accessible Project Image",
                "The img tag embeds an image. src is the file path and alt describes the image for accessibility.",
                "<img src=\"photo.png\" alt=\"A cave\">",
                "<img src=\"workshop.png\" alt=\"Students building interfaces\">",
                "<img src=\"___\" alt=\"___\">",
                "Why is alt important?",
                new[] { "It changes the image size", "It describes the image when it cannot be seen", "It is required to color the image", "It replaces src always" },
                1,
                "alt text helps screen readers and appears if the image fails to load.",
                "<img src=\"workshop.png\">",
                "Include a meaningful alt attribute on content images.",
                "Add workshop.png with helpful alt text.",
                "Add banner.png with alt Studio entrance.",
                "<img src=\"banner.png\" alt=\"Studio entrance\">",
                "An accessible project image is on the page."
            )
            {
                ConceptPoints = new[] { "src points to the file.", "alt describes the image.", "Images need accessible text." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Lists",
                "Build the Feature List",
                "Unordered lists use ul with li items. Ordered lists use ol when sequence matters.",
                "<ul>\n  <li>One</li>\n</ul>",
                "<ul>\n  <li>Live preview</li>\n  <li>Theme tokens</li>\n</ul>",
                "<ul>\n  <li>___</li>\n  <li>___</li>\n</ul>",
                "Which tag wraps each list item?",
                new[] { "p", "ul", "li", "href" },
                2,
                "li marks one item inside ul or ol.",
                "<ul>\n  <li>Live preview\n  <li>Theme tokens</li>\n</ul>",
                "Close every li tag.",
                "Build a two-item feature list for the workshop.",
                "Build a two-item checklist for layout and color.",
                "<ul>\n  <li>Layout</li>\n  <li>Color</li>\n</ul>",
                "The feature list presents workshop highlights."
            )
            {
                ConceptPoints = new[] { "ul starts an unordered list.", "li is each item.", "Lists organize related points." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Semantic sections",
                "Divide the Page into Meaningful Regions",
                "Semantic tags like header, main, and footer describe page regions for structure and accessibility.",
                "<main>\n  <p>Content</p>\n</main>",
                "<header></header>\n<main></main>\n<footer></footer>",
                "<___></___>\n<main></main>\n<footer></footer>",
                "Which region is best for the primary page content?",
                new[] { "main", "head", "DOCTYPE", "footer" },
                0,
                "main identifies the dominant content of the document.",
                "<header>\n<main>\n<footer>",
                "Close each semantic region tag.",
                "Add empty header, main, and footer regions.",
                "Add empty nav and main regions.",
                "<nav></nav>\n<main></main>",
                "The page is divided into meaningful regions."
            )
            {
                ConceptPoints = new[] { "Semantic tags describe purpose.", "main holds primary content.", "header and footer frame the page." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Navigation",
                "Build the Complete Site Header",
                "A site header often combines a title and a nav list of links for orientation.",
                "<header>\n  <h1>Site</h1>\n  <nav><a href=\"/\">Home</a></nav>\n</header>",
                "<header>\n  <h1>Interface Workshop</h1>\n  <nav>\n    <a href=\"/projects\">Projects</a>\n  </nav>\n</header>",
                "<header>\n  <h1>___</h1>\n  <nav>\n    <a href=\"___\">Projects</a>\n  </nav>\n</header>",
                "What does nav communicate?",
                new[] { "That the image is decorative", "That CSS is required", "That the links are for navigation", "That the text is a heading" },
                2,
                "nav marks a section of major navigation links.",
                "<header>\n  <h1>Interface Workshop</h1>\n  <nav>\n    <a>Projects</a>\n  </nav>\n</header>",
                "Navigation anchors still need href destinations.",
                "Build a header with title and Projects link.",
                "Build a header with title Studio and a Home link.",
                "<header>\n  <h1>Studio</h1>\n  <nav>\n    <a href=\"/home\">Home</a>\n  </nav>\n</header>",
                "The complete site header is ready."
            )
            {
                ConceptPoints = new[] { "header groups top matter.", "nav holds key links.", "Titles and links work together." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "HTML integration",
                "Complete the First Workshop Page",
                "Combine skeleton, heading, paragraph, and a list into one coherent first page.",
                "<!DOCTYPE html>\n<html>\n<body>\n  <h1>Hi</h1>\n</body>\n</html>",
                "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\n  <h1>Interface Workshop</h1>\n  <p>Build polished web layouts.</p>\n  <ul>\n    <li>Live preview</li>\n  </ul>\n</body>\n</html>",
                "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\n  <h1>___</h1>\n  <p>___</p>\n  <ul>\n    <li>___</li>\n  </ul>\n</body>\n</html>",
                "Which tag must wrap the visible content?",
                new[] { "body", "li", "ul", "head" },
                0,
                "Visible elements belong inside body.",
                "<!DOCTYPE html>\n<html>\n<head></head>\n  <h1>Interface Workshop</h1>\n</html>",
                "Place headings and lists inside body.",
                "Complete the first workshop page with title, intro, and one list item.",
                "Complete a page with title Practice and one list item Flexbox.",
                "<!DOCTYPE html>\n<html>\n<head></head>\n<body>\n  <h1>Practice</h1>\n  <ul>\n    <li>Flexbox</li>\n  </ul>\n</body>\n</html>",
                "Chapter 1 complete: the first Workshop page is assembled."
            )
            {
                ConceptPoints = new[] { "Combine structure and content.", "Keep tags nested cleanly.", "Ship a readable first page." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 2 · CSS Foundations",
                "Stylesheets",
                "Connect the Workshop Stylesheet",
                "A link tag in head attaches an external CSS file so styles stay separate from HTML structure.",
                "<link rel=\"stylesheet\" href=\"styles.css\">",
                "<link rel=\"stylesheet\" href=\"workshop.css\">",
                "<link rel=\"stylesheet\" href=\"___\">",
                "Which attribute points to the CSS file?",
                new[] { "rel", "alt", "src", "href" },
                3,
                "href names the stylesheet file to load.",
                "<link rel=\"stylesheet\" src=\"workshop.css\">",
                "Stylesheet links use href, not src.",
                "Link workshop.css as the stylesheet.",
                "Link theme.css as the stylesheet.",
                "<link rel=\"stylesheet\" href=\"theme.css\">",
                "The workshop stylesheet is connected."
            )
            {
                ConceptPoints = new[] { "rel describes the relationship.", "href locates the CSS file.", "Keep CSS linked from head." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 2 · CSS Foundations",
                "Selectors",
                "Target the Right Page Elements",
                "CSS selectors choose which elements receive a rule. An element selector uses the tag name.",
                "p {\n  color: black;\n}",
                "h1 {\n  color: navy;\n}",
                "___ {\n  color: navy;\n}",
                "What does the h1 selector target?",
                new[] { "All h1 elements", "Only the stylesheet", "Only links", "Only images" },
                0,
                "An element selector matches every element with that tag name.",
                "h1\n  color: navy;\n}",
                "Wrap declarations in braces after the selector.",
                "Color all h1 elements navy.",
                "Color all p elements teal.",
                "p {\n  color: teal;\n}",
                "Selectors can target the right page elements."
            )
            {
                ConceptPoints = new[] { "Selectors choose targets.", "Declarations live in braces.", "Properties assign values." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 2 · CSS Foundations",
                "Color",
                "Create the Workshop Color System",
                "color sets text color and background-color sets the surface behind content.",
                "body {\n  color: #111;\n  background-color: #f5f5f5;\n}",
                "body {\n  color: #102a43;\n  background-color: #f0f4f8;\n}",
                "body {\n  color: ___;\n  background-color: ___;\n}",
                "Which property paints the page surface?",
                new[] { "href", "font-size", "color", "background-color" },
                3,
                "background-color fills the element's background area.",
                "body {\n  text-color: #102a43;\n  background: #f0f4f8;\n}",
                "Use the property names color and background-color for this lesson.",
                "Set body text and background to the workshop palette values.",
                "Set body text #222 and background #fff.",
                "body {\n  color: #222;\n  background-color: #fff;\n}",
                "The workshop color system is applied to the page."
            )
            {
                ConceptPoints = new[] { "color styles text.", "background-color styles surfaces.", "Consistent palettes improve clarity." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 2 · CSS Foundations",
                "Typography",
                "Style the Workshop Typography",
                "font-family chooses typefaces and font-size controls text size.",
                "body {\n  font-family: Arial, sans-serif;\n  font-size: 16px;\n}",
                "body {\n  font-family: Georgia, serif;\n  font-size: 18px;\n}",
                "body {\n  font-family: ___;\n  font-size: ___;\n}",
                "What does font-size control?",
                new[] { "The image file", "The link destination", "How large the text appears", "The DOCTYPE" },
                2,
                "font-size sets the rendered size of text.",
                "body {\n  font: Georgia;\n  size: 18px;\n}",
                "Use font-family and font-size property names.",
                "Set Georgia serif body text at 18px.",
                "Set Arial sans-serif body text at 16px.",
                "body {\n  font-family: Arial, sans-serif;\n  font-size: 16px;\n}",
                "Workshop typography is tuned for reading."
            )
            {
                ConceptPoints = new[] { "font-family sets the typeface.", "font-size sets scale.", "Fallbacks improve reliability." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 2 · CSS Foundations",
                "Box model",
                "Control Spacing with the Box Model",
                "margin creates space outside an element and padding creates space inside its border box.",
                ".card {\n  margin: 16px;\n  padding: 12px;\n}",
                ".card {\n  margin: 20px;\n  padding: 16px;\n}",
                ".card {\n  margin: ___;\n  padding: ___;\n}",
                "Which property adds space inside the element?",
                new[] { "color", "href", "margin", "padding" },
                3,
                "padding sits between content and border; margin sits outside.",
                ".card {\n  spacing: 20px;\n}",
                "Use margin and padding to control outer and inner space.",
                "Give .card 20px margin and 16px padding.",
                "Give .panel 8px margin and 10px padding.",
                ".panel {\n  margin: 8px;\n  padding: 10px;\n}",
                "Card spacing follows the box model."
            )
            {
                ConceptPoints = new[] { "margin is outside.", "padding is inside.", "Box model controls spacing." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 2 · CSS Foundations",
                "Visual surfaces",
                "Add Borders, Radius, and Shadows",
                "Borders outline surfaces, border-radius softens corners, and box-shadow adds depth.",
                ".card {\n  border: 1px solid #ccc;\n  border-radius: 8px;\n}",
                ".card {\n  border: 1px solid #9fb3c8;\n  border-radius: 12px;\n  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.12);\n}",
                ".card {\n  border: 1px solid ___;\n  border-radius: ___;\n  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.12);\n}",
                "What does border-radius change?",
                new[] { "The link target", "The font family", "How rounded the corners are", "The DOCTYPE" },
                2,
                "border-radius curves the corners of the box.",
                ".card {\n  border: 1px solid #9fb3c8;\n  radius: 12px;\n}",
                "Use border-radius, not radius, for rounded corners.",
                "Style .card with border, radius, and shadow.",
                "Style .tile with a 1px solid #ddd border and 6px radius.",
                ".tile {\n  border: 1px solid #ddd;\n  border-radius: 6px;\n}",
                "Project cards now have clear visual surfaces."
            )
            {
                ConceptPoints = new[] { "Borders define edges.", "Radius softens corners.", "Shadows suggest elevation." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 2 · CSS Foundations",
                "Interactive styles",
                "Design Buttons and Hover States",
                "Pseudo-classes like :hover style an element in a particular state, such as when the pointer is over a button.",
                "button:hover {\n  background-color: #333;\n}",
                ".button:hover {\n  background-color: #243b53;\n}",
                ".button:hover {\n  background-color: ___;\n}",
                "When does :hover apply?",
                new[] { "Only for images", "Only in print", "When the pointer is over the element", "Only on page load" },
                2,
                ":hover matches while the user is hovering with a pointing device.",
                ".button hover {\n  background-color: #243b53;\n}",
                "Attach :hover directly to the selector with a colon.",
                "Set .button:hover background to #243b53.",
                "Set .link:hover color to #0b6e4f.",
                ".link:hover {\n  color: #0b6e4f;\n}",
                "Buttons respond to hover interaction."
            )
            {
                ConceptPoints = new[] { ":hover is a state.", "Interactive styles give feedback.", "Keep contrast readable." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 2 · CSS Foundations",
                "CSS integration",
                "Style the Complete Project Card",
                "Combine color, spacing, and surface styles into one polished project card rule set.",
                ".card {\n  padding: 12px;\n  border-radius: 8px;\n}",
                ".card {\n  color: #102a43;\n  background-color: #ffffff;\n  margin: 16px;\n  padding: 16px;\n  border: 1px solid #9fb3c8;\n  border-radius: 12px;\n}",
                ".card {\n  color: ___;\n  background-color: ___;\n  margin: ___;\n  padding: ___;\n  border: 1px solid ___;\n  border-radius: ___;\n}",
                "Which property sets the card's outer spacing from neighbors?",
                new[] { "padding", "margin", "color", "font-size" },
                1,
                "margin controls space outside the card box.",
                ".card {\n  color: #102a43;\n  background-color: #ffffff;\n  padding: 16px;\n  border: 1px solid #9fb3c8;\n}",
                "Include margin and border-radius for the complete card treatment.",
                "Assemble the full project card style block.",
                "Assemble a compact .note card with padding 8px and radius 6px.",
                ".note {\n  padding: 8px;\n  border-radius: 6px;\n}",
                "Chapter 2 complete: the project card is fully styled."
            )
            {
                ConceptPoints = new[] { "Layer visual properties.", "Cards package content.", "Consistency builds polish." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 3 · Layout Systems",
                "Display",
                "Understand Block and Inline Layout",
                "display controls whether an element behaves as a block, inline, or other layout mode.",
                ".label {\n  display: inline;\n}",
                ".badge {\n  display: inline-block;\n}",
                ".badge {\n  display: ___;\n}",
                "What is a difference of block vs inline?",
                new[] { "Inline elements replace HTML", "Block elements cannot have margin", "Block elements start on a new line by default", "Inline elements always fill the page width" },
                2,
                "Block-level boxes stack vertically; inline boxes flow within a line.",
                ".badge {\n  display: in-line;\n}",
                "Use a valid display value such as inline-block.",
                "Set .badge to inline-block.",
                "Set .chip to inline.",
                ".chip {\n  display: inline;\n}",
                "Display modes control basic flow."
            )
            {
                ConceptPoints = new[] { "display changes layout mode.", "block stacks.", "inline flows in text." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 3 · Layout Systems",
                "Flexbox",
                "Create a Flexible Navigation Row",
                "display: flex creates a flex container so children can sit in a row or column with flexible distribution.",
                ".row {\n  display: flex;\n}",
                ".nav {\n  display: flex;\n}",
                ".nav {\n  display: ___;\n}",
                "What does display: flex do?",
                new[] { "Only works on images", "Deletes child elements", "Replaces the HTML file", "Turns the element into a flex container" },
                3,
                "A flex container can arrange its flex items along a main axis.",
                ".nav {\n  display: flexible;\n}",
                "The value is flex, not flexible.",
                "Make .nav a flex container.",
                "Make .toolbar a flex container.",
                ".toolbar {\n  display: flex;\n}",
                "Navigation can sit in a flexible row."
            )
            {
                ConceptPoints = new[] { "flex enables flexible layout.", "Children become flex items.", "Rows and columns are easy to build." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 3 · Layout Systems",
                "Flex alignment",
                "Align and Distribute Interface Items",
                "justify-content distributes items on the main axis and align-items aligns them on the cross axis.",
                ".row {\n  display: flex;\n  justify-content: space-between;\n}",
                ".nav {\n  display: flex;\n  justify-content: space-between;\n  align-items: center;\n}",
                ".nav {\n  display: flex;\n  justify-content: ___;\n  align-items: ___;\n}",
                "Which property spaces items along the main axis?",
                new[] { "font-size", "align-items", "justify-content", "href" },
                2,
                "justify-content works along the main axis of the flex container.",
                ".nav {\n  display: flex;\n  justify: space-between;\n}",
                "Use the full property name justify-content.",
                "Space nav items between edges and center them vertically.",
                "Center toolbar items on both axes with center values.",
                ".toolbar {\n  display: flex;\n  justify-content: center;\n  align-items: center;\n}",
                "Nav items align and distribute cleanly."
            )
            {
                ConceptPoints = new[] { "justify-content is main-axis.", "align-items is cross-axis.", "Centering is a common pattern." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 3 · Layout Systems",
                "Flex wrapping",
                "Make the Card Row Wrap Safely",
                "flex-wrap allows flex items to move onto new lines when space runs out.",
                ".row {\n  display: flex;\n  flex-wrap: wrap;\n}",
                ".cards {\n  display: flex;\n  flex-wrap: wrap;\n}",
                ".cards {\n  display: flex;\n  flex-wrap: ___;\n}",
                "What does flex-wrap: wrap do?",
                new[] { "Removes flex", "Hides overflow forever", "Locks a single column only", "Lets items flow onto additional lines" },
                3,
                "wrap allows a multi-line flex layout when items do not fit.",
                ".cards {\n  display: flex;\n  flex-wrap: wrapping;\n}",
                "The keyword is wrap.",
                "Allow .cards to wrap.",
                "Allow .chips to wrap.",
                ".chips {\n  display: flex;\n  flex-wrap: wrap;\n}",
                "Card rows wrap safely on smaller widths."
            )
            {
                ConceptPoints = new[] { "wrap prevents awkward overflow.", "Flex items can multi-line.", "Useful for card galleries." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 3 · Layout Systems",
                "CSS Grid",
                "Build a Two-Dimensional Project Grid",
                "display: grid enables two-dimensional layout. grid-template-columns defines column tracks.",
                ".grid {\n  display: grid;\n  grid-template-columns: 1fr 1fr;\n}",
                ".projects {\n  display: grid;\n  grid-template-columns: 1fr 1fr 1fr;\n}",
                ".projects {\n  display: grid;\n  grid-template-columns: ___;\n}",
                "What does 1fr mean in a grid track?",
                new[] { "A link target", "A font size", "Exactly one pixel", "One share of free space" },
                3,
                "fr units distribute remaining free space among tracks.",
                ".projects {\n  display: grid;\n  columns: 1fr 1fr 1fr;\n}",
                "Use grid-template-columns to define column tracks.",
                "Build a three-column project grid with equal fr tracks.",
                "Build a two-column panel grid with equal fr tracks.",
                ".panels {\n  display: grid;\n  grid-template-columns: 1fr 1fr;\n}",
                "Projects can sit in a two-dimensional grid."
            )
            {
                ConceptPoints = new[] { "grid is two-dimensional.", "template columns define tracks.", "fr shares free space." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 3 · Layout Systems",
                "Grid areas",
                "Name the Dashboard Layout Regions",
                "grid-template-areas names regions so children can be placed with grid-area.",
                ".layout {\n  display: grid;\n  grid-template-areas: \"header header\" \"side main\";\n}",
                ".dashboard {\n  display: grid;\n  grid-template-areas: \"nav main\";\n}",
                ".dashboard {\n  display: grid;\n  grid-template-areas: \"___\";\n}",
                "What does grid-template-areas define?",
                new[] { "Named placement regions for the grid", "The HTML DOCTYPE", "Only animation speed", "Only font styles" },
                0,
                "Named areas make complex layouts easier to read and maintain.",
                ".dashboard {\n  display: grid;\n  areas: \"nav main\";\n}",
                "Use grid-template-areas for named regions.",
                "Define a dashboard with nav and main areas on one row.",
                "Define a page with header and body areas on one row.",
                ".page {\n  display: grid;\n  grid-template-areas: \"header body\";\n}",
                "Dashboard regions are named for placement."
            )
            {
                ConceptPoints = new[] { "Areas name regions.", "Children can target area names.", "Readable layouts scale better." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 3 · Layout Systems",
                "Positioning",
                "Place an Interface Badge Precisely",
                "position: relative on a parent and absolute on a child can place a badge relative to that parent.",
                ".wrap { position: relative; }\n.badge { position: absolute; top: 0; }",
                ".card {\n  position: relative;\n}\n.badge {\n  position: absolute;\n  top: 8px;\n  right: 8px;\n}",
                ".card {\n  position: ___;\n}\n.badge {\n  position: ___;\n  top: 8px;\n  right: 8px;\n}",
                "Why is the parent often relative?",
                new[] { "So images delete themselves", "So CSS cannot load", "So absolute children are positioned against that parent", "So text becomes bold" },
                2,
                "Absolute positioning is resolved against the nearest positioned ancestor.",
                ".card {\n  position: absolute;\n}\n.badge {\n  position: absolute;\n}",
                "Keep the card relative and the badge absolute for inset placement.",
                "Pin a badge 8px from the top and right of a relative card.",
                "Pin a tag 4px from the top and left of a relative tile.",
                ".tile {\n  position: relative;\n}\n.tag {\n  position: absolute;\n  top: 4px;\n  left: 4px;\n}",
                "Interface badges can sit precisely on cards."
            )
            {
                ConceptPoints = new[] { "relative establishes a containing context.", "absolute offsets from that context.", "Use sparingly for overlays." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 3 · Layout Systems",
                "Layout integration",
                "Assemble the Workshop Dashboard",
                "Combine flex navigation with a grid content area for a simple dashboard shell.",
                ".shell { display: grid; }\n.nav { display: flex; }",
                ".shell {\n  display: grid;\n  grid-template-columns: 200px 1fr;\n}\n.nav {\n  display: flex;\n}",
                ".shell {\n  display: ___;\n  grid-template-columns: 200px 1fr;\n}\n.nav {\n  display: ___;\n}",
                "Which module creates the two-column shell?",
                new[] { "grid on .shell", "alt on images", "flex on .shell only", "DOCTYPE" },
                0,
                "The shell grid defines columns; nav flex arranges links.",
                ".shell {\n  display: flex;\n  grid-template-columns: 200px 1fr;\n}",
                "Use display: grid when defining grid-template-columns.",
                "Assemble shell grid columns and flex nav.",
                "Assemble shell grid 240px 1fr and flex toolbar.",
                ".shell {\n  display: grid;\n  grid-template-columns: 240px 1fr;\n}\n.toolbar {\n  display: flex;\n}",
                "Chapter 3 complete: the workshop dashboard shell is assembled."
            )
            {
                ConceptPoints = new[] { "Grid structures the page.", "Flex arranges toolbars.", "Combine layout systems thoughtfully." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 4 · Responsive Interfaces and Forms",
                "Responsive units",
                "Use Flexible Sizes",
                "Relative units like % and rem adapt more gracefully than only fixed pixels for many interface sizes.",
                ".panel {\n  width: 80%;\n}",
                ".panel {\n  width: 90%;\n  max-width: 960px;\n}",
                ".panel {\n  width: ___;\n  max-width: ___;\n}",
                "What does max-width prevent?",
                new[] { "The element growing beyond a chosen limit", "Headings from existing", "Links from working", "All CSS from loading" },
                0,
                "max-width caps growth while width can still be flexible.",
                ".panel {\n  width: 90%;\n  maximum-width: 960px;\n}",
                "The property name is max-width.",
                "Set panel width 90% with max-width 960px.",
                "Set stage width 100% with max-width 720px.",
                ".stage {\n  width: 100%;\n  max-width: 720px;\n}",
                "Flexible sizes keep panels readable."
            )
            {
                ConceptPoints = new[] { "% is relative to parent.", "max-width caps size.", "Mix fluid and fixed limits." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 4 · Responsive Interfaces and Forms",
                "Media queries",
                "Adapt the Workshop at a Breakpoint",
                "@media applies styles only when a media condition matches, such as a maximum viewport width.",
                "@media (max-width: 600px) {\n  .nav { flex-direction: column; }\n}",
                "@media (max-width: 700px) {\n  .nav {\n    flex-direction: column;\n  }\n}",
                "@media (max-width: ___) {\n  .nav {\n    flex-direction: column;\n  }\n}",
                "When do these rules apply?",
                new[] { "Only on print always", "Only for h1 elements", "Only if JavaScript runs", "When the viewport is at most 700px wide" },
                3,
                "max-width media queries target viewports at or below the breakpoint.",
                "@media max-width 700px {\n  .nav { flex-direction: column; }\n}",
                "Wrap the condition in parentheses after @media.",
                "Stack .nav in a column under 700px.",
                "Stack .toolbar in a column under 500px.",
                "@media (max-width: 500px) {\n  .toolbar {\n    flex-direction: column;\n  }\n}",
                "The workshop adapts at the breakpoint."
            )
            {
                ConceptPoints = new[] { "@media adds conditions.", "Breakpoints tune layouts.", "Mobile-first or desktop-first both work." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 4 · Responsive Interfaces and Forms",
                "Mobile navigation",
                "Rebuild Navigation for Small Screens",
                "On small screens, navigation often switches direction or spacing so links remain tappable.",
                ".nav {\n  display: flex;\n  gap: 8px;\n}",
                "@media (max-width: 600px) {\n  .nav {\n    flex-direction: column;\n    gap: 12px;\n  }\n}",
                "@media (max-width: 600px) {\n  .nav {\n    flex-direction: ___;\n    gap: ___;\n  }\n}",
                "Why increase gap on mobile nav?",
                new[] { "To hide the stylesheet", "To remove headings", "To give links more separation for touch", "To disable href" },
                2,
                "Touch targets benefit from clearer spacing between links.",
                "@media (max-width: 600px) {\n  .nav {\n    flex-direction: column;\n  }\n}",
                "Include gap for comfortable mobile spacing.",
                "Columnize nav and set gap 12px under 600px.",
                "Columnize menu and set gap 10px under 480px.",
                "@media (max-width: 480px) {\n  .menu {\n    flex-direction: column;\n    gap: 10px;\n  }\n}",
                "Mobile navigation is easier to use."
            )
            {
                ConceptPoints = new[] { "Column layouts fit narrow screens.", "gap separates controls.", "Touch-friendly spacing matters." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 4 · Responsive Interfaces and Forms",
                "Forms",
                "Build the Project Request Form",
                "Forms collect input. label describes a field and input creates a control.",
                "<label>Name</label>\n<input type=\"text\">",
                "<label for=\"email\">Email</label>\n<input id=\"email\" type=\"email\">",
                "<label for=\"email\">___</label>\n<input id=\"email\" type=\"___\">",
                "Why match for and id?",
                new[] { "To load CSS", "To color the page", "To create a grid", "To associate the label with the input" },
                3,
                "Connecting label and input improves accessibility and click targets.",
                "<label>Email</label>\n<input type=\"email\">",
                "Use for on the label and the same id on the input.",
                "Build an email field with linked label and input.",
                "Build a name field with linked label and text input.",
                "<label for=\"name\">Name</label>\n<input id=\"name\" type=\"text\">",
                "The project request form can collect email."
            )
            {
                ConceptPoints = new[] { "label describes controls.", "input types guide entry.", "for/id pairs improve a11y." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 4 · Responsive Interfaces and Forms",
                "Accessible interaction",
                "Make Form Focus Visible",
                ":focus styles the element that currently accepts keyboard input so users can see where they are.",
                "input:focus {\n  outline: 2px solid blue;\n}",
                "input:focus {\n  outline: 3px solid #486581;\n}",
                "input:focus {\n  outline: ___;\n}",
                "When does :focus apply?",
                new[] { "Only while printing", "Only for finished pages", "When the element is the active keyboard target", "Only on hover with a mouse" },
                2,
                ":focus highlights the element that currently has focus.",
                "input:focus {\n  border-focus: 3px solid #486581;\n}",
                "Use outline (or another clear focus style) with :focus.",
                "Give focused inputs a 3px solid #486581 outline.",
                "Give focused buttons a 2px solid #0b6e4f outline.",
                "button:focus {\n  outline: 2px solid #0b6e4f;\n}",
                "Keyboard users can see form focus clearly."
            )
            {
                ConceptPoints = new[] { ":focus supports keyboard use.", "Visible focus is required for a11y.", "Never remove focus without a replacement." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 4 · Responsive Interfaces and Forms",
                "Data tables",
                "Display Project Data Clearly",
                "Tables structure tabular data with table, tr rows, th headers, and td cells.",
                "<table>\n  <tr><th>Name</th></tr>\n  <tr><td>A</td></tr>\n</table>",
                "<table>\n  <tr>\n    <th>Project</th>\n    <th>Status</th>\n  </tr>\n  <tr>\n    <td>Dashboard</td>\n    <td>Live</td>\n  </tr>\n</table>",
                "<table>\n  <tr>\n    <th>___</th>\n    <th>___</th>\n  </tr>\n  <tr>\n    <td>___</td>\n    <td>___</td>\n  </tr>\n</table>",
                "Which tag marks a header cell?",
                new[] { "tr", "table", "th", "td" },
                2,
                "th is for header cells; td is for data cells.",
                "<table>\n  <tr>\n    <td>Project</td>\n    <td>Status</td>\n  </tr>\n</table>",
                "Use th for column headers.",
                "Build a two-column project status table.",
                "Build a two-column tool version table.",
                "<table>\n  <tr>\n    <th>Tool</th>\n    <th>Version</th>\n  </tr>\n  <tr>\n    <td>Grid</td>\n    <td>1</td>\n  </tr>\n</table>",
                "Project data displays in a clear table."
            )
            {
                ConceptPoints = new[] { "table holds rows.", "th labels columns.", "td holds values." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 4 · Responsive Interfaces and Forms",
                "CSS variables",
                "Create Reusable Theme Tokens",
                "Custom properties (CSS variables) store reusable values. var(--name) reads them.",
                ":root {\n  --accent: #3366ff;\n}\n.button {\n  color: var(--accent);\n}",
                ":root {\n  --accent: #486581;\n}\n.button {\n  background-color: var(--accent);\n}",
                ":root {\n  --accent: ___;\n}\n.button {\n  background-color: var(___);\n}",
                "How do you read a custom property?",
                new[] { "With alt(--name)", "With href(--name)", "With var(--name)", "With main(--name)" },
                2,
                "var(--token) substitutes the token's value.",
                ":root {\n  --accent: #486581;\n}\n.button {\n  background-color: accent;\n}",
                "Wrap the custom property name in var().",
                "Define --accent and use it on .button background.",
                "Define --danger and use it on .alert color.",
                ":root {\n  --danger: #9b1c1c;\n}\n.alert {\n  color: var(--danger);\n}",
                "Theme tokens make styles reusable."
            )
            {
                ConceptPoints = new[] { "Define tokens on :root.", "Read them with var().", "Tokens centralize theme values." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 4 · Responsive Interfaces and Forms",
                "Responsive integration",
                "Complete the Responsive Control Panel",
                "Combine flexible width, a breakpoint, and a theme token for a responsive control panel.",
                ".panel {\n  width: 100%;\n}\n:root {\n  --panel: #fff;\n}",
                ":root {\n  --panel: #f0f4f8;\n}\n.panel {\n  width: 100%;\n  max-width: 800px;\n  background-color: var(--panel);\n}\n@media (max-width: 600px) {\n  .panel {\n    max-width: 100%;\n  }\n}",
                ":root {\n  --panel: ___;\n}\n.panel {\n  width: 100%;\n  max-width: ___;\n  background-color: var(--panel);\n}",
                "What role does the media query play here?",
                new[] { "It sets the DOCTYPE", "It replaces class names", "It creates the HTML table", "It adapts max-width on smaller screens" },
                3,
                "The breakpoint adjusts layout constraints for narrow viewports.",
                ".panel {\n  width: 100%;\n  max-width: 800px;\n  background-color: panel;\n}",
                "Define --panel and reference it with var(--panel).",
                "Build the responsive panel token and width rules.",
                "Build a stage token #eef and max-width 640px.",
                ":root {\n  --stage: #eef;\n}\n.stage {\n  width: 100%;\n  max-width: 640px;\n  background-color: var(--stage);\n}",
                "Chapter 4 complete: the responsive control panel is in place."
            )
            {
                ConceptPoints = new[] { "Tokens + fluid width + breakpoints.", "Responsive does not mean mobile only.", "Integrate earlier CSS skills." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 5 · Polished Web Application",
                "Pseudo-classes",
                "Style Interface States",
                "Pseudo-classes target states such as :hover and :focus beyond the base selector.",
                "a:hover {\n  text-decoration: underline;\n}",
                "a:focus {\n  outline: 2px solid #486581;\n}",
                "a:focus {\n  outline: ___;\n}",
                "Which selector styles a focused link?",
                new[] { "a src", "a:hover only", "a::before", "a:focus" },
                3,
                ":focus matches when the link has keyboard or script focus.",
                "a:focus {\n  outline-width: 2px solid #486581;\n}",
                "Provide a complete outline value, not only outline-width with a full shorthand value.",
                "Style focused links with a 2px solid #486581 outline.",
                "Style hovered buttons with background #333.",
                "button:hover {\n  background-color: #333;\n}",
                "Interface states are visible and intentional."
            )
            {
                ConceptPoints = new[] { ":hover and :focus are states.", "State styles guide interaction.", "Keep contrast strong." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 5 · Polished Web Application",
                "Pseudo-elements",
                "Add Decorative Interface Details",
                "Pseudo-elements such as ::before create decorative fragments without extra HTML nodes.",
                ".label::before {\n  content: \"*\";\n}",
                ".required::before {\n  content: \"*\";\n  color: #9b1c1c;\n}",
                ".required::before {\n  content: \"___\";\n  color: ___;\n}",
                "What does content set on ::before?",
                new[] { "The HTML file name", "The DOCTYPE", "The viewport width", "The generated text or value of the pseudo-element" },
                3,
                "content provides what the pseudo-element generates.",
                ".required::before {\n  text: \"*\";\n}",
                "Use the content property with ::before.",
                "Mark required fields with a red asterisk via ::before.",
                "Mark notes with a · prefix via ::before.",
                ".note::before {\n  content: \"·\";\n}",
                "Decorative details can be generated in CSS."
            )
            {
                ConceptPoints = new[] { "::before and ::after decorate.", "content is required for many cases.", "Prefer real HTML for critical content." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 5 · Polished Web Application",
                "Transitions",
                "Smooth Important State Changes",
                "transition animates property changes over time when an element moves between states.",
                ".button {\n  transition: background-color 0.2s ease;\n}",
                ".button {\n  transition: background-color 0.25s ease;\n}",
                ".button {\n  transition: background-color ___;\n}",
                "What does 0.25s control?",
                new[] { "The grid template", "The font family", "How long the transition lasts", "The href" },
                2,
                "The time value sets transition duration.",
                ".button {\n  transition: background-color ease;\n}",
                "Include a duration such as 0.25s before the timing function.",
                "Transition button background over 0.25s ease.",
                "Transition link color over 0.2s ease.",
                "a {\n  transition: color 0.2s ease;\n}",
                "State changes can animate smoothly."
            )
            {
                ConceptPoints = new[] { "transition softens jumps.", "Duration and easing matter.", "Animate intentional properties." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 5 · Polished Web Application",
                "Animations",
                "Create a Purposeful Loading Animation",
                "@keyframes defines animation stages. animation applies them to an element.",
                "@keyframes pulse {\n  from { opacity: 0.5; }\n  to { opacity: 1; }\n}",
                "@keyframes pulse {\n  from {\n    opacity: 0.4;\n  }\n  to {\n    opacity: 1;\n  }\n}\n.loader {\n  animation: pulse 1s infinite alternate;\n}",
                "@keyframes pulse {\n  from {\n    opacity: ___;\n  }\n  to {\n    opacity: ___;\n  }\n}\n.loader {\n  animation: pulse 1s infinite alternate;\n}",
                "What does @keyframes define?",
                new[] { "The stages of an animation", "The server route", "The HTML skeleton only", "The image format" },
                0,
                "@keyframes names a sequence of animated property values.",
                ".loader {\n  animation: pulse 1s infinite alternate;\n}",
                "Define the pulse keyframes before applying the animation.",
                "Create a pulse opacity animation for .loader.",
                "Create a fade keyframes from 0 to 1 and use it on .ghost.",
                "@keyframes fade {\n  from {\n    opacity: 0;\n  }\n  to {\n    opacity: 1;\n  }\n}\n.ghost {\n  animation: fade 0.8s ease;\n}",
                "A purposeful loading animation is available."
            )
            {
                ConceptPoints = new[] { "keyframes define motion.", "animation applies it.", "Prefer subtle, meaningful motion." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 5 · Polished Web Application",
                "Reusable components",
                "Build a Reusable UI Component Library",
                "Shared class names create reusable components. A button class can be reused across pages.",
                ".button {\n  padding: 8px 12px;\n}",
                ".button {\n  padding: 10px 16px;\n  border-radius: 8px;\n  background-color: #486581;\n  color: #fff;\n}",
                ".button {\n  padding: ___;\n  border-radius: ___;\n  background-color: ___;\n  color: ___;\n}",
                "Why put these styles on a class?",
                new[] { "So many elements can reuse the same button look", "So CSS files cannot link", "So HTML becomes illegal", "So alt text is unused" },
                0,
                "Classes package reusable visual patterns.",
                ".button {\n  padding: 10px 16px;\n  border-radius: 8px;\n}",
                "Include background and text color for a complete button component.",
                "Define the reusable .button component styles.",
                "Define a reusable .chip with padding 4px 8px and radius 999px.",
                ".chip {\n  padding: 4px 8px;\n  border-radius: 999px;\n}",
                "A reusable button component is in the library."
            )
            {
                ConceptPoints = new[] { "Classes enable reuse.", "Components stay consistent.", "Library patterns speed design." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 5 · Polished Web Application",
                "Theme switching",
                "Support Light and Dark Themes",
                "Theme tokens can change under a dark class so one component library supports light and dark modes.",
                ":root {\n  --bg: #fff;\n}\n.dark {\n  --bg: #111;\n}",
                ":root {\n  --bg: #f0f4f8;\n  --text: #102a43;\n}\n.dark {\n  --bg: #102a43;\n  --text: #f0f4f8;\n}",
                ":root {\n  --bg: ___;\n  --text: ___;\n}\n.dark {\n  --bg: ___;\n  --text: ___;\n}",
                "What changes when .dark is applied?",
                new[] { "The HTML DOCTYPE", "The file extension", "The grid unit fr", "The token values used by the theme" },
                3,
                "Overriding custom properties under .dark retokens the UI.",
                ":root {\n  --bg: #f0f4f8;\n}\n.dark {\n  background: #102a43;\n}",
                "Override the same token names in .dark rather than only a one-off background.",
                "Define light and dark --bg and --text tokens.",
                "Define light and dark --accent tokens #06c and #8cf.",
                ":root {\n  --accent: #06c;\n}\n.dark {\n  --accent: #8cf;\n}",
                "Light and dark themes share one component system."
            )
            {
                ConceptPoints = new[] { "Tokens power themes.", "A class can switch token sets.", "Keep contrast in both modes." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 5 · Polished Web Application",
                "Quality",
                "Audit Accessibility and Performance",
                "Quality checks include visible focus, image alt text, and avoiding huge decorative animations that distract.",
                "img {\n  max-width: 100%;\n}",
                "img {\n  max-width: 100%;\n  height: auto;\n}\n:focus {\n  outline: 2px solid #486581;\n}",
                "img {\n  max-width: ___;\n  height: ___;\n}\n:focus {\n  outline: ___;\n}",
                "Why set img max-width to 100%?",
                new[] { "So large images scale down within their container", "So alt text is removed", "So tables become grids", "So CSS stops loading" },
                0,
                "max-width: 100% helps images remain responsive inside fluid layouts.",
                "img {\n  max-width: 100%;\n}\n:focus {\n  outline: none;\n}",
                "Keep a visible focus outline for keyboard users.",
                "Add responsive image rules and a visible focus outline.",
                "Add responsive svg rules with max-width 100% and height auto.",
                "svg {\n  max-width: 100%;\n  height: auto;\n}",
                "Accessibility and performance basics are in the audit checklist."
            )
            {
                ConceptPoints = new[] { "Responsive media scales.", "Focus must stay visible.", "Quality is part of shipping." },
                EditorFileNameOverride = "workshop.css"
            },
            new CourseLesson(
                "Chapter 5 · Polished Web Application",
                "Final integration",
                "Launch the Complete Interface Workshop",
                "The final challenge combines a tokenized surface, a reusable button, and a simple layout container.",
                ":root {\n  --bg: #fff;\n}\n.wrap {\n  background-color: var(--bg);\n}",
                ":root {\n  --bg: #f0f4f8;\n  --accent: #486581;\n}\n.wrap {\n  max-width: 960px;\n  background-color: var(--bg);\n}\n.button {\n  background-color: var(--accent);\n  color: #fff;\n}",
                ":root {\n  --bg: ___;\n  --accent: ___;\n}\n.wrap {\n  max-width: ___;\n  background-color: var(--bg);\n}\n.button {\n  background-color: var(--accent);\n  color: ___;\n}",
                "Which idea ties this final page together?",
                new[] { "Avoiding all CSS", "Removing the body tag", "Reusable tokens and components in one layout", "Using only pixels for everything" },
                2,
                "Tokens, components, and layout complete the workshop system.",
                ":root {\n  --bg: #f0f4f8;\n}\n.wrap {\n  max-width: 960px;\n}",
                "Include --accent and apply tokens to both wrap and button.",
                "Launch the complete workshop styles with tokens, wrap, and button.",
                "Launch a compact studio theme with --bg #fff and --accent #222.",
                ":root {\n  --bg: #fff;\n  --accent: #222;\n}\n.wrap {\n  max-width: 720px;\n  background-color: var(--bg);\n}\n.button {\n  background-color: var(--accent);\n  color: #fff;\n}",
                "The complete Interface Workshop path is online."
            )
            {
                ConceptPoints = new[] { "Integrate tokens, components, layout.", "Ship coherent UI systems.", "Chapter 5 closes the course." },
                EditorFileNameOverride = "workshop.css"
            }
        };
}
