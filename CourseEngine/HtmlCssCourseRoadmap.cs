namespace CaveCode.CourseEngine;

public sealed record HtmlCssRoadmapModule(
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

public static class HtmlCssCourseRoadmap
{
    public static IReadOnlyList<HtmlCssRoadmapModule> Modules { get; } =
        new[]
        {
            new HtmlCssRoadmapModule(
                1,
                "Chapter 1 · HTML Foundations",
                "Document structure",
                "Build the Workshop Page Skeleton",
                "Create the required HTML document structure and understand the head and body.",
                "Open a valid Workshop page inside the browser canvas.",
                "index.html",
                "Page skeleton",
                new[] { "<!DOCTYPE html>", "<html>", "<head>", "<body>" }
            ),
            new HtmlCssRoadmapModule(
                2,
                "Chapter 1 · HTML Foundations",
                "Text content",
                "Add Headings and Paragraphs",
                "Organize readable content with headings and paragraphs.",
                "Add the first title and introduction.",
                "index.html",
                "Content hierarchy",
                new[] { "<h1> through <h6>", "<p>", "heading order", "readable content" }
            ),
            new HtmlCssRoadmapModule(
                3,
                "Chapter 1 · HTML Foundations",
                "Links",
                "Connect the Workshop Navigation",
                "Create links with descriptive anchor text.",
                "Turn navigation labels into working links.",
                "index.html",
                "Navigation links",
                new[] { "<a>", "href", "link text", "page anchors" }
            ),
            new HtmlCssRoadmapModule(
                4,
                "Chapter 1 · HTML Foundations",
                "Images",
                "Place an Accessible Project Image",
                "Add an image with useful alternative text.",
                "Add the first project preview image.",
                "index.html",
                "Project image",
                new[] { "<img>", "src", "alt", "image dimensions" }
            ),
            new HtmlCssRoadmapModule(
                5,
                "Chapter 1 · HTML Foundations",
                "Lists",
                "Build the Feature List",
                "Represent grouped information with ordered and unordered lists.",
                "Display Workshop features in a structured list.",
                "index.html",
                "Feature list",
                new[] { "<ul>", "<ol>", "<li>", "list meaning" }
            ),
            new HtmlCssRoadmapModule(
                6,
                "Chapter 1 · HTML Foundations",
                "Semantic sections",
                "Divide the Page into Meaningful Regions",
                "Use semantic elements for each major page region.",
                "Create header, main, section, and footer regions.",
                "index.html",
                "Semantic regions",
                new[] { "<header>", "<main>", "<section>", "<footer>" }
            ),
            new HtmlCssRoadmapModule(
                7,
                "Chapter 1 · HTML Foundations",
                "Navigation",
                "Build the Complete Site Header",
                "Combine branding, navigation, and semantic structure.",
                "Complete the brand bar and primary navigation.",
                "index.html",
                "Site header",
                new[] { "<nav>", "landmarks", "logo text", "navigation structure" }
            ),
            new HtmlCssRoadmapModule(
                8,
                "Chapter 1 · HTML Foundations",
                "HTML integration",
                "Complete the First Workshop Page",
                "Combine the chapter's HTML into one accessible landing page.",
                "Finish the unstyled but fully structured homepage.",
                "index.html",
                "HTML landing page",
                new[] { "document structure", "semantic HTML", "links and images", "content organization" }
            ),
            new HtmlCssRoadmapModule(
                9,
                "Chapter 2 · CSS Foundations",
                "Stylesheets",
                "Connect the Workshop Stylesheet",
                "Link an external stylesheet and understand how CSS reaches HTML.",
                "Activate the first visual style layer.",
                "styles.css",
                "Stylesheet connection",
                new[] { "<link>", "rel=\"stylesheet\"", "CSS file path", "separation of concerns" }
            ),
            new HtmlCssRoadmapModule(
                10,
                "Chapter 2 · CSS Foundations",
                "Selectors",
                "Target the Right Page Elements",
                "Use element, class, and ID selectors.",
                "Create reusable selectors for interface parts.",
                "styles.css",
                "Selector system",
                new[] { "element selectors", "class selectors", "ID selectors", "specificity" }
            ),
            new HtmlCssRoadmapModule(
                11,
                "Chapter 2 · CSS Foundations",
                "Color",
                "Create the Workshop Color System",
                "Apply readable foreground, background, and accent colors.",
                "Give the Workshop a coherent visual identity.",
                "styles.css",
                "Color palette",
                new[] { "color", "background", "hex values", "contrast" }
            ),
            new HtmlCssRoadmapModule(
                12,
                "Chapter 2 · CSS Foundations",
                "Typography",
                "Style the Workshop Typography",
                "Control font family, size, weight, and line height.",
                "Create a consistent title and body hierarchy.",
                "styles.css",
                "Typography system",
                new[] { "font-family", "font-size", "font-weight", "line-height" }
            ),
            new HtmlCssRoadmapModule(
                13,
                "Chapter 2 · CSS Foundations",
                "Box model",
                "Control Spacing with the Box Model",
                "Use content, padding, border, and margin deliberately.",
                "Space sections and cards consistently.",
                "styles.css",
                "Spacing system",
                new[] { "content box", "padding", "border", "margin" }
            ),
            new HtmlCssRoadmapModule(
                14,
                "Chapter 2 · CSS Foundations",
                "Visual surfaces",
                "Add Borders, Radius, and Shadows",
                "Build clear surfaces with borders, rounded corners, and depth.",
                "Turn plain sections into interface cards.",
                "styles.css",
                "Card surfaces",
                new[] { "border", "border-radius", "box-shadow", "surface hierarchy" }
            ),
            new HtmlCssRoadmapModule(
                15,
                "Chapter 2 · CSS Foundations",
                "Interactive styles",
                "Design Buttons and Hover States",
                "Style normal, hover, focus, and disabled states.",
                "Create polished Workshop actions.",
                "styles.css",
                "Interactive controls",
                new[] { "button styles", ":hover", ":focus-visible", ":disabled" }
            ),
            new HtmlCssRoadmapModule(
                16,
                "Chapter 2 · CSS Foundations",
                "CSS integration",
                "Style the Complete Project Card",
                "Combine CSS foundations into one reusable card.",
                "Finish the first polished project showcase card.",
                "styles.css",
                "Project card",
                new[] { "selectors", "color", "typography", "box model" }
            ),
            new HtmlCssRoadmapModule(
                17,
                "Chapter 3 · Layout Systems",
                "Display",
                "Understand Block and Inline Layout",
                "Compare block, inline, and inline-block behavior.",
                "Correct the alignment of labels and links.",
                "layout.css",
                "Display behavior",
                new[] { "display: block", "display: inline", "inline-block", "normal flow" }
            ),
            new HtmlCssRoadmapModule(
                18,
                "Chapter 3 · Layout Systems",
                "Flexbox",
                "Create a Flexible Navigation Row",
                "Use Flexbox to arrange items along one dimension.",
                "Align the logo and navigation in one row.",
                "layout.css",
                "Flex navigation",
                new[] { "display: flex", "flex-direction", "gap", "flex items" }
            ),
            new HtmlCssRoadmapModule(
                19,
                "Chapter 3 · Layout Systems",
                "Flex alignment",
                "Align and Distribute Interface Items",
                "Control main-axis and cross-axis alignment.",
                "Balance headers, cards, and action rows.",
                "layout.css",
                "Flex alignment",
                new[] { "justify-content", "align-items", "alignment axes", "space distribution" }
            ),
            new HtmlCssRoadmapModule(
                20,
                "Chapter 3 · Layout Systems",
                "Flex wrapping",
                "Make the Card Row Wrap Safely",
                "Allow flexible items to wrap without overflow.",
                "Build an adaptive project-card row.",
                "layout.css",
                "Wrapping card row",
                new[] { "flex-wrap", "flex-basis", "min-width", "responsive rows" }
            ),
            new HtmlCssRoadmapModule(
                21,
                "Chapter 3 · Layout Systems",
                "CSS Grid",
                "Build a Two-Dimensional Project Grid",
                "Use Grid to control rows and columns together.",
                "Create the project gallery.",
                "layout.css",
                "Project grid",
                new[] { "display: grid", "grid-template-columns", "gap", "repeat()" }
            ),
            new HtmlCssRoadmapModule(
                22,
                "Chapter 3 · Layout Systems",
                "Grid areas",
                "Name the Dashboard Layout Regions",
                "Use named grid areas for maintainable structure.",
                "Arrange header, sidebar, content, and preview.",
                "layout.css",
                "Dashboard regions",
                new[] { "grid-template-areas", "grid-area", "layout maps", "maintainability" }
            ),
            new HtmlCssRoadmapModule(
                23,
                "Chapter 3 · Layout Systems",
                "Positioning",
                "Place an Interface Badge Precisely",
                "Use relative and absolute positioning safely.",
                "Attach a status badge to a card.",
                "layout.css",
                "Status badge",
                new[] { "position: relative", "position: absolute", "inset properties", "stacking" }
            ),
            new HtmlCssRoadmapModule(
                24,
                "Chapter 3 · Layout Systems",
                "Layout integration",
                "Assemble the Workshop Dashboard",
                "Combine normal flow, Flexbox, Grid, and positioning.",
                "Finish the desktop Workshop dashboard.",
                "layout.css",
                "Desktop dashboard",
                new[] { "normal flow", "Flexbox", "CSS Grid", "positioning" }
            ),
            new HtmlCssRoadmapModule(
                25,
                "Chapter 4 · Responsive Interfaces and Forms",
                "Responsive units",
                "Use Flexible Sizes",
                "Compare fixed and relative units for adaptable interfaces.",
                "Make spacing and typography scale naturally.",
                "responsive.css",
                "Flexible sizing",
                new[] { "px", "rem", "%", "vw and clamp()" }
            ),
            new HtmlCssRoadmapModule(
                26,
                "Chapter 4 · Responsive Interfaces and Forms",
                "Media queries",
                "Adapt the Workshop at a Breakpoint",
                "Apply styles when the viewport meets a condition.",
                "Stack the dashboard on small screens.",
                "responsive.css",
                "Responsive breakpoint",
                new[] { "@media", "max-width", "mobile-first design", "breakpoints" }
            ),
            new HtmlCssRoadmapModule(
                27,
                "Chapter 4 · Responsive Interfaces and Forms",
                "Mobile navigation",
                "Rebuild Navigation for Small Screens",
                "Design compact navigation that remains usable.",
                "Create the mobile header and menu layout.",
                "responsive.css",
                "Mobile navigation",
                new[] { "responsive navigation", "touch targets", "wrapping", "small-screen hierarchy" }
            ),
            new HtmlCssRoadmapModule(
                28,
                "Chapter 4 · Responsive Interfaces and Forms",
                "Forms",
                "Build the Project Request Form",
                "Create labels, inputs, text areas, and buttons.",
                "Add a usable project-request form.",
                "index.html",
                "Request form",
                new[] { "<form>", "<label>", "<input>", "<textarea>" }
            ),
            new HtmlCssRoadmapModule(
                29,
                "Chapter 4 · Responsive Interfaces and Forms",
                "Accessible interaction",
                "Make Form Focus Visible",
                "Create clear focus, error, help, and disabled states.",
                "Add accessible feedback to controls.",
                "forms.css",
                "Form states",
                new[] { ":focus-visible", "labels", "help text", "error states" }
            ),
            new HtmlCssRoadmapModule(
                30,
                "Chapter 4 · Responsive Interfaces and Forms",
                "Data tables",
                "Display Project Data Clearly",
                "Use tables for real row-and-column information.",
                "Add a responsive project-status table.",
                "index.html",
                "Project table",
                new[] { "<table>", "<thead>", "<tbody>", "table headings" }
            ),
            new HtmlCssRoadmapModule(
                31,
                "Chapter 4 · Responsive Interfaces and Forms",
                "CSS variables",
                "Create Reusable Theme Tokens",
                "Store shared colors and spacing in custom properties.",
                "Prepare the Workshop for multiple themes.",
                "themes.css",
                "Theme tokens",
                new[] { "--custom-properties", "var()", "shared tokens", "fallback values" }
            ),
            new HtmlCssRoadmapModule(
                32,
                "Chapter 4 · Responsive Interfaces and Forms",
                "Responsive integration",
                "Complete the Responsive Control Panel",
                "Combine responsive layout, forms, tables, and variables.",
                "Finish the control panel from desktop through mobile.",
                "responsive.css",
                "Responsive control panel",
                new[] { "responsive layout", "forms", "tables", "theme variables" }
            ),
            new HtmlCssRoadmapModule(
                33,
                "Chapter 5 · Polished Web Application",
                "Pseudo-classes",
                "Style Interface States",
                "Use pseudo-classes to style elements by state.",
                "Add active, checked, and invalid states.",
                "components.css",
                "Component states",
                new[] { ":first-child", ":last-child", ":checked", ":invalid" }
            ),
            new HtmlCssRoadmapModule(
                34,
                "Chapter 5 · Polished Web Application",
                "Pseudo-elements",
                "Add Decorative Interface Details",
                "Create visual details without extra HTML.",
                "Add accent lines, icons, and card layers.",
                "components.css",
                "Decorative details",
                new[] { "::before", "::after", "content", "decorative styling" }
            ),
            new HtmlCssRoadmapModule(
                35,
                "Chapter 5 · Polished Web Application",
                "Transitions",
                "Smooth Important State Changes",
                "Animate changes without distracting the user.",
                "Smooth button, card, and theme transitions.",
                "motion.css",
                "Transitions",
                new[] { "transition-property", "duration", "timing function", "reduced motion" }
            ),
            new HtmlCssRoadmapModule(
                36,
                "Chapter 5 · Polished Web Application",
                "Animations",
                "Create a Purposeful Loading Animation",
                "Define keyframes that communicate progress.",
                "Add restrained loading and status motion.",
                "motion.css",
                "Status animation",
                new[] { "@keyframes", "animation", "iteration count", "motion purpose" }
            ),
            new HtmlCssRoadmapModule(
                37,
                "Chapter 5 · Polished Web Application",
                "Reusable components",
                "Build a Reusable UI Component Library",
                "Create consistent buttons, cards, badges, and fields.",
                "Turn the Workshop into a design system.",
                "components.css",
                "Component library",
                new[] { "component classes", "variants", "shared rules", "consistency" }
            ),
            new HtmlCssRoadmapModule(
                38,
                "Chapter 5 · Polished Web Application",
                "Theme switching",
                "Support Light and Dark Themes",
                "Use shared tokens to preserve contrast across modes.",
                "Add complete light and dark themes.",
                "themes.css",
                "Theme switching",
                new[] { "data attributes", "color-scheme", "theme tokens", "contrast" }
            ),
            new HtmlCssRoadmapModule(
                39,
                "Chapter 5 · Polished Web Application",
                "Quality",
                "Audit Accessibility and Performance",
                "Review semantics, keyboard access, contrast, and efficiency.",
                "Complete the Workshop quality checklist.",
                "index.html / styles.css",
                "Quality audit",
                new[] { "semantic review", "keyboard testing", "contrast", "performance" }
            ),
            new HtmlCssRoadmapModule(
                40,
                "Chapter 5 · Polished Web Application",
                "Final integration",
                "Launch the Complete Interface Workshop",
                "Integrate the whole course into one polished responsive app.",
                "Complete the final publish-ready interface.",
                "index.html / styles.css",
                "Complete Workshop",
                new[] { "HTML architecture", "responsive CSS", "accessibility", "design-system integration" }
            )
        };

    public static HtmlCssRoadmapModule Get(int moduleIndex)
    {
        if (moduleIndex < 0 || moduleIndex >= Modules.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(moduleIndex),
                moduleIndex,
                "The HTML/CSS roadmap module index is invalid.");
        }

        return Modules[moduleIndex];
    }
}
