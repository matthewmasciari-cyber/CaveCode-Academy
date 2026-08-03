namespace CaveCode.CourseEngine;

public static class HtmlCssChapterOneLessons
{
    public const int PlayableModuleCount = 8;

    public static IReadOnlyList<CourseLesson> All { get; } =
        new[]
        {
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Document structure",
                "Build the Workshop Page Skeleton",
                "A valid HTML page starts with a document type, then an html element containing a head and body. The head stores page information such as the character set and title. The body contains everything visitors can see.",
                "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <title>Example Page</title>\n</head>\n<body>\n</body>\n</html>",
                "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <title>Interface Workshop</title>\n</head>\n<body>\n</body>\n</html>",
                "<!DOCTYPE ___>\n<html lang=\"___\">\n<head>\n    <meta charset=\"___\">\n    <title>___</title>\n</head>\n<body>\n</body>\n</html>",
                "Which section contains the visible page content?",
                new[] { "The body", "The head", "The title", "The doctype" },
                0,
                "Visible headings, paragraphs, images, links, and page regions belong inside the body.",
                "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <title>Interface Workshop</title>\n<body>\n</body>\n</html>",
                "The head element must be closed before the body begins.",
                "Rebuild the complete Interface Workshop document skeleton.",
                "Create a valid page skeleton titled Project Portfolio.",
                "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <title>Project Portfolio</title>\n</head>\n<body>\n</body>\n</html>",
                "The Workshop browser now has a valid HTML document."
            )
            {
                ConceptPoints = new[] { "<!DOCTYPE html> selects modern HTML.", "The head stores page information.", "The body contains visible content." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Text content",
                "Add Headings and Paragraphs",
                "Headings identify sections and communicate importance. An h1 is the page's main heading, while p elements hold normal paragraphs. Clear hierarchy helps both readers and assistive technology understand the page.",
                "<h1>Project Dashboard</h1>\n<p>Track every active project in one place.</p>",
                "<h1>Interface Workshop</h1>\n<p>Build clear and responsive web interfaces.</p>",
                "<___>Interface Workshop</___>\n<___>Build clear and responsive web interfaces.</___>",
                "Which element should normally hold the page's main title?",
                new[] { "h1", "p", "title", "body" },
                0,
                "The h1 identifies the main visible heading for the page.",
                "<h1>Interface Workshop<h1>\n<p>Build clear and responsive web interfaces.<p>",
                "Closing tags need a forward slash: </h1> and </p>.",
                "Write the Workshop heading and introduction without the visible guide.",
                "Create an h1 named Project Portfolio and a paragraph that says Explore my latest work.",
                "<h1>Project Portfolio</h1>\n<p>Explore my latest work.</p>",
                "The landing page now explains what the Workshop is for."
            )
            {
                ConceptPoints = new[] { "Use one clear h1 for the page's main title.", "Paragraphs hold normal explanatory text.", "Opening and closing tags surround content." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Links",
                "Connect the Workshop Navigation",
                "The anchor element creates a link. Its href attribute identifies the destination, while the text between the tags tells users where the link goes. A value beginning with # points to an element ID on the same page.",
                "<nav>\n    <a href=\"#about\">About</a>\n    <a href=\"#contact\">Contact</a>\n</nav>",
                "<nav>\n    <a href=\"#projects\">Projects</a>\n    <a href=\"#skills\">Skills</a>\n    <a href=\"#contact\">Contact</a>\n</nav>",
                "<nav>\n    <a ___=\"#projects\">Projects</a>\n    <a href=\"___\">Skills</a>\n    <a href=\"#contact\">___</a>\n</nav>",
                "What does href=\"#projects\" target?",
                new[] { "An element whose ID is projects", "A projects folder", "A CSS class", "The browser history" },
                0,
                "The # symbol creates an in-page link to id=\"projects\".",
                "<nav>\n    <a=\"#projects\">Projects</a>\n    <a href=\"#skills\">Skills<a>\n    <a href=\"#contact\">Contact</a>\n</nav>",
                "The first link needs an href attribute, and the Skills link needs a proper closing tag.",
                "Rebuild the three-link Workshop navigation.",
                "Create navigation links to #home and #services.",
                "<nav>\n    <a href=\"#home\">Home</a>\n    <a href=\"#services\">Services</a>\n</nav>",
                "The Workshop header can now navigate between page sections."
            )
            {
                ConceptPoints = new[] { "The a element creates a link.", "href stores the destination.", "Descriptive link text explains the action." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Images",
                "Place an Accessible Project Image",
                "The img element places an image on the page. src identifies the image file, and alt describes its meaning when the image cannot be seen or loaded. The img element does not use a separate closing tag.",
                "<img src=\"project-card.png\" alt=\"Preview of a project dashboard\">",
                "<img src=\"workshop-preview.png\" alt=\"Preview of the Interface Workshop homepage\">",
                "<img ___=\"workshop-preview.png\" ___=\"Preview of the Interface Workshop homepage\">",
                "Why is the alt attribute important?",
                new[] { "It describes the image's purpose", "It controls the image width", "It links to another page", "It changes the file type" },
                0,
                "Useful alternative text communicates the image's meaning when the image is unavailable or cannot be seen.",
                "<img href=\"workshop-preview.png\" text=\"Preview of the Interface Workshop homepage\">",
                "Images use src for the file and alt for the description.",
                "Add the Workshop preview image with its full alternative text.",
                "Add team-photo.jpg with the alternative text Workshop team reviewing a design.",
                "<img src=\"team-photo.jpg\" alt=\"Workshop team reviewing a design\">",
                "The landing page now includes a meaningful visual project preview."
            )
            {
                ConceptPoints = new[] { "src identifies the image resource.", "alt describes the image's meaning.", "img is a void element without </img>." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Lists",
                "Build the Feature List",
                "An unordered list groups related items when their order is not important. The ul element contains the list, and every item belongs inside its own li element.",
                "<ul>\n    <li>Fast setup</li>\n    <li>Clear navigation</li>\n</ul>",
                "<ul>\n    <li>Semantic HTML</li>\n    <li>Responsive CSS</li>\n    <li>Accessible controls</li>\n</ul>",
                "<___>\n    <___>Semantic HTML</___>\n    <li>Responsive CSS</li>\n    <li>Accessible controls</li>\n</___>",
                "Which element represents one item inside a list?",
                new[] { "li", "ul", "p", "section" },
                0,
                "Each individual list item is wrapped in an li element.",
                "<ul>\n    <li>Semantic HTML\n    <li>Responsive CSS</li>\n    <li>Accessible controls</li>\n<ul>",
                "Close the first li and close the list with </ul>.",
                "Rebuild the three-item Workshop feature list.",
                "Create a list containing Fast, Clear, and Responsive.",
                "<ul>\n    <li>Fast</li>\n    <li>Clear</li>\n    <li>Responsive</li>\n</ul>",
                "Visitors can now scan the Workshop's core features."
            )
            {
                ConceptPoints = new[] { "ul creates an unordered list.", "li creates one list item.", "Lists communicate grouped information." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Semantic sections",
                "Divide the Page into Meaningful Regions",
                "Semantic elements describe the purpose of page regions. header introduces the page, main contains its primary content, section groups a related topic, and footer closes the page with supporting information.",
                "<main>\n    <section id=\"about\">\n        <h2>About</h2>\n    </section>\n</main>",
                "<main>\n    <section id=\"projects\">\n        <h2>Featured Projects</h2>\n        <p>Explore interfaces built inside the Workshop.</p>\n    </section>\n</main>",
                "<___>\n    <___ id=\"projects\">\n        <h2>Featured Projects</h2>\n        <p>Explore interfaces built inside the Workshop.</p>\n    </___>\n</___>",
                "Which element should contain the page's primary unique content?",
                new[] { "main", "header", "footer", "nav" },
                0,
                "The main landmark contains the central content unique to the page.",
                "<main>\n    <section id=\"projects\">\n        <h2>Featured Projects</h2>\n        <p>Explore interfaces built inside the Workshop.</p>\n    </main>\n</section>",
                "Nested elements must close in reverse order: close section before main.",
                "Rebuild the projects main and section region.",
                "Create a main region containing a section with id skills and an h2 named Skills.",
                "<main>\n    <section id=\"skills\">\n        <h2>Skills</h2>\n    </section>\n</main>",
                "The page now has meaningful landmarks and a projects region."
            )
            {
                ConceptPoints = new[] { "main identifies primary page content.", "section groups one related topic.", "Semantic landmarks improve navigation." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "Navigation",
                "Build the Complete Site Header",
                "A complete site header combines branding and navigation inside a header landmark. The nav element identifies the group of primary navigation links so users and assistive technology can find it quickly.",
                "<header>\n    <strong>Project Studio</strong>\n    <nav>\n        <a href=\"#work\">Work</a>\n    </nav>\n</header>",
                "<header>\n    <strong>CaveCode Workshop</strong>\n    <nav>\n        <a href=\"#projects\">Projects</a>\n        <a href=\"#skills\">Skills</a>\n        <a href=\"#contact\">Contact</a>\n    </nav>\n</header>",
                "<___>\n    <strong>___</strong>\n    <___>\n        <a href=\"#projects\">Projects</a>\n        <a href=\"#skills\">Skills</a>\n        <a href=\"#contact\">Contact</a>\n    </___>\n</___>",
                "What is the nav element communicating?",
                new[] { "This group contains navigation links", "This text is the page title", "This region is an image", "This content is hidden" },
                0,
                "nav identifies a major group of links used to move through the site or page.",
                "<header>\n    <strong>CaveCode Workshop</strong>\n    <nav>\n        <a href=\"#projects\">Projects</a>\n        <a href=\"#skills\">Skills</a>\n        <a href=\"#contact\">Contact</a>\n    </header>\n</nav>",
                "Close nav before closing the surrounding header.",
                "Rebuild the full CaveCode Workshop site header.",
                "Create a header named Portfolio Lab with a nav link to #work.",
                "<header>\n    <strong>Portfolio Lab</strong>\n    <nav>\n        <a href=\"#work\">Work</a>\n    </nav>\n</header>",
                "The Workshop now has a complete semantic brand and navigation header."
            )
            {
                ConceptPoints = new[] { "header introduces a page or section.", "nav identifies primary navigation.", "Branding and links can share one header." },
                EditorFileNameOverride = "index.html"
            },
            new CourseLesson(
                "Chapter 1 · HTML Foundations",
                "HTML integration",
                "Complete the First Workshop Page",
                "A complete HTML page combines document structure, semantic landmarks, text, navigation, images, and grouped content. The final page should remain understandable even before CSS is added.",
                "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <title>Project Studio</title>\n</head>\n<body>\n    <header>\n        <h1>Project Studio</h1>\n    </header>\n    <main>\n        <p>Explore the latest work.</p>\n    </main>\n</body>\n</html>",
                "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <title>Interface Workshop</title>\n</head>\n<body>\n    <header>\n        <strong>CaveCode Workshop</strong>\n        <nav>\n            <a href=\"#projects\">Projects</a>\n            <a href=\"#skills\">Skills</a>\n            <a href=\"#contact\">Contact</a>\n        </nav>\n    </header>\n\n    <main>\n        <h1>Interface Workshop</h1>\n        <p>Build clear and responsive web interfaces.</p>\n        <img src=\"workshop-preview.png\" alt=\"Preview of the Interface Workshop homepage\">\n\n        <section id=\"projects\">\n            <h2>Featured Projects</h2>\n            <p>Explore interfaces built inside the Workshop.</p>\n            <ul>\n                <li>Semantic HTML</li>\n                <li>Responsive CSS</li>\n                <li>Accessible controls</li>\n            </ul>\n        </section>\n    </main>\n\n    <footer id=\"contact\">\n        <p>Built in CaveCode Academy.</p>\n    </footer>\n</body>\n</html>",
                "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <title>___</title>\n</head>\n<body>\n    <___>\n        <strong>CaveCode Workshop</strong>\n        <nav>\n            <a href=\"#projects\">Projects</a>\n            <a href=\"#skills\">Skills</a>\n            <a href=\"#contact\">Contact</a>\n        </nav>\n    </___>\n\n    <main>\n        <___>Interface Workshop</___>\n        <p>Build clear and responsive web interfaces.</p>\n        <img src=\"workshop-preview.png\" alt=\"Preview of the Interface Workshop homepage\">\n\n        <section id=\"projects\">\n            <h2>Featured Projects</h2>\n            <p>Explore interfaces built inside the Workshop.</p>\n            <___>\n                <li>Semantic HTML</li>\n                <li>Responsive CSS</li>\n                <li>Accessible controls</li>\n            </___>\n        </section>\n    </main>\n\n    <footer id=\"contact\">\n        <p>Built in CaveCode Academy.</p>\n    </footer>\n</body>\n</html>",
                "Why should the page remain understandable before CSS is added?",
                new[] { "HTML carries the content and meaning", "CSS creates every word", "Browsers ignore HTML structure", "Only images need HTML" },
                0,
                "HTML defines the content, relationships, and semantic structure; CSS will enhance its appearance later.",
                "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <title>Interface Workshop</title>\n</head>\n<body>\n    <header>\n        <strong>CaveCode Workshop</strong>\n        <nav>\n            <a href=\"#projects\">Projects</a>\n            <a href=\"#skills\">Skills</a>\n            <a href=\"#contact\">Contact</a>\n        </header>\n    </nav>\n    <main>\n        <h1>Interface Workshop<h1>\n        <p>Build clear and responsive web interfaces.</p>\n        <img href=\"workshop-preview.png\" text=\"Preview of the Interface Workshop homepage\">\n        <section id=\"projects\">\n            <h2>Featured Projects</h2>\n            <ul>\n                <li>Semantic HTML</li>\n                <li>Responsive CSS</li>\n                <li>Accessible controls</li>\n            <ul>\n        </section>\n    </main>\n</body>\n</html>",
                "Repair the header and nav closing order, the h1 closing tag, the image attributes, and the list closing tag.",
                "Rebuild the complete first Interface Workshop page from memory.",
                "Create a complete page titled Portfolio Lab with a header, main heading, paragraph, and footer.",
                "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <title>Portfolio Lab</title>\n</head>\n<body>\n    <header>\n        <strong>Portfolio Lab</strong>\n    </header>\n    <main>\n        <h1>My Projects</h1>\n        <p>Explore my latest interface work.</p>\n    </main>\n    <footer>\n        <p>Portfolio Lab</p>\n    </footer>\n</body>\n</html>",
                "Chapter 1 complete: the first semantic Workshop page is ready for CSS."
            )
            {
                ConceptPoints = new[] { "A complete page combines structure and content.", "Semantic landmarks explain each region.", "Accessible HTML works before visual styling." },
                EditorFileNameOverride = "index.html"
            }
        };

    public static CourseDefinition Definition(CourseManifest manifest) =>
        new(manifest, All);
}
